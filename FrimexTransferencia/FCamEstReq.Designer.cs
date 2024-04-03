namespace FrimexTransferencia
{
    partial class FCamEstReq
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
            this.bCambiar = new System.Windows.Forms.Button();
            this.cBEstatus = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cambiar a Estatus:";
            // 
            // bCambiar
            // 
            this.bCambiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCambiar.Location = new System.Drawing.Point(67, 79);
            this.bCambiar.Name = "bCambiar";
            this.bCambiar.Size = new System.Drawing.Size(94, 31);
            this.bCambiar.TabIndex = 2;
            this.bCambiar.Text = "Cambiar";
            this.bCambiar.UseVisualStyleBackColor = true;
            this.bCambiar.Click += new System.EventHandler(this.bCambiar_Click);
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
            "Cancelada",
            "Pendiente",
            "Terminada"});
            this.cBEstatus.Location = new System.Drawing.Point(42, 52);
            this.cBEstatus.Name = "cBEstatus";
            this.cBEstatus.Size = new System.Drawing.Size(146, 21);
            this.cBEstatus.TabIndex = 3;
            // 
            // FCamEstReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 142);
            this.Controls.Add(this.cBEstatus);
            this.Controls.Add(this.bCambiar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FCamEstReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Estatus";
            this.Load += new System.EventHandler(this.FCamEstReq_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bCambiar;
        private System.Windows.Forms.ComboBox cBEstatus;
    }
}