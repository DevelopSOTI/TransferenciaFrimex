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

namespace FrimexTransferencia
{
    class C_REPORTES
    {
        public string ExportarDataGridViewExcel(string ReporteTitulo, string RutaLogo,ProgressBar pBAvance, SaveFileDialog sFDGuardar, DataGridView grd, string USUARIO, string PERIODO, out string msg)
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
                    int i = 0, iCol = 65, cantCol= grd.ColumnCount,iCol2=65,Col3=65;
                    string A1 = "", A2 = "";
                    A1 = Convert.ToChar(iCol) + (++i).ToString();
                    A2 = Convert.ToChar(iCol+grd.Columns.Count) + (i).ToString(); ;
                   
                    //A2 = Convert.ToChar(iCol + grd.ColumnCount - 1) + (i).ToString();
                    hojaTrabajo.get_Range(A1, A2).Merge(true);
                    hojaTrabajo.get_Range(A1, A2).Font.Bold = true; //Letra negrita                      
                    hojaTrabajo.get_Range(A1, A2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//Alineación horizontal
                    hojaTrabajo.get_Range(A1, A2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter; //Alineación vertical
                    hojaTrabajo.get_Range(A1, A2).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic); //Borde
                                                                                                                                                                                                                                                                                                                       //if (chBFecha.Checked == true)

                    hojaTrabajo.get_Range(A1, A2).Value = ReporteTitulo.ToUpper();
                    //grd.Rows.Add();
                    //hojaTrabajo.PageSetup.CenterHorizontally = true;
                    //Agregar Logo
                    if (RutaLogo.Length > 0)
                        AgregarImagenDesdeArchivo(hojaTrabajo, RutaLogo, Convert.ToChar(iCol + grd.ColumnCount) + (i).ToString(), Convert.ToChar(iCol + grd.ColumnCount + 4) + (i + 4).ToString());

                    hojaTrabajo.get_Range("A2", "D4").Merge(true);
                    hojaTrabajo.get_Range("A2", "D4").Value = "Periodo: " + PERIODO;

                    hojaTrabajo.get_Range("A3", "C3").Merge(true);
                    hojaTrabajo.get_Range("A3", "C3").Value = "Fecha Cracion: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


                    hojaTrabajo.get_Range("A4", "C4").Merge(true);
                    hojaTrabajo.get_Range("A4", "C4").Value = "Usuario creador: " + USUARIO;

                    string [] _encabezados=new string[grd.Columns.Count];
                    int a = 0;
                    foreach (DataGridViewColumn _coluna in grd.Columns)
                        _encabezados[a++] = _coluna.HeaderText;
                    grd.Rows.Insert(0, _encabezados);                    
                    grd.Rows[0].Visible = false;
                    pBAvance.Visible = true;
                    pBAvance.Minimum = 0;
                    pBAvance.Value = 0;
                    pBAvance.Maximum = grd.ColumnCount;
                    pBAvance.Visible = true;
                    //lProgressBar.Visible = true;
                    //lProgressBar.Text = "Paso 1 de 2 ...";
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                           /* if(grd.Columns[j].HeaderText.Contains("FECHA"))
                            {
                            Microsoft.Office.Interop.Excel.Range formatRange;

                            string strFormat = "dd/mm/yyyy hh:mm:ss tt";
                            string strColSelect = Convert.ToChar(j + 65) + ":" + Convert.ToChar(j + 65);
                            formatRange = hojaTrabajo.get_Range(strColSelect);
                            formatRange.NumberFormatLocal = strFormat;

                               // hojaTrabajo.Range[Convert.ToChar(j+65)+':'+Convert.ToChar(j+65)].Value =Convert.ToString( grd.Rows[i].Cells[j].Value);
                               // hojaTrabajo.Cells[i + 5, j + 1].NumberFormat = FRM_EXCEL_FECHA;

                            }*/
                        for (i = 0; i < grd.Rows.Count - 1; i++)
                        {

                            //hojaTrabajo.Cells[i + 5, j + 1] = Convert.ToString(grd.Rows[i].Cells[j].Value);
                            hojaTrabajo.Cells[i + 5, j + 1] = grd.Rows[i].Cells[j].Value;
                        }
                            hojaTrabajo.Columns[j + 1].WrapText = false;
                            hojaTrabajo.Columns[j + 1].AutoFit();
                        pBAvance.Value = j;
                    }
                    i = 5; iCol = 65;
                    A1 = "";
                    A2 = "";
                    A1 = Convert.ToChar(iCol) + (i).ToString();
                    A2 = Convert.ToChar(iCol + grd.Columns.Count - 1) + (i).ToString();
                    hojaTrabajo.get_Range(A1, A2).Font.Bold = true; //Letra negrita
                    hojaTrabajo.get_Range(A1, A2).WrapText = false; //Respetar ancho de la celda                       
                    hojaTrabajo.get_Range(A1, A2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//Alineación horizontal
                    hojaTrabajo.get_Range(A1, A2).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignDistributed; //Alineación vertical
                    hojaTrabajo.get_Range(A1, A2).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic); //Borde
                    hojaTrabajo.get_Range(A1, A2).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.LightSteelBlue);
                    hojaTrabajo.get_Range(A1, A2).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    int iFil = 5; iCol = 65;
                    pBAvance.Visible = true;
                    pBAvance.Minimum = 0;
                    pBAvance.Value = 0;
                    pBAvance.Maximum = grd.RowCount;
                    pBAvance.Visible = true;
                    //lProgressBar.Text = "Paso 2 de 2 ...";
                   /* for (iFil = 5; iFil < grd.RowCount + 4; iFil++)
                    {
                        for (iCol = 65; iCol < grd.ColumnCount + 65; iCol++)
                        {
                            int fila = iFil - 5, col = iCol - 65;
                            string aux = Convert.ToString(grd[col, fila].Value);
                            if (aux.Length > 0)
                            {
                                A1 = Convert.ToChar(iCol) + (iFil).ToString();
                                hojaTrabajo.get_Range(A1).WrapText = false; 
                                hojaTrabajo.get_Range(A1).Font.Bold = false;
                                hojaTrabajo.Rows[iFil].Cells[col + 1].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                                hojaTrabajo.Cells[iFil, col + 1] = Convert.ToString(grd[col, fila].Value);
                            }
                        }
                        pBAvance.Value = iFil - 5;
                    }*/
                    //grd.Rows.RemoveAt(grd.Rows.Count-1);
                    pBAvance.Visible = false;
                    //hojaTrabajo.Columns.WrapText = false;
                    //hojaTrabajo.Columns.AutoFit();
                    //hojaTrabajo.Cells.WrapText = false;
                    librosTrabajo.SaveAs(ruta, XlFileFormat.xlOpenXMLWorkbook,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value, false, false,
                                           XlSaveAsAccessMode.xlNoChange, false, false,
                                           System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                    ruta = librosTrabajo.Path+"\\"+sFDGuardar.FileName;
                    librosTrabajo.Close(true);
                    aplicacion.Quit();
                   // MessageBox.Show("Exportación Exitosa");
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
                ruta = "";
            }
            msg = msg_local;
            return ruta;
        }
        private void AgregarImagenDesdeArchivo(Microsoft.Office.Interop.Excel.Worksheet hoja,string Ruta_imagen,string RangoCol1,string RangoCol2)
        {
            // Rango donde se intertará la imagen
            Microsoft.Office.Interop.Excel.Range rango;
            rango = (Microsoft.Office.Interop.Excel.Range)hoja.get_Range(RangoCol1, RangoCol2);
            rango.Select(); //rango para poder insertar la imagen

            // objeto de las imagenes en excel
            Microsoft.Office.Interop.Excel.Pictures oPictures =
            (Microsoft.Office.Interop.Excel.Pictures)hoja.Pictures(System.Reflection.Missing.Value);

           /* hoja.Shapes.AddPicture("@"+Ruta_imagen,
            Microsoft.Office.Core.MsoTriState.msoFalse,
            Microsoft.Office.Core.MsoTriState.msoCTrue,
            float.Parse(rango.Left.ToString()), float.Parse(rango.Top.ToString()),
            float.Parse(rango.Width.ToString()), float.Parse(rango.Height.ToString()));*/
        }
        
    }
}