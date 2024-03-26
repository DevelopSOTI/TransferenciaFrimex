namespace FrimexTransferencia
{
    partial class FRegEmbImp
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
            this.label1 = new System.Windows.Forms.Label();
            this.cBContrato = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBTolva = new System.Windows.Forms.TextBox();
            this.tBTrasporte = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msOpciones = new System.Windows.Forms.MenuStrip();
            this.contratosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturarContratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completarContratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelarContratoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tBSellos = new System.Windows.Forms.TextBox();
            this.tBPesoProducto = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bAceptar = new System.Windows.Forms.Button();
            this.bCancelar = new System.Windows.Forms.Button();
            this.bContrato = new System.Windows.Forms.Button();
            this.bSeleccionarContrato = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dTPFechaAproxLlegada = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.msOpciones.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contrato";
            // 
            // cBContrato
            // 
            this.cBContrato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBContrato.FormattingEnabled = true;
            this.cBContrato.Location = new System.Drawing.Point(85, 35);
            this.cBContrato.Name = "cBContrato";
            this.cBContrato.Size = new System.Drawing.Size(130, 21);
            this.cBContrato.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vagón/Furgón";
            // 
            // tBTolva
            // 
            this.tBTolva.Enabled = false;
            this.tBTolva.Location = new System.Drawing.Point(91, 31);
            this.tBTolva.Name = "tBTolva";
            this.tBTolva.Size = new System.Drawing.Size(245, 20);
            this.tBTolva.TabIndex = 4;
            // 
            // tBTrasporte
            // 
            this.tBTrasporte.Enabled = false;
            this.tBTrasporte.Location = new System.Drawing.Point(91, 70);
            this.tBTrasporte.Name = "tBTrasporte";
            this.tBTrasporte.Size = new System.Drawing.Size(245, 20);
            this.tBTrasporte.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Empresa \r\nTransportadora";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Producto";
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.Enabled = false;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(91, 113);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(245, 21);
            this.cBProducto.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBProducto);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tBTrasporte);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tBTolva);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 151);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de trasporte";
            // 
            // msOpciones
            // 
            this.msOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contratosToolStripMenuItem});
            this.msOpciones.Location = new System.Drawing.Point(0, 0);
            this.msOpciones.Name = "msOpciones";
            this.msOpciones.Size = new System.Drawing.Size(368, 24);
            this.msOpciones.TabIndex = 10;
            this.msOpciones.Text = "mSOpciones";
            // 
            // contratosToolStripMenuItem
            // 
            this.contratosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.capturarContratoToolStripMenuItem,
            this.completarContratoToolStripMenuItem,
            this.cancelarContratoToolStripMenuItem});
            this.contratosToolStripMenuItem.Name = "contratosToolStripMenuItem";
            this.contratosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.contratosToolStripMenuItem.Text = "Contratos";
            // 
            // capturarContratoToolStripMenuItem
            // 
            this.capturarContratoToolStripMenuItem.Name = "capturarContratoToolStripMenuItem";
            this.capturarContratoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.capturarContratoToolStripMenuItem.Text = "Capturar Contrato";
            this.capturarContratoToolStripMenuItem.Click += new System.EventHandler(this.capturarContratoToolStripMenuItem_Click);
            // 
            // completarContratoToolStripMenuItem
            // 
            this.completarContratoToolStripMenuItem.Name = "completarContratoToolStripMenuItem";
            this.completarContratoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.completarContratoToolStripMenuItem.Text = "Completar Contrato";
            this.completarContratoToolStripMenuItem.Click += new System.EventHandler(this.completarContratoToolStripMenuItem_Click);
            // 
            // cancelarContratoToolStripMenuItem
            // 
            this.cancelarContratoToolStripMenuItem.Name = "cancelarContratoToolStripMenuItem";
            this.cancelarContratoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cancelarContratoToolStripMenuItem.Text = "Cancelar Contrato";
            this.cancelarContratoToolStripMenuItem.Click += new System.EventHandler(this.cancelarContratoToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Peso Producto";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sellos";
            // 
            // tBSellos
            // 
            this.tBSellos.Enabled = false;
            this.tBSellos.Location = new System.Drawing.Point(94, 62);
            this.tBSellos.Name = "tBSellos";
            this.tBSellos.Size = new System.Drawing.Size(245, 20);
            this.tBSellos.TabIndex = 10;
            // 
            // tBPesoProducto
            // 
            this.tBPesoProducto.Enabled = false;
            this.tBPesoProducto.Location = new System.Drawing.Point(94, 23);
            this.tBPesoProducto.Name = "tBPesoProducto";
            this.tBPesoProducto.Size = new System.Drawing.Size(245, 20);
            this.tBPesoProducto.TabIndex = 9;
            this.tBPesoProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBPesoProducto_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dTPFechaAproxLlegada);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tBSellos);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tBPesoProducto);
            this.groupBox2.Location = new System.Drawing.Point(12, 226);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 117);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Vagón";
            // 
            // bAceptar
            // 
            this.bAceptar.Location = new System.Drawing.Point(21, 349);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(107, 33);
            this.bAceptar.TabIndex = 14;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
            // 
            // bCancelar
            // 
            this.bCancelar.Location = new System.Drawing.Point(249, 349);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(107, 33);
            this.bCancelar.TabIndex = 15;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // bContrato
            // 
            this.bContrato.Location = new System.Drawing.Point(221, 33);
            this.bContrato.Name = "bContrato";
            this.bContrato.Size = new System.Drawing.Size(75, 23);
            this.bContrato.TabIndex = 16;
            this.bContrato.Text = "button3";
            this.bContrato.UseVisualStyleBackColor = true;
            this.bContrato.Visible = false;
            this.bContrato.Click += new System.EventHandler(this.bContrato_Click);
            // 
            // bSeleccionarContrato
            // 
            this.bSeleccionarContrato.Location = new System.Drawing.Point(221, 33);
            this.bSeleccionarContrato.Name = "bSeleccionarContrato";
            this.bSeleccionarContrato.Size = new System.Drawing.Size(138, 23);
            this.bSeleccionarContrato.TabIndex = 17;
            this.bSeleccionarContrato.Text = "Seleccionar Contrato";
            this.bSeleccionarContrato.UseVisualStyleBackColor = true;
            this.bSeleccionarContrato.Click += new System.EventHandler(this.bSeleccionarContrato_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Fecha tentativa de llegada";
            // 
            // dTPFechaAproxLlegada
            // 
            this.dTPFechaAproxLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPFechaAproxLlegada.Location = new System.Drawing.Point(173, 88);
            this.dTPFechaAproxLlegada.Name = "dTPFechaAproxLlegada";
            this.dTPFechaAproxLlegada.Size = new System.Drawing.Size(166, 20);
            this.dTPFechaAproxLlegada.TabIndex = 14;
            // 
            // FRegEmbImp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 388);
            this.Controls.Add(this.bSeleccionarContrato);
            this.Controls.Add(this.bContrato);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.bAceptar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cBContrato);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.msOpciones);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.msOpciones;
            this.Name = "FRegEmbImp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de embarque de importación";
            this.Load += new System.EventHandler(this.FRegEmbImp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.msOpciones.ResumeLayout(false);
            this.msOpciones.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBContrato;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBTolva;
        private System.Windows.Forms.TextBox tBTrasporte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip msOpciones;
        private System.Windows.Forms.ToolStripMenuItem contratosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capturarContratoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completarContratoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelarContratoToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBSellos;
        private System.Windows.Forms.TextBox tBPesoProducto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bAceptar;
        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button bContrato;
        private System.Windows.Forms.Button bSeleccionarContrato;
        private System.Windows.Forms.DateTimePicker dTPFechaAproxLlegada;
        private System.Windows.Forms.Label label7;
    }
}