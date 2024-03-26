using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FrimexTransferencia
{
    public partial class FVisReq : Form
    {
        public FVisReq()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        private Mensajes mensajes = new Mensajes();
        private void FVisReq_Load(object sender, EventArgs e)
        {
            cBEstatus.SelectedIndex = 1;
            this.dGVRequisiciones.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGVRequisiciones.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Actualzar();
        }
        private void Actualzar()
        {
            string msg_local = "";
            string aux = cBEstatus.SelectedItem.ToString();
            if (aux == "Canceladas")
                aux = "C";
            else if (aux == "Pendientes")
                aux = "A";
            else if (aux == "Terminadas")
                aux = "T";
            CRequisiciones requisiciones = new CRequisiciones();
            requisiciones.LlenarGrid(requisiciones.BuscarRequisiciones(aux, 0,out msg_local), dGVRequisiciones, "Cambiar\n\rEstatus",out msg_local);
            if (msg_local.Length > 0)
                mensajes.Error(msg_local, "Error");
        }
        

        private void bNuevaRequisicion_Click(object sender, EventArgs e)
        {
            FReq freq = new FReq();
            freq._Usuario = _Usuario;
            freq.ShowDialog();
            Actualzar();
        }

        private void bMostrarRequisiciones_Click(object sender, EventArgs e)
        {
            Actualzar();
        }

        private void agregarOCAFurgónToolStripMenuItem_Click(object sender, EventArgs e)
        {
                FOCoFur fOCoFur = new FOCoFur();
                fOCoFur._Usuario = _Usuario;
                fOCoFur.ShowDialog();            
        }

        private void verTransferenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FDetTra fDetTra = new FDetTra();
            fDetTra._Usuario = _Usuario;
            fDetTra.REQUISICION_ID = Convert.ToInt32(dGVRequisiciones["REQUISICION_ID", dGVRequisiciones.CurrentCell.RowIndex].Value);
            fDetTra.ShowDialog();
        }

        private void dGVRequisiciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int fila = e.RowIndex, columna = e.ColumnIndex;
                if(columna==5)
                {
                FCamEstReq fCamEstReq = new FCamEstReq();
                fCamEstReq._Usuario = this._Usuario;
                fCamEstReq.REQUISICIONES_ID = Convert.ToInt32(dGVRequisiciones[0,e.RowIndex].Value);
                fCamEstReq.ShowDialog();
                Actualzar();

                }
            }
        }
    }
}
