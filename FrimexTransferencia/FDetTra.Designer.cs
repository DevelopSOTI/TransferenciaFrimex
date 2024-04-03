namespace FrimexTransferencia
{
    partial class FDetTra
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
            this.dGVTransferencia = new System.Windows.Forms.DataGridView();
            this.TRANSTERENCIA_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHA_TRANSTERENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dGVDetalleTransferencia = new System.Windows.Forms.DataGridView();
            this.PRODUCTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dGVTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDetalleTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGVTransferencia
            // 
            this.dGVTransferencia.AllowUserToAddRows = false;
            this.dGVTransferencia.AllowUserToDeleteRows = false;
            this.dGVTransferencia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVTransferencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVTransferencia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TRANSTERENCIA_ID,
            this.FECHA_TRANSTERENCIA});
            this.dGVTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVTransferencia.Location = new System.Drawing.Point(3, 3);
            this.dGVTransferencia.Name = "dGVTransferencia";
            this.dGVTransferencia.ReadOnly = true;
            this.dGVTransferencia.RowHeadersVisible = false;
            this.dGVTransferencia.Size = new System.Drawing.Size(173, 220);
            this.dGVTransferencia.TabIndex = 0;
            this.dGVTransferencia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVTransferencia_CellClick);
            // 
            // TRANSTERENCIA_ID
            // 
            this.TRANSTERENCIA_ID.FillWeight = 45F;
            this.TRANSTERENCIA_ID.HeaderText = "ID";
            this.TRANSTERENCIA_ID.Name = "TRANSTERENCIA_ID";
            this.TRANSTERENCIA_ID.ReadOnly = true;
            // 
            // FECHA_TRANSTERENCIA
            // 
            this.FECHA_TRANSTERENCIA.FillWeight = 155F;
            this.FECHA_TRANSTERENCIA.HeaderText = "FECHA";
            this.FECHA_TRANSTERENCIA.Name = "FECHA_TRANSTERENCIA";
            this.FECHA_TRANSTERENCIA.ReadOnly = true;
            // 
            // dGVDetalleTransferencia
            // 
            this.dGVDetalleTransferencia.AllowUserToAddRows = false;
            this.dGVDetalleTransferencia.AllowUserToDeleteRows = false;
            this.dGVDetalleTransferencia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGVDetalleTransferencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVDetalleTransferencia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PRODUCTO,
            this.CANTIDAD});
            this.dGVDetalleTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVDetalleTransferencia.Location = new System.Drawing.Point(3, 3);
            this.dGVDetalleTransferencia.Name = "dGVDetalleTransferencia";
            this.dGVDetalleTransferencia.ReadOnly = true;
            this.dGVDetalleTransferencia.RowHeadersVisible = false;
            this.dGVDetalleTransferencia.Size = new System.Drawing.Size(348, 220);
            this.dGVDetalleTransferencia.TabIndex = 1;
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
            this.CANTIDAD.FillWeight = 50F;
            this.CANTIDAD.HeaderText = "CANTIDAD";
            this.CANTIDAD.Name = "CANTIDAD";
            this.CANTIDAD.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dGVTransferencia);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dGVDetalleTransferencia);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(537, 226);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 2;
            // 
            // FDetTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 226);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FDetTra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de Trasnferencias";
            this.Load += new System.EventHandler(this.FDetTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVTransferencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDetalleTransferencia)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVTransferencia;
        private System.Windows.Forms.DataGridView dGVDetalleTransferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANSTERENCIA_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA_TRANSTERENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}