﻿using System;
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
    public partial class FCon : Form
    {
        public FCon()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        string mensaje_error;
        Mensajes mensajes = new Mensajes();
        private string titulo_modulo = "Configuración";
        string selected_item = "",_almacenAnterior="",_usuarioModificarID="";
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void asignarPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FPerUsu fPer = new FPerUsu();
            fPer._Usuario.USUARIOID = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value); 
            fPer.ShowDialog();
        }
        private void CargarAlmacenesEnGrid()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "select INVENTARIO_FRIMEX_ID AS ALMACEN_ID, INVENTARIO_FRIMEX_FECHA_CREA AS FECHA_CREACION" +
                    ",ALMACEN_MSP_DESCRIPCION AS NOMBRE_ALMACEN, ESTATUS AS ESTATUS_ALMACEN from INVENTARIO_FRIMEX";
                DataTable _datos = new DataTable();
                SqlDataAdapter _DA = new SqlDataAdapter(consulta, cn.SC);
                _DA.Fill(_datos);
                dGVAlmacenes.DataSource = _datos;
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void bGuardarBodega_Click(object sender, EventArgs e)
        {
            try
            {
                string _Estatus, _nombre, _tipoMovimiento;
                //DataRowView _dRV = (DataRowView)cBEstatusBodega.SelectedItem;
                //_AlmacenID = _dRV.Row.ItemArray[0].ToString();
                _Estatus = cBEstatusBodega.SelectedItem.ToString() ;
                _tipoMovimiento = cBTipoOperacionBodega.SelectedItem.ToString();
                _nombre = tBNombreBodega.Text.Trim();
                ConexionSql cn = new ConexionSql();

                if (_nombre.Length >0 )
                    if (_tipoMovimiento.Length > 0)
                        if (_Estatus.Length > 0)
                        {
                            if (_tipoMovimiento == "ALTA")
                                _tipoMovimiento = "A";
                            else if (_tipoMovimiento == "BAJA")
                                _tipoMovimiento = "B";
                            else if (_tipoMovimiento == "CAMBIO")
                                _tipoMovimiento = "C";

                            if (_Estatus == "ACTIVO")
                                _Estatus = "A";
                            else if (_Estatus == "BAJA")
                                _Estatus = "B";

                            cn.ConectarSQLServer();
                            if(_tipoMovimiento == "A")
                            {
                                 string consulta = "INSERT INTO [dbo].[INVENTARIO_FRIMEX] " +
                                   " ([INVENTARIO_FRIMEX_ID] " +
                                   " ,[INVENTARIO_FRIMEX_USUARIO_CREA] " +
                                   " ,[INVENTARIO_FRIMEX_FECHA_CREA] " +
                                   " ,[ALMACEN_MSP_ID] " +
                                   " ,[ALMACEN_MSP_DESCRIPCION] " +
                                   " ,[ESTATUS]) " +
                                    " VALUES " +
                                   " ( " + cn.ObtenerSigID() +
                                   " , " + _Usuario.USUARIOID +
                                   " ,'" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                                   " , 0" +
                                   " ,'" +_nombre+"'"+
                                   " ,'"+_Estatus+"')";

                                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                                if(cmdm.ExecuteNonQuery() >0)
                                    MessageBox.Show("Almacen dado de alta correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Favor de reinentar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                cmdm.Dispose();
                                cn.Desconectar();
                            }else if (_tipoMovimiento == "B")
                            {
                                string _almId = tBAlmacenId.Text.Trim();
                                if (_almId.Length > 0)
                                {
                                    string consulta = "UPDATE [dbo].[INVENTARIO_FRIMEX] " +
                                           " SET " +
                                           " [ESTATUS] = 'B' " +
                                           " WHERE[INVENTARIO_FRIMEX_ID] = "+_almId;

                                    SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                                    if (cmdm.ExecuteNonQuery() > 0)
                                        MessageBox.Show("Almacen dado de baja correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        MessageBox.Show("Favor de reinentar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    cmdm.Dispose();
                                    cn.Desconectar();
                                }else
                                    MessageBox.Show("Favor de especificar el Id del almacen a dar de baja", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            }
                            else if (_tipoMovimiento == "C")
                            {

                                string _almId = tBAlmacenId.Text.Trim(),_desc=tBNombreBodega.Text.Trim();
                                if (_almId.Length > 0)                                
                                {
                                    if (_desc.Length > 0)
                                    {
                                        string consulta = "UPDATE [dbo].[INVENTARIO_FRIMEX] " +
                                               " SET[ALMACEN_MSP_DESCRIPCION] = '"+_desc+"'" +
                                               " ,[ESTATUS] = '" +_Estatus+"'"+
                                               " WHERE[INVENTARIO_FRIMEX_ID] ="+_almId;

                                        SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                                        if (cmdm.ExecuteNonQuery() > 0)
                                            MessageBox.Show("Almacen actualizado correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        else
                                            MessageBox.Show("Favor de reinentar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        cmdm.Dispose();
                                        cn.Desconectar();
                                    }
                                    else
                                        MessageBox.Show("Favor de especificar el nombre de almacen a modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                    MessageBox.Show("Favor de especificar el Id del almacen a modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    
                            }
                        }
                        else
                            MessageBox.Show("Favor de seleccionar estatus de bodega", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Favor de especificar el tipo de movimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Favor de especificar el nombre del almacen a crear", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarAlmacenesEnGrid();

            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tCConfiguracion_Selected(object sender, TabControlEventArgs e)
        {
            CargarAlmacenesEnGrid();
            CargarAlmacen(cBAlmacenes);
        }

        private void cBTipoOperacionBodega_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _TipoMovimiento = Convert.ToString(cBTipoOperacionBodega.SelectedItem);
            if(_TipoMovimiento.Length > 0)
            {
                if(_TipoMovimiento=="ACTIVO")
                {
                    lAlmacenId.Visible = false;
                    tBAlmacenId.Visible = false;
                }
                else if (_TipoMovimiento == "BAJA")
                {
                    lAlmacenId.Visible = true;
                    tBAlmacenId.Visible = true;
                }
                else if (_TipoMovimiento == "CAMBIO")
                {
                    lAlmacenId.Visible = true;
                    tBAlmacenId.Enabled = true;
                    tBAlmacenId.Visible = true;
                }
            }
            

        }

        private void tBAlmacenId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string _almId = tBAlmacenId.Text.Trim();
                if(_almId.Length >0)
                    CargarAlmacen(_almId);
            }
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
        private void CargarAlmacen(string Almacen_id)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                tBAlmacenId.Enabled = false;
                cn.ConectarSQLServer();
                string consulta = "select INVENTARIO_FRIMEX_ID AS ALMACEN_ID, INVENTARIO_FRIMEX_FECHA_CREA AS FECHA_CREACION" +
                    ",ALMACEN_MSP_DESCRIPCION AS NOMBRE_ALMACEN, ESTATUS AS ESTATUS_ALMACEN " +
                    " from INVENTARIO_FRIMEX " +
                    " where INVENTARIO_FRIMEX_ID ="+Almacen_id;
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                {
                    tBNombreBodega.Text = Convert.ToString(reader["NOMBRE_ALMACEN"]);
                    int _estatus = 0;
                    string _almEstatus= Convert.ToString(reader["ESTATUS_ALMACEN"]);
                    if (_almEstatus == "A")
                        _estatus = 0;
                    else if (_almEstatus == "B")
                        _estatus = 1;
                    else if (_almEstatus == "C")
                        _estatus = 2;
                    cBEstatusBodega.SelectedIndex = _estatus;
                }
                reader.Close();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CargarAlmacen(ComboBox cBAlmacen)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                //consulta
                //cn.ConectarMicrosip("FRIMEX_COMPRAS");
                // Insertar Orden de compra
                //_FolioOrdenCompra;
                // Insertar Encabezado
                cBAlmacen.DataSource = null;
                string cadena = "select INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +                   
                    " where ESTATUS = 'A'";
                DataTable Table = new DataTable();
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
                // FbDataReader readerm = cmdm.ExecuteReader();
                SqlDataAdapter DA = new SqlDataAdapter(cadena, cn.SC);
                DA.Fill(Table);
                if (Table != null)
                {
                    cBAlmacen.DataSource = Table.DefaultView;
                    cBAlmacen.ValueMember = "INVENTARIO_FRIMEX_ID";
                    cBAlmacen.DisplayMember = "ALMACEN_MSP_DESCRIPCION";
                }
                DA.Dispose();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private DataTable CargarAlmacen()
        {
            ConexionSql cn = new ConexionSql();
                DataTable Table = new DataTable();
            try
            {
                cn.ConectarSQLServer();
                //consulta
                //cn.ConectarMicrosip("FRIMEX_COMPRAS");
                // Insertar Orden de compra
                //_FolioOrdenCompra;
                // Insertar Encabezado
                string cadena = "select INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " where ESTATUS = 'A'";
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
                // FbDataReader readerm = cmdm.ExecuteReader();
                SqlDataAdapter DA = new SqlDataAdapter(cadena, cn.SC);
                DA.Fill(Table);
                DA.Dispose();
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return Table;
        }

        private void butAgregar_Click(object sender, EventArgs e)
        {
            UsuariosC nuevo_usuario = new UsuariosC();
            string msj = "";
            mensaje_error = "";
            try
            {

                //Set values:
                string _fecha = DateTime.Now.Date.Day.ToString() + "/" + DateTime.Now.Date.Month.ToString() + "/" + DateTime.Now.Date.Year.ToString() + " " + DateTime.Now.Date.TimeOfDay.Hours + ":" + DateTime.Now.Date.TimeOfDay.Minutes + ":" + DateTime.Now.Date.TimeOfDay.Seconds;
                nuevo_usuario.NOMBRE = txtNombre.Text;
                nuevo_usuario.APELLIDOP = tBApellidoP.Text;
                nuevo_usuario.APELLIDOM = tBApellidoM.Text;
                nuevo_usuario.USUARIO = txtNomUser.Text;
                nuevo_usuario.CONTRASEÑA = maskedPass.Text;
                //nuevo_usuario.EMAIL = txt.Text;
                //nuevo_usuario.TELEFONO = txt_usu_tel.Text;
                nuevo_usuario.TIPO_USU_ID = nuevo_usuario.OBTENER_TIPO_USU_ID(Convert.ToString(cBTipoUsuario.SelectedItem), out mensaje_error);
                nuevo_usuario.FECHACREACION = _fecha;
                nuevo_usuario.USUARIOCREADOR = _Usuario.USUARIOID;


                if (!String.IsNullOrEmpty(mensaje_error))
                    mensajes.Error(mensaje_error, titulo_modulo);

                else
                {
                    if (butAgregar.Text.Equals("Agregar Usuario"))
                    {
                        //Aqui va la consulta:
                        nuevo_usuario.ESTATUS = 'A';
                        msj = nuevo_usuario.Nuevo_Usuario(nuevo_usuario, out mensaje_error);
                        //Asignar el almacen al usuario
                        DataRowView _dRV = (DataRowView)cBAlmacenes.SelectedItem;
                        string _almacenID = _dRV.Row.ItemArray[0].ToString();
                        if (mensaje_error.Length == 0)
                        {
                            nuevo_usuario.InsertarUsuarioAlmacen(nuevo_usuario.USUARIO, _almacenID, _Usuario.USUARIOID.ToString(), out mensaje_error);
                            if (!String.IsNullOrEmpty(mensaje_error))
                                mensajes.Error(mensaje_error, titulo_modulo);
                            else
                            {
                                mensajes.Exito(msj, titulo_modulo);
                                dgvUsuarios = nuevo_usuario.MostrarUsuarios(dgvUsuarios, out mensaje_error);
                            }
                        }
                        else
                            mensajes.Error(mensaje_error, titulo_modulo);
                    }

                    else if (butAgregar.Text.Equals("Modificar"))
                    {
                        //Aqui va la consulta:
                        if (cbEstatusUsu.SelectedIndex == 0)
                            nuevo_usuario.ESTATUS = 'A';
                        else if (cbEstatusUsu.SelectedIndex == 0)
                            nuevo_usuario.ESTATUS = 'B';

                        msj = nuevo_usuario.Actualizar_Usuario(nuevo_usuario, out mensaje_error, selected_item);

                        if (!String.IsNullOrEmpty(mensaje_error))
                            mensajes.Error(mensaje_error, titulo_modulo);
                        else
                        {
                            mensajes.Exito(msj, titulo_modulo);
                            dgvUsuarios = nuevo_usuario.MostrarUsuarios(dgvUsuarios, out mensaje_error);
                        }
                    }
                    else if (butAgregar.Text.Equals("Dar de Baja"))
                    {
                        //Aqui va la consulta:
                        msj = nuevo_usuario.Baja_Usuario(nuevo_usuario, out mensaje_error, selected_item);

                        if (!String.IsNullOrEmpty(mensaje_error))
                            mensajes.Error(mensaje_error, titulo_modulo);
                        else
                        {
                            mensajes.Exito(msj, titulo_modulo);
                            dgvUsuarios = nuevo_usuario.MostrarUsuarios(dgvUsuarios, out mensaje_error);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                mensajes.Error(ex.Message, titulo_modulo);
            }

        }

        private void FCon_Load(object sender, EventArgs e)
        {
            UsuariosC usu = new UsuariosC();
            cBTipoUsuario = usu.MostrarTiposUsuario(cBTipoUsuario, out mensaje_error);
            dgvUsuarios = usu.MostrarUsuarios(dgvUsuarios, out mensaje_error);

            ConexionMicrosip con_msp = new ConexionMicrosip();
            ConexionSql con_sql = new ConexionSql();
            RegistrosWindows registros = new RegistrosWindows();

            registros.LeerRegistros("SOFTWARE\\SOTI\\FrimexProduccion");

            textBUserMicro.Text = registros.MICRO_USER;
            maskedPassMicro.Text = registros.MICRO_PASS;
            txtRootMicro.Text = registros.MICRO_ROOT;
            txtBDMicro.Text = registros.MICRO_BD;
            txtServerMicro.Text = registros.MICRO_SERVER;

            txtUserMSSql.Text = registros.SQL_USUARIO;
            maskedPassMSSql.Text = registros.SQL_PASSWORD;
            txtBDSQL.Text = registros.SQL_BD;
            txtServerMSSql.Text = registros.SQL_SERVIDOR;


            maskedPass.UseSystemPasswordChar = true;
            maskedpass2.UseSystemPasswordChar = true;
        }

        private void butProbConexionF_Click(object sender, EventArgs e)
        {
            //message box para conexion exitosa o error
            try
            {
                ConexionMicrosip ConexionMicro = new ConexionMicrosip();
                ConexionMicro.ConectarMicrosip(txtBDMicro.Text);
                ConexionMicro.Desconectar();
                MessageBox.Show("Conexion Exitosa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                 MessageBox.Show("Error de conexion\n" + ex.Message, "Error de conexion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void butProbConexionMS_Click(object sender, EventArgs e)
        {
            ConexionSql ConexionSQL = new ConexionSql();
            //message box para conexion exitosa o error
            try
            {
                ConexionSQL.ConectarSQLServer();
                SqlCommand sc = new SqlCommand("select * from usuarios_t", ConexionSQL.SC);
                ConexionSQL.Desconectar();
                sc.Connection.Open();
                //if (sc.Connection.State.ToString() == "Open")                     
                MessageBox.Show("Conexion Exitosa","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Information);
                ConexionSQL.Desconectar();
            }
            catch (Exception ex)
            {
                ConexionSQL.Desconectar();
                 MessageBox.Show("Error de conexion\n" + ex.Message, "Error de conexion", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void checkPass1_CheckedChanged(object sender, EventArgs e)
        {
            string text1 = maskedPass.Text,
                text2=maskedpass2.Text;
            if (checkPass1.Checked)
            {
                maskedPass.UseSystemPasswordChar = false;
                maskedpass2.UseSystemPasswordChar = false;
            }
            else
            {
                maskedPass.UseSystemPasswordChar = true;
                maskedpass2.UseSystemPasswordChar = true;
            }
        }

        private void butGuardarCambios_Click(object sender, EventArgs e)
        {
            //Actualizar datos del usuario
            string msg_local = "";
            if (maskedPass.Text == maskedpass2.Text)
            {
                UsuariosC usuarios = new UsuariosC();
                usuarios.NOMBRE = txtNombre.Text;
                usuarios.APELLIDOP = tBApellidoP.Text;
                usuarios.APELLIDOM = tBApellidoM.Text;
                usuarios.USUARIO = txtNomUser.Text;
                usuarios.CONTRASEÑA = maskedPass.Text;
                usuarios.TIPO_USU_ID = usuarios.OBTENER_TIPO_USU_ID(Convert.ToString(cBTipoUsuario.SelectedItem), out mensaje_error);
                int _auxEstatus = cbEstatusUsu.SelectedIndex;

                if (_auxEstatus == 0)
                    usuarios.ESTATUS = 'A';
                else if (_auxEstatus == 1)
                    usuarios.ESTATUS = 'B';

                usuarios.Actualizar_Usuario(usuarios, out msg_local, _usuarioModificarID);
                DataRowView _dRV = (DataRowView)cBAlmacenes.SelectedItem;
                string almacenNuevo = _dRV.Row.ItemArray[0].ToString();

                usuarios.ActualizarUsuarioAlmacen(_usuarioModificarID, _almacenAnterior, almacenNuevo, _Usuario.USUARIOID.ToString(), out msg_local);
                butAgregar.Enabled = true;
                txtNombre.Text = "";
                tBApellidoP.Text = "";
                tBApellidoM.Text = "";
                txtNomUser.Text = "";
                maskedPass.Text = "";
                maskedpass2.Text = "";
                dgvUsuarios = usuarios.MostrarUsuarios(dgvUsuarios, out mensaje_error);
            }
            else
            MessageBox.Show("Las contraseñas no son iguales","Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cargar datos del usuario
            string _UsuarioNombre = Convert.ToString(dgvUsuarios[7, e.RowIndex].Value);
            UsuariosC usuarios = new UsuariosC();
            List<string> _datosUsuario=usuarios.BuscarUsuario("Usuario",_UsuarioNombre);
            if(_datosUsuario.Count>0)
            {
                if (_datosUsuario.Count >= 12)
                {
                    _usuarioModificarID= _datosUsuario[0];
                    txtNombre.Text = _datosUsuario[1];
                    tBApellidoP.Text = _datosUsuario[2];
                    tBApellidoM.Text = _datosUsuario[3];
                    txtNomUser.Text = _datosUsuario[4];
                    maskedPass.Text = _datosUsuario[5];
                    maskedpass2.Text = _datosUsuario[5];
                    string _estatus= _datosUsuario[9],_tipoUsuario= _datosUsuario[6],_almacenes= _datosUsuario[11] ;
                    if (_estatus == "ACTIVO")
                        cbEstatusUsu.SelectedIndex = 0;
                    else if (_estatus == "BAJA")
                        cbEstatusUsu.SelectedIndex = 1;
                    int j = 0;
                    DataTable _Datosalmacenes = CargarAlmacen();
                    foreach (DataRow _fila in _Datosalmacenes.Rows)
                    {

                        string aux = Convert.ToString(_fila[1]);
                        if (_almacenes == aux)
                        {
                            _almacenAnterior= Convert.ToString(_fila[0]);
                            cBAlmacenes.SelectedIndex = j;
                            break;
                        }
                        j++;
                    }
                    
                    for(int i=0;i<cBTipoUsuario.Items.Count;i++)
                    {
                        //DataRowView _dRV = (DataRowView)cBTipoUsuario.SelectedItem;
                        string aux =Convert.ToString( cBTipoUsuario.Items[i]);
                        //string _tipoUsuID = _dRV.Row.ItemArray[0].ToString();
                        if (_tipoUsuario == aux)
                        {
                            cBTipoUsuario.SelectedIndex = i;
                            break;
                        }
                    }
                    //cbEstatusUsu
                    //cBTipoUsuario
                    //cBAlmacenes
                    
                }
                else
                {
                    MessageBox.Show("El usuario no cuenta con almacen asignado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _usuarioModificarID = _datosUsuario[0];
                    txtNombre.Text = _datosUsuario[1];
                    tBApellidoP.Text = _datosUsuario[2];
                    tBApellidoM.Text = _datosUsuario[3];
                    txtNomUser.Text = _datosUsuario[4];
                    maskedPass.Text = _datosUsuario[5];
                    maskedpass2.Text = _datosUsuario[5];
                    string _estatus = _datosUsuario[9], _tipoUsuario = _datosUsuario[6];
                    if (_estatus == "ACTIVO")
                        cbEstatusUsu.SelectedIndex = 0;
                    else if (_estatus == "BAJA")
                        cbEstatusUsu.SelectedIndex = 1;
                    int j = 0;

                    cBAlmacenes.SelectedIndex = -1;
                    for (int i = 0; i < cBTipoUsuario.Items.Count; i++)
                    {
                        //DataRowView _dRV = (DataRowView)cBTipoUsuario.SelectedItem;
                        string aux = Convert.ToString(cBTipoUsuario.Items[i]);
                        //string _tipoUsuID = _dRV.Row.ItemArray[0].ToString();
                        if (_tipoUsuario == aux)
                        {
                            cBTipoUsuario.SelectedIndex = i;
                            break;
                        }
                    }
                    //cbEstatusUsu
                    //cBTipoUsuario
                    //cBAlmacenes

                }
            }
        }
    }
}
