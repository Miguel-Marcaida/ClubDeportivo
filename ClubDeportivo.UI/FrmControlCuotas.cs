using ClubDeportivo.UI.BLL; 
using ClubDeportivo.UI.Entidades; 
using ClubDeportivo.UI.Utilitarios;
using System.Data;
using System.Collections.Generic; // <--- NUEVO (Para usar List<T>)
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
            // Ocultar columnas que no son para la UI, pero sí para la lógica (ID)
            dgvMorosos.Columns["IdPersona"].Visible = false;
            dgvMorosos.Columns["IdCuotaPendiente"].Visible = false;
            dgvMorosos.Columns["EstadoActivo"].Visible = false; // Se asume True, pero se puede mostrar si es relevante
            dgvMorosos.Columns["EstaVigente"].Visible = false;

            // Renombrar y configurar Headers
            dgvMorosos.Columns["NombreCompleto"].HeaderText = "Socio";
            dgvMorosos.Columns["Dni"].HeaderText = "DNI";
            dgvMorosos.Columns["NumeroCarnet"].HeaderText = "N° Carnet";
            dgvMorosos.Columns["Telefono"].HeaderText = "Teléfono";

            dgvMorosos.Columns["MontoCuotaPendiente"].HeaderText = "Monto (Venc.)";
            dgvMorosos.Columns["FechaVencimientoPendiente"].HeaderText = "F. Vencimiento";
            //dgvMorosos.Columns["DiasMora"].HeaderText = "Días Mora";
            dgvMorosos.Columns["MesesMora"].HeaderText = "Meses Mora";
            dgvMorosos.Columns["FechaPagoUltima"].HeaderText = "Último Pago";

            // Formato
            dgvMorosos.Columns["MontoCuotaPendiente"].DefaultCellStyle.Format = "C";
            dgvMorosos.Columns["MontoCuotaPendiente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvMorosos.Columns["FechaVencimientoPendiente"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvMorosos.Columns["FechaPagoUltima"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Ocultar columnas que no son para la UI o son menos críticas
            dgvMorosos.Columns["IdPersona"].Visible = false;
            dgvMorosos.Columns["IdCuotaPendiente"].Visible = false;
            dgvMorosos.Columns["EstadoActivo"].Visible = false;
            dgvMorosos.Columns["EstaVigente"].Visible = false;
            dgvMorosos.Columns["Email"].Visible = false; // El email no es crítico en este reporte

            // Autoajustar
            dgvMorosos.AutoResizeColumns();
        }

        private void EstablecerModo(TipoListadoCuotas modo)
        {

            // 1. Control de Título (Feedback al Administrador)
            if (modo == TipoListadoCuotas.Morosos)
            {
                lblTitulo.Text = "CONTROL DE CUOTAS (TODOS LOS MOROSOS)";
                rbMorosos.Checked = true;
            }
            else // TipoListadoCuotas.VencimientoHoy
            {
                lblTitulo.Text = "CONTROL DE CUOTAS (VENCEN HOY)";
                rbVencimientoHoy.Checked = true;
            }

            // 4. Aplicar el Filtro al BindingSource (Con un valor fijo de 1 para Morosos)
            if (modo == TipoListadoCuotas.Morosos)
            {
                AplicarFiltro(modo, 1); // Siempre mostramos todos los morosos (1 mes o más)
            }
            else
            {
                AplicarFiltro(modo);
            }
        
        }


        /// <summary>
        /// Aplica la lógica de filtrado al BindingSource utilizando el modo seleccionado.
        /// </summary>
        private void AplicarFiltro(TipoListadoCuotas modo, int mesesMinimos = 1)
        {
            if (_listadoMaestro == null || _listadoMaestro.Count == 0)
            {
                // No hay datos que filtrar
                _bindingSource.DataSource = null;
                return;
            }

            List<SocioEstadoCuotaDTO> listaFiltrada;

            if (modo == TipoListadoCuotas.Morosos)
            {
                
                listaFiltrada = _listadoMaestro
                    // El filtro ahora es simple: MesesMora (entero) debe ser mayor o igual al mínimo.
                    .FindAll(s => s.MesesMora >= mesesMinimos);

                lblTitulo.Text = $"CONTROL DE CUOTAS (MOROSOS con {mesesMinimos} mes(es) o más)";
            }
            else // TipoListadoCuotas.VencimientoHoy
            {
                // Lógica de Negocio 2: Filtrar Socios cuya FechaVencimientoPendiente es HOY
                DateTime hoy = DateTime.Today;

                listaFiltrada = _listadoMaestro
                    .FindAll(s => s.FechaVencimientoPendiente.HasValue &&
                                  s.FechaVencimientoPendiente.Value.Date == hoy);

                lblTitulo.Text = "CONTROL DE CUOTAS (VENCIMIENTO HOY)";
            }

            // Actualiza el BindingSource con la lista filtrada
            _bindingSource.DataSource = listaFiltrada;
            _bindingSource.ResetBindings(false);

            // 🚨 Limpia el filtro del BindingSource (ya que usamos una lista nueva)
            _bindingSource.Filter = null;
        }




        #endregion

        #region BOTONES DE ACCION


        private void btnImprimirMorosos_Click(object sender, EventArgs e)
        {
            // 1. Obtener la fuente de datos actual del BindingSource.
            // Usamos 'as' para intentar castear y manejar NULL si falla.
            List<ClubDeportivo.UI.Entidades.SocioEstadoCuotaDTO> listaFiltrada =
                _bindingSource.DataSource as List<ClubDeportivo.UI.Entidades.SocioEstadoCuotaDTO>;

            // 2. Verificar si la lista existe y tiene elementos.
            if (listaFiltrada == null || listaFiltrada.Count == 0)
            {
                // REF: Reemplazo de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(
                      MensajesUI.CUOTAS_MSG_ADVERTENCIA_NO_IMPRIMIR,
                      MensajesUI.TITULO_ADVERTENCIA
                    );
                return;
            }

            // 3. Obtener el título dinámico del reporte.
            string tituloReporte = lblTitulo.Text.Replace("CONTROL DE CUOTAS Y MOROSIDAD (", "").Replace(")", "");
            // Limpiamos el prefijo si es necesario (ej: si no se usó el formato entre paréntesis)
            if (tituloReporte.StartsWith("CONTROL DE CUOTAS Y MOROSIDAD"))
            {
                tituloReporte = tituloReporte.Replace("CONTROL DE CUOTAS Y MOROSIDAD", "LISTADO MAESTRO");
            }

            string rutaPdfGenerado = string.Empty;

            try
            {
                // 4. Llamar al nuevo método de impresión (GenerarListadoCuotasMorosidad)
                rutaPdfGenerado = ClubDeportivo.UI.Utilitarios.PdfGenerator.GenerarListadoCuotasMorosidad(
                    listaFiltrada,
                    tituloReporte
                );

                // 5. Notificar al usuario y preguntar si desea abrir el PDF
                DialogResult dialogResult = Prompt.MostrarDialogoSiNo( // ✅ CORRECCIÓN: Uso de la utilidad MostrarDialogoSiNo (2 argumentos)
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
                // 6. Capturar errores (incluyendo errores de PdfSharp o de I/O)
                // REF: Reemplazo de MessageBox por Prompt.MostrarError
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

