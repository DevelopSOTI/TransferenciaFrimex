﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace FrimexTransferencia
{
    class CRequisiciones
    {
        public bool LlenarGrid(DataTable REQUISICIONES, DataGridView dGVREQUISICIONES, string TextoBoton,out string msg)
        {
            string msg_local = "";
            dGVREQUISICIONES.Rows.Clear();
            bool _exito = false;
            try
            {
                List<List<string>> _Requisiciones = new List<List<string>>();
                string _REQUISICION_ID = "",
                    _REQUISICION_DESC = "",
                    _REQUISICION_FECHA = "",
                    _ALMACEN_SOL_ID = "",
                    _ALMACEN_SOL = "",
                    _CAMBIAR_ESTATUS = "";
                foreach (DataRow row in REQUISICIONES.Rows)
                {
                    List<string> _nuevo = new List<string>();
                    if (_Requisiciones.Count == 0)
                    {
                        _REQUISICION_ID = Convert.ToString(row["REQUISICION_ID"]);
                        _REQUISICION_DESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]) + " CANT. " + Convert.ToString(row["REQUISICION_DETALLE_CANTIDAD"] + " SS");
                        _REQUISICION_FECHA = Convert.ToString(row["REQUISICION_FECHA"]);
                        _ALMACEN_SOL_ID = Convert.ToString(row["INVENTARIO_FRIMEX_ID"]);
                        _ALMACEN_SOL = Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"]);
                        _CAMBIAR_ESTATUS = TextoBoton;
                        _nuevo.Add(_REQUISICION_ID);
                        _nuevo.Add(_REQUISICION_DESC);
                        _nuevo.Add(_REQUISICION_FECHA);
                        _nuevo.Add(_ALMACEN_SOL_ID);
                        _nuevo.Add(_ALMACEN_SOL);
                        _nuevo.Add(_CAMBIAR_ESTATUS);
                        _Requisiciones.Add(_nuevo);
                    }
                    else
                    {
                        _REQUISICION_ID = Convert.ToString(row["REQUISICION_ID"]);
                        bool existe = false;
                        for (int i = 0; i < _Requisiciones.Count; i++)
                        {
                            List<string> _Aux_nuevo = new List<string>();
                            _Aux_nuevo = _Requisiciones[i];
                            string aux_req_id = _Aux_nuevo[0];
                            if (_REQUISICION_ID == aux_req_id)
                            {
                                _REQUISICION_DESC += ",\n\r" + Convert.ToString(row["PRODUCTO_DESCRIPCION"]) + " CANT. " + Convert.ToString(row["REQUISICION_DETALLE_CANTIDAD"] + "SS");
                                _Requisiciones[i][1] = _REQUISICION_DESC;
                                existe = true;
                                break;
                            }
                        }
                        if (existe == false)
                        {
                            _REQUISICION_ID = Convert.ToString(row["REQUISICION_ID"]);
                            _REQUISICION_DESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]) + " CANT. " + Convert.ToString(row["REQUISICION_DETALLE_CANTIDAD"] + " SS");
                            _REQUISICION_FECHA = Convert.ToString(row["REQUISICION_FECHA"]);
                            _ALMACEN_SOL_ID = Convert.ToString(row["INVENTARIO_FRIMEX_ID"]);
                            _ALMACEN_SOL = Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"]);
                            _CAMBIAR_ESTATUS = TextoBoton ;
                            _nuevo.Add(_REQUISICION_ID);
                            _nuevo.Add(_REQUISICION_DESC);
                            _nuevo.Add(_REQUISICION_FECHA);
                            _nuevo.Add(_ALMACEN_SOL_ID);
                            _nuevo.Add(_ALMACEN_SOL);
                            _nuevo.Add(_CAMBIAR_ESTATUS);
                            _Requisiciones.Add(_nuevo);
                        }
                    }
                }
                for (int i = 0; i < _Requisiciones.Count; i++)
                {
                    dGVREQUISICIONES.Rows.Add();
                    dGVREQUISICIONES["REQUISICION_ID", i].Value = _Requisiciones[i][0];
                    dGVREQUISICIONES["REQUISICION_DESC", i].Value = _Requisiciones[i][1];
                    dGVREQUISICIONES["REQUISICION_FECHA", i].Value = _Requisiciones[i][2];
                    dGVREQUISICIONES["ALMACEN_SOL_ID", i].Value = _Requisiciones[i][3];
                    dGVREQUISICIONES["ALMACEN_SOL", i].Value = _Requisiciones[i][4];
                    dGVREQUISICIONES["CAMBIAR_ESTATUS", i].Value = _Requisiciones[i][5];
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _exito;
        }
        public DataTable BuscarRequisiciones(string ESTATUS, int ALMACEN_ID, out string msg)
        {
            string consulta = "", msg_local = "",where="";
            DataTable _datos = new DataTable();
            ConexionSql cn = new ConexionSql();
            try
            {
                if (ALMACEN_ID > 0)
                    where = " AND R.ALMACEN_ID="+ALMACEN_ID;
                consulta = "select r.REQUISICION_ID,r.REQUISICION_FECHA_CREACION,r.REQUISICION_FECHA,p.PRODUCTO_DESCRIPCION " +
                    " ,REQUISICION_DETALLE_CANTIDAD,ifr.INVENTARIO_FRIMEX_ID,ifr.ALMACEN_MSP_DESCRIPCION " +
                    " from REQUISICION as r " +
                    " inner join REQUISICION_DETALLE as rd on r.REQUISICION_ID = rd.REQUISICION_ID " +
                    " inner join PRODUCTO as p on rd.PRODUCTO_ID = p.PRODUCTO_ID  or rd.PRODUCTO_ID = p.PRODUCTO_MSP_ID" +
                    " inner join INVENTARIO_FRIMEX as ifr on r.ALMACEN_ID = ifr.INVENTARIO_FRIMEX_ID " +
                    " where r.REQUISICION_ESTATUS = '" + ESTATUS + "'"+where;
                cn.ConectarSQLServer();
                //SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                //cmdm.ExecuteNonQuery();
                // FbDataReader readerm = cmdm.ExecuteReader();
                SqlDataAdapter DA = new SqlDataAdapter(consulta, cn.SC);
                DA.Fill(_datos);

                DA.Dispose();
                //cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _datos;
        }
        public void CargarAlmacenes(ComboBox cBAlmacen,int USUARIOID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                cBAlmacen.DataSource = null;
                string cadena = "select infr.INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " inner join USU_ALM as ua on infr.INVENTARIO_FRIMEX_ID = ua.ALMACEN_ID " +
                    " inner join INVENTARIO_SUPERSACO as iss on iss.INVENTARIO_FRIMEX_ID=infr.INVENTARIO_FRIMEX_ID" +
                    " where USU_ALM_ESTATUS = 'A' and USUARIO_ID = " + USUARIOID;
                DataTable Table = new DataTable();
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
                SqlDataAdapter DA = new SqlDataAdapter(cadena, cn.SC);
                DA.Fill(Table);
                if (Table != null)
                {
                    cBAlmacen.DataSource = Table.DefaultView;
                    cBAlmacen.ValueMember = "INVENTARIO_FRIMEX_ID";
                    cBAlmacen.DisplayMember = "ALMACEN_MSP_DESCRIPCION";
                }
                DA.Dispose();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        public DataTable CargarDatosRequisicion(int REQUISICIONID,out string msg)
        {
            DataTable _Datos = new DataTable();
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select r.REQUISICION_ID,r.REQUISICION_FECHA_CREACION,r.REQUISICION_FECHA,p.PRODUCTO_ID,P.PRODUCTO_MSP_ID,p.PRODUCTO_DESCRIPCION " +
                      " ,REQUISICION_DETALLE_CANTIDAD,ifr.INVENTARIO_FRIMEX_ID,ifr.ALMACEN_MSP_DESCRIPCION,r.ALMACEN_DESTINO_ID " +
                      " from REQUISICION as r " +
                      " inner join REQUISICION_DETALLE as rd on r.REQUISICION_ID = rd.REQUISICION_ID " +
                      " inner join PRODUCTO as p on rd.PRODUCTO_ID = p.PRODUCTO_ID or rd.PRODUCTO_ID=P.PRODUCTO_ID" +
                      " inner join INVENTARIO_FRIMEX as ifr on r.ALMACEN_ID = ifr.INVENTARIO_FRIMEX_ID " +
                      " where r.REQUISICION_ESTATUS = 'A' AND R.REQUISICION_ID = " + REQUISICIONID;
                cn.ConectarSQLServer();
                SqlDataAdapter DA = new SqlDataAdapter(consulta, cn.SC);
                DA.Fill(_Datos);
                DA.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

            msg = msg_local;
            return _Datos;
        }
        public DataTable CargarDatosSupersaco(string SUPERSACOID, out string msg)
        {
            DataTable _Datos = new DataTable();
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select * from supersaco  as s " +
                    " inner join producto as p on s.producto_id = p.producto_msp_id or s.producto_id=p.producto_id" +
                    " where supersaco_id=" + SUPERSACOID;
                cn.ConectarSQLServer();
                SqlDataAdapter DA = new SqlDataAdapter(consulta, cn.SC);
                DA.Fill(_Datos);
                DA.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

            msg = msg_local;
            return _Datos;
        }

        public bool CambiarEstatusRequisicion(int REQUISICIONID, string ESTATUS,out string msg)
        {
            bool _exito = false;
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "UPDATE [dbo].[REQUISICION] " +
                   " SET[REQUISICION_ESTATUS] = '" + ESTATUS + "' " +
                   " WHERE[REQUISICION_ID] = " + REQUISICIONID;
                cn.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                if (cmd.UpdatedRowSource > 0)
                _exito = true;
                else
                    _exito = false;
                cmd.Cancel();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }
        public bool ValidaRequisicionCompletada(int REQUISICIONID,  out string msg)
        {
            bool _exito = false;
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select RD.PRODUCTO_ID,P.PRODUCTO_DESCRIPCION,RD.REQUISICION_DETALLE_CANTIDAD, " +
                    "Count(TD.SUPERSACO_ID) as CANT_SACOS_TRANSFERIDOS" +
                    " from REQ_TRA as RT" +
                    " inner join REQUISICION as R on RT.REQUISICION_ID = R.REQUISICION_ID" +
                    " inner join REQUISICION_DETALLE as RD on RD.REQUISICION_ID = R.REQUISICION_ID" +
                    " inner join TRANSFERENCIA as T on RT.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID" +
                    " inner join TRANSFERENCIA_DETALLE as TD on RT.TRANSFERENCIA_ID = TD.TRANSFERENCIA_ID" +
                    " inner join PRODUCTO as P on RD.PRODUCTO_ID = p.PRODUCTO_ID or RD.PRODUCTO_ID = p.PRODUCTO_MSP_ID" +
                    " where" +
                    " RT.REQUISICION_ID = " + REQUISICIONID +
                    " AND R.REQUISICION_ESTATUS = 'A'" +
                    " group by RD.PRODUCTO_ID,P.PRODUCTO_DESCRIPCION,RD.REQUISICION_DETALLE_CANTIDAD ";
                cn.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    int _SSSolicitados=0,_SSSurtidos=0;
                    while(reader.Read())
                    {
                        _SSSolicitados = Convert.ToInt32(reader["REQUISICION_DETALLE_CANTIDAD"]);
                        _SSSurtidos = Convert.ToInt32(reader["CANT_SACOS_TRANSFERIDOS"]);
                        if (_SSSurtidos == _SSSolicitados)
                            _exito = true;
                        else
                        {
                            _exito =false ;
                            break;
                        }
                    }
                }
                else
                    _exito = true;
                //if (_exito == false)
                //    CambiarEstatusRequisicion(REQUISICIONID, "T", out msg_local);
                cmd.Cancel();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }
        public bool ActualizarPesoSS(int SUPERSACO_ID,double SUPERSACO_CANTIDAD,int ALMACEN_ID,int USUARIO_ID, out string msg)
        {
            bool _exito = false;
            string consulta = "", msg_local = "", _Usuario_ID = "", 
                FECHA_ACTUALIZACION = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            ConexionSql cn = new ConexionSql();
            try
            {
                if (USUARIO_ID == 0)
                    _Usuario_ID = "NULL";
                consulta = "UPDATE [dbo].[SUPERSACO] " +
                   " SET[INVENTARIO_SUPERSACO_ID] = " + ALMACEN_ID +
                   " ,[SUPERSACO_CANTIDAD] = " + SUPERSACO_CANTIDAD +
                   //" ,[SUPERSACO_FOLIO_TRASNF] = " + TRANSFERENCIA_ID +
                   " ,[SUPERSACO_AUTORIZA_PESO] = " + _Usuario_ID +
                   " WHERE[SUPERSACO_ID]=" + SUPERSACO_ID;
                cn.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                //consulta = "INSERT INTO [dbo].[HIST_SS_PESO] " +
                //    " ([SUPERSACO_ID] " +
                //   " ,[SUPERSACO_CANTIDAD] " +
                //   " ,[FECHA_ACTUALIZACION] " +
                //   " ,[ALMACEN_ID] " +
                //   " ,[USUARIO_AUTORIZACION_ID]) " +
                //     " VALUES " +
                //   " ( " + SUPERSACO_ID +
                //   " ,'" + SUPERSACO_CANTIDAD + "'" +
                //   " ,'" + FECHA_ACTUALIZACION + "'" +
                //   " , " + ALMACEN_ID +
                //   " , " + _Usuario_ID + ")";

                //cmd = new SqlCommand(consulta, cn.SC);
                //cmd.ExecuteNonQuery();
                cmd.Cancel();
                cn.Desconectar();
                _exito = true;
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }

        public bool ActualizarFolioTrasferencia(List<string>SUPERSACOS,int TRANSFERENCIA_ID,out string msg)
        {

            bool _exito = false;
            string consulta = "", msg_local = "", _TRANSFERENCIA = TRANSFERENCIA_ID.ToString(),
                FECHA_ACTUALIZACION = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            ConexionSql cn = new ConexionSql();
            try
            {
                if (TRANSFERENCIA_ID == 0)
                    _TRANSFERENCIA = "NULL";
                SqlCommand cmd;
                for (int i = 0; i < SUPERSACOS.Count; i++)
                {
                    consulta = "UPDATE [dbo].[SUPERSACO] " +
                       " SET [SUPERSACO_FOLIO_TRASNF] = " + _TRANSFERENCIA +
                       " WHERE [SUPERSACO_ID]=" + SUPERSACOS[i];
                    cn.ConectarSQLServer();
                    cmd = new SqlCommand(consulta, cn.SC);
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
                cn.Desconectar();
                _exito = true;
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }
    }
}
