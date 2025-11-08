using ClubDeportivo.UI.BLL; 
using ClubDeportivo.UI.Entidades; 
using ClubDeportivo.UI.Utilitarios;
using System.Data;
using System.Collections.Generic; 
namespace ClubDeportivo.UI
{
    public partial class FrmControlCuotas : Form
    {

        #region PROPIEDADES Y CAMPOS PRIVADOS
        // --- CAMPO CRÍTICO: Para centrado dinámico. ---
        private readonly int _anchoMenuLateral;

        private readonly SocioEstadoCuotaBLL oSocioEstadoCuotaBLL = new SocioEstadoCuotaBLL();
        private readonly PersonaBLL oPersonaBLL = new PersonaBLL();
        private List<SocioEstadoCuotaDTO> _listadoMaestro; // Guardará TODOS los morosos (la fuente de datos)
        private BindingSource _bindingSource = new BindingSource(); // NECESARIO para hacer el filtrado sin recargar datos

        #endregion

        #region CONTRUCTOR
        // 3. CONSTRUCTOR
        public FrmControlCuotas(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;

            ConfigurarFormulario();

            // Suscripciones Críticas
            this.Load += FrmListadoMorosos_Load;

        }

        #endregion

        #region CONFIGURACION UI


        private void ConfigurarFormulario()
        {
            EstablecerGeometriaYTextos();
            ConfigurarEstilos();
            ConfigurarDataGrid();
        }

        private void ConfigurarDataGrid()
        {
            // Asignamos el BindingSource como fuente de datos
            dgvMorosos.DataSource = _bindingSource;

            // Configuración visual básica
            EstilosGlobales.AplicarEstiloDataGridView(dgvMorosos);
        }
        private void ConfigurarEstilos()
        {
            EstilosGlobales.AplicarFormatoBase(this);
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;
            lblTitulo.ForeColor = EstilosGlobales.ColorError; // Usamos color de error para el título

            // Botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnEstadoCuenta);
            EstilosGlobales.AplicarEstiloBotonAccion(btnCerrar);
            
            // Configuramos los estilos y un color que denote 'Advertencia/Acción Urgente'
            EstilosGlobales.AplicarEstiloBotonAccion(btnImprimirMorosos);
            btnImprimirMorosos.BackColor = EstilosGlobales.ColorError; // Rojo para morosos
            btnImprimirMorosos.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorError; // Mantiene el color al pasar el mouse


            // Estilo para el DataGridView (usando el método que ya tienes)
            EstilosGlobales.AplicarEstiloDataGridView(dgvMorosos);
        }

        private void EstablecerGeometriaYTextos()
        {
            // --- CONFIGURACIÓN DEL PANEL BASE Y EL FORMULARIO ---
            pnlBase.Size = new Size(1000, 550); //
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            // --- 1. TÍTULO ---
            lblTitulo.Text = MensajesUI.CUOTAS_TITULO_FORM; // <--- CONSTANTE
            lblTitulo.Location = new Point(20, 20); 

            // --- 2. CONTROLES DE MODO PRINCIPAL (RadioButtons) ---
            // Posicionamos los RadioButtons cerca del título (izquierda)

            // 2a. Modo Vencimiento Hoy
            rbVencimientoHoy.Text = MensajesUI.CUOTAS_RB_VENCEN_HOY; // <--- CONSTANTE
            rbVencimientoHoy.Location = new Point(20, 70);
            rbVencimientoHoy.AutoSize = true; 

            // 2b. Modo Morosos (Será el modo por defecto o el que tenga el filtro de días)
            rbMorosos.Text = MensajesUI.CUOTAS_RB_MOROSOS; // <--- CONSTANTE
            rbMorosos.Location = new Point(180, 70); 
            rbMorosos.AutoSize = true;
            rbMorosos.Checked = true;

            // --- 4. LISTADO (DataGridView) ---
            dgvMorosos.Location = new Point(20, 120); 
            dgvMorosos.Size = new Size(960, 340);

            // --- 5. BOTONES DE ACCIÓN (Al pie) ---
            btnImprimirMorosos.Text = MensajesUI.CUOTAS_BTN_IMPRIMIR; // <--- CONSTANTE
            btnImprimirMorosos.Location = new Point(20, 480);
            btnImprimirMorosos.Size = new Size(300, 50);

            btnEstadoCuenta.Text = MensajesUI.CUOTAS_BTN_ESTADO_CUENTA; // <--- CONSTANTE
            btnEstadoCuenta.Location = new Point(340, 480); 
            btnEstadoCuenta.Size = new Size(300, 50); 

            btnCerrar.Text = MensajesUI.CUOTAS_BTN_CERRAR; // <--- CONSTANTE
            btnCerrar.Location = new Point(660, 480); 
            btnCerrar.Size = new Size(300, 50); 

        }



