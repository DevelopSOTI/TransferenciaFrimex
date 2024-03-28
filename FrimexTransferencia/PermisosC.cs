using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrimexTransferencia
{
    class PermisosC
    {
        //Obtener Permisos del usuario
        public List<string> ObtenerPermisosUsuario(int usuario_id, out string msg)
        {
            string msg_local = "";
            string consulta = "";
            List<string> lista_permisos_usuario = new List<string>();
            SqlCommand command;
            SqlDataReader sqlreader;
            ConexionSql conexionSql = new ConexionSql();
            try
            {
                consulta = "SELECT  UPP.USUARIO_ID, UPP.PERMISO_ID, MPP.MODULO_ID, MPP.PERMISO_ID ,MPP.DESCRIPCION, MPP.COMPONENTE FROM USUARIOS_PERMISOS_T UPP "+
                            "INNER JOIN MODULOS_PERMISOS_T MPP ON(UPP.PERMISO_ID = MPP.PERMISO_ID) "+
                            "WHERE UPP.USUARIO_ID ="+usuario_id;

                conexionSql.ConectarSQLServer();
                command = new SqlCommand(consulta, conexionSql.SC);
                sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    lista_permisos_usuario.Add(Convert.ToString(sqlreader["DESCRIPCION"]));
                }
                command.Cancel();
                sqlreader.Close();
                conexionSql.Desconectar();
            }
            catch (Exception err)
            {
                msg_local = err.Message;
            }
            msg = msg_local;
            return lista_permisos_usuario;
        }


        //Obtener solamente los modulos que tiene asigandos el usuario
        public List<string> ObtenerModulosUsuario(int usuario_id, out string msg)
        {
            string msg_local = "";
            string consulta="";
            List<string> lista_modulos_usuario = new List<string>();
            SqlCommand command;
            SqlDataReader sqlreader;
            ConexionSql conexionSql = new ConexionSql();
            try
            {
                consulta = "SELECT DISTINCT MP.MODULO_ID, MP.DESCRIPCION FROM USUARIOS_T AS UP " +
                            " INNER JOIN USUARIOS_PERMISOS_T AS UPP ON(UP.USUARIO_ID= UPP.USUARIO_ID) " +
                            " INNER JOIN PERMISOS_T AS MPP ON(UPP.PERMISO_ID= MPP.PERMISO_ID) " +
                            " INNER JOIN MODULO_T AS MP ON(MPP.MODULO_ID= MP.MODULO_ID) " +
                            " WHERE UP.USUARIO_ID = " + usuario_id +
                            " GROUP BY MP.MODULO_ID, MP.DESCRIPCION; ";

                conexionSql.ConectarSQLServer();
                command = new SqlCommand(consulta, conexionSql.SC);
                sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    lista_modulos_usuario.Add(Convert.ToString(sqlreader["DESCRIPCION"]));
                }
                command.Cancel();
                sqlreader.Close();
                conexionSql.Desconectar();
            }
            catch(Exception err)
            {
                msg_local = err.Message;
            }
            msg = msg_local;
            return lista_modulos_usuario;
        }

        //Guardar Permisos de Usuario
        public bool GuardarPermisosUsuario(int usuario_id, TreeView treeView_PermisosUsuario, out string msg)
        {
            string msg_local = "";
            bool band = false;
            try
            {
                string[] arr_permiso_id, arrModulId;
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                //SqlTransaction transaction = cn.SC.BeginTransaction();
                //SqlTransaction transaction2 = cn.SC.BeginTransaction();
                SqlDataReader reader, reader2;
                SqlCommand cmd2;
                //Borrar los antiguos derechos de usuaris
                string consulta = "DELETE FROM USUARIOS_PERMISOS_T WHERE USUARIO_ID=" + usuario_id;
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd.Cancel();


                //Recorrer el arbol para guardar los nuevos derechos de usuario
                int num_nodos = treeView_PermisosUsuario.GetNodeCount(true);
                arr_permiso_id = new string[num_nodos];
                arrModulId = new string[num_nodos];
                int cont = 0;
                TreeNodeCollection nodes = treeView_PermisosUsuario.Nodes;
                foreach (TreeNode n in nodes)
                {
                    foreach (TreeNode tn in n.Nodes)
                    {
                        if (tn.Checked == true)
                        {
                            arr_permiso_id[cont] = tn.Text;
                            arrModulId[cont] = n.Text;
                            cont++;
                        }
                    }
                }
                //Insertar los valores del arreglo en la tabla USUARIOS_PERMISOS
                cont = 0;
                int permisoId = 0;

                //Asignar permisos para usuario SYSDBA
                for (cont = 0; cont < arr_permiso_id.Length; cont++)
                {
                    if (!String.IsNullOrEmpty(arr_permiso_id[cont]))
                    {
                        consulta = "SELECT P.PERMISO_ID FROM MODULOS_PERMISOS_T P INNER JOIN MODULOS_T M ON (P.MODULO_ID = M.MODULO_ID) " +
                            "WHERE M.DESCRIPCION = '" + arrModulId[cont] + "' AND P.DESCRIPCION = '" + arr_permiso_id[cont] + "'";
                        cmd = new SqlCommand(consulta, cn.SC);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                            permisoId = Convert.ToInt32(reader["PERMISO_ID"]);
                        cmd.Cancel();
                        reader.Close();

                        consulta = "INSERT INTO USUARIOS_PERMISOS_T (USUARIO_ID, PERMISO_ID) " +
                            "VALUES("+ usuario_id + ", " + permisoId + ")";
                        cmd = new SqlCommand(consulta, cn.SC);
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                band = true;
                cn.Desconectar();
            }
            catch (Exception ex)
            {
                msg_local = ex.Message;
                band = false;
            }

            msg = msg_local;
            return band;
        }

        //Obtener Permisos de Usuario para mostrarlos en la vista de arbol
        public TreeView MostrarPermisosUsuario(int usuario_id, TreeView treeView_PermisosUsuario)
        {
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                string consulta = "SELECT * FROM MODULOS_T ORDER BY DESCRIPCION";
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                int nodo = 0;
                while (reader.Read())
                {
                    treeView_PermisosUsuario.Nodes.Add(reader["DESCRIPCION"].ToString());
                    consulta = "SELECT * FROM MODULOS_PERMISOS_T WHERE MODULO_ID = " + reader["MODULO_ID"].ToString() + "";
                    cmd = new SqlCommand(consulta, cn.SC);
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        treeView_PermisosUsuario.Nodes[nodo].Nodes.Add(reader2["DESCRIPCION"].ToString());
                    }
                    nodo++;
                    reader2.Close();
                }
                reader.Close();
                cmd.Dispose();
                //Consulta para leer los derechos del usuario
                consulta = "SELECT P.DESCRIPCION, UP.USUARIO_ID, UP.PERMISO_ID FROM USUARIOS_PERMISOS_T AS UP " +
                    "INNER JOIN MODULOS_PERMISOS_T P ON(P.PERMISO_ID = UP.PERMISO_ID) " +
                    "WHERE UP.USUARIO_ID = " + usuario_id + "";
                //consulta = "";
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                for (int p = 0; p < treeView_PermisosUsuario.Nodes.Count; p++)
                {
                    while (reader.Read())
                    {
                        for (int h = 0; h < treeView_PermisosUsuario.Nodes[p].Nodes.Count; h++)
                        {
                            string sss = reader["DESCRIPCION"].ToString();
                            if (treeView_PermisosUsuario.Nodes[p].Nodes[h].Text == reader["DESCRIPCION"].ToString())
                            {

                                treeView_PermisosUsuario.Nodes[p].Nodes[h].Checked = true;
                                treeView_PermisosUsuario.Nodes[p].Expand();
                                break;
                            }
                        }
                    }
                    reader.Close();
                    reader = cmd.ExecuteReader();
                }
                reader.Close();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return treeView_PermisosUsuario;
        }
    }
}
