namespace ClubDeportivo.UI
{
    partial class FrmLogin
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
            btnIngresar = new Button();
            txtUsuario = new TextBox();
            txtPass = new TextBox();
            pnlLateral = new Panel();
            pbLogoClub = new PictureBox();
            lblTitulo = new Label();
            pbCerrar = new PictureBox();
            pnlLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogoClub).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbCerrar).BeginInit();
            SuspendLayout();
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(356, 161);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(75, 23);
            btnIngresar.TabIndex = 0;
            btnIngresar.Text = "button1";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(258, 75);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(173, 23);
            txtUsuario.TabIndex = 1;
            txtUsuario.TextChanged += txtUsuario_TextChanged;
            txtUsuario.Enter += txtUsuario_Enter;
            txtUsuario.Leave += txtUsuario_Leave;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(258, 122);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(173, 23);
            txtPass.TabIndex = 2;
            txtPass.TextChanged += txtPass_TextChanged;
            txtPass.Enter += txtPass_Enter;
            txtPass.Leave += txtPass_Leave;
            // 
            // pnlLateral
            // 
            pnlLateral.Controls.Add(pbLogoClub);
            pnlLateral.Location = new Point(53, 75);
            pnlLateral.Name = "pnlLateral";
            pnlLateral.Size = new Size(167, 174);
            pnlLateral.TabIndex = 3;
            // 
            // pbLogoClub
            // 
            pbLogoClub.Location = new Point(45, 47);
            pbLogoClub.Name = "pbLogoClub";
            pbLogoClub.Size = new Size(63, 67);
            pbLogoClub.TabIndex = 0;
            pbLogoClub.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(216, 31);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "label1";
            // 
            // pbCerrar
            // 
            pbCerrar.Location = new Point(392, 300);
            pbCerrar.Name = "pbCerrar";
            pbCerrar.Size = new Size(52, 27);
            pbCerrar.TabIndex = 5;
            pbCerrar.TabStop = false;
            pbCerrar.Click += pbCerrar_Click;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(566, 348);
            Controls.Add(pbCerrar);
            Controls.Add(lblTitulo);
            Controls.Add(pnlLateral);
            Controls.Add(txtPass);
            Controls.Add(txtUsuario);
            Controls.Add(btnIngresar);
            Name = "FrmLogin";
            Text = "FormLogin";
            Shown += FormLogin_Shown;
            pnlLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogoClub).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbCerrar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIngresar;
        private TextBox txtUsuario;
        private TextBox txtPass;
        private Panel pnlLateral;
        private Label lblTitulo;
        private PictureBox pbCerrar;
        private PictureBox pbLogoClub;
    }
}