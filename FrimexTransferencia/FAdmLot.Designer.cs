namespace FrimexTransferencia
{
    partial class FAdmLot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAdmLot));
            this.tBLoteID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bBuscar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dGVSupersacos = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cBOpcionesLote = new System.Windows.Forms.ComboBox();
            this.bRecepcionLote = new System.Windows.Forms.Button();
            this.bExportar = new System.Windows.Forms.Button();
            this.sFDGuardar = new System.Windows.Forms.SaveFileDialog();
            this.pBAvance = new System.Windows.Forms.ProgressBar();
            this.cBOcionConsumoLote = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dTPFechaFumigacion = new System.Windows.Forms.DateTimePicker();
            this.tBInfoMSP = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSupersacos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tBLoteID
            // 
            this.tBLoteID.Location = new System.Drawing.Point(86, 24);
            this.tBLoteID.Name = "tBLoteID";
            this.tBLoteID.Size = new System.Drawing.Size(100, 20);
            this.tBLoteID.TabIndex = 0;
            this.tBLoteID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBLoteID_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar Lote:";
            // 
            // bBuscar
            // 
            this.bBuscar.Location = new System.Drawing.Point(215, 22);
            this.bBuscar.Name = "bBuscar";
            this.bBuscar.Size = new System.Drawing.Size(75, 23);
            this.bBuscar.TabIndex = 2;
            this.bBuscar.Text = "Buscar";
            this.bBuscar.UseVisualStyleBackColor = true;
            this.bBuscar.Click += new System.EventHandler(this.bBuscar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Listado de supersacos";
            // 
            // dGVSupersacos
            // 
            this.dGVSupersacos.AllowUserToAddRows = false;
            this.dGVSupersacos.AllowUserToDeleteRows = false;
            this.dGVSupersacos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVSupersacos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVSupersacos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVSupersacos.Location = new System.Drawing.Point(9, 19);
            this.dGVSupersacos.Name = "dGVSupersacos";
            this.dGVSupersacos.ReadOnly = true;
            this.dGVSupersacos.RowHeadersVisible = false;
            this.dGVSupersacos.Size = new System.Drawing.Size(597, 295);
            this.dGVSupersacos.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Opciones de recepcion de lote";
            // 
            // cBOpcionesLote
            // 
            this.cBOpcionesLote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBOpcionesLote.FormattingEnabled = true;
            this.cBOpcionesLote.Items.AddRange(new object[] {
            "Terminar",
            "Cancelar"});
            this.cBOpcionesLote.Location = new System.Drawing.Point(166, 5);
            this.cBOpcionesLote.Name = "cBOpcionesLote";
            this.cBOpcionesLote.Size = new System.Drawing.Size(119, 21);
            this.cBOpcionesLote.TabIndex = 7;
            // 
            // bRecepcionLote
            // 
            this.bRecepcionLote.Location = new System.Drawing.Point(300, 3);
            this.bRecepcionLote.Name = "bRecepcionLote";
            this.bRecepcionLote.Size = new System.Drawing.Size(97, 23);
            this.bRecepcionLote.TabIndex = 8;
            this.bRecepcionLote.Text = "Aplicar cambios";
            this.bRecepcionLote.UseVisualStyleBackColor = true;
            this.bRecepcionLote.Click += new System.EventHandler(this.bRecepcionLote_Click);
            // 
            // bExportar
            // 
            this.bExportar.Location = new System.Drawing.Point(505, 3);
            this.bExportar.Name = "bExportar";
            this.bExportar.Size = new System.Drawing.Size(103, 30);
            this.bExportar.TabIndex = 9;
            this.bExportar.Text = "Exportar Excel";
            this.bExportar.UseVisualStyleBackColor = true;
            this.bExportar.Click += new System.EventHandler(this.bExportar_Click);
            // 
            // pBAvance
            // 
            this.pBAvance.Location = new System.Drawing.Point(505, 39);
            this.pBAvance.Name = "pBAvance";
            this.pBAvance.Size = new System.Drawing.Size(103, 23);
            this.pBAvance.TabIndex = 10;
            this.pBAvance.Visible = false;
            // 
            // cBOcionConsumoLote
            // 
            this.cBOcionConsumoLote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBOcionConsumoLote.FormattingEnabled = true;
            this.cBOcionConsumoLote.Items.AddRange(new object[] {
            "Disponible",
            "Consumido",
            "Cancelado"});
            this.cBOcionConsumoLote.Location = new System.Drawing.Point(164, 32);
            this.cBOcionConsumoLote.Name = "cBOcionConsumoLote";
            this.cBOcionConsumoLote.Size = new System.Drawing.Size(121, 21);
            this.cBOcionConsumoLote.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Opciones de consumo del lote";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Fecha defumigación";
            // 
            // dTPFechaFumigacion
            // 
            this.dTPFechaFumigacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPFechaFumigacion.Location = new System.Drawing.Point(114, 66);
            this.dTPFechaFumigacion.Name = "dTPFechaFumigacion";
            this.dTPFechaFumigacion.Size = new System.Drawing.Size(171, 20);
            this.dTPFechaFumigacion.TabIndex = 15;
            // 
            // tBInfoMSP
            // 
            this.tBInfoMSP.Location = new System.Drawing.Point(305, 7);
            this.tBInfoMSP.Multiline = true;
            this.tBInfoMSP.Name = "tBInfoMSP";
            this.tBInfoMSP.ReadOnly = true;
            this.tBInfoMSP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBInfoMSP.Size = new System.Drawing.Size(297, 95);
            this.tBInfoMSP.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bBuscar);
            this.panel1.Controls.Add(this.tBInfoMSP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tBLoteID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 111);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dGVSupersacos);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 111);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(620, 317);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dTPFechaFumigacion);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.bExportar);
            this.panel3.Controls.Add(this.cBOcionConsumoLote);
            this.panel3.Controls.Add(this.bRecepcionLote);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.cBOpcionesLote);
            this.panel3.Controls.Add(this.pBAvance);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 428);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(620, 95);
            this.panel3.TabIndex = 19;
            // 
            // FAdmLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 523);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(636, 562);
            this.Name = "FAdmLot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrador de Lotes";
            ((System.ComponentModel.ISupportInitialize)(this.dGVSupersacos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tBLoteID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dGVSupersacos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBOpcionesLote;
        private System.Windows.Forms.Button bRecepcionLote;
        private System.Windows.Forms.Button bExportar;
        private System.Windows.Forms.SaveFileDialog sFDGuardar;
        private System.Windows.Forms.ProgressBar pBAvance;
        private System.Windows.Forms.ComboBox cBOcionConsumoLote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dTPFechaFumigacion;
        private System.Windows.Forms.TextBox tBInfoMSP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}