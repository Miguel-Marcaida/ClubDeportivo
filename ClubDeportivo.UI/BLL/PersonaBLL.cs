using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.Utilitarios;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ClubDeportivo.UI.BLL
{

    public class PersonaBLL
    {
        // Instancias de la capa de datos/lógica necesarias
        private readonly PersonaDAL oPersonaDAL = new PersonaDAL();
        private readonly SocioDAL oSocioDAL = new SocioDAL();
        private readonly NoSocioDAL oNoSocioDAL = new NoSocioDAL();

        // INSTANCIAS CRÍTICAS para la lógica de pagos (¡ACTUALIZADO!)
        private readonly CuotaBLL oCuotaBLL = new CuotaBLL();
        private readonly RegistroAccesoBLL oRegistroAccesoBLL = new RegistroAccesoBLL();
        private readonly ConfiguracionBLL oConfiguracionBLL = new ConfiguracionBLL();


        /// <summary>
        /// Implementa la regla de negocio: (Total Socios + 1) + 100 de offset
        /// </summary>
        public int ObtenerProximoNumeroCarnet()
        {
            try
            {
                // 1. Obtener el total actual de socios registrados.
                int totalSocios = oSocioDAL.ContarSocios();

                // 2. Aplicar la regla de negocio (Contador + 100 + 1)
                // Usamos 101 ya que el contador es BASE CERO.
                int proximoCarnet = totalSocios + 101;

                return proximoCarnet;
            }
            catch (Exception ex)
            {
                throw new Exception("Error BLL al obtener el próximo número de carnet: " + ex.Message);
            }
        }


        // 1. MÉTODO PARA REGISTRO Y PAGO DE SOCIO (RegistrarInscripcionSocio)
        public int RegistrarInscripcionSocio(Socio oSocio, string formaPago)
        {
            if (oSocio == null) throw new ArgumentNullException(nameof(oSocio));
            if (string.IsNullOrEmpty(formaPago)) throw new Exception("La forma de pago es obligatoria.");

            try
            {
                // 1. Verificar si existe la persona por DNI
                int idGenerado = oPersonaDAL.ObtenerIdPersonaPorDni(oSocio.Dni);

                if (idGenerado > 0)
                {
                    throw new Exception($"El DNI {oSocio.Dni} ya está registrado en el sistema. Debe registrar una persona nueva.");
                }

                // 2. Registrar la Persona (obtiene el ID)
                idGenerado = oPersonaDAL.RegistrarPersona(oSocio);
                oSocio.IdPersona = idGenerado; // Asignar el ID generado al objeto

                // 3. Registrar como Socio
                oSocioDAL.RegistrarSocio(oSocio);

                // 4. Registrar la PRIMERA CUOTA (Llamada al BLL)
                decimal montoCuota = oConfiguracionBLL.ObtenerMontoCuotaBase(); // La BLL obtiene el monto

                Cuota cuotaInicial = new Cuota
                {
                    IdPersona = oSocio.IdPersona,
                    Monto = montoCuota,
                    FechaPago = DateTime.Today,
                    FechaVencimiento = DateTime.Today.AddMonths(1).AddDays(-1),
                    FormaPago = formaPago,
                    Concepto = $"Cuota Inicial {DateTime.Today.ToString("MMMM yyyy")}" // Concepto generado
                };

                int idCuotaGenerada = oCuotaBLL.RegistrarPagoDeCuota(cuotaInicial);

                if (idCuotaGenerada <= 0)
                {
                    throw new Exception("Error CRÍTICO al registrar la PRIMERA CUOTA. La inscripción de Socio se completó, pero el pago falló. Revisar tabla 'cuotas'.");
                }

                return idGenerado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Lógica de Negocio (BLL) al registrar la inscripción de Socio: " + ex.Message);
            }
        }


        // 2. MÉTODO PARA REGISTRO Y PAGO DE NO SOCIO (RegistrarAccesoDiarioNoSocio)
        public int RegistrarAccesoDiarioNoSocio(NoSocio oNoSocio, string formaPago)
        {
            if (oNoSocio == null) throw new ArgumentNullException(nameof(oNoSocio));
            if (string.IsNullOrEmpty(formaPago)) throw new Exception("La forma de pago es obligatoria.");

            try
            {
                // 1. Verificar si existe la persona por DNI
                int idGenerado = oPersonaDAL.ObtenerIdPersonaPorDni(oNoSocio.Dni);

                if (idGenerado > 0)
                {
                    throw new Exception($"El DNI {oNoSocio.Dni} ya está registrado en el sistema. Debe registrar una persona nueva.");
                }

                // 2. Registrar la Persona (obtiene el ID)
                idGenerado = oPersonaDAL.RegistrarPersona(oNoSocio);
                oNoSocio.IdPersona = idGenerado; // Asignar el ID generado al objeto

                // 3. Registrar como No Socio
                oNoSocio.FechaPagoDia = DateTime.Today; // Se marca como pagado hoy
                oNoSocioDAL.RegistrarNoSocio(oNoSocio);

                // 4. Registrar el Pago de Acceso Diario
                decimal montoAcceso = oConfiguracionBLL.ObtenerMontoAccesoDiario(); // La BLL obtiene el monto

                RegistroAcceso registroAcceso = new RegistroAcceso
                {
                    IdPersona = oNoSocio.IdPersona,
                    Monto = montoAcceso,
                    Fecha = DateTime.Now, // Fecha y hora actual
                    FormaPago = formaPago
                };

                int idRegistroGenerado = oRegistroAccesoBLL.RegistrarAccesoYpago(registroAcceso);

                if (idRegistroGenerado <= 0)
                {
                    throw new Exception("Error CRÍTICO al registrar el PAGO DE ACCESO DIARIO. El registro de No Socio se completó, pero el pago falló. Revisar tabla 'registros_acceso'.");
                }

                return idGenerado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la Lógica de Negocio (BLL) al registrar el Acceso Diario: " + ex.Message);
            }
        }



        public List<PersonaListadoDTO> ObtenerListadoGeneral()
        {
            List<PersonaListadoDTO> lista = new List<PersonaListadoDTO>();

            try
            {
                // 1. Obtener el DataTable de la DAL (Contiene los nuevos campos de pago)
                DataTable dt = oPersonaDAL.ObtenerListadoUnificado();

                // --- Definiciones de fechas para la lógica de mora ---
                DateTime hoy = DateTime.Today; // 2025-11-04
                                               // Primer día del mes actual (ej: 2025-11-01)
                DateTime inicioMesActual = new DateTime(hoy.Year, hoy.Month, 1);

                // 2. Iterar sobre las filas y aplicar la Lógica de Negocio
                foreach (DataRow row in dt.Rows)
                {
                    PersonaListadoDTO dto = new PersonaListadoDTO
                    {
                        // Mapeo directo de columnas de la VIEW
                        IdPersona = Convert.ToInt32(row["IdPersona"]),
                        Dni = row["Dni"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        TipoPersona = row["TipoPersona"].ToString()
                    };

                    // Manejo del Carnet (Puede ser NULL si es No Socio)
                    if (row["NumeroCarnet"] != DBNull.Value)
                    {
                        dto.NumeroCarnet = Convert.ToInt32(row["NumeroCarnet"]);
                    }
                    else
                    {
                        dto.NumeroCarnet = null;
                    }

                    // --- LÓGICA CRÍTICA CORREGIDA: CALCULAR ESTADO DE MEMBRESÍA ---
                    if (dto.TipoPersona == "Socio")
                    {
                        // 1. Lectura segura de los nuevos campos de la vista (DBNull.Value handling)

                        // a) Fecha del último pago efectivo (NULL si nunca pagó)
                        DateTime? fechaUltimoPago = row["fecha_pago_ultima"] != DBNull.Value
                                                   ? (DateTime?)Convert.ToDateTime(row["fecha_pago_ultima"])
                                                   : null;

                        // b) Existencia de cuota pendiente (es TRUE si id_cuota_pendiente NO es NULL)
                        bool tieneCuotasPendientes = row["id_cuota_pendiente"] != DBNull.Value;


                        if (fechaUltimoPago.HasValue && fechaUltimoPago.Value >= inicioMesActual)
                        {
                            // REGLA CLAVE: Si pagó en el mes actual (Noviembre 2025), está AL DÍA.
                            // (Aquí cae Lucía Vera, que pagó hoy)
                            dto.EstadoMembresia = "AL DÍA";
                        }
                        else if (!fechaUltimoPago.HasValue && tieneCuotasPendientes)
                        {
                            // REGLA: Nunca ha pagado (NULL en fecha_pago_ultima) y tiene deuda.
                            dto.EstadoMembresia = "MOROSO";
                        }
                        else if (tieneCuotasPendientes)
                        {
                            // REGLA: Tiene cuotas pendientes y su último pago es anterior al mes actual.
                            // (Aquí caen Fede, Ana, Raúl, etc. Fede es moroso porque su pago fue Octubre).
                            dto.EstadoMembresia = "MOROSO";
                        }
                        else
                        {
                            // REGLA: Está al día (o al menos no tiene cuotas pendientes). 
                            // Esto cubre el caso donde el último pago fue hace mucho, pero la cuota 
                            // del mes actual aún no se genera o no tiene deuda.
                            dto.EstadoMembresia = "AL DÍA";
                        }
                    }
                    else
                    {
                        // No Socio no tiene estado de cuota
                        dto.EstadoMembresia = "N/A";
                    }
                    // --- FIN DE LÓGICA CRÍTICA ---

                    lista.Add(dto);
                }
            }
            catch (Exception ex)
            {
                // Es buena práctica incluir el nombre del método para el debug.
                throw new Exception("Error BLL al procesar el listado de personas en ObtenerListadoGeneral: " + ex.Message);
            }

            return lista;
        }

        // Dentro de la clase PersonaBLL
        public bool DarDeBajaPersona(int idPersona)
        {
            // Aquí puedes añadir reglas de negocio futuras: 
            // ej: if (idPersona == 1) { throw new Exception("No se puede dar de baja al administrador"); }

            try
            {
                // Llamada a la DAL para ejecutar el Soft Delete
                return oPersonaDAL.DarDeBajaPersonaLogica(idPersona);
            }
            catch (Exception ex)
            {
                throw new Exception("Error BLL al procesar la baja de la persona: " + ex.Message);
            }
        }

        public PersonaDetalleDTO ObtenerDetallePersona(int idPersona)
        {
            PersonaDetalleDTO detalle = null;

            try
            {
                // 1. Obtener el DataTable de la DAL (Ejecución del Stored Procedure)
                DataTable dt = oPersonaDAL.ObtenerDetallePersonaSP(idPersona);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    detalle = new PersonaDetalleDTO();

                    // Mapeo de Campos Básicos (Comunes a Socio y No Socio)
                    detalle.IdPersona = Convert.ToInt32(row["IdPersona"]);
                    detalle.Dni = row["Dni"].ToString();
                    detalle.Nombre = row["Nombre"].ToString();
                    detalle.Apellido = row["Apellido"].ToString();
                    detalle.Telefono = row["Telefono"].ToString();
                    detalle.Email = row["Email"].ToString();
                    detalle.EstaVigente = Convert.ToBoolean(row["EstaVigente"]);

                    // CRÍTICO: Conversión de Fecha de Nacimiento (Asegurar que no sea DBNull)
                    if (row["FechaNacimiento"] != DBNull.Value)
                    {
                        detalle.FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]);

                    }
                    // NOTA: Si la fecha es null, el DTO usa el valor default (DateTime.MinValue), esto es aceptable.


                    // 2. Lógica de Negocio: Determinar si es Socio
                    // Si IdSocio tiene un valor (no es DBNull), entonces es Socio.
                    if (row["IdSocio"] != DBNull.Value)
                    {
                        detalle.EsSocio = true;
                        detalle.IdSocio = Convert.ToInt32(row["IdSocio"]);

                        // Mapeo del Carnet (solo si es Socio)
                        if (row["NumeroCarnet"] != DBNull.Value)
                        {
                            detalle.NumeroCarnet = Convert.ToInt32(row["NumeroCarnet"]);
                        }
                        // Dentro del bloque 'if (row["IdSocio"] != DBNull.Value)'
                        detalle.EstadoActivo = Convert.ToBoolean(row["EstadoActivo"]);
                        detalle.FichaMedicaEntregada = Convert.ToBoolean(row["FichaMedicaEntregada"]);

                    }
                    else
                    {
                        detalle.EsSocio = false;
                        detalle.IdSocio = null;
                        detalle.NumeroCarnet = null;

                        if (row["FechaPagoDia"] != DBNull.Value)
                        {
                            // El DTO debe estar listo para recibir un DateTime? (Nullable)
                            detalle.FechaPagoDia = Convert.ToDateTime(row["FechaPagoDia"]);
                        }
                        else
                        {
                            detalle.FechaPagoDia = null; // Aseguramos que sea nulo si no hay fecha
                        }
                    }
                }
                else
                {
                    // Si no se encontró la persona
                    throw new Exception($"La persona con ID {idPersona} no fue encontrada en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error BLL al obtener el detalle de la persona: " + ex.Message);
            }

            return detalle;
        }

        public string ActualizarDatosPersona(PersonaDetalleDTO detalle)
        {
            // Instancia de la DAL (asumo que 'oPersonaDAL' ya existe como campo de la clase)
            // private readonly PersonaDAL oPersonaDAL = new PersonaDAL(); 

            try
            {
                // =========================================================
                // 1. VALIDACIÓN BÁSICA DE DATOS
                // =========================================================
                if (detalle == null)
                {
                    return "Error: El objeto de detalle de persona es nulo.";
                }
                if (detalle.IdPersona <= 0)
                {
                    return "Error: ID de persona inválido para la actualización.";
                }
                if (string.IsNullOrWhiteSpace(detalle.Dni) || string.IsNullOrWhiteSpace(detalle.Nombre) || string.IsNullOrWhiteSpace(detalle.Apellido))
                {
                    return "Error: DNI, Nombre y Apellido son campos obligatorios.";
                }

                // =========================================================
                // 2. REGLA CRÍTICA: VERIFICACIÓN DE UNICIDAD DEL DNI
                // =========================================================
                // Llamamos a la DAL para ejecutar el SP 'VerificarDniExistente'
                int dnisEncontrados = oPersonaDAL.VerificarDniExistente(detalle.Dni, detalle.IdPersona);

                if (dnisEncontrados > 0)
                {
                    // El DNI ya está siendo utilizado por otra persona vigente.
                    return $"Error de Negocio: El DNI '{detalle.Dni}' ya se encuentra registrado en el sistema para otra persona vigente.";
                }


                // =========================================================
                // 3. EJECUCIÓN DE LA ACTUALIZACIÓN EN LA DAL
                // =========================================================
                // Llamamos a la DAL para ejecutar el SP 'ActualizarDatosPersona'
                string resultadoDal = oPersonaDAL.ActualizarDatosPersonaSP(detalle);

                if (resultadoDal == "OK")
                {
                    return "OK"; // Éxito en la actualización.
                }
                else
                {
                    // La DAL devolvió un error de SQL o ejecución.
                    return "Error al actualizar la persona en la base de datos: " + resultadoDal;
                }

            }
            catch (Exception ex)
            {
                // Captura cualquier error inesperado de la BLL o un error grave en la DAL (ej. conexión)
                return "Error crítico en la lógica de negocio al actualizar la persona: " + ex.Message;
            }
        }



        public PersonaPagoDetalleDTO BuscarPersonaParaPago(string identificador)
        {
            try
            {
                // 1. Obtener Datos Brutos de la DAL
                PersonaPagoDetalleDTO detalle = oPersonaDAL.BuscarPersonaDetalle(identificador);

                if (detalle == null)
                {
                    return null; // No se encontró a la persona
                }

                // 2. Aplicar Lógica de Negocio según el tipo de persona.
                if (detalle.EsSocio)
                {
                    // Lógica para SOCIO

                    DateTime ultimaCuotaPagada = detalle.UltimaCuotaCubierta;
                    DateTime hoy = DateTime.Today;
                    int mesesAtraso = 0;

                    // CRÍTICO: Si UltimaCuotaCubierta es el valor por defecto (no hay pagos)
                    // Este caso aplica si la persona fue registrada pero la DAL no encontró pagos.
                    if (ultimaCuotaPagada == DateTime.MinValue || ultimaCuotaPagada == default(DateTime))
                    {
                        // Se asume 1 mes de mora mínimo para un socio que debe.
                        mesesAtraso = 1;

                        // Ajuste para el cálculo: Si nunca pagó, fingimos que el mes pasado fue su último pago
                        // para que la UI sepa que debe el mes actual.
                        ultimaCuotaPagada = hoy.AddMonths(-1).AddDays(1);
                    }

                    // Solo procedemos al cálculo si hay un historial de pago válido
                    if (mesesAtraso == 0)
                    {
                        // La fecha de corte es el día siguiente al mes pagado.
                        // Ej: Pago 25/07/2025. Mes cubierto es Julio.
                        // La cuota que DEBE pagar es la que inicia en Agosto.
                        DateTime mesQueDebePagar = ultimaCuotaPagada.AddMonths(1);

                        // 2. Comprobar si está AL DÍA.
                        // Está al día si el mes que DEBE pagar (mesQueDebePagar) es FUTURO con respecto al mes actual.
                        if (mesQueDebePagar.Year > hoy.Year || (mesQueDebePagar.Year == hoy.Year && mesQueDebePagar.Month > hoy.Month))
                        {
                            // AL DÍA: No hay mora. El contador es 0.
                            mesesAtraso = 0;
                        }
                        else
                        {
                            
                            int diferenciaAnios = hoy.Year - mesQueDebePagar.Year;
                            int diferenciaMeses = hoy.Month - mesQueDebePagar.Month;

                            mesesAtraso = (diferenciaAnios * 12) + diferenciaMeses + 1;

                            // Nos aseguramos de que el mínimo de mora sea 1 si ya entró en el ELSE.
                            if (mesesAtraso <= 0)
                            {
                                mesesAtraso = 1;
                            }
                        }
                    }


                    // 3. Asignar el Estado Final
                    if (mesesAtraso > 0)
                    {
                        // PENDIENTE / MORA
                        detalle.EstadoMembresia = $"PENDIENTE: MORA ({mesesAtraso} meses de atraso)";
                    }
                    else
                    {
                        // AL DÍA
                        DateTime mesQueDebePagar = ultimaCuotaPagada.AddMonths(1);
                        detalle.EstadoMembresia = $"AL DÍA (Próxima cuota a pagar: {mesQueDebePagar.ToString("MMMM yyyy")})";
                    }
                }
                else
                {
                    // Lógica para NO SOCIO o VENTA.
                    detalle.EstadoMembresia = "N/A - Acceso Diario";
                }

                return detalle;
            }
            catch (Exception ex)
            {
                // Captura errores de DAL o errores de cálculo
                throw new Exception("Error BLL al procesar los datos para el pago: " + ex.Message);
            }
        }


    }

}

