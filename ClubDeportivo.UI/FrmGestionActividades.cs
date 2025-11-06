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
using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.DAL;



namespace ClubDeportivo.UI
{
    public partial class FrmGestionActividades : Form
    {

        #region PROPIEDAD Y CAMPOS

        private readonly int _anchoMenuLateral;
        private ActividadBLL _actividadBLL = new ActividadBLL();
        private int idActividadSeleccionada = 0;
        private bool modoEdicion = false;


        #endregion

        #region CONSTRUCTOR
        public FrmGestionActividades(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;
            ConfigurarFormulario();
            // Suscripciones
            this.Load += FrmGestionActividades_Load;

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

            // Título
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;
            lblTitulo.ForeColor = EstilosGlobales.ColorAcento;

            // Elementos de Formulario (GroupBox de la derecha)
            gbActividad.BackColor = Color.FromArgb(45, 45, 45);
            gbActividad.ForeColor = EstilosGlobales.ColorTextoClaro;
            gbActividad.Font = EstilosGlobales.EstiloFuente;

            // Campos de entrada
            EstilosGlobales.AplicarEstiloCampo(txtNombreActividad);
            EstilosGlobales.AplicarEstiloCampo(txtHorario); // APLICAR ESTILO AL NUEVO txtHorario
            EstilosGlobales.AplicarEstiloCampo(txtCostoDiario);
            

            // Botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnGuardar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnCancelar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnEditar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnEliminar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnCerrar);

            // Configuración del DataGridView
            EstilosGlobales.AplicarEstiloDataGridView(dgvActividades);
            // Nuevos estilos para los Labels
            lblNombre.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblNombre.Font = EstilosGlobales.EstiloFuente;
            lblHorario.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblHorario.Font = EstilosGlobales.EstiloFuente;
            lblCostoDiario.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblCostoDiario.Font = EstilosGlobales.EstiloFuente;
        }


        private void EstablecerGeometriaYTextos()
        {
            // --- GEOMETRÍA DE PANELES Y CONTENEDORES ---

            // Panel Base (Se recomienda usar un tamaño fijo para centrar)
            pnlBase.Size = new Size(1000, 550);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            // Panel de Listado (Izquierda: 45000px de ancho)            
            int anchoListado = 500;
            pnlListado.Size = new Size(anchoListado, pnlBase.Height - 140);
            pnlListado.Location = new Point(110, 80);
            // CRÍTICO: Aseguramos que la grilla ocupe todo el panel de listado
            dgvActividades.Dock = DockStyle.Fill;

            // GroupBox de Formulario             
            int anchoForm = 340;
            gbActividad.Size = new Size(anchoForm, pnlBase.Height - 230);
            gbActividad.Location = new Point(pnlListado.Right + 15, 80);



            // --- TAMAÑOS Y POSICIONES DE CAMPOS (CORRECCIÓN) ---

            int margenX = 15; // Margen interno del GroupBox
            int separacionVertical = 25;
            int altoCampo = 25; // Altura estándar para campos de texto
            int anchoCampo = anchoForm - (margenX * 2); // Ancho total menos los márgenes (Ej: 360 - 30 = 330px)

            int yPos = 30; // Posición inicial debajo del título del GroupBox

            // 1. Nombre de Actividad
            lblNombre.Text = "Nombre de la Actividad:";
            lblNombre.Location = new Point(margenX, yPos);
            yPos += 20;
            txtNombreActividad.Location = new Point(margenX, yPos);
            txtNombreActividad.Size = new Size(anchoCampo, altoCampo);
            txtNombreActividad.MaxLength = 15;
            yPos += altoCampo + separacionVertical;

            // 2. Horario
            lblHorario.Text = "Horario:";

            lblHorario.Location = new Point(margenX, yPos);
            yPos += 20;
            txtHorario.Location = new Point(margenX, yPos);
            txtHorario.Size = new Size(anchoCampo, altoCampo);
            txtHorario.MaxLength = 25;
            yPos += altoCampo + separacionVertical;

            // 3. Costo Diario
            lblCostoDiario.Text = "Costo Diario (en ARS):";
            lblCostoDiario.Location = new Point(margenX, yPos);
            yPos += 20;
            txtCostoDiario.Location = new Point(margenX, yPos);
            txtCostoDiario.Size = new Size(anchoCampo, altoCampo);
            txtCostoDiario.MaxLength = 10;
            yPos += altoCampo + separacionVertical;

            int yPosBotones = txtCostoDiario.Top + txtCostoDiario.Height + 30; // Separación de 30px


            // --- POSICIÓN Y TAMAÑO DE BOTONES (CORRECCIÓN CRÍTICA DE DISTANCIA) ---
            int anchoBoton = 150;
            int separacionBotones = 10;

            // Botones dentro de GroupBox (Derecha)
            btnGuardar.Size = new Size(anchoBoton, 40);
            // *** APLICAR NUEVA POSICIÓN Y ***
            btnGuardar.Location = new Point(15, yPosBotones);

            btnCancelar.Size = new Size(anchoBoton, 40);
            // *** APLICAR NUEVA POSICIÓN Y ***
            btnCancelar.Location = new Point(btnGuardar.Right + separacionBotones, yPosBotones);

            // Botón de Edición (Debajo del listado)           
            btnEditar.Size = new Size(anchoBoton + 50, 40);
            btnEditar.Location = new Point(pnlListado.Left + 30, pnlListado.Bottom + 10);
            btnEliminar.Size = new Size(anchoBoton + separacionBotones, 40);
            btnEliminar.Location = new Point(btnEditar.Left + 220, pnlListado.Bottom + 10);

            btnCerrar.Size = new Size(anchoBoton + separacionBotones, 40);
            btnCerrar.Location = new Point(btnEliminar.Left + 260, pnlListado.Bottom + 10);

            // --- TEXTOS ETIQUETAS/TÍTULOS ---
            lblTitulo.Text = "GESTIÓN DE ACTIVIDADES";
            gbActividad.Text = "Datos de Actividad (Alta / Edición)";
            btnGuardar.Text = "GUARDAR";
            btnCancelar.Text = "CANCELAR";
            btnEditar.Text = "EDITAR SELECCIONADA";
            btnEliminar.Text = "ELIMINAR";
            btnCerrar.Text = "CERRAR";
        }

