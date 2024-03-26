namespace FrimexTransferencia
{
    partial class FRep
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
            this.bGenerar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dTPFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dTPInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cBSeleccionReporte = new System.Windows.Forms.ComboBox();
            this.sFDGuardar = new System.Windows.Forms.SaveFileDialog();
            this.dTPFechaFinHora = new System.Windows.Forms.DateTimePicker();
            this.dTPInicioHora = new System.Windows.Forms.DateTimePicker();
            this.lProgressBar = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.cBAlmacenes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bGenerar
            // 
            this.bGenerar.Location = new System.Drawing.Point(194, 97);
            this.bGenerar.Name = "bGenerar";
            this.bGenerar.Size = new System.Drawing.Size(75, 23);
            this.bGenerar.TabIndex = 17;
            this.bGenerar.Text = "Generar";
            this.bGenerar.UseVisualStyleBackColor = true;
            this.bGenerar.Click += new System.EventHandler(this.bGenerar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Fecha final:";
            // 
            // dTPFin
            // 
            this.dTPFin.CustomFormat = "dd/MM/yyyy";
            this.dTPFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPFin.Location = new System.Drawing.Point(357, 44);
            this.dTPFin.Name = "dTPFin";
            this.dTPFin.Size = new System.Drawing.Size(87, 20);
            this.dTPFin.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Fecha inicio:";
            // 
            // dTPInicio
            // 
            this.dTPInicio.CustomFormat = "dd/MM/yyyy";
            this.dTPInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPInicio.Location = new System.Drawing.Point(125, 44);
            this.dTPInicio.Name = "dTPInicio";
            this.dTPInicio.Size = new System.Drawing.Size(87, 20);
            this.dTPInicio.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Seleccionar Reporte:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cBSeleccionReporte
            // 
            this.cBSeleccionReporte.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cBSeleccionReporte.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cBSeleccionReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBSeleccionReporte.FormattingEnabled = true;
            this.cBSeleccionReporte.Items.AddRange(new object[] {
            "Reporte de existencia de supersacos en fecha especifica",
            "Reporte de pesos de supersacos",
            "Reporte de transferencias detallado",
            "Reporte diferencias de pesos de supersaco",
            "Reporte diferencias de pesos de supersacos por transferencias",
            "Reporte Supersacos disponibles por ubicacion",
            "Reporte tranferencias por rango de fechas"});
            this.cBSeleccionReporte.Location = new System.Drawing.Point(125, 12);
            this.cBSeleccionReporte.Name = "cBSeleccionReporte";
            this.cBSeleccionReporte.Size = new System.Drawing.Size(319, 21);
            this.cBSeleccionReporte.Sorted = true;
            this.cBSeleccionReporte.TabIndex = 11;
            this.cBSeleccionReporte.SelectedIndexChanged += new System.EventHandler(this.cBSeleccionReporte_SelectedIndexChanged);
            // 
            // dTPFechaFinHora
            // 
            this.dTPFechaFinHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dTPFechaFinHora.Location = new System.Drawing.Point(357, 70);
            this.dTPFechaFinHora.Name = "dTPFechaFinHora";
            this.dTPFechaFinHora.ShowUpDown = true;
            this.dTPFechaFinHora.Size = new System.Drawing.Size(87, 20);
            this.dTPFechaFinHora.TabIndex = 21;
            this.dTPFechaFinHora.Value = new System.DateTime(2020, 9, 15, 7, 0, 0, 0);
            // 
            // dTPInicioHora
            // 
            this.dTPInicioHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dTPInicioHora.Location = new System.Drawing.Point(125, 70);
            this.dTPInicioHora.Name = "dTPInicioHora";
            this.dTPInicioHora.ShowUpDown = true;
            this.dTPInicioHora.Size = new System.Drawing.Size(87, 20);
            this.dTPInicioHora.TabIndex = 20;
            this.dTPInicioHora.Value = new System.DateTime(2020, 9, 15, 7, 0, 0, 0);
            // 
            // lProgressBar
            // 
            this.lProgressBar.AutoSize = true;
            this.lProgressBar.Location = new System.Drawing.Point(286, 94);
            this.lProgressBar.Name = "lProgressBar";
            this.lProgressBar.Size = new System.Drawing.Size(0, 13);
            this.lProgressBar.TabIndex = 19;
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb.Location = new System.Drawing.Point(0, 126);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(467, 10);
            this.pb.TabIndex = 18;
            this.pb.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(251, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Almacen:";
            this.label4.Visible = false;
            // 
            // cBAlmacenes
            // 
            this.cBAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacenes.FormattingEnabled = true;
            this.cBAlmacenes.Location = new System.Drawing.Point(308, 42);
            this.cBAlmacenes.Name = "cBAlmacenes";
            this.cBAlmacenes.Size = new System.Drawing.Size(136, 21);
            this.cBAlmacenes.TabIndex = 23;
            this.cBAlmacenes.Visible = false;
            // 
            // FRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 136);
            this.Controls.Add(this.cBAlmacenes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bGenerar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dTPFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTPInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBSeleccionReporte);
            this.Controls.Add(this.dTPFechaFinHora);
            this.Controls.Add(this.dTPInicioHora);
            this.Controls.Add(this.lProgressBar);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FRep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.FRep_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bGenerar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dTPFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTPInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBSeleccionReporte;
        private System.Windows.Forms.SaveFileDialog sFDGuardar;
        private System.Windows.Forms.DateTimePicker dTPFechaFinHora;
        private System.Windows.Forms.DateTimePicker dTPInicioHora;
        private System.Windows.Forms.Label lProgressBar;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBAlmacenes;
    }
}