using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace ClubDeportivo.UI.BLL
{
    // Clase BLL dedicada al control y reportes de cuotas pendientes (Morosos/Vencimiento Hoy)
    public class SocioEstadoCuotaBLL
    {
        // Instancia de la DAL específica para esta lógica
        private readonly SocioEstadoCuotaDAL oSocioEstadoCuotaDAL = new SocioEstadoCuotaDAL();


        public List<SocioEstadoCuotaDTO> ObtenerListadoMaestro()
        {
            List<SocioEstadoCuotaDTO> lista = new List<SocioEstadoCuotaDTO>();

            try
            {
                DataTable dt = oSocioEstadoCuotaDAL.ObtenerListadoMaestroCuotas();

                foreach (DataRow row in dt.Rows)
                {
                    SocioEstadoCuotaDTO dto = new SocioEstadoCuotaDTO
                    {
                        // Mapeo de Datos de Persona/Socio
                        IdPersona = Convert.ToInt32(row["id_persona"]),
                        Dni = row["dni"].ToString(),
                        NombreCompleto = row["nombre_completo"].ToString(),
                        Telefono = row["telefono"].ToString(), // Sin guiones
                        Email = row["email"].ToString(),
                        NumeroCarnet = Convert.ToInt32(row["numero_carnet"]),
                        EstadoActivo = Convert.ToBoolean(row["estado_activo"]),
                        EstaVigente = Convert.ToBoolean(row["esta_vigente"]),

                        // Mapeo inicial de MesesMora
                        MesesMora = Convert.ToInt32(row["meses_mora"]),

                        // Mapeo de Ultimo Pago (Nullable)
                        FechaPagoUltima = row["fecha_pago_ultima"] != DBNull.Value
                                          ? Convert.ToDateTime(row["fecha_pago_ultima"])
                                          : (DateTime?)null
                    };

                    // Lógica para Cuotas Pendientes
                    if (row["id_cuota_pendiente"] != DBNull.Value)
                    {
                        dto.IdCuotaPendiente = Convert.ToInt32(row["id_cuota_pendiente"]);

                        // Mapeo de Fecha Vencimiento Pendiente
                        if (row["fecha_vencimiento_pendiente"] != DBNull.Value)
                        {
                            dto.FechaVencimientoPendiente = Convert.ToDateTime(row["fecha_vencimiento_pendiente"]);
                        }

                        // 1. CORRECCIÓN CRÍTICA DE MORA: Si vence hoy y el SQL dio 1 (error), se corrige a 0.
                        bool venceHoy = dto.FechaVencimientoPendiente.HasValue &&
                                        dto.FechaVencimientoPendiente.Value.Date == DateTime.Today.Date;

                        if (venceHoy && dto.MesesMora == 1)
                        {
                            dto.MesesMora = 0; // Inés y Julián ahora tienen 0 meses de mora.
                        }

                        // 2. CÁLCULO DEL MONTO PENDIENTE (Adaptado a la mora corregida)
                        decimal montoCuotaUnica = row["monto_cuota_pendiente"] != DBNull.Value
                                                ? Convert.ToDecimal(row["monto_cuota_pendiente"])
                                                : 0M;

                        if (dto.MesesMora > 0)
                        {
                            // Mora Real (> 1 mes): Deuda Total (Raúl, Ana)
                            dto.MontoCuotaPendiente = montoCuotaUnica * dto.MesesMora;
                        }
                        else if (venceHoy)
                        {
                            // Vencimiento Hoy (Mora 0 Corregida): Monto a Pagar (Inés, Julián)
                            dto.MontoCuotaPendiente = montoCuotaUnica;
                        }
                        else
                        {
                            // Al Día (Mora 0 y no vence hoy): No deben nada (Laura)
                            dto.MontoCuotaPendiente = 0M;
                        }
                    }

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                // Se recomienda usar un sistema de log aquí
                throw new Exception("Error BLL al obtener y procesar el Listado Maestro de Cuotas: " + ex.Message, ex);
            }

            return lista;
        }
    }
}