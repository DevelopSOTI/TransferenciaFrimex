using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.Control;

namespace FrimexTransferencia
{
    class PrivilegioUsuario
    {
        private int modulo_id;
        private int permiso_id;
        private string descripcion_permiso;
        private int usuario_id;

        public PrivilegioUsuario()
        {
            modulo_id = permiso_id = usuario_id = 0;
            descripcion_permiso = "";
        }
        public int USUARIO_ID
        {
            get { return usuario_id; }
            set { usuario_id = value; }
        }

        public string DESCRIPCION_PERMISO
        {
            get { return descripcion_permiso; }
            set { descripcion_permiso = value; }
        }

        public int PERMISO_ID
        {
            get { return permiso_id; }
            set { permiso_id = value; }
        }

        public int MODULO_ID
        {
            get { return modulo_id; }
            set { modulo_id = value; }
        }

        public TreeView ObtenerDerechosUsuario(int usuarioId, TreeView treeView1)
        {
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                string consulta = "SELECT * FROM MODULO_T order by DESCRIPCION";
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                int nodo = 0;
                while (reader.Read())
                {
                    treeView1.Nodes.Add(reader["DESCRIPCION"].ToString());
                    consulta = "SELECT * FROM PERMISOS_T where modulo_id = " + reader["MODULO_ID"].ToString() + "";
                    cmd = new SqlCommand(consulta, cn.SC);
                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        treeView1.Nodes[nodo].Nodes.Add(reader2["DESCRIPCION"].ToString());
                    }
                    nodo++;
                    reader2.Close();
                }
                reader.Close();
                cmd.Dispose();
                //Consulta para leer los derechos del usuario
                consulta = "SELECT p.descripcion, up.Usuario_ID, up.Permiso_ID, up.USUARIO_PERMISOS_activo FROM USUARIOS_PERMISOS_T as up " +
                    "join PERMISOS_T p on(p.Permiso_ID = up.Permiso_ID) " +
                    "where usuario_id = " + usuarioId + "";
                //consulta = "";
                cmd = new SqlCommand(consulta, cn.SC);
                reader = cmd.ExecuteReader();
                for (int p = 0; p < treeView1.Nodes.Count; p++)
                {
                    while (reader.Read())
                    {
                        for (int h = 0; h < treeView1.Nodes[p].Nodes.Count; h++)
                        {
                            string sss = reader["descripcion"].ToString();
                            if (treeView1.Nodes[p].Nodes[h].Text == reader["descripcion"].ToString())
                            {

                                treeView1.Nodes[p].Nodes[h].Checked = true;
                                treeView1.Nodes[p].Expand();
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
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return treeView1;
        }
        public bool GuardarCambios(int usuarioId, TreeView treeView1)
        {
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
                string consulta = "DELETE FROM USUARIOS_PERMISOS_T WHERE USUARIO_ID=" + usuarioId;
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                cmd.Dispose();


                //Recorrer el arbol para guardar los nuevos derechos de usuario
                int num_nodos = treeView1.GetNodeCount(true);
                arr_permiso_id = new string[num_nodos];
                arrModulId = new string[num_nodos];
                int cont = 0;
                TreeNodeCollection nodes = treeView1.Nodes;
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
                
                string CentroCompraID = "";
                if (usuarioId == 1)
                {
                    //Asignar permisos para usuario SYSDBA
                   // CentroCompraID = "1";
                    for (cont = 0; cont < arr_permiso_id.Length; cont++)
                    {
                        if (!String.IsNullOrEmpty(arr_permiso_id[cont]))
                        {
                            consulta = "select p.PERMISO_ID from PERMISOS_t p join modulo_T m on (p.MODULO_ID = m.modulo_id) " +
                                "where m.modulo_descripcion = '" + arrModulId[cont] + "' and p.PERMISO_DESCRIPCION = '" + arr_permiso_id[cont] + "'";
                            cmd = new SqlCommand(consulta, cn.SC);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                                permisoId = Convert.ToInt32(reader["PERMISO_ID"]);
                            cmd.Dispose();
                            reader.Close();

                            consulta = "INSERT INTO USUARIO_PERMISOS_T (USUARIO_PERMISOS,USUARIO_ID, PERMISO_ID) " +
                                "VALUES(" + cn.ObtenerSigIDCC(CentroCompraID).ToString() + "," + usuarioId + ", " + permisoId + ")";
                            cmd = new SqlCommand(consulta, cn.SC);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                        }
                    }
                    band = true;
                    //transaction.Commit();
                    cn.Desconectar();
                }
                else
                {
                    //ASIGNAR PERMISOS USUARIOS
                   // consulta = "SELECT CENTRO_COMPRA_ID FROM CENT_COMP_USUA WHERE USUARIO_ID=" + usuarioId;
                    //cmd2 = new SqlCommand(consulta, cn.SC);
                    //reader2 = cmd2.ExecuteReader();

                    //while (reader2.Read())
                    //{
                       // CentroCompraID = Convert.ToString(reader2["CENTRO_COMPRA_ID"]);
                        for (cont = 0; cont < arr_permiso_id.Length; cont++)
                        {
                            if (!String.IsNullOrEmpty(arr_permiso_id[cont]))
                            {
                                consulta = "select p.PERMISO_ID from PERMISOS_T p join modulo_t m on (p.MODULO_ID = m.modulo_id) " +
                                    "where m.descripcion = '" + arrModulId[cont] + "' and p.DESCRIPCION = '" + arr_permiso_id[cont] + "'";
                                cmd = new SqlCommand(consulta, cn.SC);
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                    permisoId = Convert.ToInt32(reader["PERMISO_ID"]);
                                cmd.Dispose();
                                reader.Close();

                                consulta = "INSERT INTO USUARIOS_PERMISOS_T (USUARIO_ID, PERMISO_ID) " +
                                    "VALUES(" + usuarioId + ", " + permisoId + ")";
                                cmd = new SqlCommand(consulta, cn.SC);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
                        }
                        band = true;
                        
                    //}
                    //cmd2.Cancel();
                    //reader2.Close();
                    cn.Desconectar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                band = false;
            }
            return band;
        }
        public TabControl PermisosUsuario(int usuario_id, int opcion, Form forma , TabControl tCConfiguracion)
        {
            try
            {
                switch(opcion)
                {
                    case 2:
                        string aux = "";
                        ControlCollection comp = forma.Controls;
                        ConexionSql conexion = new ConexionSql();
                        conexion.ConectarSQLServer();
                        string consulta = "select up.Usuario_ID, p.Modulo_ID, p.Permiso_ID, p.PERMISO_COMPONENTE from USUARIO_PERMISOS up inner join PERMISOS p on(up.Permiso_ID=p.Permiso_ID) where Usuario_ID=" + usuario_id;
                        SqlCommand com = new SqlCommand(consulta, conexion.SC);
                        SqlDataReader reader = com.ExecuteReader();
                        List<string> lista = new List<string>();

                        while (reader.Read())
                        {
                            if (reader["Modulo_id"].ToString().Equals("1")) //Permisos del modulo de usuarios
                                lista.Add(reader["permiso_componente"].ToString().Trim());
                        }
                        com.Cancel();
                        reader.Close();
                        bool band = false;
                        for (int i = 0; i < comp.Count; i++)
                        {
                            if (comp[i].Name.Equals("tCConfiguracion"))
                            {
                                for (int j =comp[i].Controls.Count-1 ; j >=0 ; j--)
                                {
                                    aux = comp[i].Controls[j].Name;
                                    band = false;
                                    for (int k = 0; k < lista.Count; k++)
                                    {
                                        if(aux.Equals(lista[k].ToString()))
                                        {
                                            band = true;
                                        }
                                    }
                                    if (band == false)
                                    {
                                        tCConfiguracion.TabPages.Remove(tCConfiguracion.TabPages[aux]);
                                    }
                                }
                            }
                        }
                        conexion.Desconectar();
                        break;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("Error al cargar los privilegios de usuario en el modulo de configuracion: "+err.Message, "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return tCConfiguracion;
        }
        public ControlCollection PermisosUsuario(int usuario_id, int opcion, Form forma)
        {
            bool band = false;
            TabControl tc = new TabControl();
            ControlCollection comp = forma.Controls;
            ControlCollection comp_aux = null;
            MenuStrip obj = new MenuStrip();
            //TabControl obj = new TabControl();
            Panel objp = new Panel();
            try
            {
                switch (opcion)
                {
                    //Opcion para validar modulos principales
                    case 1:
                        {
                            if (usuario_id == 0)
                            {
                                //Usuario SYSDBA
                                for (int i = 0; i < comp.Count; i++)
                                {
                                    comp[i].Visible = true;
                                }
                            }
                            else
                            {
                                ConexionSql conexion = new ConexionSql();
                                conexion.ConectarSQLServer();
                                string consulta = "select distinct(Modulo_ID) from USUARIOS_PERMISOS up inner join MODULOS_PERMISOS mp on(up.Permiso_ID=mp.Permiso_ID) where Usuario_ID=" + usuario_id;
                                SqlCommand com = new SqlCommand(consulta, conexion.SC);
                                SqlDataReader reader = com.ExecuteReader();
                                while (reader.Read())
                                {
                                    //2 usuarios, 3configuracion, 4Requisiciones
                                    if (reader["modulo_id"].ToString().Equals("2"))
                                    {
                                        for (int i = 0; i < comp.Count; i++)
                                        {
                                            if (comp[i].Name.Equals("butUser"))
                                            {
                                                comp[i].Visible = true;
                                            }
                                        }
                                    }
                                    else if (reader["modulo_id"].ToString().Equals("3"))
                                    {
                                        for (int i = 0; i < comp.Count; i++)
                                        {
                                            if (comp[i].Name.Equals("butConfig"))
                                            {
                                                comp[i].Visible = true;
                                            }
                                        }
                                    }
                                    else if (reader["modulo_id"].ToString().Equals("4"))
                                    {
                                        for (int i = 0; i < comp.Count; i++)
                                        {
                                            if (comp[i].Name.Equals("butReq"))
                                            {
                                                comp[i].Visible = true;
                                            }
                                        }

                                    }
                                }
                                com.Cancel();
                                reader.Close();
                                conexion.Desconectar();
                            }
                        }
                        break;
                    //Opcion para validar permisos de cada modulo
                    case 2:
                        {
                            
                            
                            ConexionSql conexion = new ConexionSql();
                            conexion.ConectarSQLServer();
                            string consulta = "select up.Usuario_ID, p.Modulo_ID, p.Permiso_ID, p.PERMISO_COMPONENTE from USUARIO_PERMISOS up inner join PERMISOS p on(up.Permiso_ID=p.Permiso_ID) where Usuario_ID=" + usuario_id;
                            SqlCommand com = new SqlCommand(consulta, conexion.SC);
                            SqlDataReader reader = com.ExecuteReader();
                            while (reader.Read())
                            {
                                //MessageBox.Show(reader["modulo_id"].ToString());
                                if (reader["Modulo_id"].ToString().Equals("2")) //Permisos del modulo de usuarios
                                {
                                    for (int i = 0; i < comp.Count; i++)
                                    {
                                        if (comp[i].Name.Equals("tabControl1"))
                                        //obj = (TabControl)comp[i];
                                        {
                                            for (int j = 0; j < comp[i].Controls.Count; j++)
                                            {
                                                if (comp[i].Controls[j].Name.Equals("tabPage1"))
                                                {
                                                    for (int m = 0; m < comp[i].Controls[j].Controls.Count; m++)
                                                    {
                                                        if (comp[i].Controls[j].Controls[m].Name.Equals("panel1"))
                                                        {
                                                            for (int k = 0; k < comp[i].Controls[j].Controls[m].Controls.Count; k++)
                                                            {
                                                                //MessageBox.Show(reader["componente"].ToString());
                                                                if (reader["permiso_componente"].ToString().Equals(comp[i].Controls[j].Controls[m].Controls[k].Name))
                                                                {
                                                                    comp[i].Controls[j].Controls[m].Controls[k].Enabled = true;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            for (int k = 0; k < comp[i].Controls[j].Controls[m].Controls.Count; k++)
                                                            {
                                                                // MessageBox.Show(comp[i].Controls[j].Controls[m].Controls[k].Name);
                                                                //MessageBox.Show(comp[i].Controls[j].Controls[m].Controls[k].ContextMenuStrip.Name);
                                                                //MessageBox.Show(reader["componente"].ToString());
                                                                string aux = reader["componente"].ToString();

                                                                if (aux.Equals(comp[i].Controls[j].Controls[m].Controls[k].ContextMenuStrip.Items["permisosDeUsuarioToolStripMenuItem"].Name))
                                                                {
                                                                    comp[i].Controls[j].Controls[m].Controls[k].ContextMenuStrip.Items["permisosDeUsuarioToolStripMenuItem"].Enabled = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (reader["modulo_id"].ToString().Equals("1"))//Permisos del modulo de configuraciones
                                {
                                    //for (int i = 0; i < comp.Count; i++)
                                    //{
                                    //    if (comp[i].Name.Equals("tCConfiguracion"))
                                    //    {
                                    //        for (int j = 0; j < comp[i].Controls.Count; j++)
                                    //        {
                                    //            string aux = reader["permiso_componente"].ToString().Trim();
                                    //            if (aux.Equals(comp[i].Controls[j].Name))
                                    //            {
                                    //                tc = (TabControl) comp[i];
                                    //                //MessageBox.Show(tc.TabPages[j].Name);
                                    //                tc.TabPages.Remove(tc.TabPages[j]);
                                    //                TabPage a=(TabPage)comp[i].Controls[j];
                                    //                band = true;

                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    
                                }
                                else if (reader["modulo_id"].ToString().Equals("4"))//Permisos del modulo de Requisiciones
                                {
                                    for (int i = 0; i < comp.Count; i++)
                                    {
                                        if (comp[i].Name.Equals("tabControlReq"))
                                        {
                                            for (int j = 0; j < comp[i].Controls.Count; j++)
                                            {
                                                if (comp[i].Controls[j].Name.Equals("tabPageMateriales"))
                                                {
                                                    for (int k = 0; k < comp[i].Controls[j].Controls.Count; k++)
                                                    {
                                                        if (comp[i].Controls[j].Controls[k].Name.Equals("dgvmateriales"))
                                                        {
                                                            string aux = Convert.ToString(reader["componente"]);
                                                            string aux2 = comp[i].Controls[j].Controls[k].ContextMenuStrip.Items["solicitarAdicionToolStripMenuItem"].Name;
                                                            //solicitarAdicionToolStripMenuItem
                                                            if (aux.Equals(aux2))
                                                            {
                                                                comp[i].Controls[j].Controls[k].ContextMenuStrip.Items["solicitarAdicionToolStripMenuItem"].Enabled = true;
                                                            }

                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                }

                            }
                            com.Cancel();
                            reader.Close();
                            conexion.Desconectar();
                        }
                        break;
                }

                if(band==true)
                {
                    comp_aux = tc.Controls;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los permisos de usuario: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            return comp_aux;
        }

    }
}
