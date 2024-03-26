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
    public partial class FRep : Form
    {
        public FRep()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();

        private void bGenerar_Click(object sender, EventArgs e)
        {
            string aux = cBSeleccionReporte.SelectedItem.ToString();
            if(aux.Length>0)
            {
                DateTime _fechaInicio = dTPInicio.Value, _fechaFin = dTPFin.Value;
                C_REPORTES _reporte = new C_REPORTES();
                        string consulta = "", msg_local = "", _periodo = "Del " + _fechaInicio.ToString("dd/MM/yyyy") + " al " + _fechaFin.ToString("dd/MM/yyyy"); ;


                if (aux == "Reporte de pesos de supersacos")
                    consulta = "SELECT S.SUPERSACO_ID,P.PRODUCTO_DESCRIPCION,HSSP.SUPERSACO_CANTIDAD as CANT_ANTERIOR,s.SUPERSACO_CANTIDAD as ULTIMA_CANT, hssp.FECHA_ACTUALIZACION " +
                                    " FROM SUPERSACO AS S " +
                                    " INNER JOIN PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.SUPERSACO_ID = P.PRODUCTO_MSP_ID " +
                                    " INNER JOIN HIST_SS_PESO AS HSSP ON S.SUPERSACO_ID = HSSP.SUPERSACO_ID " +
                                    " where " +
                                    " HSSP.SUPERSACO_CANTIDAD<> S.SUPERSACO_CANTIDAD " +
                                    //" or HSSP.SUPERSACO_CANTIDAD = s.SUPERSACO_CANTIDAD " +
                                    " and HSSP.FECHA_ACTUALIZACION between '" + _fechaInicio.ToString("dd/MM/yyyy") + " " + dTPInicioHora.Value.ToString("HH:mm:ss") +
                                    "' AND '" + _fechaFin.ToString("dd/MM/yyyy") + " " + dTPFechaFinHora.Value.ToString("HH:mm:ss") + "'";
                else if (aux == "Reporte de transferencias detallado")
                    consulta = "Select R.REQUISICION_ID as [NO REQUISICION],R.REQUISICION_FECHA AS [FECHA REQUISICION],IVO.ALMACEN_MSP_DESCRIPCION as [ALMACEN ORIGEN] " +
                            " ,IVD.ALMACEN_MSP_DESCRIPCION AS[ALMACEN DESTINO], T.TRANSFERENCIA_ID AS[NO TRANSFERENCIA], t.TRANSFERENCIA_FECHA AS[FECHA TRANSFERENCIA] " +
                            " , S.SUPERSACO_ID AS SUPERSACO,P.PRODUCTO_DESCRIPCION AS PRODUCTO,S.SUPERSACO_CANTIDAD AS CANTIDAD " +
                            " from TRANSFERENCIA as T " +
                            " inner join TRANSFERENCIA_DETALLE as TD on Td.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID " +
                            " inner join SUPERSACO as s on Td.SUPERSACO_ID = s.SUPERSACO_ID " +
                            " inner join PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.PRODUCTO_ID = P.PRODUCTO_MSP_ID " +
                            " inner join INVENTARIO_FRIMEX as IVO on T.ALMACEN_ORIGEN_ID = IVO.INVENTARIO_FRIMEX_ID " +
                            " inner join INVENTARIO_FRIMEX as IVD on T.ALMACEN_DESTINO_ID = IVD.INVENTARIO_FRIMEX_ID " +
                            " INNER JOIN REQ_TRA AS RT ON RT.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID " +
                            " INNER JOIN REQUISICION AS R ON RT.REQUISICION_ID = R.REQUISICION_ID " +
                            " INNER JOIN REQUISICION_DETALLE AS RD ON R.REQUISICION_ID = RD.REQUISICION_ID " +
                            " where T.TRANSFERENCIA_FECHA between '" + _fechaInicio.ToString("dd/MM/yyyy") + " " + dTPInicioHora.Value.ToString("HH:mm:ss") +
                            "' AND '" + _fechaFin.ToString("dd/MM/yyyy") + " " + dTPFechaFinHora.Value.ToString("HH:mm:ss") + "'";
                else if (aux == "Reporte Supersacos disponibles por ubicacion")
                    consulta = "select ifo.ALMACEN_MSP_DESCRIPCION,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID " +
                        " ,s.SUPERSACO_CANTIDAD,s.SUPERSACO_FECHA,poc.FOLIO,s.LOTE_ID " +
                        " from  SUPERSACO as s " +
                        " inner join PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.PRODUCTO_ID = P.PRODUCTO_MSP_ID " +
                        " inner join INVENTARIO_SUPERSACO as iss on s.INVENTARIO_SUPERSACO_ID = iss.INVENTARIO_SUPERSACO_ID " +
                        " inner join INVENTARIO_FRIMEX as ifo on iss.INVENTARIO_FRIMEX_ID = ifo.INVENTARIO_FRIMEX_ID " +
                        " left join EMBARQUE as e on s.EMBARQUE_ID=e.EMBARQUE_ID" +
                        " left join PETICION_OC as POC on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID" +
                        " where s.SUPERSACO_ESTATUS = 'A' AND S.SUPERSACO_CANTIDAD>=10" +
                        " and  Cast(S.SUPERSACO_FECHA as date)between '31-12-2019' and '" + dTPInicio.Value.ToString("dd/MM/yyyy") +
                        "' order by ALMACEN_MSP_DESCRIPCION,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID";
                else if (aux == "Reporte tranferencias por rango de fechas")
                    consulta = "select IVO.ALMACEN_MSP_DESCRIPCION AS ORIGEN,IVD.ALMACEN_MSP_DESCRIPCION AS DESTINO,t.TRANSFERENCIA_ID " +
                        " ,P.PRODUCTO_DESCRIPCION,COUNT(tD.SUPERSACO_ID) AS CANT_SUPERSACOS " +
                        " from TRANSFERENCIA AS T " +
                        " inner join TRANSFERENCIA_DETALLE as TD on Td.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID " +
                        " inner join INVENTARIO_FRIMEX as IVO on T.ALMACEN_ORIGEN_ID = IVO.INVENTARIO_FRIMEX_ID " +
                        " inner join INVENTARIO_FRIMEX as IVD on T.ALMACEN_DESTINO_ID = IVD.INVENTARIO_FRIMEX_ID " +
                        " INNER JOIN SUPERSACO AS S ON TD.SUPERSACO_ID = S.SUPERSACO_ID " +
                        " INNER JOIN PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.SUPERSACO_ID = P.PRODUCTO_MSP_ID " +
                        " where " +
                        " T.TRANSFERENCIA_FECHA between '" + _fechaInicio.ToString("dd/MM/yyyy") + " " + dTPInicioHora.Value.ToString("HH:mm:ss") +
                        "' AND '" + _fechaFin.ToString("dd/MM/yyyy") + " " + dTPFechaFinHora.Value.ToString("HH:mm:ss") + "'" +
                        " GROUP BY IVO.ALMACEN_MSP_DESCRIPCION ,IVD.ALMACEN_MSP_DESCRIPCION ,t.TRANSFERENCIA_ID,P.PRODUCTO_DESCRIPCION " +
                        " ORDER BY T.TRANSFERENCIA_ID";
                else if (aux == "Reporte diferencias de pesos de supersaco")
                    consulta = "select HSSP.HIST_SS_PESO_ID,S.SUPERSACO_ID,P.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD as CANTIDAD_ACTUAL,s.SUPERSACO_FECHA as FECHA_CREACION " +
                        ",HSSP.SUPERSACO_CANTIDAD as CANTIDAD_HIST,HSSP.FECHA_ACTUALIZACION,IFR.ALMACEN_MSP_DESCRIPCION AS ALMACEN " +
                        "from HIST_SS_PESO as HSSP " +
                        "inner join supersaco as s on HSSP.SUPERSACO_ID = s.SUPERSACO_ID " +
                        "inner join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                        "inner join INVENTARIO_SUPERSACO as ISS on HSSP.ALMACEN_ID = ISS.INVENTARIO_SUPERSACO_ID " +
                        "inner join INVENTARIO_FRIMEX as IFR on ISS.INVENTARIO_FRIMEX_ID = IFR.INVENTARIO_FRIMEX_ID " +
                        "where " +
                        "HSSP.FECHA_ACTUALIZACION between '" + _fechaInicio.ToString("dd/MM/yyyy") + " " + dTPInicioHora.Value.ToString("HH:mm:ss") +
                        "' AND '" + _fechaFin.ToString("dd/MM/yyyy") + " " + dTPFechaFinHora.Value.ToString("HH:mm:ss") + "'" +
                        " ORDER BY P.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID";
                else if (aux == "Reporte diferencias de pesos de supersacos por transferencias")//Reporte nuevo
                    consulta = "Select A.TRANSFERENCIA_ID, a.SUPERSACO_ID,a.PRODUCTO_DESCRIPCION,a.FECHA_CREACION,a.PESO_CREACION " +
                        " ,a.PESO_SALIDA,A.PESO_CREACION - A.PESO_SALIDA AS DIF_PESO_CRE_SAL, a.PESO_LLEGADA " +
                        " ,a.PESO_SALIDA - a.PESO_LLEGADA as DIF_PESO_SAL_LLE ,a.PESO_ACTUAL, a.PESO_ACTUAL - a.PESO_CREACION as DIF_PESO_CRE_ACT " +
                        " from(Select t.TRANSFERENCIA_ID, s.SUPERSACO_ID, p.PRODUCTO_DESCRIPCION, s.SUPERSACO_FECHA AS FECHA_CREACION, isnull((" +
                        " select top 1 HSSP.SUPERSACO_CANTIDAD from HIST_SS_PESO as HSSP " +
                        " where HSSP.SUPERSACO_ID = s.SUPERSACO_ID " +
                        " ), 0) AS PESO_CREACION, isnull((" +
                        " select top 1 HSSP.SUPERSACO_CANTIDAD from HIST_SS_PESO as HSSP where HSSP.SUPERSACO_ID = s.SUPERSACO_ID and cast(HSSP.FECHA_ACTUALIZACION as date) = cast(t.TRANSFERENCIA_FECHA as date) " +
                        " ),0) AS PESO_SALIDA, isnull((" +
                        " select top 1 HSSP.SUPERSACO_CANTIDAD from HIST_SS_PESO as HSSP where HSSP.SUPERSACO_ID = s.SUPERSACO_ID and cast(HSSP.FECHA_ACTUALIZACION as date) = cast(t.TRANSFERENCIA_FECHA as date) ORDER BY HSSP.FECHA_ACTUALIZACION ASC " +
                        " ),0) AS PESO_LLEGADA, s.SUPERSACO_CANTIDAD as PESO_ACTUAL " +
                        " from TRANSFERENCIA as t " +
                        " inner join TRANSFERENCIA_DETALLE as td on t.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
                        " inner join SUPERSACO as s on td.SUPERSACO_ID = s.SUPERSACO_ID " +
                        " inner join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                        " where " +
                        " (t.TRANSFERENCIA_ESTATUS in ('T', 'A')) and " +
                        "  (t.TRANSFERENCIA_FECHA between '" + _fechaInicio.ToString("dd/MM/yyyy") + " " + dTPInicioHora.Value.ToString("HH:mm:ss") +
                        "' AND '" + _fechaFin.ToString("dd/MM/yyyy") + " " + dTPFechaFinHora.Value.ToString("HH:mm:ss") + "') " +
                        " ) as A " +
                        " order by A.TRANSFERENCIA_ID";
                else if (aux == "Reporte de existencia de supersacos en fecha especifica")//Reporte nuevo
                {
                    int _inventario_frimex_id = 0;
                    DataRowView _dRVAlmacen = (DataRowView)cBAlmacenes.SelectedItem;
                    _inventario_frimex_id = Convert.ToInt32(Convert.ToString(_dRVAlmacen.Row.ItemArray[0]));
                    if (_inventario_frimex_id > 0)
                    {
                        consulta = "select ifo.ALMACEN_MSP_DESCRIPCION,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID,s.SUPERSACO_ESTATUS " +
                            " ,s.SUPERSACO_CANTIDAD,s.SUPERSACO_FECHA,poc.FOLIO,s.LOTE_ID,opss.FECHA_ASIGNACION,ifo2.ALMACEN_MSP_DESCRIPCION as ALMACEN_MSP_DESCRIPCION2 " +
                            " from SUPERSACO as s " +
                            " inner join PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.PRODUCTO_ID = P.PRODUCTO_MSP_ID " +
                            " inner join INVENTARIO_SUPERSACO as iss on s.INVENTARIO_SUPERSACO_ID = iss.INVENTARIO_SUPERSACO_ID " +
                            " inner join INVENTARIO_FRIMEX as ifo on iss.INVENTARIO_FRIMEX_ID = ifo.INVENTARIO_FRIMEX_ID " +
                            " left join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                            " left join PETICION_OC as POC on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                            " left join OP_SS as opss on opss.SUPERSACO_ID = s.SUPERSACO_ID " +
                            " left join TRANSFERENCIA_DETALLE as TD on s.SUPERSACO_ID = td.SUPERSACO_ID " +
                            " left join TRANSFERENCIA as T on TD.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID " +
                            " left join INVENTARIO_FRIMEX as ifo2 on t.ALMACEN_ORIGEN_ID = ifo2.INVENTARIO_FRIMEX_ID " +
                            " where " +
                            " ((s.SUPERSACO_ESTATUS = 'A'AND iss.INVENTARIO_FRIMEX_ID="+_inventario_frimex_id+" AND S.SUPERSACO_CANTIDAD >= 10 " +
                            " and  Cast(S.SUPERSACO_FECHA as date)between '31-12-2019' and '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "' /*and ifo2.ALMACEN_MSP_DESCRIPCION is null*/) " +
                            " or(s.SUPERSACO_ESTATUS = 'B'AND iss.INVENTARIO_FRIMEX_ID="+_inventario_frimex_id+ " AND S.SUPERSACO_CANTIDAD >= 10 and Cast(opss.FECHA_ASIGNACION as date) > '" + dTPInicio.Value.ToString("dd/MM/yyyy") +
                            "' and  Cast(S.SUPERSACO_FECHA as date)between '31-12-2019' and '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "' /*and ifo2.ALMACEN_MSP_DESCRIPCION is null*/)) " +

                            " or ((s.SUPERSACO_ESTATUS = 'A'AND T.ALMACEN_ORIGEN_ID=" + _inventario_frimex_id + " AND S.SUPERSACO_CANTIDAD >= 10 and Cast(T.TRANSFERENCIA_FECHA as date) > '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "'" +
                            " and  Cast(S.SUPERSACO_FECHA as date)between '31-12-2019' and '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "') " +
                            " or  (s.SUPERSACO_ESTATUS = 'B'AND T.ALMACEN_ORIGEN_ID=" + _inventario_frimex_id + " AND S.SUPERSACO_CANTIDAD >= 10 and Cast(T.TRANSFERENCIA_FECHA as date) > '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "' and opss.FECHA_ASIGNACION >= '" + dTPInicio.Value.ToString("dd/MM/yyyy") +
                            "' and  Cast(S.SUPERSACO_FECHA as date)between '31-12-2019' and '" + dTPInicio.Value.ToString("dd/MM/yyyy") + "'))"+

                            " order by ifo.ALMACEN_MSP_DESCRIPCION,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID; ";
                    }
                }
                if (_fechaInicio < _fechaFin||aux== "Reporte Supersacos disponibles por ubicacion"||aux== "Reporte de existencia de supersacos en fecha especifica")
                {
                    DialogResult _resp = MessageBox.Show("¿Desea exportar la infomacion a Excel?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_resp == DialogResult.Yes)
                    {
                        try
                        {
                             ConexionSql con = new ConexionSql();
                            try
                            {
                                con.ConectarSQLServer();
                            }
                            catch (Exception ex)
                            {
                                msg_local = ex.Message;
                                MessageBox.Show("Hubo un error al conectar con la base de datos, verifique la red", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            SqlDataAdapter _da = new SqlDataAdapter(consulta, con.SC);
                            DataTable datos = new DataTable();
                            _da.Fill(datos);
                            _periodo = "Al " + _fechaInicio.ToString("dd/MM/yyyy");
                            if (aux == "Reporte Supersacos disponibles por ubicacion")
                            {
                                DataTable _datosFolio = new DataTable();
                                //Agrupar FOLIO  ALMACEN	SUPERSACO_ID	PRODUCTO	PESO_KG	FOLIO	LOTE_ID	FECHA_CREACION

                                _datosFolio.Columns.Add("ALMACEN", typeof(string));
                                _datosFolio.Columns.Add("SUPERSACO_ID", typeof(int));
                                _datosFolio.Columns.Add("PRODUCTO", typeof(string));
                                _datosFolio.Columns.Add("CANTIDAD", typeof(string));
                                _datosFolio.Columns.Add("FOLIO", typeof(string));
                                _datosFolio.Columns.Add("FECHA_CREACION", typeof(DateTime));
                                foreach (DataRow row in datos.Rows)
                                {
                                    int _SSID = 0, _FOLIO = 0;
                                    string _CANTIDAD ="";
                                    string  _FECHA = "",_ALMACEN = "",_aux = "", _PRODDESC = "";

                                    if (string.IsNullOrEmpty(Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"])))
                                        _ALMACEN = "";
                                    else
                                        _ALMACEN = Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"]);

                                    if (string.IsNullOrEmpty(row["SUPERSACO_ID"].ToString()))
                                        _SSID = -1;
                                    else
                                       _SSID = Convert.ToInt32(row["SUPERSACO_ID"]);

                                    if (string.IsNullOrEmpty(row["SUPERSACO_CANTIDAD"].ToString()))
                                        _CANTIDAD = "";
                                    else
                                        _CANTIDAD = Convert.ToDouble(row["SUPERSACO_CANTIDAD"]).ToString("N2");
                                    
                                    _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]);

                                    _aux = Convert.ToString(row["FOLIO"]);
                                    if (_aux.Length == 0)
                                        _aux = Convert.ToString(row["LOTE_ID"]);
                                    _FOLIO = Convert.ToInt32(_aux);

                                    if (string.IsNullOrEmpty(Convert.ToString(row["SUPERSACO_FECHA"])))
                                        _FECHA = "";
                                    else
                                        _FECHA = Convert.ToDateTime(row["SUPERSACO_FECHA"]).ToString("dd/MM/yyyy HH:mm:ss");//.Replace("p. m.","p.m.").Replace("a. m.", "a.m.");
                                   

                                    _datosFolio.Rows.Add(_ALMACEN, _SSID, _PRODDESC, _CANTIDAD, _FOLIO, _FECHA);
                                }
                                datos = new DataTable();
                                datos =_datosFolio;
                            }
                            else if(aux== "Reporte diferencias de pesos de supersaco")
                            {
                                //Cargar datos 
                                _periodo = "Del " + _fechaInicio.ToString("dd/MM/yyyy") + " al " + _fechaFin.ToString("dd/MM/yyyy");
                                DataTable _datosFolio = new DataTable();
                                //Agrupar HIST_SS_PESO_ID	SUPERSACO_ID	PRODUCTO_DESCRIPCION	CANTIDAD_ACTUAL	FECHA_CREACION	CANTIDAD_HIST	FECHA_ACTUALIZACION	ALMACEN	FECHA_ACTUALIZACION

                                //_datosFolio.Columns.Add("HIST_SS_PESO_ID", typeof(string));//
                                _datosFolio.Columns.Add("SUPERSACO ID", typeof(string));//
                                _datosFolio.Columns.Add("PRODUCTO DESCRIPCION", typeof(string));//
                                _datosFolio.Columns.Add("CANTIDAD ACTUAL", typeof(string));//
                                _datosFolio.Columns.Add("FECHA CREACION", typeof(string));//
                                _datosFolio.Columns.Add("CANTIDAD HIST", typeof(string));//
                                _datosFolio.Columns.Add("FECHA ACTUALIZACION", typeof(string));
                                _datosFolio.Columns.Add("ALMACEN", typeof(string));//
                                string _SSID_ANTERIOR = "";
                                foreach (DataRow row in datos.Rows)
                                {
                                    //Cambiar a las columnas correctas
                                     
                                    string _CANTIDAD = "", _CANTIDAD_HIST="";
                                    string _SSID = "",_FECHA = "", _FECHA_ACTUALIZACION = "", _ALMACEN = "", _aux = "", _PRODDESC = "";//, _HIST_SS_PESO_ID="";

                                    //if (string.IsNullOrEmpty(Convert.ToString(row["HIST_SS_PESO_ID"])))
                                    //    _HIST_SS_PESO_ID = "";
                                    //else
                                    //    _HIST_SS_PESO_ID = Convert.ToString(row["HIST_SS_PESO_ID"]);

                                    if (string.IsNullOrEmpty(row["SUPERSACO_ID"].ToString()))
                                        _SSID = "";
                                    else
                                        _SSID = Convert.ToString(row["SUPERSACO_ID"]);
                                        _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]);

                                    if (string.IsNullOrEmpty(row["CANTIDAD_ACTUAL"].ToString()))
                                        _CANTIDAD = "0.00";
                                    else
                                        _CANTIDAD = Convert.ToDouble(row["CANTIDAD_ACTUAL"]).ToString("N2");

                                    if (string.IsNullOrEmpty(Convert.ToString(row["FECHA_CREACION"])))
                                        _FECHA = "";
                                    else
                                        _FECHA = Convert.ToDateTime(row["FECHA_CREACION"]).ToString("dd/MM/yyyy hh:mm:ss tt");//.Replace("p. m.","p.m.").Replace("a. m.", "a.m.");

                                    if (string.IsNullOrEmpty(row["CANTIDAD_HIST"].ToString()))
                                        _CANTIDAD_HIST = "0.00";
                                    else
                                        _CANTIDAD_HIST = Convert.ToDouble(row["CANTIDAD_HIST"]).ToString("N2");

                                    if (string.IsNullOrEmpty(Convert.ToString(row["FECHA_ACTUALIZACION"])))
                                        _FECHA_ACTUALIZACION = "";
                                    else
                                        _FECHA_ACTUALIZACION = Convert.ToDateTime(row["FECHA_ACTUALIZACION"]).ToString("dd/MM/yyyy hh:mm:ss tt");//.Replace("p. m.","p.m.").Replace("a. m.", "a.m.");

                                    if (string.IsNullOrEmpty(Convert.ToString(row["ALMACEN"])))
                                        _ALMACEN = "";
                                    else
                                        _ALMACEN = Convert.ToString(row["ALMACEN"]);


                                    if (_SSID != _SSID_ANTERIOR)
                                    {
                                        _SSID_ANTERIOR = _SSID;
                                        _CANTIDAD_HIST= _FECHA_ACTUALIZACION= _ALMACEN = "";
                                    }
                                    else
                                    {
                                        _PRODDESC = _SSID = _CANTIDAD = _FECHA = "";
                                    }

                                    _datosFolio.Rows.Add( _SSID, _PRODDESC, _CANTIDAD, _FECHA, _CANTIDAD_HIST, _FECHA_ACTUALIZACION, _ALMACEN);
                                }
                                datos = new DataTable();
                                datos = _datosFolio;


                            }
                            else if (aux== "Reporte de existencia de supersacos en fecha especifica")
                            {
                                DataTable _datosFolio = new DataTable();
                                //Agrupar FOLIO  ALMACEN	SUPERSACO_ID	PRODUCTO	PESO_KG	FOLIO	LOTE_ID	FECHA_CREACION

                                _datosFolio.Columns.Add("ALMACEN", typeof(string));
                                _datosFolio.Columns.Add("SUPERSACO_ID", typeof(int));
                                _datosFolio.Columns.Add("PRODUCTO", typeof(string));
                                _datosFolio.Columns.Add("CANTIDAD", typeof(string));
                                _datosFolio.Columns.Add("FOLIO", typeof(string));
                                _datosFolio.Columns.Add("FECHA_CREACION", typeof(string));
                                foreach (DataRow row in datos.Rows)
                                {
                                    int _SSID = 0, _FOLIO = 0;
                                    string _CANTIDAD = "";
                                    string _FECHA = "", _ALMACEN = "", _aux = "", _PRODDESC = "";

                                    if (string.IsNullOrEmpty(Convert.ToString(row["ALMACEN_MSP_DESCRIPCION2"])))
                                        _ALMACEN = Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"]); 
                                    else
                                        _ALMACEN = Convert.ToString(row["ALMACEN_MSP_DESCRIPCION"]);

                                    if (string.IsNullOrEmpty(row["SUPERSACO_ID"].ToString()))
                                        _SSID = -1;
                                    else
                                        _SSID = Convert.ToInt32(row["SUPERSACO_ID"]);

                                    if (string.IsNullOrEmpty(row["SUPERSACO_CANTIDAD"].ToString()))
                                        _CANTIDAD = "";
                                    else
                                        _CANTIDAD = Convert.ToDouble(row["SUPERSACO_CANTIDAD"]).ToString("N2");

                                    _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]);

                                    _aux = Convert.ToString(row["FOLIO"]);
                                    if (_aux.Length == 0)
                                        _aux = Convert.ToString(row["LOTE_ID"]);
                                    _FOLIO = Convert.ToInt32(_aux);

                                    if (string.IsNullOrEmpty(Convert.ToString(row["SUPERSACO_FECHA"])))
                                        _FECHA = "";
                                    else
                                        _FECHA = Convert.ToDateTime(row["SUPERSACO_FECHA"]).ToString("dd/MM/yyyy hh:mm:ss tt");//.Replace("p. m.","p.m.").Replace("a. m.", "a.m.");


                                    _datosFolio.Rows.Add(_ALMACEN, _SSID, _PRODDESC, _CANTIDAD, _FOLIO, _FECHA);
                                }
                                datos = new DataTable();
                                datos = _datosFolio;
                            }
                                if (datos.Rows.Count > 0)
                            {
                                DataGridView _datosConsulta = new DataGridView();
                                //asingar datos de la consulta al nuevo grid
                                foreach (DataColumn _columna in datos.Columns)
                                {
                                    _datosConsulta.Columns.Add(_columna.ColumnName, _columna.ColumnName);
                                }
                                int j = 0;
                                foreach (DataRow _fila in datos.Rows)
                                {
                                    _datosConsulta.Rows.Add();
                                    Color color = Color.White;
                                    if (j % 2 == 0)
                                        color = Color.White;
                                    else
                                        color = Color.LightCyan;
                                    _datosConsulta.Rows[j].DefaultCellStyle.BackColor = color;
                                    for (int i = 0; i < datos.Columns.Count; i++)
                                    {
                                        string aux2 = Convert.ToString(_fila[i]);
                                        if (aux2.Length == 0)
                                            aux2 = "";
                                        _datosConsulta[i, j].Value = aux2;
                                    }
                                    j++;
                                }
                                _datosConsulta.Rows.Add();
                                string ruta = _reporte.ExportarDataGridViewExcel(aux, "", pb, sFDGuardar, _datosConsulta, _Usuario.USUARIO, _periodo, out msg_local);
                                if (ruta.Length > 0)
                                    MessageBox.Show("Archivo generado con éxito\n\r\"" + ruta + "\"", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                MessageBox.Show("Sin datos encontrados", "Información");
                        }
                        catch (Exception Ex)
                        {
                            msg_local = Ex.Message;
                        }
                        if (msg_local.Length > 0)
                            MessageBox.Show(msg_local, "Error");
                    }
                }
            }
        }
        private void PesosSS()
        {
            DateTime _fechaInicio = dTPInicio.Value, _fechaFin = dTPFin.Value;
            if (_fechaInicio < _fechaFin)
            {
                DialogResult _resp = MessageBox.Show("¿Desea exportar la infomacion a Excel?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_resp == DialogResult.Yes)
                {
                    C_REPORTES _reporte = new C_REPORTES();
                    string consulta = "", msg_local = "", _periodo = "Del " + _fechaInicio.ToString("dd/MM/yyyy") + " al " + _fechaFin.ToString("dd/MM/yyyy"); ;

                    try
                    {
                        consulta = "SELECT S.SUPERSACO_ID,P.PRODUCTO_DESCRIPCION,HSSP.SUPERSACO_CANTIDAD as CANT_ANTERIOR,s.SUPERSACO_CANTIDAD as ULTIMA_CANT, hssp.FECHA_ACTUALIZACION " +
                            " FROM SUPERSACO AS S " +
                            " INNER JOIN PRODUCTO AS P ON S.PRODUCTO_ID = P.PRODUCTO_ID OR S.SUPERSACO_ID = P.PRODUCTO_MSP_ID " +
                            " INNER JOIN HIST_SS_PESO AS HSSP ON S.SUPERSACO_ID = HSSP.SUPERSACO_ID " +
                            " where " +
                            " HSSP.SUPERSACO_CANTIDAD<> S.SUPERSACO_CANTIDAD " +
                            //" or HSSP.SUPERSACO_CANTIDAD = s.SUPERSACO_CANTIDAD " +
                            " and HSSP.FECHA_ACTUALIZACION between '"+ _fechaInicio.ToString("dd/MM/yyyy")+" " +dTPInicioHora.Value.ToString("HH:mm:ss")+
                            "' AND '" +_fechaFin.ToString("dd/MM/yyyy")+ " "+dTPFechaFinHora.Value.ToString("HH:mm:ss")+"'";
                        ConexionSql con = new ConexionSql();
                        try
                        {
                            con.ConectarSQLServer();
                        }
                        catch (Exception ex)
                        {
                            msg_local = ex.Message;
                            MessageBox.Show("Hubo un error al conectar con la base de datos, verifique la red", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        SqlDataAdapter _da = new SqlDataAdapter(consulta, con.SC);
                        DataTable datos = new DataTable();
                        _da.Fill(datos);
                        if (datos.Rows.Count > 0)
                        {
                            DataGridView _datosConsulta = new DataGridView();
                            //asingar datos de la consulta al nuevo grid
                            foreach (DataColumn _columna in datos.Columns)
                            {
                                _datosConsulta.Columns.Add(_columna.ColumnName, _columna.ColumnName);
                            }
                            int j = 0;
                            foreach (DataRow _fila in datos.Rows)
                            {
                                _datosConsulta.Rows.Add();
                                Color color = Color.White;
                                if (j % 2 == 0)
                                    color = Color.White;
                                else
                                    color = Color.LightCyan;
                                _datosConsulta.Rows[j].DefaultCellStyle.BackColor = color;
                                for (int i = 0; i < datos.Columns.Count; i++)
                                {
                                    string aux = Convert.ToString(_fila[i]);
                                    if (aux.Length == 0)
                                        aux = "0.0";
                                    _datosConsulta[i, j].Value = aux;
                                }
                                j++;
                            }
                            _datosConsulta.Rows.Add();
                            string ruta = _reporte.ExportarDataGridViewExcel("Reporte de Metas de Ventas", "", pb, sFDGuardar, _datosConsulta, _Usuario.USUARIO, _periodo, out msg_local);
                            if (ruta.Length > 0)
                                MessageBox.Show("Archivo generado con éxito\n\r\"" + ruta + "\"", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("Sin datos encontrados", "Información");
                    }
                    catch (Exception Ex)
                    {
                        msg_local = Ex.Message;
                    }
                    if (msg_local.Length > 0)
                        MessageBox.Show(msg_local, "Error");
                }
            }
        }
        private void TranferenciasDetallado()
        {

            string msg_local = "";
            try
            {

            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error");
        }
        private void SupersacosDisponiblesXUbicacion()
        {

            string msg_local = "";
            try
            {

            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error");
        }
        private void TransferenciasXFechas()
        {

            string msg_local = "";
            try
            {

            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error");
        }

        private void cBSeleccionReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _seleccionado = "";
            _seleccionado = cBSeleccionReporte.SelectedItem.ToString();
            if(_seleccionado.Length>0)
            {
                if(_seleccionado== "Reporte Supersacos disponibles por ubicacion"||_seleccionado== "Reporte de existencia de supersacos en fecha especifica")
                {
                    label2.Text = "Fecha de analisis:";
                    dTPFin.Visible = false;
                    dTPFechaFinHora.Visible = false;
                    dTPInicioHora.Visible = false;
                    label3.Visible = false;
                    label4.Visible = true;
                    cBAlmacenes.Visible = true;
                }else
                {
                    label2.Text = "Fecha inicio:";
                    dTPFin.Visible = true;
                    dTPFechaFinHora.Visible = true;
                    dTPInicioHora.Visible = true;
                    label3.Visible = true;
                    label4.Visible = false;
                    cBAlmacenes.Visible = false;
                }
            }
        }

        private void FRep_Load(object sender, EventArgs e)
        {
            //Cargar almacenes de SQL 
            CargarAlmacenes();
        }
        private void CargarAlmacenes()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                cBAlmacenes.DataSource = null;
                string cadena = "select infr.INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " inner join INVENTARIO_SUPERSACO as iss on iss.INVENTARIO_FRIMEX_ID = infr.INVENTARIO_FRIMEX_ID " +
                    " where ESTATUS = 'A'  ";
                DataTable Table = new DataTable();
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
                SqlDataAdapter DA = new SqlDataAdapter(cadena, cn.SC);
                DA.Fill(Table);
                if (Table != null)
                {
                    cBAlmacenes.DataSource = Table.DefaultView;
                    cBAlmacenes.ValueMember = "INVENTARIO_FRIMEX_ID";
                    cBAlmacenes.DisplayMember = "ALMACEN_MSP_DESCRIPCION";
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
    }
}
