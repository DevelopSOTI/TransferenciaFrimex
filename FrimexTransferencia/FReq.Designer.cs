namespace FrimexTransferencia
{
    partial class FReq
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbAlmacen = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tBNoRequisicion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bCancelar = new System.Windows.Forms.Button();
            this.bTerminar = new System.Windows.Forms.Button();
            this.bAgregar = new System.Windows.Forms.Button();
            this.nUDCantidadSS = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.dGVRequisicion = new System.Windows.Forms.DataGridView();
            this.PRODUCTO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTO_NOMBRE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUITAR = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cBAlmacenDestino = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dTPFechaReq = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCantidadSS)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRequisicion)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dTPFechaReq);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.cBAlmacenDestino);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbAlmacen);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tBNoRequisicion);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.bCancelar);
            this.panel1.Controls.Add(this.bTerminar);
            this.panel1.Controls.Add(this.bAgregar);
            this.panel1.Controls.Add(this.nUDCantidadSS);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cBProducto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 351);
            this.panel1.TabIndex = 0;
            // 
            // cbAlmacen
            // 
            this.cbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlmacen.FormattingEnabled = true;
            this.cbAlmacen.Location = new System.Drawing.Point(9, 53);
            this.cbAlmacen.Name = "cbAlmacen";
            this.cbAlmacen.Size = new System.Drawing.Size(198, 21);
            this.cbAlmacen.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Almacen Origen";
            // 
            // tBNoRequisicion
            // 
            this.tBNoRequisicion.Enabled = false;
            this.tBNoRequisicion.Location = new System.Drawing.Point(99, 11);
            this.tBNoRequisicion.Name = "tBNoRequisicion";
            this.tBNoRequisicion.Size = new System.Drawing.Size(108, 20);
            this.tBNoRequisicion.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "N° de requisición:";
            // 
            // bCancelar
            // 
            this.bCancelar.Location = new System.Drawing.Point(132, 316);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(75, 23);
            this.bCancelar.TabIndex = 6;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // bTerminar
            // 
            this.bTerminar.Location = new System.Drawing.Point(6, 316);
            this.bTerminar.Name = "bTerminar";
            this.bTerminar.Size = new System.Drawing.Size(75, 23);
            this.bTerminar.TabIndex = 5;
            this.bTerminar.Text = "Terminar";
            this.bTerminar.UseVisualStyleBackColor = true;
            this.bTerminar.Click += new System.EventHandler(this.bTerminar_Click);
            // 
            // bAgregar
            // 
            this.bAgregar.Location = new System.Drawing.Point(6, 263);
            this.bAgregar.Name = "bAgregar";
            this.bAgregar.Size = new System.Drawing.Size(198, 23);
            this.bAgregar.TabIndex = 4;
            this.bAgregar.Text = "Agregar ->";
            this.bAgregar.UseVisualStyleBackColor = true;
            this.bAgregar.Click += new System.EventHandler(this.bAgregar_Click);
            // 
            // nUDCantidadSS
            // 
            this.nUDCantidadSS.Location = new System.Drawing.Point(141, 160);
            this.nUDCantidadSS.Name = "nUDCantidadSS";
            this.nUDCantidadSS.ReadOnly = true;
            this.nUDCantidadSS.Size = new System.Drawing.Size(67, 20);
            this.nUDCantidadSS.TabIndex = 3;
            this.nUDCantidadSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUDCantidadSS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cantidad de Supersacos";
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(9, 133);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(198, 21);
            this.cBProducto.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dGVRequisicion);
            this.panel2.Location = new System.Drawing.Point(216, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(518, 350);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Detalle de requisiciones";
            // 
            // dGVRequisicion
            // 
            this.dGVRequisicion.AllowUserToAddRows = false;
            this.dGVRequisicion.AllowUserToDeleteRows = false;
            this.dGVRequisicion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVRequisicion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVRequisicion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PRODUCTO_ID,
            this.PRODUCTO_NOMBRE,
            this.CANTIDAD,
            this.QUITAR});
            this.dGVRequisicion.Location = new System.Drawing.Point(6, 30);
            this.dGVRequisicion.Name = "dGVRequisicion";
            this.dGVRequisicion.ReadOnly = true;
            this.dGVRequisicion.RowHeadersVisible = false;
            this.dGVRequisicion.Size = new System.Drawing.Size(504, 309);
            this.dGVRequisicion.TabIndex = 0;
            this.dGVRequisicion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVRequisicion_CellContentClick);
            // 
            // PRODUCTO_ID
            // 
            this.PRODUCTO_ID.HeaderText = "PRODUCO_ID";
            this.PRODUCTO_ID.Name = "PRODUCTO_ID";
            this.PRODUCTO_ID.ReadOnly = true;
            this.PRODUCTO_ID.Visible = false;
            // 
            // PRODUCTO_NOMBRE
            // 
            this.PRODUCTO_NOMBRE.FillWeight = 300F;
            this.PRODUCTO_NOMBRE.HeaderText = "PRODUCTO";
            this.PRODUCTO_NOMBRE.Name = "PRODUCTO_NOMBRE";
            this.PRODUCTO_NOMBRE.ReadOnly = true;
            // 
            // CANTIDAD
            // 
            this.CANTIDAD.HeaderText = "CANTIDAD";
            this.CANTIDAD.Name = "CANTIDAD";
            this.CANTIDAD.ReadOnly = true;
            // 
            // QUITAR
            // 
            this.QUITAR.HeaderText = "QUITAR";
            this.QUITAR.Name = "QUITAR";
            this.QUITAR.ReadOnly = true;
            this.QUITAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QUITAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cBAlmacenDestino
            // 
            this.cBAlmacenDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacenDestino.FormattingEnabled = true;
            this.cBAlmacenDestino.Location = new System.Drawing.Point(9, 93);
            this.cBAlmacenDestino.Name = "cBAlmacenDestino";
            this.cBAlmacenDestino.Size = new System.Drawing.Size(198, 21);
            this.cBAlmacenDestino.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Almacen Destino";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Fecha de requisición";
            // 
            // dTPFechaReq
            // 
            this.dTPFechaReq.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPFechaReq.Location = new System.Drawing.Point(9, 204);
            this.dTPFechaReq.Name = "dTPFechaReq";
            this.dTPFechaReq.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dTPFechaReq.Size = new System.Drawing.Size(198, 20);
            this.dTPFechaReq.TabIndex = 14;
            // 
            // FReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 385);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requisiciones";
            this.Load += new System.EventHandler(this.Freq_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDCantidadSS)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRequisicion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button bTerminar;
        private System.Windows.Forms.Button bAgregar;
        private System.Windows.Forms.NumericUpDown nUDCantidadSS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dGVRequisicion;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO_NOMBRE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewButtonColumn QUITAR;
        private System.Windows.Forms.TextBox tBNoRequisicion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAlmacen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBAlmacenDestino;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dTPFechaReq;
        private System.Windows.Forms.Label label7;
    }
}