﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrimexTransferencia
{
    public class UsuariosC
    {
        private string nombre, apellidoP, apellidoM, /*clave_depto, Email, telefono,*/ tipo, fechaCreacion, fechaUltimaModif, usuario, contraseña;
        private char estatus;
        private int usuarioID, usuarioCreador, usuarioModificador, departamento_msp_id, empresa_msp_id, tipo_usu_id;
        private string mensaje_error;

        public UsuariosC()
        {
            nombre = usuario = contraseña = /*clave_depto = Email = telefono =*/ tipo = fechaCreacion = fechaUltimaModif = "";
            estatus = ' ';
            usuarioID = usuarioCreador = usuarioModificador = departamento_msp_id = empresa_msp_id = 0;
        }

        public int TIPO_USU_ID { get; set; }
        public string MENSAJE_ERROR
        {
            set;
            get;
        }

        public int USUARIOID
        {
            set { usuarioID = value; }
            get { return usuarioID; }
        }
        public string NOMBRE
        {
            set { nombre = value; }
            get { return nombre; }
        }
        public string APELLIDOP
        {
            set { apellidoP = value; }
            get { return apellidoP; }
        }
        public string APELLIDOM
        {
            set { apellidoM = value; }
            get { return apellidoM; }
        }
        public string USUARIO
        {
            set { usuario = value; }
            get { return usuario; }
        }
        public string CONTRASEÑA
        {
            set { contraseña = value; }
            get { return contraseña; }
        }

        //public string CLAVE_DEPTO
        //{
        //    set { clave_depto = value; }
        //    get { return clave_depto; }
        //}
        //public string EMAIL
        //{
        //    set { Email = value; }
        //    get { return Email; }
        //}
        //public string TELEFONO
        //{
        //    set { telefono = value; }
        //    get { return telefono; }
        //}

        public string TIPO
        {
            set { tipo = value; }
            get { return tipo; }
        }
        public char ESTATUS
        {
            set { estatus = value; }
            get { return estatus; }
        }
        public int USUARIOCREADOR
        {
            set { usuarioCreador = value; }
            get { return usuarioCreador; }
        }
        public int USUARIOMODIFICADOR
        {
            set { usuarioModificador = value; }
            get { return usuarioModificador; }
        }
        public int DEPARTAMENTO_MSP_ID
        {
            set { departamento_msp_id = value; }
            get { return departamento_msp_id; }
        }

        public int EMPRESA_MSP_ID
        {
            set { empresa_msp_id = value; }
            get { return empresa_msp_id; }
        }
        public string FECHACREACION
        {
            set { fechaCreacion = value; }
            get { return fechaCreacion; }
        }

        public string FECHAULTIMAMODIF
        {
            set { fechaUltimaModif = value; }
            get { return fechaUltimaModif; }
        }

        //Metodo para Login
        public bool LogIn(string usuario, string pass, out UsuariosC obj_usuario)
        {
            bool resultado = false;
            UsuariosC obj_aux = new UsuariosC();
            try
            {

                string consulta = "";
                int num_reg = 0;

                ConexionSql ConexionSQL = new ConexionSql();
                ConexionSQL.ConectarSQLServer();
                consulta = "SELECT *" +
                             "FROM USUARIOS_T AS U " +
                             "LEFT JOIN TIPO_USUARIO_t TU ON(U.TIPO_USU_ID=TU.TIPO_USU_ID) " +
                             "WHERE U.PASSWORD = '" + pass + "' AND U.USUARIO = '" + usuario + "' AND U.ESTATUS='A'";
                //"' and USUARIO_ALIAS = '" + usuario +"'";
                SqlCommand sc = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader sdr = sc.ExecuteReader();

                while (sdr.Read())
                {
                    num_reg++;
                    obj_aux.USUARIOID = Convert.ToInt32(sdr["USUARIO_ID"]);
                    obj_aux.NOMBRE = sdr["NOMBRE"].ToString().Trim();
                    obj_aux.APELLIDOP = sdr["APELLIDOP"].ToString().Trim();
                    obj_aux.APELLIDOM = sdr["APELLIDOM"].ToString().Trim();
                    //obj_aux.EMAIL = sdr["CORREO"].ToString().Trim();
                    //obj_aux.TELEFONO = sdr["TELEFONO"].ToString().Trim();
                    obj_aux.TIPO = sdr["TIPO_USU_ID"].ToString().Trim();
                    obj_aux.ESTATUS = Convert.ToChar(sdr["ESTATUS"].ToString().Trim());
                    obj_aux.FECHACREACION = sdr["FECHA_CREACION"].ToString().Trim();
                    obj_aux.CONTRASEÑA = sdr["PASSWORD"].ToString().Trim();
                    obj_aux.USUARIO = sdr["USUARIO"].ToString().Trim();
                    obj_aux.TIPO_USU_ID = Convert.ToInt32(sdr["TIPO_USU_ID"].ToString().Trim());
                }

                if (num_reg == 0)
                {
                    resultado = false;
                }
                else if (num_reg == 1)
                {
                    resultado = true;
                }
                ConexionSQL.Desconectar();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            obj_usuario = obj_aux;
            return resultado;
        }


        //Mostrar todos los usuarios
        public DataGridView MostrarUsuarios(DataGridView dgvUsuarios, out string mensaje_error)
        {
            string consulta = "";
            string msg_error = "";
            dgvUsuarios.Rows.Clear();
            try
            {
                ConexionSql ConexionSQL = new ConexionSql();
                ConexionSQL.ConectarSQLServer();
                consulta = "SELECT UP.*, TUP.NOMBRE AS NOM_TIPO_USU FROM USUARIOS_T UP INNER JOIN TIPO_USUARIO_P TUP ON(UP.TIPO_USU_ID=TUP.TIPO_USU_ID) WHERE UP.USUARIO_ID > 1 ORDER BY UP.NOMBRE";
                SqlCommand sc = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader sdr = sc.ExecuteReader();
                int i = 0;
                while (sdr.Read())
                {
                    dgvUsuarios.Rows.Add();
                    dgvUsuarios[0, i].Value = sdr["USUARIO_ID"].ToString();
                    dgvUsuarios[1, i].Value = sdr["NOMBRE"].ToString();
                    dgvUsuarios[2, i].Value = sdr["APELLIDOP"].ToString();
                    dgvUsuarios[3, i].Value = sdr["APELLIDOM"].ToString();
                    dgvUsuarios[7, i].Value = sdr["USUARIO"].ToString();
                    //dgvUsuarios[5, i].Value = sdr["PASSWORD"].ToString();
                    //dgvUsuarios[6, i].Value = sdr["CORREO"].ToString();
                    //dgvUsuarios[7, i].Value = sdr["TELEFONO"].ToString();
                    dgvUsuarios[4, i].Value = sdr["NOM_TIPO_USU"].ToString();
                    dgvUsuarios[6, i].Value = sdr["FECHA_CREACION"].ToString();
                    //dgvUsuarios[10, i].Value = sdr["USU_CREADOR_ID"].ToString();
                    string _estatus = Convert.ToString(sdr["ESTATUS"]).Trim();
                    if (_estatus=="A")
                        dgvUsuarios[5, i].Value = true;
                    else
                        dgvUsuarios[5, i].Value = false;
                    i++;
                }

                ConexionSQL.Desconectar();
            }
            catch (Exception ex)
            {

                msg_error = ex.Message;
            }
            mensaje_error = msg_error;

            return dgvUsuarios;
        }
        //public class DataGridViewCheckboxCellFilter : DataGridViewCheckBoxCell
        //{
        //    public DataGridViewCheckboxCellFilter() : base()
        //    {
        //        this.FalseValue = 0;
        //        this.TrueValue = 1;
        //        this.Value = TrueValue;
        //    }
        //}
        public ComboBox MostrarTiposUsuario(ComboBox cb_tipo_usu, out string mensaje_error)
        {
            string consulta = "";
            string msg_error = "";
            try
            {
                ConexionSql con_sql = new ConexionSql();
                //Encoding obj_e = Encoding.GetEncoding("UTF-8");

                //Cargar Departamentos de Microsip de la empresa DEMO
                con_sql.ConectarSQLServer();
                consulta = "SELECT * FROM TIPO_USUARIO_T  WHERE TIPO_USU_ID > 1 ORDER BY NOMBRE";
                SqlCommand sq = new SqlCommand(consulta, con_sql.SC);
                SqlDataReader reader = sq.ExecuteReader();
                cb_tipo_usu.Items.Clear();
                string nom_tipo_usu = "";
                while (reader.Read())
                {
                    nom_tipo_usu = reader["NOMBRE"].ToString();
                    cb_tipo_usu.Items.Add(nom_tipo_usu);
                }
                con_sql.Desconectar();
            }
            catch (Exception ex)
            {
                msg_error = ex.Message;
            }
            mensaje_error = msg_error;

            return cb_tipo_usu;
        }

        public int OBTENER_TIPO_USU_ID(string nom_tipo_usu, out string mensaje_error)
        {
            string consulta = "";
            string msg_error = "";
            int tipo_usu_id = 0;
            try
            {
                ConexionSql con_sql = new ConexionSql();
                //Encoding obj_e = Encoding.GetEncoding("UTF-8");

                //Cargar Departamentos de Microsip de la empresa DEMO
                con_sql.ConectarSQLServer();
                consulta = "SELECT TIPO_USU_ID FROM TIPO_USUARIO_T  WHERE NOMBRE='" + nom_tipo_usu + "'";
                SqlCommand sq = new SqlCommand(consulta, con_sql.SC);
                SqlDataReader reader = sq.ExecuteReader();
                while (reader.Read())
                {
                    tipo_usu_id = Convert.ToInt32(reader["TIPO_USU_ID"].ToString());
                }
                con_sql.Desconectar();
            }
            catch (Exception ex)
            {
                msg_error = ex.Message;
            }
            mensaje_error = msg_error;

            return tipo_usu_id;
        }

        //Alta Usuarios
        public string Nuevo_Usuario(UsuariosC obj, out string mensaje_error)
        {
            Mensajes m = new Mensajes();
            string salida = "";
            string msg_error = "";
            try
            {
                //Validar que los campos no esten vacios
                if (!String.IsNullOrEmpty(obj.NOMBRE))
                {
                    Lexico lex = new Lexico(obj.NOMBRE);
                    if (!lex.Analiza('a'))
                        if (!String.IsNullOrEmpty(obj.APELLIDOP))
                        {
                            lex = new Lexico(obj.APELLIDOP);
                            if (!lex.Analiza('a'))
                                if (!String.IsNullOrEmpty(obj.APELLIDOM))
                                {
                                    lex = new Lexico(obj.APELLIDOM);
                                    if (!lex.Analiza('a'))
                                        if (!String.IsNullOrEmpty(obj.USUARIO))
                                        {
                                            lex = new Lexico(obj.USUARIO);
                                            if (!lex.Analiza('d'))
                                                if (!String.IsNullOrEmpty(obj.CONTRASEÑA))
                                                {
                                                    lex = new Lexico(obj.CONTRASEÑA);
                                                    if (!lex.Analiza('d'))
                                                        //if (!String.IsNullOrEmpty(obj.EMAIL))
                                                        //{
                                                        //    lex = new Lexico(obj.EMAIL);
                                                        //    if (!lex.Analiza('c'))
                                                        //    {
                                                        //        bool PhoneNumber = true;
                                                        //if (!String.IsNullOrEmpty(obj.TELEFONO))
                                                        //{
                                                        //    lex = new Lexico(obj.TELEFONO);
                                                        //    if (lex.Analiza('b'))
                                                        //        PhoneNumber = false;
                                                        //}

                                                    if ((obj.TIPO_USU_ID == 2 || obj.TIPO_USU_ID == 3 || obj.TIPO_USU_ID == 4 || obj.TIPO_USU_ID == 5 || obj.TIPO_USU_ID == 6 || obj.TIPO_USU_ID == 7 || obj.TIPO_USU_ID == 8 || obj.TIPO_USU_ID == 9 || obj.TIPO_USU_ID == 10) /*&& PhoneNumber*/)
                                                                {
                                                                    //Validar contenido de los campos(solo letras o solo numeros y longitud)
                                                                    if (Existe_NombreUsuario(obj.USUARIO) == false)
                                                                    {
                                                                        ConexionSql cnSql = new ConexionSql();
                                                                        cnSql.ConectarSQLServer();
                                                                        SqlCommand comm = new SqlCommand();
                                                                        comm.Connection = cnSql.SC;
                                                                        string insert = "INSERT INTO USUARIOS_T (NOMBRE, APELLIDOP, APELLIDOM, USUARIO, PASSWORD,  TIPO_USU_ID, FECHA_CREACION, USU_CREADOR, ESTATUS)"
                                                                                        + "VALUES (" +
                                                                                                    "'" + obj.NOMBRE + "'," +
                                                                                                    "'" + obj.APELLIDOP + "'," +
                                                                                                    "'" + obj.APELLIDOM + "'," +
                                                                                                    "'" + obj.USUARIO + "'," +
                                                                                                    "'" + obj.CONTRASEÑA + "'," +
                                                                                                    //"'" + obj.EMAIL + "'," +
                                                                                                    //"'" + obj.TELEFONO + "'," +
                                                                                                    "" + obj.TIPO_USU_ID + "," +
                                                                                                    "'" + obj.FECHACREACION + "'," +
                                                                                                    "" + obj.USUARIOCREADOR + "," +
                                                                                                    "'" + obj.ESTATUS + "')";

                                                                        comm.CommandText = insert;
                                                                        comm.ExecuteNonQuery();
                                                                        salida = "Nuevo Usuario Registrado";
                                                                        cnSql.Desconectar();
                                                                    }
                                                                    else
                                                                    {
                                                                        msg_error = "El nombre de usuario: " + USUARIO + " ya existe";
                                                                    }
                                                                }
                                                            //    else
                                                            //    {
                                                            //        if (!PhoneNumber)
                                                            //            msg_error = "Campo NUMERO TELEFONICO no valido";
                                                            //        else
                                                            //            msg_error = "Tipo Usuario no valido";
                                                            //    }
                                                            //}
                                                        //    else
                                                        //    {
                                                        //        msg_error = "Campo CORREO ELECTRONICO no valido";
                                                        //    }
                                                        //}
                                                        //else
                                                        //{
                                                        //    msg_error = "Campo de Correo Electronica vacio";
                                                        //}
                                                    else
                                                    {
                                                        msg_error = "Usuario no valido";
                                                    }
                                                }
                                                else
                                                {
                                                    msg_error = "Campo Contraseña vacio";
                                                }
                                            else
                                            {
                                                msg_error = "Campo NOMBRE DE USUARIO no valido";
                                            }
                                        }
                                        else
                                        {
                                            msg_error = "Campo Usuario vacio";
                                        }
                                    else
                                    {
                                        msg_error = "Campo APELLIDO MATERNO no valido";
                                    }
                                }
                                else
                                {
                                    msg_error = "Campo Apellido Materno vacio";
                                }
                            else
                            {
                                msg_error = "Campo APELLIDO PATERNO no valido";
                            }
                        }
                        else
                        {
                            msg_error = "Campo Apellido Paterno vacio";
                        }
                    else
                    {
                        msg_error = "Campo NOMBRE no valido";
                    }
                }
                else
                {
                    msg_error = "Campo Nombre vacio";
                }
            }
            catch (Exception ex)
            {
                msg_error = ex.Message;
            }
            mensaje_error = msg_error;

            return salida;
        }

        //Baja Usuarios
        public string Baja_Usuario(UsuariosC obj, out string mensaje_error, string userID)
        {
            Mensajes m = new Mensajes();
            string salida = "";
            string msg_error = "";
            try
            {
                ConexionSql cnSql = new ConexionSql();
                cnSql.ConectarSQLServer();
                SqlCommand comm = new SqlCommand();
                comm.Connection = cnSql.SC;
                string update = "UPDATE USUARIOS_P SET "
                                + "ESTATUS = 'B' "
                                + "WHERE USUARIO_ID = " + userID;
                comm.CommandText = update;
                comm.ExecuteNonQuery();
                salida = "Usuario Actualizado";
                cnSql.Desconectar();
            }
            catch (Exception ex)
            {
                msg_error = ex.Message;
            }

            mensaje_error = msg_error;
            return salida;
        }


        //Actualizar Usuarios
        public string Actualizar_Usuario(UsuariosC obj, out string mensaje_error, string userID)
        {
            Mensajes m = new Mensajes();
            string salida = "";
            string msg_error = "";
            try
            {
                //Validar que los campos no esten vacios
                if (!String.IsNullOrEmpty(obj.NOMBRE))
                {
                    Lexico lex = new Lexico(obj.NOMBRE);
                    if (!lex.Analiza('a'))
                        if (!String.IsNullOrEmpty(obj.APELLIDOP))
                        {
                            lex = new Lexico(obj.APELLIDOP);
                            if (!lex.Analiza('a'))
                                if (!String.IsNullOrEmpty(obj.APELLIDOM))
                                {
                                    lex = new Lexico(obj.APELLIDOM);
                                    if (!lex.Analiza('a'))
                                        if (!String.IsNullOrEmpty(obj.USUARIO))
                                        {
                                            lex = new Lexico(obj.USUARIO);
                                            if (!lex.Analiza('d'))
                                                if (!String.IsNullOrEmpty(obj.CONTRASEÑA))
                                                {
                                                    lex = new Lexico(obj.CONTRASEÑA);
                                                    if (!lex.Analiza('d'))
                                                        //if (!String.IsNullOrEmpty(obj.EMAIL))
                                                        //{
                                                        //    lex = new Lexico(obj.EMAIL);
                                                        //    if (!lex.Analiza('c'))
                                                        //    {
                                                        //        bool PhoneNumber = true;
                                                        //        if (!String.IsNullOrEmpty(obj.TELEFONO))
                                                        //        {
                                                        //            lex = new Lexico(obj.TELEFONO);
                                                        //            if (lex.Analiza('b'))
                                                        //                PhoneNumber = false;
                                                        //        }

                                                                if ((obj.TIPO_USU_ID == 2 || obj.TIPO_USU_ID == 3 || obj.TIPO_USU_ID == 4 || obj.TIPO_USU_ID == 5 || obj.TIPO_USU_ID == 6 || obj.TIPO_USU_ID == 7 || obj.TIPO_USU_ID == 8)/* && PhoneNumber*/)
                                                                {
                                                                    ConexionSql cnSql = new ConexionSql();
                                                                    cnSql.ConectarSQLServer();
                                                                    SqlCommand comm = new SqlCommand();
                                                                    comm.Connection = cnSql.SC;
                                                            string update = "UPDATE USUARIOS_T SET "
                                                                            + "NOMBRE = '" + obj.NOMBRE + "', "
                                                                            + "APELLIDOP = '" + obj.APELLIDOP + "', "
                                                                            + "APELLIDOM = '" + obj.APELLIDOM + "', "
                                                                            //+ "CORREO = '" + obj.EMAIL + "', "
                                                                            //+ "TELEFONO = '" + obj.TELEFONO + "', "
                                                                            + "TIPO_USU_ID = " + obj.TIPO_USU_ID + ", "
                                                                            + "ESTATUS = '" + obj.ESTATUS + "', " +
                                                                            " PASSWORD='" + obj.CONTRASEÑA + "'"
                                                                            + "WHERE USUARIO_ID = " + userID;
                                                                    comm.CommandText = update;
                                                                    comm.ExecuteNonQuery();
                                                                    salida = "Usuario Actualizado";
                                                                    cnSql.Desconectar();
                                                                }
                                                                else
                                                                {
                                                                    //if (!PhoneNumber)
                                                                    //    msg_error = "Campo NUMERO TELEFONICO no valido";
                                                                    //else
                                                                    //    msg_error = "Tipo Usuario no valido";
                                                                }
                                                            //}
                                                        //    else
                                                        //    {
                                                        //        msg_error = "Campo CORREO ELECTRONICO no valido";
                                                        //    }
                                                        //}
                                                        //else
                                                        //{
                                                        //    msg_error = "Campo de Correo Electronica vacio";
                                                        //}
                                                    else
                                                    {
                                                        msg_error = "Campo CONTRASEÑA no valido";
                                                    }
                                                }
                                                else
                                                {
                                                    msg_error = "Campo Contraseña vacio";
                                                }
                                            else
                                            {
                                                msg_error = "Campo NOMBRE DE USUARIO no valido";
                                            }
                                        }
                                        else
                                        {
                                            msg_error = "Campo Usuario vacio";
                                        }
                                    else
                                    {
                                        msg_error = "Campo APELLIDO MATERNO no valido";
                                    }
                                }
                                else
                                {
                                    msg_error = "Campo Apellido Materno vacio";
                                }
                            else
                            {
                                msg_error = "Campo APELLIDO PATERNO no valido";
                            }
                        }
                        else
                        {
                            msg_error = "Campo Apellido Paterno vacio";
                        }
                    else
                    {
                        msg_error = "Campo NOMBRE no valido";
                    }
                }
                else
                {
                    msg_error = "Campo Nombre vacio";
                }
            }
            catch (Exception ex)
            {
                msg_error = ex.Message;
            }
            mensaje_error = msg_error;

            return salida;
        }


        public bool Existe_NombreUsuario(string nomUsuario)
        {
            int cont = 0;
            bool existe = true;
            try
            {
                ConexionSql cnSql = new ConexionSql();
                cnSql.ConectarSQLServer();
                string consulta = "SELECT USUARIO_ID FROM USUARIOS_T WHERE USUARIO = '" + nomUsuario + "'";
                SqlCommand sc = new SqlCommand(consulta, cnSql.SC);
                SqlDataReader sdr = sc.ExecuteReader();
                while (sdr.Read())
                    cont++;
                if (cont == 1)
                    existe = true;
                else
                    existe = false;

                cnSql.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return existe;
        }


        /*public ComboBox MostrarDepartamentosMicrosip(ComboBox comboDepartamento)
        {
            string consulta = "";
            try
            {
                ConexionMicrosip ConexionMSP = new ConexionMicrosip();
                Encoding obj_e = Encoding.GetEncoding("UTF-8");

                //Cargar Departamentos de Microsip de la empresa DEMO
                ConexionMSP.ConectarMicrosip("DEMO");
                consulta = "SELECT * FROM DEPTOS_CO ORDER BY NOMBRE";
                FbCommand fb = new FbCommand(consulta, ConexionMSP.FBC);
                FbDataReader reader = fb.ExecuteReader();
                comboDepartamento.Items.Clear();
                string depto = "";
                string aux = "";
                while (reader.Read())
                {
                    depto = (reader["NOMBRE"].ToString());
                    comboDepartamento.Items.Add(depto);
                }
                ConexionMSP.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return comboDepartamento;
        }*/

        //Obtener Clave Departamento Microsip
        /* public string ObtenerClaveDepartamento(string departamento)
         {
             string consulta = "";
             string depto_clave = "";
             try
             {
                 ConexionMicrosip ConexionMSP = new ConexionMicrosip();

                 //Cargar Departamentos de Microsip de la empresa DEMO
                 ConexionMSP.ConectarMicrosip("DEMO");
                 consulta = "SELECT * FROM DEPTOS_CO WHERE NOMBRE= '"+departamento+"' ORDER BY NOMBRE";
                 FbCommand fb = new FbCommand(consulta, ConexionMSP.FBC);
                 FbDataReader reader = fb.ExecuteReader();

                 string aux = "";
                 while (reader.Read())
                 {
                     depto_clave = (reader["CLAVE"].ToString());
                 }
                 ConexionMSP.Desconectar();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
             return depto_clave;
         }*/

        public bool CamposVacios()
        {

            return false;
        }

        //Modificar Usuarios
        /* public void Modificar(int id)
         {
             try
             {
                 ConexionSql cnSql = new ConexionSql();
                 cnSql.ConectarSQLServer();
                 SqlCommand sc = new SqlCommand();
                 sc.Connection = cnSql.SC;
                 sc.CommandText = "update Usuarios set " +
                     "Nombre = '" + NOMBRE +
                     "', Usuario = '" + USUARIO +
                     "', Contraseña = '" + CONTRASEÑA +
                     "', Requisitante = '" + REQUISITANTE +
                     "', Departamento = '" + DEPARTAMENTO +
                     "', Correo = '" + EMAIL +
                     "', Privilegio = '" + PRIVILEGIO +
                     "', Estatus = '" + ESTATUS +
                     "', Fecha_Ultima_Modificacion = '" + FECHAULTIMAMODIF +
                     "', Usuario_Modificador = '" + USUARIOMODIFICADOR +
                     "', Clave_Depto = '" + CLAVE_DEPTO + 
                     "' where Usuario_id = "+ id;

                 //MessageBox.Show(sc.CommandText);
                 sc.ExecuteNonQuery();
                 sc.Dispose();
                 cnSql.Desconectar();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }*/

        //METODO PARA DETERMINAR SI UN NOMBRE DE USUARIO YA ESTA REGISTRADO EN LA BD


        public List<string> BuscarUsuario(string tipo_busqueda, string cadena_busqueda)
        {
            List<string> datosUsuario = new List<string>() ;

            try
            {
                string consulta = "";
                ConexionSql cnSql = new ConexionSql();
                cnSql.ConectarSQLServer();
                if (tipo_busqueda.Equals("Nombre"))
                {
                    if (String.IsNullOrEmpty(cadena_busqueda))
                    {
                        consulta = "SELECT * FROM USUARIOS ORDER BY NOMBRE";
                    }
                    else
                    {
                        consulta = "SELECT * FROM USUARIOS WHERE NOMBRE LIKE '" + cadena_busqueda + "%' ORDER BY NOMBRE";
                    }

                }
                else if (tipo_busqueda.Equals("Usuario"))
                {
                    if (String.IsNullOrEmpty(cadena_busqueda))
                    {
                        consulta = "SELECT * FROM USUARIOS ORDER BY NOMBRE";
                    }
                    else
                    {
                        consulta = "select UT.*,TUT.NOMBRE AS TIPO_USU from USUARIOS_T AS UT " +
                            " INNER JOIN TIPO_USUARIO_T AS TUT ON UT.TIPO_USU_ID = TUT.TIPO_USU_ID " +
                            " WHERE UT.USUARIO LIKE '" + cadena_busqueda + "%' ORDER BY UT.NOMBRE";
                    }

                }
                else if (tipo_busqueda.Equals("Departamento"))
                    consulta = "SELECT * FROM USUARIOS WHERE DEPARTAMENTO= '" + cadena_busqueda + "' ORDER BY NOMBRE";
                else if (tipo_busqueda.Equals("Estatus"))
                {
                    if (cadena_busqueda.Equals("ACTIVO"))
                        cadena_busqueda = "A";
                    else
                        cadena_busqueda = "B";

                    consulta = "SELECT * FROM USUARIOS " +
                        " INNER JOIN " +
                        " WHERE ESTATUS='" + cadena_busqueda + "' ORDER BY NOMBRE";
                }
                SqlCommand sc = new SqlCommand(consulta, cnSql.SC);
                SqlDataReader sdr = sc.ExecuteReader();
                int i = 0;
                while (sdr.Read())
                {
                    datosUsuario.Add(sdr["USUARIO_ID"].ToString().Trim());
                    datosUsuario.Add(sdr["NOMBRE"].ToString().Trim());
                    datosUsuario.Add(sdr["APELLIDOP"].ToString().Trim());
                    datosUsuario.Add(sdr["APELLIDOM"].ToString().Trim());
                    datosUsuario.Add(sdr["USUARIO"].ToString().Trim());
                    datosUsuario.Add(sdr["PASSWORD"].ToString().Trim());
                    datosUsuario.Add(sdr["TIPO_USU"].ToString().Trim());
                    datosUsuario.Add(sdr["Usu_Creador"].ToString().Trim());
                    datosUsuario.Add(sdr["Fecha_Creacion"].ToString().Trim());
                    string _estatus=Convert.ToString(sdr["Estatus"]).Trim();
                    if (_estatus=="A")
                        datosUsuario.Add("ACTIVO");
                    else
                        datosUsuario.Add("BAJA");
                    i++;
                }
                sc.Dispose();
                sdr.Close();
                cnSql.Desconectar();
                //Buscar usuario en almacen
                consulta = "select UA.ALMACEN_ID,IFR.ALMACEN_MSP_DESCRIPCION from USU_ALM  as UA " +
                    " inner join INVENTARIO_FRIMEX as IFR on UA.ALMACEN_ID = IFR.INVENTARIO_FRIMEX_ID" +
                    " WHERE UA.USUARIO_ID =" + datosUsuario[0];
                cnSql.ConectarSQLServer();
                sc = new SqlCommand(consulta, cnSql.SC);
                sdr = sc.ExecuteReader();
                if (sdr.HasRows)
                    while (sdr.Read())
                    {
                        datosUsuario.Add(sdr["ALMACEN_ID"].ToString().Trim());
                        datosUsuario.Add(sdr["ALMACEN_MSP_DESCRIPCION"].ToString().Trim());
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return datosUsuario;
        }
        public bool LogIn_P(string usuario, string pass, out UsuariosC obj_usuario)
        {
            bool resultado = false;
            UsuariosC obj_aux = new UsuariosC();
            try
            {

                string consulta = "";
                int num_reg = 0;

                ConexionSql ConexionSQL = new ConexionSql();
                ConexionSQL.ConectarSQLServer();
                //consulta = "SELECT * FROM USUARIOS WHERE USUARIO='"+ usuario +"' AND CONTRASEÑA= '"+ pass +"' AND ESTATUS='A'";
                consulta = "SELECT * " +
                             "FROM [dbo].[USUARIOS_T] " +
                             "WHERE [PASSWORD] = '" + pass +
                             "' and USUARIO = '" + usuario + "' AND ESTATUS='A'";
                //"' and USUARIO_ALIAS = '" + usuario +"'";
                SqlCommand sc = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader sdr = sc.ExecuteReader();

                while (sdr.Read())
                {
                    num_reg++;
                    /*obj_aux.USUARIOID = Convert.ToInt32(sdr["USUARIO_ID"]);
                    obj_aux.NOMBRE = sdr["USUARIO_NOMBRE"].ToString().Trim();
                    obj_aux.APELLIDOP = sdr["USUARIO_APELLIDOP"].ToString().Trim();
                    obj_aux.APELLIDOM = sdr["USUARIO_APELLIDOM"].ToString().Trim();
                    obj_aux.EMAIL = sdr["USUARIO_CORREO"].ToString().Trim();
                    obj_aux.TELEFONO = sdr["USUARIO_TELEFONO"].ToString().Trim();
                    obj_aux.TIPO = sdr["USUARIO_TIPO"].ToString().Trim();
                    obj_aux.ESTATUS = Convert.ToChar(sdr["USUARIO_ESTATUS"].ToString().Trim());
                    //obj_aux.FECHACREACION=sdr["USUARIO_FEHCA_CREACION"].ToString().Trim();
                    obj_aux.FECHAULTIMAMODIF = sdr["USUARIO_FECHA_ULTIMA_MODIFICACION"].ToString().Trim();
                    obj_aux.USUARIOMODIFICADOR = Convert.ToInt32(sdr["USUARIO_MODIFICADOR"]);*/
                    obj_aux.CONTRASEÑA = sdr["PASSWORD"].ToString().Trim();
                    obj_aux.USUARIO = sdr["USUARIO"].ToString().Trim();
                    obj_aux.TIPO_USU_ID =Convert.ToInt32( sdr["TIPO_USU_ID"].ToString().Trim());
                }

                if (num_reg == 0)
                {
                    resultado = false;
                }
                else if (num_reg == 1)
                {
                    resultado = true;
                }
                ConexionSQL.Desconectar();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            obj_usuario = obj_aux;
            return resultado;
        }

        public bool InsertarUsuarioAlmacen(string Usuario,string AlmacenID,string UsuarioCreador,out string msg)
        {
            bool _exito = false;
            string msg_local = "", consulta = "",_usuarioID="";
            try
            {
                consulta = "SELECT TOP 1 [USUARIO_ID] " +
                    " FROM [USUARIOS_T] " +
                    " WHERE USUARIO = '" + Usuario + "'";
                ConexionSql cnSql = new ConexionSql();
                cnSql.ConectarSQLServer();
                SqlCommand comm = new SqlCommand(consulta,cnSql.SC);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        _usuarioID = Convert.ToString(reader["USUARIO_ID"]);
                        //cnSql.Desconectar();
                reader.Close();
                        consulta = "INSERT INTO [dbo].[USU_ALM] " +
                            " ([USUARIO_ID] " +
                            " ,[ALMACEN_ID] " +
                            " ,[USU_ALM_ESTATUS] " +
                            " ,[FECHA_CREACION] " +
                            " ,[USUARIO_CREADOR]) " +
                            " VALUES " +
                            " ( " + _usuarioID +
                            " ," + AlmacenID +
                            " ,'A' " +
                            " , GETDATE() " +
                            " ," + UsuarioCreador + ")";
                        comm.Connection = cnSql.SC;
                        comm.CommandText = consulta;
                        comm.ExecuteNonQuery();
                _exito = true;
                        break;
                    }
                else
                    _exito = false;
                comm.Dispose();
                cnSql.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _exito;
        }
        public bool ActualizarUsuarioAlmacen(string Usuario, string AlmacenIDAnterior,string AlmacenIDNuevo, string UsuarioCreador, out string msg)
        {
            bool _exito = false;
            string msg_local = "", consulta = "", _usuarioID = "";
            try
            {
                consulta = "SELECT TOP 1 [USUARIO_ID] " +
                    " FROM [USUARIOS_T] " +
                    " WHERE USUARIO_ID = '" + Usuario + "'";
                ConexionSql cnSql = new ConexionSql();
                cnSql.ConectarSQLServer();
                SqlCommand comm = new SqlCommand(consulta, cnSql.SC);
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        _usuarioID = Convert.ToString(reader["USUARIO_ID"]);
                        //cnSql.Desconectar();
                        reader.Close();
                        consulta = "UPDATE [dbo].[USU_ALM] " +
                            " SET[ALMACEN_ID] = " + AlmacenIDNuevo +
                            " WHERE " +
                            " [USUARIO_ID] = " + _usuarioID +
                            " AND[ALMACEN_ID] = "+ AlmacenIDAnterior;
                        comm.Connection = cnSql.SC;
                        comm.CommandText = consulta;
                        comm.ExecuteNonQuery();
                        break;
                    }
                else
                    _exito = false;
                comm.Dispose();
                cnSql.Desconectar();
                _exito = true;
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _exito;
        }
    }
}