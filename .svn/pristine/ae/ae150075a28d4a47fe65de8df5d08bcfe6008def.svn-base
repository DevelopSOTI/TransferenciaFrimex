using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;

namespace FrimexTransferencia
{
    class RegistrosWindows
    {

        RegistryKey rk1 = Registry.CurrentUser;
        RegistryKey rk2 = Registry.CurrentUser;

        private string mysql_dominio, mysql_usuario, mysql_pass, mysql_puerto, mysql_bd;
        private string sql_usuario, sql_password, sql_servidor, sql_bd, rutaserver;
        private string micro_user, micro_pass, micro_server, micro_root, micro_bd, micro_oc;
        private string dir_archivos, serv_archivos;

        public RegistrosWindows()
        {
            mysql_dominio = mysql_usuario = mysql_pass = mysql_puerto = mysql_bd = "";
            sql_usuario = sql_password = sql_servidor = rutaserver = "";
            micro_user = micro_pass = micro_server = micro_root = micro_bd = micro_oc = "";
            dir_archivos = serv_archivos = "";
        }

        //Propiedades mysql
        public string MYSQL_DOMINIO
        {
            get { return mysql_dominio; }
            set { mysql_dominio = value; }
        }
        public string MYSQL_USUARIO
        {
            get { return mysql_usuario; }
            set { mysql_usuario = value; }
        }
        public string MYSQL_PASS
        {
            set { mysql_pass = value; }
            get { return mysql_pass; }
        }
        public string MYSQL_PUERTO
        {
            set { mysql_puerto = value; }
            get { return mysql_puerto; }
        }
        public string MYSQL_BD
        {
            set { mysql_bd = value; }
            get { return mysql_bd; }
        }

        //Propiedades SQLServer
        public string SQL_USUARIO
        {
            get { return sql_usuario; }
            set { sql_usuario = value; }
        }
        public string SQL_PASSWORD
        {
            set { sql_password = value; }
            get { return sql_password; }
        }
        public string SQL_SERVIDOR
        {
            set { sql_servidor = value; }
            get { return sql_servidor; }
        }
        public string RUTASERVER
        {
            set { rutaserver = value; }
            get { return rutaserver; }
        }
        public string SQL_BD
        {
            set { sql_bd = value; }
            get { return sql_bd; }
        }
        //propiedades Microsip
        public string MICRO_USER
        {
            get { return micro_user; }
            set { micro_user = value; }
        }
        public string MICRO_PASS
        {
            set { micro_pass = value; }
            get { return micro_pass; }
        }
        public string MICRO_SERVER
        {
            set { micro_server = value; }
            get { return micro_server; }
        }
        public string MICRO_ROOT
        {
            set { micro_root = value; }
            get { return micro_root; }
        }
        public string MICRO_BD
        {
            set { micro_bd = value; }
            get { return micro_bd; }
        }
        public string MICRO_OC
        {
            set { micro_oc = value; }
            get { return micro_oc; }
        }

        //Propiedades Archivos Adjuntos
        public string DIR_ARCHIVOS
        {
            set { dir_archivos = value; }
            get { return dir_archivos; }
        }
        public string SERV_ARCHIVOS
        {
            set { serv_archivos = value; }
            get { return serv_archivos; }
        }
        //PROPIEDADES CORREO
        public string CORREO_DESTIN_DEST { set; get; }
        public string CORREO_PUERTO_SMTP { set; get; }
        public string CORREO_SMTP_MAIL { set; get; }
        public string CORREO_DIRECCION { set; get; }
        public string CORREO_CONTRAS { set; get; }
        public string CORREO_DESTIN_DIRECCION { set; get; }
        public string CORREO_DESTIN_GRANELES { set; get; }

