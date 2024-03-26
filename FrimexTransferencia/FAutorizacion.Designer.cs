namespace FrimexTransferencia
{
    partial class FAutorizacion
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
            this.bCancelar = new System.Windows.Forms.Button();
            this.BAcceso = new System.Windows.Forms.Button();
            this.labelIniciarSesion = new System.Windows.Forms.Label();
            this.tBContraseña = new System.Windows.Forms.TextBox();
            this.tBUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bCancelar
            // 
            this.bCancelar.BackColor = System.Drawing.Color.Lavender;
            this.bCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.bCancelar.Location = new System.Drawing.Point(9, 166);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(233, 31);
            this.bCancelar.TabIndex = 35;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = false;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // BAcceso
            // 
            this.BAcceso.BackColor = System.Drawing.Color.Lavender;
            this.BAcceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BAcceso.Location = new System.Drawing.Point(9, 131);
            this.BAcceso.Name = "BAcceso";
            this.BAcceso.Size = new System.Drawing.Size(233, 29);
            this.BAcceso.TabIndex = 34;
            this.BAcceso.Text = "Aceptar";
            this.BAcceso.UseVisualStyleBackColor = false;
            this.BAcceso.Click += new System.EventHandler(this.BAcceso_Click);
            // 
            // labelIniciarSesion
            // 
            this.labelIniciarSesion.BackColor = System.Drawing.Color.Transparent;
            this.labelIniciarSesion.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIniciarSesion.ForeColor = System.Drawing.Color.White;
            this.labelIniciarSesion.Location = new System.Drawing.Point(5, 3);
            this.labelIniciarSesion.Name = "labelIniciarSesion";
            this.labelIniciarSesion.Size = new System.Drawing.Size(237, 45);
            this.labelIniciarSesion.TabIndex = 31;
            this.labelIniciarSesion.Text = "Autorización";
            this.labelIniciarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelIniciarSesion.Click += new System.EventHandler(this.labelIniciarSesion_Click);
            // 
            // tBContraseña
            // 
            this.tBContraseña.Location = new System.Drawing.Point(73, 92);
            this.tBContraseña.Name = "tBContraseña";
            this.tBContraseña.PasswordChar = '*';
            this.tBContraseña.Size = new System.Drawing.Size(169, 20);
            this.tBContraseña.TabIndex = 30;
            this.tBContraseña.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tBUsuario
            // 
            this.tBUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tBUsuario.Location = new System.Drawing.Point(73, 51);
            this.tBUsuario.Name = "tBUsuario";
            this.tBUsuario.Size = new System.Drawing.Size(169, 20);
            this.tBUsuario.TabIndex = 29;
            this.tBUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(24, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Usuario";
            // 
            // FAutorizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(247, 201);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.BAcceso);
            this.Controls.Add(this.labelIniciarSesion);
            this.Controls.Add(this.tBContraseña);
            this.Controls.Add(this.tBUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FAutorizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autorización";
            this.Load += new System.EventHandler(this.FAutorizacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button BAcceso;
        private System.Windows.Forms.Label labelIniciarSesion;
        private System.Windows.Forms.TextBox tBContraseña;
        private System.Windows.Forms.TextBox tBUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}