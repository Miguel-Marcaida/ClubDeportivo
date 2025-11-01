
namespace ClubDeportivo.UI
{
    partial class FrmRegistrarPagos
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
            components = new System.ComponentModel.Container();
            lblTituloPagos = new Label();
            pnlBase = new Panel();
            btnReimprimirCarnet = new Button();
            btnCerrar = new Button();
            btnLimpiar = new Button();
            gbDatosPago = new GroupBox();
            lblActividades = new Label();
            cmbActividades = new ComboBox();
            lblFormaPago = new Label();
            cmbMedioPago = new ComboBox();
            lblMonto = new Label();
            txtMonto = new TextBox();
            lblConcepto = new Label();
            cmbConcepto = new ComboBox();
            gbInfoPersona = new GroupBox();
            lblEstadoMembresia = new Label();
            lblDni = new Label();
            lblTipoSocio = new Label();
            lblNombrePersona = new Label();
            lblMesesAtraso = new Label();
            lblFechaVencimiento = new Label();
            lblFechaUltimoPago = new Label();
            pnlBusqueda = new Panel();
            btnBuscar = new Button();
            txtBuscarIdentificador = new TextBox();
            lblBuscar = new Label();
            btnRegistrarPago = new Button();
            errorProvider1 = new ErrorProvider(components);
            pnlBase.SuspendLayout();
            gbDatosPago.SuspendLayout();
            gbInfoPersona.SuspendLayout();
            pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // lblTituloPagos
            // 
            lblTituloPagos.AutoSize = true;
            lblTituloPagos.Location = new Point(93, 17);
            lblTituloPagos.Name = "lblTituloPagos";
            lblTituloPagos.Size = new Size(38, 15);
            lblTituloPagos.TabIndex = 0;
            lblTituloPagos.Text = "label1";
            // 
            // pnlBase
            // 
            pnlBase.Controls.Add(btnReimprimirCarnet);
            pnlBase.Controls.Add(btnCerrar);
            pnlBase.Controls.Add(btnLimpiar);
            pnlBase.Controls.Add(gbDatosPago);
            pnlBase.Controls.Add(gbInfoPersona);
            pnlBase.Controls.Add(pnlBusqueda);
            pnlBase.Controls.Add(btnRegistrarPago);
            pnlBase.Controls.Add(lblTituloPagos);
            pnlBase.Location = new Point(12, 12);
            pnlBase.Name = "pnlBase";
            pnlBase.Size = new Size(762, 414);
            pnlBase.TabIndex = 1;
            // 
            // btnReimprimirCarnet
            // 
            btnReimprimirCarnet.Location = new Point(345, 372);
            btnReimprimirCarnet.Name = "btnReimprimirCarnet";
            btnReimprimirCarnet.Size = new Size(75, 23);
            btnReimprimirCarnet.TabIndex = 20;
            btnReimprimirCarnet.Text = "button1";
            btnReimprimirCarnet.UseVisualStyleBackColor = true;
            btnReimprimirCarnet.Click += btnReimprimirCarnet_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(433, 372);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 19;
            btnCerrar.Text = "button1";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(532, 372);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 18;
            btnLimpiar.Text = "button1";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // gbDatosPago
            // 
            gbDatosPago.Controls.Add(lblActividades);
            gbDatosPago.Controls.Add(cmbActividades);
            gbDatosPago.Controls.Add(lblFormaPago);
            gbDatosPago.Controls.Add(cmbMedioPago);
            gbDatosPago.Controls.Add(lblMonto);
            gbDatosPago.Controls.Add(txtMonto);
            gbDatosPago.Controls.Add(lblConcepto);
            gbDatosPago.Controls.Add(cmbConcepto);
            gbDatosPago.Location = new Point(288, 130);
            gbDatosPago.Name = "gbDatosPago";
            gbDatosPago.Size = new Size(445, 148);
            gbDatosPago.TabIndex = 17;
            gbDatosPago.TabStop = false;
            gbDatosPago.Text = "groupBox1";
            // 
            // lblActividades
            // 
            lblActividades.AutoSize = true;
            lblActividades.Location = new Point(244, 90);
            lblActividades.Name = "lblActividades";
            lblActividades.Size = new Size(38, 15);
            lblActividades.TabIndex = 17;
            lblActividades.Text = "label1";
            // 
            // cmbActividades
            // 
            cmbActividades.FormattingEnabled = true;
            cmbActividades.Location = new Point(307, 83);
            cmbActividades.Name = "cmbActividades";
            cmbActividades.Size = new Size(121, 23);
            cmbActividades.TabIndex = 16;
            cmbActividades.SelectedIndexChanged += cmbActividades_SelectedIndexChanged;
            // 
            // lblFormaPago
            // 
            lblFormaPago.AutoSize = true;
            lblFormaPago.Location = new Point(30, 71);
            lblFormaPago.Name = "lblFormaPago";
            lblFormaPago.Size = new Size(39, 15);
            lblFormaPago.TabIndex = 15;
            lblFormaPago.Text = "forma";
            // 
            // cmbMedioPago
            // 
            cmbMedioPago.FormattingEnabled = true;
            cmbMedioPago.Location = new Point(89, 71);
            cmbMedioPago.Name = "cmbMedioPago";
            cmbMedioPago.Size = new Size(121, 23);
            cmbMedioPago.TabIndex = 14;
            cmbMedioPago.SelectedIndexChanged += cmbMedioPago_SelectedIndexChanged;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(16, 39);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(38, 15);
            lblMonto.TabIndex = 13;
            lblMonto.Text = "label2";
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(89, 31);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(100, 23);
            txtMonto.TabIndex = 6;
            // 
            // lblConcepto
            // 
            lblConcepto.AutoSize = true;
            lblConcepto.Location = new Point(226, 37);
            lblConcepto.Name = "lblConcepto";
            lblConcepto.Size = new Size(38, 15);
            lblConcepto.TabIndex = 12;
            lblConcepto.Text = "label2";
            // 
            // cmbConcepto
            // 
            cmbConcepto.FormattingEnabled = true;
            cmbConcepto.Location = new Point(293, 34);
            cmbConcepto.Name = "cmbConcepto";
            cmbConcepto.Size = new Size(121, 23);
            cmbConcepto.TabIndex = 7;
            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
            // 
            // gbInfoPersona
            // 
            gbInfoPersona.Controls.Add(lblEstadoMembresia);
            gbInfoPersona.Controls.Add(lblDni);
            gbInfoPersona.Controls.Add(lblTipoSocio);
            gbInfoPersona.Controls.Add(lblNombrePersona);
            gbInfoPersona.Controls.Add(lblMesesAtraso);
            gbInfoPersona.Controls.Add(lblFechaVencimiento);
            gbInfoPersona.Controls.Add(lblFechaUltimoPago);
            gbInfoPersona.Location = new Point(24, 47);
            gbInfoPersona.Name = "gbInfoPersona";
            gbInfoPersona.Size = new Size(201, 218);
            gbInfoPersona.TabIndex = 16;
            gbInfoPersona.TabStop = false;
            gbInfoPersona.Text = "groupBox1";
            // 
            // lblEstadoMembresia
            // 
            lblEstadoMembresia.AutoSize = true;
            lblEstadoMembresia.Location = new Point(107, 138);
            lblEstadoMembresia.Name = "lblEstadoMembresia";
            lblEstadoMembresia.Size = new Size(38, 15);
            lblEstadoMembresia.TabIndex = 17;
            lblEstadoMembresia.Text = "label1";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(117, 91);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(38, 15);
            lblDni.TabIndex = 16;
            lblDni.Text = "label1";
            // 
            // lblTipoSocio
            // 
            lblTipoSocio.AutoSize = true;
            lblTipoSocio.Location = new Point(20, 58);
            lblTipoSocio.Name = "lblTipoSocio";
            lblTipoSocio.Size = new Size(38, 15);
            lblTipoSocio.TabIndex = 15;
            lblTipoSocio.Text = "label1";
            // 
            // lblNombrePersona
            // 
            lblNombrePersona.AutoSize = true;
            lblNombrePersona.Location = new Point(19, 36);
            lblNombrePersona.Name = "lblNombrePersona";
            lblNombrePersona.Size = new Size(38, 15);
            lblNombrePersona.TabIndex = 14;
            lblNombrePersona.Text = "label1";
            // 
            // lblMesesAtraso
            // 
            lblMesesAtraso.AutoSize = true;
            lblMesesAtraso.Location = new Point(117, 46);
            lblMesesAtraso.Name = "lblMesesAtraso";
            lblMesesAtraso.Size = new Size(38, 15);
            lblMesesAtraso.TabIndex = 13;
            lblMesesAtraso.Text = "label1";
            // 
            // lblFechaVencimiento
            // 
            lblFechaVencimiento.AutoSize = true;
            lblFechaVencimiento.Location = new Point(20, 91);
            lblFechaVencimiento.Name = "lblFechaVencimiento";
            lblFechaVencimiento.Size = new Size(38, 15);
            lblFechaVencimiento.TabIndex = 12;
            lblFechaVencimiento.Text = "label1";
            // 
            // lblFechaUltimoPago
            // 
            lblFechaUltimoPago.AutoSize = true;
            lblFechaUltimoPago.Location = new Point(20, 138);
            lblFechaUltimoPago.Name = "lblFechaUltimoPago";
            lblFechaUltimoPago.Size = new Size(38, 15);
            lblFechaUltimoPago.TabIndex = 5;
            lblFechaUltimoPago.Text = "label2";
            // 
            // pnlBusqueda
            // 
            pnlBusqueda.Controls.Add(btnBuscar);
            pnlBusqueda.Controls.Add(txtBuscarIdentificador);
            pnlBusqueda.Controls.Add(lblBuscar);
            pnlBusqueda.Location = new Point(288, 48);
            pnlBusqueda.Name = "pnlBusqueda";
            pnlBusqueda.Size = new Size(440, 60);
            pnlBusqueda.TabIndex = 14;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(302, 18);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "btnBuscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscarIdentificador
            // 
            txtBuscarIdentificador.Location = new Point(115, 18);
            txtBuscarIdentificador.Name = "txtBuscarIdentificador";
            txtBuscarIdentificador.Size = new Size(165, 23);
            txtBuscarIdentificador.TabIndex = 10;
            txtBuscarIdentificador.KeyPress += txtBuscarIdentificador_KeyPress;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(57, 22);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(38, 15);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "label2";
            // 
            // btnRegistrarPago
            // 
            btnRegistrarPago.Location = new Point(636, 372);
            btnRegistrarPago.Name = "btnRegistrarPago";
            btnRegistrarPago.Size = new Size(75, 23);
            btnRegistrarPago.TabIndex = 8;
            btnRegistrarPago.Text = "button2";
            btnRegistrarPago.UseVisualStyleBackColor = true;
            btnRegistrarPago.Click += btnRegistrarPago_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // FrmRegistrarPagos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlBase);
            Name = "FrmRegistrarPagos";
            Text = "FrmGestionSocios";
            Load += FrmRegistrarPagos_Load;
            pnlBase.ResumeLayout(false);
            pnlBase.PerformLayout();
            gbDatosPago.ResumeLayout(false);
            gbDatosPago.PerformLayout();
            gbInfoPersona.ResumeLayout(false);
            gbInfoPersona.PerformLayout();
            pnlBusqueda.ResumeLayout(false);
            pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }



        #endregion

        private Label lblTituloPagos;
        private Panel pnlBase;
        private Label lblBuscar;
        private Button btnRegistrarPago;
        private ComboBox cmbConcepto;
        private TextBox txtMonto;
        private Label lblFechaUltimoPago;
        private Button btnBuscar;
        private TextBox txtBuscarIdentificador;
        private Label lblConcepto;
        private Label lblMonto;
        private Panel pnlBusqueda;
        private GroupBox gbInfoPersona;
        private GroupBox gbDatosPago;
        private ComboBox cmbMedioPago;
        private Label lblFormaPago;
        private Label lblDni;
        private Label lblMesesAtraso;
        private Label lblFechaVencimiento;
        private Label lblTipoSocio;
        private Label lblNombrePersona;
        private ErrorProvider errorProvider1;
        private Label lblEstadoMembresia;
        private Label lblActividades;
        private ComboBox cmbActividades;
        private Button btnReimprimirCarnet;
        private Button btnCerrar;
        private Button btnLimpiar;
    }
}