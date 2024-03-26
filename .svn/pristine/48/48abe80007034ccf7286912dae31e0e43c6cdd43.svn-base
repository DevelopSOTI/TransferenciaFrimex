using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrimexTransferencia
{
    public partial class FTraDet : Form
    {
        public FTraDet()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        public int _TransferenciaID = 0;
        Mensajes mensajes = new Mensajes();
        private void FTraDet_Load(object sender, EventArgs e)
        {
            CTransferencias transferencias = new CTransferencias();
            this.Text += _TransferenciaID;
            string msg_local = "";
            dGVDatos.DataSource = transferencias.VerDetalleTransferencias(_TransferenciaID, out msg_local);
            dGVDatos.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;
            if(dGVDatos.Columns.Count>0)
            {
                dGVDatos.Columns[0].Width = 40;
                dGVDatos.Columns[1].Width = 60;
                dGVDatos.Columns[2].Width = 180;
                dGVDatos.Columns[3].Width = 60;
            }
            double _total = 0.0;
            if(dGVDatos.Rows.Count>0)
            {
                for (int i = 0; i < dGVDatos.Rows.Count; i++)
                    _total += Convert.ToDouble(dGVDatos["PESO_SS", i].Value);
            }
            label1.Text +=" "+ _total.ToString("N2");
            if(msg_local.Length >0)
                mensajes.Error(msg_local, "Error");
        }
    }
}
