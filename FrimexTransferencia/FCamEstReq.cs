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
    public partial class FCamEstReq : Form
    {
        public FCamEstReq()
        {
            InitializeComponent();
        }

        public UsuariosC _Usuario = new UsuariosC();
        private Mensajes mensajes = new Mensajes();
        public int REQUISICIONES_ID = 0;
        private void bCambiar_Click(object sender, EventArgs e)
        {
            string aux = cBEstatus.SelectedItem.ToString(),msg_local="";
            if (aux == "Cancelada")
                aux = "C";
            else if (aux == "Pendiente")
                aux = "A";
            else if (aux == "Terminada")
                aux = "T";
            else if (aux == "En Trancito")
                aux = "E";
            CRequisiciones requisiciones = new CRequisiciones();
            if (requisiciones.CambiarEstatusRequisicion(REQUISICIONES_ID, aux, out msg_local))
            {
                mensajes.Exito("Estatus de la requisicion " + REQUISICIONES_ID + " actualizado a \"" + cBEstatus.SelectedItem.ToString() + "\"", "Exito");
                this.Close();
            }
            else
                mensajes.Exito("Estatus de la requisicion " + REQUISICIONES_ID + " no se pudo actualizar\n\r" + msg_local, "Error");
        }

        private void FCamEstReq_Load(object sender, EventArgs e)
        {
            this.Text += " - "+REQUISICIONES_ID;
        }
    }
}