        //Transferir pallet entre almacenes
        public string EXITO { set; get; }
        public string ALMACEN_ORIGEN { set; get; }
        public string ALMACEN_DESTINO { set; get; }
        public string PALLET_ID { set; get; }
        public string PRODUCTOIDMSP { set; get; }
        public string CANTIDAD { set; get; }
        public string COSTO_STD { set; get; }
        public string MENSAJE_ERROR { set; get; }

        //PROPIEDADES DE TRANSFERENCIA A PRODUCCION
        public string LOTE { set; get; }
        public string SUPERSACO_ID { set; get; }
        public string OP_FOLIO { set; get; }
        public string FECHA_ASIGNACION { set; get; }
        public string DESCRIPCION { set; get; }
        public bool SO64bits()
        {
            bool bits;
            if (Environment.Is64BitOperatingSystem == true)
            {
                bits = true;
            }
            else
            {
                bits = false;
            }

            return bits;
        }
        public bool SO32bits()
        {
            bool bits;
            if (Environment.Is64BitOperatingSystem == false)
            {
                bits = true;
            }
            else
            {
                bits = false;
            }
            return bits;
        }

        public void LeerRegistros(string ruta_registros)
        {
            if (SO64bits() == true)
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            else
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            rk2 = rk1.OpenSubKey(ruta_registros, false);

            //REGISTROS MYSQL
            /*MYSQL_DOMINIO = (string)rk2.GetValue("MYSQL_DOMINIO");
            MYSQL_USUARIO = (string)rk2.GetValue("MYSQL_USUARIO");
            MYSQL_PASS = (string)rk2.GetValue("MYSQL_PASS");
            MYSQL_PUERTO = (string)rk2.GetValue("MYSQL_PUERTO");
            MYSQL_BD = (string)rk2.GetValue("MYSQL_BD");*/

            //REGISTROS MSSQL
            SQL_USUARIO = (string)rk2.GetValue("USU_SQL");
            SQL_PASSWORD = (string)rk2.GetValue("PASS_SQL");
            SQL_SERVIDOR = (string)rk2.GetValue("SERV_SQL");
            SQL_BD = (string)rk2.GetValue("BD_SQL");
            //RUTASERVER= (string)rk2.GetValue("SERVIDOR_RUTA");

            //REGSITROS MICROSIP
            MICRO_USER = (string)rk2.GetValue("USU_MSP");
            MICRO_PASS = (string)rk2.GetValue("PASS_MSP");
            MICRO_SERVER = (string)rk2.GetValue("SERV_MSP");
            MICRO_ROOT = (string)rk2.GetValue("RUTA_MSP");
            MICRO_BD = (string)rk2.GetValue("BD_MSP");
            //MICRO_OC = (string)rk2.GetValue("MICRO_OC");

            //REGISTROS ARCHIVOS ADJUNTOS
            DIR_ARCHIVOS = (string)rk2.GetValue("DIR_ARCH_ADJ");
            SERV_ARCHIVOS = (string)rk2.GetValue("SERV_ARCH_ADJ");

            //REGISTRO CORREO
            CORREO_DESTIN_DEST = (string)rk2.GetValue("CORREO_DESTIN_DEST");
            CORREO_PUERTO_SMTP = (string)rk2.GetValue("CORREO_PUERTO_SMTP");
            CORREO_SMTP_MAIL = (string)rk2.GetValue("CORREO_SMTP_MAIL");
            CORREO_DIRECCION = (string)rk2.GetValue("CORREO_DIRECCION");
            CORREO_CONTRAS = (string)rk2.GetValue("CORREO_CONTRAS");
            CORREO_DESTIN_DIRECCION = (string)rk2.GetValue("CORREO_DESTIN_DIRECCION");
            CORREO_DESTIN_GRANELES = (string)rk2.GetValue("CORREO_DESTIN_GRANELES");

            //Tranferencias de pallet
            EXITO = (string)rk2.GetValue("EXITO");
            ALMACEN_ORIGEN = (string)rk2.GetValue("ALMACEN_ORIGEN");
            ALMACEN_DESTINO = (string)rk2.GetValue("ALMACEN_DESTINO");
            PALLET_ID = (string)rk2.GetValue("PALLET_ID");
            PRODUCTOIDMSP = (string)rk2.GetValue("PRODUCTOIDMSP");
            CANTIDAD = (string)rk2.GetValue("CANTIDAD");
            COSTO_STD = (string)rk2.GetValue("COSTO_STD");
            MENSAJE_ERROR = (string)rk2.GetValue("MENSAJE_ERROR");

            //Transferencia Produccion
            //PROPIEDADES DE TRANSFERENCIA A PRODUCCION
            LOTE = (string)rk2.GetValue("LOTE");
            SUPERSACO_ID = (string)rk2.GetValue("SUPERSACO_ID");
            OP_FOLIO = (string)rk2.GetValue("OP_FOLIO");
            FECHA_ASIGNACION = (string)rk2.GetValue("FECHA_ASIGNACION");
            DESCRIPCION = (string)rk2.GetValue("DESCRIPCION");
        }
        public void EscribirRegistros(string nombre_registro, string valor_registro, bool mostrar_alerta, string ruta_registros)
        {
            try
            {
                if (SO64bits())
                {
                    rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                    rk2 = rk1.OpenSubKey(ruta_registros, true);
                    rk2.SetValue(nombre_registro, valor_registro);
                }
                else
                {
                    if (SO32bits())
                    {
                        rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                        rk2 = rk1.OpenSubKey(ruta_registros, true);
                        rk2.SetValue(nombre_registro, valor_registro);
                    }
                }
            }
            catch (Exception ex)
            {
                if (mostrar_alerta)
                {
                    MessageBox.Show("No fue posible escribir en los registros de Windows.\n\n" + ex.Message, "Mensaje de aplicación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void EscribirRegistro_Usuario_bd(string ruta_registro, string usuario_bd)
        {
            if (SO64bits() == true)
            {

                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("USU_SQL", usuario_bd);

            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("USU_SQL", usuario_bd);
            }
        }
        public void EscribirRegistro_Usuario_bd_M(string ruta_registro, string usuario_bd)
        {
            if (SO64bits() == true)
            {

                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("USU_MSP", usuario_bd);

            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("USU_MSP", usuario_bd);
            }
        }
        public void EscribirRegistro_Pass_bd(string ruta_registro, string pass_bd)
        {

            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("PASS_SQL", pass_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("PASS_SQL", pass_bd);
            }
        }
        public void EscribirRegistro_Pass_bd_M(string ruta_registro, string pass_bd)
        {

            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("PASS_MSP", pass_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("PASS_MSP", pass_bd);
            }
        }
        public void EscribirRegistro_Nombre_bd(string ruta_registro, string nombre_bd)
        {
            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("BD_SQL", nombre_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("BD_SQL", nombre_bd);

            }
        }
        public void EscribirRegistro_Nombre_bd_M(string ruta_registro, string nombre_bd)
        {
            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("BD_MSP", nombre_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("BD_MSP", nombre_bd);

            }
        }
        public void EscribirRegistro_Ruta_bd(string ruta_registro, string ruta_bd)
        {

            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("SERV_SQL", ruta_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("SERV_SQL", ruta_bd);
            }
        }
        public void EscribirRegistro_Servidor_bd_M(string ruta_registro, string ruta_bd)
        {

            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("SERV_MSP", ruta_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("SERV_MSP", ruta_bd);
            }
        }
        public void EscribirRegistro_Ruta_bd_M(string ruta_registro, string ruta_bd)
        {

            if (SO64bits() == true)
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("RUTA_MSP", ruta_bd);
            }
            else
            {
                rk1 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
                rk2 = rk1.OpenSubKey(ruta_registro, true);
                rk2.SetValue("RUTA_MSP", ruta_bd);
            }
        }
    }
}
