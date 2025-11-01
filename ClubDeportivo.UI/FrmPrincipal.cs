using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClubDeportivo.UI.Utilitarios;
using ClubDeportivo.UI.Entidades;

namespace ClubDeportivo.UI
{
    public partial class FrmPrincipal : Form
    {

        #region CAMPOS Y PROPIEDADES

        private readonly Usuario _usuarioLogueado;
        private readonly FrmLogin _frmLoginPadre; // ¡Nuevo campo para guardar el FrmLogin!
        private bool _cerrandoSesion = false; // Nuevo flag


        #endregion

        #region CONSTRUCTOR
        public FrmPrincipal(Usuario usuario, FrmLogin frmLogin)
        {
            InitializeComponent();

            _usuarioLogueado = usuario;
            _frmLoginPadre = frmLogin;

            AplicarEstilosEstaticos();

            MostrarInfoUsuario();

            //Adjunta el nuevo método ConfigurarVisualizacionFinal al evento Load
            this.Load += FrmPrincipal_Load;

            // ASIGNACIÓN CRÍTICA: Asignar el método al evento FormClosing
            //this.FormClosing += FrmPrincipal_FormClosing;
        }


        #endregion

        #region CONFIGURACION UI

        private void AplicarEstilosEstaticos()
        {
            // 1. FORMATO BASE DEL FORMULARIO
            EstilosGlobales.AplicarFormatoBase(this);
            this.FormBorderStyle = FormBorderStyle.None; // Modo sin borde para diseño custom
            this.Size = new Size(1400, 950);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. CONFIGURACIÓN DE PANELES PRINCIPALES
            pnlSuperior.BackColor = EstilosGlobales.ColorAcento;
            pnlInferior.BackColor = EstilosGlobales.ColorAcento;
            pnlCentral.BackColor = EstilosGlobales.ColorFondo;

            pnlSuperior.Height = 50;
            pnlInferior.Height = 30;

            // 3. CONFIGURACIÓN DE PANELES HIJOS EN pnlCentral
            pnlMenu.BackColor = EstilosGlobales.ColorAcento;
            pnlContenido.BackColor = EstilosGlobales.ColorFondo;
            pnlMenu.Width = 250;

            pnlMenu.Dock = DockStyle.Left;
            pnlContenido.Dock = DockStyle.Fill;

            // 4. LOGO (PNG/JPG debe estar en Resources)
            if (pbLogoMenu != null)
            {
                try
                {
                    // Asumiendo que ahora usas PNG para el logo también
                    pbLogoMenu.Image = Image.FromFile("Resources\\Img\\logoClub.png");
                    pbLogoMenu.SizeMode = PictureBoxSizeMode.Zoom;
                    pbLogoMenu.Size = new Size(150, 120);
                    pbLogoMenu.Location = new Point((pnlMenu.Width - 150) / 2, 10);
                }
                catch (FileNotFoundException) { pbLogoMenu.BackColor = Color.Gray; }
            }

            // 5. BARRA DE ESTADO (pnlInferior)
            if (lblUsuarioStatus != null)
            {
                EstilosGlobales.AplicarEstiloLabel(lblUsuarioStatus);
                lblUsuarioStatus.Text = "Cargando...";
                lblUsuarioStatus.AutoSize = true;
                lblUsuarioStatus.Location = new Point(pnlMenu.Width + 20, 7);
            }

            // 6. BOTONES
            AplicarEstilosYPosicionamientoABotones();
        }

