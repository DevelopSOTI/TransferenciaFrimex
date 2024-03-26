using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace ApiInventarios
{
   public class ConexionMicrosip
    {
        private string conectionString;
        private string user, password, database, root, dataSource;
        private FbConnection fbc;
        private RegistrosWindows reg;

        public ConexionMicrosip ()
        {
            user = password = database = root = dataSource = "";            
        }

        public string USER
        {
            set { user = value; } get { return user; }
        }

        public string PASSWORD
        {
            set { password = value; } get { return password; }
        }

        public string DATABASE
        {
            set { database = value; } get { return database; }
        }

        public string ROOT
        {
            set { root = value; } get { return root; }
        }

        public string DATASOURSE
        {
            set { dataSource = value; } get { return dataSource; }
        }

        public string CONECTIONSTRING
        {
            set { conectionString = value; } get { return conectionString; }
        }

        public FbConnection FBC
        {
            set { fbc = value; } get { return fbc; }
        }

        public RegistrosWindows REG
        {
            set { reg = value; } get { return reg; }
        }

        public void ConectarMicrosip (string db)
        {
            try
            {
                reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexProduccion");

                /*conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                        + "; Database=" + reg.MICRO_ROOT + "\\" + db + ".FDB"
                        + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1" + "; Port=3055"; // */

                 conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                      + "; Database=" + reg.MICRO_ROOT + "\\" + db + ".FDB"
                      + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1"; // */

                fbc = new FbConnection(conectionString);
                fbc.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        public void ConectarConfigMicrosip()
        {
            try
            {
                reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexProduccion");

                /* conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                        + "; Database=" + reg.MICRO_ROOT + "\\" + "System" + "\\" + "CONFIG" + ".FDB"
                        + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1" + "; Port=3055"; // */

                conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                       + "; Database=" + reg.MICRO_ROOT + "\\" + "System" + "\\" + "CONFIG" + ".FDB"
                       + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1"; // */

                fbc = new FbConnection(conectionString);
                fbc.Open();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        public void Desconectar ()
        {
            fbc.Close();
        }
    }
}
