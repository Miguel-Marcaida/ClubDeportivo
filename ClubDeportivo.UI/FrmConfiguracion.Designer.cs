namespace ClubDeportivo.UI
{
    partial class FrmConfiguracion
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
            lblTitulo = new Label();
            gbCampos = new GroupBox();
            txtDescripcion = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            txtValor = new TextBox();
            txtClave = new TextBox();
            lblDescripcion = new Label();
            lblValor = new Label();
            lblClave = new Label();
            dgvConfiguraciones = new DataGridView();
            pnlBase.SuspendLayout();
            gbCampos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfiguraciones).BeginInit();
            SuspendLayout();
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(lblTitulo);
            pnlBase.Controls.Add(gbCampos);
            pnlBase.Controls.Add(dgvConfiguraciones);
            pnlBase.Location = new Point(33, 27);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(738, 411);
            pnlBase.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(107, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 5;
            lblTitulo.Text = "label1";
            // 
            // gbCampos
            // 
            gbCampos.Controls.Add(txtDescripcion);
            gbCampos.Controls.Add(btnNuevo);
            gbCampos.Controls.Add(btnGuardar);
            gbCampos.Controls.Add(btnEliminar);
            gbCampos.Controls.Add(txtValor);
            gbCampos.Controls.Add(txtClave);
            gbCampos.Controls.Add(lblDescripcion);
            gbCampos.Controls.Add(lblValor);
            gbCampos.Controls.Add(lblClave);
            gbCampos.Location = new Point(376, 42);
            gbCampos.Name = "gbCampos";
            gbCampos.Size = new Size(304, 284);
            gbCampos.TabIndex = 1;
            gbCampos.TabStop = false;
            gbCampos.Text = "groupBox1";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(41, 219);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(100, 23);
            txtDescripcion.TabIndex = 5;
            txtDescripcion.KeyPress += txtDescripcion_KeyPress;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(169, 137);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(75, 23);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "button1";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(169, 201);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "button2";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(169, 241);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(114, 23);
            btnEliminar.TabIndex = 4;
            btnEliminar.Text = "ad";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // txtValor
            // 
            txtValor.Location = new Point(41, 137);
            txtValor.Name = "txtValor";
            txtValor.Size = new Size(100, 23);
            txtValor.TabIndex = 4;
            txtValor.KeyPress += txtValor_KeyPress;
            // 
            // txtClave
            // 
            txtClave.Location = new Point(41, 66);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(100, 23);
            txtClave.TabIndex = 3;
            txtClave.KeyPress += txtClave_KeyPress;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(23, 180);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(38, 15);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "label3";
            // 
            // lblValor
            // 
            lblValor.AutoSize = true;
            lblValor.Location = new Point(23, 107);
            lblValor.Name = "lblValor";
            lblValor.Size = new Size(38, 15);
            lblValor.TabIndex = 1;
            lblValor.Text = "label2";
            // 
            // lblClave
            // 
            lblClave.AutoSize = true;
            lblClave.Location = new Point(23, 35);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(38, 15);
            lblClave.TabIndex = 0;
            lblClave.Text = "label1";
            // 
            // dgvConfiguraciones
            // 
            dgvConfiguraciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConfiguraciones.Location = new Point(87, 52);
            dgvConfiguraciones.Name = "dgvConfiguraciones";
            dgvConfiguraciones.Size = new Size(240, 150);
            dgvConfiguraciones.TabIndex = 0;
            dgvConfiguraciones.CellClick += dgvConfiguraciones_CellClick;
            // 
            // FrmConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmConfiguracion";
            Text = "FrmConfiguracion";
            Load += FrmConfiguracion_Load;
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            gbCampos.ResumeLayout(false);
            gbCampos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvConfiguraciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBase;
        private GroupBox gbCampos;
        private DataGridView dgvConfiguraciones;
        private Label lblDescripcion;
        private Label lblValor;
        private Label lblClave;
        private TextBox txtDescripcion;
        private TextBox txtValor;
        private TextBox txtClave;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
        private Label lblTitulo;
    }
}