namespace FrimexTransferencia
{
    partial class FVisReq
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
            this.cBEstatus = new System.Windows.Forms.ComboBox();
            this.bMostrarRequisiciones = new System.Windows.Forms.Button();
            this.bNuevaRequisicion = new System.Windows.Forms.Button();
            this.dGVRequisiciones = new System.Windows.Forms.DataGridView();
            this.REQUISICION_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQUISICION_DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REQUISICION_FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALMACEN_SOL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALMACEN_SOL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMBIAR_ESTATUS = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cMSTrasnferencias = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verTransferenciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarOCAFurgónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dGVRequisiciones)).BeginInit();
            this.cMSTrasnferencias.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            "Canceladas",
            "Pendientes",
            "Terminadas"});
            this.cBEstatus.Location = new System.Drawing.Point(12, 37);
            this.cBEstatus.Name = "cBEstatus";
            this.cBEstatus.Size = new System.Drawing.Size(146, 21);
            this.cBEstatus.TabIndex = 0;
            // 
            // bMostrarRequisiciones
            // 
            this.bMostrarRequisiciones.Location = new System.Drawing.Point(164, 37);
            this.bMostrarRequisiciones.Name = "bMostrarRequisiciones";
            this.bMostrarRequisiciones.Size = new System.Drawing.Size(75, 23);
            this.bMostrarRequisiciones.TabIndex = 1;
            this.bMostrarRequisiciones.Text = "Mostrar";
            this.bMostrarRequisiciones.UseVisualStyleBackColor = true;
            this.bMostrarRequisiciones.Click += new System.EventHandler(this.bMostrarRequisiciones_Click);
            // 
            // bNuevaRequisicion
            // 
            this.bNuevaRequisicion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bNuevaRequisicion.Location = new System.Drawing.Point(718, 27);
            this.bNuevaRequisicion.Name = "bNuevaRequisicion";
            this.bNuevaRequisicion.Size = new System.Drawing.Size(75, 38);
            this.bNuevaRequisicion.TabIndex = 2;
            this.bNuevaRequisicion.Text = "Nueva \r\nRequisición";
            this.bNuevaRequisicion.UseVisualStyleBackColor = true;
            this.bNuevaRequisicion.Click += new System.EventHandler(this.bNuevaRequisicion_Click);
            // 
            // dGVRequisiciones
            // 
            this.dGVRequisiciones.AllowUserToAddRows = false;
            this.dGVRequisiciones.AllowUserToDeleteRows = false;
            this.dGVRequisiciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGVRequisiciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVRequisiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVRequisiciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.REQUISICION_ID,
            this.REQUISICION_DESC,
            this.REQUISICION_FECHA,
            this.ALMACEN_SOL_ID,
            this.ALMACEN_SOL,
            this.CAMBIAR_ESTATUS});
            this.dGVRequisiciones.ContextMenuStrip = this.cMSTrasnferencias;
            this.dGVRequisiciones.Location = new System.Drawing.Point(12, 78);
            this.dGVRequisiciones.Name = "dGVRequisiciones";
            this.dGVRequisiciones.RowHeadersVisible = false;
            this.dGVRequisiciones.Size = new System.Drawing.Size(781, 334);
            this.dGVRequisiciones.TabIndex = 3;
            this.dGVRequisiciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVRequisiciones_CellClick);
            // 
            // REQUISICION_ID
            // 
            this.REQUISICION_ID.HeaderText = "REQUOSICION_ID";
            this.REQUISICION_ID.Name = "REQUISICION_ID";
            this.REQUISICION_ID.ReadOnly = true;
            this.REQUISICION_ID.Visible = false;
            // 
            // REQUISICION_DESC
            // 
            this.REQUISICION_DESC.FillWeight = 140F;
            this.REQUISICION_DESC.HeaderText = "REQUISICION";
            this.REQUISICION_DESC.Name = "REQUISICION_DESC";
            this.REQUISICION_DESC.ReadOnly = true;
            // 
            // REQUISICION_FECHA
            // 
            this.REQUISICION_FECHA.FillWeight = 60F;
            this.REQUISICION_FECHA.HeaderText = "FECHA";
            this.REQUISICION_FECHA.Name = "REQUISICION_FECHA";
            this.REQUISICION_FECHA.ReadOnly = true;
            // 
            // ALMACEN_SOL_ID
            // 
            this.ALMACEN_SOL_ID.HeaderText = "ALMACEN_SOL_ID";
            this.ALMACEN_SOL_ID.Name = "ALMACEN_SOL_ID";
            this.ALMACEN_SOL_ID.ReadOnly = true;
            this.ALMACEN_SOL_ID.Visible = false;
            // 
            // ALMACEN_SOL
            // 
            this.ALMACEN_SOL.FillWeight = 50F;
            this.ALMACEN_SOL.HeaderText = "ALMACEN ORIGEN";
            this.ALMACEN_SOL.Name = "ALMACEN_SOL";
            this.ALMACEN_SOL.ReadOnly = true;
            // 
            // CAMBIAR_ESTATUS
            // 
            this.CAMBIAR_ESTATUS.FillWeight = 30F;
            this.CAMBIAR_ESTATUS.HeaderText = "CAMBIAR ESTATUS";
            this.CAMBIAR_ESTATUS.Name = "CAMBIAR_ESTATUS";
            // 
            // cMSTrasnferencias
            // 
            this.cMSTrasnferencias.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verTransferenciasToolStripMenuItem});
            this.cMSTrasnferencias.Name = "cMSTrasnferencias";
            this.cMSTrasnferencias.Size = new System.Drawing.Size(167, 26);
            // 
            // verTransferenciasToolStripMenuItem
            // 
            this.verTransferenciasToolStripMenuItem.Name = "verTransferenciasToolStripMenuItem";
            this.verTransferenciasToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.verTransferenciasToolStripMenuItem.Text = "Ver transferencias";
            this.verTransferenciasToolStripMenuItem.Click += new System.EventHandler(this.verTransferenciasToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(805, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarOCAFurgónToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // agregarOCAFurgónToolStripMenuItem
            // 
            this.agregarOCAFurgónToolStripMenuItem.Name = "agregarOCAFurgónToolStripMenuItem";
            this.agregarOCAFurgónToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.agregarOCAFurgónToolStripMenuItem.Text = "Agregar OC a Furgón";
            this.agregarOCAFurgónToolStripMenuItem.Click += new System.EventHandler(this.agregarOCAFurgónToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // FVisReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 424);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dGVRequisiciones);
            this.Controls.Add(this.bNuevaRequisicion);
            this.Controls.Add(this.bMostrarRequisiciones);
            this.Controls.Add(this.cBEstatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FVisReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Requisiciones";
            this.Load += new System.EventHandler(this.FVisReq_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVRequisiciones)).EndInit();
            this.cMSTrasnferencias.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBEstatus;
        private System.Windows.Forms.Button bMostrarRequisiciones;
        private System.Windows.Forms.Button bNuevaRequisicion;
        private System.Windows.Forms.DataGridView dGVRequisiciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQUISICION_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQUISICION_DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn REQUISICION_FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALMACEN_SOL_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALMACEN_SOL;
        private System.Windows.Forms.DataGridViewButtonColumn CAMBIAR_ESTATUS;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarOCAFurgónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cMSTrasnferencias;
        private System.Windows.Forms.ToolStripMenuItem verTransferenciasToolStripMenuItem;
    }
}