using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.Utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubDeportivo.UI
{
    public partial class FrmLogin : Form
    {

        #region PROPIEDADES Y CAMPOS PRIVADOS
        private readonly UsuarioBLL oUsuarioBLL = new UsuarioBLL();
        private bool _isCleaning = false; // <<< NUEVO FLAG CRÍTICO

        #endregion

        #region CONSTRUCTOR
        public FrmLogin()
        {
            InitializeComponent();
            this.Size = new Size(600, 300);
            ConfigurarFormulario();
        }


        #endregion

        #region CONFIGURACION UI

        private void ConfigurarFormulario()
        {
            ConfigurarEstilos();
            EstablecerGeometriaYTextos();

        }

        private void ConfigurarEstilos()
        {
            EstilosGlobales.AplicarFormatoBase(this);
            //titulo
            lblTitulo.ForeColor = EstilosGlobales.ColorAcento;
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;

            //panel
            pnlLateral.BackColor = EstilosGlobales.ColorAcento;

            //botones
            EstilosGlobales.AplicarEstiloBotonMenu(btnIngresar);
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Cursor = Cursors.Hand;

            //picture box
            pbCerrar.Cursor = Cursors.Hand;
            pbCerrar.BackColor = EstilosGlobales.ColorAcento;
            // Cargar la imagen desde la carpeta custom "Resources"
            try
            {
                // La imagen debe estar configurada como Contenido/Copiar si es posterior

                pbCerrar.Image = Image.FromFile("Resources\\Img\\imgClose.png");
                pbCerrar.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (FileNotFoundException)
            {
                pbCerrar.BackColor = Color.Red;
            }

            pbLogoClub.SizeMode = PictureBoxSizeMode.Zoom;
            try
            {

                pbLogoClub.Image = Image.FromFile("Resources\\Img\\logoClub.png");
            }
            catch (System.IO.FileNotFoundException)
            {
                pbLogoClub.BackColor = Color.Red;
                pbLogoClub.Text = "LOGO";
                pbLogoClub.ForeColor = Color.White;
            }


            //txt
            EstilosGlobales.AplicarEstiloCampo(txtPass);
            EstilosGlobales.AplicarEstiloCampo(txtUsuario);


        }


        private void EstablecerGeometriaYTextos()
        {
            // --- ESTILO GENERAL DEL FORMULARIO ---
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            //TÍTULO (lblTitulo)
            lblTitulo.Text = MensajesUI.LOGIN_TITULO_FORM;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(250, 40);

            // 1. PANEL LATERAL (pnlLateral)
            pnlLateral.Size = new Size(200, 300);
            pnlLateral.Location = new Point(0, 0);

            // NUEVO: LOGO DEL CLUB DENTRO DEL PANEL LATERAL (pbLogoClub)
            pbLogoClub.Size = new Size(150, 150);
            pbLogoClub.Location = new Point(25, 75); // Centrado en el panel lateral (200 - 150) / 2 = 25

            // 2. BOTÓN CERRAR (pbCerrar)
            pbCerrar.Size = new Size(20, 20);
            pbCerrar.Location = new Point(560, 10);

            // 3. TEXTBOX USUARIO (txtUsuario)
            txtUsuario.Text = MensajesUI.LOGIN_PLACEHOLDER_USUARIO; // Placeholder inicial
            txtUsuario.Size = new Size(300, 25);
            txtUsuario.Location = new Point(250, 100);
            txtUsuario.ForeColor = Color.DimGray; // Inicia con color gris para placeholder
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;

            // 4. TEXTBOX CONTRASEÑA (txtPass)
            txtPass.Text = MensajesUI.LOGIN_PLACEHOLDER_PASS; // Placeholder inicial
            txtPass.Size = new Size(300, 25);
            txtPass.Location = new Point(250, 150);
            txtPass.ForeColor = Color.DimGray; // Inicia con color gris para placeholder
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.UseSystemPasswordChar = false; // Se usa false para mostrar "Contraseña"

            // 5. BOTÓN INGRESAR (btnIngresar)
            btnIngresar.Text = MensajesUI.LOGIN_BOTON_INGRESAR;
            btnIngresar.Size = new Size(300, 40);
            btnIngresar.Location = new Point(250, 200);

        }



        #endregion

        #region EVENTOS DE FORMULARIO

        private void FormLogin_Shown(object sender, EventArgs e)
        {
            RestablecerEstadoInicial();

        }

        #endregion

        #region VALIDACION DE ENTRADA

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (_isCleaning) return;

            // **CRÍTICO:** Si el color es Color.DimGray (el color del placeholder), significa que el campo está vacío.
            if (txtUsuario.ForeColor == Color.DimGray)
            {
                txtUsuario.Text = string.Empty; // Borrado sin depender de la coincidencia de texto

                // Cambiamos al color normal de texto de la aplicación
                txtUsuario.ForeColor = Utilitarios.EstilosGlobales.ColorTextoClaro;
            }


        }


        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (_isCleaning) return;

            // **CRÍTICO:** Si el color es Color.DimGray (el color del placeholder), borramos.
            if (txtPass.ForeColor == Color.DimGray)
            {
                txtPass.Text = string.Empty;

                txtPass.ForeColor = Utilitarios.EstilosGlobales.ColorTextoClaro;
                txtPass.UseSystemPasswordChar = true; // Activamos los puntos/asteriscos
            }


        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Text = MensajesUI.LOGIN_PLACEHOLDER_USUARIO;
                txtUsuario.ForeColor = Color.DimGray; // Vuelve a un color atenuado (placeholder)
            }
        }


        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                txtPass.Text = MensajesUI.LOGIN_PLACEHOLDER_PASS;
                txtPass.ForeColor = Color.DimGray;
                // CLAVE: Cuando es placeholder, NO debe ser un campo de contraseña.
                txtPass.UseSystemPasswordChar = false;
            }

        }


        #endregion


        #region METODOS Y LOGICA DE DATOS

        public void RestablecerEstadoInicial()
        {
            _isCleaning = true; // 1. Proteger el proceso

            // 2. Limpieza de valores
            txtUsuario.Clear();
            txtPass.Clear();

            // 3. Forzar el estado visual para el placeholder
            txtPass.UseSystemPasswordChar = false;

            //txtUsuario_Leave(txtUsuario, EventArgs.Empty);
            //txtPass_Leave(txtPass, EventArgs.Empty);
            // 4. Ejecutar la lógica de placeholder (que inserta el texto y color DimGray)
            // Se puede llamar directamente a la lógica en lugar de los eventos Leave, ya que es más directo:
            txtUsuario.Text = MensajesUI.LOGIN_PLACEHOLDER_USUARIO;
            txtUsuario.ForeColor = Color.DimGray;

            txtPass.Text = MensajesUI.LOGIN_PLACEHOLDER_PASS;
            txtPass.ForeColor = Color.DimGray;

            // 5. Salir del modo de limpieza y colocar foco
            txtUsuario.Focus();
            _isCleaning = false;

        }


        #endregion

        #region BOTONES DE ACCION
        private void pbCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnIngresar_Click(object sender, EventArgs e)
        {

            // Validación para asegurar que no están vacíos o con el placeholder
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtUsuario.Text == MensajesUI.LOGIN_PLACEHOLDER_USUARIO ||
                string.IsNullOrWhiteSpace(txtPass.Text) || txtPass.Text == MensajesUI.LOGIN_PLACEHOLDER_PASS)
            {
                Utilitarios.Prompt.Alerta(
                     MensajesUI.LOGIN_MSG_CAMPOS_REQUERIDOS, // ⬅️ Refactorizado
                     MensajesUI.TITULO_ADVERTENCIA, // Usa la constante general de Advertencia
                     Utilitarios.Prompt.IconType.Advertencia
                 );
                return;
            }

            try
            {

                string usuario = txtUsuario.Text.Trim();
                string clave = txtPass.Text.Trim();

                Usuario usuarioLogueado = oUsuarioBLL.IniciarSesion(usuario, clave);

                if (usuarioLogueado != null)
                {
                    string mensajeBienvenida = MensajesUI.LOGIN_MSG_BIENVENIDA_PARTE1 + usuarioLogueado.NombreUsuario + "."; // ⬅️ Refactorizado con concatenación

                    Utilitarios.Prompt.Alerta(
                        mensajeBienvenida,
                        MensajesUI.LOGIN_TITULO_ACCESO_CONCEDIDO, // ⬅️ Refactorizado
                        Utilitarios.Prompt.IconType.Informacion
                    );

                    this.Hide();

                    FrmPrincipal formPrincipal = new FrmPrincipal(usuarioLogueado, this); // 'this' es la instancia de FrmLogin
                    formPrincipal.Show();

                }
                else
                {
                    Utilitarios.Prompt.Alerta(
                         MensajesUI.LOGIN_MSG_ACCESO_DENEGADO, // ⬅️ Refactorizado
                         MensajesUI.LOGIN_TITULO_ACCESO_DENEGADO, // ⬅️ Refactorizado
                         Utilitarios.Prompt.IconType.Error
                     );

                    RestablecerEstadoInicial();

                }
            }
            catch (Exception ex)
            {
                // Usamos string.Format con el placeholder {0} de la constante
                string mensajeCritico = string.Format(MensajesUI.LOGIN_MSG_ERROR_CRITICO, ex.Message); // ⬅️ Refactorizado

                // Usamos la utilidad Prompt para el error crítico
                Prompt.MostrarError(mensajeCritico);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // 1. Si el campo contiene texto, aseguramos el color normal.
            if (!string.IsNullOrEmpty(txtUsuario.Text) && txtUsuario.ForeColor == Color.DimGray)
            {
                // Usamos la constante global de tu aplicación: Color.FromArgb(200, 200, 200)
                txtUsuario.ForeColor = Utilitarios.EstilosGlobales.ColorTextoClaro;
            }

            //// 2. Si el usuario borra todo, aseguramos que el Leave se encargue.
            //if (string.IsNullOrEmpty(txtUsuario.Text))
            //{
            //    // Forzamos el Leave para que se ponga el placeholder "Usuario"
            //    txtUsuario_Leave(txtUsuario, EventArgs.Empty);
            //}
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPass.Text) && txtPass.ForeColor == Color.DimGray)
            {
                txtPass.ForeColor = Utilitarios.EstilosGlobales.ColorTextoClaro;
                // La activación del UseSystemPasswordChar ya se hace en _Enter
            }

            //if (string.IsNullOrEmpty(txtPass.Text))
            //{
            //    txtPass_Leave(txtPass, EventArgs.Empty);
            //}
        }



        #endregion


        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // 🚨 CORRECCIÓN CLAVE: Forzar la carga de la conexión ANTES de iniciar el Login.
            // Esto dispara la Autoconfiguración/InputBox si el archivo .INI no existe.
            try
            {
                Conexion.getInstancia();
            }
            catch (Exception ex)
            {
                // Si la configuracion falla, mostramos error simple (sin acentos para evitar el error de guardado) y cerramos.
                MessageBox.Show("Error de inicializacion de conexion. La aplicacion se cerrara. Detalles: " + ex.Message,
                                "Error Critico", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }
    }
}
