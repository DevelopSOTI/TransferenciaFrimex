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
    public partial class FImpTra : Form
    {
        public FImpTra()
        {
            InitializeComponent();
        }
        Mensajes mensaje = new Mensajes();
        //Image photo = null;
        private ArrayList MY_DATA = new ArrayList();
        public UsuariosC Usuario = new UsuariosC();
        public string _BodegaNombre = "", _DestinoNombre="", _NoTransferencia="";
        public List<string[,]>  _Productos = new List<string[,]>();
        //public string _usuarioCreador = "";
        //public string _centroCompra = "";
        //public DateTime _fechaCreacion = new DateTime();
        public Double _cantidad = 0;

        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;
        Bitmap memoryImage;
        private void pDDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void bImprimir_Click(object sender, EventArgs e)
        {
            bImprimir.Visible = false;
            imprimir();
            imprimir();
            Timer T = new Timer();
            T.Stop();
            T.Interval = 3000; //Intervalo de duracion 3000 ms => 3.000s
            T.Start(); //Inicializa el timer
            T.Stop();
            this.Close();
        }

        private void FImpTra_Load(object sender, EventArgs e)
        {
            lOrigen.Text += _BodegaNombre;
            lDestino.Text += _DestinoNombre;
            lFechaYhora.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            lUsuario.Text = Usuario.USUARIO;
            lNoTransf.Text += _NoTransferencia;
            lProductoID.Text = "Cant    Producto \n\r";
            for (int i =0;i<_Productos.Count;i++)
            {
                string _AuxCant = _Productos[i][0,0];
                if (_AuxCant.Length < 2)
                    _AuxCant = "0" + _AuxCant;
                lProductoID.Text += _AuxCant + "   " + _Productos[i][0,1]+"\n\r";
            }
        }
        private void CapturarForma()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }
        private void imprimir()
        {
            //DialogResult result = pDImprimir.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //pDDocumento.PrinterSettings = pDImprimir.PrinterSettings.PrinterName;             
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                {
                    CapturarForma();
                    pDDocumento.PrinterSettings.PrinterName = printer;
                    pDDocumento.Print();
                } 
             }
            //}
        }

    }
}