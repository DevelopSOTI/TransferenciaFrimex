﻿namespace FrimexTransferencia
{
    partial class FOCoFur
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
            this.bAceptar = new System.Windows.Forms.Button();
            this.cBFurgon = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tBFolioMSP = new System.Windows.Forms.TextBox();
            this.tBCantReciv = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cBProducto = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bCancelar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bAceptar
            // 
            this.bAceptar.Location = new System.Drawing.Point(12, 156);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(138, 23);
            this.bAceptar.TabIndex = 5;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
            // 
            // cBFurgon
            // 
            this.cBFurgon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBFurgon.FormattingEnabled = true;
            this.cBFurgon.Location = new System.Drawing.Point(74, 23);
            this.cBFurgon.Name = "cBFurgon";
            this.cBFurgon.Size = new System.Drawing.Size(208, 21);
            this.cBFurgon.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Furgon";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Orden de Compra";
            // 
            // tBFolioMSP
            // 
            this.tBFolioMSP.Enabled = false;
            this.tBFolioMSP.Location = new System.Drawing.Point(106, 59);
            this.tBFolioMSP.Name = "tBFolioMSP";
            this.tBFolioMSP.Size = new System.Drawing.Size(85, 20);
            this.tBFolioMSP.TabIndex = 2;
            this.tBFolioMSP.TextChanged += new System.EventHandler(this.tBFolioMSP_TextChanged);
            this.tBFolioMSP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBFolioOCMSP_KeyPress);
            // 
            // tBCantReciv
            // 
            this.tBCantReciv.Location = new System.Drawing.Point(107, 130);
            this.tBCantReciv.Name = "tBCantReciv";
            this.tBCantReciv.Size = new System.Drawing.Size(175, 20);
            this.tBCantReciv.TabIndex = 4;
            this.tBCantReciv.Visible = false;
            this.tBCantReciv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBCantReciv_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Cantidad a recibir";
            this.label3.Visible = false;
            // 
            // cBProducto
            // 
            this.cBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBProducto.FormattingEnabled = true;
            this.cBProducto.Location = new System.Drawing.Point(74, 95);
            this.cBProducto.Name = "cBProducto";
            this.cBProducto.Size = new System.Drawing.Size(208, 21);
            this.cBProducto.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Producto";
            // 
            // bCancelar
            // 
            this.bCancelar.Location = new System.Drawing.Point(206, 156);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(75, 23);
            this.bCancelar.TabIndex = 6;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = true;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(207, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "Asignar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FOCoFur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 191);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.cBProducto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tBCantReciv);
            this.Controls.Add(this.tBFolioMSP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bAceptar);
            this.Controls.Add(this.cBFurgon);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(308, 230);
            this.MinimumSize = new System.Drawing.Size(308, 230);
            this.Name = "FOCoFur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Orden de compra - Furgón";
            this.Load += new System.EventHandler(this.FOCoFur_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bAceptar;
        private System.Windows.Forms.ComboBox cBFurgon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBFolioMSP;
        private System.Windows.Forms.TextBox tBCantReciv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBProducto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button button1;
    }
}