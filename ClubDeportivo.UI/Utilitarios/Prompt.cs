using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ClubDeportivo.UI.Utilitarios
{
    public static class Prompt
    {
        // Tipos de iconos que el diálogo puede mostrar
        public enum IconType
        {
            Pregunta,
            Informacion,
            Advertencia,
            Error,
            Ok
        }

        private static bool isDragging = false;
        private static Point lastCursor;
        private static Point lastForm;

        // =================================================================
        // 1. DIÁLOGOS DE ENTRADA Y SELECCIÓN (ShowDialog, MostrarMenu)
        // =================================================================

        // A. ShowDialog (InputBox genérico)
        public static string ShowDialog(string text, string caption)
        {
            // Crea un nuevo formulario
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = EstilosGlobales.ColorFondo // Usamos un estilo consistente
            };

            // Etiqueta de la pregunta
            Label textLabel = new Label()
            {
                Left = 20,
                Top = 20,
                Width = 350,
                Text = text,
                ForeColor = EstilosGlobales.ColorTextoClaro // Texto visible
            };

            // Campo de texto para la respuesta
            TextBox inputBox = new TextBox()
            {
                Left = 20,
                Top = 45,
                Width = 350,
                Font = EstilosGlobales.EstiloCampo
            };

            // Botón Aceptar
            Button confirmation = new Button()
            {
                Text = MensajesUI.BOTON_ACEPTAR,
                Width = 80,
                Top = 85,
                DialogResult = DialogResult.OK
            };
            EstilosGlobales.AplicarEstiloBotonAccion(confirmation);
            confirmation.Left = (prompt.Width - 80 - 20); // Ajuste simple a la derecha
            confirmation.Top = 85;

            // Botón Cancelar
            Button cancel = new Button()
            {
                Text = MensajesUI.BOTON_CANCELAR,
                Left = confirmation.Left - confirmation.Width - 10, // A la izquierda de Aceptar
                Width = 80,
                Top = 85,
                DialogResult = DialogResult.Cancel
            };
            EstilosGlobales.AplicarEstiloBotonAccion(cancel);
            cancel.BackColor = EstilosGlobales.ColorError;


            // Configuración final y adición de controles
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.AcceptButton = confirmation; // Presionar Enter para Aceptar
            prompt.CancelButton = cancel;        // Presionar ESC para Cancelar


            // Mostrar el diálogo
            DialogResult result = prompt.ShowDialog();

            // Devolver el texto si el usuario presionó Aceptar
            if (result == DialogResult.OK)
            {
                return inputBox.Text.Trim();
            }
            else
            {
                return string.Empty; // Retorna vacío si cancela
            }
        }

        // B. MostrarMenu (Diálogo de selección con botones)
        public static string MostrarMenu(string caption, string text, string[] options)
        {
            Form prompt = new Form()
            {
                Width = 450,
                Height = 200 + (options.Length * 20), // Altura dinámica
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = EstilosGlobales.ColorFondo
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true, ForeColor = EstilosGlobales.ColorTextoClaro };
            prompt.Controls.Add(textLabel);

            int currentTop = 60;
            string selectedOption = string.Empty;

            // Generación de botones de opción
            foreach (string option in options)
            {
                Button optionButton = new Button()
                {
                    Text = option,
                    Left = 20,
                    Top = currentTop,
                    Width = 400,
                    Height = 35,
                    DialogResult = DialogResult.OK,
                    Tag = option // Usamos el Tag para guardar el valor
                };
                EstilosGlobales.AplicarEstiloBotonAccion(optionButton);
                optionButton.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorAcento;
                optionButton.Click += (sender, e) =>
                {
                    selectedOption = (sender as Button).Tag.ToString();
                    prompt.Close();
                };

                prompt.Controls.Add(optionButton);
                currentTop += 40;
            }

            // Botón de Cancelar al final 
            Button cancel = new Button()
            {
                Text = MensajesUI.BOTON_CANCELAR,
                Left = 250,
                Top = currentTop + 10,
                Width = 170,
                Height = 30,
                DialogResult = DialogResult.Cancel
            };
            EstilosGlobales.AplicarEstiloBotonAccion(cancel);
            cancel.BackColor = EstilosGlobales.ColorError;
            prompt.Controls.Add(cancel);

            DialogResult result = prompt.ShowDialog();

            if (result == DialogResult.OK)
            {
                return selectedOption;
            }
            else
            {
                return string.Empty; // Retorna vacío si cancela
            }
        }


        // =================================================================
        // 2. DIÁLOGOS DE ALERTA (Un solo botón OK / Retorno VOID)
        // =================================================================

        // Alerta: Método principal interno que construye el diálogo con un botón OK
        public static void Alerta(string message, string caption, IconType iconType)
        {
            // --- 1. VARIABLES DE DISEÑO ---
            int buttonHeight = 30;
            int margin = 30;
            int iconSize = 64;
            int textWidth = 360;
            int titleBarHeight = 40;

            Form prompt = new Form()
            {
                Width = 500,
                Height = 0,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = EstilosGlobales.ColorBoton
            };

            // --- 1.1 BARRA DE TÍTULO PERSONALIZADA ---
            Panel titleBar = new Panel()
            {
                Height = titleBarHeight,
                Dock = DockStyle.Top,
                BackColor = EstilosGlobales.ColorAcento
            };
            prompt.Controls.Add(titleBar);

            int closeButtonWidth = 40;
            int titleMargin = 10;

            // Título
            Label titleLabel = new Label()
            {
                Text = caption,
                ForeColor = Color.White,
                Font = EstilosGlobales.EstiloCampo,
                Location = new Point(titleMargin, (titleBarHeight - EstilosGlobales.EstiloCampo.Height) / 2),
                Width = prompt.Width - titleMargin - closeButtonWidth - 10,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            titleBar.Controls.Add(titleLabel);

            // Botón de Cierre (X)
            Button btnClose = new Button()
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = EstilosGlobales.ColorAcento,
                Width = 40,
                Height = titleBarHeight,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Font = EstilosGlobales.EstiloTitulo
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TextAlign = ContentAlignment.MiddleCenter;
            btnClose.FlatAppearance.MouseOverBackColor = Color.DarkRed; // Mantener un hover para la X
            btnClose.Click += (s, e) => prompt.Close();
            titleBar.Controls.Add(btnClose);

            // --- Lógica de Arrastre ---
            titleBar.MouseDown += (s, e) => { isDragging = true; lastCursor = Cursor.Position; lastForm = prompt.Location; };
            titleBar.MouseMove += (s, e) => {
                if (isDragging)
                {
                    int xDiff = Cursor.Position.X - lastCursor.X;
                    int yDiff = Cursor.Position.Y - lastCursor.Y;
                    prompt.Location = new Point(lastForm.X + xDiff, lastForm.Y + yDiff);
                }
            };
            titleBar.MouseUp += (s, e) => { isDragging = false; };


            // --- 2. CONFIGURACIÓN DEL ICONO ---
            PictureBox iconBox = new PictureBox()
            {
                Left = margin,
                Top = titleBarHeight + margin,
                Size = new Size(iconSize, iconSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            prompt.Controls.Add(iconBox);

            string iconPath = $"Resources\\Img\\icon_{iconType.ToString().ToLower()}.png";
            try { iconBox.Image = Image.FromFile(iconPath); }
            catch (FileNotFoundException) { /* Manejo de error */ }

            // --- 3. ETIQUETA DEL MENSAJE (Centrado ajustado) ---
            Label messageLabel = new Label()
            {
                Left = margin + iconSize + 10,
                Top = titleBarHeight + margin,
                Width = textWidth,
                Text = message,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = EstilosGlobales.ColorTextoClaro,
                Font = EstilosGlobales.EstiloCampo,
                AutoSize = true,
                MaximumSize = new Size(textWidth, 0)
            };
            prompt.Controls.Add(messageLabel);

            // --- CÁLCULO CRÍTICO DE ALTURA ---
            int minContentBottom = iconBox.Top + iconBox.Height;
            int textBottom = messageLabel.Top + messageLabel.Height;
            int contentMaxBottom = Math.Max(minContentBottom, textBottom);
            int buttonTop = contentMaxBottom + margin;
            int clientHeight = buttonTop + buttonHeight + margin;
            prompt.ClientSize = new Size(prompt.Width, clientHeight);

            // --- 4. BOTÓN ACEPTAR ÚNICO (Centrado y Estilo) ---
            int buttonY = buttonTop;

            Button btnOK = new Button()
            {
                Text = MensajesUI.BOTON_ACEPTAR,
                Width = 120,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.OK,
                Font = EstilosGlobales.EstiloTitulo
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnOK);
            btnOK.BackColor = EstilosGlobales.ColorAcento;
            btnOK.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorAdvertencia; // Usar color de advertencia para hover en OK

            btnOK.Left = (prompt.Width / 2) - (btnOK.Width / 2);

            prompt.Controls.Add(btnOK);
            prompt.AcceptButton = btnOK;
            prompt.CancelButton = btnOK;

            prompt.ShowDialog();
        }

        // Métodos de utilidad que usan Alerta (Retorno VOID)
        public static void MostrarExito(string message)
        {
            Alerta(message, MensajesUI.TITULO_EXITO, IconType.Ok);
        }

        public static void MostrarError(string message)
        {
            Alerta(message, MensajesUI.TITULO_ERROR, IconType.Error);
        }

        public static void MostrarAlerta(string message)
        {
            Alerta(message, MensajesUI.TITULO_ADVERTENCIA, IconType.Advertencia);
        }

        public static void MostrarInformacion(string message)
        {
            Alerta(message, MensajesUI.TITULO_INFORMACION, IconType.Informacion);
        }

        // Sobrecargas de Alerta
        public static void MostrarError(string message, string customCaption)
        {
            Alerta(message, customCaption, IconType.Error);
        }

        public static void MostrarAlerta(string message, string customCaption)
        {
            Alerta(message, customCaption, IconType.Advertencia);
        }

        public static void MostrarExito(string message, string customCaption)
        {
            Alerta(message, customCaption, IconType.Ok);
        }

        // =================================================================
        // 3. DIÁLOGOS DE CONFIRMACIÓN (Dos botones SI/NO / Retorno DialogResult o BOOL)
        // =================================================================

        // MostrarDialogoConfirmacion: Método principal interno que construye el diálogo SI/NO
        public static DialogResult MostrarDialogoConfirmacion(string message, string caption, IconType iconType)
        {
            // --- VARIABLES DE DISEÑO ---
            int buttonHeight = 30;
            int margin = 30;
            int iconSize = 64;
            int textWidth = 360;
            int titleBarHeight = 40;

            Form prompt = new Form()
            {
                Width = 500,
                Height = 0,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = EstilosGlobales.ColorBoton
            };

            // --- 1.1 BARRA DE TÍTULO PERSONALIZADA (Idéntica a Alerta) ---
            Panel titleBar = new Panel()
            {
                Height = titleBarHeight,
                Dock = DockStyle.Top,
                BackColor = EstilosGlobales.ColorAcento
            };
            prompt.Controls.Add(titleBar);

            int closeButtonWidth = 40;
            int titleMargin = 10;

            // Título
            Label titleLabel = new Label()
            {
                Text = caption,
                ForeColor = Color.White,
                Font = EstilosGlobales.EstiloCampo,
                Location = new Point(titleMargin, (titleBarHeight - EstilosGlobales.EstiloCampo.Height) / 2),
                Width = prompt.Width - titleMargin - closeButtonWidth - 10,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            titleBar.Controls.Add(titleLabel);

            // Botón de Cierre (X)
            Button btnClose = new Button()
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = EstilosGlobales.ColorAcento,
                Width = closeButtonWidth,
                Height = titleBarHeight,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Font = EstilosGlobales.EstiloTitulo
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TextAlign = ContentAlignment.MiddleCenter;
            btnClose.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            // CRÍTICO: El botón X debe cerrar el diálogo con Cancel
            btnClose.Click += (s, e) => prompt.DialogResult = DialogResult.Cancel;
            titleBar.Controls.Add(btnClose);

            // --- Lógica de Arrastre ---
            titleBar.MouseDown += (s, e) => { isDragging = true; lastCursor = Cursor.Position; lastForm = prompt.Location; };
            titleBar.MouseMove += (s, e) => {
                if (isDragging)
                {
                    int xDiff = Cursor.Position.X - lastCursor.X;
                    int yDiff = Cursor.Position.Y - lastCursor.Y;
                    prompt.Location = new Point(lastForm.X + xDiff, lastForm.Y + yDiff);
                }
            };
            titleBar.MouseUp += (s, e) => { isDragging = false; };


            // --- 2. CONFIGURACIÓN DEL ICONO ---
            PictureBox iconBox = new PictureBox()
            {
                Left = margin,
                Top = titleBarHeight + margin,
                Size = new Size(iconSize, iconSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            prompt.Controls.Add(iconBox);

            string iconPath = $"Resources\\Img\\icon_{iconType.ToString().ToLower()}.png";
            try { iconBox.Image = Image.FromFile(iconPath); }
            catch (FileNotFoundException) { /* Manejo de error */ }


            // --- 3. ETIQUETA DEL MENSAJE ---
            Label messageLabel = new Label()
            {
                Left = margin + iconSize + 10,
                Top = titleBarHeight + margin,
                Width = textWidth,
                Text = message,
                TextAlign = ContentAlignment.TopLeft,
                ForeColor = EstilosGlobales.ColorTextoClaro,
                Font = EstilosGlobales.EstiloCampo,
                AutoSize = true,
                MaximumSize = new Size(textWidth, 0)
            };
            prompt.Controls.Add(messageLabel);

            // 💥 CÁLCULO DE ALTURA 💥
            int minContentBottom = iconBox.Top + iconBox.Height;
            int textBottom = messageLabel.Top + messageLabel.Height;
            int contentMaxBottom = Math.Max(minContentBottom, textBottom);
            int buttonTop = contentMaxBottom + margin;
            int clientHeight = buttonTop + buttonHeight + margin;
            prompt.ClientSize = new Size(prompt.Width, clientHeight);

            // --- 4. BOTONES (SI/NO) ---
            int buttonY = buttonTop;

            // Botón NO (Botón de Cancelar a la Derecha)
            Button btnNo = new Button()
            {
                Text = MensajesUI.BOTON_NO,
                Width = 80,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.No,
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnNo);
            btnNo.BackColor = EstilosGlobales.ColorError;
            btnNo.Left = prompt.Width - margin - btnNo.Width;
            btnNo.FlatAppearance.MouseOverBackColor = Color.DarkRed;
            btnNo.Font = EstilosGlobales.EstiloTitulo;

            // Botón SÍ (Botón de Acción Principal a la Izquierda del NO)
            Button btnYes = new Button()
            {
                Text = MensajesUI.BOTON_SI,
                Width = 80,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.Yes,
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnYes);
            btnYes.BackColor = EstilosGlobales.ColorAcento;
            btnYes.Left = btnNo.Left - btnYes.Width - 10;
            btnYes.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            btnYes.Font = EstilosGlobales.EstiloTitulo;

            prompt.Controls.Add(btnYes);
            prompt.Controls.Add(btnNo);
            prompt.AcceptButton = btnYes;
            prompt.CancelButton = btnNo;

            return prompt.ShowDialog();
        }

        // Métodos de utilidad que usan MostrarDialogoConfirmacion (Retorno DialogResult/BOOL)

        // 3.1. Confirmar: Utilidad base para diálogos de pregunta (Retorna DialogResult)
        public static DialogResult Confirmar(string message, string caption)
        {
            return MostrarDialogoConfirmacion(message, caption, IconType.Pregunta);
        }

        // 3.2. MostrarConfirmacionDialogResult: Alias directo para el DialogResult
        public static DialogResult MostrarConfirmacionDialogResult(string message, string caption)
        {
            return MostrarDialogoConfirmacion(message, caption, IconType.Pregunta);
        }

        // 3.3. MostrarDialogoConfirmacion: Alias para usar un retorno BOOLEAN
        public static bool MostrarDialogoConfirmacion(string message)
        {
            // Llama a Confirmar (que a su vez llama a MostrarDialogoConfirmacion con IconType.Pregunta)
            return Confirmar(message, MensajesUI.TITULO_CONFIRMAR_ACCION) == DialogResult.Yes;
        }

        // 3.4. NUEVO MÉTODO AGREGADO: MostrarDialogoSiNo
        // Retorna el DialogResult (Yes/No) permitiendo especificar el título.
        public static DialogResult MostrarDialogoSiNo(string message, string caption)
        {
            return MostrarDialogoConfirmacion(message, caption, IconType.Pregunta);
        }

    }
}