
using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.Utilitarios;
using MySql.Data.MySqlClient;
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
    public partial class FrmGestionUsuarios : Form
    {

        #region PROPIEDADES Y CAMPOS PRIVADOS
        // 1. CAMPO CRÍTICO: Para centrado dinámico.
        private readonly int _anchoMenuLateral;
        private int idSeleccionado = -1;

        #endregion

        #region CONTRUCTOR

        public FrmGestionUsuarios(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;

            ConfigurarFormulario();


            this.Load += FrmGestionUsuarios_Load;

        }


        #endregion

        #region CONFIGURACION UI


        private void ConfigurarFormulario()
        {
            // 1. Primero, se establece la ubicación y los textos.
            EstablecerGeometriaYTextos();

            // 2. Luego, se aplica el formato visual (color/fuente).
            ConfigurarEstilos();
        }

        private void EstablecerGeometriaYTextos()
        {

            pnlBase.Location = new Point(20, 20); // Posición inicial simple
            pnlBase.Size = new Size(1000, 650); // Altura reducida a 650px
            // --- 1. TÍTULO ---
            lblTituloPrincipal.Text = MensajesUI.USUARIOS_TITULO_FORM; // ✅ Uso de constante
            lblTituloPrincipal.AutoSize = true;
            lblTituloPrincipal.Location = new Point(20, 25); // Alineado más a la izquierda

            // --- 2. GRUPOBOX DE DATOS USUARIO (A la izquierda) ---
            gbFiltros.Text = MensajesUI.USUARIOS_GB_DATOS; // ✅ Uso de constante
            gbFiltros.Location = new Point(20, 80);
            gbFiltros.Size = new Size(420, 220); // Ancho aumentado para mejor estética

            // --- 3. ETIQUETAS (Dentro del GroupBox) ---
            lblUsuario.Text = MensajesUI.USUARIOS_LBL_USUARIO; // ✅ Uso de constante
            lblUsuario.Location = new Point(18, 35);
            lblUsuario.AutoSize = true;

            lblContrasena.Text = MensajesUI.USUARIOS_LBL_CONTRASENA; // ✅ Uso de constante
            lblContrasena.Location = new Point(18, 75);
            lblContrasena.AutoSize = true;

            lblConfirmarContrasena.Text = MensajesUI.USUARIOS_LBL_CONFIRMAR_CONTRASENA; // ✅ Uso de constante
            lblConfirmarContrasena.Location = new Point(18, 115);
            lblConfirmarContrasena.AutoSize = true;

            lblRol.Text = MensajesUI.USUARIOS_LBL_ROL; // ✅ Uso de constante
            lblRol.Location = new Point(18, 155);
            lblRol.AutoSize = true;

            // --- 4. TXT y CMBOX (Dentro del GroupBox) ---
            txtUsuario.Location = new Point(220, 35); // Ajuste de posición
            txtUsuario.Size = new Size(180, 25);
            txtUsuario.MaxLength = 10;//10 caracteres

            txtContrasena.Location = new Point(220, 75);
            txtContrasena.Size = new Size(180, 25);
            txtContrasena.PasswordChar = '•'; // ✅ Seguridad
            txtContrasena.MaxLength = 15;

            txtConfirmarContrasena.Location = new Point(220, 115);
            txtConfirmarContrasena.Size = new Size(180, 25);
            txtConfirmarContrasena.PasswordChar = '•'; // ✅ Seguridad
            txtConfirmarContrasena.MaxLength = 15;

            cmbRol.Location = new Point(220, 155);
            cmbRol.Size = new Size(180, 25);

            // --- 5. DATA GRID VIEW (A la derecha) ---
            dgvUsuarios.Size = new Size(540, 590); // Más ancho y alto para aprovechar el espacio
            dgvUsuarios.Location = new Point(450, 80); // Posicionado junto al GroupBox

            // --- 6, 7, 8, 9. BOTONES DE ACCIÓN ---
            // Reposicionamos los botones para que estén bajo el GroupBox.
            int yPosBoton = 320;
            int xStart = 20;

            btnRegistrar.Text = MensajesUI.USUARIOS_BTN_REGISTRAR; // ✅ Uso de constante (estado inicial)
            btnRegistrar.Location = new Point(xStart, yPosBoton);
            btnRegistrar.Size = new Size(100, 40);

            btnEditar.Text = MensajesUI.USUARIOS_BTN_EDITAR; // ✅ Uso de constante
            btnEditar.Location = new Point(xStart + 110, yPosBoton);
            btnEditar.Size = new Size(100, 40);

            btnEliminar.Text = MensajesUI.USUARIOS_BTN_ELIMINAR; // ✅ Uso de constante
            btnEliminar.Location = new Point(xStart + 220, yPosBoton);
            btnEliminar.Size = new Size(100, 40);

            btnCerrar.Text = MensajesUI.USUARIOS_BTN_CERRAR; // ✅ Uso de constante
            btnCerrar.Location = new Point(xStart + 330, yPosBoton);
            btnCerrar.Size = new Size(100, 40);
        }

        private void ConfigurarEstilos()
        {
            // 1. Estilos del Formulario y Contenedores
            EstilosGlobales.AplicarFormatoBase(this);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            EstilosGlobales.AplicarEstiloTitulo(lblTituloPrincipal);

            // 2. Estilos de GroupBox y Labels
            EstilosGlobales.AplicarEstiloGroupBox(gbFiltros);

            // 3. Estilos de Campos de Entrada
            EstilosGlobales.AplicarEstiloCampo(txtUsuario);
            EstilosGlobales.AplicarEstiloCampo(txtContrasena);
            EstilosGlobales.AplicarEstiloCampo(txtConfirmarContrasena);

            // Aplicación de estilo al ComboBox y restricción de edición
            EstilosGlobales.AplicarEstiloCampo(cmbRol);
            if (cmbRol is ComboBox cmb)
            {
                // ✅ RESTRICCIÓN: Impide escribir texto, solo permite seleccionar.
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            // 4. Estilos de DataGridView
            EstilosGlobales.AplicarEstiloDGV(dgvUsuarios);

            // 5. Estilos de Botones
            EstilosGlobales.AplicarEstiloBotonAccion(btnRegistrar);
            btnRegistrar.BackColor = EstilosGlobales.ColorExito;
            EstilosGlobales.AplicarEstiloBotonAccion(btnEditar);
            btnEditar.BackColor = EstilosGlobales.ColorAdvertencia;
            EstilosGlobales.AplicarEstiloBotonAccion(btnEliminar);
            btnEliminar.BackColor= EstilosGlobales.ColorError;
            EstilosGlobales.AplicarEstiloBotonAccion(btnCerrar);
        }


        #endregion

        #region EVENTOS DE FORMULARIO

        private void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            CargarRoles();

            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));

            CargarUsuarios();
            EstablecerEstadoBotones();
        }

        #endregion

        #region VALIDACIONES ENTRADAS

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ForzarMayusculas(e);
            Validaciones.SoloAlfanumericos(e);
        }



        #endregion

        #region METODOS/LOGICA DE DATOS


        private void CargarUsuarios()
        {
            try
            {
                UsuarioBLL oUsuarioBLL = new UsuarioBLL();
                DataTable dt = oUsuarioBLL.ListarUsuarios();

                dgvUsuarios.DataSource = dt;

                // Ajustar columnas y filas
                dgvUsuarios.Columns[0].HeaderText = "ID Usuario";
                dgvUsuarios.Columns[1].HeaderText = "Nombre de Usuario";
                dgvUsuarios.Columns[2].HeaderText = "Rol";

                //dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dgvUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvUsuarios.Columns["IdUsuario"].Visible = false;
                dgvUsuarios.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError("Error al cargar los usuarios: " + ex.Message, MensajesUI.TITULO_ERROR);
            }
        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtConfirmarContrasena.Clear();
            cmbRol.SelectedIndex = -1;
            idSeleccionado = -1;
            EstablecerEstadoBotones();
        }

        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Recepcionista");
            cmbRol.SelectedIndex = -1;
        }

        private void EstablecerEstadoBotones()
        {
            // Si NO hay un ID seleccionado (idSeleccionado == -1) -> MODO REGISTRO
            if (idSeleccionado == -1)
            {
                btnRegistrar.Text = MensajesUI.USUARIOS_BTN_REGISTRAR; // ✅ Uso de constante
                btnRegistrar.BackColor = EstilosGlobales.ColorExito;
                btnRegistrar.Enabled = true;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            // Si SÍ hay un ID seleccionado (idSeleccionado > -1) -> MODO EDICIÓN/NUEVO
            else
            {
                btnRegistrar.Text = MensajesUI.USUARIOS_BTN_NUEVO; // ✅ Uso de constante
                btnRegistrar.BackColor = EstilosGlobales.ColorAcento;
                btnRegistrar.Enabled = true; // Sigue habilitado para cancelar la edición
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            }
        }
        #endregion

        #region BOTONES DE ACCION


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // 🚨 LÓGICA DE DESVÍO: Si estamos en Modo Edición (el botón dice "NUEVO"), limpiamos y salimos.
            if (idSeleccionado > -1)
            {
                LimpiarCampos();
                dgvUsuarios.ClearSelection();
                return; // Terminamos la ejecución del método aquí.
            }

            // --- LÓGICA DE REGISTRO (Solo se ejecuta si idSeleccionado == -1) ---

            UsuarioBLL oUsuarioBLL = new UsuarioBLL();

            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string confirmar = txtConfirmarContrasena.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(confirmar))
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_CAMPOS_OBLIGATORIOS);
                return;
            }

            if (contrasena != confirmar)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_CONTRASENAS_NO_COINCIDEN);
                return;
            }

            string rolSeleccionado = cmbRol.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(rolSeleccionado))
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_SELECCION_ROL_OBLIGATORIA);
                return;
            }

            // Llamar a la capa de negocio
            string respuesta = oUsuarioBLL.RegistrarUsuario(usuario, contrasena, rolSeleccionado);

            if (respuesta.Equals("OK"))
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarExito
                Prompt.MostrarExito(MensajesUI.USUARIOS_MSG_REGISTRO_EXITO);
                LimpiarCampos();
                CargarUsuarios();
            }
            else
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.USUARIOS_MSG_REGISTRO_ERROR + respuesta);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_USUARIO_SELECCION_OBLIGATORIA);
                return;
            }

            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            string confirmar = txtConfirmarContrasena.Text.Trim();
            string rolSeleccionado = cmbRol.SelectedItem?.ToString();

            if (contrasena != confirmar)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_CONTRASENAS_NO_COINCIDEN);
                return;
            }

            UsuarioBLL oUsuarioBLL = new UsuarioBLL();
            string respuesta = oUsuarioBLL.ModificarUsuario(idSeleccionado, usuario, contrasena, rolSeleccionado);

            if (respuesta.Equals("OK"))
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarExito
                Prompt.MostrarExito(MensajesUI.USUARIOS_MSG_MODIFICACION_EXITO);

                // Actualizar la grilla
                CargarUsuarios();

                LimpiarCampos();

            }
            else
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.USUARIOS_MSG_MODIFICACION_ERROR + respuesta);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.USUARIOS_MSG_USUARIO_SELECCION_OBLIGATORIA);
                return;
            }

            
            // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarDialogoConfirmacion
            if (Prompt.MostrarDialogoConfirmacion(MensajesUI.USUARIOS_MSG_CONFIRMAR_ELIMINAR))
            {
                UsuarioBLL oUsuarioBLL = new UsuarioBLL();
                string respuesta = oUsuarioBLL.EliminarUsuario(idSeleccionado);

                if (respuesta.Equals("OK"))
                {
                    // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarExito
                    Prompt.MostrarExito(MensajesUI.USUARIOS_MSG_ELIMINACION_EXITO);

                    CargarUsuarios(); // Refresca el DataGridView
                    LimpiarCampos();
                }
                else
                {
                    // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                    Prompt.MostrarError(MensajesUI.USUARIOS_MSG_ELIMINACION_ERROR + respuesta);
                }
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                // Guardar el ID oculto en una variable de clase
                idSeleccionado = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                txtUsuario.Text = fila.Cells["NombreUsuario"].Value.ToString();
                cmbRol.SelectedItem = fila.Cells["Rol"].Value.ToString();

                txtContrasena.Clear();
                txtConfirmarContrasena.Clear();
                txtContrasena.Focus();
                EstablecerEstadoBotones();
            }
        }





        #endregion



        
    
    }
}
