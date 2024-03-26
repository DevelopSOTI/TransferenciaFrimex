﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using System.Data.SqlClient;
using System.Data;
using ApiBa = ApisMicrosip.ApiMspBasicaExt;
using ApiIn = ApisMicrosip.ApiMspInventExt;
using System.Diagnostics;

namespace FrimexTransferencia
{
    class C_FuncionesMSP
    {
        public List<string> DatosAlmacenMSP (string NOMBRE_ALMACEN, out string msg)
        {
            List<string> _Datos = new List<string>();
            string msg_local = "", consulta="";
            try
            {
                FbCommand fbComm;
                FbDataReader fbReader;
                ConexionMicrosip con_msp = new ConexionMicrosip();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                consulta = " select ALMACEN_ID, NOMBRE, NOMBRE_ABREV " +
                    " from ALMACENES where nombre = '"+NOMBRE_ALMACEN+"'";
                fbComm = new FbCommand(consulta, con_msp.FBC);
                fbReader = fbComm.ExecuteReader();
                if (fbReader.HasRows)
                {
                    while (fbReader.Read())
                    {
                        _Datos.Add(Convert.ToString(fbReader["ALMACEN_ID"]));
                        _Datos.Add(Convert.ToString(fbReader["NOMBRE"]));
                        _Datos.Add(Convert.ToString(fbReader["NOMBRE_ABREV"]));
                    }
                }
                fbReader.Close();
                fbComm.Dispose();
                con_msp.Desconectar();
            }
            catch(Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _Datos;
        }

        public bool TransferenciaEntreAlmacenesMSP(int ALMACEN_ORIGEN,int ALMACEN_DESTINO,int TransferenciaID, out string msg)
        {
            List<string> _Datos = new List<string>();
            bool _exito = false;
            string msg_local = "",_descripcion="TRANSFERENCIA "+TransferenciaID, _fecha =  DateTime.Now.ToString("dd/MM/yyyy");
            int _concepto = 36;
            //Crear la salida por la aplicacion externa
            RegistrosWindows _reg = new RegistrosWindows();
            string _ruta_registros = "SOFTWARE\\SOTI\\FrimexTransferencias";
            //Escribir en registros
            _reg.EscribirRegistros("EXITO", "", false, _ruta_registros);
            _reg.EscribirRegistros("TRANSFERENCIA_ID", Convert.ToString(TransferenciaID), false, _ruta_registros);//Transferencia id
            _reg.EscribirRegistros("CONCEPTO", _concepto.ToString(), false, _ruta_registros) ;
            _reg.EscribirRegistros("DESCRIPCION", _descripcion, false, _ruta_registros);
            _reg.EscribirRegistros("FECHA_ASIGNACION", _fecha, false, _ruta_registros);
            _reg.EscribirRegistros("ALMACEN_ORIGEN", ALMACEN_ORIGEN.ToString(), false, _ruta_registros) ;
            _reg.EscribirRegistros("ALMACEN_DESTINO", ALMACEN_DESTINO.ToString(), false, _ruta_registros);
            _reg.LeerRegistros(_ruta_registros);

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("ApiInventarios.exe");
            p.StartInfo = psi;
            p.Start();
            p.WaitForExit();
            _reg = new RegistrosWindows();
            _reg.LeerRegistros(_ruta_registros);
            string _error = Convert.ToString(_reg.MENSAJE_ERROR);
            if (String.IsNullOrEmpty(_error) && _reg.EXITO == "true")
            {
                _exito = true;
            }
            else
            {
                _exito = false;
            }
            #region Código comentado
            //try
            //{
            //    ConexionMicrosip con_msp = new ConexionMicrosip();
            //    RegistrosWindows reg = new RegistrosWindows();
            //    reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
            //    int TR_Empresa, DB_Connect_E, DB_Inventario, DB_Empresa, API_ENCABEZADO, API_DETALLE, API_ENTRADA,API_LOTES;
            //    StringBuilder ErrorMessage = new StringBuilder(300);
            //    #region NOS CONECTAMOS A LA EMPRESA MICROSIP            
            //    DB_Empresa = ApiBa.NewDB();
            //    TR_Empresa = ApiBa.NewTrn(DB_Empresa, 3);
            //    DB_Connect_E = ApiBa.DBConnect(DB_Empresa, reg.MICRO_SERVER + ":" + reg.MICRO_ROOT + "\\" + reg.MICRO_BD + ".FDB", "SYSDBA", reg.MICRO_PASS);
            //    if (DB_Connect_E != 0)
            //    {
            //        ApiBa.GetLastErrorMessage(ErrorMessage);
            //        msg_local += ErrorMessage.ToString();
            //        _exito = false;
            //    }
            //    else
            //    #endregion

            //    #region ESTABLECEMOS LA BASE DE DATOS PARA LA API DE INVENTARIOS
            //    {
            //        DB_Inventario = ApiIn.SetDBInventarios(DB_Empresa);
            //        if (DB_Inventario != 0)
            //        {
            //            ApiIn.inGetLastErrorMessage(ErrorMessage);
            //            msg_local += ErrorMessage.ToString();
            //            ApiBa.DBDisconnect(DB_Empresa);
            //            _exito = false;
            //        }
            //        else
            //        #endregion

            //        {
            //            string folio = TransferenciaID.ToString();
            //            while (folio.Length < 8)
            //            {
            //                folio = "0" + folio;
            //            }
            //            folio = "T" + folio;
            //            folio = "";
            //            #region GENERAMOS EL ENCABEZADO DE LA ENTRADA

            //            API_ENCABEZADO = ApiIn.NuevaSalida(_concepto, ALMACEN_ORIGEN, ALMACEN_DESTINO, _fecha, "", _descripcion, _centro_costos);

            //            if (API_ENCABEZADO != 0)
            //            {
            //                ApiIn.inGetLastErrorMessage(ErrorMessage);
            //                msg_local += ErrorMessage.ToString();
            //                ApiIn.AbortaDoctoInventarios();
            //                ApiBa.DBDisconnect(-1);

            //                _exito = false;
            //            }
            //            else
            //            {
            //                #endregion

            //                #region GENERAMOS LOS RENGLONES DE LA SALIDA
            //                //Buscar en la transferencia
            //                string _consulta = "select S.SUPERSACO_ID,PNomb.PRODUCTO_MSP_ID,s.SUPERSACO_CANTIDAD as CANT,pocd.PRECIO" +
            //                    ",(s.SUPERSACO_CANTIDAD *pocd.PRECIO)AS COSTO_TOTAL,POC.FOLIO " +
            //                    " from TRANSFERENCIA as T " +
            //                    " inner join TRANSFERENCIA_DETALLE as TD on t.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
            //                    " inner join SUPERSACO as s on td.SUPERSACO_ID = s.SUPERSACO_ID " +
            //                    " inner join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
            //                    " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
            //                    " inner join PETICION_OC_DETALLE as pocd on poc.PETICION_OC_ID = pocd.PETICION_OC_ID " +
            //                    " inner join PRODUCTO as p on p.PRODUCTO_ID = s.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
            //                    " inner join PRODUCTO as PNomb on p.PRODUCTO_DESCRIPCION=PNomb.PRODUCTO_DESCRIPCION " +
            //                    " where" +
            //                    " t.TRANSFERENCIA_ID = " + TransferenciaID + " and PNomb.PRODUCTO_ESTATUS_ID=21";
            //                ConexionSql cn = new ConexionSql();
            //                cn.ConectarSQLServer();
            //                SqlCommand sqlCommand = new SqlCommand(_consulta, cn.SC);
            //                //SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            //                DataTable _datos = new DataTable();
            //                SqlDataAdapter _DA = new SqlDataAdapter(_consulta, cn.SC);
            //                _DA.Fill(_datos);
            //                int cont = 0;
            //                if (_datos.Rows.Count>0)
            //                {
            //                    foreach(DataRow sqlReader in _datos.Rows )
            //                    {
            //                        int ARTICULO_ID = 0, SS_ID;
            //                        string LOTE = "";
            //                        double CANT = 0.0, COSTO_UNIT = 0.0, COSTO_TOTAL = 0.0;
            //                        SS_ID = Convert.ToInt32(sqlReader[0]);
            //                        ARTICULO_ID = Convert.ToInt32(sqlReader[1]);
            //                        CANT = Convert.ToDouble(sqlReader[2]);
            //                        COSTO_UNIT = Convert.ToDouble(sqlReader[3]);
            //                        COSTO_TOTAL = Convert.ToDouble(sqlReader[4]);
            //                        LOTE = Convert.ToString(sqlReader[5]);
            //                        API_DETALLE = ApiIn.RenglonSalida(ARTICULO_ID, CANT, 0, 0);

            //                        if (API_DETALLE != 0)
            //                        {
            //                            ApiIn.inGetLastErrorMessage(ErrorMessage);
            //                            msg_local += ErrorMessage.ToString();
            //                            ApiIn.AbortaDoctoInventarios();
            //                            ApiBa.DBDisconnect(-1);
            //                            _exito = false;
            //                            break;
            //                        }
            //                        #region GENERAR SALIDA POR LOTES
            //                        if (LOTE.Length > 0)
            //                        {
            //                            API_LOTES = ApiIn.RenglonSalidaLotes(Convert.ToInt32(LOTE).ToString(), CANT);
            //                            if (API_LOTES != 0)
            //                            {
            //                                ApiIn.inGetLastErrorMessage(ErrorMessage);
            //                                msg_local += ErrorMessage.ToString();
            //                                ApiIn.AbortaDoctoInventarios();
            //                                ApiBa.DBDisconnect(-1);
            //                                _exito = false;
            //                                break;
            //                            }
            //                        }

            //                        #endregion
            //                        cont++;
            //                    }

            //                }
            //                #endregion

            //                #region APLICAMOS LA SALIDA
            //                API_ENTRADA = ApiIn.AplicaSalida();
            //                if (API_ENTRADA != 0)
            //                {
            //                    ApiIn.inGetLastErrorMessage(ErrorMessage);
            //                    msg_local += ErrorMessage.ToString();
            //                    ApiIn.AbortaDoctoInventarios();
            //                    ApiBa.DBDisconnect(-1);

            //                    _exito = false;
            //                }
            //                else
            //                {
            //                    _exito = true;
            //                    #endregion
            //                    ApiBa.TrnCommit(TR_Empresa);
            //                    ApiBa.DBDisconnect(-1);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    msg_local = Ex.Message;
            //}
            #endregion;
            msg = msg_local;
            return _exito;
        }

    }
}