        private void AplicarEstilosYPosicionamientoABotones()
        {
            int yPos = 150;
            int ySpacing = 60;

            Button[] botonesMenu = new Button[] {
                btnRegistrarPagos,
                btnInscripcion,
                btnGestionPersona,
                btnGestionActividades,
                btnListadoMorosos,
                btnConfiguracionGlobal,
                btnGestionUsuarios,
                //btnCerrarSesion
            };

            // Asignación de texto
            btnGestionPersona.Text = "  Gestión de Personas";
            btnInscripcion.Text = "  Inscripción ";
            btnRegistrarPagos.Text = "  Registro de Pagos";
            btnGestionActividades.Text = "  Gestión de Actividades";
            btnListadoMorosos.Text = "  Listado de Morosos";
            btnConfiguracionGlobal.Text = "  Configuracion";
            btnGestionUsuarios.Text = "  Gestión de Usuarios";
            btnCerrarSesion.Text = "  Cerrar Sesión";

            foreach (Button btn in botonesMenu)
            {
                EstilosGlobales.AplicarEstiloBotonMenu(btn);

                // Carga de íconos .png
                try
                {
                    if (btn == btnGestionPersona) btn.Image = Image.FromFile("Resources\\Img\\usuario2.png");
                    if (btn == btnInscripcion) btn.Image = Image.FromFile("Resources\\Img\\usuario1.png");
                    if (btn == btnRegistrarPagos) btn.Image = Image.FromFile("Resources\\Img\\pago1.png");
                    if (btn == btnGestionActividades) btn.Image = Image.FromFile("Resources\\Img\\listado.png");
                    if (btn == btnListadoMorosos) btn.Image = Image.FromFile("Resources\\Img\\listado.png");
                    if (btn == btnConfiguracionGlobal) btn.Image = Image.FromFile("Resources\\Img\\pago2.png");
                    if (btn == btnGestionUsuarios) btn.Image = Image.FromFile("Resources\\Img\\usuario2.png");
                    //if (btn == btnCerrarSesion) btn.Image = Image.FromFile("Resources\\Img\\logout.png");
                }
                catch (FileNotFoundException) { }

                // Posicionamiento
                btn.Location = new Point(0, yPos);
                btn.Width = pnlMenu.Width;

                yPos += ySpacing;

                // Enlazar los eventos Click
                if (btn == btnGestionPersona) btn.Click += btnGestionPersona_Click;
                if (btn == btnInscripcion) btn.Click += btnInscripcion_Click; // Corregido: Usar un nombre de evento específico
                if (btn == btnRegistrarPagos) btn.Click += btnRegistrarPagos_Click;
                if (btn == btnGestionActividades) btn.Click += btnGestionActividades_Click;
                if (btn == btnListadoMorosos) btn.Click += btnListadoMorosos_Click;
                if (btn == btnConfiguracionGlobal) btn.Click += btnConfiguracionGlobal_Click;
                if (btn == btnGestionUsuarios) btn.Click += btnGestionUsuarios_Click; // Corregido: Usar un nombre de evento específico
                //if (btn == btnCerrarSesion) btn.Click += btnCerrarSesion_Click;
            }

            // 3. Posicionamiento Inferior para Cerrar Sesión
            Button btns = btnCerrarSesion;
            EstilosGlobales.AplicarEstiloBotonMenu(btns);

            // Carga de ícono específico para Cerrar Sesión
            try
            {
                if (btns == btnCerrarSesion) btns.Image = Image.FromFile("Resources\\Img\\logout.png");
            }
            catch (FileNotFoundException) { }

            // CÁLCULO CRÍTICO: Posición fija cerca del fondo
            // La ubicación Y se calcula: Altura total del pnlCentral - Altura del pnlInferior - Espaciado (ej. 100)
            //int yBottom = pnlMenu.Height - 50;

            // Si la altura del Formulario Principal es 950 y el pnlMenu está en DockStyle.Left,
            // su altura será 950 - pnlSuperior.Height (50) - pnlInferior.Height (30) = 870
            // pnlMenu.Height - btn.Height - Margen

            btns.Location = new Point(0, this.Height - pnlInferior.Height - 100);
            btns.Width = pnlMenu.Width;

            // Enlazar el evento Click de Cerrar Sesión
            //btns.Click += btnCerrarSesion_Click;



        }

        private void ConfigurarVisualizacionFinal()
        {
            //CENTRADO DEL TÍTULO
            if (lblTituloPrincipal != null)
            {
                lblTituloPrincipal.Text = "CLUB DEPORTIVO - SISTEMA DE GESTIÓN";
                lblTituloPrincipal.Font = EstilosGlobales.EstiloTitulo;
                lblTituloPrincipal.ForeColor = EstilosGlobales.ColorTextoClaro;
                lblTituloPrincipal.AutoSize = true;

                // CÁLCULO PARA CENTRAR EL TÍTULO en el espacio libre
                int anchoDisponible = this.Width - pnlMenu.Width;
                lblTituloPrincipal.Refresh();
                int xCentrado = pnlMenu.Width + (anchoDisponible / 2) - (lblTituloPrincipal.Width / 2);
                lblTituloPrincipal.Location = new Point(xCentrado, 12);
            }

            //  PictureBox Cerrar (pbCerrarPrincipal) - VISIBILIDAD GARANTIZADA
            if (pbCerrarPrincipal != null)
            {
                pbCerrarPrincipal.Location = new Point(this.Width - 45, 10);
                pbCerrarPrincipal.Size = new Size(30, 30);
                pbCerrarPrincipal.Cursor = Cursors.Hand;
                pbCerrarPrincipal.BringToFront();

                // Cargar imagen de cerrar (PNG confirmado)
                try
                {
                    pbCerrarPrincipal.Image = Image.FromFile("Resources\\Img\\imgClose.png");
                    pbCerrarPrincipal.SizeMode = PictureBoxSizeMode.Zoom;
                    pbCerrarPrincipal.BackColor = EstilosGlobales.ColorFondo;
                }
                catch (FileNotFoundException) { pbCerrarPrincipal.BackColor = Color.Red; }

                //pbCerrarPrincipal.Click += (s, e) => this.Close();
            }
            ConfigurarPermisosDeMenu();

        }

