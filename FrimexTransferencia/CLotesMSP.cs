﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace FrimexTransferencia
{
    class CLotesMSP
    {
        public bool ActualizarEstatusImpresion(string SS_ID)
        {
            bool _exito = false;
            string consulta = "", _reimpr = "";
            try
            {
                //Buscar cantidad recibida
                ConexionSql cn = new ConexionSql();
                consulta = "SELECT [SUPERSACO_IMPRESO] FROM SUPERSACO" +
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                cn.ConectarSQLServer();
                SqlCommand sqlCommand = new SqlCommand(consulta, cn.SC);
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        _reimpr = Convert.ToString(sqlReader["SUPERSACO_IMPRESO"]);
                    }
                }
                sqlReader.Close();
                sqlCommand.Dispose();
                cn.Desconectar();
                if (_reimpr == "")
                {
                    consulta = "UPDATE [dbo].[SUPERSACO] " +
                        " SET [SUPERSACO_IMPRESO] = 'S'"+
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                    cn.ConectarSQLServer();
                    sqlCommand = new SqlCommand(consulta, cn.SC);
                    if (sqlCommand.ExecuteNonQuery() > 0)
                        _exito = true;
                    else
                        _exito = false;
                    sqlCommand.Dispose();
                    cn.Desconectar();
                }
                else
                    _exito = false;
            }
            catch
            {
                _exito = false;
            }
            return _exito;
        }
        public bool ActualizarEstatusExcedente(string SS_ID)
        {
            bool _exito = false;
            string consulta = "";//, _reimpr = "",_excedente="";
            try
            {
                //Buscar supersaco a poner como excendente
                ConexionSql cn = new ConexionSql();
                consulta = "SELECT [SUPERSACO_IMPRESO],[SUPERSACO_EXCEDENTE] FROM SUPERSACO" +
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                cn.ConectarSQLServer();
                SqlCommand sqlCommand; //= new SqlCommand(consulta, cn.SC);
                //SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                //if (sqlReader.HasRows)
                //{
                //    while (sqlReader.Read())
                //    {
                //        _reimpr = Convert.ToString(sqlReader["SUPERSACO_IMPRESO"]);
                //        _excedente = Convert.ToString(sqlReader["SUPERSACO_EXCEDENTE"]);
                //    }
                //}
                //sqlReader.Close();
                //sqlCommand.Dispose();
                //cn.Desconectar();
                //if (_reimpr == ""||_reimpr=="N")
                //{
                    consulta = "UPDATE [dbo].[SUPERSACO] " +
                        " SET [SUPERSACO_EXCEDENTE] = 'S'" +
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                    cn.ConectarSQLServer();
                    sqlCommand = new SqlCommand(consulta, cn.SC);
                    if (sqlCommand.ExecuteNonQuery() > 0)
                        _exito = true;
                    else
                        _exito = false;
                    sqlCommand.Dispose();
                    cn.Desconectar();
                //}
                //else
                //    _exito = false;
            }
            catch
            {
                _exito = false;
            }
            return _exito;
        }
        public bool EsReimpresion(string SS_ID,string USUARIO_ID)
        {
            bool _exito = false;
            string consulta = "",_reimpr="";
            try
            {
                //Buscar cantidad recibida
                ConexionSql cn = new ConexionSql();
                consulta = "SELECT * FROM SUPERSACO WHERE SUPERSACO_ID=" + SS_ID;
                cn.ConectarSQLServer();
                SqlCommand sqlCommand = new SqlCommand(consulta, cn.SC);
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        _reimpr = Convert.ToString(sqlReader["SUPERSACO_IMPRESO"]);
                    }
                }
                sqlReader.Close();
                sqlCommand.Dispose();
                cn.Desconectar();
                if (_reimpr == "S")
                {
                    _exito = true;
                    consulta = "UPDATE [dbo].[SUPERSACO] " +
                        " SET [SUPERSACO_USU_REIMP] = " + USUARIO_ID +
                        " ,[SUPERSACO_FECHA_REIMP] = '" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                    cn.ConectarSQLServer();
                    sqlCommand = new SqlCommand(consulta, cn.SC);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    cn.Desconectar();
                }
                else if (_reimpr == "N"||_reimpr.Length==0)
                {
                    _exito = false;
                    consulta = "UPDATE [dbo].[SUPERSACO] " +
                        " SET [SUPERSACO_IMPRESO] = 'S'" +
                        " ,[SUPERSACO_USU_CREADOR]=" +USUARIO_ID+                        
                        " WHERE[SUPERSACO_ID] = " + SS_ID;
                    cn.ConectarSQLServer();
                    sqlCommand = new SqlCommand(consulta, cn.SC);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    cn.Desconectar();
                }
                else
                    _exito = false;

            }
            catch
            {
                _exito = false;
            }
            return _exito;
        }
        public bool EsExcedente(string Lote, string ProductoMSP,double SSCantidad)
        {
            bool _exito = false;
            try
            {
                string consulta = "";
                double _CantCompra = 0,_cantRecibida= 0;

                FbCommand fbComm;
                FbDataReader fbReader;
                ConexionMicrosip con_msp = new ConexionMicrosip();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                consulta = "select distinct " +
                    " cast(DCM.folio as integer) AS Folio_OC,  " +
                    " cast(DCMC.folio as integer) as Folio_Compra, " +
                    " AC.nombre as Producto_Compra,AC.Articulo_ID as Producto_ID,DCMDC.unidades as Cant_compra " +
                    " from doctos_cm as DCM " +
                    " inner join doctos_cm_ligas as dcml on DCM.docto_cm_id = dcml.docto_cm_fte_id " +
                    " inner join doctos_cm as DCMC on dcml.docto_cm_dest_id = DCMC.docto_cm_id " +
                    " inner join doctos_cm_det as DCMDC on DCMDC.docto_cm_id = DCMC.docto_cm_id " +
                    " inner join articulos as AC on DCMDC.Articulo_id = AC.Articulo_id " +
                    " inner join Elementos_cat_clasif as ECA on Eca.elemento_id = AC.articulo_id " +
                    " inner join clasificadores_cat_valores as CCV on ECA.valor_clasif_id = CCV.valor_clasif_id " +
                    " where " +
                    " DCMC.TIPO_DOCTO = 'C' " +
                    " and DCM.folio in (" + Lote + ")" +
                    " group by " +
                    " DCM.folio,DCMC.folio,AC.Articulo_ID ,AC.nombre ,DCMDC.unidades ,CCv.valor " +
                    " order by DCM.folio asc";
                fbComm = new FbCommand(consulta, con_msp.FBC);
                fbReader = fbComm.ExecuteReader();
                if(fbReader.HasRows)
                {
                    while(fbReader.Read())
                    {
                        _CantCompra = Convert.ToDouble(fbReader["Cant_compra"]);
                    }
                }
                fbReader.Close();
                fbComm.Dispose();
                con_msp.Desconectar();
                //Buscar cantidad de compra
                if (_CantCompra > 0)
                {
                    //Buscar cantidad recibida
                    ConexionSql cn = new ConexionSql();
                    consulta = "select sum(s.SUPERSACO_CANTIDAD) as CANT_RECIBIDA,p.producto_descripcion as Producto_desc " +
                        " from SUPERSACO as s " +
                        " full outer join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                        " full outer join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                        " full outer join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                        " where " +
                        " (FOLIO in (" + Lote + ") or LOTE_ID in (" + Lote + ")) " +
                        " and P.PRODUCTO_DESCRIPCION ='" + ProductoMSP+"'" +
                        " group by p.producto_descripcion";
                    cn.ConectarSQLServer();
                    SqlCommand sqlCommand = new SqlCommand(consulta, cn.SC);
                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                    if(sqlReader.HasRows)
                    {
                        while(sqlReader.Read())
                        {
                            _cantRecibida = Convert.ToDouble(sqlReader["CANT_RECIBIDA"]);
                        }
                    }
                    sqlReader.Close();
                    sqlCommand.Dispose();
                    cn.Desconectar();
                    if (_cantRecibida > _CantCompra)
                    {
                        //Actualizar columna de excedente
                        _exito = true;
                    }
                    else
                        _exito = false;
                }
                else
                    _exito = false;
            }
            catch
            {
                _exito = false;
            }
            return _exito;
        }
    }
}