namespace ClubDeportivo.UI
{
    partial class FrmGestionUsuarios
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
            btnCerrar = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnRegistrar = new Button();
            dgvUsuarios = new DataGridView();
            gbFiltros = new GroupBox();
            cmbRol = new ComboBox();
            txtConfirmarContrasena = new TextBox();
            txtContrasena = new TextBox();
            lblRol = new Label();
            lblContrasena = new Label();
            lblConfirmarContrasena = new Label();
            txtUsuario = new TextBox();
            lblUsuario = new Label();
            lblTituloPrincipal = new Label();
            pnlBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            gbFiltros.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(btnCerrar);
            pnlBase.Controls.Add(btnEliminar);
            pnlBase.Controls.Add(btnEditar);
            pnlBase.Controls.Add(btnRegistrar);
            pnlBase.Controls.Add(dgvUsuarios);
            pnlBase.Controls.Add(gbFiltros);
            pnlBase.Controls.Add(lblTituloPrincipal);
            pnlBase.Location = new Point(7, 8);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(747, 436);
            pnlBase.TabIndex = 0;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(527, 358);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(89, 32);
            btnCerrar.TabIndex = 6;
            btnCerrar.Text = "button5";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(362, 358);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(89, 32);
            btnEliminar.TabIndex = 5;
            btnEliminar.Text = "button4";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(197, 358);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(89, 32);
            btnEditar.TabIndex = 4;
            btnEditar.Text = "button3";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(37, 358);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(89, 32);
            btnRegistrar.TabIndex = 3;
            btnRegistrar.Text = "button2";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(371, 56);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(352, 167);
            dgvUsuarios.TabIndex = 2;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // gbFiltros
            // 
            gbFiltros.Controls.Add(cmbRol);
            gbFiltros.Controls.Add(txtConfirmarContrasena);
            gbFiltros.Controls.Add(txtContrasena);
            gbFiltros.Controls.Add(lblRol);
            gbFiltros.Controls.Add(lblContrasena);
            gbFiltros.Controls.Add(lblConfirmarContrasena);
            gbFiltros.Controls.Add(txtUsuario);
            gbFiltros.Controls.Add(lblUsuario);
            gbFiltros.Location = new Point(18, 56);
            gbFiltros.Name = "gbFiltros";
            gbFiltros.Size = new Size(310, 275);
            gbFiltros.TabIndex = 1;
            gbFiltros.TabStop = false;
            gbFiltros.Text = "groupBox1";
            // 
            // cmbRol
            // 
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(77, 164);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(140, 23);
            cmbRol.TabIndex = 7;
            // 
            // txtConfirmarContrasena
            // 
            txtConfirmarContrasena.Location = new Point(77, 130);
            txtConfirmarContrasena.Name = "txtConfirmarContrasena";
            txtConfirmarContrasena.PasswordChar = '*';
            txtConfirmarContrasena.Size = new Size(138, 23);
            txtConfirmarContrasena.TabIndex = 6;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(77, 84);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(138, 23);
            txtContrasena.TabIndex = 5;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(19, 168);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(38, 15);
            lblRol.TabIndex = 4;
            lblRol.Text = "label1";
            // 
            // lblContrasena
            // 
            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(19, 84);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(38, 15);
            lblContrasena.TabIndex = 3;
            lblContrasena.Text = "label1";
            // 
            // lblConfirmarContrasena
            // 
            lblConfirmarContrasena.AutoSize = true;
            lblConfirmarContrasena.Location = new Point(19, 130);
            lblConfirmarContrasena.Name = "lblConfirmarContrasena";
            lblConfirmarContrasena.Size = new Size(38, 15);
            lblConfirmarContrasena.TabIndex = 2;
            lblConfirmarContrasena.Text = "label1";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(77, 30);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(138, 23);
            txtUsuario.TabIndex = 1;
            txtUsuario.KeyPress += txtUsuario_KeyPress;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(19, 36);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(38, 15);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "label1";
            // 
            // lblTituloPrincipal
            // 
            lblTituloPrincipal.AutoSize = true;
            lblTituloPrincipal.Location = new Point(15, 13);
            lblTituloPrincipal.Name = "lblTituloPrincipal";
            lblTituloPrincipal.Size = new Size(38, 15);
            lblTituloPrincipal.TabIndex = 0;
            lblTituloPrincipal.Text = "label1";
            // 
            // FrmGestionUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmGestionUsuarios";
            Text = "FrmGestionUsuarios";
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            gbFiltros.ResumeLayout(false);
            gbFiltros.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBase;
        private GroupBox gbFiltros;
        private TextBox txtUsuario;
        private Label lblUsuario;
        private Label lblTituloPrincipal;
        private Button btnCerrar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnRegistrar;
        private DataGridView dgvUsuarios;
        private Label lblRol;
        private Label lblContrasena;
        private Label lblConfirmarContrasena;
        private ComboBox cmbRol;
        private TextBox txtConfirmarContrasena;
        private TextBox txtContrasena;
    }
}