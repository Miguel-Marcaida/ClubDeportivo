namespace ClubDeportivo.UI
{
    partial class FrmInscripcionClub
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
            lblTitulo = new Label();
            gbDatosPersona = new GroupBox();
            txtEmail = new TextBox();
            txtTelefono = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            txtDni = new TextBox();
            gbTipoPersona = new GroupBox();
            rbNoSocio = new RadioButton();
            rbSocio = new RadioButton();
            pnlSocioData = new Panel();
            btnPagarYRegistrar = new Button();
            chkFichaMedica = new CheckBox();
            txtNumCarnet = new TextBox();
            pnlNoSocioData = new Panel();
            btnRegistrarAcceso = new Button();
            lblFechaAcceso = new Label();
            btnCancelar = new Button();
            pnlBase = new Panel();
            gbDatosPersona.SuspendLayout();
            gbTipoPersona.SuspendLayout();
            pnlSocioData.SuspendLayout();
            pnlNoSocioData.SuspendLayout();
            pnlBase.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(122, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "label1";
            // 
            // gbDatosPersona
            // 
            gbDatosPersona.Controls.Add(txtEmail);
            gbDatosPersona.Controls.Add(txtTelefono);
            gbDatosPersona.Controls.Add(dtpFechaNacimiento);
            gbDatosPersona.Controls.Add(txtApellido);
            gbDatosPersona.Controls.Add(txtNombre);
            gbDatosPersona.Controls.Add(txtDni);
            gbDatosPersona.Location = new Point(25, 55);
            gbDatosPersona.Name = "gbDatosPersona";
            gbDatosPersona.Size = new Size(283, 327);
            gbDatosPersona.TabIndex = 1;
            gbDatosPersona.TabStop = false;
            gbDatosPersona.Text = "groupBox1";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(39, 237);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 5;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(39, 199);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(100, 23);
            txtTelefono.TabIndex = 4;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(35, 161);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(200, 23);
            dtpFechaNacimiento.TabIndex = 3;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(35, 117);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(100, 23);
            txtApellido.TabIndex = 2;
            txtApellido.KeyPress += txtApellido_KeyPress;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(35, 75);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 1;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // txtDni
            // 
            txtDni.Location = new Point(35, 37);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(100, 23);
            txtDni.TabIndex = 0;
            txtDni.KeyPress += txtDni_KeyPress;
            // 
            // gbTipoPersona
            // 
            gbTipoPersona.Controls.Add(rbNoSocio);
            gbTipoPersona.Controls.Add(rbSocio);
            gbTipoPersona.Location = new Point(355, 66);
            gbTipoPersona.Name = "gbTipoPersona";
            gbTipoPersona.Size = new Size(200, 100);
            gbTipoPersona.TabIndex = 2;
            gbTipoPersona.TabStop = false;
            gbTipoPersona.Text = "groupBox1";
            // 
            // rbNoSocio
            // 
            rbNoSocio.AutoSize = true;
            rbNoSocio.Location = new Point(39, 69);
            rbNoSocio.Name = "rbNoSocio";
            rbNoSocio.Size = new Size(94, 19);
            rbNoSocio.TabIndex = 1;
            rbNoSocio.TabStop = true;
            rbNoSocio.Text = "radioButton2";
            rbNoSocio.UseVisualStyleBackColor = true;
            rbNoSocio.CheckedChanged += rbNoSocio_CheckedChanged;
            // 
            // rbSocio
            // 
            rbSocio.AutoSize = true;
            rbSocio.Location = new Point(34, 37);
            rbSocio.Name = "rbSocio";
            rbSocio.Size = new Size(94, 19);
            rbSocio.TabIndex = 0;
            rbSocio.TabStop = true;
            rbSocio.Text = "radioButton1";
            rbSocio.UseVisualStyleBackColor = true;
            rbSocio.CheckedChanged += rbSocio_CheckedChanged;
            // 
            // pnlSocioData
            // 
            pnlSocioData.Controls.Add(btnPagarYRegistrar);
            pnlSocioData.Controls.Add(chkFichaMedica);
            pnlSocioData.Controls.Add(txtNumCarnet);
            pnlSocioData.Location = new Point(355, 187);
            pnlSocioData.Name = "pnlSocioData";
            pnlSocioData.Size = new Size(207, 175);
            pnlSocioData.TabIndex = 3;
            // 
            // btnPagarYRegistrar
            // 
            btnPagarYRegistrar.Location = new Point(25, 89);
            btnPagarYRegistrar.Name = "btnPagarYRegistrar";
            btnPagarYRegistrar.Size = new Size(75, 23);
            btnPagarYRegistrar.TabIndex = 2;
            btnPagarYRegistrar.Text = "button1";
            btnPagarYRegistrar.UseVisualStyleBackColor = true;
            btnPagarYRegistrar.Click += btnPagarYRegistrar_Click;
            // 
            // chkFichaMedica
            // 
            chkFichaMedica.AutoSize = true;
            chkFichaMedica.Location = new Point(25, 64);
            chkFichaMedica.Name = "chkFichaMedica";
            chkFichaMedica.Size = new Size(83, 19);
            chkFichaMedica.TabIndex = 1;
            chkFichaMedica.Text = "checkBox1";
            chkFichaMedica.UseVisualStyleBackColor = true;
            // 
            // txtNumCarnet
            // 
            txtNumCarnet.Location = new Point(15, 21);
            txtNumCarnet.Name = "txtNumCarnet";
            txtNumCarnet.Size = new Size(100, 23);
            txtNumCarnet.TabIndex = 0;
            // 
            // pnlNoSocioData
            // 
            pnlNoSocioData.Controls.Add(btnRegistrarAcceso);
            pnlNoSocioData.Controls.Add(lblFechaAcceso);
            pnlNoSocioData.Location = new Point(568, 187);
            pnlNoSocioData.Name = "pnlNoSocioData";
            pnlNoSocioData.Size = new Size(148, 128);
            pnlNoSocioData.TabIndex = 4;
            // 
            // btnRegistrarAcceso
            // 
            btnRegistrarAcceso.Location = new Point(36, 71);
            btnRegistrarAcceso.Name = "btnRegistrarAcceso";
            btnRegistrarAcceso.Size = new Size(75, 23);
            btnRegistrarAcceso.TabIndex = 1;
            btnRegistrarAcceso.Text = "button1";
            btnRegistrarAcceso.UseVisualStyleBackColor = true;
            btnRegistrarAcceso.Click += btnRegistrarAcceso_Click;
            // 
            // lblFechaAcceso
            // 
            lblFechaAcceso.AutoSize = true;
            lblFechaAcceso.Location = new Point(33, 24);
            lblFechaAcceso.Name = "lblFechaAcceso";
            lblFechaAcceso.Size = new Size(38, 15);
            lblFechaAcceso.TabIndex = 0;
            lblFechaAcceso.Text = "label1";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(413, 379);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "button1";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(lblTitulo);
            pnlBase.Controls.Add(gbDatosPersona);
            pnlBase.Controls.Add(gbTipoPersona);
            pnlBase.Controls.Add(pnlSocioData);
            pnlBase.Controls.Add(btnCancelar);
            pnlBase.Controls.Add(pnlNoSocioData);
            pnlBase.Location = new Point(12, 9);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(776, 429);
            pnlBase.TabIndex = 6;
            // 
            // FrmInscripcionClub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmInscripcionClub";
            Text = "FrmGestionPersonas";
            Load += FrmInscripcionClub_Load;
            gbDatosPersona.ResumeLayout(false);
            gbDatosPersona.PerformLayout();
            gbTipoPersona.ResumeLayout(false);
            gbTipoPersona.PerformLayout();
            pnlSocioData.ResumeLayout(false);
            pnlSocioData.PerformLayout();
            pnlNoSocioData.ResumeLayout(false);
            pnlNoSocioData.PerformLayout();
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private GroupBox gbDatosPersona;
        private TextBox txtEmail;
        private TextBox txtTelefono;
        private DateTimePicker dtpFechaNacimiento;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private TextBox txtDni;
        private GroupBox gbTipoPersona;
        private RadioButton rbNoSocio;
        private RadioButton rbSocio;
        private Panel pnlSocioData;
        private Panel pnlNoSocioData;
        private CheckBox chkFichaMedica;
        private TextBox txtNumCarnet;
        private Button btnPagarYRegistrar;
        private Label lblFechaAcceso;
        private Button btnRegistrarAcceso;
        private Button btnCancelar;
        private Panel pnlBase;
    }
}