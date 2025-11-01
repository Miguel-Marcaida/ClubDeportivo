using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClubDeportivo.UI.BLL
{
    // Clase BLL dedicada al control y reportes de cuotas pendientes (Morosos/Vencimiento Hoy)
    public class SocioEstadoCuotaBLL
    {
        // Instancia de la DAL específica para esta lógica
        private readonly SocioEstadoCuotaDAL oSocioEstadoCuotaDAL = new SocioEstadoCuotaDAL();

        /// <summary>
        /// Obtiene y mapea el listado maestro de socios con cuotas pendientes (morosos o por vencer).
        /// Este es el dataset principal para los filtros en la UI.
        /// </summary>
        /// <returns>Lista de SocioEstadoCuotaDTOs listos para la UI.</returns>
        public List<SocioEstadoCuotaDTO> ObtenerListadoMaestro()
        {
            List<SocioEstadoCuotaDTO> lista = new List<SocioEstadoCuotaDTO>();

            try
            {
                // 1. Obtener el DataTable de la DAL (SELECT de la VIEW v_socios_estado_cuota)
                DataTable dt = oSocioEstadoCuotaDAL.ObtenerListadoMaestroCuotas();

                // 2. Iterar sobre las filas y mapear al DTO
                foreach (DataRow row in dt.Rows)
                {
                    SocioEstadoCuotaDTO dto = new SocioEstadoCuotaDTO
                    {
                        // Mapeo de Datos de Persona/Socio
                        IdPersona = Convert.ToInt32(row["id_persona"]),
                        Dni = row["dni"].ToString(),
                        NombreCompleto = row["nombre_completo"].ToString(), // Campo concatenado
                        Telefono = row["telefono"].ToString(),
                        Email = row["email"].ToString(),
                        NumeroCarnet = Convert.ToInt32(row["numero_carnet"]),
                        EstadoActivo = Convert.ToBoolean(row["estado_activo"]),
                        EstaVigente = Convert.ToBoolean(row["esta_vigente"])
                    };

                    // Mapeo de Campos de Cuota (CRÍTICO: Manejar DBNull.Value para los Nullable)
                    // Mapeo de MesesMora (siempre tendrá valor gracias al GREATEST(0,...) en la VIEW)
                    // Asegúrate de que este bloque esté DENTRO del 'foreach (DataRow row in dt.Rows)'
                    if (row["meses_mora"] != DBNull.Value)
                    {
                        dto.MesesMora = Convert.ToInt32(row["meses_mora"]);
                    }
                    // NOTA: Si usas la lógica de "si la persona nunca pagó, MesesMora es 999", 
                    // este bloque es CRÍTICO para capturar ese valor.

                    // id_cuota_pendiente
                    if (row["id_cuota_pendiente"] != DBNull.Value)
                    {
                        dto.IdCuotaPendiente = Convert.ToInt32(row["id_cuota_pendiente"]);

                        // fecha_vencimiento_pendiente (Solo se mapea si existe una cuota pendiente)
                        if (row["fecha_vencimiento_pendiente"] != DBNull.Value)
                        {
                            dto.FechaVencimientoPendiente = Convert.ToDateTime(row["fecha_vencimiento_pendiente"]);
                        }

                        // monto_cuota_pendiente
                        if (row["monto_cuota_pendiente"] != DBNull.Value)
                        {
                            dto.MontoCuotaPendiente = Convert.ToDecimal(row["monto_cuota_pendiente"]);
                        }
                        /*
                        // dias_mora
                        if (row["dias_mora"] != DBNull.Value)
                        {
                            dto.DiasMora = Convert.ToInt32(row["dias_mora"]);
                        }*/
                    }
                    
                    // fecha_pago_ultima (Puede ser NULL si es un socio muy nuevo o moroso de larga data)
                    if (row["fecha_pago_ultima"] != DBNull.Value)
                    {
                        dto.FechaPagoUltima = Convert.ToDateTime(row["fecha_pago_ultima"]);
                    }

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error BLL al obtener y procesar el Listado Maestro de Cuotas: " + ex.Message);
            }

            return lista;
        }
    }
}