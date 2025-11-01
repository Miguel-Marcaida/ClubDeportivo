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
        // CORRECCIÓN DE AMBIGÜEDAD: Se especifica el namespace System.Windows.Forms
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
            this.Text = "Inscripción Club Deportivo";

            // --- 2. TÍTULO ---
            lblTitulo.Text = "REGISTRO DE PERSONA (SOCIO / NO SOCIO)";
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.AutoSize = true;

            // Se asume que el diseñador ha declarado los GroupBox y Paneles

            // --- 3. GRUPOBOX DE DATOS PERSONALES (PERSONA) ---
            gbDatosPersona.Text = "Datos de la Persona";
            gbDatosPersona.Location = new Point(20, 70);
            gbDatosPersona.Size = new Size(400, 350);

            // Etiqueta y campo DNI
            Label lblDni = new Label() { Text = "DNI:", Location = new Point(15, 30), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtDni.Location = new Point(15, 50);
            txtDni.Size = new Size(165, 25);
            gbDatosPersona.Controls.Add(lblDni);
            gbDatosPersona.Controls.Add(txtDni); // Asegurar que el control se añade

            // Etiqueta y campo Nombre
            Label lblNombre = new Label() { Text = "Nombre:", Location = new Point(200, 30), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtNombre.Location = new Point(200, 50);
            txtNombre.Size = new Size(165, 25);
            gbDatosPersona.Controls.Add(lblNombre);
            gbDatosPersona.Controls.Add(txtNombre); // Asegurar que el control se añade

            // Etiqueta y campo Apellido
            Label lblApellido = new Label() { Text = "Apellido:", Location = new Point(15, 90), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtApellido.Location = new Point(15, 110);
            txtApellido.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblApellido);
            gbDatosPersona.Controls.Add(txtApellido); // Asegurar que el control se añade

            // Etiqueta y campo Fecha Nacimiento
            Label lblFechaNacimiento = new Label() { Text = "Fecha de Nacimiento:", Location = new Point(15, 150), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            dtpFechaNacimiento.Location = new Point(15, 170);
            dtpFechaNacimiento.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblFechaNacimiento);
            gbDatosPersona.Controls.Add(dtpFechaNacimiento); // Asegurar que el control se añade

            // Etiqueta y campo Teléfono
            Label lblTelefono = new Label() { Text = "Teléfono:", Location = new Point(15, 210), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtTelefono.Location = new Point(15, 230);
            txtTelefono.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblTelefono);
            gbDatosPersona.Controls.Add(txtTelefono); // Asegurar que el control se añade

            // Etiqueta y campo Email
            Label lblEmail = new Label() { Text = "Email:", Location = new Point(15, 270), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtEmail.Location = new Point(15, 290);
            txtEmail.Size = new Size(350, 25);
            gbDatosPersona.Controls.Add(lblEmail);
            gbDatosPersona.Controls.Add(txtEmail); // Asegurar que el control se añade


            // --- 4. GRUPOBOX TIPO DE PERSONA (ROL) ---
            gbTipoPersona.Text = "Selección de Rol";
            gbTipoPersona.Location = new Point(440, 70);
            gbTipoPersona.Size = new Size(330, 90);
            gbTipoPersona.Controls.Add(rbSocio); // Asegurar que el control se añade
            gbTipoPersona.Controls.Add(rbNoSocio); // Asegurar que el control se añade


            rbSocio.Text = "Socio (Membresía Mensual)";
            rbSocio.Location = new Point(15, 25);
            rbSocio.AutoSize = true;
            rbSocio.ForeColor = EstilosGlobales.ColorTextoClaro; // Aplicar color

            rbNoSocio.Text = "No Socio (Acceso Diario)";
            rbNoSocio.Location = new Point(15, 55);
            rbNoSocio.AutoSize = true;
            rbNoSocio.ForeColor = EstilosGlobales.ColorTextoClaro; // Aplicar color

            // --- 5. PANEL DE DATOS DE SOCIO (pnlSocioData) ---
            pnlSocioData.Location = new Point(440, 180);
            pnlSocioData.Size = new Size(330, 240);
            pnlSocioData.BorderStyle = BorderStyle.FixedSingle;
            pnlSocioData.ForeColor = EstilosGlobales.ColorTextoClaro; // Asegurar que el texto sea visible

            // Etiqueta y campo Número de Carnet
            Label lblNumCarnet = new Label() { Text = "Número de Carnet:", Location = new Point(15, 15), AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            txtNumCarnet.Location = new Point(15, 35);
            txtNumCarnet.Size = new Size(300, 25);
            pnlSocioData.Controls.Add(lblNumCarnet);
            pnlSocioData.Controls.Add(txtNumCarnet); // Asegurar que el control se añade

            // Checkbox Ficha Médica
            chkFichaMedica.Text = "Ficha Médica y Apto Físico";
            chkFichaMedica.Location = new Point(15, 80);
            chkFichaMedica.AutoSize = true;
            chkFichaMedica.ForeColor = EstilosGlobales.ColorTextoClaro; // Aplicar color
            pnlSocioData.Controls.Add(chkFichaMedica);

            // Botón de Pagar/Registrar
            btnPagarYRegistrar.Text = "REGISTRAR SOCIO Y PAGAR CUOTA N°1";
            btnPagarYRegistrar.Location = new Point(15, 120);
            btnPagarYRegistrar.Size = new Size(300, 45);
            pnlSocioData.Controls.Add(btnPagarYRegistrar);

            // --- 6. PANEL DE DATOS DE NO SOCIO (pnlNoSocioData) ---
            pnlNoSocioData.Location = new Point(440, 180);
            pnlNoSocioData.Size = new Size(330, 240);
            pnlNoSocioData.BorderStyle = BorderStyle.FixedSingle;
            pnlNoSocioData.ForeColor = EstilosGlobales.ColorTextoClaro; // Asegurar que el texto sea visible

            // Etiqueta de Acceso Diario
            lblFechaAcceso.Text = "Fecha y Hora de Acceso:";
            lblFechaAcceso.Location = new Point(15, 20);
            lblFechaAcceso.AutoSize = true;
            pnlNoSocioData.Controls.Add(lblFechaAcceso); // Asegurar que el control se añade

            // Label que mostrará la hora actual (añadido para la nueva lógica)
            Label lblHoraActual = new Label()
            {
                Text = "Hora Actual", // Este texto será reemplazado por el Timer
                Location = new Point(15, 50),
                AutoSize = true,
                Font = new Font(lblFechaAcceso.Font, FontStyle.Bold),
                ForeColor = EstilosGlobales.ColorAcento,
                Name = "lblHoraActual" // Nombre para identificarlo en el Timer
            };
            pnlNoSocioData.Controls.Add(lblHoraActual);

            // Botón de acción No Socio
            btnRegistrarAcceso.Text = "REGISTRAR ACCESO Y COBRAR";
            btnRegistrarAcceso.Location = new Point(15, 100);
            btnRegistrarAcceso.Size = new Size(300, 50);
            pnlNoSocioData.Controls.Add(btnRegistrarAcceso); // Asegurar que el control se añade

            /// c.Text = "Ficha Médica y Apto Físico";
            chkAptoFisico.Text = "Apto Fisico Para No Socio";
            chkAptoFisico.Location = new Point(15, 70);
            chkAptoFisico.AutoSize = true;
            chkAptoFisico.ForeColor = EstilosGlobales.ColorTextoClaro; // Aplicar color
            pnlNoSocioData.Controls.Add(chkAptoFisico);



            // --- 7. BOTÓN CANCELAR ---
            btnCancelar.Text = "CANCELAR / CERRAR";
            btnCancelar.Location = new Point(440, 580);
            btnCancelar.Size = new Size(330, 45);
            this.Controls.Add(btnCancelar); // Asegurar que el control se añade

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


        private bool ValidarCamposPersonaCompleto(bool esSocio)
        {
            // 1. Validaciones Requeridas (DNI, Nombre, Apellido)
            if (!Validaciones.EsTextoRequerido(txtDni.Text) || !Validaciones.EsTextoRequerido(txtNombre.Text)
                || !Validaciones.EsTextoRequerido(txtApellido.Text))
            {
                MessageBox.Show("Los campos DNI, Nombre y Apellido son obligatorios.",
                    "Advertencia de Campos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Validación de Formato DNI/Teléfono (deben ser numéricos si no están vacíos)
            if (!Validaciones.EsNumerico(txtDni.Text))
            {
                MessageBox.Show("El campo DNI debe contener solo números.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Focus();
                return false;
            }
            if (!Validaciones.EsNumerico(txtTelefono.Text))
            {
                MessageBox.Show("El campo Teléfono debe contener solo números (sin guiones/espacios).", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTelefono.Focus();
                return false;
            }

            // 3. Validación de Email (opcional, pero si está, debe ser válido)
            if (!Validaciones.EsFormatoEmailValido(txtEmail.Text))
            {
                MessageBox.Show("El formato del Email es incorrecto. Si no es obligatorio, déjelo vacío.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            // 4. Validación Específica de Socio
            if (esSocio)
            {
                if (!Validaciones.EsTextoRequerido(txtNumCarnet.Text) || !Validaciones.EsNumerico(txtNumCarnet.Text) || txtNumCarnet.Text == "ERROR")
                {
                    MessageBox.Show("El Número de Carnet es obligatorio para el Socio y debe ser numérico.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumCarnet.Focus();
                    return false;
                }

                if(chkFichaMedica.Checked==false)
                {
                    Prompt.MostrarAlerta("El Socio debe entregar la ficha medica y apto fisico.");
                    return false;
                     }

            }

            if (!esSocio)
            {
                if (chkAptoFisico.Checked == false)
                {
                    Prompt.MostrarAlerta("El No Socio debe entregar apto fisico.");
                    return false;
                }
            }

            return true;
        }

        #endregion


        #region LOGICA DE NEGOCIOS
        private void rbSocio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSocio.Checked)
            {
                pnlSocioData.Visible = true;
                pnlNoSocioData.Visible = false;
                _timerActualizador.Stop(); // Detiene el timer si estaba corriendo

                txtNumCarnet.ReadOnly = true;
                txtNumCarnet.Text = ""; // Limpiar antes de calcular

                try
                {
                    // LLAMADA CRÍTICA A LA BLL: Obtenemos el carnet precalculado
                    int proximoCarnet = oPersonaBLL.ObtenerProximoNumeroCarnet();
                    txtNumCarnet.Text = proximoCarnet.ToString();
                }
                catch (Exception ex)
                {
                    txtNumCarnet.Text = "ERROR";
                    MessageBox.Show($"Error al obtener el número de carnet: {ex.Message}",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string[] opcionesPago = { "Efectivo", "Tarjeta", "Transferencia" };
                string formaPago = Prompt.MostrarMenu("Forma de Pago",
                    $"Seleccione la forma de pago de la PRIMERA CUOTA MENSUAL (Monto: ${montoCuota:N2}):", opcionesPago);

                if (string.IsNullOrEmpty(formaPago))
                {
                    MessageBox.Show("Debe seleccionar la forma de pago para completar la inscripción de Socio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // B. Confirmación del cobro (UX)
                DialogResult confirmacion = MessageBox.Show(
                    $"Confirma el registro e inscripción del Socio, y el cobro de la PRIMERA CUOTA por un monto de ${montoCuota:N2} (pago en {formaPago})?",
                    "Confirmación de Pago y Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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

                MessageBox.Show($"¡Socio {nuevoSocio.ObtenerNombreCompleto()} registrado y PRIMERA CUOTA COBRADA con éxito!\n\nID Persona: {nuevoSocio.IdPersona}\nNúmero de Carnet Asignado: {nuevoSocio.NumeroCarnet}\nMonto Cobrado: ${montoCuota:N2} ({formaPago})",
                    "Registro de Socio OK", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // F. GENERACIÓN DEL CARNET PDF
                GenerarYAbrirCarnet(nuevoSocio);


                // Limpieza de la interfaz
                LimpiarFormulario();
            }
            catch (FormatException)
            {
                MessageBox.Show("El Número de Carnet debe ser un valor numérico entero o el valor precalculado es incorrecto.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumCarnet.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Inscripción", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                string[] opcionesPago = { "Efectivo", "Tarjeta", "Transferencia" };
                string formaPago = Prompt.MostrarMenu("Forma de Pago",
                    $"Seleccione la forma de pago del ACCESO DIARIO (Monto: ${montoAcceso:N2}):", opcionesPago);

                if (string.IsNullOrEmpty(formaPago))
                {
                    MessageBox.Show("Debe seleccionar la forma de pago para registrar el acceso diario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // B. Confirmación del cobro (UX)
                DialogResult confirmacion = MessageBox.Show(
                    $"Confirma el registro y acceso diario del No Socio, y el cobro de ${montoAcceso:N2} (pago en {formaPago})?",
                    "Confirmación de Pago y Registro de Acceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                MessageBox.Show($"¡No Socio {nuevoNoSocio.ObtenerNombreCompleto()} registrado y acceso diario cobrado con éxito!\n" +
                    $"ID: {idGenerado}\nMonto cobrado: ${montoAcceso:N2} en {formaPago}.",
                    "Registro de Acceso OK", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // F. GENERACIÓN DEL COMPROBANTE DE ACCESO PDF
                GenerarYAbrirComprobanteAcceso(nuevoNoSocio.Dni, nuevoNoSocio.ObtenerNombreCompleto(), montoAcceso, "Acceso Diario General");


                // Limpieza de la interfaz
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Registro de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                MessageBox.Show($"El Carnet de Socio ha sido generado y ABIERTO correctamente.\n\nRuta de guardado: {rutaCarnet}",
                    "Carnet Generado y Abierto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Advertencia: El Carnet se generó, pero no se pudo abrir automáticamente.\nError: {ex.Message}",
                                "Error al Abrir PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Genera y abre el Comprobante de Acceso Diario como PDF.
        /// </summary>
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

                MessageBox.Show($"El Comprobante de Acceso ha sido generado y ABIERTO (o intentado abrir) correctamente.\n\nRuta de guardado: {rutaComprobante}",
                    "Comprobante Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Advertencia: El Comprobante se generó, pero no se pudo abrir automáticamente.\nError: {ex.Message}",
                                "Error al Abrir PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Limpia todos los campos del formulario y lo devuelve al estado inicial.
        /// </summary>
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