        private void ConfigurarPermisosDeMenu()
        {
            if (_usuarioLogueado.Rol != "Administrador")
            {
                // Ocultar botones si no es Administrador
                btnGestionUsuarios.Visible = false;
                btnListadoMorosos.Visible = false;
                btnGestionActividades.Visible = false;
                btnConfiguracionGlobal.Visible = false;
                // Opcional: Reubicar el botón 'Cerrar Sesión'
                // Si tienes otros botones que necesitan subir, haz el reposicionamiento aquí
            }
        }





        #endregion

        #region LOGICA DE DATOS

        private void MostrarInfoUsuario()
        {

            if (lblUsuarioStatus != null)
            {
                string nomUsuario = $"Usuario Conectado: **{_usuarioLogueado.NombreUsuario}** ({_usuarioLogueado.Rol})";
                lblUsuarioStatus.Text = nomUsuario.ToUpper();
            }

        }


        #endregion


        #region LOGICA DE UI

        public void AbrirFormularioHijo(Form formHijo)
        {
            // 1. Cierra formularios anteriores que estuvieran en el panel
            this.pnlContenido.Controls.Clear();


            // 2. Configura el formulario para ser contenido
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Anchor = AnchorStyles.None;

            // CRÍTICO: El formulario hijo debe ocupar todo el pnlContenido
            formHijo.Dock = DockStyle.Fill;

            // 3. Agrega el formulario al panel contenedor y lo muestra
            this.pnlContenido.Controls.Add(formHijo);
            formHijo.Show();


        }


        #endregion

        #region EVENTOS DE FORMULARIO

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarVisualizacionFinal();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Si estamos cerrando sesión, no preguntamos.
            if (_cerrandoSesion)
            {
                return; // Salir silenciosamente.
            }

            // 2. Si no es por cerrar sesión, procedemos con la pregunta al usuario.
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult resultado = Utilitarios.Prompt.Confirmar(
                    "¿Está seguro que desea salir del sistema y cerrar la aplicación?",
                    "Confirmación de Salida"
                );

                if (resultado == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    // Solo si es un cierre total, cerramos la instancia oculta del Login
                    _frmLoginPadre.Close();
                }
            }
        }



        #endregion


        #region EVENTOS DE BOTONES

        private void pbCerrarPrincipal_Click(object sender, EventArgs e)
        {
            this.Close(); // Esto dispara el evento FormClosing
        }


        //INSCRIPCION
        private void btnInscripcion_Click(object sender, EventArgs e)
        {
            FrmInscripcionClub frm = new FrmInscripcionClub(this.pnlMenu.Width);
            AbrirFormularioHijo(frm);
        }


        //REGISTRAR PAGO
        private void btnRegistrarPagos_Click(object sender, EventArgs e)
        {
            FrmRegistrarPagos frm = new FrmRegistrarPagos(this.pnlMenu.Width);
            AbrirFormularioHijo(frm);
        }

        //GESTION PERSONAS
        private void btnGestionPersona_Click(object sender, EventArgs e)
        {
            FrmGestionPersonas frm = new FrmGestionPersonas(this.pnlMenu.Width);
            AbrirFormularioHijo(frm);
        }

        //ACTIVIDADES
        private void btnGestionActividades_Click(object sender, EventArgs e)
        {
            FrmGestionActividades frm = new FrmGestionActividades(this.pnlMenu.Width);
            AbrirFormularioHijo(frm);
        }

        //LISTA MOROSOS
        private void btnListadoMorosos_Click(object sender, EventArgs e)
        {
            FrmControlCuotas frm = new FrmControlCuotas(this.pnlMenu.Width);
            AbrirFormularioHijo(frm);
        }

        //CONFIGURACION
        private void btnConfiguracionGlobal_Click(object sender, EventArgs e)
        {
            FrmConfiguracion form = new FrmConfiguracion(this.pnlMenu.Width);
            AbrirFormularioHijo(form);
        }

        //USUARIOS

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            FrmGestionUsuarios form = new FrmGestionUsuarios(this.pnlMenu.Width);
            AbrirFormularioHijo(form);
        }

        //CERRAR SESION
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // 1. Preguntar al usuario antes de cerrar
            DialogResult resultado = Utilitarios.Prompt.Confirmar(
                   "¿Está seguro que desea cerrar la sesión actual?",
                    "Confirmación de Cierre"
             );

            if (resultado == DialogResult.Yes)
            {
                _cerrandoSesion = true;

                // 2. Limpiar el login y mostrarlo
                _frmLoginPadre.RestablecerEstadoInicial();
                _frmLoginPadre.Show();

                // 3. Cerrar el FrmPrincipal
                this.Close(); // DIPSARA CLOSING, que será ignorado por el flag
            }
            // Si la respuesta es No, no hacemos nada y el usuario sigue en el FrmPrincipal.
        }

        #endregion


       
    }
}
