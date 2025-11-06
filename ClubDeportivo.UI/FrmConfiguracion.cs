using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.Entidades;
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
    public partial class FrmConfiguracion : Form
    {

        #region PROPIEDADES Y CAMPOS PRIVADOS

        private readonly int _anchoMenuLateral;
        private readonly ConfiguracionBLL oConfiguracionBLL = new ConfiguracionBLL(); // ⬅️ Inyección BLL
        private int IdConfigSeleccionado = 0; // ⬅️ Campo para guardar el ID de la fila seleccionada

        #endregion

        #region CONSTRUCTOR

        public FrmConfiguracion(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;
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
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;
            lblTitulo.ForeColor = EstilosGlobales.ColorAcento;

            EstilosGlobales.AplicarEstiloCampo(txtClave);
            EstilosGlobales.AplicarEstiloCampo(txtValor);
            EstilosGlobales.AplicarEstiloCampo(txtDescripcion);

            // Botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnNuevo);
            EstilosGlobales.AplicarEstiloBotonAccion(btnGuardar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnEliminar);

            // Colores especiales
            btnEliminar.BackColor = EstilosGlobales.ColorBoton;
            btnEliminar.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorError;

            EstilosGlobales.AplicarEstiloDataGridView(dgvConfiguraciones);

            // Estilos del GroupBox y sus etiquetas
            gbCampos.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblClave.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblValor.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblDescripcion.ForeColor = EstilosGlobales.ColorTextoClaro;

        }


        private void EstablecerGeometriaYTextos()
        {
            // --- DIMENSIONES BASE ---
            int anchoPanel = 850;
            int altoPanel = 600;
            int margen = 20;
            int separador = 10;
            int altoControl = 30;
            int anchoBtn = 100;
            int altoTitulo = 40;
            int altoLabel = 15; // Alto estándar para Labels

            // Altura fija y grande para el TextBox de la Descripción
            int altoDescripcion = 120;

            pnlBase.Size = new Size(anchoPanel, altoPanel);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            // --- TÍTULO ---
            lblTitulo.Text = "CONFIGURACIONES GLOBALES";
            lblTitulo.Location = new Point(margen, margen);
            lblTitulo.Size = new Size(anchoPanel - 2 * margen, altoTitulo);

            // --- CÁLCULO DE ALTURAS ---
            // Altura total fija del GroupBox para acomodar todos los controles en dos columnas y botones abajo.
            int altoControles = (altoLabel + altoControl); // Altura de Clave/Valor
            int altoDescripcionCompleta = altoLabel + altoDescripcion;
            int altoBotones = altoControl;

            int altoGroupBoxFijo = altoControles + altoDescripcionCompleta + altoBotones + 5 * separador;

            // --- GRILLA (Ocupa la parte superior) ---
            int yGrilla = lblTitulo.Bottom + separador;
            int altoGrilla = altoPanel - yGrilla - margen - altoGroupBoxFijo - separador;

            dgvConfiguraciones.Location = new Point(margen, yGrilla);
            dgvConfiguraciones.Size = new Size(anchoPanel - 2 * margen, altoGrilla);
            dgvConfiguraciones.ReadOnly = true;

            // --- GRUPOBOX (Panel de Edición - Ocupa el espacio fijo restante) ---
            int yGroupBox = dgvConfiguraciones.Bottom + separador;

            gbCampos.Text = "Detalles de la Configuración:";
            gbCampos.Location = new Point(margen, yGroupBox);
            gbCampos.Size = new Size(anchoPanel - 2 * margen, altoGroupBoxFijo);

            // --- CONTROLES DENTRO DEL GROUPBOX (Clave y Valor en dos columnas) ---
            int xStart = separador;
            int yControl = 2 * separador;
            int anchoInputTotal = gbCampos.Width - 2 * separador;

            // Espacio horizontal para Clave y Valor (50% del ancho cada uno)
            int anchoClaveValor = (anchoInputTotal / 2) - separador;

            // 1. Clave (Columna Izquierda)
            lblClave.Text = "Clave:";
            lblClave.Location = new Point(xStart, yControl);
            lblClave.Size = new Size(anchoClaveValor, altoLabel);

            txtClave.Location = new Point(xStart, lblClave.Bottom);
            txtClave.Size = new Size(anchoClaveValor, altoControl);
            txtClave.MaxLength = 100;

            // 2. Valor (Columna Derecha)
            int xValor = xStart + anchoClaveValor + separador;
            lblValor.Text = "Valor:";
            lblValor.Location = new Point(xValor, yControl);
            lblValor.Size = new Size(anchoClaveValor, altoLabel);

            txtValor.Location = new Point(xValor, lblValor.Bottom);
            txtValor.Size = new Size(anchoClaveValor, altoControl);
            txtValor.MaxLength = 255;

            // Pasamos a la siguiente fila para la Descripción
            yControl += altoControl + altoLabel + separador;

            // 3. Descripción (Ocupa todo el ancho)
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(xStart, yControl);
            lblDescripcion.Size = new Size(anchoInputTotal, altoLabel);

            txtDescripcion.Location = new Point(xStart, lblDescripcion.Bottom);
            txtDescripcion.Size = new Size(anchoInputTotal, altoDescripcion); // ⬅️ Altura grande: 120px
            txtDescripcion.MaxLength = 255;
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            yControl = txtDescripcion.Bottom + separador;

            // --- BOTONES DE ACCIÓN (DENTRO DEL GroupBox, al fondo) ---
            int yBotones = yControl;

            // 1. Eliminar (Derecha del GroupBox)
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.Size = new Size(anchoBtn, altoControl);
            btnEliminar.Location = new Point(gbCampos.Width - separador - anchoBtn, yBotones);

            // 2. Guardar (Medio)
            btnGuardar.Text = "GUARDAR";
            btnGuardar.Size = new Size(anchoBtn, altoControl);
            btnGuardar.Location = new Point(btnEliminar.Left - separador - anchoBtn, yBotones);

            // 3. Nuevo (Izquierda)
            btnNuevo.Text = "NUEVO";
            btnNuevo.Size = new Size(anchoBtn, altoControl);
            btnNuevo.Location = new Point(btnGuardar.Left - separador - anchoBtn, yBotones);
        }



        #endregion

        #region EVENTOS FORMULARIO
        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));
            CargarGrilla();

        }

        private void dgvConfiguraciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvConfiguraciones.Rows[e.RowIndex];

                // 1. Cargar el ID (Clave para UPDATE/DELETE)
                IdConfigSeleccionado = Convert.ToInt32(fila.Cells["IdConfig"].Value);

                // 2. Cargar los campos
                txtClave.Text = fila.Cells["Clave"].Value.ToString();
                txtValor.Text = fila.Cells["Valor"].Value.ToString();

                // 3. Manejar descripción (puede ser null/DBNull)
                object descripcionValue = fila.Cells["Descripcion"].Value;
                txtDescripcion.Text = (descripcionValue == null || descripcionValue == DBNull.Value)
                                      ? string.Empty
                                      : descripcionValue.ToString();

                btnEliminar.Enabled = true; // Habilitamos Eliminar si hay fila seleccionada
            }


        }


        #endregion

        #region VALIDACION DE ENTRADAS

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.ForzarMayusculas(e);
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloLetras(e);
            Validaciones.ForzarMayusculas(e);
        }

        #endregion

        #region METODOS/ LOGICAD DDE DATOS

        private void limpiarFormulario()
        {
            txtClave.Text = "";
            txtValor.Text = "";
            txtDescripcion.Text = "";
            IdConfigSeleccionado = 0; // Reiniciar ID
            btnEliminar.Enabled = false; // Deshabilitar Eliminar
            txtClave.Focus();
        }

        private void CargarGrilla()
        {
            try
            {
                // El DataSource usa el método ListarTodos de tu BLL
                dgvConfiguraciones.DataSource = oConfiguracionBLL.ListarTodos();

                ConfigurarGrilla();
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                // [MODIFICADO] Reemplazado por Prompt.MostrarError y constante CONFIG_MSG_ERROR_CARGA
                Prompt.MostrarError(string.Format(MensajesUI.CONFIG_MSG_ERROR_CARGA, ex.Message), MensajesUI.TITULO_ERROR_SISTEMA);
            }
        }

        private void ConfigurarGrilla()
        {
            // Limpieza y reconfiguración de columnas
            dgvConfiguraciones.Columns.Clear();

            // 1. Crear las columnas
            dgvConfiguraciones.Columns.Add("IdConfig", "ID");
            dgvConfiguraciones.Columns.Add("Clave", "CLAVE");
            dgvConfiguraciones.Columns.Add("Valor", "VALOR");
            dgvConfiguraciones.Columns.Add("Descripcion", "DESCRIPCIÓN");

            // 2. Mapear las propiedades de la Entidad ConfiguracionGlobal
            dgvConfiguraciones.Columns["IdConfig"].DataPropertyName = "IdConfig";
            dgvConfiguraciones.Columns["Clave"].DataPropertyName = "Clave";
            dgvConfiguraciones.Columns["Valor"].DataPropertyName = "Valor";
            dgvConfiguraciones.Columns["Descripcion"].DataPropertyName = "Descripcion";

            // 3. Formato y visibilidad
            dgvConfiguraciones.Columns["IdConfig"].Visible = false; // Ocultamos el ID
            dgvConfiguraciones.Columns["Clave"].Width = 150;
            dgvConfiguraciones.Columns["Valor"].Width = 150;
            dgvConfiguraciones.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Ocupa el espacio restante
        }

        
        private ConfiguracionGlobal CrearEntidad()
        {
            return new ConfiguracionGlobal
            {
                IdConfig = IdConfigSeleccionado, // Si es 0, es INSERT; si > 0, es UPDATE
                Clave = txtClave.Text.Trim(),
                Valor = txtValor.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim()
            };
        }


        #endregion

        #region BOTONES DE ACCION

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //NUEVO
            limpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ConfiguracionGlobal config = CrearEntidad();
                // Determinamos el mensaje usando la constante parametrizada
                string tipoAccion = (config.IdConfig == 0) ? "insertada" : "modificada";

                // Llama al método de la BLL para INSERT o UPDATE
                if (oConfiguracionBLL.GuardarOModificar(config))
                {
                    // [MODIFICADO] Uso de constante CONFIG_MSG_REGISTRO_EXITO con formato
                    Prompt.MostrarExito(string.Format(MensajesUI.CONFIG_MSG_REGISTRO_EXITO, config.Clave, tipoAccion), MensajesUI.TITULO_EXITO);
                    CargarGrilla(); // Recargar datos para ver el cambio
                }
                else
                {
                    // [MODIFICADO] Uso de constante CONFIG_MSG_NO_CAMBIOS
                    Prompt.MostrarAlerta(MensajesUI.CONFIG_MSG_NO_CAMBIOS, MensajesUI.TITULO_ADVERTENCIA);
                }
            }
            catch (Exception ex)
            {
                // [MODIFICADO] Uso de constante CONFIG_MSG_ERROR_GUARDAR
                Prompt.MostrarError(string.Format(MensajesUI.CONFIG_MSG_ERROR_GUARDAR, ex.Message), MensajesUI.TITULO_ERROR_BLL);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //ELIMINA
            if (IdConfigSeleccionado == 0)
            {
                // [MODIFICADO] Uso de constante CONFIG_MSG_SELECCIONAR_ELIMINAR
                Prompt.MostrarAlerta(MensajesUI.CONFIG_MSG_SELECCIONAR_ELIMINAR, MensajesUI.TITULO_ADVERTENCIA);
                return;
            }

            // [MODIFICADO] Uso de constante CONFIG_PREGUNTA_ELIMINAR
            if (Prompt.MostrarDialogoConfirmacion(MensajesUI.CONFIG_PREGUNTA_ELIMINAR, MensajesUI.TITULO_CONFIRMAR_ACCION) )
            {
                try
                {
                    if (oConfiguracionBLL.Eliminar(IdConfigSeleccionado))
                    {
                        // [MODIFICADO] Uso de constante CONFIG_MSG_ELIMINACION_EXITO
                        Prompt.MostrarExito(MensajesUI.CONFIG_MSG_ELIMINACION_EXITO, MensajesUI.TITULO_EXITO);
                        CargarGrilla();
                    }
                    else
                    {
                        // [MODIFICADO] Uso de constante CONFIG_MSG_NO_ELIMINADO
                        Prompt.MostrarAlerta(MensajesUI.CONFIG_MSG_NO_ELIMINADO, MensajesUI.TITULO_ADVERTENCIA);
                    }
                }
                catch (Exception ex)
                {
                    // [MODIFICADO] Uso de constante CONFIG_MSG_ERROR_ELIMINAR
                    Prompt.MostrarError(string.Format(MensajesUI.CONFIG_MSG_ERROR_ELIMINAR, ex.Message), MensajesUI.TITULO_ERROR_BLL);
                }
            }
        }

        #endregion

      
    }
}
