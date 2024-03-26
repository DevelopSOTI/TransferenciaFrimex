﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using System.Data.SqlClient;
using ApiBa = ApisMicrosip.ApiMspBasicaExt;
using ApiIn = ApisMicrosip.ApiMspInventExt;

namespace ApiInventarios
{
    public partial class ApiInvent : Form
    {
        
        public ApiInvent()
        {
            InitializeComponent();
        }
        //public bool EXITO { set; get; }
        //public int ALMACEN_ORIGEN { set; get; }
        //public int ALMACEN_DESTINO { set; get; }
        //public int PALLET_ID { set; get; }
        //public int PRODUCTOIDMSP { set; get; }
        //public double CANTIDAD { set; get; }
        //public double COSTO_STD { set; get; }
        string LOTE { set; get; }
        string TRANSFERENCIA_ID { set; get; }
        string CONCEPTO { set; get; }
        string FECHA_ASIGNACION { set; get; }
        string DESCRIPCION { set; get; }
        private void ApiInvent_Load(object sender, EventArgs e)
        {
            string msg_local = "",ruta_registros= "SOFTWARE\\SOTI\\FrimexTransferencias";
            try
            {
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros(ruta_registros);
                if (reg.TRANSFERENCIA_ID.Length > 0)
                {
                    //LOTE = Convert.ToString(reg.LOTE);
                    TRANSFERENCIA_ID = Convert.ToString(reg.TRANSFERENCIA_ID);
                    CONCEPTO = Convert.ToString(reg.CONCEPTO);
                    FECHA_ASIGNACION = Convert.ToString(reg.FECHA_ASIGNACION);
                    DESCRIPCION = Convert.ToString(reg.DESCRIPCION);
                    if (TransferenciaEntreAlmacenesMSP(Convert.ToInt32(reg.ALMACEN_ORIGEN)
                      , Convert.ToInt32(reg.ALMACEN_DESTINO)
                      , Convert.ToInt32(reg.TRANSFERENCIA_ID)
                      /*, Convert.ToInt32(reg.PRODUCTOIDMSP)
                      , Convert.ToDouble(reg.CANTIDAD)
                      , Convert.ToDouble(reg.COSTO_STD)*/
                      , out msg_local))
                    {
                        reg.EXITO = "true";
                        reg.EscribirRegistros("ALMACEN_ORIGEN", "", false, ruta_registros);
                        reg.EscribirRegistros("ALMACEN_DESTINO", "", false, ruta_registros);
                        reg.EscribirRegistros("SUPERSACO_ID", "", false, ruta_registros);
                        reg.EscribirRegistros("PRODUCTOIDMSP", "", false, ruta_registros);
                        reg.EscribirRegistros("CANTIDAD", "", false, ruta_registros);
                        reg.EscribirRegistros("COSTO_STD", "", false, ruta_registros);
                        reg.EscribirRegistros("MENSAJE_ERROR", msg_local, false, ruta_registros);
                    }
                    else
                        reg.EXITO = "false";
                }
                else
                    reg.EXITO = "false";
                reg.EscribirRegistros("EXITO", reg.EXITO.ToString(), false, ruta_registros);
            }
            catch(Exception Ex)
            {
                msg_local +="\n\r"+ Ex.Message;
            }
            if (msg_local.Length>0)
            {
                MessageBox.Show("Error al realizar la transferencia \n\r"+msg_local+".\n\r Hacer la transferencia manual","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            this.Close();
        }
        public int GET_SIGFOL(ConexionMicrosip micro, FbTransaction trans)
        {
            int docto_folio = 0;

            try
            {
                FbCommand cmd = new FbCommand("execute procedure GET_SIGFOL_CONCEPTO('IN', 36, 'S', 0)", micro.FBC, trans/**/);
                FbDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    docto_folio = Convert.ToInt32(read["CONSEC"].ToString());
                }
                read.Close();
                cmd.Dispose();
                docto_folio++;

                cmd = new FbCommand("execute procedure GET_SIGFOL_CONCEPTO('IN', 36, 'S', " + docto_folio + ")", micro.FBC, trans);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch //(Exception Ex)
            {
                docto_folio = 0;
            }

            return docto_folio;
        }
        public bool TransferenciaEntreAlmacenesMSP(int ALMACEN_ORIGEN, int ALMACEN_DESTINO, int TRANSFERENCIA_ID,/* int PRODUCTOIDMSP
            ,double CANTIDAD, double COSTO_STD,*/ out string msg)
        {
            List<string> _Datos = new List<string>();
            bool _exito = false;
            string msg_local = "", _descripcion = "TRANSFERENCIA " + TRANSFERENCIA_ID, _fecha = DateTime.Now.ToString("dd/MM/yyyy");
            int /*_concepto = 36,*/ _centro_costos = 0;
            try
            {
                ConexionMicrosip con_msp = new ConexionMicrosip();
                RegistrosWindows reg = new RegistrosWindows();
                FbTransaction trans;
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                trans = con_msp.FBC.BeginTransaction();
                string _numfol = GET_SIGFOL(con_msp, trans).ToString();
                if (_numfol.Length < 8)
                    while (_numfol.Length < 8)
                        _numfol = "0" + _numfol;
                string _folio = "S" + _numfol;
                trans.CommitRetaining();
                con_msp.Desconectar();
                int TR_Empresa, DB_Connect_E, DB_Inventario, DB_Empresa, API_RESULT, API_DETALLE,API_LOTES; /*API_ENCABEZADO, API_ENTRADA*/;

                StringBuilder ErrorMessage = new StringBuilder(300);
                #region NOS CONECTAMOS A LA EMPRESA MICROSIP            
                DB_Empresa = ApiBa.NewDB();
                TR_Empresa = ApiBa.NewTrn(DB_Empresa, 3);
                DB_Connect_E = ApiBa.DBConnect(DB_Empresa, reg.MICRO_SERVER + ":" + reg.MICRO_ROOT + "\\" + reg.MICRO_BD + ".FDB", "SYSDBA", reg.MICRO_PASS);
                if (DB_Connect_E != 0)
                {
                    ApiBa.GetLastErrorMessage(ErrorMessage);
                    msg_local += ErrorMessage.ToString();
                    _exito = false;
                }
                else
                #endregion

                #region ESTABLECEMOS LA BASE DE DATOS PARA LA API DE INVENTARIOS
                {
                    DB_Inventario = ApiIn.SetDBInventarios(DB_Empresa);
                    if (DB_Inventario != 0)
                    {
                        ApiIn.inGetLastErrorMessage(ErrorMessage);
                        msg_local += ErrorMessage.ToString();
                        ApiBa.DBDisconnect(DB_Empresa);
                        _exito = false;
                    }
                    else
                    #endregion

                    {
                        #region GENERAMOS EL ENCABEZADO DE LA ENTRADA
                        API_RESULT = ApiIn.NuevaSalida(Convert.ToInt32(reg.CONCEPTO), ALMACEN_ORIGEN, ALMACEN_DESTINO, _fecha, "", reg.DESCRIPCION, _centro_costos);

                        if (API_RESULT != 0)
                        {
                            ApiIn.inGetLastErrorMessage(ErrorMessage);
                            msg_local += ErrorMessage.ToString();
                            ApiIn.AbortaDoctoInventarios();
                            ApiBa.DBDisconnect(-1);
                            //trans.Rollback();
                            //con_msp.Desconectar();
                            _exito = false;
                        }
                        else
                        {
                            #endregion

                            #region GENERAMOS LOS RENGLONES DE LA SALIDA
                            //Buscar en la transferencia
                            /*string _consulta = "select S.SUPERSACO_ID,PNomb.PRODUCTO_MSP_ID,s.SUPERSACO_CANTIDAD as CANT,pocd.PRECIO" +
                                ",(s.SUPERSACO_CANTIDAD *pocd.PRECIO)AS COSTO_TOTAL,POC.FOLIO " +
                                " from TRANSFERENCIA as T " +
                                " inner join TRANSFERENCIA_DETALLE as TD on t.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
                                " inner join SUPERSACO as s on td.SUPERSACO_ID = s.SUPERSACO_ID " +
                                " inner join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                                " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                                " inner join PETICION_OC_DETALLE as pocd on poc.PETICION_OC_ID = pocd.PETICION_OC_ID " +
                                " inner join PRODUCTO as p on p.PRODUCTO_ID = s.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                                " inner join PRODUCTO as PNomb on p.PRODUCTO_DESCRIPCION=PNomb.PRODUCTO_DESCRIPCION" +
                                " where" +
                                " t.TRANSFERENCIA_ID = " + reg.TRANSFERENCIA_ID + " and PNomb.PRODUCTO_ESTATUS_ID=21 and PNomb.PRODUCTO_EMPRESA_ID=6142130 order by POC.FOLIO";*/
                            string _consulta = "select PNomb.PRODUCTO_MSP_ID,sum( s.SUPERSACO_CANTIDAD) as CANT,pocd.PRECIO,POC.FOLIO " +
                                " from TRANSFERENCIA as T " +
                                " inner join TRANSFERENCIA_DETALLE as TD on t.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
                                " inner join SUPERSACO as s on td.SUPERSACO_ID = s.SUPERSACO_ID " +
                                " inner join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                                " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                                " inner join PETICION_OC_DETALLE as pocd on poc.PETICION_OC_ID = pocd.PETICION_OC_ID " +
                                " inner join PRODUCTO as p on p.PRODUCTO_ID = s.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                                " inner join PRODUCTO as PNomb on p.PRODUCTO_DESCRIPCION = PNomb.PRODUCTO_DESCRIPCION " +
                                " where t.TRANSFERENCIA_ID = " + reg.TRANSFERENCIA_ID + " and PNomb.PRODUCTO_ESTATUS_ID=21 and PNomb.PRODUCTO_EMPRESA_ID=6142130 " +
                                " group by PNomb.PRODUCTO_MSP_ID,pocd.PRECIO,poc.FOLIO " +
                                " order by POC.FOLIO";
                            ConexionSql cn = new ConexionSql();
                            cn.ConectarSQLServer();
                            SqlCommand sqlCommand = new SqlCommand(_consulta, cn.SC);
                            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
                            DataTable _datos = new DataTable();
                            SqlDataAdapter _DA = new SqlDataAdapter(_consulta, cn.SC);
                            _DA.Fill(_datos);
                            int cont = 0;

                           /* DataTable _datos2 = new DataTable();
                            //Creamos las mismas columnas que tiene dataSet en Datos
                            foreach(DataColumn _columna in _datos.Columns)
                            {
                                _datos2.Columns.Add(_columna.ColumnName);
                            }
                            _datos2.Columns.Add("PESO_X_LOTE");
                            if (_datos.Rows.Count > 0)
                            {
                                foreach (DataRow row in _datos.Rows)
                                {
                                    string _lotes = "";
                                    int ARTICULO_ID = 0, SS_ID;
                                    string LOTE = "";
                                    double CANT = 0.0, COSTO_UNIT = 0.0, COSTO_TOTAL = 0.0;
                                    if (_datos2.Rows.Count == 0)
                                    {

                                        //SS_ID = Convert.ToInt32(row[0]);
                                        ARTICULO_ID = Convert.ToInt32(row["PRODUCTO_MSP_ID"]);
                                        CANT = Convert.ToDouble(row["CANT"]);
                                        //COSTO_UNIT = Convert.ToDouble(row[3]);
                                        //COSTO_TOTAL = Convert.ToDouble(row[4]);
                                        LOTE = Convert.ToString(row["FOLIO"]);

                                    }
                                    
                                    cont++;
                                }
                            }
                            cont = 0;*/
                            if (_datos.Rows.Count > 0)
                            {
                                foreach (DataRow row in _datos.Rows)
                                {
                                    int ARTICULO_ID = 0, SS_ID;
                                    string LOTE = "";
                                    double CANT = 0.0, COSTO_UNIT = 0.0, COSTO_TOTAL = 0.0;
                                    //SS_ID = Convert.ToInt32(row[0]);
                                    ARTICULO_ID = Convert.ToInt32(row["PRODUCTO_MSP_ID"]);
                                    CANT = Convert.ToDouble(row["CANT"]);
                                    //COSTO_UNIT = Convert.ToDouble(row[3]);
                                    //COSTO_TOTAL = Convert.ToDouble(row[4]);
                                    LOTE = Convert.ToString(row["FOLIO"]);
                                    API_DETALLE = ApiIn.RenglonSalida(ARTICULO_ID, CANT, 0, 0);

                                    if (API_DETALLE != 0)
                                    {
                                        ApiIn.inGetLastErrorMessage(ErrorMessage);
                                        msg_local += ErrorMessage.ToString();
                                        ApiIn.AbortaDoctoInventarios();
                                        ApiBa.DBDisconnect(-1);
                                        _exito = false;
                                        break;
                                    }
                                    #region GENERAR SALIDA POR LOTES
                                    if (LOTE.Length > 0)
                                    {
                                        API_LOTES = ApiIn.RenglonSalidaLotes(Convert.ToInt32(LOTE).ToString(), CANT);
                                        if (API_LOTES != 0)
                                        {
                                            ApiIn.inGetLastErrorMessage(ErrorMessage);
                                            msg_local += ErrorMessage.ToString();
                                            ApiIn.AbortaDoctoInventarios();
                                            ApiBa.DBDisconnect(-1);
                                            _exito = false;
                                            break;
                                        }
                                    }

                                    #endregion
                                    cont++;
                                }

                            }
                            #endregion

                            #region APLICAMOS LA SALIDA
                            API_RESULT = ApiIn.AplicaSalida();
                            if (API_RESULT != 0)
                            {
                                ApiIn.inGetLastErrorMessage(ErrorMessage);
                                msg_local += ErrorMessage.ToString();
                                ApiIn.AbortaDoctoInventarios();
                                ApiBa.DBDisconnect(-1);
                                //trans.Rollback();
                                //con_msp.Desconectar();

                                _exito = false;
                            }
                            else
                            {
                                _exito = true;
                                #endregion
                                ApiBa.TrnCommit(TR_Empresa);
                                ApiBa.DBDisconnect(-1);
                                //ApiBa.TrnCommit(DB_Empresa);
                                //ApiBa.DBDisconnect(DB_Empresa);
                                //trans.Commit();
                                //con_msp.Desconectar();
                                //ApiBa.li
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _exito;
        }
        
    }
}