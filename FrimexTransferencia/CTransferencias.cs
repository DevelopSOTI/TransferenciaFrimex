using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FrimexTransferencia
{
    class CTransferencias
    {
        public DataTable VerTransferencias(string ESTATUS,DateTime FInicio,DateTime FFin,out string msg)
        {
            DataTable _datos = new DataTable();
            string msg_local = "", consulta = "";
            try
            {
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "select T.TRANSFERENCIA_ID,COUNT(TD.SUPERSACO_ID) as SS_ASIGNADOS,T.TRANSFERENCIA_FECHA, " +
                    " IFOR.ALMACEN_MSP_DESCRIPCION as ORIGEN,IFDE.ALMACEN_MSP_DESCRIPCION as DESTINO " +
                    " from TRANSFERENCIA as T " +
                    " inner join INVENTARIO_FRIMEX as IFOR on T.ALMACEN_ORIGEN_ID = IFOR.INVENTARIO_FRIMEX_ID " +
                    " inner join INVENTARIO_FRIMEX as IFDE on T.ALMACEN_DESTINO_ID = IFDE.INVENTARIO_FRIMEX_ID " +
                    " inner join TRANSFERENCIA_DETALLE as TD on T.TRANSFERENCIA_ID = TD.TRANSFERENCIA_ID " +
                    " inner join SUPERSACO as s on TD.SUPERSACO_ID = S.SUPERSACO_ID " +
                    " inner join PRODUCTO as p  on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                    " where T.TRANSFERENCIA_ESTATUS = '"+ESTATUS+"' and cast(t.TRANSFERENCIA_FECHA as date) between @FechaIni and @FechaFin " +
                    " group by T.TRANSFERENCIA_ID,T.TRANSFERENCIA_FECHA,IFOR.ALMACEN_MSP_DESCRIPCION,IFDE.ALMACEN_MSP_DESCRIPCION,p.PRODUCTO_DESCRIPCION" +
                    " ORDER BY T.TRANSFERENCIA_ID";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.SelectCommand.Parameters.Add("@FechaIni", SqlDbType.Date).Value = FInicio;
                _da.SelectCommand.Parameters.Add("@FechaFin",SqlDbType.Date).Value=FFin;
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();
            }
            catch(Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _datos;
        }
        public DataTable VerDetalleTransferencias(int TransferenciaID, out string msg)
        {
            DataTable _datos = new DataTable();
            string msg_local = "", consulta = "";
            try
            {
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "select ROW_NUMBER() OVER(ORDER BY TRANSFERENCIA_DETALLE_ID ASC) AS #,TD.SUPERSACO_ID as [SS ID],P.PRODUCTO_DESCRIPCION as PRODUCTO" +
                    " ,FORMAT(S.SUPERSACO_CANTIDAD, 'N', 'sp-mx') as PESO_SS,s.SUPERSACO_FECHA as [CREACION SUPERSACO],R.REQUISICION_FECHA_CREACION as [FECHA REQUISICION]" +
                    " ,T.TRANSFERENCIA_FECHA as [FECHA TRANSFERENCIA] " +                     
                    " from TRANSFERENCIA as T " +
                    " inner join TRANSFERENCIA_DETALLE as TD on T.TRANSFERENCIA_ID = TD.TRANSFERENCIA_ID " +
                    " inner join SUPERSACO as s on TD.SUPERSACO_ID = S.SUPERSACO_ID " +
                    " inner join PRODUCTO as p  on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                    " inner join REQ_TRA as RT on t.TRANSFERENCIA_ID = rt.TRANSFERENCIA_ID " +
                    " inner join REQUISICION as R on rt.REQUISICION_ID = R.REQUISICION_ID " +
                    " where T.TRANSFERENCIA_ID =  " +TransferenciaID +
                    " order by td.TRANSFERENCIA_DETALLE_ID";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _datos;
        }
        public bool ExisteTransferencia(int TRANSFERENCIA_ID)
        {
            bool _existe = false;
            string consulta = "" , aux ="";
            try
            {
                ConexionSql cn = new ConexionSql();
                consulta = "Select TRANSFERENCIA_ID from TRANSFERENCIA where TRANSFERENCIA_ID =" + TRANSFERENCIA_ID;
                cn.ConectarSQLServer();
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())                    
                        aux = Convert.ToString(reader["TRANSFERENCIA_ID"]);                   
                else
                    _existe = false;
                if (Convert.ToInt32(aux) > 0)
                    _existe = true;
                else
                    _existe = false;
                reader.Close();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch
            {
                _existe = false;
            }
            return _existe;
        }
        public bool InsertarEncabezadoTransferencia(string FOLIO_TRANSFERENCIA,string FECHA,string ESTATUS,string ALMACEN_ORIGEN,string ALMACEN_DESTINO,int REQ_ID,UsuariosC USUARIO,ConexionSql cn,SqlTransaction transaction)
        {
            bool _exito = false;
            try
            {
                string consulta = "INSERT INTO [dbo].[TRANSFERENCIA] " +
             " ([TRANSFERENCIA_ID] " +
             " ,[TRANSFERENCIA_FECHA] " +
             " ,[TRANSFERENCIA_ESTATUS] " +
             " ,[ALMACEN_ORIGEN_ID] " +
             " ,[ALMACEN_DESTINO_ID]) " +
              " VALUES " +
             " ( " + FOLIO_TRANSFERENCIA +
             " , @Fecha "  +
             " , '" + ESTATUS + "'" +
             " , " + ALMACEN_ORIGEN +
             " , " + ALMACEN_DESTINO + ")";// Consulta para insertar el encabezado de la transferencia

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                cmdm.Parameters.Add("@Fecha", SqlDbType.Date).Value = Convert.ToDateTime(FECHA);
                if (cmdm.ExecuteNonQuery() > 0)
                {
                    consulta = "INSERT INTO [dbo].[REQ_TRA] " +
                            " ([REQUISICION_ID] " +
                            " ,[TRANSFERENCIA_ID] " +
                            " ,[REQ_TRA_FECHA] " +
                            " ,[REQ_TRA_USUARIO_ID]) " +
                            " VALUES " +
                            " (" + REQ_ID +
                            " , " + FOLIO_TRANSFERENCIA +
                            " , @Fecha " +
                            " ," + USUARIO.USUARIOID + ")";
                    cmdm = new SqlCommand(consulta, cn.SC, transaction);
                    cmdm.Parameters.Add("@Fecha",SqlDbType.Date).Value = Convert.ToDateTime(FECHA);
                    if(cmdm.ExecuteNonQuery()>0)
                        _exito = true;
                    else
                        _exito = false;
                }
                else
                    _exito = false;
                cmdm.Dispose();
            }
            catch
            {
                _exito = false;
                transaction.Rollback();
            }
            return _exito;
        }
        public bool InsertarRenglonTransferencia(string TRANSFERENCIA_ID, string SUPERSACO_ID,ConexionSql cn, SqlTransaction transaction)
        {
            bool _exito = false;
            try
            {
                string consulta = "INSERT INTO [dbo].[TRANSFERENCIA_DETALLE] " +
                                   " ([TRANSFERENCIA_ID]" +
                                   ",[SUPERSACO_ID]) " +
                                    " VALUES " +
                                   " (" + TRANSFERENCIA_ID +
                                   ", " + SUPERSACO_ID + ")";// Consulta para insertar el SS de la transferencia

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                if (cmdm.ExecuteNonQuery() > 0)
                    _exito = true;
                else
                    _exito = false;
                cmdm.Dispose();
            }
            catch
            {
                _exito = false;
                transaction.Rollback();
            }
            return _exito;
        }
        public bool EliminarRenglonTransferencia(string TRANSFERENCIA_ID, string SUPERSACO_ID, ConexionSql cn, SqlTransaction transaction)
        {
            bool _exito = false;
            try
            {
                string consulta = "DELETE FROM [dbo].[TRANSFERENCIA_DETALLE] " +
                    " WHERE " +
                    " [TRANSFERENCIA_ID] = " + TRANSFERENCIA_ID +
                    " AND [SUPERSACO_ID] = " + SUPERSACO_ID; // Consulta para insertar el SS de la transferencia

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                if (cmdm.ExecuteNonQuery() > 0)
                    _exito = true;
                else
                    _exito = false;
                cmdm.Dispose();
            }
            catch
            {
                _exito = false;
                transaction.Rollback();
            }
            return _exito;
        }
        public DataTable CargarTransferencia(string TRANSFERENCIA_ID,string  ESTATUS)
        {
            DataTable _datos = new DataTable();
            try
            {
                string consulta = "";
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "Select s.SUPERSACO_ID,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD,poc.FOLIO " +
                    " from SUPERSACO as s " +
                    " inner join TRANSFERENCIA_DETALLE as td on td.SUPERSACO_ID = s.SUPERSACO_ID " +
                    " inner join TRANSFERENCIA as t on t.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
                    " inner join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_MSP_ID or s.PRODUCTO_ID = p.PRODUCTO_ID " +
                    " inner join EMBARQUE as e on s.EMBARQUE_ID=e.EMBARQUE_ID " +
                    " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID" +
                    " where " +
                    " t.TRANSFERENCIA_ID = " +TRANSFERENCIA_ID+ " and T.TRANSFERENCIA_ESTATUS = '" + ESTATUS+"'";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();
            }
            catch
            {
                _datos = null;
            }
            return _datos;
        }
        public bool ActualizarEstatusTransferencia(string TRANSFERENCIA_ID,string ESTATUS, ConexionSql cn, SqlTransaction transaction)
        {
            bool _exito = false;
            try
            {
                string consulta = "UPDATE [dbo].[TRANSFERENCIA] " +
                    " SET[TRANSFERENCIA_ESTATUS] = '"+ESTATUS+"' " +
                    " WHERE[TRANSFERENCIA_ID] = "+TRANSFERENCIA_ID;// Consulta para actualizar el estatus de la transferencia

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                if (cmdm.ExecuteNonQuery() > 0)
                    _exito = true;
                else
                    _exito = false;
                cmdm.Dispose();
            }
            catch
            {
                _exito = false;
                transaction.Rollback();
            }
            return _exito;
        }
        public int AlmacenMSPDesdeSQL(string INVENTARIO_FRIMEX_ID)
        {
            int _almacenID = -1;
            string consulta = "";
            try
            {
                ConexionSql cn = new ConexionSql();
                consulta = "Select  ALMACEN_MSP_ID from INVENTARIO_FRIMEX where INVENTARIO_FRIMEX_ID="+ INVENTARIO_FRIMEX_ID;
                cn.ConectarSQLServer();
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                        _almacenID = Convert.ToInt32(reader["ALMACEN_MSP_ID"]);
                reader.Close();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch
            {
                _almacenID = -1;
            }
            return _almacenID;
        }
    }
}
