using System;
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
    public partial class FMenu : Form
    {
        public FMenu()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        string startupPath = System.IO.Directory.GetCurrentDirectory();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bRegEmb_Click(object sender, EventArgs e)
        {
            FRegEmb fReg = new FRegEmb();
            fReg._Usuario = _Usuario;
            fReg.ShowDialog();
            
        }

        private void bRecEmb_Click(object sender, EventArgs e)
        {
            FRecEmb fRec = new FRecEmb();
            fRec._Usuario = _Usuario;
            fRec.ShowDialog();
        }

        private void bSalTra_Click(object sender, EventArgs e)
        {
            //FVerReqTra fSal = new FVerReqTra();
            //fSal._Usuario = _Usuario;
            //fSal.ShowDialog();
            FVerTra fVerTra = new FVerTra();
            fVerTra._Usuario = _Usuario;
            fVerTra.ShowDialog();
        }

        private void bCon_Click(object sender, EventArgs e)
        {
            FCon fCon = new FCon();
            fCon._Usuario = _Usuario;
            fCon.ShowDialog();
        }

        private void bRep_Click(object sender, EventArgs e)
        {
            FRep fRep = new FRep();
            fRep._Usuario = _Usuario;
            fRep.ShowDialog();
        }

        private void bRecTra_Click(object sender, EventArgs e)
        {
            FRecTra fRecTra = new FRecTra();
            fRecTra._Usuario = _Usuario;
            fRecTra.ShowDialog();
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            PermisosC permisos = new PermisosC();

            string msg = "";
            List<string> modulos = permisos.ObtenerModulosUsuario(_Usuario.USUARIOID, out msg);
            foreach (string modulo in modulos)
            {
                if (modulo.Equals("CONFIGURACION"))
                {
                    bCon.Enabled = true;
                }
                else if (modulo.Equals("RECEPCIÓN DE MERCANCIA"))
                {
                    bRecEmb.Enabled = true;
                }
                else if (modulo.Equals("REGISTRO DE MERCANCIA"))
                {
                    bRegEmb.Enabled = true;
                }
                else if (modulo.Equals("RECEPCION POR TRANSFERENCIA"))
                {
                    bRecTra.Enabled = true;
                }
                else if (modulo.Equals("SALIDA POR TRASPASO"))
                {
                    bSalTra.Enabled = true;
                }
                else if (modulo.Equals("REQUISICIONES DE MERCANCIA"))
                {
                    bRequisiciones.Enabled = true;
                }
                else if (modulo.Equals("REPORTES"))
                {
                    bRep.Enabled = true;
                }
                else if (modulo.Equals("LOTES"))
                {
                    bLotes.Enabled = true;
                }
            }

            if (bCon.Enabled == false)
            {
                bCon.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Configuracion2.png");
            }

            if (bRegEmb.Enabled == false)
            {
                bRegEmb.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Registro2.png");
            }
            if (bRecTra.Enabled == false)
            {
                bRecTra.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Entrada2.png");
            }

            if (bSalTra.Enabled == false)
            {
                bSalTra.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Salidas2.png");
            }
            if (bRequisiciones.Enabled == false)
            {
                bRequisiciones.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Requisiciones2.png");
            }

            if (bRecEmb.Enabled == false)
            {
                bRecEmb.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Entrada2.png");
            }
            if (bRep.Enabled == false)
            {
                bRep.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Reporte2.png");
            }
            if (bLotes.Enabled == false)
            {
                bLotes.BackgroundImage = Image.FromFile(startupPath + "\\iconos\\Lotes2.png");
            }
        }

        private void bRequisiciones_Click(object sender, EventArgs e)
        {
            FVisReq freq = new FVisReq();
            freq._Usuario = _Usuario;
            freq.ShowDialog();
        }

        private void bLotes_Click(object sender, EventArgs e)
        {
            FAdmLot fAdm = new FAdmLot();
            fAdm._Usuario = _Usuario;
            fAdm.ShowDialog();
        }
    }
}
