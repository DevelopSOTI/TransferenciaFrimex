﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Collections;

namespace FrimexTransferencia
{
    public partial class FCodigoQR : Form
    {
        public FCodigoQR()
        {
            InitializeComponent();
        }
        Mensajes mensaje = new Mensajes();
        Image photo = null;
        private ArrayList MY_DATA = new ArrayList();
        public UsuariosC Usuario = new UsuariosC();
        public string _supersacoID = "";
        public string _productoNombre = "";
        public string _usuarioCreador = "";
        public string _centroCompra = "";
        public string _folioMSP = "";
        public string _reimpresion = "";
        public DateTime _fechaCreacion = new DateTime();
        public Double _cantidad = 0;

        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;
        Bitmap memoryImage;
        private void GenerarCodigoBarras(string _codigo)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            // pCodigoBarras.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, _codigo, Color.Black, Color.White, 500, 150);
            //pCodigoBarras.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, _codigo, Color.Black, Color.White, 230, 100);
            QRCoder.QRCodeGenerator MY_CODE = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData MY_DATA = MY_CODE.CreateQrCode(_supersacoID + "\n\rLOTE " + _folioMSP + "\n\r" + _cantidad.ToString("N2")
                + "Kg\n\r"+_productoNombre+"\n\r"+_fechaCreacion.ToString("dddd, dd MMMM yyyy h:mm tt ") + _reimpresion
                , QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.QRCode MY_QR_CODE = new QRCoder.QRCode(MY_DATA);

            Bitmap MY_IMAGE = new Bitmap(MY_QR_CODE.GetGraphic(20), 330, 330);
            pCodigoBarras.BackgroundImage = MY_IMAGE;
            var Margenes = new Margins(0, 0, 40, 0);
            /*PrintDocument printDoc = new PrintDocument();
            printDoc.OriginAtMargins = true;
            printDoc.DefaultPageSettings.Landscape = false;
            printDoc.DefaultPageSettings.Margins = Margenes;
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            printDoc.Print(); // */
        }
        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            Point ulCorner = new Point(40, 1);
            e.Graphics.DrawImage(photo, ulCorner);
            //InsertText(“OK”);
        }
        private void imprimir()
        {
            DialogResult result = pDImprimir.ShowDialog();
            if (result == DialogResult.OK)
            {
                //pDDocumento.PrinterSettings = pDImprimir.PrinterSettings.PrinterName; 

                CapturarForma();
                pDDocumento.PrinterSettings.PrinterName = pDImprimir.PrinterSettings.PrinterName;
                pDDocumento.Print();
            }
        }
        private void PaginaAImprimir(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }
        private void pDDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
        private void CapturarForma()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void bImprimir_Click(object sender, EventArgs e)
        {
            bImprimir.Visible = false;
            imprimir();
            Timer T = new Timer();
            T.Stop();
            T.Interval = 3000; //Intervalo de duracion 3000 ms => 3.000s
            T.Start(); //Inicializa el timer
            T.Stop();
            this.Close();
        }

        private void FCOdigoQR_Load(object sender, EventArgs e)
        {

            lProductoID.Text = _productoNombre;
            lUsuarioCreador.Text = _usuarioCreador;
            lCantidad.Text = _cantidad.ToString("N2") + "KG";
            lSuperSacoID.Text = "SS_ID: " +_supersacoID+"\n\rLOTE: "+_folioMSP+"\r\n"+_reimpresion;
            //DateTimePicker fecha = new DateTimePicker();
            // fecha.Value=DateTime.Today;
            if (_fechaCreacion == new DateTime())
                _fechaCreacion = DateTime.Now;
            lFechaCaptura.Text = _fechaCreacion.ToString("dddd, dd MMMM yyyy h:mm tt ");
            GenerarCodigoBarras(_supersacoID);
            lCentroCompra.Text = _centroCompra;
            /* Timer T = new Timer();
             T.Interval = 7000; //Intervalo de duracion 3000 ms => 3.000s
             T.Start(); //Inicializa el timer
             imprimir();

             T.Stop();
             this.Close();*/
        }

        private void lSuperSacoID_Paint(object sender, PaintEventArgs e)
        {
            /*
            e.Graphics.TranslateTransform(lSuperSacoID.Width, lSuperSacoID.ClientSize.Height);
            e.Graphics.RotateTransform(180);
            e.Graphics.DrawString(_supersacoID, lSuperSacoID.Font,
            Brushes.Black, (lSuperSacoID.ClientRectangle));
            lSuperSacoID.BringToFront();*/
        }
    }
}