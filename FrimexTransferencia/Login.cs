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
    public partial class Login : Form
    {
        internal static UsuariosC usuario = new UsuariosC();
        Mensajes mensajes = new Mensajes();
        public Login()
        {
            InitializeComponent();
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BAcceso_Click(object sender, EventArgs e)
        {
            Accesar();
        }
        private void Accesar()
        {
            //validacion de campos de acceso
            if (tBUsuario.Text.Length > 0)
            {
                if (tBContraseña.Text.Length > 0)
                {
                    UsuariosC usr = new UsuariosC();
                    string _usuario, _contraseña;
                    _usuario = tBUsuario.Text;
                    _contraseña = tBContraseña.Text;

                    Lexico lex1 = new Lexico(_usuario);
                    Lexico lex2 = new Lexico(_contraseña);

                    if (usr.LogIn(_usuario, _contraseña, out usuario) && !lex1.Analiza('d') && !lex2.Analiza('d'))
                    {
                        if (usuario.CONTRASEÑA == _contraseña)
                        {

                            FMenu fMenu = new FMenu();
                            fMenu._Usuario = usuario;
                            this.Hide();
                            fMenu.ShowDialog();
                            this.Show();
                            //if (usuario.TIPO_USU_ID == 6)
                            //{
                            //    FLineaProduccion flp = new FLineaProduccion();
                            //    this.Visible = false;
                            //    flp.ShowDialog();
                            //    this.Visible = true;
                            //}
                            //else if (usuario.TIPO_USU_ID == 7)
                            //{
                            //    FEtiquetaEmplaye FEE = new FEtiquetaEmplaye();
                            //    this.Visible = false;
                            //    FEE.Usuario = usuario;
                            //    FEE.ShowDialog();
                            //    this.Visible = true;
                            //}
                            //else if (usuario.TIPO_USU_ID != 6 && usuario.TIPO_USU_ID != 7)
                            //{
                            //    Form _Fmenu = new FMenu(usuario);
                            //    this.Visible = false;
                            //    _Fmenu.Activate();
                            //    _Fmenu.ShowDialog();
                            //    this.Visible = true;
                            //}
                        }
                        else
                        {
                            mensajes.Error("Contraseña Incorrecta", "Inicio Sesión");
                            tBContraseña.Text = "";
                        }
                    }
                    else
                    {
                        mensajes.Error("Error de Autenticacion", "Inicio Sesión");
                        tBContraseña.Text = "";
                    }
                }
                else
                {
                    mensajes.Advertencia("Campo Usuario Vacio", "Inicio Sesión");
                }
            }
            else
            {
                mensajes.Advertencia("Campo Contraseña Vacio", "Inicio Sesión");
            }
        }

        private void tBContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Accesar();
        }
    }
}
