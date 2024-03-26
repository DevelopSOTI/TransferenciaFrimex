﻿namespace FrimexTransferencia
{
    partial class FRecEmb
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nUDSupersacos = new System.Windows.Forms.NumericUpDown();
            this.lSuperSacos = new System.Windows.Forms.Label();
            this.lCantidadRecibida = new System.Windows.Forms.Label();
            this.mSOpciones = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conectarBásculaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desconectarBásculaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBPuertosDisponibles = new System.Windows.Forms.ComboBox();
            this.bBuscarPuertos = new System.Windows.Forms.Button();
            this.lConecarBascula = new System.Windows.Forms.Label();
            this.lEmbarqueSeleccionado = new System.Windows.Forms.Label();
            this.gBGeneral = new System.Windows.Forms.GroupBox();
            this.bCerrarRecepcion = new System.Windows.Forms.Button();
            this.tBSerie = new System.Windows.Forms.TextBox();
            this.lSerie = new System.Windows.Forms.Label();
            this.bAsignar = new System.Windows.Forms.Button();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.lProducto = new System.Windows.Forms.Label();
            this.lEmbarquesRecibidos = new System.Windows.Forms.Label();
            this.pConectarPuerto = new System.Windows.Forms.Panel();
            this.gBSuperSacos = new System.Windows.Forms.GroupBox();
            this.bActivar = new System.Windows.Forms.Button();
            this.cBAlmacen = new System.Windows.Forms.ComboBox();
            this.lAlmacen = new System.Windows.Forms.Label();
            this.dGVSupersacos = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapturarPeso = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Agregar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Imprimir = new System.Windows.Forms.DataGridViewButtonColumn();
            this.EmbarqueID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupersacoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tBEmbarqueRecibir = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSupersacos)).BeginInit();
            this.mSOpciones.SuspendLayout();
            this.gBGeneral.SuspendLayout();
            this.pConectarPuerto.SuspendLayout();
            this.gBSuperSacos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSupersacos)).BeginInit();
            this.SuspendLayout();
            // 
            // nUDSupersacos
            // 
            this.nUDSupersacos.Location = new System.Drawing.Point(121, 19);
            this.nUDSupersacos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSupersacos.Name = "nUDSupersacos";
            this.nUDSupersacos.Size = new System.Drawing.Size(55, 20);
            this.nUDSupersacos.TabIndex = 1;
            this.nUDSupersacos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDSupersacos.ValueChanged += new System.EventHandler(this.nUDSupersacos_ValueChanged);
            // 
            // lSuperSacos
            // 
            this.lSuperSacos.AutoSize = true;
            this.lSuperSacos.Location = new System.Drawing.Point(7, 20);
            this.lSuperSacos.Name = "lSuperSacos";
            this.lSuperSacos.Size = new System.Drawing.Size(108, 13);
            this.lSuperSacos.TabIndex = 0;
            this.lSuperSacos.Text = "Cantidad Supersacos";
            // 
            // lCantidadRecibida
            // 
            this.lCantidadRecibida.AutoSize = true;
            this.lCantidadRecibida.Location = new System.Drawing.Point(402, 32);
            this.lCantidadRecibida.Name = "lCantidadRecibida";
            this.lCantidadRecibida.Size = new System.Drawing.Size(0, 13);
            this.lCantidadRecibida.TabIndex = 34;
            // 
            // mSOpciones
            // 
            this.mSOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.mSOpciones.Location = new System.Drawing.Point(0, 0);
            this.mSOpciones.Name = "mSOpciones";
            this.mSOpciones.Size = new System.Drawing.Size(856, 24);
            this.mSOpciones.TabIndex = 31;
            this.mSOpciones.Text = "mSOpciones";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conectarBásculaToolStripMenuItem,
            this.desconectarBásculaToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // conectarBásculaToolStripMenuItem
            // 
            this.conectarBásculaToolStripMenuItem.Name = "conectarBásculaToolStripMenuItem";
            this.conectarBásculaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.conectarBásculaToolStripMenuItem.Text = "Conectar Báscula";
            this.conectarBásculaToolStripMenuItem.Click += new System.EventHandler(this.conectarBásculaToolStripMenuItem_Click);
            // 
            // desconectarBásculaToolStripMenuItem
            // 
            this.desconectarBásculaToolStripMenuItem.Name = "desconectarBásculaToolStripMenuItem";
            this.desconectarBásculaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.desconectarBásculaToolStripMenuItem.Text = "Desconectar Báscula";
            this.desconectarBásculaToolStripMenuItem.Visible = false;
            this.desconectarBásculaToolStripMenuItem.Click += new System.EventHandler(this.desconectarBásculaToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // cBPuertosDisponibles
            // 
            this.cBPuertosDisponibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBPuertosDisponibles.FormattingEnabled = true;
            this.cBPuertosDisponibles.Location = new System.Drawing.Point(288, 90);
            this.cBPuertosDisponibles.Name = "cBPuertosDisponibles";
            this.cBPuertosDisponibles.Size = new System.Drawing.Size(121, 21);
            this.cBPuertosDisponibles.TabIndex = 2;
            // 
            // bBuscarPuertos
            // 
            this.bBuscarPuertos.Location = new System.Drawing.Point(308, 139);
            this.bBuscarPuertos.Name = "bBuscarPuertos";
            this.bBuscarPuertos.Size = new System.Drawing.Size(75, 38);
            this.bBuscarPuertos.TabIndex = 1;
            this.bBuscarPuertos.Text = "Buscar Puertos ";
            this.bBuscarPuertos.UseVisualStyleBackColor = true;
            this.bBuscarPuertos.Click += new System.EventHandler(this.bBuscarPuertos_Click);
            // 
            // lConecarBascula
            // 
            this.lConecarBascula.AutoSize = true;
            this.lConecarBascula.Location = new System.Drawing.Point(305, 36);
            this.lConecarBascula.Name = "lConecarBascula";
            this.lConecarBascula.Size = new System.Drawing.Size(91, 13);
            this.lConecarBascula.TabIndex = 0;
            this.lConecarBascula.Text = "Conectar Bascula";
            // 
            // lEmbarqueSeleccionado
            // 
            this.lEmbarqueSeleccionado.AutoSize = true;
            this.lEmbarqueSeleccionado.Location = new System.Drawing.Point(146, 32);
            this.lEmbarqueSeleccionado.Name = "lEmbarqueSeleccionado";
            this.lEmbarqueSeleccionado.Size = new System.Drawing.Size(0, 13);
            this.lEmbarqueSeleccionado.TabIndex = 33;
            // 
            // gBGeneral
            // 
            this.gBGeneral.Controls.Add(this.bCerrarRecepcion);
            this.gBGeneral.Controls.Add(this.tBSerie);
            this.gBGeneral.Controls.Add(this.lSerie);
            this.gBGeneral.Controls.Add(this.bAsignar);
            this.gBGeneral.Controls.Add(this.cBProducto);
            this.gBGeneral.Controls.Add(this.lProducto);
            this.gBGeneral.Location = new System.Drawing.Point(17, 80);
            this.gBGeneral.Name = "gBGeneral";
            this.gBGeneral.Size = new System.Drawing.Size(267, 167);
            this.gBGeneral.TabIndex = 28;
            this.gBGeneral.TabStop = false;
            this.gBGeneral.Text = "General";
            // 
            // bCerrarRecepcion
            // 
            this.bCerrarRecepcion.Location = new System.Drawing.Point(6, 109);
            this.bCerrarRecepcion.Name = "bCerrarRecepcion";
            this.bCerrarRecepcion.Size = new System.Drawing.Size(249, 36);
            this.bCerrarRecepcion.TabIndex = 7;
            this.bCerrarRecepcion.Text = "Terminar Recepción";
            this.bCerrarRecepcion.UseVisualStyleBackColor = true;
            this.bCerrarRecepcion.Click += new System.EventHandler(this.bCerrarRecepcion_Click);
            // 
            // tBSerie
            // 
            this.tBSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tBSerie.Location = new System.Drawing.Point(209, 23);
            this.tBSerie.MaxLength = 1;
            this.tBSerie.Name = "tBSerie";
            this.tBSerie.Size = new System.Drawing.Size(57, 20);
            this.tBSerie.TabIndex = 6;
            this.tBSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lSerie
            // 
            this.lSerie.AutoSize = true;
            this.lSerie.Location = new System.Drawing.Point(169, 26);
            this.lSerie.Name = "lSerie";
            this.lSerie.Size = new System.Drawing.Size(34, 13);
            this.lSerie.TabIndex = 5;
            this.lSerie.Text = "Serie:";
            // 
            // bAsignar
            // 
            this.bAsignar.Location = new System.Drawing.Point(234, 49);
            this.bAsignar.Name = "bAsignar";
            this.bAsignar.Size = new System.Drawing.Size(32, 23);
            this.bAsignar.TabIndex = 4;
            this.bAsignar.Text = ">";
            this.bAsignar.UseVisualStyleBackColor = true;
            this.bAsignar.Click += new System.EventHandler(this.bAsignar_Click);
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(11, 51);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(212, 21);
            this.cBProducto.TabIndex = 1;
            // 
            // lProducto
            // 
            this.lProducto.AutoSize = true;
            this.lProducto.Location = new System.Drawing.Point(9, 30);
            this.lProducto.Name = "lProducto";
            this.lProducto.Size = new System.Drawing.Size(50, 13);
            this.lProducto.TabIndex = 0;
            this.lProducto.Text = "Producto";
            // 
            // lEmbarquesRecibidos
            // 
            this.lEmbarquesRecibidos.AutoSize = true;
            this.lEmbarquesRecibidos.Location = new System.Drawing.Point(20, 47);
            this.lEmbarquesRecibidos.Name = "lEmbarquesRecibidos";
            this.lEmbarquesRecibidos.Size = new System.Drawing.Size(97, 13);
            this.lEmbarquesRecibidos.TabIndex = 30;
            this.lEmbarquesRecibidos.Text = "Mercancia a recibir";
            // 
            // pConectarPuerto
            // 
            this.pConectarPuerto.Controls.Add(this.cBPuertosDisponibles);
            this.pConectarPuerto.Controls.Add(this.bBuscarPuertos);
            this.pConectarPuerto.Controls.Add(this.lConecarBascula);
            this.pConectarPuerto.Location = new System.Drawing.Point(290, 59);
            this.pConectarPuerto.Name = "pConectarPuerto";
            this.pConectarPuerto.Size = new System.Drawing.Size(554, 598);
            this.pConectarPuerto.TabIndex = 32;
            this.pConectarPuerto.Visible = false;
            // 
            // gBSuperSacos
            // 
            this.gBSuperSacos.Controls.Add(this.bActivar);
            this.gBSuperSacos.Controls.Add(this.cBAlmacen);
            this.gBSuperSacos.Controls.Add(this.lAlmacen);
            this.gBSuperSacos.Controls.Add(this.dGVSupersacos);
            this.gBSuperSacos.Controls.Add(this.nUDSupersacos);
            this.gBSuperSacos.Controls.Add(this.lSuperSacos);
            this.gBSuperSacos.Location = new System.Drawing.Point(287, 59);
            this.gBSuperSacos.Name = "gBSuperSacos";
            this.gBSuperSacos.Size = new System.Drawing.Size(557, 597);
            this.gBSuperSacos.TabIndex = 27;
            this.gBSuperSacos.TabStop = false;
            this.gBSuperSacos.Text = "Supersacos";
            // 
            // bActivar
            // 
            this.bActivar.Location = new System.Drawing.Point(462, 16);
            this.bActivar.Name = "bActivar";
            this.bActivar.Size = new System.Drawing.Size(69, 23);
            this.bActivar.TabIndex = 5;
            this.bActivar.Text = "Activar";
            this.bActivar.UseVisualStyleBackColor = true;
            this.bActivar.Click += new System.EventHandler(this.bActivar_Click);
            // 
            // cBAlmacen
            // 
            this.cBAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacen.FormattingEnabled = true;
            this.cBAlmacen.Items.AddRange(new object[] {
            "Bodega Tanques"});
            this.cBAlmacen.Location = new System.Drawing.Point(239, 18);
            this.cBAlmacen.Name = "cBAlmacen";
            this.cBAlmacen.Size = new System.Drawing.Size(190, 21);
            this.cBAlmacen.TabIndex = 4;
            // 
            // lAlmacen
            // 
            this.lAlmacen.AutoSize = true;
            this.lAlmacen.Location = new System.Drawing.Point(182, 21);
            this.lAlmacen.Name = "lAlmacen";
            this.lAlmacen.Size = new System.Drawing.Size(51, 13);
            this.lAlmacen.TabIndex = 3;
            this.lAlmacen.Text = "Almacen:";
            // 
            // dGVSupersacos
            // 
            this.dGVSupersacos.AllowUserToAddRows = false;
            this.dGVSupersacos.AllowUserToDeleteRows = false;
            this.dGVSupersacos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVSupersacos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dGVSupersacos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVSupersacos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.Peso,
            this.Producto,
            this.ProductoID,
            this.CapturarPeso,
            this.Agregar,
            this.Imprimir,
            this.EmbarqueID,
            this.Estatus,
            this.SupersacoID});
            this.dGVSupersacos.Location = new System.Drawing.Point(3, 46);
            this.dGVSupersacos.Name = "dGVSupersacos";
            this.dGVSupersacos.ReadOnly = true;
            this.dGVSupersacos.RowHeadersVisible = false;
            this.dGVSupersacos.Size = new System.Drawing.Size(554, 545);
            this.dGVSupersacos.TabIndex = 2;
            this.dGVSupersacos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVSupersacos_CellContentClick);
            // 
            // Numero
            // 
            this.Numero.FillWeight = 40F;
            this.Numero.HeaderText = "#";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            // 
            // Peso
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.Peso.DefaultCellStyle = dataGridViewCellStyle4;
            this.Peso.FillWeight = 61.59804F;
            this.Peso.HeaderText = "Peso(Kg)";
            this.Peso.Name = "Peso";
            this.Peso.ReadOnly = true;
            // 
            // Producto
            // 
            this.Producto.FillWeight = 258.1875F;
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            // 
            // ProductoID
            // 
            this.ProductoID.HeaderText = "ProductoID";
            this.ProductoID.Name = "ProductoID";
            this.ProductoID.ReadOnly = true;
            this.ProductoID.Visible = false;
            // 
            // CapturarPeso
            // 
            this.CapturarPeso.HeaderText = "Capturar Peso";
            this.CapturarPeso.Name = "CapturarPeso";
            this.CapturarPeso.ReadOnly = true;
            this.CapturarPeso.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CapturarPeso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Agregar
            // 
            this.Agregar.FillWeight = 67.9137F;
            this.Agregar.HeaderText = "Agregar";
            this.Agregar.Name = "Agregar";
            this.Agregar.ReadOnly = true;
            // 
            // Imprimir
            // 
            this.Imprimir.HeaderText = "Imprimir";
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.ReadOnly = true;
            this.Imprimir.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Imprimir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EmbarqueID
            // 
            this.EmbarqueID.HeaderText = "EmbarqueID";
            this.EmbarqueID.Name = "EmbarqueID";
            this.EmbarqueID.ReadOnly = true;
            this.EmbarqueID.Visible = false;
            // 
            // Estatus
            // 
            this.Estatus.HeaderText = "Estatus";
            this.Estatus.Name = "Estatus";
            this.Estatus.ReadOnly = true;
            this.Estatus.Visible = false;
            // 
            // SupersacoID
            // 
            this.SupersacoID.HeaderText = "SupersacoID";
            this.SupersacoID.Name = "SupersacoID";
            this.SupersacoID.ReadOnly = true;
            this.SupersacoID.Visible = false;
            // 
            // tBEmbarqueRecibir
            // 
            this.tBEmbarqueRecibir.Location = new System.Drawing.Point(128, 44);
            this.tBEmbarqueRecibir.Name = "tBEmbarqueRecibir";
            this.tBEmbarqueRecibir.Size = new System.Drawing.Size(147, 20);
            this.tBEmbarqueRecibir.TabIndex = 35;
            this.tBEmbarqueRecibir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBEmbarqueRecibir_KeyPress);
            // 
            // FRecEmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 664);
            this.Controls.Add(this.tBEmbarqueRecibir);
            this.Controls.Add(this.lCantidadRecibida);
            this.Controls.Add(this.mSOpciones);
            this.Controls.Add(this.lEmbarqueSeleccionado);
            this.Controls.Add(this.gBGeneral);
            this.Controls.Add(this.lEmbarquesRecibidos);
            this.Controls.Add(this.pConectarPuerto);
            this.Controls.Add(this.gBSuperSacos);
            this.Name = "FRecEmb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recepción de Mercancia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRecEmb_FormClosing);
            this.Load += new System.EventHandler(this.FRecEmb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUDSupersacos)).EndInit();
            this.mSOpciones.ResumeLayout(false);
            this.mSOpciones.PerformLayout();
            this.gBGeneral.ResumeLayout(false);
            this.gBGeneral.PerformLayout();
            this.pConectarPuerto.ResumeLayout(false);
            this.pConectarPuerto.PerformLayout();
            this.gBSuperSacos.ResumeLayout(false);
            this.gBSuperSacos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSupersacos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nUDSupersacos;
        private System.Windows.Forms.Label lSuperSacos;
        private System.Windows.Forms.Label lCantidadRecibida;
        private System.Windows.Forms.MenuStrip mSOpciones;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conectarBásculaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desconectarBásculaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ComboBox cBPuertosDisponibles;
        private System.Windows.Forms.Button bBuscarPuertos;
        private System.Windows.Forms.Label lConecarBascula;
        private System.Windows.Forms.Label lEmbarqueSeleccionado;
        private System.Windows.Forms.GroupBox gBGeneral;
        private System.Windows.Forms.Button bCerrarRecepcion;
        private System.Windows.Forms.TextBox tBSerie;
        private System.Windows.Forms.Label lSerie;
        private System.Windows.Forms.Button bAsignar;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.Label lProducto;
        private System.Windows.Forms.Label lEmbarquesRecibidos;
        private System.Windows.Forms.Panel pConectarPuerto;
        private System.Windows.Forms.GroupBox gBSuperSacos;
        private System.Windows.Forms.Button bActivar;
        private System.Windows.Forms.ComboBox cBAlmacen;
        private System.Windows.Forms.Label lAlmacen;
        private System.Windows.Forms.DataGridView dGVSupersacos;
        private System.Windows.Forms.TextBox tBEmbarqueRecibir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductoID;
        private System.Windows.Forms.DataGridViewButtonColumn CapturarPeso;
        private System.Windows.Forms.DataGridViewButtonColumn Agregar;
        private System.Windows.Forms.DataGridViewButtonColumn Imprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmbarqueID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupersacoID;
    }
}