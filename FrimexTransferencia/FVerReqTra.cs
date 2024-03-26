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
    public partial class FVerReqTra : Form
    {
        public FVerReqTra()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        Mensajes mensajes = new Mensajes();
        private void FVerReqTra_Load(object sender, EventArgs e)
        {
            this.dGVRequisiciones.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.dGVRequisiciones.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            CargarDesdeAlmacen();
        }
        private void CargarDesdeAlmacen()
        {
            string msg_local = "";
            CRequisiciones requisiciones = new CRequisiciones();
            requisiciones.CargarAlmacenes(cBAlmacen, _Usuario.USUARIOID);
            DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
            if (cBAlmacen.SelectedItem != null)
            {
                int _AlmacenID = Convert.ToInt32(Convert.ToString(_dRVAlmacen.Row.ItemArray[0]));
                if (_AlmacenID > 0)
                {
                    requisiciones.LlenarGrid(requisiciones.BuscarRequisiciones("A", _AlmacenID, out msg_local), dGVRequisiciones, "Crear\n\rTransf.", out msg_local);
                    if (dGVRequisiciones.Rows.Count == 0)
                        mensajes.Informacion("El almacén no tiene requisiciones asignadas", "Información");
                    if (msg_local.Length > 0)
                        mensajes.Error(msg_local, "Error");
                }
                else
                    mensajes.Informacion("El usuario no tiene almacen asignado", "Información");
            }
        }
        private void dGVRequisiciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex, columna = e.ColumnIndex;
            if(dGVRequisiciones.Columns[ columna].HeaderText== "CREAR TRANSF.")
            {
                FSalTra salTra = new FSalTra();
                salTra._Usuario = _Usuario;
                salTra._ReqID = Convert.ToInt32(dGVRequisiciones["REQUISICION_ID", fila].Value);
                salTra.Text = "Salida de supersacos de la Requisición - "+salTra._ReqID +" -";
                salTra.ShowDialog();
                CargarDesdeAlmacen();
            }
        }

        private void cBAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CargarDesdeAlmacen();
        }
    }
}
