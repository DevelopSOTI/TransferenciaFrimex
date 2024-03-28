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
    public partial class FPerUsu : Form
    {
        public UsuariosC _Usuario = new UsuariosC();
        public FPerUsu()
        {
            InitializeComponent();
        }

        private void FPerUsu_Load(object sender, EventArgs e)
        {
            PrivilegioUsuario pu = new PrivilegioUsuario();
            treeViewPrivilegios = pu.ObtenerDerechosUsuario(_Usuario.USUARIOID, treeViewPrivilegios);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrivilegioUsuario pu = new PrivilegioUsuario();
            if (pu.GuardarCambios(_Usuario.USUARIOID, treeViewPrivilegios))
            {
                MessageBox.Show("Cambios Guardados","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se guardaron los cambios","Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
