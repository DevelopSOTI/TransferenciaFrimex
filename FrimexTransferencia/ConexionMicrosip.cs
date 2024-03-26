using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace FrimexTransferencia
{
    class ConexionMicrosip
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
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                        + "; Database=" + reg.MICRO_ROOT + "\\" + db + ".FDB"
                        + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1";

                fbc = new FbConnection(conectionString);
                fbc.Open();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }
        public int GenDocto(string BDMicrosip)
        {
            int docto_cm_id=0;
            try
            {
                ConexionMicrosip cn = new ConexionMicrosip();
                
                cn.ConectarMicrosip(BDMicrosip);
                FbConnection con_microsip = new FbConnection(cn.CONECTIONSTRING);
                con_microsip.Open();
                FbCommand gen_docto_cm_id = new FbCommand("GEN_DOCTO_ID");
                gen_docto_cm_id.CommandType = CommandType.StoredProcedure;
                gen_docto_cm_id.Connection = con_microsip;
                docto_cm_id = Convert.ToInt32(gen_docto_cm_id.ExecuteScalar());
                gen_docto_cm_id.Cancel();
                con_microsip.Close();
                cn.Desconectar();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error");
            }
            return docto_cm_id;
        }
        public void ConectarConfigMicrosip()
        {
            try
            {
                reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                conectionString = @"User=" + reg.MICRO_USER + "; Password=" + reg.MICRO_PASS
                        + "; Database=" + reg.MICRO_ROOT + "\\" + "System" + "\\" + "CONFIG" + ".FDB"
                        + "; Datasource=" + reg.MICRO_SERVER + "; Dialect=3" + "; Charset=ISO8859_1";

                fbc = new FbConnection(conectionString);
                fbc.Open();
            }
            catch(Exception err)
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
