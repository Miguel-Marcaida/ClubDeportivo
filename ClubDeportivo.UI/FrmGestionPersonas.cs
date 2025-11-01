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
            gbFiltros.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblBuscar.ForeColor = EstilosGlobales.ColorTextoClaro;
        }

        private void EstablecerGeometriaYTextos()
        {
            // --- CONFIGURACIÓN DEL PANEL BASE Y EL FORMULARIO ---
            pnlBase.Size = new Size(1000, 650);
            pnlBase.BackColor = EstilosGlobales.ColorFondo;

            // --- TÍTULO ---
            lblTitulo.Text = "GESTIÓN DE PERSONAS (SOCIO Y NO SOCIO)";

            // --- GRUPOBOX DE FILTROS ---
            gbFiltros.Text = "Búsqueda por DNI, Nombre o Carnet";
            gbFiltros.Location = new Point(20, 70);
            gbFiltros.Size = new Size(960, 100);

            // Controles de Búsqueda
            lblBuscar.Location = new Point(15, 30);
            lblBuscar.Text = "DNI / Nombre / Carnet:";
            txtBuscar.Location = new Point(15, 50);
            txtBuscar.Size = new Size(400, 25);

            btnBuscar.Location = new Point(430, 48);
            btnBuscar.Text = "BUSCAR";
            btnBuscar.Size = new Size(150, 30);

            // --- LISTADO (DataGridView) ---
            dgvPersonas.Location = new Point(20, 190);
            dgvPersonas.Size = new Size(960, 350);

            // --- BOTONES DE ACCIÓN ---
            btnEditar.Location = new Point(20, 580);
            btnEditar.Text = "EDITAR SELECCIONADO";
            btnEditar.Size = new Size(230, 50);

            btnBaja.Location = new Point(260, 580);
            btnBaja.Text = "DAR DE BAJA (BAJA LÓGICA)";
            btnBaja.Size = new Size(230, 50);

            btnImprimirReporte.Location = new Point(500, 580);
            btnImprimirReporte.Text = "IMPRIMIR LISTADO (PDF)";
            btnImprimirReporte.Size = new Size(230, 50);

            btnCancelar.Location = new Point(740, 580);
            btnCancelar.Text = "CERRAR";
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
                MessageBox.Show("Error al cargar la lista de personas: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvPersonas.Columns["Dni"].HeaderText = "DNI";
                dgvPersonas.Columns["NombreCompleto"].HeaderText = "NOMBRE Y APELLIDO";
                dgvPersonas.Columns["TipoPersona"].HeaderText = "TIPO";
                dgvPersonas.Columns["NumeroCarnet"].HeaderText = "CARNET";
                dgvPersonas.Columns["EstadoMembresia"].HeaderText = "ESTADO CUOTA";

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
                // Llamaremos a un método de re-configuración.
                ConfigurarColumnasDataGrid();

                MessageBox.Show("Mostrando todos los registros vigentes.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // sigue siendo la misma, solo se muestran menos filas, y la configuración se mantiene.
            }
            else
            {
                dgvPersonas.DataSource = null;
                MessageBox.Show($"No se encontraron personas con el criterio '{filtro}'.", "Búsqueda sin Resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Debe seleccionar una persona de la lista para editar.", "Advertencia",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Obtener el IdPersona de la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvPersonas.SelectedRows[0];
                int idPersonaAEditar = Convert.ToInt32(filaSeleccionada.Cells["IdPersona"].Value);
                string nombre = filaSeleccionada.Cells["NombreCompleto"].Value.ToString();

                // 3. LLAMADA CRÍTICA: Abrir el formulario de edición en modo Modal
                // Usamos using y ShowDialog para garantizar la sincronización y recarga.
                using (FrmActualizarPersona frmEdicion = new FrmActualizarPersona(idPersonaAEditar, _anchoMenuLateral))
                {
                    if (frmEdicion.ShowDialog() == DialogResult.OK)
                    {
                        // Si la edición fue exitosa, recargamos la grilla para reflejar los cambios.
                        LlenarDataGrid();
                        MessageBox.Show($"Persona '{nombre}' (ID: {idPersonaAEditar}) actualizada con éxito.",
                                        "Edición Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Si el DialogResult es CANCEL o CERRAR, no hacemos nada ni mostramos mensaje.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la edición: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Debe seleccionar una fila de la lista para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // --- PRIMER MESSAGEBOX ---
                DialogResult result = MessageBox.Show($"¿Está seguro que desea dar de BAJA LÓGICA a la persona '{nombre}'? Esta acción la hace INVISIBLE en el listado, pero MANTIENE su historial de pagos para auditoría.",
                                                     "Confirmar Baja Lógica (Soft Delete)", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    bool bajaExitosa = oPersonaBLL.DarDeBajaPersona(idPersona);

                    if (bajaExitosa)
                    {
                        // CRÍTICO: Recargar la grilla para que la persona dada de baja desaparezca
                        LlenarDataGrid();
                        MessageBox.Show($"Persona '{nombre}' dada de baja LÓGICA con éxito.", "Baja Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"No se pudo dar de baja a la persona '{nombre}'. Verifique la conexión.", "Error de Baja", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error CRÍTICO al procesar la baja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // **NOTA:** Nos basamos en la lista, NO en las filas de la grilla (dgvPersonas.Rows), 
            // porque es la fuente de verdad.
            if (_listaPersonas == null || _listaPersonas.Count == 0)
            {
                MessageBox.Show("No hay registros de personas para imprimir en el listado. Por favor, cargue los datos.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rutaPdfGenerado = string.Empty;

            try
            {
                // 2. Generar el Listado PDF
                // La llamada ahora pasa la lista de objetos DTO directamente
                rutaPdfGenerado = Utilitarios.PdfGenerator.GenerarListadoGeneral(_listaPersonas);

                // 3. Notificar al usuario y preguntar si desea abrir el PDF
                DialogResult dialogResult = MessageBox.Show(
                    $"Listado generado con éxito en:\n{rutaPdfGenerado}\n\n¿Desea abrir el archivo ahora?",
                    "Impresión de Listado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    // Abrir el archivo con el programa predeterminado (navegador/lector PDF)
                    // Se usa Process.StartInfo para mayor compatibilidad en entornos modernos.
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaPdfGenerado) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier error que provenga del Generador de PDF (como el error de fuentes si vuelve)
                MessageBox.Show("Error al intentar generar el listado PDF: " + ex.Message,
                                "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        
        #endregion
    }


}






