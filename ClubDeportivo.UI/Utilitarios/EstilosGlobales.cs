using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace ClubDeportivo.UI.Utilitarios
{
    public static class EstilosGlobales
    {
        // =================================================================
        // 1. PALETA DE COLORES PRINCIPALES
        // =================================================================

        public static readonly Color ColorFondo = Color.FromArgb(35, 35, 35);
        public static readonly Color ColorAcento = Color.FromArgb(70, 80, 150);
        public static readonly Color ColorBoton = Color.FromArgb(60, 60, 60);
        public static readonly Color ColorBotonHover = Color.FromArgb(85, 85, 85); // Para efecto de mouse
        public static readonly Color ColorTextoClaro = Color.White;


        // =================================================================
        // 2. COLORES DE ESTADO
        // =================================================================

        public static readonly Color ColorExito = Color.FromArgb(100, 220, 100);
        public static readonly Color ColorError = Color.FromArgb(220, 100, 100);
        public static readonly Color ColorAdvertencia = Color.FromArgb(255, 192, 0);


        // =================================================================
        // 3. FUENTES (TIPOGRAFÍA)
        // =================================================================

        public static readonly Font EstiloTitulo = new Font("Century Gothic", 16F, FontStyle.Bold);
        public static readonly Font EstiloFuente = new Font("Century Gothic", 10F, FontStyle.Regular);
        // CRÍTICO: Añadido para cajas de texto y controles de entrada
        public static readonly Font EstiloCampo = new Font("Century Gothic", 12F, FontStyle.Regular);


        // =================================================================
        // MÉTODOS DE APLICACIÓN DE ESTILO
        // =================================================================

        // A. GLOBAL: Aplica el estilo base a CUALQUIER formulario
        public static void AplicarFormatoBase(Form form)
        {
            form.BackColor = ColorFondo;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ForeColor = ColorTextoClaro;
        }

        // B. MENU: Aplica estilo a botones de menú principal
        public static void AplicarEstiloBotonMenu(Button btn)
        {
            btn.BackColor = ColorAcento;
            btn.ForeColor = ColorTextoClaro;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = EstiloFuente;
            btn.Cursor = Cursors.Hand;
            btn.Height = 50;

            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Padding = new Padding(15, 0, 0, 0); // Ajusta la distancia del texto
        }

        // C. ACCION: Aplica estilo a botones de acción (Guardar, Buscar, Aceptar, Cancelar)
        public static void AplicarEstiloBotonAccion(Button btn)
        {
            btn.BackColor = ColorBoton;
            btn.ForeColor = ColorTextoClaro;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = EstiloFuente;
            btn.Cursor = Cursors.Hand;
            btn.Height = 35;
            btn.FlatAppearance.MouseOverBackColor = ColorBotonHover;
        }

        // D. DATOS: Aplica estilo a cajas de texto, combobox y otros campos de entrada.
        public static void AplicarEstiloCampo(Control ctrl)
        {
            ctrl.BackColor = Color.FromArgb(45, 45, 45);
            ctrl.ForeColor = ColorTextoClaro;
            ctrl.Font = EstiloCampo; // CRÍTICO: Usamos el nuevo EstiloCampo

            // 1. Estilo para TextBox, MaskedTextBox, ComboBox
            if (ctrl is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
            }
            // 2. CRÍTICO: Estilo para DateTimePicker para asegurar visibilidad en tema oscuro
            else if (ctrl is DateTimePicker dtp)
            {
                dtp.CalendarForeColor = ColorFondo;
                dtp.CalendarMonthBackground = ColorTextoClaro;
                dtp.CalendarTitleBackColor = ColorAcento;
                dtp.CalendarTitleForeColor = ColorTextoClaro;
                dtp.CalendarTrailingForeColor = ColorBotonHover;
                dtp.Format = DateTimePickerFormat.Short;
            }
        }

        // E. DATAGRIDVIEW: Aplica estilos visuales a la cuadrícula de datos
        // NOTA: Se ha mantenido el método con el nombre más completo.
        public static void AplicarEstiloDataGridView(DataGridView dgv)
        {
            // 1. Apariencia General del DGV
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = ColorFondo;
            dgv.GridColor = Color.FromArgb(50, 50, 50);
            dgv.Font = EstiloFuente;

            // 2. Comportamiento del DGV
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 3. Estilo de Encabezados (Headers)
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorAcento;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTextoClaro;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 40;

            // 4. Estilo de Celdas (Filas)
            dgv.DefaultCellStyle.BackColor = ColorFondo;
            dgv.DefaultCellStyle.ForeColor = ColorTextoClaro;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
            dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 130, 200);
            dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;
        }


        // =================================================================
        // MÉTODOS FALTANTES REQUERIDOS POR FrmActualizarPersona.cs
        // (AÑADIDOS ESTRICTAMENTE PARA LA COMPILACIÓN)
        // =================================================================

        // F. FALTANTE: Estilo para el título (lblTitulo)
        public static void AplicarEstiloTitulo(Label lbl)
        {
            lbl.ForeColor = ColorAcento;
            lbl.Font = EstiloTitulo;
            lbl.BackColor = Color.Transparent;
        }

        // G. FALTANTE: Estilo para el Label de datos (lblDni, lblNombre, etc.)
        public static void AplicarEstiloLabel(Label lbl)
        {
            lbl.ForeColor = ColorTextoClaro;
            lbl.Font = EstiloFuente;
            lbl.BackColor = Color.Transparent;
        }

        // H. FALTANTE: Estilo para GroupBoxes (gbDatosPersonales, gpTipoYEstado)
        public static void AplicarEstiloGroupBox(GroupBox gb)
        {
            gb.ForeColor = ColorTextoClaro;
            gb.BackColor = Color.Transparent; // Usamos transparente para que el fondo del Form se vea
            gb.Font = EstiloFuente;
        }

        // I. FALTANTE: Alias para el DGV (Si el formulario lo llama de forma abreviada)
        public static void AplicarEstiloDGV(DataGridView dgv)
        {
            AplicarEstiloDataGridView(dgv);
        }
    }
}