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
    public partial class FDetTra : Form
    {
        public FDetTra()
        {
            InitializeComponent();
        }

        public UsuariosC _Usuario = new UsuariosC();
        private Mensajes mensajes = new Mensajes();
        public int REQUISICION_ID = 0;
        private void FDetTra_Load(object sender, EventArgs e)
        {
            string msg_local = "";
            CargarTransferencias(REQUISICION_ID,dGVTransferencia,out msg_local);
        }
        public void CargarTransferencias(int REQUISICIONID,DataGridView dGVTRANSFERENCIAS,out string msg)
        {
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select T.* from  TRANSFERENCIA AS T " +
                    " Inner join REQ_TRA as RT on T.TRANSFERENCIA_ID = RT.TRANSFERENCIA_ID " +
                    " where RT.REQUISICION_ID = " + REQUISICIONID;
                cn.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    int i = 0;
                    while(reader.Read())
                    {
                        dGVTRANSFERENCIAS.Rows.Add();
                        dGVTRANSFERENCIAS[0, i].Value = Convert.ToString(reader["TRANSFERENCIA_ID"]);
                        dGVTRANSFERENCIAS[1, i].Value = Convert.ToString(reader["TRANSFERENCIA_FECHA"]);
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
        }
        public void CargarDetalleTransferencias(int TRANSFERENCIAID, DataGridView dGVDETALLETRANSFERENCIAS, out string msg)
        {
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select Td.TRANSFERENCIA_ID,count(p.PRODUCTO_ID) as CANTIDAD,p.PRODUCTO_DESCRIPCION " +
                    " from  TRANSFERENCIA_DETALLE as TD " +
                    " inner join SUPERSACO as s on TD.SUPERSACO_ID = s.SUPERSACO_ID " +
                    " inner join PRODUCTO as P on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID "+
                    " where TD.TRANSFERENCIA_ID = "+ TRANSFERENCIAID+
                    " group by Td.TRANSFERENCIA_ID,p.PRODUCTO_DESCRIPCION "+
                    " order by TRANSFERENCIA_ID, p.PRODUCTO_DESCRIPCION asc" ;
                cn.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        dGVDETALLETRANSFERENCIAS.Rows.Add();
                        dGVDETALLETRANSFERENCIAS[0, i].Value = Convert.ToString(reader["PRODUCTO_DESCRIPCION"]);
                        dGVDETALLETRANSFERENCIAS[1, i].Value = Convert.ToString(reader["CANTIDAD"]);
                        i++;
                    }
                }
                cmd.ExecuteNonQuery();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
        }
        private void dGVTransferencia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >-1)
            {
                string msg_local = "";
                CargarDetalleTransferencias(Convert.ToInt32(dGVTransferencia[0,e.RowIndex].Value), dGVDetalleTransferencia,out msg_local);
            }
        }
    }
}
