﻿namespace FrimexTransferencia
{
    partial class FVerTra
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
            this.dGVTransferencias = new System.Windows.Forms.DataGridView();
            this.bNuevaTransferencia = new System.Windows.Forms.Button();
            this.bMostrarRequisiciones = new System.Windows.Forms.Button();
            this.cBEstatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cBAlmacen = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dTPInicio = new System.Windows.Forms.DateTimePicker();
            this.dTPFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.bContinuarTra = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGVTransferencias)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVTransferencias
            // 
            this.dGVTransferencias.AllowUserToAddRows = false;
            this.dGVTransferencias.AllowUserToDeleteRows = false;
            this.dGVTransferencias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVTransferencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVTransferencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVTransferencias.Location = new System.Drawing.Point(12, 93);
            this.dGVTransferencias.Name = "dGVTransferencias";
            this.dGVTransferencias.ReadOnly = true;
            this.dGVTransferencias.RowHeadersVisible = false;
            this.dGVTransferencias.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dGVTransferencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVTransferencias.Size = new System.Drawing.Size(815, 338);
            this.dGVTransferencias.TabIndex = 7;
            this.dGVTransferencias.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVTransferencias_CellDoubleClick);
            // 
            // bNuevaTransferencia
            // 
            this.bNuevaTransferencia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bNuevaTransferencia.Location = new System.Drawing.Point(738, 5);
            this.bNuevaTransferencia.Name = "bNuevaTransferencia";
            this.bNuevaTransferencia.Size = new System.Drawing.Size(89, 38);
            this.bNuevaTransferencia.TabIndex = 6;
            this.bNuevaTransferencia.Text = "Nueva \r\nTransferencia";
            this.bNuevaTransferencia.UseVisualStyleBackColor = true;
            this.bNuevaTransferencia.Click += new System.EventHandler(this.bNuevaTransferencia_Click);
            // 
            // bMostrarRequisiciones
            // 
            this.bMostrarRequisiciones.Location = new System.Drawing.Point(135, 28);
            this.bMostrarRequisiciones.Name = "bMostrarRequisiciones";
            this.bMostrarRequisiciones.Size = new System.Drawing.Size(75, 23);
            this.bMostrarRequisiciones.TabIndex = 5;
            this.bMostrarRequisiciones.Text = "Mostrar";
            this.bMostrarRequisiciones.UseVisualStyleBackColor = true;
            this.bMostrarRequisiciones.Click += new System.EventHandler(this.bMostrarRequisiciones_Click);
            // 
            // cBEstatus
            // 
            this.cBEstatus.AutoCompleteCustomSource.AddRange(new string[] {
            "Pendiete",
            "Terminadas",
            "Canceladas"});
            this.cBEstatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cBEstatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cBEstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBEstatus.FormattingEnabled = true;
            this.cBEstatus.Items.AddRange(new object[] {
            "En tránsito",
            "Terminadas"});
            this.cBEstatus.Location = new System.Drawing.Point(12, 30);
            this.cBEstatus.Name = "cBEstatus";
            this.cBEstatus.Size = new System.Drawing.Size(117, 21);
            this.cBEstatus.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(441, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Almacen";
            // 
            // cBAlmacen
            // 
            this.cBAlmacen.AutoCompleteCustomSource.AddRange(new string[] {
            "Pendiete",
            "Terminadas",
            "Canceladas"});
            this.cBAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cBAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cBAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacen.FormattingEnabled = true;
            this.cBAlmacen.Items.AddRange(new object[] {
            "Canceladas",
            "Pendientes",
            "Terminadas"});
            this.cBAlmacen.Location = new System.Drawing.Point(495, 30);
            this.cBAlmacen.Name = "cBAlmacen";
            this.cBAlmacen.Size = new System.Drawing.Size(198, 21);
            this.cBAlmacen.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Fecha inicial:";
            // 
            // dTPInicio
            // 
            this.dTPInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPInicio.Location = new System.Drawing.Point(294, 12);
            this.dTPInicio.Name = "dTPInicio";
            this.dTPInicio.Size = new System.Drawing.Size(117, 20);
            this.dTPInicio.TabIndex = 11;
            // 
            // dTPFin
            // 
            this.dTPFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPFin.Location = new System.Drawing.Point(294, 38);
            this.dTPFin.Name = "dTPFin";
            this.dTPFin.Size = new System.Drawing.Size(117, 20);
            this.dTPFin.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Fecha final:";
            // 
            // bContinuarTra
            // 
            this.bContinuarTra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bContinuarTra.Location = new System.Drawing.Point(738, 49);
            this.bContinuarTra.Name = "bContinuarTra";
            this.bContinuarTra.Size = new System.Drawing.Size(89, 38);
            this.bContinuarTra.TabIndex = 14;
            this.bContinuarTra.Text = "Continuar \r\nTransferencia";
            this.bContinuarTra.UseVisualStyleBackColor = true;
            this.bContinuarTra.Click += new System.EventHandler(this.bContinuarTra_Click);
            // 
            // FVerTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 450);
            this.Controls.Add(this.bContinuarTra);
            this.Controls.Add(this.dTPFin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dTPInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBAlmacen);
            this.Controls.Add(this.dGVTransferencias);
            this.Controls.Add(this.bNuevaTransferencia);
            this.Controls.Add(this.bMostrarRequisiciones);
            this.Controls.Add(this.cBEstatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FVerTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferencias";
            this.Load += new System.EventHandler(this.FVerTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVTransferencias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVTransferencias;
        private System.Windows.Forms.Button bNuevaTransferencia;
        private System.Windows.Forms.Button bMostrarRequisiciones;
        private System.Windows.Forms.ComboBox cBEstatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBAlmacen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTPInicio;
        private System.Windows.Forms.DateTimePicker dTPFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bContinuarTra;
    }
}