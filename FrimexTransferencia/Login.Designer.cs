namespace FrimexTransferencia
{
    partial class Login
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.labelIniciarSesion = new System.Windows.Forms.Label();
            this.bCancelar = new System.Windows.Forms.Button();
            this.BAcceso = new System.Windows.Forms.Button();
            this.tBContraseña = new System.Windows.Forms.TextBox();
            this.tBUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelIniciarSesion
            // 
            this.labelIniciarSesion.BackColor = System.Drawing.Color.Transparent;
            this.labelIniciarSesion.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIniciarSesion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.labelIniciarSesion.Location = new System.Drawing.Point(12, 9);
            this.labelIniciarSesion.Name = "labelIniciarSesion";
            this.labelIniciarSesion.Size = new System.Drawing.Size(233, 22);
            this.labelIniciarSesion.TabIndex = 12;
            this.labelIniciarSesion.Text = "Iniciar sesión";
            this.labelIniciarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bCancelar
            // 
            this.bCancelar.BackColor = System.Drawing.Color.Lavender;
            this.bCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.bCancelar.Location = new System.Drawing.Point(7, 189);
            this.bCancelar.Name = "bCancelar";
            this.bCancelar.Size = new System.Drawing.Size(233, 31);
            this.bCancelar.TabIndex = 11;
            this.bCancelar.Text = "Cancelar";
            this.bCancelar.UseVisualStyleBackColor = false;
            this.bCancelar.Click += new System.EventHandler(this.bCancelar_Click);
            // 
            // BAcceso
            // 
            this.BAcceso.BackColor = System.Drawing.Color.Lavender;
            this.BAcceso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.BAcceso.Location = new System.Drawing.Point(7, 140);
            this.BAcceso.Name = "BAcceso";
            this.BAcceso.Size = new System.Drawing.Size(233, 29);
            this.BAcceso.TabIndex = 10;
            this.BAcceso.Text = "Ingresar";
            this.BAcceso.UseVisualStyleBackColor = false;
            this.BAcceso.Click += new System.EventHandler(this.BAcceso_Click);
            // 
            // tBContraseña
            // 
            this.tBContraseña.Location = new System.Drawing.Point(71, 102);
            this.tBContraseña.Name = "tBContraseña";
            this.tBContraseña.PasswordChar = '*';
            this.tBContraseña.Size = new System.Drawing.Size(169, 20);
            this.tBContraseña.TabIndex = 9;
            this.tBContraseña.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tBContraseña.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBContraseña_KeyDown);
            // 
            // tBUsuario
            // 
            this.tBUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tBUsuario.Location = new System.Drawing.Point(71, 55);
            this.tBUsuario.Name = "tBUsuario";
            this.tBUsuario.Size = new System.Drawing.Size(169, 20);
            this.tBUsuario.TabIndex = 8;
            this.tBUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(4, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(22, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Usuario";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::FrimexTransferencia.Properties.Resources.tejido_gamuza_azul_celeste_textura_terciopelo_fondo_fieltro_113767_562;
            this.ClientSize = new System.Drawing.Size(249, 237);
            this.Controls.Add(this.labelIniciarSesion);
            this.Controls.Add(this.bCancelar);
            this.Controls.Add(this.BAcceso);
            this.Controls.Add(this.tBContraseña);
            this.Controls.Add(this.tBUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIniciarSesion;
        private System.Windows.Forms.Button bCancelar;
        private System.Windows.Forms.Button BAcceso;
        private System.Windows.Forms.TextBox tBContraseña;
        private System.Windows.Forms.TextBox tBUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

