namespace ClubDeportivo.UI
{
    partial class FrmControlCuotas
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
            pnlBase = new Panel();
            rbMorosos = new RadioButton();
            rbVencimientoHoy = new RadioButton();
            btnImprimirMorosos = new Button();
            btnCerrar = new Button();
            btnEstadoCuenta = new Button();
            dgvMorosos = new DataGridView();
            lblTitulo = new Label();
            pnlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMorosos).BeginInit();
            SuspendLayout();
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(rbMorosos);
            pnlBase.Controls.Add(rbVencimientoHoy);
            pnlBase.Controls.Add(btnImprimirMorosos);
            pnlBase.Controls.Add(btnCerrar);
            pnlBase.Controls.Add(btnEstadoCuenta);
            pnlBase.Controls.Add(dgvMorosos);
            pnlBase.Controls.Add(lblTitulo);
            pnlBase.Location = new Point(24, 25);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(748, 401);
            pnlBase.TabIndex = 0;
            // 
            // rbMorosos
            // 
            rbMorosos.AutoSize = true;
            rbMorosos.Location = new Point(226, 63);
            rbMorosos.Name = "rbMorosos";
            rbMorosos.Size = new Size(94, 19);
            rbMorosos.TabIndex = 5;
            rbMorosos.TabStop = true;
            rbMorosos.Text = "radioButton1";
            rbMorosos.UseVisualStyleBackColor = true;
            rbMorosos.CheckedChanged += rbMorosos_CheckedChanged;
            // 
            // rbVencimientoHoy
            // 
            rbVencimientoHoy.AutoSize = true;
            rbVencimientoHoy.Location = new Point(60, 63);
            rbVencimientoHoy.Name = "rbVencimientoHoy";
            rbVencimientoHoy.Size = new Size(94, 19);
            rbVencimientoHoy.TabIndex = 6;
            rbVencimientoHoy.TabStop = true;
            rbVencimientoHoy.Text = "radioButton2";
            rbVencimientoHoy.UseVisualStyleBackColor = true;
            rbVencimientoHoy.CheckedChanged += rbVencimientoHoy_CheckedChanged;
            // 
            // btnImprimirMorosos
            // 
            btnImprimirMorosos.Location = new Point(226, 336);
            btnImprimirMorosos.Name = "btnImprimirMorosos";
            btnImprimirMorosos.Size = new Size(75, 23);
            btnImprimirMorosos.TabIndex = 4;
            btnImprimirMorosos.Text = "button1";
            btnImprimirMorosos.UseVisualStyleBackColor = true;
            btnImprimirMorosos.Click += btnImprimirMorosos_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(476, 336);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 3;
            btnCerrar.Text = "button1";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnEstadoCuenta
            // 
            btnEstadoCuenta.Location = new Point(335, 336);
            btnEstadoCuenta.Name = "btnEstadoCuenta";
            btnEstadoCuenta.Size = new Size(75, 23);
            btnEstadoCuenta.TabIndex = 2;
            btnEstadoCuenta.Text = "button1";
            btnEstadoCuenta.UseVisualStyleBackColor = true;
            btnEstadoCuenta.Click += btnEstadoCuenta_Click;
            // 
            // dgvMorosos
            // 
            dgvMorosos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMorosos.Location = new Point(80, 109);
            dgvMorosos.Name = "dgvMorosos";
            dgvMorosos.Size = new Size(240, 150);
            dgvMorosos.TabIndex = 1;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(60, 33);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "label1";
            // 
            // FrmControlCuotas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmControlCuotas";
            Text = "FrmListadoMorosos";
            Load += FrmListadoMorosos_Load;
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMorosos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBase;
        private Button btnCerrar;
        private Button btnEstadoCuenta;
        private DataGridView dgvMorosos;
        private Label lblTitulo;
        private Button btnImprimirMorosos;
        private RadioButton rbMorosos;
        private RadioButton rbVencimientoHoy;
    }
}