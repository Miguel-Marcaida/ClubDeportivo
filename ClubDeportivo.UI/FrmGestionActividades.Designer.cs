namespace ClubDeportivo.UI
{
    partial class FrmGestionActividades
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
            txtNombreActividad = new TextBox();
            txtCostoDiario = new TextBox();
            btnGuardar = new Button();
            btnEditar = new Button();
            btnCancelar = new Button();
            dgvActividades = new DataGridView();
            pnlBase = new Panel();
            btnEliminar = new Button();
            btnCerrar = new Button();
            gbActividad = new GroupBox();
            txtHorario = new TextBox();
            lblHorario = new Label();
            lblCostoDiario = new Label();
            lblNombre = new Label();
            lblTitulo = new Label();
            pnlListado = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvActividades).BeginInit();
            pnlBase.SuspendLayout();
            gbActividad.SuspendLayout();
            pnlListado.SuspendLayout();
            SuspendLayout();
            // 
            // txtNombreActividad
            // 
            txtNombreActividad.Location = new Point(25, 45);
            txtNombreActividad.Name = "txtNombreActividad";
            txtNombreActividad.Size = new Size(150, 23);
            txtNombreActividad.TabIndex = 0;
            txtNombreActividad.KeyPress += txtNombreActividad_KeyPress;
            // 
            // txtCostoDiario
            // 
            txtCostoDiario.Location = new Point(25, 99);
            txtCostoDiario.Name = "txtCostoDiario";
            txtCostoDiario.Size = new Size(150, 23);
            txtCostoDiario.TabIndex = 1;
            txtCostoDiario.KeyPress += txtCostoDiario_KeyPress;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(25, 286);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "button1";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(44, 354);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "button2";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(122, 286);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "button3";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dgvActividades
            // 
            dgvActividades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvActividades.Location = new Point(61, 27);
            dgvActividades.Name = "dgvActividades";
            dgvActividades.Size = new Size(331, 209);
            dgvActividades.TabIndex = 5;
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(btnEliminar);
            pnlBase.Controls.Add(btnCerrar);
            pnlBase.Controls.Add(gbActividad);
            pnlBase.Controls.Add(lblTitulo);
            pnlBase.Controls.Add(pnlListado);
            pnlBase.Controls.Add(btnEditar);
            pnlBase.Location = new Point(12, 33);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(743, 392);
            pnlBase.TabIndex = 6;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(163, 354);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "button2";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(291, 354);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 9;
            btnCerrar.Text = "button2";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // gbActividad
            // 
            gbActividad.Controls.Add(txtHorario);
            gbActividad.Controls.Add(lblHorario);
            gbActividad.Controls.Add(lblCostoDiario);
            gbActividad.Controls.Add(lblNombre);
            gbActividad.Controls.Add(txtNombreActividad);
            gbActividad.Controls.Add(txtCostoDiario);
            gbActividad.Controls.Add(btnGuardar);
            gbActividad.Controls.Add(btnCancelar);
            gbActividad.Location = new Point(472, 23);
            gbActividad.Name = "gbActividad";
            gbActividad.Size = new Size(252, 334);
            gbActividad.TabIndex = 8;
            gbActividad.TabStop = false;
            gbActividad.Text = "groupBox1";
            // 
            // txtHorario
            // 
            txtHorario.Location = new Point(25, 159);
            txtHorario.Name = "txtHorario";
            txtHorario.Size = new Size(150, 23);
            txtHorario.TabIndex = 8;
            txtHorario.KeyPress += txtHorario_KeyPress;
            // 
            // lblHorario
            // 
            lblHorario.AutoSize = true;
            lblHorario.Location = new Point(25, 141);
            lblHorario.Name = "lblHorario";
            lblHorario.Size = new Size(38, 15);
            lblHorario.TabIndex = 7;
            lblHorario.Text = "label1";
            // 
            // lblCostoDiario
            // 
            lblCostoDiario.AutoSize = true;
            lblCostoDiario.Location = new Point(25, 81);
            lblCostoDiario.Name = "lblCostoDiario";
            lblCostoDiario.Size = new Size(38, 15);
            lblCostoDiario.TabIndex = 6;
            lblCostoDiario.Text = "label2";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(25, 27);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(38, 15);
            lblNombre.TabIndex = 5;
            lblNombre.Text = "label1";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(142, 5);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(38, 15);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "label1";
            // 
            // pnlListado
            // 
            pnlListado.Controls.Add(dgvActividades);
            pnlListado.Location = new Point(21, 23);
            pnlListado.Name = "pnlListado";
            pnlListado.Size = new Size(415, 253);
            pnlListado.TabIndex = 7;
            // 
            // FrmGestionActividades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmGestionActividades";
            Text = "FrmGestionActividades";
            Load += FrmGestionActividades_Load;
            ((System.ComponentModel.ISupportInitialize)dgvActividades).EndInit();
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            gbActividad.ResumeLayout(false);
            gbActividad.PerformLayout();
            pnlListado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtNombreActividad;
        private TextBox txtCostoDiario;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnCancelar;
        private DataGridView dgvActividades;
        private Panel pnlBase;
        private Panel pnlListado;
        private Label lblTitulo;
        private GroupBox gbActividad;
        private Label lblCostoDiario;
        private Label lblNombre;
        private Label lblHorario;
        private TextBox txtHorario;
        private Button btnCerrar;
        private Button btnEliminar;
    }
}