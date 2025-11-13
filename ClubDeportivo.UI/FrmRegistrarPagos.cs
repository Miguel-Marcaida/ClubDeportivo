using ClubDeportivo.UI.BLL;
using ClubDeportivo.UI.DAL;
using ClubDeportivo.UI.Entidades;
using ClubDeportivo.UI.Utilitarios;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubDeportivo.UI
{
    public partial class FrmRegistrarPagos : Form
    {
        #region CAMPOS PRIVADOS Y OBJETOS DE NEGOCIO

        private readonly PersonaBLL oPersonaBLL = new PersonaBLL();
        private readonly ConfiguracionBLL oConfiguracionBLL = new ConfiguracionBLL();
        private readonly ActividadBLL oActividadBLL = new ActividadBLL();
        private readonly CuotaBLL oCuotaBLL = new CuotaBLL();             // <-- BLL de Cuotas
        private readonly RegistroAccesoBLL oRegistroAccesoBLL = new RegistroAccesoBLL(); // <-- BLL de Acceso
        private readonly int _anchoMenuLateral;
        private PersonaPagoDetalleDTO? _personaEncontrada;
        private int meses;

        #endregion

        #region CONSTRUCTOR

        public FrmRegistrarPagos(int anchoMenuLateral)
        {

            InitializeComponent();
            _anchoMenuLateral = anchoMenuLateral;
            ConfigurarFormulario();
            this.Load += FrmRegistrarPagos_Load;
        }

        #endregion

        #region CONFIGURACION UI


        private void ConfigurarFormulario()
        {
            EstablecerGeometriaYTextos(); // 1. Estructura y Posición
            ConfigurarEstilos();           // 2. Colores y Fuentes

        }
        private void ConfigurarEstilos()
        {
            EstilosGlobales.AplicarFormatoBase(this);
            this.BackColor = EstilosGlobales.ColorFondo;
            pnlBase.BackColor = EstilosGlobales.ColorFondo;
            pnlBusqueda.BackColor = EstilosGlobales.ColorAcento;

            // Configurar color de fondo y asegurar que el texto del GroupBox sea claro
            gbInfoPersona.BackColor = EstilosGlobales.ColorFondo;
            gbInfoPersona.ForeColor = EstilosGlobales.ColorTextoClaro;

            gbDatosPago.BackColor = EstilosGlobales.ColorFondo;
            gbDatosPago.ForeColor = EstilosGlobales.ColorTextoClaro;

            // Estilo del Título 
            lblTituloPagos.Font = EstilosGlobales.EstiloTitulo;
            lblTituloPagos.ForeColor = EstilosGlobales.ColorAcento;

            // Aplicar estilos a los campos de entrada
            EstilosGlobales.AplicarEstiloCampo(txtBuscarIdentificador);
            EstilosGlobales.AplicarEstiloCampo(txtMonto);
            EstilosGlobales.AplicarEstiloCampo(cmbConcepto);
            EstilosGlobales.AplicarEstiloCampo(cmbMedioPago);
            EstilosGlobales.AplicarEstiloCampo(cmbActividades); // ¡AÑADIDO!

            // Estilos de Etiquetas de Info (USANDO TODOS LOS LABELS IMPLÍCITOS)
            lblNombrePersona.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblDni.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblTipoSocio.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblFechaUltimoPago.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblFechaVencimiento.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblMesesAtraso.ForeColor = EstilosGlobales.ColorTextoClaro; // Se actualiza en btnBuscar_Click
            lblEstadoMembresia.ForeColor = EstilosGlobales.ColorTextoClaro; // Se actualiza en btnBuscar_Click

            lblBuscar.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblMonto.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblConcepto.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblFormaPago.ForeColor = EstilosGlobales.ColorTextoClaro;
            lblActividades.ForeColor = EstilosGlobales.ColorTextoClaro; // ¡AÑADIDO!

            // Estilo a los botones de acción
            EstilosGlobales.AplicarEstiloBotonAccion(btnBuscar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnRegistrarPago);
            EstilosGlobales.AplicarEstiloBotonAccion(btnLimpiar);
            EstilosGlobales.AplicarEstiloBotonAccion(btnReimprimirCarnet);
            EstilosGlobales.AplicarEstiloBotonAccion(btnCerrar);


            // Personalización del botón Registrar (color de Éxito)
            btnRegistrarPago.BackColor = EstilosGlobales.ColorExito;
            btnCerrar.BackColor = EstilosGlobales.ColorError;
            btnLimpiar.BackColor = EstilosGlobales.ColorAdvertencia;
            btnReimprimirCarnet.BackColor = EstilosGlobales.ColorAcento;


            // Lógica de inicio
            btnRegistrarPago.Enabled = false;
        }

        private void EstablecerGeometriaYTextos()
        {
            // ----------------------------------------------------------------------
            // --- 1. CONFIGURACIÓN BASE (pnlBase) ---
            // ----------------------------------------------------------------------
            int baseWidth = 800;
            int marginX = 25;
            pnlBase.Size = new Size(baseWidth, 650); // Altura ajustada

            // Título
            lblTituloPagos.Text = "REGISTRO DE PAGOS";
            lblTituloPagos.TextAlign = ContentAlignment.MiddleCenter;
            lblTituloPagos.Location = new Point(marginX, 20);
            lblTituloPagos.Size = new Size(baseWidth - (marginX * 2), 40);

            // ----------------------------------------------------------------------
            // --- 2. PANEL DE BÚSQUEDA (pnlBusqueda) ---
            // ----------------------------------------------------------------------
            pnlBusqueda.Location = new Point(marginX, 80);
            pnlBusqueda.Size = new Size(baseWidth - (marginX * 2), 45); // 750

            // Controles dentro de pnlBusqueda
            lblBuscar.Text = "DNI / Carnet:";
            lblBuscar.Location = new Point(0, 10);
            lblBuscar.Size = new Size(110, 20);

            txtBuscarIdentificador.Location = new Point(115, 5);
            txtBuscarIdentificador.Size = new Size(400, 35);

            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(535, 5);
            btnBuscar.Size = new Size(150, 35);


            // ----------------------------------------------------------------------
            // --- 3. GROUPBOX DE INFORMACIÓN DE LA PERSONA (gbInfoPersona) ---
            // ----------------------------------------------------------------------
            int infoTopY = 140;
            int infoHeight = 160;
            int labelWidth = 120;
            int valueWidth = 220;
            int spacingY = 28;
            int internalMarginX = 15;
            int col2X = 380; // Columna de la derecha

            gbInfoPersona.Text = "Datos de la Persona y Estado de Membresía";
            gbInfoPersona.Location = new Point(marginX, infoTopY);
            gbInfoPersona.Size = new Size(baseWidth - (marginX * 2), infoHeight); // 750x160

            // Lado Izquierdo (Mantenemos la estructura CrearFilaDoble)
            CrearFilaDoble(gbInfoPersona, "DNI:", internalMarginX, 30, labelWidth, valueWidth, lblDni);
            CrearFilaDoble(gbInfoPersona, "Tipo:", internalMarginX, 30 + spacingY, labelWidth, valueWidth, lblTipoSocio);
            CrearFilaDoble(gbInfoPersona, "Ult. Pago:", internalMarginX, 30 + (spacingY * 2), labelWidth, valueWidth, lblFechaUltimoPago);
            CrearFilaDoble(gbInfoPersona, "Mora:", internalMarginX, 30 + (spacingY * 3), labelWidth, valueWidth, lblMesesAtraso);

            // Lado Derecho
            CrearFilaDoble(gbInfoPersona, "Nombre:", col2X, 30, labelWidth, valueWidth, lblNombrePersona);
            CrearFilaDoble(gbInfoPersona, "Estado:", col2X, 30 + spacingY, labelWidth, valueWidth, lblEstadoMembresia);
            CrearFilaDoble(gbInfoPersona, "Vencimiento:", col2X, 30 + (spacingY * 2), labelWidth, valueWidth, lblFechaVencimiento);


            // ----------------------------------------------------------------------
            // --- 4. GROUPBOX DE DATOS DE PAGO (gbDatosPago) ---
            // ----------------------------------------------------------------------
            int pagoTopY = infoTopY + infoHeight + 20;
            int pagoHeight = 170; // Aumentamos la altura para los botones
            int inputHeight = 35;

            // Ancho total disponible en una columna
            int fullWidth = baseWidth - (marginX * 2) - (internalMarginX * 2); // 750 - 30 = 720
            int columnSpacing = 20; // Espacio entre columnas

            // Ancho de una columna de entrada (mitad del espacio disponible, menos el espaciado)
            int internalInputWidth = (fullWidth - labelWidth * 2 - columnSpacing) / 2; // (720 - 240 - 20) / 2 = 230

            // Posición X para la segunda columna (la derecha)
            int col2StartX = internalMarginX + labelWidth + internalInputWidth + columnSpacing;

            // Variables de posicionamiento de filas
            int row1Y = 30;
            int row2Y = row1Y + 50; // Nueva fila para los combos
            int labelVOffset = 5;

            gbDatosPago.Text = "Detalles del Pago y Actividades";
            gbDatosPago.Location = new Point(marginX, pagoTopY);
            gbDatosPago.Size = new Size(baseWidth - (marginX * 2), pagoHeight); // 750x170

            // --- Fila 1: Monto (Izquierda) y Concepto (Derecha) ---

            // Monto (Columna 1)
            lblMonto.Text = "Monto a Pagar:";
            lblMonto.Location = new Point(internalMarginX, row1Y + labelVOffset);
            lblMonto.Size = new Size(labelWidth, 20);

            txtMonto.Location = new Point(internalMarginX + labelWidth, row1Y);
            txtMonto.Size = new Size(internalInputWidth, inputHeight);

            // Concepto (Columna 2)
            lblConcepto.Text = "Concepto:";
            lblConcepto.Location = new Point(col2StartX, row1Y + labelVOffset);
            lblConcepto.Size = new Size(labelWidth, 20);

            cmbConcepto.Location = new Point(col2StartX + labelWidth, row1Y);
            cmbConcepto.Size = new Size(internalInputWidth, inputHeight);

            // --- Fila 2: Forma de Pago (Izquierda) y Actividades (Derecha) ---

            // Forma de Pago (Columna 1)
            lblFormaPago.Text = "Forma de Pago:";
            lblFormaPago.Location = new Point(internalMarginX, row2Y + labelVOffset);
            lblFormaPago.Size = new Size(labelWidth, 20);

            cmbMedioPago.Location = new Point(internalMarginX + labelWidth, row2Y);
            cmbMedioPago.Size = new Size(internalInputWidth, inputHeight);

            // Actividades (Columna 2)
            lblActividades.Text = "Actividad (Opc.):";
            lblActividades.Location = new Point(col2StartX, row2Y + labelVOffset);
            lblActividades.Size = new Size(labelWidth, 20);

            // *** ASUMO QUE EL CONTROL cmbActividades EXISTE EN EL DISEÑADOR ***
            cmbActividades.Location = new Point(col2StartX + labelWidth, row2Y);
            cmbActividades.Size = new Size(internalInputWidth, inputHeight);

            // Configuración de ComboBox para que sean de solo lectura (DropDownList)
            cmbConcepto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedioPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbActividades.DropDownStyle = ComboBoxStyle.DropDownList;



            // ----------------------------------------------------------------------
            // --- 5. BOTONES DE ACCIÓN (MISMA ALTURA) ---
            // ----------------------------------------------------------------------
            int btnTopY = pagoTopY + pagoHeight + 20;
            int btnWidth = 150;
            int btnSpace = 10;
            int totalBtnWidth = 4 * btnWidth + 3 * btnSpace; // 4 botones

            // Posición X inicial para centrar los botones
            int startX = (baseWidth / 2) - (totalBtnWidth / 2);

            btnRegistrarPago.Text = "REGISTRAR PAGO";
            btnRegistrarPago.Location = new Point(startX, btnTopY);
            btnRegistrarPago.Size = new Size(btnWidth, 45); // Lo hacemos un poco más ancho

            btnLimpiar.Text = "LIMPIAR";
            btnLimpiar.Location = new Point(startX + btnWidth + btnSpace, btnTopY);
            btnLimpiar.Size = new Size(btnWidth, 45);

            btnReimprimirCarnet.Text = "REIMPRIMIR";
            btnReimprimirCarnet.Location = new Point(startX + btnWidth * 2 + btnSpace * 2, btnTopY);
            btnReimprimirCarnet.Size = new Size(btnWidth, 45);

            btnCerrar.Text = "CERRAR";
            btnCerrar.Location = new Point(startX + btnWidth * 3 + btnSpace * 3, btnTopY);
            btnCerrar.Size = new Size(btnWidth, 45);


            // CRÍTICO: Fijar el Dock/Anchor para el centrado, replicando el FrmInscripcionClub
            pnlBase.Anchor = AnchorStyles.None;
            pnlBase.Dock = DockStyle.None;

        }

        private void CrearFilaDoble(GroupBox parent, string labelText, int x, int y, int labelWidth, int valueWidth, Label valueLabel)
        {

            // EJEMPLO: Si usaras un Label temporal
            Label fixedLabel = new Label();
            fixedLabel.Text = labelText;
            fixedLabel.Location = new Point(x, y + 5);
            fixedLabel.Size = new Size(labelWidth, 20);
            fixedLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            fixedLabel.ForeColor = EstilosGlobales.ColorTextoClaro;
            parent.Controls.Add(fixedLabel);

            // RECOMENDACIÓN: Usa el Label que ya tienes en el diseñador. 
            // Como no sé el nombre del Label fijo, ajusto solo el Label de valor.

            // Ajustamos el Label de valor (ej: lblDni)
            valueLabel.Text = ""; // Se actualiza en btnBuscar_Click
            valueLabel.Location = new Point(x + labelWidth, y + 5);
            valueLabel.Size = new Size(valueWidth, 20);
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            valueLabel.ForeColor = EstilosGlobales.ColorTextoClaro;



        }



        #endregion

        #region EVENTOS DE FORMULARIO

        private void FrmRegistrarPagos_Load(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => Utilitarios.Utilitarios.ForzarCentrado(this, pnlBase, _anchoMenuLateral)));
            CargarConceptosPago();
            CargarActividades();
        }

        #endregion

        #region VALIDACIONES DE ENTRADA


        private void txtBuscarIdentificador_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validaciones.SoloNumeros(e);
        }


        #endregion

        #region METODOS

        private void CargarConceptosPago()
        {
            cmbConcepto.Items.Clear();
            cmbConcepto.Items.Add("Cuota Mensual");
            cmbConcepto.Items.Add("Acceso Diario");
            // cmbConcepto.Items.Add("Alquiler de Cancha"); // Ejemplo de otro concepto
            // Opcional: Establecer un concepto por defecto, o dejarlo vacío
            cmbConcepto.SelectedIndex = -1;

        }

        private void CargarFormaPago(PersonaPagoDetalleDTO personaEnco )
        {
             
            if (personaEnco.EsSocio)
            {
                cmbMedioPago.Items.Clear();
                cmbMedioPago.Items.Add("EFECTIVO");
                cmbMedioPago.Items.Add("TARJETA 1 CUOTA");
                cmbMedioPago.Items.Add("TARJETA 3 CUOTAS");
                cmbMedioPago.Items.Add("TARJETA 6 CUOTAS");
                cmbMedioPago.Items.Add("TRANSFERENCIA");
                cmbMedioPago.SelectedIndex = -1;
            }
            else
            {
                cmbMedioPago.Items.Clear();
                cmbMedioPago.Items.Add("EFECTIVO");
                cmbMedioPago.Items.Add("TARJETA 1 CUOTA");
                cmbMedioPago.Items.Add("TRANSFERENCIA");
                cmbMedioPago.SelectedIndex = -1;
            }
            
        }

        private void CargarActividades()
        {
            try
            {
                var listaActividades = oActividadBLL.ObtenerTodasActividades();

                // 1. Opción por defecto para "Ninguna Actividad Adicional" (ID=0, Costo=0)
                var listaConOpcionNinguna = new List<Actividad> {
            new Actividad { IdActividad = 0, Nombre = "NINGUNA ACTIVIDAD ADICIONAL", Costo = 0m }
        };
                listaConOpcionNinguna.AddRange(listaActividades);

                // 2. Configurar el ComboBox
                cmbActividades.DataSource = listaConOpcionNinguna;
                cmbActividades.DisplayMember = "Nombre";
                cmbActividades.ValueMember = "IdActividad";
                cmbActividades.SelectedIndex = 0; // Seleccionar la opción "Ninguna"

                cmbActividades.Enabled = false;
            }
            catch (Exception ex)
            {
                // [!] REFACTORIZADO: Reemplazo de MessageBox.Show por Prompt.MostrarError
                Prompt.MostrarError($"Error al cargar actividades: {ex.Message}", MensajesUI.TITULO_ERROR);
            }
        }

        // --- Función Auxiliar para Extracción de Meses de Atraso (Necesaria para el cálculo) ---
        private int ExtraerMesesAtraso(string estadoMembresia)
        {
            if (string.IsNullOrEmpty(estadoMembresia) || !estadoMembresia.Contains("meses de atraso"))
            {
                // Devuelve 0 para "AL DÍA", "N/A - Acceso Diario" o cualquier otro estado.
                return 0;
            }

            // Regex para encontrar el número dentro del paréntesis, antes de "meses"
            var match = Regex.Match(estadoMembresia, @"\((\d+)\s+meses de atraso\)");

            if (match.Success && match.Groups.Count > 1)
            {
                if (int.TryParse(match.Groups[1].Value, out int meses))
                {
                    return meses;
                }
            }
            return 0;
        }

        private void RecalcularMonto()
        {
            // Criterio de Guardia: Si no hay persona encontrada o el combo de concepto está vacío, salir.
            if (_personaEncontrada == null || cmbConcepto.SelectedItem == null)
            {
                txtMonto.Text = "0.00";
                btnRegistrarPago.Enabled = false;
                cmbActividades.Enabled = false;
                return;
            }

            decimal montoBase = 0m;
            decimal montoActividad = 0m;
            decimal montoTotal = 0m;
            string concepto = cmbConcepto.SelectedItem.ToString() ?? string.Empty;

            // --- 1. Lógica basada en el Concepto seleccionado ---

            if (concepto == "Acceso Diario")
            {
                // ACCESO DIARIO (Permitido para Socios al día y No Socios)
                montoBase = oConfiguracionBLL.ObtenerMontoAccesoDiario();
                cmbActividades.Enabled = true; // Se permite seleccionar actividad adicional

                // Calcular costo de la Actividad (si está seleccionada y habilitada)
                if (cmbActividades.SelectedIndex > 0)
                {
                    // Asumimos que el item es una ActividadEntidad
                    Actividad? actividadSeleccionada = cmbActividades.SelectedItem as Actividad;
                    montoActividad = actividadSeleccionada?.Costo ?? 0m;
                }

                montoTotal = montoBase + montoActividad;
            }
            else if (concepto == "Cuota Mensual")
            {
                // CUOTA MENSUAL (Solo para Socios)

                // Al pagar Cuota, NUNCA se cobra Actividad adicional.
                cmbActividades.Enabled = false;
                cmbActividades.SelectedIndex = 0; // Forzar a "Ninguna"

                if (_personaEncontrada.EsSocio)
                {
                    if (lblEstadoMembresia.Text.Contains("MORA"))
                    {
                        // 🚨 CASO 1: MORA (Re-calculamos la deuda para evitar leer del txtMonto)

                        // Extraemos los meses de mora del estado (debe ser el estado de la BLL)
                        // Usamos el estado del DTO, no de la etiqueta, para mayor robustez
                        int mesesMora = ExtraerMesesAtraso(_personaEncontrada.EstadoMembresia);

                        if (mesesMora == 0)
                        {
                            // Si el estado dice MORA pero extrae 0, forzamos a 1 (pago mínimo por mora).
                            mesesMora = 1;
                        }

                        decimal cuotaBase = oConfiguracionBLL.ObtenerMontoCuotaBase();
                        montoTotal = cuotaBase * mesesMora;
                        meses = mesesMora;
                    }
                    else
                    {
                        // CASO 2: SOCIO AL DÍA (Pago adelantado)
                        montoBase = oConfiguracionBLL.ObtenerMontoCuotaBase();
                        montoTotal = montoBase;
                    }
                }
                else
                {
                    // Concepto de Cuota Mensual no debería estar habilitado/seleccionado para No Socios.
                    montoTotal = 0m;
                }
            }

            // --- 2. Actualizar UI y Habilitar Botón ---
            txtMonto.Text = montoTotal.ToString("F2"); // Formato de dos decimales
            btnRegistrarPago.Enabled = montoTotal > 0 && cmbMedioPago.SelectedIndex != -1;
        }

        /// <summary>Limpia todos los campos de información y pago.</summary>
        private void LimpiarFormularioBusqueda(bool limpiarTodo = false)
        {
            lblNombrePersona.Text = "Esperando búsqueda...";
            lblEstadoMembresia.Text = "Estado: N/A";
            lblFechaUltimoPago.Text = string.Empty;
            lblTipoSocio.Text = string.Empty;
            lblDni.Text = string.Empty;
            lblFechaVencimiento.Text = string.Empty;
            lblMesesAtraso.Text = string.Empty;
            lblMesesAtraso.ForeColor = EstilosGlobales.ColorTextoClaro;
            txtMonto.Text = "0.00";
            cmbConcepto.SelectedIndex = -1;
            cmbMedioPago.SelectedIndex = -1;
            cmbActividades.SelectedIndex = 0; // Limpiar Actividad
            cmbActividades.Enabled = false;
            btnRegistrarPago.Enabled = false;
            btnReimprimirCarnet.Enabled = false; // <--- Debe estar aquí para el estado inicial

            if (limpiarTodo)
            {
                txtBuscarIdentificador.Text = string.Empty;
            }
        }

        /// <summary>Muestra los datos fijos de la persona encontrada y configura el concepto inicial.</summary>
        private void ConfigurarUIInicial(PersonaPagoDetalleDTO persona)
        {
            _personaEncontrada= persona;
             decimal totalAPagar;
            // Datos de Identificación
            lblNombrePersona.Text = persona.NombreCompleto;
            lblDni.Text = persona.DNI;
            CargarFormaPago(_personaEncontrada);
            // Configuración por tipo de persona
            if (persona.EsSocio)
            {
                lblTipoSocio.Text = "Socio";
                // Mostrar último pago
                lblFechaUltimoPago.Text = persona.UltimaCuotaCubierta == DateTime.MinValue
                    ? "Nunca pagó / Inicial"
                    : persona.UltimaCuotaCubierta.ToString("dd/MM/yyyy");

                // 🚨 Lógica de Estado y Concepto inicial (Corrección Central)
               // lblEstadoMembresia.Text = persona.EstadoMembresia.StartsWith("PENDIENTE: MORA") ? "PENDIENTE: MORA" : persona.EstadoMembresia;
                // Lógica de Estado y Concepto inicial
                if (persona.EstadoMembresia.StartsWith("AL DÍA"))
                {
                    // SOCIO AL DÍA
                    lblEstadoMembresia.Text = "AL DÍA";
                    lblEstadoMembresia.ForeColor = EstilosGlobales.ColorExito;

                    // Configurar Vencimiento
                    DateTime fechaVencimiento = persona.UltimaCuotaCubierta.AddMonths(1);
                    lblFechaVencimiento.Text = fechaVencimiento.ToString("dd/MM/yyyy");

                    // Configurar Pago Sugerido: Cuota Mensual
                    lblMesesAtraso.Text = "0 meses de atraso.";
                    lblMesesAtraso.ForeColor = EstilosGlobales.ColorExito;
                    cmbConcepto.SelectedIndex = 0; // Cuota Mensual
                    cmbConcepto.Enabled = true;

                    btnReimprimirCarnet.Enabled = true; // Socio AL DÍA puede reimprimir.

                }
                else // PENDIENTE/MORA
                {
                    // SOCIO EN MORA
                    lblEstadoMembresia.Text = "PENDIENTE: MORA";
                    lblEstadoMembresia.ForeColor = EstilosGlobales.ColorError;

                    // Configurar Primer Cuota Pendiente
                    DateTime fechaPrimerCuotaPendiente = persona.UltimaCuotaCubierta.AddMonths(1);
                    lblFechaVencimiento.Text = fechaPrimerCuotaPendiente.ToString("dd/MM/yyyy");

                    // Calcular y Mostrar Mora (se necesita la cuota base aquí)
                    decimal cuotaBase = oConfiguracionBLL.ObtenerMontoCuotaBase();
                    int mesesMora = ExtraerMesesAtraso(persona.EstadoMembresia);
                    if (mesesMora == 0) { mesesMora = 1; }//modificacion
                    totalAPagar = cuotaBase * mesesMora;

                    // Configurar Pago Obligatorio: Cuota Mensual por el total adeudado
                    lblMesesAtraso.Text = $"{mesesMora} cuota(s) pendiente(s). Total: {totalAPagar:C}";
                    lblMesesAtraso.ForeColor = EstilosGlobales.ColorError;
                    txtMonto.Text = totalAPagar.ToString("F2"); // Establecer el monto de MORA

                    cmbConcepto.SelectedIndex = 0; // Cuota Mensual
                    cmbConcepto.Enabled = false; // Forzar

                    btnReimprimirCarnet.Enabled = false; // Socio en MORA NO puede reimprimir.

                }
            }
            else // NO SOCIO
            {
                lblTipoSocio.Text = "No Socio";
                lblEstadoMembresia.Text = "Acceso Diario";
                lblEstadoMembresia.ForeColor = EstilosGlobales.ColorAdvertencia;
               // CargarFormaPago(_personaEncontrada);

                lblFechaVencimiento.Text = "N/A";
                lblFechaUltimoPago.Text = "N/A";
                lblMesesAtraso.Text = "No aplica cuota.";
                lblMesesAtraso.ForeColor = EstilosGlobales.ColorTextoClaro;

                // Configurar Pago Sugerido: Acceso Diario
                cmbConcepto.SelectedIndex = 1; // Acceso Diario
                cmbConcepto.Enabled = false; // Forzar

                btnReimprimirCarnet.Enabled = false; // No Socio nunca puede reimprimir carnet.
            }

           
            RecalcularMonto();
        }

        private string ImprimirComprobante(int idRegistro, bool esCuota, string formaPago, decimal montoTotal)
        {
            try
            {
                string nombreCompleto = _personaEncontrada?.NombreCompleto ?? "N/A";
                string dni = _personaEncontrada?.DNI ?? "N/A";
                //decimal montoTotal = Convert.ToDecimal(this.txtMonto.Text);
                string ruta = "";

                if (esCuota)
                {
                    bool estaEnMora = _personaEncontrada!.EstadoMembresia.Contains("MORA");
                    int mesesPagados = estaEnMora
                                         ? ExtraerMesesAtraso(_personaEncontrada.EstadoMembresia)
                                         : 1;
                    DateTime fechaVencimientoAnterior = _personaEncontrada!.UltimaCuotaCubierta;
                    

                    ruta = Utilitarios.PdfGenerator.GenerarComprobanteCuota(
                        idRegistro, nombreCompleto, dni, montoTotal* meses, formaPago,
                        fechaVencimientoAnterior, mesesPagados
                    );
                }
                else
                {
                    // Acceso Diario (con o sin actividad)
                    Actividad? actividadSeleccionada = cmbActividades.SelectedIndex > 0
                        ? cmbActividades.SelectedItem as Actividad
                        : null;

                    string nombreActividad = actividadSeleccionada?.Nombre ?? "Ninguna";
                    decimal costoActividad = actividadSeleccionada?.Costo ?? 0m;
                    decimal costoBase = oConfiguracionBLL.ObtenerMontoAccesoDiario();

                    ruta = Utilitarios.PdfGenerator.GenerarComprobanteAccesoActividad(
                        idRegistro, dni, nombreCompleto, montoTotal, formaPago,
                        nombreActividad, costoActividad, costoBase
                    );
                }
                return ruta;

            }
            catch (Exception ex)
            {
                // [!] REFACTORIZADO: Reemplazo de MessageBox.Show por Prompt.MostrarAlerta
                Prompt.MostrarAlerta($"El pago fue registrado, pero ocurrió un error al generar el PDF: {ex.Message}", MensajesUI.IMPRESION_FALLO_WARN_MSG);
                return string.Empty;
            }
        }


        #endregion

        #region BOTONES DE ACCION
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Limpieza de todos los campos de visualización antes de la búsqueda
            LimpiarFormularioBusqueda();

            string identificador = txtBuscarIdentificador.Text.Trim();

            if (string.IsNullOrEmpty(identificador))
            {
                // [!] REFACTORIZADO
                Prompt.MostrarAlerta("Debe ingresar un DNI o un Número de Carnet.", MensajesUI.TITULO_ADVERTENCIA);
                return;
            }

            // 1. Ejecutar la BLL
            try
            {
                _personaEncontrada = oPersonaBLL.BuscarPersonaParaPago(identificador);
                
            }
            catch (Exception ex)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarError($"Ocurrió un error al buscar la persona: {ex.Message}", MensajesUI.TITULO_ERROR);
                return;
            }

            // 2. Procesar el resultado
            if (_personaEncontrada != null)
            {
                // Pasa el objeto encontrado al método auxiliar para configurar la UI
                ConfigurarUIInicial(_personaEncontrada);
            }
            else
            {
                // [!] REFACTORIZADO: Uso de constante de MensajesUI
                Prompt.MostrarAlerta(MensajesUI.BUSQUEDA_FALLIDA_MSG, MensajesUI.TITULO_ADVERTENCIA);
                LimpiarFormularioBusqueda();
                lblNombrePersona.Text = "Persona no encontrada.";
            }
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            // --- 1. VALIDACIONES CRÍTICAS ---
            if (_personaEncontrada == null)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarAlerta(MensajesUI.PERSONA_NO_ENCONTRADA_WARN_MSG, MensajesUI.TITULO_ADVERTENCIA);
                return;
            }
            if (!decimal.TryParse(txtMonto.Text, out decimal montoAPagar) || montoAPagar <= 0)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarAlerta(MensajesUI.MONTO_INVALIDO_WARN_MSG, MensajesUI.TITULO_ADVERTENCIA);
                return;
            }
            if (cmbMedioPago.SelectedIndex == -1)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarAlerta(MensajesUI.FORMA_PAGO_INVALIDA_WARN_MSG, MensajesUI.TITULO_ADVERTENCIA);
                return;
            }

            // --- 2. PREPARACIÓN DE DATOS COMUNES ---
            string concepto = cmbConcepto.SelectedItem.ToString() ?? string.Empty;
            string formaPago = cmbMedioPago.SelectedItem.ToString() ?? "EFECTIVO";//cambio
            int resultadoRegistro = 0;
            string mensajeExito = "";
            btnRegistrarPago.Enabled = true;
            try
            {
                if (concepto == "Cuota Mensual")
                {
                    // --- A. PAGO DE CUOTA (SOCIO) ---

                    // Creamos la Entidad Cuota con los datos necesarios para tu BLL
                    var oCuota = new Cuota
                    {
                        IdPersona = _personaEncontrada.IdPersona,
                        Monto = montoAPagar,
                        Concepto = concepto, // Concepto: "Cuota Mensual"
                        FormaPago = formaPago,
                       
                    };

                    // Llamamos a tu método BLL, que devuelve el IdCuota registrada.
                    resultadoRegistro = oCuotaBLL.RegistrarPagoDeCuota(oCuota);
                    mensajeExito = "Cuota(s) registrada(s) exitosamente.";
                }
                else if (concepto == "Acceso Diario")
                {
                    // --- B. REGISTRO DE ACCESO DIARIO (SOCIO o NO SOCIO) ---

                    // Obtener el ID de la Actividad (NULL si se eligió "Ninguna...")
                    int? idActividad = null;
                    if (cmbActividades.SelectedIndex > 0)
                    {
                        Actividad? actividadSeleccionada = cmbActividades.SelectedItem as Actividad;
                        idActividad = actividadSeleccionada?.IdActividad;
                    }

                    // Creamos la Entidad RegistroAcceso
                    var oRegistro = new RegistroAcceso
                    {
                        IdPersona = _personaEncontrada.IdPersona,
                        Monto = montoAPagar,
                        FormaPago = formaPago,
                        Fecha = DateTime.Now, // Fecha del acceso es HOY
                        IdActividad = idActividad // Esto es CRÍTICO
                    };

                    // Llamamos a tu método BLL, que devuelve el IdRegistro.
                    resultadoRegistro = oRegistroAccesoBLL.RegistrarAccesoYpago(oRegistro);
                    mensajeExito = "Acceso diario registrado exitosamente.";
                }
                else
                {
                    // [!] REFACTORIZADO
                    Prompt.MostrarError(MensajesUI.CONCEPTO_PAGO_INVALIDO_ERROR_MSG, MensajesUI.TITULO_ERROR);
                    return;
                }

                // --- 3. RESULTADO DE LA TRANSACCIÓN ---
                if (resultadoRegistro > 0)
                {
                    // [!] REFACTORIZADO
                    Prompt.MostrarExito($"¡Transacción exitosa! {mensajeExito} (ID: {resultadoRegistro})", MensajesUI.TITULO_EXITO);
                    
                    // Esto garantiza que el usuario vea el nuevo estado (ej. de MORA a AL DÍA) inmediatamente.
                    _personaEncontrada = oPersonaBLL.BuscarPersonaParaPago(_personaEncontrada.DNI);
                    if (_personaEncontrada != null)
                    {
                        // ConfigurarUIInicial ya se encarga de mostrar el estado de mora/al día y el monto a cero
                        ConfigurarUIInicial(_personaEncontrada);
                    }
                    // FIN: Re-consulta y Re-configuración de la UI

                    // ** 🚨 NUEVO PASO CRÍTICO: IMPRIMIR 🚨 **
                    bool esCuota = (concepto == "Cuota Mensual");

                    try
                    {
                        Decimal monto = Convert.ToDecimal(txtMonto.Text);
                        // Llama a la impresión y obtiene la ruta
                        string rutaComprobante = ImprimirComprobante(resultadoRegistro, esCuota, formaPago,monto );

                        if (!string.IsNullOrEmpty(rutaComprobante))
                        {
                            // [!] REFACTORIZADO: Diálogo de Confirmación
                            DialogResult dialogResult = Prompt.Confirmar(
                                $"Comprobante generado con éxito en:\n{rutaComprobante}\n\n¿Desea abrir el archivo ahora?",
                                MensajesUI.TITULO_IMPRESION
                            );

                            if (dialogResult == DialogResult.Yes)
                            {
                                // Usamos el método de apertura seguro que sabes que funciona
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaComprobante) { UseShellExecute = true });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // [!] REFACTORIZADO
                        Prompt.MostrarAlerta(
                            $"Error al intentar generar el comprobante PDF: {ex.Message}",
                            MensajesUI.IMPRESION_FALLO_WARN_MSG
                        );
                    }

                    LimpiarFormularioBusqueda(limpiarTodo: true);
                }
                else
                {
                    // [!] REFACTORIZADO
                    Prompt.MostrarError(MensajesUI.REGISTRO_FALLO_ERROR_MSG, MensajesUI.TITULO_ERROR);
                }
            }
            catch (Exception ex)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarError($"Error crítico al procesar el pago: {ex.Message}", MensajesUI.PAGO_ERROR_CRITICO_ERROR_MSG);
            }
        }

        private void btnReimprimirCarnet_Click(object sender, EventArgs e)
        {
            // 1. Validaciones Críticas
            if (_personaEncontrada == null || !_personaEncontrada.EsSocio || _personaEncontrada.NumeroCarnet == null)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarAlerta(MensajesUI.REIMPRESION_CARNET_WARN_MSG, MensajesUI.TITULO_ADVERTENCIA);
                return;
            }

            // 2. Construcción del DTO requerido por el Generador de PDF
            var detalleParaCarnet = new PersonaDetalleDTO
            {
                Dni = _personaEncontrada.DNI,
                Nombre = _personaEncontrada.Nombre ?? string.Empty,
                Apellido = _personaEncontrada.Apellido ?? string.Empty,
                NumeroCarnet = _personaEncontrada.NumeroCarnet,

                // El estado se deduce de la membresía: si no está en 'MORA', está ACTIVO para el carnet.
                // (El botón solo debería estar habilitado si el estado es 'AL DÍA').
                EstadoActivo = !_personaEncontrada.EstadoMembresia.Contains("MORA"),
            };

            // 3. Generación del Carnet PDF
            string rutaCarnet = string.Empty;
            try
            {
                rutaCarnet = Utilitarios.PdfGenerator.GenerarCarnetSocio(detalleParaCarnet);

                // 4. Notificación y Apertura de Archivo
                // [!] REFACTORIZADO: Diálogo de Confirmación
                DialogResult dialogResult = Prompt.Confirmar(
                    $"Carnet reimpreso con éxito. ✅\nGuardado en: {rutaCarnet}\n\n¿Desea abrir el carnet ahora?",
                    MensajesUI.TITULO_REIMPRESION_EXITO
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Uso del método seguro (que ya funciona en tu proyecto) para abrir el PDF
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(rutaCarnet) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                // [!] REFACTORIZADO
                Prompt.MostrarError($"Error al intentar generar el PDF del carnet: {ex.Message}", MensajesUI.IMPRESION_ERROR_CRITICO_ERROR_MSG);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormularioBusqueda(true);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Solo recalculamos si ya se ha buscado y encontrado una persona
            if (_personaEncontrada != null)
            {
                RecalcularMonto();
            }

        }

        private void cmbActividades_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Solo recalculamos si ya se ha buscado y encontrado una persona
            if (_personaEncontrada != null)
            {
                RecalcularMonto();
            }
        }



        private void cmbMedioPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Usamos el mismo criterio: si hay monto válido, habilitar.
            if (decimal.TryParse(txtMonto.Text, out decimal montoActual) && montoActual > 0)
            {
                btnRegistrarPago.Enabled = cmbMedioPago.SelectedIndex != -1; // <-- AQUÍ
            }
            else
            {
                // Si el monto es 0 o inválido, se queda deshabilitado
                btnRegistrarPago.Enabled = false;
            }
        }
        #endregion
    }
}
