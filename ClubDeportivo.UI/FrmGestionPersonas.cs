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
    public partial class FrmGestionPersonas : Form
    {
        #region PROPIEDADES Y CAMPOS PRIVADOS
        
        
        private readonly int _anchoMenuLateral;
        private readonly PersonaBLL oPersonaBLL = new PersonaBLL();

        //NUEVO CAMPO: Guardaremos la lista activa de DTOs
        private List<PersonaListadoDTO> _listaPersonas;

        #endregion


        #region CONSTRUCTOR

        public FrmGestionPersonas(int anchoMenuLateral)
        {
            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;

            ConfigurarFormulario();

            // Suscripciones Críticas
            this.Load += FrmGestionPersonas_Load;
            this.Resize += (s, e) => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral);
            // Los eventos de click de los botones deben estar suscritos en el Designer, 
            // no es necesario aquí a menos que el Designer no lo haga.
        }

        #endregion

        
        #region CONFIGURACION UI

        private void ConfigurarFormulario()
        {
            EstablecerGeometriaYTextos();
            ConfigurarEstilos();
            LlenarDataGrid(); // Llama al método de carga de datos real
        }

        private void ConfigurarEstilos()
        {
            EstilosGlobales.AplicarFormatoBase(this);
            lblTitulo.Font = EstilosGlobales.EstiloTitulo;
            lblTitulo.ForeColor = EstilosGlobales.ColorAcento;

            EstilosGlobales.AplicarEstiloCampo(txtBuscar);

            // Botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnBuscar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnEditar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnBaja);
            EstilosGlobales.AplicarEstiloBotonAccion(btnCancelar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnImprimirReporte);

            // Colores especiales
            btnBaja.BackColor = EstilosGlobales.ColorError;
            btnBaja.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorError;
            btnImprimirReporte.BackColor = EstilosGlobales.ColorAdvertencia;

            EstilosGlobales.AplicarEstiloDataGridView(dgvPersonas);

            // Estilos del GroupBox y sus etiquetas
            EstilosGlobales.AplicarEstiloGroupBox(gbFiltros); // ✅ Se aplica estilo estandarizado
            lblBuscar.ForeColor = EstilosGlobales.ColorTextoClaro;
        }

        private void EstablecerGeometriaYTextos()
        {
            // --- CONFIGURACIÓN DEL PANEL BASE Y EL FORMULARIO ---
            pnlBase.Size = new Size(1000, 650);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            // --- TÍTULO ---
            lblTitulo.Text = MensajesUI.PERSONAS_TITULO_FORM; // ✅ REFACTORIZACIÓN: Uso de constante
            lblTitulo.Location = new Point(20, 25);

            // --- GRUPOBOX DE FILTROS ---
            gbFiltros.Text = MensajesUI.PERSONAS_GB_BUSQUEDA; // ✅ REFACTORIZACIÓN: Uso de constante
            gbFiltros.Location = new Point(20, 70);
            gbFiltros.Size = new Size(960, 100);

            // Controles de Búsqueda
            lblBuscar.Location = new Point(15, 30);
            lblBuscar.Text = MensajesUI.PERSONAS_LBL_BUSCAR; // ✅ REFACTORIZACIÓN: Uso de constante
            txtBuscar.Location = new Point(15, 50);
            txtBuscar.Size = new Size(400, 25);

            btnBuscar.Location = new Point(430, 48);
            btnBuscar.Text = MensajesUI.PERSONAS_BTN_BUSCAR; // ✅ REFACTORIZACIÓN: Uso de constante
            btnBuscar.Size = new Size(150, 30);

            // --- LISTADO (DataGridView) ---
            dgvPersonas.Location = new Point(20, 190);
            dgvPersonas.Size = new Size(960, 350);

            // --- BOTONES DE ACCIÓN ---
            btnEditar.Location = new Point(20, 580);
            btnEditar.Text = MensajesUI.PERSONAS_BTN_EDITAR; // ✅ REFACTORIZACIÓN: Uso de constante
            btnEditar.Size = new Size(230, 50);

            btnBaja.Location = new Point(260, 580);
            btnBaja.Text = MensajesUI.PERSONAS_BTN_BAJA; // ✅ REFACTORIZACIÓN: Uso de constante
            btnBaja.Size = new Size(230, 50);

            btnImprimirReporte.Location = new Point(500, 580);
            btnImprimirReporte.Text = MensajesUI.PERSONAS_BTN_IMPRIMIR; // ✅ REFACTORIZACIÓN: Uso de constante
            btnImprimirReporte.Size = new Size(230, 50);

            btnCancelar.Location = new Point(740, 580);
            btnCancelar.Text = MensajesUI.PERSONAS_BTN_CERRAR; // ✅ REFACTORIZACIÓN: Uso de constante
            btnCancelar.Size = new Size(240, 50);
            btnCancelar.Click += (sender, e) => this.Close();
        }

        #endregion

        #region EVENTOS DE FORMULARIO

        private void FrmGestionPersonas_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));
        }

        #endregion

        #region VALIDACION DE ENTRADA
        
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilitarios.Validaciones.SoloAlfanumericos(e);
            Utilitarios.Validaciones.ForzarMayusculas(e); 
        }
        
        
        #endregion


        #region LOGICA DE DATOS

        private void LlenarDataGrid()
        {
            try
            {
                _listaPersonas = oPersonaBLL.ObtenerListadoGeneral();

                dgvPersonas.DataSource = _listaPersonas;

                // 3. Configuración de las columnas y estilos
                ConfigurarColumnasDataGrid(); // Llama al nuevo método
            }
            catch (Exception ex)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.PERSONAS_MSG_ERROR_CARGA_CRITICO + ex.Message, MensajesUI.TITULO_ERROR);
                dgvPersonas.DataSource = null;
            }
        }

        
        private void ConfigurarColumnasDataGrid()
        {
            if (dgvPersonas.Columns.Count > 0)
            {
                // Ocultar las columnas internas
                dgvPersonas.Columns["IdPersona"].Visible = false;
                dgvPersonas.Columns["Nombre"].Visible = false;
                dgvPersonas.Columns["Apellido"].Visible = false;

                // Configurar encabezados
                dgvPersonas.Columns["Dni"].HeaderText = MensajesUI.PERSONAS_COL_DNI;
                dgvPersonas.Columns["NombreCompleto"].HeaderText = MensajesUI.PERSONAS_COL_NOMBRE_COMPLETO; // ✅ Uso de constante
                dgvPersonas.Columns["TipoPersona"].HeaderText = MensajesUI.PERSONAS_COL_TIPO; // ✅ Uso de constante
                dgvPersonas.Columns["NumeroCarnet"].HeaderText = MensajesUI.PERSONAS_COL_CARNET; // ✅ Uso de constante
                dgvPersonas.Columns["EstadoMembresia"].HeaderText = MensajesUI.PERSONAS_COL_ESTADO_CUOTA; // ✅ Uso de constante

                // Reordenar columnas para mejor visualización
                dgvPersonas.Columns["NombreCompleto"].DisplayIndex = 0;
                dgvPersonas.Columns["Dni"].DisplayIndex = 1;
                dgvPersonas.Columns["TipoPersona"].DisplayIndex = 2;
                dgvPersonas.Columns["NumeroCarnet"].DisplayIndex = 3;
                dgvPersonas.Columns["EstadoMembresia"].DisplayIndex = 4;
                dgvPersonas.ClearSelection();
            }
        }

        #endregion

        #region BOTONES DE ACCIONES
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToUpper();

            // CRÍTICO: Si la lista base no está cargada, la cargamos.
            if (_listaPersonas == null)
            {
                LlenarDataGrid();
                if (_listaPersonas == null) return; // Si la carga falló, salimos.
            }

            // CASO 1: FILTRO VACÍO
            if (string.IsNullOrEmpty(filtro))
            {
                // 1. Asignamos la lista COMPLETA al DataSource (esta lista ya tiene las columnas configuradas/ocultas).
                dgvPersonas.DataSource = _listaPersonas;

                // 2. Necesitamos forzar la reconfiguración de columnas por si se desordenó el Binding.
                ConfigurarColumnasDataGrid();

                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarInformacion
                Prompt.MostrarInformacion(MensajesUI.PERSONAS_MSG_BUSQUEDA_COMPLETA);
                return;
            }

            // CASO 2: FILTRO ACTIVO
            // Filtrado local en la lista de DTOs ya cargada
            List<PersonaListadoDTO> resultados = _listaPersonas
                .Where(p =>
                    (p.Dni?.ToUpper().Contains(filtro) ?? false) ||
                    (p.NombreCompleto?.ToUpper().Contains(filtro) ?? false) ||
                    (p.NumeroCarnet.HasValue && p.NumeroCarnet.Value.ToString().Contains(filtro))
                )
                .ToList();

            if (resultados.Count > 0)
            {
                dgvPersonas.DataSource = resultados;
                // Aquí NO llamamos a ConfigurarColumnasDataGrid() porque el Binding Source (la lista)
                
            }
            else
            {
                dgvPersonas.DataSource = null;
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(string.Format(MensajesUI.PERSONAS_MSG_BUSQUEDA_SIN_RESULTADOS + " (Criterio: '{0}')", filtro));
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Bloqueamos el botón mientras se procesa la acción
            btnEditar.Enabled = false;

            try
            {
                // 1. Validación de selección de fila
                if (dgvPersonas.SelectedRows.Count == 0)
                {
                    // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                    Prompt.MostrarAlerta(MensajesUI.PERSONAS_MSG_SELECCION_OBLIGATORIA);
                    return;
                }

                // 2. Obtener el IdPersona de la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvPersonas.SelectedRows[0];
                int idPersonaAEditar = Convert.ToInt32(filaSeleccionada.Cells["IdPersona"].Value);
                string nombre = filaSeleccionada.Cells["NombreCompleto"].Value.ToString();

                // 3. LLAMADA CRÍTICA: Abrir el formulario de edición en modo Modal
                using (FrmActualizarPersona frmEdicion = new FrmActualizarPersona(idPersonaAEditar, _anchoMenuLateral))
                {
                    if (frmEdicion.ShowDialog() == DialogResult.OK)
                    {
                        // Si la edición fue exitosa, recargamos la grilla para reflejar los cambios.
                        LlenarDataGrid();

                        // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarExito, usando formato
                        string mensajeExito = string.Format(MensajesUI.PERSONAS_MSG_EDICION_EXITOSA, nombre, idPersonaAEditar);
                        Prompt.MostrarExito(mensajeExito);
                    }
                    // Si el DialogResult es CANCEL o CERRAR, no hacemos nada ni mostramos mensaje.
                }
            }
            catch (Exception ex)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError, usando formato
                Prompt.MostrarError(MensajesUI.PERSONAS_MSG_ERROR_EDICION + ex.Message, MensajesUI.TITULO_ERROR);
            }
            finally
            {
                // CRÍTICO: Re-habilitar el botón
                btnEditar.Enabled = true;
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {

            if (dgvPersonas.SelectedRows.Count == 0)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.PERSONAS_MSG_SELECCION_OBLIGATORIA);
                return;
            }

            // SOLUCIÓN FINAL: Deshabilitar el botón para prevenir el "Click Fantasma" del ENTER.
            btnBaja.Enabled = false;

            try
            {
                DataGridViewRow filaSeleccionada = dgvPersonas.SelectedRows[0];
                PersonaListadoDTO personaSeleccionada = (PersonaListadoDTO)filaSeleccionada.DataBoundItem;

                int idPersona = personaSeleccionada.IdPersona;
                string nombre = personaSeleccionada.NombreCompleto;

                // --- PRIMER MESSAGEBOX (Confirmación) ---
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarDialogoConfirmacion
                string msgConfirmacion = string.Format(MensajesUI.PERSONAS_MSG_CONFIRMAR_BAJA_LOGICA, nombre);

                if (Prompt.MostrarDialogoConfirmacion(msgConfirmacion))
                {
                    bool bajaExitosa = oPersonaBLL.DarDeBajaPersona(idPersona);

                    if (bajaExitosa)
                    {
                        // CRÍTICO: Recargar la grilla para que la persona dada de baja desaparezca
                        LlenarDataGrid();

                        // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarExito
                        string msgExito = string.Format(MensajesUI.PERSONAS_MSG_BAJA_EXITOSA, nombre);
                        Prompt.MostrarExito(msgExito);
                    }
                    else
                    {
                        // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                        Prompt.MostrarError(MensajesUI.PERSONAS_MSG_ERROR_BAJA_BLL, MensajesUI.TITULO_ERROR);
                    }
                }
            }
            catch (Exception ex)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.PERSONAS_MSG_ERROR_BAJA_CRITICO + ex.Message, MensajesUI.TITULO_ERROR);
            }
            finally
            {
                // CRÍTICO: Re-habilitar el botón SÓLO cuando todo el flujo terminó.
                btnBaja.Enabled = true;
            }
        }

        private void btnImprimirReporte_Click(object sender, EventArgs e)
        {

            // 1. Verificar si la fuente de datos (la lista) tiene elementos.
            if (_listaPersonas == null || _listaPersonas.Count == 0)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarAlerta
                Prompt.MostrarAlerta(MensajesUI.PERSONAS_MSG_IMPRESION_SIN_DATOS);
                return;
            }

            string rutaPdfGenerado = string.Empty;

            try
            {
                // 2. Generar el Listado PDF
                rutaPdfGenerado = Utilitarios.PdfGenerator.GenerarListadoGeneral(_listaPersonas);

                // 3. Notificar al usuario y preguntar si desea abrir el PDF
                // ✅ REFACTORIZACIÓN: Se usa Prompt.MostrarDialogoSiNo para manejar la apertura.
                string msgAbrir = string.Format(MensajesUI.PERSONAS_MSG_IMPRESION_PREGUNTAR_ABRIR, rutaPdfGenerado);

                if (Prompt.MostrarDialogoSiNo(msgAbrir, MensajesUI.TITULO_EXITO) == DialogResult.Yes)
                {
                    // Abrir el archivo con el programa predeterminado (navegador/lector PDF)
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaPdfGenerado) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                // ✅ REFACTORIZACIÓN: Sustitución de MessageBox por Prompt.MostrarError
                Prompt.MostrarError(MensajesUI.PERSONAS_MSG_ERROR_GENERAR_PDF + ex.Message, MensajesUI.TITULO_ERROR);
            }


        }

        
        #endregion

    }


}






