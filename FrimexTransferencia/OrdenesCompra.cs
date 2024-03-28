using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;

namespace FrimexTransferencia
{
    class OrdenesCompra
    {

        public OrdenesCompra()
        {
            ORDENCOMPRAID =  ESTATUS_ID  = PROVEEDOR_MSP_ID= 0;
            PROVEEDOR_DIRECCION = PROVEEDOR_NOMBRE=OCDESCRIPCION = "";
            ORDENCOMPRAIMPORTE = 0;
            FECHAOC = DateTime.Today;
        }

        public int ORDENCOMPRAID { set; get; }
        //public int BANCO_MSP_ID { set; get; }
        public int PROVEEDOR_MSP_ID { set; get; }
        public int ESTATUS_ID { set; get; }
        public string PROVEEDOR_NOMBRE { set; get; }
        public string PROVEEDOR_DIRECCION { set; get; }
        public string OCDESCRIPCION { set; get; }
        public double ORDENCOMPRAIMPORTE { set; get; }
        public DateTime FECHAOC { set; get; }

        public bool EncontrarOCMicrosip(string OCID, string Finicio, string Ffin)
        {
            Mensajes Mensaje = new Mensajes();
            ConexionMicrosip con_msp = new ConexionMicrosip();
            FbDataAdapter FDA;
            DataTable Tabla = new DataTable();
            string consulta = "";
            bool _encontrado = false;
            try
            {
                consulta = "select * from doctos_cm " +
                    " where " +
                    " tipo_docto = 'O' " +
                    " and estatus = 'P' " +
                    " " +
                    " and (fecha between '" + Finicio + "' and '" + Ffin + "')";
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FDA = new FbDataAdapter(consulta, con_msp.FBC);
                FDA.Fill(Tabla);
                FDA.Dispose();
                con_msp.Desconectar();
                if (Tabla.Rows.Count > 0)
                    _encontrado = true;
                else
                    _encontrado = false;
            }
            catch (Exception Ex)
            {
                Mensaje.Error(Ex.Message, "Error");
            }
            return _encontrado;
        }
        public DataTable CargarOrdenesCompraPendientesMicrosip(string IdProveedor,string Finicio, string Ffin,bool BuscarPorFechas,string LineasArticulos)
        {
            Mensajes Mensaje = new Mensajes();
            ConexionMicrosip con_msp = new ConexionMicrosip();
            FbDataAdapter FDA;
            DataTable Tabla=new DataTable();
            string consulta = "";
            try
            {
                string fechas = "";
                if (BuscarPorFechas == true)
                    fechas= " and (fecha between '" + Finicio + "' and '" + Ffin + "')";

                consulta = "select distinct DCM.docto_cm_id,DCM.folio,DCM.fecha, DCM.fecha_entrega,DCM.estatus, DCM.descripcion,DCM.importe_neto+DCM.fletes+DCM.otros_cargos+ " +
                   " DCM.total_impuestos + DCM.total_retenciones + DCM.gastos_aduanales + DCM.otros_gastos as importe from doctos_cm as DCM" +
                   " inner join DOCTOS_CM_DET as DCMD on DCMD.docto_cm_id=DCM.docto_cm_id "+
                   " inner join articulos as A on DCMD.Articulo_id = A.Articulo_id " +                   
                   " where " +
                   " DCM.tipo_docto = 'O' " +
                   " and DCM.estatus = 'P' " +
                   " and A.linea_articulo_id in ("+LineasArticulos +")"+
                   " and DCM.proveedor_id= " + IdProveedor +
                   " and fecha > '12/31/2017'" +
                   fechas +
                   " order by DCM.folio desc"; RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FDA = new FbDataAdapter(consulta, con_msp.FBC);
                FDA.Fill(Tabla);
                FDA.Dispose();
                con_msp.Desconectar();
            }
            catch (Exception Ex)
            {
                Mensaje.Error(Ex.Message, "Error");
                Tabla = new DataTable();
            }
            return Tabla;
        }
        public DataTable CargarDetalleOrdenCompraSeleccionada(string OrdenCompraId)
        {
            Mensajes Mensaje = new Mensajes();
            ConexionMicrosip con_msp = new ConexionMicrosip();
            FbDataAdapter FDA;
            DataTable Tabla = new DataTable();
            string consulta = "";
            try
            {

                consulta = "select DCMD.posicion,DCMD.Docto_cm_det_id,DCMD.precio_unitario,DCMD.Docto_cm_id,A.articulo_id,DCMD.Clave_articulo,A.nombre,DCMD.unidades, " +
                    " DCMD.unidades_rec_dev,DCMD.unidades_a_rec,DCMD.umed,DCMD.precio_total_neto as importe_neto from DOCTOS_CM_DET as DCMD " +
                    " inner join articulos as A on DCMD.Articulo_id = A.Articulo_id "+
                    " inner join Doctos_cm as DCM on DCM.docto_cm_id=DCMD.docto_cm_id " +
                    " where DCMD.docto_cm_id = "+OrdenCompraId;
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FDA = new FbDataAdapter(consulta, con_msp.FBC);
                FDA.Fill(Tabla);
                FDA.Dispose();
                con_msp.Desconectar();
            }
            catch (Exception Ex)
            {
                Mensaje.Error(Ex.Message, "Error");
                Tabla = new DataTable();
            }
            return Tabla;
        }
        public string InsertarOCSQL(UsuariosC Usuario, string _IdProveedor,string FOLIO,string FECHA,string ESTATUS,string DESCRIPCION, string IMPORTE
             , string PRODUCTO_ID, string DOCTO_CM_DET_ID, string DOCTO_CM_ID_DET, string ARTICULO_ID, string CLAVE_ARTICULO
            , string UNIDADES, string PRECIO, string UNIDADES_REC_DEV, string UNIDADES_A_REC, string UMED,string PRECIO_TOTAL_NETO,string NOMBRE)
        {
            ConexionSql cn = new ConexionSql();
            int i=0;
            string _OrdenCompraID = "";
            try
            {
                //Insertar Orden de compra
                cn.ConectarSQLServer();
                string _EstatusId = "4", PETICION_OC_ID = "";                
                string _fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                DateTime _FechaMsp = Convert.ToDateTime(FECHA);
                FECHA = _FechaMsp.ToString("dd-MM-yyyy");
                string consulta = "INSERT INTO [dbo].[PETICION_OC] " +
                    " ([PETICION_OC_ID] " +
                    " ,[PETICION_OC_FECHA] " +
                    " ,[PETICION_OC_USUARIO_CREACION] " +
                    " ,[PETICION_OC_FECHA_MODIF] " +
                    " ,[PETICION_OC_USUARIO_MODIF] " +
                    " ,[ESTATUS_ID] " +
                    " ,[PROVEEDOR_MSP_ID] " +
                    " ,[FOLIO] " +
                    " ,[FECHA] " +
                    " ,[ESTATUS] " +
                    " ,[DESCRIPCION] " +
                    " ,[IMPORTE]) " +
                    " VALUES " +
                    " ( " + cn.ObtenerSigID() +
                    " ,@Fecha1"  +
                    ", " + Usuario.USUARIOID.ToString() +
                    " ,@Fecha2"  +
                    ", " + Usuario.USUARIOID.ToString() +
                    " , " + _EstatusId +
                    " , " + _IdProveedor +
                    " ,'" + FOLIO +
                    "', @Fecha3"  +
                    ",'" + ESTATUS +
                    "','" + DESCRIPCION +
                    "', " + IMPORTE + ")";
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);

                cmd.Parameters.Add("@Fecha1", SqlDbType.Date).Value = _fecha;
                cmd.Parameters.Add("@Fecha2", SqlDbType.Date).Value = _fecha;
                cmd.Parameters.Add("@Fecha3", SqlDbType.Date).Value = FECHA;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();

                consulta = "SELECT [PETICION_OC_ID] " +
                    " FROM [dbo].[PETICION_OC] " +
                    " WHERE " +
                    " [PETICION_OC_FECHA]= @Fecha" +
                    " AND [PETICION_OC_USUARIO_CREACION]= " + Usuario.USUARIOID.ToString() +
                    "  AND [PETICION_OC_FECHA_MODIF]=@Fecha2"  +
                    " AND [PETICION_OC_USUARIO_CREACION]=" + Usuario.USUARIOID.ToString() +
                    "  AND [ESTATUS_ID]= " + _EstatusId +
                    "  AND [PROVEEDOR_MSP_ID]=" + _IdProveedor +
                    "  AND [FOLIO]='" + FOLIO +
                    "' AND [FECHA]= @Fecha3"  +
                    " AND [ESTATUS]='" + ESTATUS +
                    "' AND [DESCRIPCION]='" + DESCRIPCION +
                    "' AND [IMPORTE]=" + IMPORTE;

                cn = new ConexionSql();
                cn.ConectarSQLServer();
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = _fecha;
                cmd.Parameters.Add("@Fecha2", SqlDbType.Date).Value = _fecha;
                cmd.Parameters.Add("@Fecha3", SqlDbType.Date).Value = FECHA;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                    //_OC_ID = (Convert.ToInt32(sdr["PETICION_OC_ID"])).ToString().Trim();
                   _OrdenCompraID= PETICION_OC_ID = (Convert.ToInt32(sdr["PETICION_OC_ID"])).ToString().Trim();
                // _OC_ID = PETICION_OC_ID;
                cn.Desconectar();
                cmd.Dispose();
                //Insertar OC Detalle

                
                    consulta = "INSERT INTO [dbo].[PETICION_OC_DETALLE] " +
                        " ([PETICION_OC_DETALLE_ID] " +
                        " ,[PETICION_OC_ID] " +
                        " ,[PRODUCTO_ID] " +
                        " ,[DOCTO_CM_DET_ID] " +
                        " ,[DOCTO_CM_DET] " +
                        " ,[ARTICULO_ID] " +
                        " ,[CLAVE_ARTICULO] " +
                        " ,[UNIDADES] " +
                        " ,[PRECIO] " +
                        " ,[UNIDADES_REC_DEV] " +
                        " ,[UNIDADES_A_REC] " +
                        " ,[UMED] " +
                        " ,[PRECIO_TOTAL_NETO]) " +
                        " VALUES " +
                        " (" + cn.ObtenerSigID() +//cn.ObtenerSigID()+
                        " , " + PETICION_OC_ID +
                        " , " + PRODUCTO_ID +
                        " , " + DOCTO_CM_DET_ID +
                        " , " + DOCTO_CM_ID_DET +
                        " , " + ARTICULO_ID +
                        " ,'" + CLAVE_ARTICULO +
                        "', " + UNIDADES +
                        " , " + PRECIO +
                        " , " + UNIDADES_REC_DEV +
                        " , " + UNIDADES_A_REC +
                        " ,'" + UMED +
                        "', " + PRECIO_TOTAL_NETO + ")";
                    cn = new ConexionSql();
                    cn.ConectarSQLServer();
                    cmd = new SqlCommand(consulta, cn.SC);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.Desconectar();
                
            }
            catch (Exception Ex)
            {
                _OrdenCompraID = "";
                MessageBox.Show(Ex.Message + " El articulo \"" + NOMBRE + "\" no se encuentra registrado en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (cn.IsConected())
                    cn.Desconectar();
            }
            return _OrdenCompraID;
        }
        public bool ExisteOC(string _folio,out string PeticionOCId)
        {
            bool _existe = false;
            ConexionSql cn = new ConexionSql();
            PeticionOCId = "";
            try
            {
                string consulta = " select POC.*from PETICION_OC AS POC " +
                             "where Poc.FOLIO like '%" + _folio + "'";
                cn.ConectarSQLServer();               
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader= cmd.ExecuteReader();
                while (reader.Read())
                {
                    _existe = true;
                    PeticionOCId = Convert.ToString(reader["PETICION_OC_ID"]);
                }

                reader.Close();
                cmd.Dispose();
                cn.Desconectar();                
            }
            catch (Exception Ex)
            {
                _existe = false;
                if (cn.IsConected())
                    cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _existe;
        }
    }
}
