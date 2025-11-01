using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubDeportivo.UI
{
    public partial class FrmActualizarPersona : Form
    {
        #region PROPIEDADES Y CAMPOS PRIVADOS

        // Almacena el ID de la persona a editar (0 = Inscripción, > 0 = Edición)
        private int _idPersonaActual;
        private readonly int _anchoMenuLateral;
        private readonly PersonaBLL oPersonaBLL = new PersonaBLL();
        private Entidades.PersonaDetalleDTO _detallePersonaCargado;

        #endregion

        #region CONSTRUCTOR
        public FrmActualizarPersona(int idAEditar, int anchoMenuLateral)
        {
            InitializeComponent();
            this._idPersonaActual = idAEditar; // Guardamos el ID
            this._anchoMenuLateral = anchoMenuLateral; // Guardamos el ancho

            ConfigurarFormulario();

            // Suscripción para forzar el centrado del panel de contenido al redimensionar
            this.Load += FrmActualizarPersona_Load; // Usamos el evento de carga para asegurar el primer centrado
            this.Resize += (s, e) => Utilitarios.Utilitarios.ForzarCentrado(this, pnlContenido, 0);
        }



        #endregion

        #region CONFIGURACION UI
        private void ConfigurarFormulario()
        {
            EstablecerGeometriaYTextos();
            ConfigurarEstilos();
        }

        private void EstablecerGeometriaYTextos()
        {
            // =================================================================
            // 1. FORMULARIO PRINCIPAL
            // =================================================================
            this.Text = "Inscripción y Edición de Persona";
            this.Size = new Size(850, 700);
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // =================================================================
            // 2. TÍTULO Y CONTENEDOR PRINCIPAL
            // =================================================================

            lblTitulo.Text = "Edición de Persona";
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.Size = new Size(800, 30);

            // pnlContenido
            pnlContenido.Location = new Point(0, 0);
            pnlContenido.Size = new Size(820, 620);

            // =================================================================
            // 3. GROUPBOX DATOS PERSONALES (Columna Izquierda)
            // =================================================================

            gbDatosPersonales.Text = "Datos Personales";
            gbDatosPersonales.Location = new Point(10, 80);
            gbDatosPersonales.Size = new Size(400, 300);

            // ID Persona
            lblidPersona.Text = "ID:";
            lblidPersona.Location = new Point(20, 30);
            lblidPersona.Size = new Size(100, 20);
            txtIdPersona.Text = "0";
            txtIdPersona.Location = new Point(140, 30);
            txtIdPersona.Size = new Size(50, 25);

            // DNI
            lblDni.Text = "DNI / CUIL (*)";
            lblDni.Location = new Point(20, 65);
            lblDni.Size = new Size(110, 20);
            txtDni.Location = new Point(140, 65);
            txtDni.Size = new Size(240, 25);

            // Nombre
            lblNombre.Text = "Nombre (*)";
            lblNombre.Location = new Point(20, 100);
            lblNombre.Size = new Size(110, 20);
            txtNombre.Location = new Point(140, 100);
            txtNombre.Size = new Size(240, 25);

            // Apellido
            lblApellido.Text = "Apellido (*)";
            lblApellido.Location = new Point(20, 135);
            lblApellido.Size = new Size(110, 20);
            txtApellido.Location = new Point(140, 135);
            txtApellido.Size = new Size(240, 25);

            // Fecha Nacimiento
            lblFechaNacimiento.Text = "F. Nacimiento";
            lblFechaNacimiento.Location = new Point(20, 170);
            lblFechaNacimiento.Size = new Size(110, 20);
            dtpFechaNacimiento.Location = new Point(140, 170);
            dtpFechaNacimiento.Size = new Size(150, 25);

            // Teléfono
            lblTelefono.Text = "Teléfono";
            lblTelefono.Location = new Point(20, 205);
            lblTelefono.Size = new Size(110, 20);
            txtTelefono.Location = new Point(140, 205);
            txtTelefono.Size = new Size(240, 25);

            // Email
            lblEmail.Text = "Email";
            lblEmail.Location = new Point(20, 240);
            lblEmail.Size = new Size(110, 20);
            txtEmail.Location = new Point(140, 240);
            txtEmail.Size = new Size(240, 25);


            // =================================================================
            // 4. GROUPBOX TIPO Y ESTADO (Columna Derecha)
            // =================================================================

            gpTipoYEstado.Text = "Vigencia y Rol";
            gpTipoYEstado.Location = new Point(415, 80);
            gpTipoYEstado.Size = new Size(385, 300);

            // Baja Lógica
            lblVigente.Text = "Estado (Baja Lógica):";
            lblVigente.Location = new Point(20, 30);
            lblVigente.Size = new Size(180, 20);
            chkEstaVigente.Text = "Activo";
            chkEstaVigente.Location = new Point(200, 30);
            chkEstaVigente.Size = new Size(100, 20);

            // Tipo de Persona
            lblTipoPersona.Text = "Tipo de Persona:";
            lblTipoPersona.Location = new Point(20, 60);
            lblTipoPersona.Size = new Size(150, 20);

            // 5. CONTROL DE PESTAÑAS (TABCONTROL)
            tabControlDetalle.Location = new Point(415, 180);
            tabControlDetalle.Size = new Size(380, 199);
            tabControlDetalle.Alignment = TabAlignment.Right;

            // Pestaña SOCIO
            tbpSocio.Text = "Detalle Socio";
            lblNumCarnet.Text = "N° Carnet:";
            lblNumCarnet.Location = new Point(10, 10);
            lblNumCarnet.Size = new Size(100, 20);
            txtNumCarnet.Text = "AUTO-GENERADO";
            txtNumCarnet.Location = new Point(130, 10);
            txtNumCarnet.Size = new Size(180, 25);
            lblEstadoActivo.Text = "Cuota al Día / Activo:";
            lblEstadoActivo.Location = new Point(10, 45);
            lblEstadoActivo.Size = new Size(150, 20);
            chkEstadoActivo.Text = "Socio Activo";
            chkEstadoActivo.Location = new Point(10, 70);
            chkEstadoActivo.Size = new Size(150, 20);
            lblFichaMedica.Text = "Ficha Médica Entregada:";
            lblFichaMedica.Location = new Point(10, 105);
            lblFichaMedica.Size = new Size(170, 20);
            chkFichaMedicaEntregada.Text = "Entregada";
            chkFichaMedicaEntregada.Location = new Point(10, 130);
            chkFichaMedicaEntregada.Size = new Size(100, 20);

            // Pestaña NO SOCIO
            tbpNoSocio.Text = "Detalle No Socio";
            lblUltimoPagoDia.Text = "Fecha de Acceso:";
            lblUltimoPagoDia.Location = new Point(10, 10);
            lblUltimoPagoDia.Size = new Size(200, 20);
            txtUltimoPagoDia.Text = "N/A - Sin Registros";
            txtUltimoPagoDia.Location = new Point(10, 35);
            txtUltimoPagoDia.Size = new Size(200, 25);
            lblInfoNoSocio.Text = "INFO: Los No Socios pagan por acceso diario \nal momento de ingresar.";
            lblInfoNoSocio.Location = new Point(10, 80);
            lblInfoNoSocio.Size = new Size(350, 40);


            // =================================================================
            // 6. BOTONES DE ACCIÓN (Fuera del Panel de Contenido)
            // =================================================================

            // Botón Cancelar
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(470, 550);
            btnCancelar.Size = new Size(160, 35);
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Botón Guardar
            btnGuardar.Text = "Guardar Cambios";
            btnGuardar.Location = new Point(640, 550);
            btnGuardar.Size = new Size(160, 35);
            btnGuardar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private void ConfigurarEstilos()
        {
            // 1. Estilos del Formulario y Contenedores
            this.BackColor = EstilosGlobales.ColorBoton;
            Utilitarios.EstilosGlobales.AplicarFormatoBase(this);

            pnlContenido.BackColor = EstilosGlobales.ColorBoton;
            EstilosGlobales.AplicarEstiloTitulo(lblTitulo);

            // 1.2 GroupBoxes y TabControl
            EstilosGlobales.AplicarEstiloGroupBox(gbDatosPersonales);
            EstilosGlobales.AplicarEstiloGroupBox(gpTipoYEstado);

            tabControlDetalle.Font = EstilosGlobales.EstiloFuente;
            tabControlDetalle.BackColor = EstilosGlobales.ColorBoton;
            tbpSocio.BackColor = EstilosGlobales.ColorBoton;
            tbpNoSocio.BackColor = EstilosGlobales.ColorBoton;


            // 2. Estilos de Controles (Labels, CheckBoxes y TextBoxes)
            // TextBoxes
            EstilosGlobales.AplicarEstiloCampo(txtIdPersona);
            EstilosGlobales.AplicarEstiloCampo(txtDni);
            EstilosGlobales.AplicarEstiloCampo(txtNombre);
            EstilosGlobales.AplicarEstiloCampo(txtApellido);
            EstilosGlobales.AplicarEstiloCampo(txtTelefono);
            EstilosGlobales.AplicarEstiloCampo(txtEmail);
            EstilosGlobales.AplicarEstiloCampo(txtNumCarnet);
            EstilosGlobales.AplicarEstiloCampo(txtUltimoPagoDia);

            // Aplicar estilo general a todos los Labels y CheckBoxes
            foreach (Control control in gbDatosPersonales.Controls.Cast<Control>()
                .Union(gpTipoYEstado.Controls.Cast<Control>())
                .Union(tbpSocio.Controls.Cast<Control>())
                .Union(tbpNoSocio.Controls.Cast<Control>()))
            {
                if (control is Label label) // Usamos patrón matching para asegurar el tipo Label
                {
                    EstilosGlobales.AplicarEstiloLabel(label);
                }
                else if (control is CheckBox checkBox)
                {
                    // Aplicamos estilos directamente al CheckBox para evitar el error de conversión 
                    // de tipo al llamar a AplicarEstiloLabel, que parece esperar solo objetos Label.
                    checkBox.Font = EstilosGlobales.EstiloFuente;
                    checkBox.ForeColor = EstilosGlobales.ColorTextoClaro;
                    checkBox.BackColor = EstilosGlobales.ColorBoton;
                }
            }

            // DateTimePicker (Configuración de estilo manual)
            dtpFechaNacimiento.CalendarForeColor = EstilosGlobales.ColorTextoClaro;
            dtpFechaNacimiento.CalendarMonthBackground = EstilosGlobales.ColorBoton;
            dtpFechaNacimiento.Font = EstilosGlobales.EstiloFuente;
            dtpFechaNacimiento.CustomFormat = "dd/MM/yyyy";
            dtpFechaNacimiento.Format = DateTimePickerFormat.Custom;
            dtpFechaNacimiento.ForeColor = EstilosGlobales.ColorTextoClaro;
            dtpFechaNacimiento.BackColor = EstilosGlobales.ColorBoton;

            // Label de Información/Advertencia 
            lblInfoNoSocio.ForeColor = EstilosGlobales.ColorAdvertencia;

            // 3. Botones de Acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnGuardar);
            btnGuardar.BackColor = EstilosGlobales.ColorAcento; // Color principal para Guardar

            EstilosGlobales.AplicarEstiloBotonAccion(btnCancelar);
            btnCancelar.BackColor = EstilosGlobales.ColorFondo; // Color más neutro para Cancelar


            // 4. Configuración de Solo Lectura y Visibilidad
            txtIdPersona.Visible = false;
            lblidPersona.Visible = false;
            txtIdPersona.ReadOnly = true;
            txtNumCarnet.ReadOnly = true;
            txtUltimoPagoDia.ReadOnly = true;
            // Deshabilitamos la modificación de estados clave desde la UI de Edición.
            chkEstaVigente.AutoCheck = false;
            chkEstadoActivo.AutoCheck = false;
            chkFichaMedicaEntregada.AutoCheck = false;
        }

        #endregion
        
        #region EVENTOS DEL FORMULARIO
        
        private void FrmActualizarPersona_Load(object sender, EventArgs e)
        {

            if (_idPersonaActual > 0)
            {
                // Modo Edición
                lblTitulo.Text = "EDICIÓN DE DATOS DE PERSONA";
                btnGuardar.Text = "Guardar Cambios";
                CargarDatosParaEdicion(_idPersonaActual);
            }
            else
            {
                // Si se abre sin ID, hay un error de lógica para un formulario de edición.
                lblTitulo.Text = "ERROR DE CONFIGURACIÓN";
                btnGuardar.Enabled = false;
                MessageBox.Show("Este formulario solo se puede usar para editar personas existentes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            Utilitarios.Utilitarios.ForzarCentrado(this, pnlContenido, 0);
        }

        #endregion

        #region VALIDACIONES

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

        #region METODOS DE DATOS (Bll)

        /// <summary>
        /// Llama a la BLL para obtener los datos de la persona por ID y los carga en la UI.
        /// </summary>
        private void CargarDatosParaEdicion(int idPersona)
        {
            try
            {
                _detallePersonaCargado = oPersonaBLL.ObtenerDetallePersona(idPersona);

                if (_detallePersonaCargado == null)
                {
                    MessageBox.Show("No se encontraron los datos de la persona seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // 2. Cargar datos en los controles del GroupBox 'gbDatosPersonales'
                txtIdPersona.Text = _detallePersonaCargado.IdPersona.ToString();
                txtDni.Text = _detallePersonaCargado.Dni;
                txtNombre.Text = _detallePersonaCargado.Nombre;
                txtApellido.Text = _detallePersonaCargado.Apellido;

                if (_detallePersonaCargado.FechaNacimiento.HasValue)
                {
                    dtpFechaNacimiento.Value = _detallePersonaCargado.FechaNacimiento.Value;
                }
                else
                {
                    dtpFechaNacimiento.Value = DateTime.Today;
                }

                txtTelefono.Text = _detallePersonaCargado.Telefono;
                txtEmail.Text = _detallePersonaCargado.Email;

                // 3. Cargar datos en el GroupBox 'gpTipoYEstado'
                chkEstaVigente.Checked = _detallePersonaCargado.EstaVigente;

                // 4. Lógica del TabControl para mostrar la pestaña correcta
                if (_detallePersonaCargado.EsSocio)
                {
                    // Es Socio: mostramos la pestaña de Socio y cargamos sus datos
                    if (tabControlDetalle.TabPages.Contains(tbpNoSocio))
                        tabControlDetalle.TabPages.Remove(tbpNoSocio);

                    if (!tabControlDetalle.TabPages.Contains(tbpSocio))
                        tabControlDetalle.TabPages.Add(tbpSocio);

                    tabControlDetalle.SelectedTab = tbpSocio;

                    txtNumCarnet.Text = _detallePersonaCargado.NumeroCarnet?.ToString() ?? "NO ASIGNADO";
                    chkEstadoActivo.Checked = _detallePersonaCargado.EstadoActivo.GetValueOrDefault(false);
                    chkFichaMedicaEntregada.Checked = _detallePersonaCargado.FichaMedicaEntregada.GetValueOrDefault(false);
                }
                else
                {
                    // Es No Socio: mostramos la pestaña de No Socio
                    if (tabControlDetalle.TabPages.Contains(tbpSocio))
                        tabControlDetalle.TabPages.Remove(tbpSocio);

                    if (!tabControlDetalle.TabPages.Contains(tbpNoSocio))
                        tabControlDetalle.TabPages.Add(tbpNoSocio);

                    tabControlDetalle.SelectedTab = tbpNoSocio;

                    txtUltimoPagoDia.Text = _detallePersonaCargado.FechaPagoDia?.ToString("dd/MM/yyyy") ?? "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de la persona: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }


        /// <summary>
        /// Recolecta los datos de la UI, aplica validaciones y llama a la BLL para guardar la persona.
        /// </summary>
        private void GuardarPersona()
        {
            // 0. Validación de Modo (Solo Edición)
            if (_idPersonaActual <= 0)
            {
                MessageBox.Show("Error: Este formulario solo permite la edición de personas existentes.", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 1. VALIDACIÓN DE CAMPOS utilizando Utilitarios.Validaciones

            // Nombre Requerido
            if (!Validaciones.EsTextoRequerido(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Apellido Requerido
            if (!Validaciones.EsTextoRequerido(txtApellido.Text))
            {
                MessageBox.Show("El campo Apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // DNI/CUIL Requerido y Numérico
            if (!Validaciones.EsTextoRequerido(txtDni.Text) || !Validaciones.EsNumerico(txtDni.Text))
            {
                MessageBox.Show("El DNI / CUIL es obligatorio y debe ser un valor numérico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Teléfono Numérico (opcional pero si se ingresa debe ser válido)
            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) && !Validaciones.EsNumerico(txtTelefono.Text))
            {
                MessageBox.Show("El Teléfono debe contener solo números (o déjelo vacío).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Email Formato Válido (opcional pero si se ingresa debe ser válido)
            if (!Validaciones.EsFormatoEmailValido(txtEmail.Text))
            {
                MessageBox.Show("El Email ingresado no tiene un formato válido (ej: usuario@dominio.com).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Recolección de Datos
            Entidades.PersonaDetalleDTO detalleAActualizar = new Entidades.PersonaDetalleDTO();

            // Usamos _idPersonaActual para asegurarnos de que el ID es válido para edición
            detalleAActualizar.IdPersona = _idPersonaActual;

            // Mapeo de campos editables (datos básicos de Persona)
            detalleAActualizar.Dni = txtDni.Text.Trim();
            detalleAActualizar.Nombre = txtNombre.Text.Trim();
            detalleAActualizar.Apellido = txtApellido.Text.Trim();
            detalleAActualizar.Telefono = txtTelefono.Text.Trim();
            detalleAActualizar.Email = txtEmail.Text.Trim();
            detalleAActualizar.FechaNacimiento = dtpFechaNacimiento.Value;
            // Se envía el estado vigente actual. La baja/alta lógica se manejaría en otro control.
            detalleAActualizar.EstaVigente = chkEstaVigente.Checked;

            try
            {
                // Llamada a la BLL para Actualizar
                string respuesta = oPersonaBLL.ActualizarDatosPersona(detalleAActualizar);

                // 3. Mostrar Resultado
                if (respuesta == "OK")
                {
                    MessageBox.Show("La persona ha sido actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Indica que se hizo un cambio
                    this.Close();
                }
                else
                {
                    // Muestra los errores de validación de DNI (duplicado) o errores de SQL
                    MessageBox.Show(respuesta, "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura cualquier error de BLL no manejado (ej. excepción de conexión)
                MessageBox.Show("Error crítico durante el proceso de Guardado: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion
        
        #region BOTONES DE ACCION
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarPersona();
        }

        #endregion

        
    }
}
