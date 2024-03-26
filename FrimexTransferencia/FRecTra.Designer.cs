namespace FrimexTransferencia
{
    partial class FRecTra
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
            this.tBTransferenciaID = new System.Windows.Forms.TextBox();
            this.bIniciarRecepción = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dGVAsignados = new System.Windows.Forms.DataGridView();
            this.SUPERSACO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PESO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGVRecibidos = new System.Windows.Forms.DataGridView();
            this.SUPERSACO_IDR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTO_R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PESO_R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tBSupersacoID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bLeerSS = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dGVAsignados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRecibidos)).BeginInit();
            this.mSOpciones.SuspendLayout();
            this.pConectarPuerto.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(436, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio de Tranferencia";
            // 
            // tBTransferenciaID
            // 
            this.tBTransferenciaID.Location = new System.Drawing.Point(549, 36);
            this.tBTransferenciaID.Name = "tBTransferenciaID";
            this.tBTransferenciaID.Size = new System.Drawing.Size(120, 20);
            this.tBTransferenciaID.TabIndex = 1;
            this.tBTransferenciaID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBTransferenciaID_KeyPress);
            // 
            // bIniciarRecepción
            // 
            this.bIniciarRecepción.Location = new System.Drawing.Point(675, 33);
            this.bIniciarRecepción.Name = "bIniciarRecepción";
            this.bIniciarRecepción.Size = new System.Drawing.Size(99, 24);
            this.bIniciarRecepción.TabIndex = 2;
            this.bIniciarRecepción.Text = "Iniciar Recepción";
            this.bIniciarRecepción.UseVisualStyleBackColor = true;
            this.bIniciarRecepción.Click += new System.EventHandler(this.bIniciarRecepción_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Supersacos Asignados a la tranferencia";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(454, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Supersacos Recibidos";
            // 
            // dGVAsignados
            // 
            this.dGVAsignados.AllowUserToAddRows = false;
            this.dGVAsignados.AllowUserToDeleteRows = false;
            this.dGVAsignados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVAsignados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SUPERSACO_ID,
            this.PRODUCTO,
            this.PESO});
            this.dGVAsignados.Location = new System.Drawing.Point(12, 102);
            this.dGVAsignados.Name = "dGVAsignados";
            this.dGVAsignados.ReadOnly = true;
            this.dGVAsignados.RowHeadersVisible = false;
            this.dGVAsignados.Size = new System.Drawing.Size(380, 337);
            this.dGVAsignados.TabIndex = 5;
            // 
            // SUPERSACO_ID
            // 
            this.SUPERSACO_ID.HeaderText = "SUPERSACO";
            this.SUPERSACO_ID.Name = "SUPERSACO_ID";
            this.SUPERSACO_ID.ReadOnly = true;
            // 
            // PRODUCTO
            // 
            this.PRODUCTO.FillWeight = 180F;
            this.PRODUCTO.HeaderText = "PRODUCTO";
            this.PRODUCTO.Name = "PRODUCTO";
            this.PRODUCTO.ReadOnly = true;
            // 
            // PESO
            // 
            this.PESO.HeaderText = "PESO";
            this.PESO.Name = "PESO";
            this.PESO.ReadOnly = true;
            // 
            // dGVRecibidos
            // 
            this.dGVRecibidos.AllowUserToAddRows = false;
            this.dGVRecibidos.AllowUserToDeleteRows = false;
            this.dGVRecibidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVRecibidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVRecibidos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SUPERSACO_IDR,
            this.PRODUCTO_R,
            this.PESO_R});
            this.dGVRecibidos.Location = new System.Drawing.Point(402, 102);
            this.dGVRecibidos.Name = "dGVRecibidos";
            this.dGVRecibidos.ReadOnly = true;
            this.dGVRecibidos.RowHeadersVisible = false;
            this.dGVRecibidos.Size = new System.Drawing.Size(380, 337);
            this.dGVRecibidos.TabIndex = 6;
            // 
            // SUPERSACO_IDR
            // 
            this.SUPERSACO_IDR.HeaderText = "SUPERSACO";
            this.SUPERSACO_IDR.Name = "SUPERSACO_IDR";
            this.SUPERSACO_IDR.ReadOnly = true;
            // 
            // PRODUCTO_R
            // 
            this.PRODUCTO_R.FillWeight = 180F;
            this.PRODUCTO_R.HeaderText = "PRODUCTO";
            this.PRODUCTO_R.Name = "PRODUCTO_R";
            this.PRODUCTO_R.ReadOnly = true;
            // 
            // PESO_R
            // 
            this.PESO_R.HeaderText = "PESO";
            this.PESO_R.Name = "PESO_R";
            this.PESO_R.ReadOnly = true;
            // 
            // tBSupersacoID
            // 
            this.tBSupersacoID.Enabled = false;
            this.tBSupersacoID.Location = new System.Drawing.Point(107, 44);
            this.tBSupersacoID.Multiline = true;
            this.tBSupersacoID.Name = "tBSupersacoID";
            this.tBSupersacoID.Size = new System.Drawing.Size(120, 20);
            this.tBSupersacoID.TabIndex = 7;
            this.tBSupersacoID.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Super saco ID";
            this.label4.Visible = false;
            // 
            // bLeerSS
            // 
            this.bLeerSS.Enabled = false;
            this.bLeerSS.Location = new System.Drawing.Point(233, 37);
            this.bLeerSS.Name = "bLeerSS";
            this.bLeerSS.Size = new System.Drawing.Size(99, 39);
            this.bLeerSS.TabIndex = 9;
            this.bLeerSS.Text = "Capturar super saco";
            this.bLeerSS.UseVisualStyleBackColor = true;
            this.bLeerSS.Visible = false;
            this.bLeerSS.Click += new System.EventHandler(this.bLeerSS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Almacen";
            // 
            // cBAlmacen
            // 
            this.cBAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAlmacen.FormattingEnabled = true;
            this.cBAlmacen.Location = new System.Drawing.Point(487, 62);
            this.cBAlmacen.Name = "cBAlmacen";
            this.cBAlmacen.Size = new System.Drawing.Size(287, 21);
            this.cBAlmacen.TabIndex = 11;
            // 
            // mSOpciones
            // 
            this.mSOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.mSOpciones.Location = new System.Drawing.Point(0, 0);
            this.mSOpciones.Name = "mSOpciones";
            this.mSOpciones.Size = new System.Drawing.Size(789, 24);
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
            // pConectarPuerto
            // 
            this.pConectarPuerto.Controls.Add(this.cBPuertosDisponibles);
            this.pConectarPuerto.Controls.Add(this.bBuscarPuertos);
            this.pConectarPuerto.Controls.Add(this.lConecarBascula);
            this.pConectarPuerto.Location = new System.Drawing.Point(0, 0);
            this.pConectarPuerto.Name = "pConectarPuerto";
            this.pConectarPuerto.Size = new System.Drawing.Size(789, 452);
            this.pConectarPuerto.TabIndex = 33;
            this.pConectarPuerto.Visible = false;
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
            // FRecTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 450);
            this.Controls.Add(this.pConectarPuerto);
            this.Controls.Add(this.mSOpciones);
            this.Controls.Add(this.cBAlmacen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bLeerSS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tBSupersacoID);
            this.Controls.Add(this.dGVRecibidos);
            this.Controls.Add(this.dGVAsignados);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bIniciarRecepción);
            this.Controls.Add(this.tBTransferenciaID);
            this.Controls.Add(this.label1);
            this.Name = "FRecTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recibir por Transferencia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRecTra_FormClosing);
            this.Load += new System.EventHandler(this.FRecTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVAsignados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRecibidos)).EndInit();
            this.mSOpciones.ResumeLayout(false);
            this.mSOpciones.PerformLayout();
            this.pConectarPuerto.ResumeLayout(false);
            this.pConectarPuerto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBTransferenciaID;
        private System.Windows.Forms.Button bIniciarRecepción;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dGVAsignados;
        private System.Windows.Forms.DataGridView dGVRecibidos;
        private System.Windows.Forms.TextBox tBSupersacoID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bLeerSS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPERSACO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPERSACO_IDR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO_R;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESO_R;
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
    }
}