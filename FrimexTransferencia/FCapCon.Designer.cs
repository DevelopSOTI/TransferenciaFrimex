namespace FrimexTransferencia
{
    partial class FCapCon
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tBFolioContrato = new System.Windows.Forms.TextBox();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.dTPFEchaContrato = new System.Windows.Forms.DateTimePicker();
            this.tBObservaciones = new System.Windows.Forms.TextBox();
            this.bAceptar = new System.Windows.Forms.Button();
            this.bCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de Contrato";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Producto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fecha";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Observaciones";
            // 
            // tBFolioContrato
            // 
            this.tBFolioContrato.Location = new System.Drawing.Point(172, 15);
            this.tBFolioContrato.Name = "tBFolioContrato";
            this.tBFolioContrato.Size = new System.Drawing.Size(179, 20);
            this.tBFolioContrato.TabIndex = 4;
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(64, 59);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(287, 21);
            this.cBProducto.TabIndex = 5;
            // 
            // dTPFEchaContrato
            // 
            this.dTPFEchaContrato.Location = new System.Drawing.Point(60, 107);
            this.dTPFEchaContrato.Name = "dTPFEchaContrato";
            this.dTPFEchaContrato.Size = new System.Drawing.Size(291, 20);
            this.dTPFEchaContrato.TabIndex = 6;
            // 
            // tBObservaciones
            // 
            this.tBObservaciones.Location = new System.Drawing.Point(60, 158);
            this.tBObservaciones.Multiline = true;
            this.tBObservaciones.Name = "tBObservaciones";
            this.tBObservaciones.Size = new System.Drawing.Size(291, 115);
            this.tBObservaciones.TabIndex = 7;
            // 
            // bAceptar
            // 
            this.bAceptar.Location = new System.Drawing.Point(38, 279);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(75, 23);
            this.bAceptar.TabIndex = 8;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // bCancelar
            // 
            this.bCancelar.Location = new System.Drawing.Point(256, 279);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(75, 23);
            this.bCancelar.TabIndex = 9;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            // 
            // FCapCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 314);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.bAceptar);
            this.Controls.Add(this.tBObservaciones);
            this.Controls.Add(this.dTPFEchaContrato);
            this.Controls.Add(this.cBProducto);
            this.Controls.Add(this.tBFolioContrato);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FCapCon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capturar Contrato";
            this.Load += new System.EventHandler(this.FCapCon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBFolioContrato;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.DateTimePicker dTPFEchaContrato;
        private System.Windows.Forms.TextBox tBObservaciones;
        private System.Windows.Forms.Button bAceptar;
        private System.Windows.Forms.Button bCancelar;
    }
}