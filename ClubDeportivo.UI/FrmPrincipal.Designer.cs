namespace ClubDeportivo.UI
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGestionUsuarios = new Button();
            pnlMenu = new Panel();
            btnRegistrarPagos = new Button();
            btnGestionPersona = new Button();
            btnInscripcion = new Button();
            pbLogoMenu = new PictureBox();
            btnCerrarSesion = new Button();
            btnListadoMorosos = new Button();
            btnGestionActividades = new Button();
            pnlContenido = new Panel();
            pnlInferior = new Panel();
            lblUsuarioStatus = new Label();
            pnlSuperior = new Panel();
            pbCerrarPrincipal = new PictureBox();
            lblTituloPrincipal = new Label();
            pnlCentral = new Panel();
            btnConfiguracionGlobal = new Button();
            pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogoMenu).BeginInit();
            pnlInferior.SuspendLayout();
            pnlSuperior.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCerrarPrincipal).BeginInit();
            pnlCentral.SuspendLayout();
            SuspendLayout();
            // 
            // btnGestionUsuarios
            // 
            btnGestionUsuarios.Location = new Point(28, 271);
            btnGestionUsuarios.Name = "btnGestionUsuarios";
            btnGestionUsuarios.Size = new Size(75, 23);
            btnGestionUsuarios.TabIndex = 0;
            btnGestionUsuarios.Text = "button1";
            btnGestionUsuarios.UseVisualStyleBackColor = true;
            btnGestionUsuarios.Click += btnGestionUsuarios_Click;
            // 
            // pnlMenu
            // 
            pnlMenu.Controls.Add(btnConfiguracionGlobal);
            pnlMenu.Controls.Add(btnRegistrarPagos);
            pnlMenu.Controls.Add(btnGestionPersona);
            pnlMenu.Controls.Add(btnInscripcion);
            pnlMenu.Controls.Add(pbLogoMenu);
            pnlMenu.Controls.Add(btnGestionUsuarios);
            pnlMenu.Controls.Add(btnCerrarSesion);
            pnlMenu.Controls.Add(btnListadoMorosos);
            pnlMenu.Controls.Add(btnGestionActividades);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(144, 362);
            pnlMenu.TabIndex = 1;
            // 
            // btnRegistrarPagos
            // 
            btnRegistrarPagos.Location = new Point(28, 157);
            btnRegistrarPagos.Name = "btnRegistrarPagos";
            btnRegistrarPagos.Size = new Size(75, 23);
            btnRegistrarPagos.TabIndex = 2;
            btnRegistrarPagos.Text = "button2";
            btnRegistrarPagos.UseVisualStyleBackColor = true;
            btnRegistrarPagos.Click += btnRegistrarPagos_Click;
            // 
            // btnGestionPersona
            // 
            btnGestionPersona.Location = new Point(28, 97);
            btnGestionPersona.Name = "btnGestionPersona";
            btnGestionPersona.Size = new Size(75, 23);
            btnGestionPersona.TabIndex = 7;
            btnGestionPersona.Text = "button1";
            btnGestionPersona.UseVisualStyleBackColor = true;
            btnGestionPersona.Click += btnGestionPersona_Click;
            // 
            // btnInscripcion
            // 
            btnInscripcion.Location = new Point(28, 128);
            btnInscripcion.Name = "btnInscripcion";
            btnInscripcion.Size = new Size(75, 23);
            btnInscripcion.TabIndex = 1;
            btnInscripcion.Text = "button1";
            btnInscripcion.UseVisualStyleBackColor = true;
            btnInscripcion.Click += btnInscripcion_Click;
            // 
            // pbLogoMenu
            // 
            pbLogoMenu.Location = new Point(28, 41);
            pbLogoMenu.Name = "pbLogoMenu";
            pbLogoMenu.Size = new Size(100, 50);
            pbLogoMenu.TabIndex = 6;
            pbLogoMenu.TabStop = false;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(28, 300);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(75, 23);
            btnCerrarSesion.TabIndex = 5;
            btnCerrarSesion.Text = "button5";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // btnListadoMorosos
            // 
            btnListadoMorosos.Location = new Point(28, 213);
            btnListadoMorosos.Name = "btnListadoMorosos";
            btnListadoMorosos.Size = new Size(75, 23);
            btnListadoMorosos.TabIndex = 4;
            btnListadoMorosos.Text = "button4";
            btnListadoMorosos.UseVisualStyleBackColor = true;
            btnListadoMorosos.Click += btnListadoMorosos_Click;
            // 
            // btnGestionActividades
            // 
            btnGestionActividades.Location = new Point(28, 184);
            btnGestionActividades.Name = "btnGestionActividades";
            btnGestionActividades.Size = new Size(75, 23);
            btnGestionActividades.TabIndex = 3;
            btnGestionActividades.Text = "button3";
            btnGestionActividades.UseVisualStyleBackColor = true;
            btnGestionActividades.Click += btnGestionActividades_Click;
            // 
            // pnlContenido
            // 
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(800, 362);
            pnlContenido.TabIndex = 2;
            // 
            // pnlInferior
            // 
            pnlInferior.Controls.Add(lblUsuarioStatus);
            pnlInferior.Dock = DockStyle.Bottom;
            pnlInferior.Location = new Point(0, 405);
            pnlInferior.Name = "pnlInferior";
            pnlInferior.Size = new Size(800, 45);
            pnlInferior.TabIndex = 3;
            // 
            // lblUsuarioStatus
            // 
            lblUsuarioStatus.AutoSize = true;
            lblUsuarioStatus.Location = new Point(666, 17);
            lblUsuarioStatus.Name = "lblUsuarioStatus";
            lblUsuarioStatus.Size = new Size(38, 15);
            lblUsuarioStatus.TabIndex = 0;
            lblUsuarioStatus.Text = "label1";
            // 
            // pnlSuperior
            // 
            pnlSuperior.Controls.Add(pbCerrarPrincipal);
            pnlSuperior.Controls.Add(lblTituloPrincipal);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(800, 43);
            pnlSuperior.TabIndex = 4;
            // 
            // pbCerrarPrincipal
            // 
            pbCerrarPrincipal.Location = new Point(707, 11);
            pbCerrarPrincipal.Name = "pbCerrarPrincipal";
            pbCerrarPrincipal.Size = new Size(44, 17);
            pbCerrarPrincipal.TabIndex = 1;
            pbCerrarPrincipal.TabStop = false;
            pbCerrarPrincipal.Click += pbCerrarPrincipal_Click;
            // 
            // lblTituloPrincipal
            // 
            lblTituloPrincipal.AutoSize = true;
            lblTituloPrincipal.Location = new Point(180, 13);
            lblTituloPrincipal.Name = "lblTituloPrincipal";
            lblTituloPrincipal.Size = new Size(38, 15);
            lblTituloPrincipal.TabIndex = 0;
            lblTituloPrincipal.Text = "label1";
            // 
            // pnlCentral
            // 
            pnlCentral.Controls.Add(pnlMenu);
            pnlCentral.Controls.Add(pnlContenido);
            pnlCentral.Dock = DockStyle.Fill;
            pnlCentral.Location = new Point(0, 43);
            pnlCentral.Name = "pnlCentral";
            pnlCentral.Size = new Size(800, 362);
            pnlCentral.TabIndex = 5;
            // 
            // btnConfiguracionGlobal
            // 
            btnConfiguracionGlobal.Location = new Point(28, 242);
            btnConfiguracionGlobal.Name = "btnConfiguracionGlobal";
            btnConfiguracionGlobal.Size = new Size(75, 23);
            btnConfiguracionGlobal.TabIndex = 8;
            btnConfiguracionGlobal.Text = "button1";
            btnConfiguracionGlobal.UseVisualStyleBackColor = true;
            btnConfiguracionGlobal.Click += btnConfiguracionGlobal_Click;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlCentral);
            Controls.Add(pnlSuperior);
            Controls.Add(pnlInferior);
            Name = "FrmPrincipal";
            Text = "FormPrincipal";
            FormClosing += FrmPrincipal_FormClosing;
            Load += FrmPrincipal_Load;
            pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogoMenu).EndInit();
            pnlInferior.ResumeLayout(false);
            pnlInferior.PerformLayout();
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbCerrarPrincipal).EndInit();
            pnlCentral.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnGestionUsuarios;
        private Panel pnlMenu;
        private Button btnCerrarSesion;
        private Button btnListadoMorosos;
        private Button btnGestionActividades;
        private Button btnRegistrarPagos;
        private Button btnInscripcion;
        private Panel pnlContenido;
        private PictureBox pbLogoMenu;
        private Panel pnlInferior;
        private Panel pnlSuperior;
        private Panel pnlCentral;
        private Label lblTituloPrincipal;
        private Label lblUsuarioStatus;
        private PictureBox pbCerrarPrincipal;
        private Button btnGestionPersona;
        private Button btnConfiguracionGlobal;
    }
}