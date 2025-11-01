namespace ClubDeportivo.UI
{
    partial class FrmGestionPersonas
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
            btnImprimirReporte = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            btnBaja = new Button();
            dgvPersonas = new DataGridView();
            gbFiltros = new GroupBox();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            lblTitulo = new Label();
            pnlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonas).BeginInit();
            gbFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(btnImprimirReporte);
            pnlBase.Controls.Add(btnCancelar);
            pnlBase.Controls.Add(btnEditar);
            pnlBase.Controls.Add(btnBaja);
            pnlBase.Controls.Add(dgvPersonas);
            pnlBase.Controls.Add(gbFiltros);
            pnlBase.Controls.Add(lblTitulo);
            pnlBase.Location = new Point(12, 12);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(762, 426);
            pnlBase.TabIndex = 0;
            // 
            // btnImprimirReporte
            // 
            btnImprimirReporte.Location = new Point(394, 362);
            btnImprimirReporte.Name = "btnImprimirReporte";
            btnImprimirReporte.Size = new Size(75, 23);
            btnImprimirReporte.TabIndex = 6;
            btnImprimirReporte.Text = "button1";
            btnImprimirReporte.UseVisualStyleBackColor = true;
            btnImprimirReporte.Click += btnImprimirReporte_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(382, 313);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "button1";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(382, 220);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "button1";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(371, 263);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(75, 23);
            btnBaja.TabIndex = 3;
            btnBaja.Text = "button1";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // dgvPersonas
            // 
            dgvPersonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonas.Location = new Point(65, 206);
            dgvPersonas.Name = "dgvPersonas";
            dgvPersonas.Size = new Size(240, 150);
            dgvPersonas.TabIndex = 2;
            // 
            // gbFiltros
            // 
            gbFiltros.Controls.Add(btnBuscar);
            gbFiltros.Controls.Add(txtBuscar);
            gbFiltros.Controls.Add(lblBuscar);
            gbFiltros.Location = new Point(37, 75);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(488, 100);
            gbFiltros.TabIndex = 1;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "groupBox1";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(270, 45);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "button1";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(114, 38);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            txtBuscar.TabIndex = 3;
            txtBuscar.KeyPress += txtBuscar_KeyPress;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(42, 41);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(38, 15);
            lblBuscar.TabIndex = 2;
            lblBuscar.Text = "label1";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(65, 33);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "label1";
            // 
            // FrmGestionPersonas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmGestionPersonas";
            Text = "FrmGestionPersonas";
            Load += FrmGestionPersonas_Load;
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPersonas).EndInit();
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBase;
        private GroupBox gbFiltros;
        private Label lblTitulo;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnBaja;
        private DataGridView dgvPersonas;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private Label lblBuscar;
        private Button btnImprimirReporte;
    }
}