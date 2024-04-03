﻿namespace FrimexTransferencia
{
    partial class FSalTra
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tBSupersacoID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dGVSalida = new System.Windows.Forms.DataGridView();
            this.SUPERSACOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUITAR = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bEmbarcar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBFolioSalida = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBAlmacen = new System.Windows.Forms.ComboBox();
            this.mSOpciones = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conectarBásculaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desconectarBásculaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pConectarPuerto = new System.Windows.Forms.Panel();
            this.cBPuertosDisponibles = new System.Windows.Forms.ComboBox();
            this.bBuscarPuertos = new System.Windows.Forms.Button();
            this.lConecarBascula = new System.Windows.Forms.Label();
            this.lPesoBascula = new System.Windows.Forms.Label();
            this.tPesobascula = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dGVSalida)).BeginInit();
            this.mSOpciones.SuspendLayout();
            this.pConectarPuerto.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Súper Saco ";
            // 
            // tBSupersacoID
            // 
            this.tBSupersacoID.Location = new System.Drawing.Point(94, 14);
            this.tBSupersacoID.Multiline = true;
            this.tBSupersacoID.Name = "tBSupersacoID";
            this.tBSupersacoID.Size = new System.Drawing.Size(100, 20);
            this.tBSupersacoID.TabIndex = 1;
            this.tBSupersacoID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBSupersacoID_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Supersacos agregados a la salida";
            // 
            // dGVSalida
            // 
            this.dGVSalida.AllowUserToAddRows = false;
            this.dGVSalida.AllowUserToDeleteRows = false;
            this.dGVSalida.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVSalida.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVSalida.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SUPERSACOID,
            this.LOTE,
            this.PRODUCTO,
            this.CANTIDAD,
            this.QUITAR});
            this.dGVSalida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVSalida.Location = new System.Drawing.Point(3, 3);
            this.dGVSalida.Name = "dGVSalida";
            this.dGVSalida.ReadOnly = true;
            this.dGVSalida.RowHeadersVisible = false;
            this.dGVSalida.Size = new System.Drawing.Size(575, 347);
            this.dGVSalida.TabIndex = 3;
            this.dGVSalida.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVSalida_CellContentClick);
            // 
            // SUPERSACOID
            // 
            this.SUPERSACOID.FillWeight = 55F;
            this.SUPERSACOID.HeaderText = "SS Id";
            this.SUPERSACOID.Name = "SUPERSACOID";
            this.SUPERSACOID.ReadOnly = true;
            // 
            // LOTE
            // 
            this.LOTE.HeaderText = "LOTE";
            this.LOTE.Name = "LOTE";
            this.LOTE.ReadOnly = true;
            this.LOTE.Visible = false;
            // 
            // PRODUCTO
            // 
            this.PRODUCTO.FillWeight = 150F;
            this.PRODUCTO.HeaderText = "PRODUCTO";
            this.PRODUCTO.Name = "PRODUCTO";
            this.PRODUCTO.ReadOnly = true;
            // 
            // CANTIDAD
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CANTIDAD.DefaultCellStyle = dataGridViewCellStyle3;
            this.CANTIDAD.FillWeight = 45F;
            this.CANTIDAD.HeaderText = "CANT.";
            this.CANTIDAD.Name = "CANTIDAD";
            this.CANTIDAD.ReadOnly = true;
            // 
            // QUITAR
            // 
            this.QUITAR.FillWeight = 35F;
            this.QUITAR.HeaderText = "QUITAR";
            this.QUITAR.Name = "QUITAR";
            this.QUITAR.ReadOnly = true;
            this.QUITAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.QUITAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // bEmbarcar
            // 
            this.bEmbarcar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bEmbarcar.Location = new System.Drawing.Point(3, 3);
            this.bEmbarcar.Name = "bEmbarcar";
            this.bEmbarcar.Size = new System.Drawing.Size(575, 41);
            this.bEmbarcar.TabIndex = 4;
            this.bEmbarcar.Text = "Enviar Traspaso";
            this.bEmbarcar.UseVisualStyleBackColor = true;
            this.bEmbarcar.Click += new System.EventHandler(this.bEmbarcar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Agregar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Folio de salida";
            // 
            // tBFolioSalida
            // 
            this.tBFolioSalida.Enabled = false;
            this.tBFolioSalida.Location = new System.Drawing.Point(361, 11);
            this.tBFolioSalida.Name = "tBFolioSalida";
            this.tBFolioSalida.Size = new System.Drawing.Size(100, 20);
            this.tBFolioSalida.TabIndex = 6;
            this.tBFolioSalida.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBFolioSalida_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(268, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Peso Total:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Almacen";
            // 
            // cBAlmacen
            // 
            this.cBAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacen.FormattingEnabled = true;
            this.cBAlmacen.Location = new System.Drawing.Point(93, 40);
            this.cBAlmacen.Name = "cBAlmacen";
            this.cBAlmacen.Size = new System.Drawing.Size(368, 21);
            this.cBAlmacen.TabIndex = 9;
            // 
            // mSOpciones
            // 
            this.mSOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.mSOpciones.Location = new System.Drawing.Point(0, 0);
            this.mSOpciones.Name = "mSOpciones";
            this.mSOpciones.Size = new System.Drawing.Size(581, 24);
            this.mSOpciones.TabIndex = 32;
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
            this.conectarBásculaToolStripMenuItem.Click += new System.EventHandler(this.conectarBásculaToolStripMenuItem_Click_1);
            // 
            // desconectarBásculaToolStripMenuItem
            // 
            this.desconectarBásculaToolStripMenuItem.Name = "desconectarBásculaToolStripMenuItem";
            this.desconectarBásculaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.desconectarBásculaToolStripMenuItem.Text = "Desconectar Báscula";
            this.desconectarBásculaToolStripMenuItem.Visible = false;
            this.desconectarBásculaToolStripMenuItem.Click += new System.EventHandler(this.desconectarBásculaToolStripMenuItem_Click_1);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click_1);
            // 
            // pConectarPuerto
            // 
            this.pConectarPuerto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pConectarPuerto.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pConectarPuerto.Controls.Add(this.lConecarBascula);
            this.pConectarPuerto.Controls.Add(this.cBPuertosDisponibles);
            this.pConectarPuerto.Controls.Add(this.bBuscarPuertos);
            this.pConectarPuerto.Location = new System.Drawing.Point(0, 0);
            this.pConectarPuerto.Name = "pConectarPuerto";
            this.pConectarPuerto.Size = new System.Drawing.Size(581, 522);
            this.pConectarPuerto.TabIndex = 33;
            this.pConectarPuerto.Visible = false;
            // 
            // cBPuertosDisponibles
            // 
            this.cBPuertosDisponibles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cBPuertosDisponibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBPuertosDisponibles.FormattingEnabled = true;
            this.cBPuertosDisponibles.Location = new System.Drawing.Point(165, 214);
            this.cBPuertosDisponibles.Name = "cBPuertosDisponibles";
            this.cBPuertosDisponibles.Size = new System.Drawing.Size(266, 21);
            this.cBPuertosDisponibles.TabIndex = 2;
            // 
            // bBuscarPuertos
            // 
            this.bBuscarPuertos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bBuscarPuertos.Location = new System.Drawing.Point(207, 241);
            this.bBuscarPuertos.Name = "bBuscarPuertos";
            this.bBuscarPuertos.Size = new System.Drawing.Size(156, 52);
            this.bBuscarPuertos.TabIndex = 1;
            this.bBuscarPuertos.Text = "Buscar Puertos ";
            this.bBuscarPuertos.UseVisualStyleBackColor = true;
            this.bBuscarPuertos.Click += new System.EventHandler(this.bBuscarPuertos_Click_1);
            // 
            // lConecarBascula
            // 
            this.lConecarBascula.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lConecarBascula.AutoSize = true;
            this.lConecarBascula.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lConecarBascula.Location = new System.Drawing.Point(158, 174);
            this.lConecarBascula.Name = "lConecarBascula";
            this.lConecarBascula.Size = new System.Drawing.Size(286, 37);
            this.lConecarBascula.TabIndex = 0;
            this.lConecarBascula.Text = "Conectar Bascula";
            // 
            // lPesoBascula
            // 
            this.lPesoBascula.AutoSize = true;
            this.lPesoBascula.Location = new System.Drawing.Point(280, 24);
            this.lPesoBascula.Name = "lPesoBascula";
            this.lPesoBascula.Size = new System.Drawing.Size(0, 13);
            this.lPesoBascula.TabIndex = 34;
            // 
            // tPesobascula
            // 
            this.tPesobascula.Tick += new System.EventHandler(this.tPesobascula_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bEmbarcar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 475);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(581, 47);
            this.panel1.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tBSupersacoID);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lPesoBascula);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.tBFolioSalida);
            this.panel2.Controls.Add(this.cBAlmacen);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(581, 98);
            this.panel2.TabIndex = 36;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dGVSalida);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 122);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(581, 353);
            this.panel3.TabIndex = 37;
            // 
            // FSalTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 522);
            this.Controls.Add(this.pConectarPuerto);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mSOpciones);
            this.Controls.Add(this.label3);
            this.MinimumSize = new System.Drawing.Size(597, 561);
            this.Name = "FSalTra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salida de Súper Sacos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FSalTra_FormClosing);
            this.Load += new System.EventHandler(this.FSalTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVSalida)).EndInit();
            this.mSOpciones.ResumeLayout(false);
            this.mSOpciones.PerformLayout();
            this.pConectarPuerto.ResumeLayout(false);
            this.pConectarPuerto.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBSupersacoID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dGVSalida;
        private System.Windows.Forms.Button bEmbarcar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBFolioSalida;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBAlmacen;
        private System.Windows.Forms.MenuStrip mSOpciones;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conectarBásculaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desconectarBásculaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.Panel pConectarPuerto;
        private System.Windows.Forms.ComboBox cBPuertosDisponibles;
        private System.Windows.Forms.Button bBuscarPuertos;
        private System.Windows.Forms.Label lConecarBascula;
        private System.Windows.Forms.Label lPesoBascula;
        private System.Windows.Forms.Timer tPesobascula;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPERSACOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewButtonColumn QUITAR;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}