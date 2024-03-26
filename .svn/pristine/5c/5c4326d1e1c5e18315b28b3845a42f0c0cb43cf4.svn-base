using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FrimexTransferencia
{
    class BodegaC
    {
        int BODEGAID { set; get; }
    string BODEGADESCRIPCION { set; get; }
        int USUARIOID { set; get; }

        public bool CrearBodega(int USUARIO_ID,string ALMACEN_DESCRIPCION,string ESTATUS,out string msg)
        {
            bool _estatus = false;
            string msg_local = "";
            ConexionSql cn = new ConexionSql();
            SqlCommand cmd;
            try
            {
                string consulta = "";

                consulta = "INSERT INTO [dbo].[INVENTARIO_FRIMEX] "+
                    "([INVENTARIO_FRIMEX_ID] " +
                    ",[INVENTARIO_FRIMEX_USUARIO_CREA] " +
                    ",[INVENTARIO_FRIMEX_FECHA_CREA] " +
                    ",[ALMACEN_MSP_ID] " +
                    ",[ALMACEN_MSP_DESCRIPCION] " +
                    ",[ESTATUS]) " +
                    "VALUES " +
                    "("+ cn.ObtenerSigID("").ToString() +
                    ", " + USUARIO_ID +
                    ",'" +DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") +"'"+
                    ",0" +
                    ",'"  + ALMACEN_DESCRIPCION+"' " +
                    ",'"+ ESTATUS+"')";

                cmd = new SqlCommand(consulta, cn.SC);
                if (cmd.ExecuteNonQuery() != 0)
                    _estatus = true;
                else
                    _estatus = false;
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _estatus = false;
            }
            msg = msg_local;
            return _estatus;
        }

        public bool ActualizarBodega(int INVENTARIO_ID,int USUARIO_ID, string ALMACEN_DESCRIPCION, string ESTATUS, out string msg)
        {
            bool _estatus = false;
            string msg_local = "";
            ConexionSql cn = new ConexionSql();
            SqlCommand cmd;
            try
            {
                string consulta = "";

                consulta = "UPDATE [dbo].[INVENTARIO_FRIMEX] " +
                    "SET [ALMACEN_MSP_DESCRIPCION] = '" +ALMACEN_DESCRIPCION+"'"+
                    ",[ESTATUS] = '" +ESTATUS+"'"+
                    "WHERE [INVENTARIO_FRIMEX_ID] ="+INVENTARIO_ID;

                cmd = new SqlCommand(consulta, cn.SC);
                if (cmd.ExecuteNonQuery() != 0)
                    _estatus = true;
                else
                    _estatus = false;
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _estatus = false;
            }
            msg = msg_local;
            return _estatus;
        }
        public bool BajaBodega(int INVENTARIO_ID, int USUARIO_ID, string ALMACEN_DESCRIPCION, string ESTATUS, out string msg)
        {
            bool _estatus = false;
            string msg_local = "";
            ConexionSql cn = new ConexionSql();
            SqlCommand cmd;
            try
            {
                string consulta = "";

                consulta = "UPDATE [dbo].[INVENTARIO_FRIMEX] " +
                    "SET [ALMACEN_MSP_DESCRIPCION] = '" + ALMACEN_DESCRIPCION + "'" +
                    ",[ESTATUS] = '" + ESTATUS + "'" +
                    "WHERE [INVENTARIO_FRIMEX_ID] =" + INVENTARIO_ID;

                cmd = new SqlCommand(consulta, cn.SC);
                if (cmd.ExecuteNonQuery() != 0)
                    _estatus = true;
                else
                    _estatus = false;
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _estatus = false;
            }
            msg = msg_local;
            return _estatus;
        }
    }
}