        #endregion

        #region VALIDACION DE ENTRADA
        private void txtNombreActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilitarios.Validaciones.ForzarMayusculas(e);
            Utilitarios.Validaciones.SoloLetras(e);
        }

        private void txtCostoDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void txtHorario_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloAlfanumericos(e);
            Validaciones.ForzarMayusculas(e);
        }



        #endregion

        #region EVENTOS FORMULARIO
        private void FrmGestionActividades_Load(object sender, EventArgs e)
        {

            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));

            ConfigurarDataGridView();

            CargarActividades();
        }

        #endregion


        #region METODOS/LOGICA DE DATOS

        private void ConfigurarDataGridView()
        {
            dgvActividades.AutoGenerateColumns = true;
            dgvActividades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvActividades.ScrollBars = ScrollBars.Horizontal;
            //Elimina la primera columna que tiene la flecha
            dgvActividades.RowHeadersVisible = false;

        }

        private void CargarActividades()
        {
            try
            {
                var lista = _actividadBLL.ObtenerTodas();
                dgvActividades.DataSource = lista;

                dgvActividades.Columns["IdActividad"].Visible = false;


                // Renombrar encabezados
                // dgvActividades.Columns["Id_Actividad"].HeaderText = "ID";
                dgvActividades.Columns["Nombre"].HeaderText = "Nombre";
                dgvActividades.Columns["Descripcion"].HeaderText = "Descripcion/Horario";
                dgvActividades.Columns["Costo"].HeaderText = "Costo Diario";
            }
            catch (Exception ex)
            {
                // [REFACTORIZADO] Usar Prompt.MostrarError y constante para el mensaje y título.
                Prompt.MostrarError(string.Format(MensajesUI.ACTIVIDAD_MSG_ERROR_CARGA_ACTIVIDADES, ex.Message), MensajesUI.TITULO_ERROR_SISTEMA);

            }

        }

        private void LimpiarCampos()
        {
            txtNombreActividad.Clear();
            txtHorario.Clear();
            txtCostoDiario.Clear();
            txtNombreActividad.Focus();
            idActividadSeleccionada = 0;
            modoEdicion = false;
            btnGuardar.Text = "GUARDAR";
        }

        #endregion

        #region BOTONES DE ACCION

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNombreActividad.Text) ||
                string.IsNullOrWhiteSpace(txtHorario.Text) ||
                string.IsNullOrWhiteSpace(txtCostoDiario.Text))
            {
                // [REFACTORIZADO] Usar Prompt.MostrarAdvertencia y constante.
                Prompt.MostrarAlerta(MensajesUI.ACTIVIDAD_MSG_CAMPOS_REQUERIDOS, MensajesUI.TITULO_ADVERTENCIA_CAMPOS);
                return;
            }

            if (!decimal.TryParse(txtCostoDiario.Text, out decimal costo))
            {
                // [REFACTORIZADO] Usar Prompt.MostrarError y constante.
                Prompt.MostrarError(MensajesUI.ACTIVIDAD_MSG_COSTO_NUMERICO_INVALIDO, MensajesUI.TITULO_ERROR_FORMATO);
                return;
            }

            Actividad actividad = new Actividad
            {
                IdActividad = idActividadSeleccionada,
                Nombre = txtNombreActividad.Text.Trim(),
                Descripcion = txtHorario.Text.Trim(),
                Costo = costo
            };

            try
            {
                if (modoEdicion)
                {
                    _actividadBLL.Actualizar(actividad);
                    Prompt.Alerta("Actividad actualizada correctamente.", "Exito", Prompt.IconType.Ok);
                    modoEdicion = false;
                    idActividadSeleccionada = 0;
                    btnGuardar.Text = "GUARDAR";
                    txtNombreActividad.Focus();
                }
                else
                {
                    _actividadBLL.Insertar(actividad);
                    // [REFACTORIZADO] Usar Prompt.MostrarExito y constante.
                    Prompt.MostrarExito(MensajesUI.ACTIVIDAD_MSG_REGISTRO_EXITO, MensajesUI.TITULO_EXITO);

                }

                CargarActividades();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // [REFACTORIZADO] Usar Prompt.MostrarError y constante con formato.
                Prompt.MostrarError(string.Format(MensajesUI.ACTIVIDAD_MSG_REGISTRO_ERROR, ex.Message), MensajesUI.TITULO_ERROR_BLL);

            }

        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvActividades.CurrentRow == null)
            {
                // [REFACTORIZADO] Reemplazar MessageBox por Prompt.MostrarInformacion y constante.
                Prompt.MostrarInformacion(MensajesUI.ACTIVIDAD_MSG_SELECCIONAR_EDITAR, MensajesUI.TITULO_INFORMACION);
                return;
            }

            try
            {
                Actividad actividadSeleccionada = dgvActividades.CurrentRow.DataBoundItem as Actividad;

                if (actividadSeleccionada == null)
                {
                    // [REFACTORIZADO] Reemplazar MessageBox por Prompt.MostrarError y constante.
                    Prompt.MostrarError(MensajesUI.ACTIVIDAD_MSG_ERROR_OBTENER, MensajesUI.TITULO_ERROR_SISTEMA);
                    return;
                }

                idActividadSeleccionada = actividadSeleccionada.IdActividad;
                txtNombreActividad.Text = actividadSeleccionada.Nombre;
                txtHorario.Text = actividadSeleccionada.Descripcion;
                txtCostoDiario.Text = actividadSeleccionada.Costo.ToString();

                modoEdicion = true;
                btnGuardar.Text = "ACTUALIZAR";
            }
            catch (Exception ex)
            {
                // [REFACTORIZADO] Reemplazar MessageBox por Prompt.MostrarError y constante con formato.
                Prompt.MostrarError(string.Format(MensajesUI.ACTIVIDAD_MSG_ERROR_SELECCION, ex.Message), MensajesUI.TITULO_ERROR_CRITICO);
            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvActividades.SelectedRows.Count == 0)
            {
                // [REFACTORIZADO] Reemplazar MessageBox por Prompt.MostrarInformacion y constante.
                Prompt.MostrarInformacion(MensajesUI.ACTIVIDAD_MSG_SELECCIONAR_ELIMINAR, MensajesUI.TITULO_INFORMACION);
                return;
            }

            var actividadSeleccionada = dgvActividades.SelectedRows[0].DataBoundItem as Actividad;

            if (actividadSeleccionada != null)
            {
                // [REFACTORIZADO] Reemplazar MessageBox por Prompt.MostrarDialogoSiNo y constante con formato.
                DialogResult confirmar = Prompt.MostrarDialogoSiNo(
                    string.Format(MensajesUI.ACTIVIDAD_MSG_PREGUNTAR_ELIMINAR, actividadSeleccionada.Nombre),
                    MensajesUI.TITULO_CONFIRMAR_ACCION);
                if (confirmar == DialogResult.Yes)
                {
                    _actividadBLL.Eliminar(actividadSeleccionada.IdActividad);
                    CargarActividades();
                }

            }

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion


    
    }
}
