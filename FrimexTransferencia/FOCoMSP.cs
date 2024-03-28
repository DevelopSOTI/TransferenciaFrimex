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
using FirebirdSql.Data.FirebirdClient;

namespace FrimexTransferencia
{
    public partial class FOCoMSP : Form
    {
        public FOCoMSP()
        {
            InitializeComponent();
        }
        public string _ArticulioID = "";
        public UsuariosC _Usuarios = new UsuariosC();
        public string _OCSeleccionada = "";
        public string _DOCTO_CM_ID = "";
        private void FOCoMSP_Load(object sender, EventArgs e)
        {
            CargarProveedoresOC();
        }
        private void CargarProveedoresOC()
        {

            ConexionMicrosip cn = new ConexionMicrosip();
            try
            {
                string cadena = "";
                RegistrosWindows Reg = new RegistrosWindows();
                Reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                DataTable Tabla = new DataTable();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                cn.ConectarMicrosip(reg.MICRO_BD);
                cadena = "select distinct P.* from proveedores as P " +
                    " inner join doctos_cm as dcm on P.proveedor_id = dcm.Proveedor_id " +
                    " inner join DOCTOS_CM_DET as DCMD on DCMD.docto_cm_id=DCM.docto_cm_id " +
                    " inner join articulos as A on DCMD.Articulo_id = A.Articulo_id " +
                    " where " +
                    " dcm.tipo_docto = 'O'  and dcm.estatus = 'P'" +
                    " and fecha > '12/31/2017'" +
                    " and A.Articulo_id in (" + _ArticulioID + ")" +
                    " order by nombre";
                FbDataAdapter FDA = new FbDataAdapter(cadena, cn.FBC);
                FDA.Fill(Tabla);
                FDA.Dispose();
                cn.Desconectar();
                if (Tabla.Rows.Count > 0)
                {
                    if (cBProveedor.DataSource != null)
                        cBProveedor.DataSource = null;
                    cBProveedor.DataSource = Tabla;
                    cBProveedor.DisplayMember = "nombre";
                    cBProveedor.ValueMember = "proveedor_id";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        public DataTable CargarOrdenesCompraPendientesMicrosip(string IdProveedor, string ArticuloID)
        {
            Mensajes Mensaje = new Mensajes();
            ConexionMicrosip con_msp = new ConexionMicrosip();
            FbDataAdapter FDA;
            DataTable Tabla = new DataTable();
            string consulta = "";
            try
            {
                consulta = "select distinct DCM.docto_cm_id,DCM.folio,DCM.fecha, DCM.fecha_entrega,DCM.estatus, DCM.descripcion,DCM.importe_neto+DCM.fletes+DCM.otros_cargos+ " +
                   " DCM.total_impuestos + DCM.total_retenciones + DCM.gastos_aduanales + DCM.otros_gastos as importe " +
                   " from doctos_cm as DCM" +
                   " inner join DOCTOS_CM_DET as DCMD on DCMD.docto_cm_id=DCM.docto_cm_id " +
                   " inner join articulos as A on DCMD.Articulo_id = A.Articulo_id " +
                   " inner join libres_oco_cm as loco on loco.docto_cm_id=dcm.docto_cm_id" +
                   " where " +
                   " DCM.tipo_docto = 'O' " +
                   " and DCM.estatus = 'P' " +
                   " AND loco.FURGONASIGNADO='N' " +
                   " AND loco.FURGON is null " +
                   " and loco.CONTRATO IS NULL" +
                   " and A.Articulo_id in (" + ArticuloID + ")" +
                   " and DCM.proveedor_id= " + IdProveedor +
                   " and fecha > '12/31/2017'" +
                   " order by DCM.folio desc";
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FDA = new FbDataAdapter(consulta, con_msp.FBC);
                FDA.Fill(Tabla);
                FDA.Dispose();
                con_msp.Desconectar();
            }
            catch (Exception Ex)
            {
                Mensaje.Error(Ex.Message, "Error");
                Tabla = new DataTable();
            }
            return Tabla;
        }

        private void cBProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView _dRVProveedores = (DataRowView)cBProveedor.SelectedItem;
            string _IdProveedor = _dRVProveedores.Row.ItemArray[0].ToString();
            DataTable Tabla=CargarOrdenesCompraPendientesMicrosip(_IdProveedor, _ArticulioID);
            if (Tabla.Rows.Count > 0)
            {
                if (cBOC.DataSource != null)
                    cBOC.DataSource = null;
                cBOC.DataSource = Tabla;
                cBOC.DisplayMember = "folio";
                cBOC.ValueMember = "docto_cm_id";
            }
        }

        private void bSeleccionar_Click(object sender, EventArgs e)
        {
            string folio = "";
            DataRowView _dRVFolio = (DataRowView)cBOC.SelectedItem;
            folio=Convert.ToInt32(_dRVFolio.Row.ItemArray[1]).ToString();
            DialogResult _resp = MessageBox.Show("se asignará el folio "+folio+"\n\r¿Desea continuar?","Confirmacion",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (_resp == DialogResult.Yes)
            {
                _DOCTO_CM_ID = _dRVFolio.Row.ItemArray[0].ToString();
                _OCSeleccionada = folio;
                this.Close();
            }
            else
            {
                _OCSeleccionada = "";
                _DOCTO_CM_ID = "";
            }
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
