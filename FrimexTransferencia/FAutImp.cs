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
    public partial class FAutImp : Form
    {
        public UsuariosC usuario = new UsuariosC();
        Mensajes mensajes = new Mensajes();
        public string TipoAutorizacion = "";
        public int TIPO_USU_ID = 0;
        public FAutImp()
        {
            InitializeComponent();
        }

        private void BAcceso_Click(object sender, EventArgs e)
        {

        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            Ejecutar();
        }
        private void Ejecutar()
        {
            if (tBUsuario.Text.Length > 0)
            {
                if (tBContraseña.Text.Length > 0)
                {
                    string _usuario, _contraseña;
                    _usuario = tBUsuario.Text;
                    _contraseña = tBContraseña.Text;

                    Lexico lex1 = new Lexico(_usuario);
                    Lexico lex2 = new Lexico(_contraseña);
                    if (TipoAutorizacion == "IMPRESION")
                    {
                        UsuariosC usr = new UsuariosC();

                        if (usr.LogIn(_usuario, _contraseña, out usuario) && !lex1.Analiza('d') && !lex2.Analiza('d'))
                        {
                            if (usuario.CONTRASEÑA == _contraseña)
                            {
                                this.Close();
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
                    else if (TipoAutorizacion == "BODEGA")
                    {
                        string consulta = "select * from USUARIOS_T where USUARIO='" + _usuario + "' and [PASSWORD]='" + _contraseña + "'";
                        string auxContraseña = "", auxUsuario = "";
                        ConexionSql conexionSql = new ConexionSql();
                        conexionSql.ConectarSQLServer();
                        SqlCommand sqlcom = new SqlCommand(consulta, conexionSql.SC);
                        SqlDataReader reader = sqlcom.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                auxContraseña = Convert.ToString(reader["PASSWORD"]);
                                auxUsuario = Convert.ToString(reader["USUARIO"]);
                                TIPO_USU_ID = Convert.ToInt32(reader["TIPO_USU_ID"]);
                            }
                        if (_usuario == auxUsuario)
                            if (_contraseña == auxContraseña)
                            {
                                UsuariosC usr = new UsuariosC();

                                if (usr.LogIn_P(_usuario, _contraseña, out usuario) && !lex1.Analiza('d') && !lex2.Analiza('d'))
                                {
                                    if (usuario.CONTRASEÑA == _contraseña)
                                    {

                                        this.Close();
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
                                mensajes.Advertencia("La contraseña no coincide", "Inicio Sesión");
                        else
                            mensajes.Advertencia("El usuario no válido", "Inicio Sesión");
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

        private void tBContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Ejecutar();
            }
        }
    }
}
