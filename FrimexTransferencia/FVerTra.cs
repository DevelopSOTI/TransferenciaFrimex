﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrimexTransferencia
{
    public partial class FVerTra : Form
    {
        public FVerTra()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        private void bNuevaTransferencia_Click(object sender, EventArgs e)
        {
            FVerReqTra fSal = new FVerReqTra();
            fSal._Usuario = _Usuario;
            fSal.ShowDialog();
        }

        private void FVerTra_Load(object sender, EventArgs e)
        {
            cBEstatus.SelectedIndex = 1;
            CRequisiciones requisiciones = new CRequisiciones();
            string msg_local = "";
            requisiciones.CargarAlmacenes(cBAlmacen, _Usuario.USUARIOID);
            if(cBAlmacen.Items.Count>0)
                cBAlmacen.SelectedIndex = 0;
            dGVTransferencias.DataSource = null;
            CargarDatos(Estatus(), Convert.ToDateTime("19-05-2020"), DateTime.Now,out msg_local);
            for (int colIdx = 0; colIdx < dGVTransferencias.Columns.Count; colIdx++)
                dGVTransferencias.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;

            if (msg_local.Length>0)
                MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public string Estatus()
        {
            string _estatus = "";
            try
            {
                string aux = cBEstatus.SelectedItem.ToString();
                if (aux == "En transito")
                    _estatus = "T";
                else if (aux == "Terminadas")
                    _estatus = "A";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Error");
            }
            return _estatus;
        }

        private void bMostrarRequisiciones_Click(object sender, EventArgs e)
        {
            string msg_local = "";
            CargarDatos(Estatus(), dTPInicio.Value, dTPFin.Value, out msg_local);
            if(msg_local.Length>0)
                MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool CargarDatos(string Estatus,DateTime FInicio,DateTime FFin,out string msg)
        {
            bool _exito = false;
            string msg_local = "";
            try
            {
                CTransferencias transferencias = new CTransferencias();           
                dGVTransferencias.DataSource = transferencias.VerTransferencias(Estatus, FInicio, FFin, out msg_local) ;
                _exito = true;
            }
            catch(Exception Ex)
            {
                msg_local = Ex.Message;
                _exito = false;
            }
            msg = msg_local;
            return _exito;
        }

        private void dGVTransferencias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //invocar Ventana de detalle de transferenecias
            int _fila = 0, _columna = 0;
            _fila = e.RowIndex;
            _columna = e.ColumnIndex;
            if(_fila>-1 && _columna>-1)
            {
                FTraDet det = new FTraDet();
                det._Usuario = _Usuario;
                det._TransferenciaID = Convert.ToInt32(dGVTransferencias["TRANSFERENCIA_ID",_fila].Value);
                det.ShowDialog();
            }
        }

        private void bContinuarTra_Click(object sender, EventArgs e)
        {
            FSalTra salTra = new FSalTra();
            salTra._Usuario = _Usuario;
            salTra._ReqID = -1;
            salTra.Text = "Salida de supersacos de la Requisición - " + salTra._ReqID + " -";
            salTra.ShowDialog();
        }
    }
}