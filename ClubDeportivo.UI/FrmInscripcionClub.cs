using ClubDeportivo.UI;
using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace ClubDeportivo.UI
{
    public partial class FrmInscripcionClub : Form
    {

        #region CAMPOS Y PROPIEDADES

        private readonly PersonaBLL oPersonaBLL = new PersonaBLL();
        private readonly ConfiguracionBLL oConfiguracionBLL = new ConfiguracionBLL();
        private readonly int _anchoMenuLateral;
        private readonly System.Windows.Forms.Timer _timerActualizador;

        #endregion

        #region CONSTRUCTOR

        public FrmInscripcionClub(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;
            this.Load += FrmInscripcionClub_Load;

            // Inicializar Timer para la lógica del No Socio
            _timerActualizador = new System.Windows.Forms.Timer();
            _timerActualizador.Interval = 1000; // 1 segundo
            _timerActualizador.Tick += TimerActualizador_Tick;

            // Configurar el formulario y aplicar estilos
            ConfigurarFormulario();
        }

        #endregion

        #region CONFIGURACIÓN UI


        private void ConfigurarFormulario()
        {
            EstablecerGeometriaYTextos(); // Se mantiene tu método de construcción de la UI

            ConfigurarEstilos();
        }



        private void ConfigurarEstilos()
        {
            // Estilo al formulario base
            EstilosGlobales.AplicarFormatoBase(this);

            // Estilo del Título
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;
            lblTitulo.ForeColor = EstilosGlobales.ColorAcento;

            // Aplicar estilos a todos los campos de entrada
            EstilosGlobales.AplicarEstiloCampo(txtDni);
            EstilosGlobales.AplicarEstiloCampo(txtNombre);
            EstilosGlobales.AplicarEstiloCampo(txtApellido);
            EstilosGlobales.AplicarEstiloCampo(dtpFechaNacimiento);
            EstilosGlobales.AplicarEstiloCampo(txtTelefono);
            EstilosGlobales.AplicarEstiloCampo(txtEmail);
            EstilosGlobales.AplicarEstiloCampo(txtNumCarnet);

            // Estilo a los botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnPagarYRegistrar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnRegistrarAcceso);

            // Estilo para el botón de Cancelar/Cerrar
            EstilosGlobales.AplicarEstiloBotonAccion(btnCancelar);
            btnCancelar.BackColor = EstilosGlobales.ColorError;
            btnCancelar.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorError; // Asumiendo que existe ColorErrorOscuro

            // Lógica de inicio: Ningún panel específico visible
            pnlSocioData.Visible = false;
            pnlNoSocioData.Visible = false;

            // Aplicar estilos a los GroupBox para que el texto sea visible
            gbDatosPersona.ForeColor = EstilosGlobales.ColorTextoClaro;
            gbTipoPersona.ForeColor = EstilosGlobales.ColorTextoClaro;
        }


        private void EstablecerGeometriaYTextos()
        {
            // --- 1. CONFIGURACIÓN DEL FORMULARIO ---
            this.Text = MensajesUI.INSCRIPCION_TITULO_FORM;

            // --- 2. TÍTULO ---
            lblTitulo.Text = MensajesUI.INSCRIPCION_TITULO_VISTA; // ⬅️ Refactorizado
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.AutoSize = true;

            // Se asume que el diseñador ha declarado los GroupBox y Paneles

            // --- 3. GRUPOBOX DE DATOS PERSONALES (PERSONA) ---
            gbDatosPersona.Text = MensajesUI.INSCRIPCION_GB_DATOS_PERSONA; // ⬅️ Refactorizado
            gbDatosPersona.Location = new Point(20, 70);
            gbDatosPersona.Size = new Size(400, 350);

            // Etiqueta y campo DNI
            Label lblDni = new Label() { Text = MensajesUI.INSCRIPCION_LBL_DNI, Location = new Point(15, 30), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtDni.Location = new Point(15, 50);
            txtDni.Size = new Size(165, 25);
            gbDatosPersona.Controls.Add(lblDni);
            gbDatosPersona.Controls.Add(txtDni);

            // Etiqueta y campo Nombre
            Label lblNombre = new Label() { Text = MensajesUI.INSCRIPCION_LBL_NOMBRE, Location = new Point(200, 30), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtNombre.Location = new Point(200, 50);
            txtNombre.Size = new Size(165, 25);
            gbDatosPersona.Controls.Add(lblNombre);
            gbDatosPersona.Controls.Add(txtNombre);

            // Etiqueta y campo Apellido
            Label lblApellido = new Label() { Text = MensajesUI.INSCRIPCION_LBL_APELLIDO, Location = new Point(15, 90), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtApellido.Location = new Point(15, 110);
            txtApellido.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblApellido);
            gbDatosPersona.Controls.Add(txtApellido);

            // Etiqueta y campo Fecha Nacimiento
            Label lblFechaNacimiento = new Label() { Text = MensajesUI.INSCRIPCION_LBL_FECHA_NAC, Location = new Point(15, 150), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            dtpFechaNacimiento.Location = new Point(15, 170);
            dtpFechaNacimiento.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblFechaNacimiento);
            gbDatosPersona.Controls.Add(dtpFechaNacimiento);

            // Etiqueta y campo Teléfono
            Label lblTelefono = new Label() { Text = MensajesUI.INSCRIPCION_LBL_TELEFONO, Location = new Point(15, 210), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtTelefono.Location = new Point(15, 230);
            txtTelefono.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblTelefono);
            gbDatosPersona.Controls.Add(txtTelefono);

            // Etiqueta y campo Email
            Label lblEmail = new Label() { Text = MensajesUI.INSCRIPCION_LBL_EMAIL, Location = new Point(15, 270), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtEmail.Location = new Point(15, 290);
            txtEmail.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblEmail);
            gbDatosPersona.Controls.Add(txtEmail);

            // --- 4. GRUPOBOX TIPO DE PERSONA (ROL) ---
            gbTipoPersona.Text = MensajesUI.INSCRIPCION_GB_TIPO_PERSONA; // ⬅️ Refactorizado
            gbTipoPersona.Location = new Point(440, 70);
            gbTipoPersona.Size = new Size(330, 90);
            gbTipoPersona.Controls.Add(rbSocio);
            gbTipoPersona.Controls.Add(rbNoSocio);


            rbSocio.Text = MensajesUI.INSCRIPCION_RB_SOCIO; // ⬅️ Refactorizado
            rbSocio.Location = new Point(15, 25);
            rbSocio.AutoSize = true;
            rbSocio.ForeColor = EstilosGlobales.ColorTextoClaro;

            rbNoSocio.Text = MensajesUI.INSCRIPCION_RB_NO_SOCIO; // ⬅️ Refactorizado
            rbNoSocio.Location = new Point(15, 55);
            rbNoSocio.AutoSize = true;
            rbNoSocio.ForeColor = EstilosGlobales.ColorTextoClaro;

            // --- 5. PANEL DE DATOS DE SOCIO (pnlSocioData) ---
            pnlSocioData.Location = new Point(440, 180);
            pnlSocioData.Size = new Size(330, 240);
            pnlSocioData.BorderStyle = BorderStyle.FixedSingle;
            pnlSocioData.ForeColor = EstilosGlobales.ColorTextoClaro; // Asegurar que el texto sea visible

            // Etiqueta y campo Número de Carnet
            Label lblNumCarnet = new Label() { Text = MensajesUI.INSCRIPCION_LBL_CARNET, Location = new Point(15, 15), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro }; // ⬅️ Refactorizado
            txtNumCarnet.Location = new Point(15, 35);
            txtNumCarnet.Size = new Size(300, 25);
            pnlSocioData.Controls.Add(lblNumCarnet);
            pnlSocioData.Controls.Add(txtNumCarnet);

            // Checkbox Ficha Médica
            chkFichaMedica.Text = MensajesUI.INSCRIPCION_CHK_FICHA_MEDICA; // ⬅️ Refactorizado
            chkFichaMedica.Location = new Point(15, 80);
            chkFichaMedica.AutoSize = true;
            chkFichaMedica.ForeColor = EstilosGlobales.ColorTextoClaro;
            pnlSocioData.Controls.Add(chkFichaMedica);

            // Botón de Pagar/Registrar
            btnPagarYRegistrar.Text = MensajesUI.INSCRIPCION_BTN_REGISTRAR_SOCIO; // ⬅️ Refactorizado
            btnPagarYRegistrar.Location = new Point(15, 120);
            btnPagarYRegistrar.Size = new Size(300, 45);
            pnlSocioData.Controls.Add(btnPagarYRegistrar);

            // --- 6. PANEL DE DATOS DE NO SOCIO (pnlNoSocioData) ---
            pnlNoSocioData.Location = new Point(440, 180);
            pnlNoSocioData.Size = new Size(330, 240);
            pnlNoSocioData.BorderStyle = BorderStyle.FixedSingle;
            pnlNoSocioData.ForeColor = EstilosGlobales.ColorTextoClaro; // Asegurar que el texto sea visible

            // Etiqueta de Acceso Diario
            lblFechaAcceso.Text = MensajesUI.INSCRIPCION_LBL_FECHA_ACCESO; // ⬅️ Refactorizado
            lblFechaAcceso.Location = new Point(15, 20);
            lblFechaAcceso.AutoSize = true;
            pnlNoSocioData.Controls.Add(lblFechaAcceso);

            // Label que mostrará la hora actual (añadido para la nueva lógica)
            Label lblHoraActual = new Label()
            {
                Text = MensajesUI.INSCRIPCION_LBL_HORA_ACTUAL_PLACEHOLDER, // ⬅️ Refactorizado
                Location = new Point(15, 50),
                AutoSize = true,
                Font = new Font(lblFechaAcceso.Font, FontStyle.Bold),
                ForeColor = EstilosGlobales.ColorAcento,
                Name = "lblHoraActual" // Nombre para identificarlo en el Timer
            };
            pnlNoSocioData.Controls.Add(lblHoraActual);

            // Botón de acción No Socio
            btnRegistrarAcceso.Text = MensajesUI.INSCRIPCION_BTN_REGISTRAR_NO_SOCIO; // ⬅️ Refactorizado
            btnRegistrarAcceso.Location = new Point(15, 100);
            btnRegistrarAcceso.Size = new Size(300, 50);
            pnlNoSocioData.Controls.Add(btnRegistrarAcceso);

            // Checkbox Apto Físico No Socio
            chkAptoFisico.Text = MensajesUI.INSCRIPCION_CHK_APTO_FISICO_NO_SOCIO; // ⬅️ Refactorizado
            chkAptoFisico.Location = new Point(15, 70);
            chkAptoFisico.AutoSize = true;
            chkAptoFisico.ForeColor = EstilosGlobales.ColorTextoClaro;
            pnlNoSocioData.Controls.Add(chkAptoFisico);



            // --- 7. BOTÓN CANCELAR ---
            btnCancelar.Text = MensajesUI.INSCRIPCION_BTN_CANCELAR; // ⬅️ Refactorizado
            btnCancelar.Location = new Point(440, 580);
            btnCancelar.Size = new Size(330, 45);
            this.Controls.Add(btnCancelar);

            // --- 8. PANEL BASE DE TAMAÑO FIJO Y CENTRADO ---
            pnlBase.Location = new Point(0, 0);
            pnlBase.Size = new Size(800, 650);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;
            pnlBase.Anchor = AnchorStyles.None;
            pnlBase.Dock = DockStyle.None;

            pnlBase.Controls.AddRange(new Control[] {
                lblTitulo,
                gbDatosPersona,
                gbTipoPersona,
                pnlSocioData,
                pnlNoSocioData,
                btnCancelar
            });

            this.Controls.Add(pnlBase);
        }


        #endregion


        #region EVENTOS DEL FORMULARIOS

        private void FrmInscripcionClub_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));
        }



        #endregion


        #region VALIDACIONES DE ENTRADAS

        private bool ValidarCamposPersonaCompleto(bool esSocio)
        {
            // 1. Validaciones Requeridas (DNI, Nombre, Apellido)
            if (!Validaciones.EsTextoRequerido(txtDni.Text) || !Validaciones.EsTextoRequerido(txtNombre.Text)
                || !Validaciones.EsTextoRequerido(txtApellido.Text))
            {
                Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_VALIDACION_CAMPOS_REQUERIDOS);
                return false;
            }

            // 2. Validación de Formato DNI/Teléfono
            if (!Validaciones.EsNumerico(txtDni.Text))
            {
                Prompt.MostrarError(MensajesUI.INSCRIPCION_VALIDACION_DNI_NUMERICO);
                txtDni.Focus();
                return false;
            }
            if (!Validaciones.EsNumerico(txtTelefono.Text))
            {
                Prompt.MostrarError(MensajesUI.INSCRIPCION_VALIDACION_TELEFONO_NUMERICO);
                txtTelefono.Focus();
                return false;
            }

            // 3. Validación de Email
            if (!Validaciones.EsFormatoEmailValido(txtEmail.Text))
            {
                Prompt.MostrarError(MensajesUI.INSCRIPCION_VALIDACION_EMAIL);
                txtEmail.Focus();
                return false;
            }

            // 4. Validación Específica de Socio
            if (esSocio)
            {
                if (!Validaciones.EsTextoRequerido(txtNumCarnet.Text) || !Validaciones.EsNumerico(txtNumCarnet.Text) || txtNumCarnet.Text == MensajesUI.INSCRIPCION_ERROR_CARNET_CALCULO) // ⬅️ Refactorizado
                {
                    // **REFACTORIZADO:** Usando Prompt.MostrarAlerta
                    Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_VALIDACION_CARNET_OBLIGATORIO);
                    txtNumCarnet.Focus();
                    return false;
                }

                if (chkFichaMedica.Checked == false)
                {
                    Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_VALIDACION_FICHA_SOCIO); // ⬅️ Refactorizado
                    return false;
                }

            }

            if (!esSocio)
            {
                if (chkAptoFisico.Checked == false)
                {
                    Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_VALIDACION_APTO_NO_SOCIO); // ⬅️ Refactorizado
                    return false;
                }
            }

            return true;
        }
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
            Validaciones.ForzarMayusculas(e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
            Validaciones.ForzarMayusculas(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }


        

        #endregion


        #region LOGICA DE NEGOCIOS
        private void rbSocio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSocio.Checked)
            {
                pnlSocioData.Visible = true;
                pnlNoSocioData.Visible = false;
                _timerActualizador.Stop();

                txtNumCarnet.ReadOnly = true;
                txtNumCarnet.Text = "";

                try
                {
                    int proximoCarnet = oPersonaBLL.ObtenerProximoNumeroCarnet();
                    txtNumCarnet.Text = proximoCarnet.ToString();
                }
                catch (Exception ex)
                {
                    txtNumCarnet.Text = MensajesUI.INSCRIPCION_ERROR_CARNET_CALCULO; // ⬅️ Refactorizado
                    // **REFACTORIZADO:** Usando Prompt.MostrarError
                    Prompt.MostrarError(string.Format(MensajesUI.INSCRIPCION_ERROR_OBTENER_CARNET, ex.Message)); // ⬅️ Refactorizado
                }
            }
        }

        private void rbNoSocio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoSocio.Checked)
            {
                pnlNoSocioData.Visible = true;
                pnlSocioData.Visible = false;

                // Inicia el Timer para mostrar la hora actual en tiempo real
                _timerActualizador.Start();
                TimerActualizador_Tick(null, null); // Forzar la primera actualización
            }
        }

        #endregion


        #region BOTONES DE ACCIONES

        private void btnPagarYRegistrar_Click(object sender, EventArgs e)
        {
            // --- REGISTRO DE SOCIO ---

            // 1. Validación de campos obligatorios y de formato
            if (!ValidarCamposPersonaCompleto(true)) return;

            Socio nuevoSocio = null;
            decimal montoCuota = 0;

            try
            {
                // A. OBTENER MONTO Y SOLICITAR FORMA DE PAGO
                montoCuota = oConfiguracionBLL.ObtenerMontoCuotaBase();

                string[] opcionesPago = { "EFECTIVO", "TARJETA 1 CUOTA", "TARJETA 3 CUOTAS", "TARJETA 6 CUOTAS", "TRANSFERENCIA" };
                string formaPago = Prompt.MostrarMenu(MensajesUI.INSCRIPCION_SOCIO_PAGO_TITULO, // ⬅️ Refactorizado
                    string.Format(MensajesUI.INSCRIPCION_SOCIO_PAGO_MSG, montoCuota), opcionesPago); // ⬅️ Refactorizado

                if (string.IsNullOrEmpty(formaPago))
                {
                    Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_SOCIO_PAGO_ALERTA_FALTA); // ⬅️ Refactorizado
                    return;
                }



                //B.Confirmación del cobro(UX)
                // **REFACTORIZADO:** Usando Prompt.Confirmar en lugar de MessageBox.Show(..., YesNo, ...)
                DialogResult confirmacion = Prompt.Confirmar(
                    MensajesUI.INSCRIPCION_SOCIO_PAGO_CONFIRMACION_TITULO,
                    string.Format(MensajesUI.INSCRIPCION_SOCIO_PAGO_CONFIRMACION_MSG, montoCuota, formaPago));

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // C. Creación del objeto Socio
                nuevoSocio = new Socio
                {
                    Dni = txtDni.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),

                    // Atributos de Socio
                    NumeroCarnet = Convert.ToInt32(txtNumCarnet.Text.Trim()),
                    EstadoActivo = true,
                    FichaMedicaEntregada = chkFichaMedica.Checked
                };

                // D. Llamada a la Lógica de Negocio (Registra la Persona/Socio y la Transacción inicial)
                int idGenerado = oPersonaBLL.RegistrarInscripcionSocio(nuevoSocio, formaPago);

                // E. Muestra de éxito
                nuevoSocio.IdPersona = idGenerado;

                // **REFACTORIZADO:** Usando Prompt.MostrarExito en lugar de MessageBox.Show(..., Information)
                Prompt.MostrarExito(string.Format(MensajesUI.INSCRIPCION_SOCIO_REGISTRO_EXITO_MSG,
                                                 nuevoSocio.ObtenerNombreCompleto(), idGenerado, nuevoSocio.NumeroCarnet, montoCuota, formaPago));
                // F. GENERACIÓN DEL CARNET PDF
                GenerarYAbrirCarnet(nuevoSocio);


                // Limpieza de la interfaz
                LimpiarFormulario();
            }
            catch (FormatException)
            {
                // **REFACTORIZADO:** Usando Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.INSCRIPCION_SOCIO_ERROR_FORMATO_CARNET);
                txtNumCarnet.Focus();
            }
            catch (Exception ex)
            {
                // **REFACTORIZADO:** Usando Prompt.MostrarError
                Prompt.MostrarError(ex.Message);
            }
        }


        private void btnRegistrarAcceso_Click(object sender, EventArgs e)
        {
            // --- REGISTRO DE NO SOCIO (ACCESO DIARIO) ---

            // 1. Validación de campos obligatorios y de formato
            if (!ValidarCamposPersonaCompleto(false)) return;

            decimal montoAcceso = 0;
            NoSocio nuevoNoSocio = null;

            try
            {
                // A. OBTENER MONTO Y SOLICITAR FORMA DE PAGO
                montoAcceso = oConfiguracionBLL.ObtenerMontoAccesoDiario();

                string[] opcionesPago = { "EFECTIVO", "TARJETA 1 PAGO", "TRANSFERENCIA" };
                string formaPago = Prompt.MostrarMenu(MensajesUI.INSCRIPCION_SOCIO_PAGO_TITULO, // ⬅️ Refactorizado
                    string.Format(MensajesUI.INSCRIPCION_NO_SOCIO_PAGO_MSG, montoAcceso), opcionesPago); // ⬅️ Refactorizado

                if (string.IsNullOrEmpty(formaPago))
                {
                    // **REFACTORIZADO:** Usando Prompt.MostrarAlerta
                    Prompt.MostrarAlerta(MensajesUI.INSCRIPCION_NO_SOCIO_PAGO_ALERTA_FALTA);
                    return;
                }

                // B. Confirmación del cobro (UX)
                // **REFACTORIZADO:** Usando Prompt.Confirmar en lugar de MessageBox.Show(..., YesNo, ...)
                DialogResult confirmacion = Prompt.Confirmar(
                    MensajesUI.INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_TITULO,
                    string.Format(MensajesUI.INSCRIPCION_NO_SOCIO_PAGO_CONFIRMACION_MSG, montoAcceso, formaPago));

                if (confirmacion != DialogResult.Yes)
                {
                    return;
                }

                // C. Creación del objeto Entidad NoSocio
                nuevoNoSocio = new NoSocio
                {
                    Dni = txtDni.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                };

                // D. Llamada a la Lógica de Negocio (Registra la Persona/NoSocio y la Transacción inicial)
                // Nota: La fecha de acceso es la fecha/hora actual que el servidor/BLL debe registrar.
                int idGenerado = oPersonaBLL.RegistrarAccesoDiarioNoSocio(nuevoNoSocio, formaPago);

                // E. Muestra de éxito
                // **REFACTORIZADO:** Usando Prompt.MostrarExito en lugar de MessageBox.Show(..., Information)
                Prompt.MostrarExito(string.Format(MensajesUI.INSCRIPCION_NO_SOCIO_REGISTRO_EXITO_MSG,
                                                  nuevoNoSocio.ObtenerNombreCompleto(), idGenerado, montoAcceso, formaPago));

                // F. GENERACIÓN DEL COMPROBANTE DE ACCESO PDF
                GenerarYAbrirComprobanteAcceso(nuevoNoSocio.Dni, nuevoNoSocio.ObtenerNombreCompleto(), montoAcceso, MensajesUI.INSCRIPCION_NO_SOCIO_CONCEPTO_ACCESO); // ⬅️ Refactorizado

                // Limpieza de la interfaz
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // **REFACTORIZADO:** Usando Prompt.MostrarError
                Prompt.MostrarError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }


        #endregion



        #region METODOS AUXILIARES

        private void GenerarYAbrirCarnet(Socio nuevoSocio)
        {
            try
            {
                PersonaDetalleDTO detalleCarnet = new PersonaDetalleDTO
                {
                    NumeroCarnet = nuevoSocio.NumeroCarnet,
                    Dni = nuevoSocio.Dni,
                    Nombre = nuevoSocio.Nombre,
                    Apellido = nuevoSocio.Apellido,
                    EstadoActivo = nuevoSocio.EstadoActivo
                };

                string rutaCarnet = PdfGenerator.GenerarCarnetSocio(detalleCarnet);

                var psi = new ProcessStartInfo(rutaCarnet)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);

                // **REFACTORIZADO:** Usando Prompt.MostrarExito
                Prompt.MostrarExito(string.Format(MensajesUI.PDF_CARNET_EXITO_MSG, rutaCarnet));
            }
            catch (Exception ex)
            {
                // **REFACTORIZADO:** Usando Prompt.MostrarAlerta
                Prompt.MostrarAlerta(string.Format(MensajesUI.PDF_CARNET_ERROR_MSG, ex.Message));
            }
        }

      
        private void GenerarYAbrirComprobanteAcceso(string dni, string nombreCompleto, decimal monto, string concepto)
        {
            try
            {
                string rutaComprobante = PdfGenerator.GenerarComprobanteAcceso(
                    dni,
                    nombreCompleto,
                    monto,
                    concepto);

                var psi = new ProcessStartInfo(rutaComprobante)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);

                // **REFACTORIZADO:** Usando Prompt.MostrarExito
                Prompt.MostrarExito(string.Format(MensajesUI.PDF_COMPROBANTE_EXITO_MSG, rutaComprobante));
            }
            catch (Exception ex)
            {
                // **REFACTORIZADO:** Usando Prompt.MostrarAlerta
                Prompt.MostrarAlerta(string.Format(MensajesUI.PDF_COMPROBANTE_ERROR_MSG, ex.Message));
            }
        }

        
        private void LimpiarFormulario()
        {
            // Detener el timer si está activo
            _timerActualizador.Stop();
            chkAptoFisico.Checked = false;

            // 1. Limpieza de campos de Persona
            txtDni.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            dtpFechaNacimiento.Value = DateTime.Today;

            // 2. Limpieza de campos de Socio
            txtNumCarnet.Clear();
            chkFichaMedica.Checked = false;

            // 3. Resetear el estado de los RadioButtons y Paneles
            rbSocio.Checked = false;
            rbNoSocio.Checked = false;

            // FORZAMOS LA OCULTACIÓN DE AMBOS PANELES
            pnlSocioData.Visible = false;
            pnlNoSocioData.Visible = false;

            // 4. Enfocar el DNI
            txtDni.Focus();
        }

        private void TimerActualizador_Tick(object sender, EventArgs e)
        {
            // Buscamos el Label por nombre y actualizamos la hora
            Control[] results = pnlNoSocioData.Controls.Find("lblHoraActual", true);
            if (results.Length > 0 && results[0] is Label lblHoraActual)
            {
                lblHoraActual.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        #endregion


        
    }
}
