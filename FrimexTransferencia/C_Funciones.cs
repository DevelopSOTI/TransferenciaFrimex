﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using FirebirdSql.Data.FirebirdClient;


namespace FrimexTransferencia
{
    class C_Funciones
    {
        public System.Data.DataTable CargarDatosGridXLotes(string LOTES,DateTime Finicio, DateTime FFinal,  out string msg)
        {
            System.Data.DataTable _datos = new System.Data.DataTable();
                System.Data.DataTable _datosFolio = new System.Data.DataTable();
            string consulta = "", msg_local = "",_lotes="";
            DataGridView dgv = new DataGridView();

            try
            {
                if (LOTES.Length > 0)
                    _lotes = " (FOLIO in (" + LOTES + ") or LOTE_ID in (" + LOTES + "))";
                else
                    _lotes = " s.EMBARQUE_ID is not null and s.SUPERSACO_FECHA between '" + Finicio.ToString("dd/MM/yyy HH:mm:ss") + "' and '" + FFinal.ToString("dd/MM/yyy HH:mm:ss") + "'";
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "select s.SUPERSACO_ID,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD,S.SUPERSACO_ESTATUS,S.SUPERSACO_FECHA,s.LOTE_ID,s.EMBARQUE_ID,cast(poc.FOLIO as int) FOLIO from SUPERSACO as s " +
                    " full outer join PRODUCTO as p on s.PRODUCTO_ID=p.PRODUCTO_ID or s.PRODUCTO_ID=p.PRODUCTO_MSP_ID" +
                    " full outer join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                    " full outer join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where " +                    
                    _lotes +" ";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();

                //Agrupar FOLIO
                _datosFolio.Columns.Add("SUPERSACO_ID", typeof(int));
                _datosFolio.Columns.Add("PRODUCTO_DESCRIPCION", typeof(string));
                _datosFolio.Columns.Add("SUPERSACO_CANTIDAD", typeof(double));
                _datosFolio.Columns.Add("SUPERSACO_ESTATUS", typeof(string));
                _datosFolio.Columns.Add("SUPERSACO_FECHA", typeof(DateTime));
                _datosFolio.Columns.Add("FOLIO", typeof(int));

                foreach (DataRow row in _datos.Rows)
                {
                    int _SSID = 0,_FOLIO=0;
                    double _CANTIDAD = 0.0;
                    if (string.IsNullOrEmpty(row["SUPERSACO_ID"].ToString()))
                        _SSID = -1;
                    else
                        _SSID = Convert.ToInt32(row["SUPERSACO_ID"]);
                    if (string.IsNullOrEmpty(row["SUPERSACO_CANTIDAD"].ToString()))
                        _CANTIDAD = 0;
                    else
                        _CANTIDAD = Convert.ToDouble(row["SUPERSACO_CANTIDAD"]);
                    string aux = "",_PRODDESC="",_ESTATUS="";
                    DateTime _FECHA=new DateTime();
                    _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]);
                    aux = Convert.ToString(row["FOLIO"]);
                    if (aux.Length == 0)
                        aux = Convert.ToString(row["LOTE_ID"]);
                    _FOLIO =Convert.ToInt32( aux);
                    if (string.IsNullOrEmpty(Convert.ToString(row["SUPERSACO_FECHA"])))
                        _FECHA = new DateTime();
                    else
                        _FECHA = Convert.ToDateTime(row["SUPERSACO_FECHA"]);
                    aux = Convert.ToString(row["SUPERSACO_ESTATUS"]);
                    if (aux == "B" || aux == "D")
                        aux = "Procesado";
                    else if (aux == "A")
                        aux = "Activo";
                    _ESTATUS = aux;
                    _datosFolio.Rows.Add(_SSID, _PRODDESC,_CANTIDAD,_ESTATUS,_FECHA, _FOLIO);
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            if(_datosFolio.Rows.Count>0)
            {
            DataView dtV = _datosFolio.DefaultView;
            dtV.Sort = "FOLIO ASC";
            _datosFolio = dtV.ToTable();

            }
            return _datosFolio;
        }
        public System.Data.DataTable CargarDatosGridXModulos(string LOTES,DateTime Finicio, DateTime FFinal, out string msg)
        {
            System.Data.DataTable _datos = new System.Data.DataTable();
            System.Data.DataTable _datosFolio = new System.Data.DataTable();
            string consulta = "", msg_local = "",_lotes="";
            int cont = 0;
            DataGridView dgv = new DataGridView();
            if (LOTES.Length > 0)
                _lotes = "  (FOLIO in (" + LOTES + ") or LOTE_ID in (" + LOTES + "))";
            else
                _lotes = " s.EMBARQUE_ID is not null and s.SUPERSACO_FECHA between '" + Finicio.ToString("dd/MM/yyy HH:mm:ss") + "' and '" + FFinal.ToString("dd/MM/yyy HH:mm:ss") + "'";
            try
            {
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "select s.SUPERSACO_ID,p.producto_msp_id as producto_id,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD,S.SUPERSACO_ESTATUS,S.SUPERSACO_FECHA,s.LOTE_ID as MOD_REC_TANQ,s.EMBARQUE_ID,cast(poc.FOLIO as int) MOD_POC_MSP " +
                    "from SUPERSACO as s " +
                    " full outer join PRODUCTO as p on s.PRODUCTO_ID=p.PRODUCTO_ID or s.PRODUCTO_ID=p.PRODUCTO_MSP_ID" +
                    " full outer join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                    " full outer join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where " + _lotes+" ";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();

                //Agrupar FOLIO
                _datosFolio.Columns.Add("SUPERSACO_ID", typeof(int));
                _datosFolio.Columns.Add("PRODUCTO_ID", typeof(int));
                _datosFolio.Columns.Add("PRODUCTO_DESCRIPCION", typeof(string));
                _datosFolio.Columns.Add("SUPERSACO_CANTIDAD", typeof(double));
                _datosFolio.Columns.Add("SUPERSACO_ESTATUS", typeof(string));
                _datosFolio.Columns.Add("SUPERSACO_FECHA", typeof(DateTime));
                _datosFolio.Columns.Add("FOLIO", typeof(int));
                _datosFolio.Columns.Add("MOD_REC_TANQ", typeof(int));
                _datosFolio.Columns.Add("MOD_POC_MSP", typeof(int));
                foreach (DataRow row in _datos.Rows)
                {
                    
                    int _SSID = 0, _FOLIO = 0,_PRODUCTO_ID=0;
                    double _CANTIDAD = 0.0,_TANQUES=0.0,_PETICIONOC=0.0;
                    if (string.IsNullOrEmpty( row["SUPERSACO_ID"].ToString()))
                        _SSID = -1;
                    else
                        _SSID = Convert.ToInt32(row["SUPERSACO_ID"]);

                    if (string.IsNullOrEmpty(row["PRODUCTO_ID"].ToString()))
                        _PRODUCTO_ID = -1;
                    else
                        _PRODUCTO_ID = Convert.ToInt32(row["PRODUCTO_ID"]);

                    if (string.IsNullOrEmpty(row["SUPERSACO_CANTIDAD"].ToString()))
                        _CANTIDAD = 0;
                    else
                        _CANTIDAD = Convert.ToDouble(row["SUPERSACO_CANTIDAD"]);

                    string aux = "", _PRODDESC = "", _ESTATUS = "";
                    DateTime _FECHA = new DateTime();
                    _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]);
                    aux = Convert.ToString(row["MOD_POC_MSP"]);
                    if (aux.Length == 0)
                    {
                        aux = Convert.ToString(row["MOD_REC_TANQ"]);
                        _TANQUES = _CANTIDAD;
                        _PETICIONOC = 0.0;
                    }
                    else
                    {
                        _TANQUES = 0.0;
                        _PETICIONOC = _CANTIDAD;
                    }
                    _FOLIO = Convert.ToInt32(aux);
                    if (string.IsNullOrEmpty(Convert.ToString(row["SUPERSACO_FECHA"])))
                        _FECHA = new DateTime();
                    else
                        _FECHA = Convert.ToDateTime(row["SUPERSACO_FECHA"]);
                    aux = Convert.ToString(row["SUPERSACO_ESTATUS"]);
                    if (aux == "B" || aux == "D")
                        aux = "Procesado";
                    else if (aux == "A")
                        aux = "Activo";
                    _ESTATUS = aux;
                    _datosFolio.Rows.Add(_SSID,_PRODUCTO_ID, _PRODDESC, _CANTIDAD, _ESTATUS, _FECHA, _FOLIO,_TANQUES,_PETICIONOC);
                   
                    cont++;
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message+" Registro:"+cont;
            }
            msg = msg_local;
            if (_datosFolio.Rows.Count>0)
            {
            DataView dtV = _datosFolio.DefaultView;
            dtV.Sort = "FOLIO ASC";
            _datosFolio = dtV.ToTable();
            }
            return _datosFolio;
        }
        public bool ActualizarDatosSS(string SS_ID,string PRODUCTO_NUEVO,string LOTE_NUEVO,out string msg,ConexionSql cn  ,SqlTransaction transaction)
        {
            bool _exito = false;
            string consulta = "",msg_local="";            
            SqlCommand cmd;
            try
            {
                consulta = "UPDATE [dbo].[SUPERSACO] " +
                    " SET " +
                    " [PRODUCTO_ID] =" + PRODUCTO_NUEVO+
                    " ,[LOTE_ID] =" + LOTE_NUEVO +
                    " ,[EMBARQUE_ID] = 1 " +
                    " WHERE[SUPERSACO_ID] ="+SS_ID;

                cmd = new SqlCommand(consulta, cn.SC,transaction);
                if (cmd.ExecuteNonQuery() > 0)
                    _exito = true;
                else
                    _exito = false;
                //cmd.Dispose();
            }
            catch(Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _exito;
        }
        public System.Data.DataTable CargarDatosGridXModulosConcentrado(string LOTES, DateTime Finicio, DateTime FFinal, out string msg)
        {
            System.Data.DataTable _datos = new System.Data.DataTable();
            System.Data.DataTable _datosFolio = new System.Data.DataTable();
            string consulta = "", msg_local = "", _lotes = "",_SS_ID="";
            int  _PRODUCTO_ID = 0;
            //double _CANTIDAD = 0.0, _TANQUES = 0.0, _PETICIONOC = 0.0;
            string _CANTIDAD ="", _TANQUES = "", _PETICIONOC = "",_ESTATUS="";
            string aux = "", _PRODDESC = "";
            string _FOLIO = "";
            int cont = 0;
            DataGridView dgv = new DataGridView();
            if (LOTES.Length > 0)
                _lotes ="(FOLIO in (" + LOTES + ") or LOTE_ID in (" + LOTES + "))";// " Cast(s.[SUPERSACO_FECHA] as date)>='12/12/2018'";   
            else
                _lotes = " s.EMBARQUE_ID is not null and s.SUPERSACO_FECHA between '" + Finicio.ToString("dd/MM/yyy HH:mm:ss") + "' and '" + FFinal.ToString("dd/MM/yyy HH:mm:ss") + "'";
            try
            {
                SqlDataAdapter _da;
                ConexionSql cn = new ConexionSql();
                consulta = "select p.PRODUCTO_DESCRIPCION,s.SUPERSACO_ID,s.SUPERSACO_FECHA,s.SUPERSACO_CANTIDAD as CANT_RECIBIDA,p.PRODUCTO_MSP_ID as Producto_ID,s.LOTE_ID as MOD_REC_TANQ,s.EMBARQUE_ID," +
                    " cast(poc.FOLIO as int) MOD_POC_MSP,S.SUPERSACO_ESTATUS AS ESTATUS " +
                    " from SUPERSACO as s " +
                    " full outer join PRODUCTO as p on s.PRODUCTO_ID=p.PRODUCTO_ID or s.PRODUCTO_ID=p.PRODUCTO_MSP_ID" +
                    " full outer join EMBARQUE as e on s.EMBARQUE_ID = e.EMBARQUE_ID " +
                    " full outer join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where " + _lotes + " " +
                    " And (S.[SUPERSACO_ESTATUS] ='A' or S.[SUPERSACO_ESTATUS]='B')" +
                    " and P.producto_id is not null" +
                    " order by p.PRODUCTO_DESCRIPCION";
                cn.ConectarSQLServer();
                _da = new SqlDataAdapter(consulta, cn.SC);
                _da.SelectCommand.CommandTimeout =180;
                _da.Fill(_datos);
                _da.Dispose();
                cn.Desconectar();

                //Agrupar FOLIO
                _datosFolio.Columns.Add("SUPERSACO_ID", typeof(string));
                _datosFolio.Columns.Add("PRODUCTO_ID", typeof(string));
                _datosFolio.Columns.Add("PRODUCTO_DESCRIPCION", typeof(string));
                _datosFolio.Columns.Add("FOLIO", typeof(string));
                _datosFolio.Columns.Add("MOD_REC_TANQ", typeof(string));
                _datosFolio.Columns.Add("MOD_POC_MSP", typeof(string));
                _datosFolio.Columns.Add("SUPERSACO_FECHA", typeof(DateTime));
                _datosFolio.Columns.Add("ESTATUS", typeof(string));
                foreach (DataRow row in _datos.Rows)
                {                   
                    if (string.IsNullOrEmpty(row["CANT_RECIBIDA"].ToString()))
                        _CANTIDAD = "0.0";
                    else
                        _CANTIDAD = (Convert.ToString(row["CANT_RECIBIDA"]).Trim());
                    _SS_ID = Convert.ToString(Convert.ToString(row["SUPERSACO_ID"]).Trim());
                    _PRODUCTO_ID = Convert.ToInt32(Convert.ToString(row["PRODUCTO_ID"]).Trim());
                    _PRODDESC = Convert.ToString(row["PRODUCTO_DESCRIPCION"]).Trim();
                    _ESTATUS= Convert.ToString(Convert.ToString(row["ESTATUS"]).Trim());
                    aux = Convert.ToString(row["MOD_POC_MSP"]).Trim();
                    if (aux.Length == 0)
                    {
                        aux = Convert.ToString(row["MOD_REC_TANQ"]).Trim();
                        _TANQUES = _CANTIDAD;
                        _PETICIONOC = "0.0";
                    }
                    else
                    {
                        _TANQUES = "0.0";
                        _PETICIONOC = _CANTIDAD;
                    }
                    _FOLIO = aux;
                    if (_ESTATUS == "A")
                        _ESTATUS = "Activo";
                    else if (_ESTATUS == "B")
                        _ESTATUS = "Usado";
                    else if (_ESTATUS == "I")
                        _ESTATUS = "Inactivo";
                    else if (_ESTATUS == "C")
                        _ESTATUS = "Cancelado";
                    //_ESTATUS = aux;
                    _datosFolio.Rows.Add(_SS_ID,_PRODUCTO_ID, _PRODDESC, _FOLIO, _TANQUES, _PETICIONOC,Convert.ToDateTime(row["SUPERSACO_FECHA"]),_ESTATUS);
                    cont++;
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message + " Registro: " + cont+"\n\rProductoID: "+
                    _PRODUCTO_ID+"\n\rProducto Desc: "+_PRODDESC + "\r\nProductoFolio: " + _FOLIO + "\n\rTanques: " + _TANQUES + "\r\nPeticionOC" + _PETICIONOC;
            }
            msg = msg_local;
            if (_datosFolio.Rows.Count > 0)
            {
                DataView dtV = _datosFolio.DefaultView;
                dtV.Sort = "FOLIO ASC";
                _datosFolio = dtV.ToTable();
            }
            return _datosFolio;
        }
        public string ReporteLotesSS(string ReporteTitulo,string Datos,ProgressBar pBAvance,SaveFileDialog sFDGuardar,DataGridView dataGridViewDatos, string Usuario,out string msg)
        {
            string _ruta = "";
            string msg_local = "";
            try
            {                
                string periodo = "";
                _ruta = ExportarDataGridViewExcel(ReporteTitulo,Datos,pBAvance,sFDGuardar, dataGridViewDatos, Usuario, periodo, out msg_local);
                if (msg_local.Length > 0)
                    MessageBox.Show(msg_local,"Error");
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _ruta;
        }
        private string ExportarDataGridViewExcel(string ReporteTitulo, string Datos,ProgressBar pBAvance, SaveFileDialog sFDGuardar, DataGridView grd, string USUARIO, string PERIODO, out string msg)
        {
            string msg_local = "", ruta = "";
            try
            {
                sFDGuardar.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                sFDGuardar.Filter = "Excel Files(.xls)|*.xls| " +
                    "Excel Files(.xlsx)| *.xlsx | Excel Files(*.xlsm) | *.xlsm";
                sFDGuardar.FilterIndex = 2;
                sFDGuardar.RestoreDirectory = true;
                string nombreArchivo = "Reporte_" +
                    DateTime.Now.Year.ToString() +
                    DateTime.Now.Month.ToString() +
                    DateTime.Now.Day.ToString() +
                    DateTime.Now.Hour.ToString() + "_" +
                    DateTime.Now.Minute.ToString() + "_" +
                    DateTime.Now.Second.ToString() +
                    ".xlsx";

                if (sFDGuardar.ShowDialog() == DialogResult.OK)
                {
                    ruta = sFDGuardar.FileName;

                    Microsoft.Office.Interop.Excel.Application aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    Workbook librosTrabajo = aplicacion.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                    Worksheet hojaTrabajo = (Worksheet)librosTrabajo.Worksheets.get_Item(1);
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    librosTrabajo = aplicacion.Workbooks.Add();
                    hojaTrabajo =
                        (Microsoft.Office.Interop.Excel.Worksheet)librosTrabajo.Worksheets.get_Item(1);
                    //Recorremos el DataGridView rellenando la hoja de trabajo
                   // string materiaPrima = "";
                    int i = 0, iCol = 65;
                    string A1 = "", A2 = "";
                    A1 = Convert.ToChar(iCol) + (++i).ToString();
                    A2 = Convert.ToChar(iCol + grd.ColumnCount - 1) + (i).ToString();
                    hojaTrabajo.get_Range(A1, A2).Merge(true);
                    hojaTrabajo.get_Range(A1, A2).Font.Bold = true; //Letra negrita                      
                    hojaTrabajo.get_Range(A1, A2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//Alineación horizontal
                    hojaTrabajo.get_Range(A1, A2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //Alineación vertical
                    hojaTrabajo.get_Range(A1, A2).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic); //Borde
                                                                                                                                                                                                                                                                                                                       //if (chBFecha.Checked == true)
                   
                        hojaTrabajo.get_Range(A1, A2).Value = ReporteTitulo.ToUpper();
                    grd.Rows.Add();
                    hojaTrabajo.PageSetup.CenterHorizontally = true;

                    hojaTrabajo.get_Range("A2", "D4").Merge(true);
                    hojaTrabajo.get_Range("A2", "D4").Value = "Cantidad Comprada: \r\n" + Datos;

                    hojaTrabajo.get_Range("A3", "C3").Merge(true);
                    hojaTrabajo.get_Range("A3", "C3").Value = "Fecha Cracion: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


                    hojaTrabajo.get_Range("A4", "C4").Merge(true);
                    hojaTrabajo.get_Range("A4", "C4").Value = "Usuario creador: " + USUARIO;

                    if(ReporteTitulo== "Reporte por Lotes")
                    grd.Rows.Insert(0, "SUPERSACO ID","PRODUCTO", "PESO SS","ESTATUS","FECHA CREACION", "FOLIO MSP", "CANT_X_LOTE");
                    else if (ReporteTitulo == "Reporte por Modulo")
                        grd.Rows.Insert(0, "SUPERSACO ID", "PRODUCTO", "PESO SS", "ESTATUS", "FECHA CREACION", "FOLIO MSP", "CANT_REC_TANQUES","CANT_REC_PETICIONOC","CANT_TOTAL_X_LOTE");
                    else if(ReporteTitulo== "Reporte de súper sacos por lote")
                        grd.Rows.Insert(0, "SUPERSACO ID", "PRODUCTO", "LOTE", "PESO", "FECHA DE CREACION");
                    grd.Rows[0].Visible = false;
                    bool uno = true;

                    pBAvance.Visible = true;
                    pBAvance.Minimum = 0;
                    pBAvance.Value = 0;
                    pBAvance.Maximum = grd.ColumnCount;
                    pBAvance.Visible = true;
                    //lProgressBar.Visible = true;
                    //lProgressBar.Text = "Paso 1 de 2 ...";
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        for (i = 0; i < grd.Rows.Count - 1; i++)
                        {
                            hojaTrabajo.Cells[i + 5, j + 1] = Convert.ToString(grd.Rows[i].Cells[j].Value);
                            // FormatoCeldas(Convert.ToString(hojaTrabajo.Cells[i + 5, j + 1]), Celda, hojaTrabajo);
                            // pBEstatus.Value = pb++;
                        }
                        //hojaTrabajo.Cells[1, j + 1].EntireColumn.AutoFit();

                        pBAvance.Value = j;
                    }
                    i = 5; iCol = 65;
                    A1 = ""; A2 = "";
                    A1 = Convert.ToChar(iCol) + (i).ToString();
                    A2 = Convert.ToChar(iCol + grd.Columns.Count - 1) + (i).ToString();
                    hojaTrabajo.get_Range(A1, A2).Font.Bold = true; //Letra negrita
                    hojaTrabajo.get_Range(A1, A2).WrapText = false; //Respetar ancho de la celda                       
                    hojaTrabajo.get_Range(A1, A2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//Alineación horizontal
                    hojaTrabajo.get_Range(A1, A2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //Alineación vertical
                    hojaTrabajo.get_Range(A1, A2).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic); //Borde
                    hojaTrabajo.get_Range(A1, A2).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightGray);

                    int iFil = 5; iCol = 65;
                    pBAvance.Visible = true;
                    pBAvance.Minimum = 0;
                    pBAvance.Value = 0;
                    pBAvance.Maximum = grd.RowCount;
                    pBAvance.Visible = true;
                    //lProgressBar.Text = "Paso 2 de 2 ...";
                    for (iFil = 5; iFil < grd.RowCount + 4; iFil++)
                    {
                        for (iCol = 65; iCol < grd.ColumnCount + 65; iCol++)
                        {
                            int fila = iFil - 5, col = iCol - 65;
                            string aux = Convert.ToString(grd[col, fila].Value);
                            string FRM_EXCEL_FECHA = "dd/MM/yyyy"; // o el formato que deseas:
                            /*hojaTrabajo.Cells[i + 2, j + 1].NumberFormat = FRM_EXCEL_FECHA;
                            hojaTrabajo.Cells[i + 2, j + 1].Value = dataGridView2.Rows[i].Cells[j].Value.ToString();*/
                            if (aux.Length > 0)
                            {
                                A1 = Convert.ToChar(iCol) + (iFil).ToString();
                                hojaTrabajo.get_Range(A1).WrapText = false; //Respetar ancho de la celda                       
                                hojaTrabajo.get_Range(A1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//Alineación horizontal
                                hojaTrabajo.get_Range(A1).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //Alineación vertical
                                hojaTrabajo.get_Range(A1).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic); //Borde

                                hojaTrabajo.Cells[iFil, col + 1] = Convert.ToString(grd[col, fila].Value);                                                                                                                                                                                                                                                                                              //pBEstatus.Value = pb++;
                            }
                        }
                         pBAvance.Value = iFil - 5;
                    }
                    pBAvance.Visible = false;
                    //lProgressBar.Visible = false;
                    if (uno == true)
                    {
                        grd.Rows.RemoveAt(grd.Rows.Count-1);
                        uno = false;
                    }
                    hojaTrabajo.Columns.AutoFit();
                    //hojaTrabajo.Rows.AutoFit();
                    string[] _datos = Datos.Split('\r');
                    int _multiplicador = _datos.Length;
                    //hojaTrabajo.Range[2].RowHeight = hojaTrabajo.Range[2].RowHeight*_multiplicador;

                    hojaTrabajo.Cells.Style.WrapText = true;
                    librosTrabajo.SaveAs(ruta, XlFileFormat.xlOpenXMLWorkbook,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, false,
                                           XlSaveAsAccessMode.xlNoChange, false, false,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    ruta = librosTrabajo.Path;
                    librosTrabajo.Close(true);
                    aplicacion.Quit();
                    MessageBox.Show("Exportación Exitosa");
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return ruta;
        }
        
        public string RellenarConCeros(string CADENA, int Longitud)
        {
            string _cadena = "";
            if (CADENA.Length < Longitud)
            {
                for (int i = 0; CADENA.Length + _cadena.Length < Longitud; i++)
                    _cadena += "0";
                _cadena += CADENA;
            }
            else
                _cadena = CADENA;
            return _cadena;
        }
        public System.Data.DataTable CargarDatosMSP(string LOTES_ESPECIFICOS, out string msg)
        {
            string msg_local = "", consulta = "";
            System.Data.DataTable _datosMsp = new System.Data.DataTable();
            try
            {
                FbDataAdapter _fbDA;
                ConexionMicrosip con_msp = new ConexionMicrosip();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                consulta = "select distinct " +
                    " cast(DCM.folio as integer) AS Folio_OC,  " +
                    " cast(DCMC.folio as integer) as Folio_Compra, " +
                    " AC.nombre as Producto_Compra,AC.Articulo_ID as Producto_ID,DCMDC.unidades as Cant_compra, " +
                    " ad.clave as LOTE_MSP" +
                    " from doctos_cm as DCM " +
                    " left join doctos_cm_ligas as dcml on DCM.docto_cm_id = dcml.docto_cm_fte_id " +
                    " left join doctos_cm as DCMC on dcml.docto_cm_dest_id = DCMC.docto_cm_id " +
                    " left join doctos_cm_det as DCMDC on DCMDC.docto_cm_id = DCMC.docto_cm_id " +
                    " left join desglose_en_discretos_cm ddcm ON ddcm.docto_cm_det_id = DCMDC.docto_cm_det_id " +
                    " left join articulos_discretos as ad on ad.art_discreto_id = ddcm.art_discreto_id" +
                    " left join articulos as AC on DCMDC.Articulo_id = AC.Articulo_id " +
                    " left join Elementos_cat_clasif as ECA on Eca.elemento_id = AC.articulo_id" +
                    " left join clasificadores_cat_valores as CCV on ECA.valor_clasif_id = CCV.valor_clasif_id " +
                    " where " +
                    " (DCMC.TIPO_DOCTO = 'C' " + " OR DCMC.TIPO_DOCTO = 'R') " +
                    //" AND DCMC.fecha > '12/31/2018' " +
                    " and (DCM.folio in (" + LOTES_ESPECIFICOS + "))" +
                    //" and CCV.valor = 'Granel' " +
                    " group by " +
                    " DCM.folio,DCMC.folio,AC.Articulo_ID ,AC.nombre ,DCMDC.unidades ,CCv.valor,ad.clave " +
                    " order by DCM.folio asc";
                _fbDA = new FbDataAdapter(consulta, con_msp.FBC);

                _fbDA.Fill(_datosMsp);
                con_msp.Desconectar();
                _fbDA.Dispose();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _datosMsp;
        }

        public bool ValidaLote(string LOTE,out string msg)
        {
            bool _existe = false;
            string msg_local = "",consulta= "";
            try
            {
                consulta = "SELECT [LOTE_ID] " +
                " ,[LOTE_FOLIO_MSP] " +
                " ,[FECHA_FUMIGACION] " +
                " ,[LOTE_ESTATUS_RECEPCION] " +
                " ,[LOTE_ESTATUS_CONSUMO] " +
                " ,[FECHA_CREACION] " +
                " ,[USUARIO_CREADOR] " +
                " FROM[dbo].[LOTE_MSP] " +
                " where[LOTE_FOLIO_MSP] = " + LOTE;
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                SqlCommand comm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                    _existe = true;
                else
                    _existe = false;
                reader.Close();
                comm.Dispose();
                cn.Desconectar();
            }
            catch(Exception Ex)
            {
                _existe = false;
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _existe;
        }
        public bool InsertarLote(string LOTE,DateTime FECHA_FUMIGACION,string ESTATUS_RECEPCION,string ESTATUS_CONSUMO,UsuariosC USUARIO,out string msg)
        {
            bool _existe = false;
            string msg_local = "", consulta = "",_fechaFumigacion="",_estatusRecepcion="",_estatusConsumo="";
            try
            {
                if (FECHA_FUMIGACION == new DateTime())
                    _fechaFumigacion = "NULL";
                else
                    _fechaFumigacion = "'" + FECHA_FUMIGACION.ToString("dd-MM-yyyy") + "'";

                if (ESTATUS_CONSUMO.Length > 0)
                    _estatusConsumo = "'" + ESTATUS_CONSUMO + "'";
                else
                    _estatusConsumo = "NULL";

                if (ESTATUS_RECEPCION.Length > 0)
                    _estatusRecepcion = "'" + ESTATUS_RECEPCION + "'";
                else
                    _estatusRecepcion = "NULL";

                consulta = "INSERT INTO [dbo].[LOTE_MSP] " +
                    " ([LOTE_FOLIO_MSP] " +
                    " ,[FECHA_FUMIGACION] " +
                    " ,[LOTE_ESTATUS_RECEPCION] " +
                    " ,[LOTE_ESTATUS_CONSUMO] " +
                    " ,[FECHA_CREACION] " +
                    " ,[USUARIO_CREADOR]) " +
                    " VALUES " +
                    " ('" + LOTE + "'" +
                    " ," + _fechaFumigacion +
                    " ," + _estatusRecepcion +
                    " ," + _estatusConsumo +
                    " ,'" + DateTime.Now.ToString("dd-MM-yyyy") + "'" +
                    " ," + USUARIO.USUARIOID + ")";
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                SqlCommand comm = new SqlCommand(consulta, cn.SC);
                
                if (comm.ExecuteNonQuery()>0)
                    _existe = true;
                else
                    _existe = false;

                comm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                _existe = false;
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _existe;
        }
        public bool ActualizarLote(string LOTE, DateTime FECHA_FUMIGACION, string ESTATUS_RECEPCION, string ESTATUS_CONSUMO, UsuariosC USUARIO, out string msg)
        {
            bool _existe = false;
            string msg_local = "", consulta = "", _fechaFumigacion = "", _estatusRecepcion = "", _estatusConsumo = "";
            try
            {
                if (ValidaLote(LOTE, out msg_local))
                {
                    if (FECHA_FUMIGACION == new DateTime())
                        _fechaFumigacion = "";
                    else
                        _fechaFumigacion = "[FECHA_FUMIGACION] = '" + FECHA_FUMIGACION.ToString("dd-MM-yyyy") + "'";

                    if (ESTATUS_CONSUMO.Length > 0)
                        _estatusConsumo = "[LOTE_ESTATUS_RECEPCION] = '" + ESTATUS_RECEPCION + "'";
                    else
                        _estatusConsumo = "";

                    if (ESTATUS_RECEPCION.Length > 0)
                        _estatusRecepcion = "[LOTE_ESTATUS_CONSUMO] = '" + ESTATUS_RECEPCION + "'";
                    else
                        _estatusRecepcion = "";
                    if (_fechaFumigacion.Length > 0 && _estatusConsumo.Length > 0 && _estatusRecepcion.Length > 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                            " SET " + _fechaFumigacion +
                            " ," + _estatusRecepcion +
                            " ," + _estatusConsumo +
                            " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    else if (_fechaFumigacion.Length == 0 && _estatusConsumo.Length > 0 && _estatusRecepcion.Length > 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                        " SET " + _estatusRecepcion +
                        " ," + _estatusConsumo +
                        " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    else if (_fechaFumigacion.Length == 0 && _estatusConsumo.Length == 0 && _estatusRecepcion.Length > 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                            " SET " + _estatusRecepcion +
                            " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    else if (_fechaFumigacion.Length > 0 && _estatusConsumo.Length == 0 && _estatusRecepcion.Length > 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                            " SET " + _fechaFumigacion +
                            " ," + _estatusRecepcion +
                            " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    else if (_fechaFumigacion.Length > 0 && _estatusConsumo.Length == 0 && _estatusRecepcion.Length == 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                            " SET " + _fechaFumigacion +
                            " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    else if (_fechaFumigacion.Length == 0 && _estatusConsumo.Length == 0 && _estatusRecepcion.Length > 0)
                        consulta = "UPDATE [dbo].[LOTE_MSP] " +
                            " SET " + _estatusConsumo +
                            " WHERE [LOTE_FOLIO_MSP]=" + LOTE;
                    ConexionSql cn = new ConexionSql();
                    cn.ConectarSQLServer();
                    SqlCommand comm = new SqlCommand(consulta, cn.SC);

                    if (comm.ExecuteNonQuery() > 0)
                    {
                        _existe = true;
                        if (ESTATUS_RECEPCION == "T")
                            ActualizarEstatusRecepcion(LOTE, out msg_local);
                    }
                    else
                        _existe = false;

                    comm.Dispose();
                    cn.Desconectar();
                }
                else
                {
                    InsertarLote(LOTE, FECHA_FUMIGACION, ESTATUS_RECEPCION, ESTATUS_CONSUMO, USUARIO, out msg_local);
                    _existe = true;
                }
            }
            catch (Exception Ex)
            {
                _existe = false;
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _existe;
        }
        private bool ActualizarEstatusRecepcion(string LOTE,out string msg)
        {
            bool _exito = false;
            ConexionSql ConexionSQL = new ConexionSql();
            string EmbarqueID = "",msg_local="";
            try
            {
                //Buscar Embarque utilizando el LOTE

                SqlCommand cmd;
                SqlDataReader reader;
                string _estatus = "23", consulta = "";
                consulta = "select EMBARQUE_ID from EMBARQUE as E " +
                    " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where poc.FOLIO like '%"+LOTE+"%'";
                cmd= new SqlCommand(consulta, ConexionSQL.SC);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EmbarqueID = Convert.ToString(reader["EMBARQUE_ID"]);
                    }
                    reader.Close();
                    cmd.Cancel();
                    ConexionSQL.Desconectar();
                    if (EmbarqueID.Length > 0)
                    {
                        consulta = "UPDATE [dbo].[EMBARQUE] " +
                            " SET [ESTATUS_ID] = " + _estatus +
                            " WHERE [EMBARQUE_ID] = " + EmbarqueID;
                        ConexionSQL.ConectarSQLServer();
                        cmd = new SqlCommand(consulta, ConexionSQL.SC);
                        if (cmd.ExecuteNonQuery() > 0)
                            _exito = true;
                        else
                            _exito = false;
                        cmd.Cancel();
                        ConexionSQL.Desconectar();
                    }
                }
                else
                {
                    reader.Close();
                    cmd.Cancel();
                    ConexionSQL.Desconectar();
                }
            }
            catch (Exception Ex)
            {
                if (ConexionSQL.IsConected())
                    ConexionSQL.Desconectar();
               msg_local= Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }
    }
}