        #endregion

        #region EVENTOS DE FORMULARIO

        private void FrmListadoMorosos_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));
            CargarDatosMaestros();

            EstablecerModo(TipoListadoCuotas.Morosos);
        }

        #endregion

        #region VALIDACIONES ENTRADAS

       

        #endregion

        #region METODOS/LOGICA DE DATOS

        public enum TipoListadoCuotas
        {
            Morosos = 0,
            VencimientoHoy = 1
        }

        private void CargarDatosMaestros()
        {
            try
            {
                // Llama a la BLL para obtener el listado completo
                _listadoMaestro = oSocioEstadoCuotaBLL.ObtenerListadoMaestro();

                // Asigna la lista maestra al BindingSource (la lista que se va a filtrar)
                _bindingSource.DataSource = _listadoMaestro;

                // 🚨 Configuramos las columnas UNA VEZ después de cargar el BindingSource
                AjustarColumnasDataGrid();
            }
            catch (Exception ex)
            {
                // REF: Reemplazo de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(
                      string.Format(MensajesUI.CUOTAS_MSG_ERROR_CARGA_MAESTRO, ex.Message),
                      MensajesUI.TITULO_ERROR_CRITICO
                    );
                // Si falla, la lista queda nula o vacía.
            }
        }

        private void AjustarColumnasDataGrid()
        {
            // Ocultar columnas que no son relevantes para la presentación principal
            // Se agrupan todas las operaciones de visibilidad al inicio.
            dgvMorosos.Columns["IdPersona"].Visible = false;
            dgvMorosos.Columns["IdCuotaPendiente"].Visible = false;
            dgvMorosos.Columns["EstadoActivo"].Visible = false;
            dgvMorosos.Columns["EstaVigente"].Visible = false;
            dgvMorosos.Columns["Email"].Visible = false;
            // La columna EsMoroso no existe en el DTO, pero si existiera, se ocultaría aquí.
            // dgvMorosos.Columns["EsMoroso"].Visible = false; 

            // Dejamos VenceHoy visible por ahora para probar el filtro visual.
            // dgvMorosos.Columns["VenceHoy"].Visible = false;

            // Renombrar y configurar Headers (UX mejorada)
            dgvMorosos.Columns["NombreCompleto"].HeaderText = "Socio";
            dgvMorosos.Columns["Dni"].HeaderText = "DNI";
            dgvMorosos.Columns["NumeroCarnet"].HeaderText = "N° Carnet";
            dgvMorosos.Columns["Telefono"].HeaderText = "Teléfono";

            // Renombre clave: Deuda Total para reflejar la lógica BLL
            dgvMorosos.Columns["MontoCuotaPendiente"].HeaderText = "Deuda Total ($)";
            dgvMorosos.Columns["FechaVencimientoPendiente"].HeaderText = "F. Venc. Más Antigua"; // o "Inicio de Mora"
            dgvMorosos.Columns["MesesMora"].HeaderText = "Meses Mora";
            dgvMorosos.Columns["FechaPagoUltima"].HeaderText = "Último Pago";

            // Formato
            dgvMorosos.Columns["MontoCuotaPendiente"].DefaultCellStyle.Format = "C";
            dgvMorosos.Columns["MontoCuotaPendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgvMorosos.Columns["FechaVencimientoPendiente"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvMorosos.Columns["FechaVencimientoPendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // <-- Nuevo
            dgvMorosos.Columns["FechaPagoUltima"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvMorosos.Columns["FechaPagoUltima"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // <-- Nuevo
            // Autoajustar
            dgvMorosos.AutoResizeColumns();
        }


        private void EstablecerModo(TipoListadoCuotas modo)
        {
            // 1. Control de Radio Buttons (Feedback al Administrador)
            rbMorosos.Checked = (modo == TipoListadoCuotas.Morosos);
            rbVencimientoHoy.Checked = (modo == TipoListadoCuotas.VencimientoHoy);

            // 2. Aplicar el Filtro. No es necesario pasar 1, ya es el valor por defecto.
            AplicarFiltro(modo);
        }

        /// <summary>
        /// Aplica la lógica de filtrado al BindingSource utilizando el modo seleccionado.
        /// </summary>
        private void AplicarFiltro(TipoListadoCuotas modo, int mesesMinimos = 1)
        {
            if (_listadoMaestro == null || _listadoMaestro.Count == 0)
            {
                _bindingSource.DataSource = null;
                return;
            }

            List<SocioEstadoCuotaDTO> listaFiltrada;

            // 1. Aplicar el Filtro y Actualizar el Título
            if (modo == TipoListadoCuotas.Morosos)
            {
                // Filtra Morosos Reales (MesesMora >= 1)
                listaFiltrada = _listadoMaestro
                    .FindAll(s => s.MesesMora >= mesesMinimos);

                lblTitulo.Text = $"CONTROL DE CUOTAS (MOROSOS con {mesesMinimos} mes(es) o más)";

                // Encabezado dinámico: Deuda Acumulada
                dgvMorosos.Columns["MontoCuotaPendiente"].HeaderText = "Deuda Total ($)";
            }
            else // TipoListadoCuotas.VencimientoHoy
            {
                // Filtra Vencimiento Hoy (FechaVencimientoPendiente = Hoy)
                DateTime hoy = DateTime.Today;

                listaFiltrada = _listadoMaestro
                    .FindAll(s => s.FechaVencimientoPendiente.HasValue &&
                                    s.FechaVencimientoPendiente.Value.Date == hoy);

                lblTitulo.Text = "CONTROL DE CUOTAS (VENCIMIENTO HOY)";

                // Encabezado dinámico: Monto de la cuota no vencida
                dgvMorosos.Columns["MontoCuotaPendiente"].HeaderText = "Total a Pagar ($)";
            }

            // 2. Actualizar el BindingSource
            _bindingSource.DataSource = listaFiltrada;
            _bindingSource.ResetBindings(false);
            _bindingSource.Filter = null;
        }

        #endregion

        #region BOTONES DE ACCION



        private void btnImprimirMorosos_Click(object sender, EventArgs e)
        {
            // 1. Obtener la fuente de datos actual del BindingSource.
            List<ClubDeportivo.UI.Entidades.SocioEstadoCuotaDTO> listaFiltrada =
                _bindingSource.DataSource as List<ClubDeportivo.UI.Entidades.SocioEstadoCuotaDTO>;

            // 2. Verificar si la lista existe y tiene elementos.
            if (listaFiltrada == null || listaFiltrada.Count == 0)
            {
                Prompt.MostrarAlerta(
                    MensajesUI.CUOTAS_MSG_ADVERTENCIA_NO_IMPRIMIR,
                    MensajesUI.TITULO_ADVERTENCIA
                );
                return;
            }

            // 3. Obtener el título dinámico del reporte.
            // 🚨 CORRECCIÓN FINAL: Obtenemos el texto COMPLETO de la etiqueta.
            string tituloReporteCompleto = lblTitulo.Text;

            // Aplicamos la lógica de negocio para cambiar el prefijo (ej: "LISTADO MAESTRO")
            if (tituloReporteCompleto.StartsWith("CONTROL DE CUOTAS Y MOROSIDAD"))
            {
                tituloReporteCompleto = tituloReporteCompleto.Replace("CONTROL DE CUOTAS Y MOROSIDAD", "LISTADO MAESTRO");
            }

            string rutaPdfGenerado = string.Empty;

            try
            {
                // 4. Llamar al método de generación de PDF con el título COMPLETO.
                rutaPdfGenerado = ClubDeportivo.UI.Utilitarios.PdfGenerator.GenerarListadoCuotasMorosidad(
                    listaFiltrada,
                    tituloReporteCompleto
                );

                // 5. Notificar al usuario y preguntar si desea abrir el PDF
                DialogResult dialogResult = Prompt.MostrarDialogoSiNo(
                    string.Format(MensajesUI.CUOTAS_MSG_PREGUNTAR_ABRIR_PDF, rutaPdfGenerado),
                    MensajesUI.TITULO_IMPRESION_LISTADO
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Abrir el archivo usando ProcessStartInfo
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaPdfGenerado) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                // 6. Capturar errores 
                Prompt.MostrarError(
                    string.Format(MensajesUI.CUOTAS_MSG_ERROR_GENERAR_PDF, ex.Message),
                    MensajesUI.TITULO_ERROR_SISTEMA
                );
            }
        }


        private void btnEstadoCuenta_Click(object sender, EventArgs e)
        {
            if (dgvMorosos.SelectedRows.Count == 0)
            {
                // REF: Reemplazo de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(
                      MensajesUI.CUOTAS_MSG_ADVERTENCIA_NO_SELECCION,
                      MensajesUI.TITULO_ADVERTENCIA
                    );
                return;
            }

            try
            {
                DataGridViewRow filaSeleccionada = dgvMorosos.SelectedRows[0];

                // 1. Obtener datos básicos directamente de la grilla (ya que no están en el DTO)
                string dni = filaSeleccionada.Cells["Dni"].Value.ToString();
                string telefono = filaSeleccionada.Cells["Telefono"].Value.ToString(); // Asumiendo que "Telefono" es el nombre de la columna

                // Usamos el DNI para la búsqueda del estado de pago
                string identificador = dni;

                // 2. Llamar a la BLL para obtener el detalle de pago completo y calculado
                // El método BuscarPersonaParaPago utiliza el 'identificador' (DNI) para la búsqueda.
                PersonaPagoDetalleDTO detalle = oPersonaBLL.BuscarPersonaParaPago(identificador);

                if (detalle == null || !detalle.EsSocio)
                {
                    // REF: Reemplazo de MessageBox por Prompt.MostrarError
                    Prompt.MostrarError(
                        string.Format(MensajesUI.CUOTAS_MSG_ERROR_BUSQUEDA_FALLIDA, identificador),
                        MensajesUI.TITULO_ERROR_BUSQUEDA
                      );
                    return;
                }

                // 3. Construir el mensaje informativo (usando el DNI y Teléfono capturados en el punto 1)
                string mensaje = $"--- ESTADO DE CUENTA DETALLADO ---\n\n" +
                                 $"Socio: {detalle.Nombre} {detalle.Apellido}\n" +
                                 $"N° Carnet: {detalle.NumeroCarnet}\n" +
                                 $"DNI: {dni}\n" + // Usamos la variable local 'dni'
                                 $"Teléfono: {telefono}\n" + // Usamos la variable local 'telefono'
                                 $"Última Cuota Cubierta: {detalle.UltimaCuotaCubierta.ToShortDateString()}\n" +
                                 $"------------------------------------------------------\n" +
                                 $"SITUACIÓN ACTUAL: {detalle.EstadoMembresia}\n";

                // 4. Añadir la instrucción de acción (Regla de Negocio del Administrador)
                if (detalle.EstadoMembresia.StartsWith("PENDIENTE"))
                {
                    mensaje += "\n⚠️ **ACCIÓN REQUERIDA**:\n" +
                               "Por favor, dirija al socio a la **Recepción/Sector de Contabilidad** para que proceda con el pago y regularice su situación.";
                }
                else
                {
                    mensaje += "\n✅ **ACCIÓN REQUERIDA**:\n" +
                               "El Socio se encuentra **AL DÍA**.\n" +
                               "La próxima cuota ya está cubierta.";
                }

                // 5. Mostrar el mensaje
                // REF: Reemplazo de MessageBox por Prompt.MostrarInformacion
                Prompt.MostrarInformacion(
                              mensaje,
                              $"Detalle de Estado de Cuenta - {detalle.Nombre} {detalle.Apellido}" // Título dinámico
                                    );
                }
            catch (Exception ex)
            {
                    // REF: Reemplazo de MessageBox por Prompt.MostrarError
                    Prompt.MostrarError(
                      string.Format(MensajesUI.CUOTAS_MSG_ERROR_BLL_CUENTA, ex.Message),
                      MensajesUI.TITULO_ERROR_BLL
                    );
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       
        private void rbMorosos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMorosos.Checked)
            {
                EstablecerModo(TipoListadoCuotas.Morosos);
            }
        }

        private void rbVencimientoHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVencimientoHoy.Checked)
            {
                EstablecerModo(TipoListadoCuotas.VencimientoHoy);
            }
        }


        #endregion



    }


}

