using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubDeportivo.UI.Utilitarios
{
    public static class Prompt
    {
        // A. ShowDialog (InputBox genérico) - Dejado como estaba en tu código
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
                Text = "Aceptar",
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
                Text = "Cancelar",
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
            prompt.CancelButton = cancel;       // Presionar ESC para Cancelar


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

        // B. MostrarMenu (Diálogo de selección con botones) - CRÍTICO para la Forma de Pago
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
                Text = "Cancelar",
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

        // Tipos de iconos que el diálogo puede mostrar
        public enum IconType
        {
            Pregunta,
            Informacion,
            Advertencia,
            Error,
            Ok
        }

        public static DialogResult Confirmar(string message, string caption)
        {
            return MostrarDialogoConfirmacion(message, caption, IconType.Pregunta);
        }

        private static bool isDragging = false;
        private static Point lastCursor;
        private static Point lastForm;

        //metodoDialogResult

        public static DialogResult MostrarDialogoConfirmacion(string message, string caption, IconType iconType)
        {
            // --- VARIABLES DE DISEÑO ---
            int buttonHeight = 30;
            int margin = 30;        // Margen estándar
            int iconSize = 64;
            int textWidth = 360;
            int titleBarHeight = 40; // Nueva Altura de la barra

            Form prompt = new Form()
            {
                Width = 500,
                Height = 0,
                // ¡CAMBIO CRÍTICO!
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                BackColor = EstilosGlobales.ColorBoton// lo cambie de color
            };

            // --- 1.1 BARRA DE TÍTULO PERSONALIZADA (COPIADA DE Alerta) ---
            Panel titleBar = new Panel()
            {
                Height = titleBarHeight,
                Dock = DockStyle.Top,
                BackColor = EstilosGlobales.ColorAcento
            };
            prompt.Controls.Add(titleBar);

            // Ancho y margen para el título y la X
            int closeButtonWidth = 40;
            int titleMargin = 10;

            // Título
            Label titleLabel = new Label()
            {
                Text = caption,
                ForeColor = Color.White,
                Font = EstilosGlobales.EstiloCampo,
                Location = new Point(titleMargin, (titleBarHeight - EstilosGlobales.EstiloCampo.Height) / 2),
                // Ancho máximo para el título
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


            // --- 2. CONFIGURACIÓN DEL ICONO (Posición ajustada) ---
            PictureBox iconBox = new PictureBox()
            {
                Left = margin,
                Top = titleBarHeight + margin, // ⬅️ Nuevo Top
                Size = new Size(iconSize, iconSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            prompt.Controls.Add(iconBox);

            string iconPath = $"Resources\\Img\\icon_{iconType.ToString().ToLower()}.png";
            try { iconBox.Image = Image.FromFile(iconPath); }
            catch (FileNotFoundException) { /* Manejo de error */ }


            // --- 3. ETIQUETA DEL MENSAJE (Posición ajustada) ---
            Label messageLabel = new Label()
            {
                Left = margin + iconSize + 10,
                Top = titleBarHeight + margin, // ⬅️ Nuevo Top
                Width = textWidth,
                Text = message,
                TextAlign = ContentAlignment.TopLeft,
                ForeColor = EstilosGlobales.ColorTextoClaro,
                Font = EstilosGlobales.EstiloCampo,
                AutoSize = true,
                MaximumSize = new Size(textWidth, 0)
            };
            prompt.Controls.Add(messageLabel);

            // =========================================================================
            // 💥 CÁLCULO DE ALTURA CRÍTICO Y SIMPLIFICADO (Basado en el nuevo Top) 💥

            // 1. Altura mínima del contenido: Posición Top del icono + altura del icono
            int minContentBottom = iconBox.Top + iconBox.Height;

            // 2. Altura del área de texto: Posición Top del Texto + Altura que ocupa el texto
            int textBottom = messageLabel.Top + messageLabel.Height;

            // 3. Altura de contenido: El punto más bajo de los controles
            int contentMaxBottom = Math.Max(minContentBottom, textBottom);

            // 4. Calcular el Top de los botones: Punto más bajo + margen
            int buttonTop = contentMaxBottom + margin;

            // 5. Altura FINAL del ÁREA DEL CLIENTE
            int clientHeight = buttonTop + buttonHeight + margin;

            prompt.ClientSize = new Size(prompt.Width, clientHeight);
            // =========================================================================

            // --- 4. BOTONES (Posicionamiento Corregido usando buttonTop) ---
            int buttonY = buttonTop; // ⬅️ Posición Y

            // Botón NO (Botón de Cancelar a la Derecha)
            Button btnNo = new Button()
            {
                Text = "No",
                Width = 80,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.No,
                //Font = EstilosGlobales.EstiloTitulo // Fuente más gruesa
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnNo);
            btnNo.BackColor = EstilosGlobales.ColorError;
            btnNo.Left = prompt.Width - margin - btnNo.Width;
            btnNo.FlatAppearance.MouseOverBackColor = Color.DarkRed; // Hover de Error
            btnNo.Font = EstilosGlobales.EstiloTitulo;
            // Botón SÍ (Botón de Acción Principal a la Izquierda del NO)
            Button btnYes = new Button()
            {
                Text = "Sí",
                Width = 80,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.Yes,
                //Font = EstilosGlobales.EstiloTitulo // Fuente más gruesa
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnYes);
            btnYes.BackColor= EstilosGlobales.ColorAcento;
            btnYes.Left = btnNo.Left - btnYes.Width - 10;
            btnYes.FlatAppearance.MouseOverBackColor = Color.DarkBlue; // Hover de Acción
            btnYes.Font = EstilosGlobales.EstiloTitulo;
            prompt.Controls.Add(btnYes);
            prompt.Controls.Add(btnNo);
            prompt.AcceptButton = btnYes;
            prompt.CancelButton = btnNo;

            return prompt.ShowDialog();
        }




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

            // Dentro de la sección 1.1 BARRA DE TÍTULO PERSONALIZADA

            // Definimos el ancho del botón de cierre y el margen de la X
            int closeButtonWidth = 40;
            int titleMargin = 10; // Margen izquierdo que ya usas

            // Título
            Label titleLabel = new Label()
            {
                Text = caption,
                ForeColor = Color.White,
                Font = EstilosGlobales.EstiloCampo,
                Location = new Point(titleMargin, (titleBarHeight - EstilosGlobales.EstiloCampo.Height) / 2),

                // CRÍTICO: Definimos el ancho máximo para el título
                // Ancho total del formulario (500) - Margen izquierdo (10) - Ancho de la 'X' (40) - Un margen de seguridad (10)
                Width = prompt.Width - titleMargin - closeButtonWidth - 10,

                // AutoSize debe estar en true o el texto se cortará de todos modos si es más largo que el ancho definido.
                // Usamos AutoSize=false y ajustamos el TextAlign para una mejor apariencia.
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft // Alineamos a la izquierda para títulos
            };
            titleBar.Controls.Add(titleLabel);

            // ... (El código del Botón de Cierre (X) se mantiene igual) ...

            // Botón de Cierre (X)
            Button btnClose = new Button()
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = EstilosGlobales.ColorBoton,
                Width = 40,
                Height = titleBarHeight,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Font = EstilosGlobales.EstiloTitulo
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TextAlign = ContentAlignment.MiddleCenter;
            btnClose.BackColor = EstilosGlobales.ColorAcento;
            btnClose.FlatAppearance.MouseOverBackColor = Color.DarkRed; // Mantener un hover para la X
            btnClose.Click += (s, e) => prompt.Close();
            titleBar.Controls.Add(btnClose);

            // --- Lógica de Arrastre --- (Se mantiene igual)
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

            // --- 💥 CÁLCULO CRÍTICO DE ALTURA (Mantenido) 💥 ---
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
                Text = "Aceptar",
                Width = 120,
                Height = buttonHeight,
                Top = buttonY,
                DialogResult = DialogResult.OK,
                Font = EstilosGlobales.EstiloTitulo
            };
            EstilosGlobales.AplicarEstiloBotonAccion(btnOK);
            // CRÍTICO: Aplicar color de acento al hover
            btnOK.BackColor = EstilosGlobales.ColorAcento;
            btnOK.FlatAppearance.MouseOverBackColor = EstilosGlobales.ColorAdvertencia;

            btnOK.Left = (prompt.Width / 2) - (btnOK.Width / 2);

            prompt.Controls.Add(btnOK);
            prompt.AcceptButton = btnOK;
            prompt.CancelButton = btnOK;

            prompt.ShowDialog();
        }



        //nuevas

        // Estos métodos usan tu método Alerta() que simula un MessageBox.OK
        public static void MostrarExito(string message)
        {
            Alerta(message, "Éxito", IconType.Ok);
        }

        public static void MostrarError(string message)
        {
            Alerta(message, "Error", IconType.Error);
        }

        public static void MostrarAlerta(string message)
        {
            Alerta(message, "Advertencia", IconType.Advertencia);
        }

        // Este es un alias para tu Confirmar, usando la lógica de DialogResult.Yes/No
        public static bool MostrarDialogoConfirmacion(string message)
        {
            // Llama a tu método Confirmar (que a su vez llama a MostrarDialogoConfirmacion con IconType.Pregunta)
            return Confirmar(message, "Confirmar Acción") == DialogResult.Yes;
        }






    }


}
