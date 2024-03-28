using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ApiInventarios
{
    class ConexionSql
    {
        private string user, pass, server, db;
        private string cadenaConexion;
        private SqlConnection sc;
        private RegistrosWindows reg;

        public ConexionSql ()
        {
            user = pass = server = "";
        }

        public string USER
        {
            set { user = value; }
            get { return user; }
        }
        public string PASS
        {
            set { pass = value; } get { return pass; }
        }
        public string SERVER
        {
            set { server = value; }
            get { return server; }
        }
        public string DB
        {
            set { db = value; }
            get { return db; }
        }
        public SqlConnection SC
        {
            set { sc = value; }
            get { return sc; }
        }
        public RegistrosWindows REG
        {
            set { reg = value; }
            get { return reg; }
        }
        public void ConectarSQLServer()
        {
            try
            {
                reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                cadenaConexion = @"Data Source=" + reg.SQL_SERVIDOR + "; Initial Catalog=" + reg.SQL_BD + "; User Id=" + reg.SQL_USUARIO
                        + "; Password=" + reg.SQL_PASSWORD + "; Trusted_Connection=False;MultipleActiveResultSets=True";
                sc = new SqlConnection(cadenaConexion);
                sc.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        public void Desconectar()
        {
            try { 
            //if (sc!=null?sc.State.Equals("true"):false)
                sc.Close();
            }
            catch
            {

            }
        }
        public int ObtenerSigID()
        {
            int _ultimo = 0;
            string aux = "";
                ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "SELECT top 1 IDS_VALOR,Centro_compra_Id FROM ID WHERE IDS_DESCRIPCION='General'",_CentroCompraID="";
                SqlCommand cmd;
                SqlDataReader reader;
                cn.ConectarSQLServer();
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux = reader["IDS_VALOR"].ToString();
                    _CentroCompraID = reader["Centro_compra_id"].ToString();
                    //_ultimo = Convert.ToInt32(_CentroCompraID + reader["IDS_VALOR"].ToString());
                }
                string aux2 = "";
                if(aux.Length>=_CentroCompraID.Length)
                    aux2 = aux.Substring(0, _CentroCompraID.Length);
                if(aux2 ==_CentroCompraID)
                    aux = aux.Substring(_CentroCompraID.Length);
                //aux = aux.Replace(_CentroCompraID, "").Trim();
                _ultimo = Convert.ToInt32(aux);
                ++_ultimo;
                aux = _CentroCompraID + Convert.ToString(_ultimo);
                _ultimo = Convert.ToInt32(aux);
                cmd.Dispose();
                reader.Close();
                cn.Desconectar();

                cn.ConectarSQLServer();
                consulta = "UPDATE [dbo].[ID] " +
                    " SET [IDS_VALOR] = " + _ultimo+
                    " WHERE IDS_DESCRIPCION='General'"+
                    " AND CENTRO_COMPRA_ID=" + _CentroCompraID;
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                if (IsConected() == true)
                    cn.Desconectar();
                MessageBox.Show("Error al obtener ultimo folio\n\"" + Ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _ultimo;
        }

        public int ObtenerSigIDCC(string CentroCompraID)
        {
            int _ultimo = 0;
            string aux = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "SELECT top 1 IDS_VALOR,Centro_compra_Id FROM ID WHERE IDS_DESCRIPCION='General' AND CENTRO_COMPRA_ID="+Convert.ToInt32(CentroCompraID);
                string _CentroCompraID = "";
                SqlCommand cmd;
                SqlDataReader reader;
                cn.ConectarSQLServer();
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux = reader["IDS_VALOR"].ToString();
                    _CentroCompraID = reader["Centro_compra_id"].ToString();
                    //_ultimo = Convert.ToInt32(_CentroCompraID + reader["IDS_VALOR"].ToString());
                }
                string aux2 = "";
                if (aux.Length >= _CentroCompraID.Length)
                    aux2 = aux.Substring(0, _CentroCompraID.Length);
                if (aux2 == _CentroCompraID)
                    aux = aux.Substring(_CentroCompraID.Length);
                //aux = aux.Replace(_CentroCompraID, "").Trim();
                _ultimo = Convert.ToInt32(aux);
                ++_ultimo;
                aux = _CentroCompraID + Convert.ToString(_ultimo);
                _ultimo = Convert.ToInt32(aux);
                cmd.Dispose();
                reader.Close();
                cn.Desconectar();
                cn.ConectarSQLServer();
                consulta = "UPDATE [dbo].[ID] " +
                    " SET [IDS_VALOR] = " + _ultimo +
                    " WHERE IDS_DESCRIPCION='General'" +
                    " AND CENTRO_COMPRA_ID=" + _CentroCompraID;
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                if (IsConected() == true)
                    cn.Desconectar();
                MessageBox.Show("Error al obtener ultimo folio\n\"" + Ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _ultimo;
        }
        public bool IsConected()
        {
            bool _estatus = false;
            try
            {
                if (sc.State.Equals("Open"))               
                    _estatus = true;                
                else
                    _estatus = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _estatus;
        }
        public int ObtenerSigID(string tabla)
        {
                ConexionSql cn = new ConexionSql();
            int _ultimo = 0;
            string aux = "";
            try
            {
                string fila = "", descripcion = "", _CentroCompraID = "";
                if (tabla == "TICKET")
                { fila = "2"; descripcion = "Tickets"; }
                else if (tabla == "LOTEEMBARQUE")
                { fila = "3"; descripcion = "Lote Embarque"; }
                else if (tabla == "LOTERECEPCION")
                { fila = "4"; descripcion = "Lote Recepcion"; }
                else if (tabla == "SUPERSACO")
                { fila = "5"; descripcion = "Supersaco"; }
                else if (tabla == "RECEPCIONPRODUCTO")
                { fila = "6"; descripcion = "Recepcion Producto"; }
                string consulta = "SELECT top 1 IDS_VALOR,Centro_compra_id FROM ID WHERE IDS_DESCRIPCION='" + descripcion+"'";//+ " AND CENTRO_COMPRA_ID=" + _CentroCompraID;
                cn.ConectarSQLServer();
                SqlCommand cmd;
                SqlDataReader reader;
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux = reader["IDS_VALOR"].ToString();
                    _CentroCompraID = reader["Centro_compra_id"].ToString();
                    //_ultimo = Convert.ToInt32(_CentroCompraID + reader["IDS_VALOR"].ToString());
                }
                string aux2 = "";
                if(aux.Length>=_CentroCompraID.Length)
                    aux2 = aux.Substring(0, _CentroCompraID.Length);
                if(aux2 ==_CentroCompraID)
                    aux = aux.Substring(_CentroCompraID.Length);
                //aux = aux.Replace(_CentroCompraID, "").Trim();
                _ultimo = Convert.ToInt32(aux);
                ++_ultimo;
                aux = _CentroCompraID + Convert.ToString(_ultimo);
                _ultimo = Convert.ToInt32(aux);
                cmd.Dispose();
                reader.Close();
                cn.Desconectar();
                cn.ConectarSQLServer();
                /*_ultimo++;
                cmd.Dispose();
                reader.Close();
                cn.Desconectar();
                cn.ConectarSQLServer();*/
                consulta = "UPDATE [dbo].[ID] " +
                    " SET [IDS_VALOR] = " + _ultimo.ToString() +
                    " WHERE IDS_DESCRIPCION='" + descripcion +
                    "' AND CENTRO_COMPRA_ID=" + _CentroCompraID;
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                if (IsConected() == true)
                    cn.Desconectar();
                MessageBox.Show("Error al obtener ultimo folio\n\"" + Ex.Message + "\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _ultimo;
        }
    }
}
