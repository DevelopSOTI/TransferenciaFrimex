namespace FrimexTransferencia
{
    partial class FRegEmb
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
            this.gBOpciones = new System.Windows.Forms.GroupBox();
            this.rBImportacion = new System.Windows.Forms.RadioButton();
            this.rBNacional = new System.Windows.Forms.RadioButton();
            this.gBGenerales = new System.Windows.Forms.GroupBox();
            this.tBCantReciv = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.tBFolioMSP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bGuardar = new System.Windows.Forms.Button();
            this.bCancelar = new System.Windows.Forms.Button();
            this.gBOpciones.SuspendLayout();
            this.gBGenerales.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBOpciones
            // 
            this.gBOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBOpciones.Controls.Add(this.rBImportacion);
            this.gBOpciones.Controls.Add(this.rBNacional);
            this.gBOpciones.Location = new System.Drawing.Point(12, 12);
            this.gBOpciones.Name = "gBOpciones";
            this.gBOpciones.Size = new System.Drawing.Size(277, 46);
            this.gBOpciones.TabIndex = 0;
            this.gBOpciones.TabStop = false;
            this.gBOpciones.Text = "Tipo de recepción";
            // 
            // rBImportacion
            // 
            this.rBImportacion.AutoSize = true;
            this.rBImportacion.Location = new System.Drawing.Point(153, 19);
            this.rBImportacion.Name = "rBImportacion";
            this.rBImportacion.Size = new System.Drawing.Size(80, 17);
            this.rBImportacion.TabIndex = 1;
            this.rBImportacion.Text = "Importacion";
            this.rBImportacion.UseVisualStyleBackColor = true;
            this.rBImportacion.CheckedChanged += new System.EventHandler(this.rBImportacion_CheckedChanged);
            // 
            // rBNacional
            // 
            this.rBNacional.AutoSize = true;
            this.rBNacional.Location = new System.Drawing.Point(6, 19);
            this.rBNacional.Name = "rBNacional";
            this.rBNacional.Size = new System.Drawing.Size(125, 17);
            this.rBNacional.TabIndex = 0;
            this.rBNacional.Text = "Nacional desde MSP";
            this.rBNacional.UseVisualStyleBackColor = true;
            this.rBNacional.CheckedChanged += new System.EventHandler(this.rBNacional_CheckedChanged);
            // 
            // gBGenerales
            // 
            this.gBGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gBGenerales.Controls.Add(this.tBCantReciv);
            this.gBGenerales.Controls.Add(this.label3);
            this.gBGenerales.Controls.Add(this.cBProducto);
            this.gBGenerales.Controls.Add(this.tBFolioMSP);
            this.gBGenerales.Controls.Add(this.label2);
            this.gBGenerales.Controls.Add(this.label1);
            this.gBGenerales.Location = new System.Drawing.Point(12, 64);
            this.gBGenerales.Name = "gBGenerales";
            this.gBGenerales.Size = new System.Drawing.Size(277, 0);
            this.gBGenerales.TabIndex = 1;
            this.gBGenerales.TabStop = false;
            this.gBGenerales.Text = "Datos generales";
            // 
            // tBCantReciv
            // 
            this.tBCantReciv.Location = new System.Drawing.Point(101, 74);
            this.tBCantReciv.Name = "tBCantReciv";
            this.tBCantReciv.Size = new System.Drawing.Size(170, 20);
            this.tBCantReciv.TabIndex = 5;
            this.tBCantReciv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBCantReciv_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantidad a recibir";
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(101, 47);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(170, 21);
            this.cBProducto.TabIndex = 3;
            // 
            // tBFolioMSP
            // 
            this.tBFolioMSP.Location = new System.Drawing.Point(101, 21);
            this.tBFolioMSP.Name = "tBFolioMSP";
            this.tBFolioMSP.Size = new System.Drawing.Size(170, 20);
            this.tBFolioMSP.TabIndex = 2;
            this.tBFolioMSP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBFolioMSP_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Producto";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio MSP";
            // 
            // bGuardar
            // 
            this.bGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bGuardar.Location = new System.Drawing.Point(46, 67);
            this.bGuardar.Name = "bGuardar";
            this.bGuardar.Size = new System.Drawing.Size(94, 23);
            this.bGuardar.TabIndex = 2;
            this.bGuardar.Text = "Aceptar";
            this.bGuardar.UseVisualStyleBackColor = true;
            this.bGuardar.Click += new System.EventHandler(this.bGuardar_Click);
            // 
            // bCancelar
            // 
            this.bCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancelar.Location = new System.Drawing.Point(165, 67);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(77, 23);
            this.bCancelar.TabIndex = 3;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            this.bCancelar.Visible = false;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // FRegEmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 102);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.bGuardar);
            this.Controls.Add(this.gBGenerales);
            this.Controls.Add(this.gBOpciones);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FRegEmb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Mercancia";
            this.Load += new System.EventHandler(this.FRegEmb_Load);
            this.gBOpciones.ResumeLayout(false);
            this.gBOpciones.PerformLayout();
            this.gBGenerales.ResumeLayout(false);
            this.gBGenerales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBOpciones;
        private System.Windows.Forms.RadioButton rBImportacion;
        private System.Windows.Forms.RadioButton rBNacional;
        private System.Windows.Forms.GroupBox gBGenerales;
        private System.Windows.Forms.TextBox tBCantReciv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.TextBox tBFolioMSP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bGuardar;
        private System.Windows.Forms.Button bCancelar;
    }
}