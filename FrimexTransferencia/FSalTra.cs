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
    public partial class FSalTra : Form
    {
        public FSalTra()
        {
            InitializeComponent();
        }
        bool correcto = false;
        string auxSSID = "";
        PuertosSerie _PuertoSerie = new PuertosSerie();
        public UsuariosC _Usuario = new UsuariosC();
        public int _ReqID = 0;
        private DataTable _DATOSREQ = new DataTable();
        Mensajes mensajes = new Mensajes();
        bool _basculaConectada = false, _activa = false;
        //string _almacenDestino = "";
        private void bEmbarcar_Click(object sender, EventArgs e)
        {
            #region METODO ANTERIOR (COMENTADO)
            /*ConexionSql cn = new ConexionSql();
            SqlTransaction transaction;
            int SS_Agregados = 0;
            cn.ConectarSQLServer();
            transaction = cn.SC.BeginTransaction();
            List<string[,]> __Productos = new List<string[,]>();
            List<string> _supersacos = new List<string>();
            string msg_local = "";
            try
            {
                //Buscar Transferencia en la base de datos con estatus "P" para armar el correo
                if (dGVSalida.Rows.Count > 0)
                {
                    DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                    string _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString()
                        , _FolioTransferencia = tBFolioSalida.Text.Trim()
                        , _AlmacenNOmbre = _dRVAlmacen.Row.ItemArray[1].ToString()
                        , _AlmacenDestino = Convert.ToString(_DATOSREQ.Rows[0]["ALMACEN_DESTINO_ID"]);
                    string consulta = "INSERT INTO [dbo].[TRANSFERENCIA] " +
                   " ([TRANSFERENCIA_ID] " +
                   " ,[TRANSFERENCIA_FECHA] " +
                   " ,[TRANSFERENCIA_ESTATUS] " +
                   " ,[ALMACEN_ORIGEN_ID] " +
                   " ,[ALMACEN_DESTINO_ID]) " +
                    " VALUES " +
                   " ( " + _FolioTransferencia +
                   " ,'" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                   " ,'A'" +
                   " , " + _AlmacenID +
                   " ," + _AlmacenDestino + ")";// Consulta para insertar el encabezado de la transferencia

                    SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                    string auxSSTrnaferidos = "", auxProdDesc = "",mensaje="",auxLote="";
                    if (cmdm.ExecuteNonQuery() > 0)
                    {
                        double _pesoTotal = 0.0;
                        for (int i = 0; dGVSalida.Rows.Count > i; i++)
                        {
                            string _supersaco = Convert.ToString(dGVSalida["SUPERSACOID", i].Value);
                            auxProdDesc = Convert.ToString(dGVSalida["PRODUCTO", i].Value) + " ";
                            auxLote = Convert.ToString(dGVSalida["LOTE", i].Value);
                            _supersacos.Add(_supersaco);
                            if (!ExisteSupersacoEnTransferencia(_supersaco, _FolioTransferencia, cn, transaction))
                            {
                                consulta = "INSERT INTO [dbo].[TRANSFERENCIA_DETALLE] " +
                                   " ([TRANSFERENCIA_ID]" +
                                   ",[SUPERSACO_ID]) " +
                                    " VALUES " +
                                   " (" + _FolioTransferencia +
                                   ", " + _supersaco + ")";// Consulta para insertar el detalle de la transferencia
                                _pesoTotal += Convert.ToDouble(dGVSalida["CANTIDAD", i].Value);
                                auxSSTrnaferidos += "<tr><td class=\"td-title\">" + auxLote + "</td>" +
                                    "<td> " + _supersaco + "</td>" +
                                    "<td> " + auxProdDesc + "</td>" +
                                    "<td> " + Convert.ToDouble(dGVSalida["CANTIDAD", i].Value).ToString("N2") + "Kg" + "</td></tr>\n\r";
                                cmdm = new SqlCommand(consulta, cn.SC, transaction);
                                if (cmdm.ExecuteNonQuery() > 0)
                                {
                                    SS_Agregados++;
                                }
                            }
                        }
                        auxSSTrnaferidos += "<tr><td class=\"td - title\"> </td>" +
                            "<td> </td>" +
                            "<td> Peso Total</td>" +
                            "<td> " + _pesoTotal.ToString("N2") + "Kg" + "</td></tr>\n\r";
                        consulta = "INSERT INTO [dbo].[REQ_TRA] " +
                            " ([REQUISICION_ID] " +
                            " ,[TRANSFERENCIA_ID] " +
                            " ,[REQ_TRA_FECHA] " +
                            " ,[REQ_TRA_USUARIO_ID]) " +
                            " VALUES " +
                            " (" + _ReqID +
                            " , " + _FolioTransferencia +
                            " , '" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                            " ," + _Usuario.USUARIOID + ")";
                        cmdm = new SqlCommand(consulta, cn.SC, transaction);
                        cmdm.ExecuteNonQuery();
                        //Imprimir en Etiqueta 4X4
                        C_FuncionesMSP c_FuncionesMSP = new C_FuncionesMSP();
                        if (c_FuncionesMSP.TransferenciaEntreAlmacenesMSP(Convert.ToInt32(_AlmacenID), Convert.ToInt32(_AlmacenDestino)
                             , Convert.ToInt32(_FolioTransferencia), out msg_local))
                        {
                            if (_AlmacenDestino == "1221")
                                _AlmacenDestino = "ALMACEN GENERAL";
                            else if (_AlmacenDestino == "1222")
                                _AlmacenDestino = "BODEGA RENTADA";
                            else if (_AlmacenDestino == "1223")
                                _AlmacenDestino = "TANQUES";

                            FImpTra fImpTra = new FImpTra();
                            fImpTra.Usuario = _Usuario;
                            fImpTra._Productos = SupersacosEnGrid();
                            fImpTra._BodegaNombre = _AlmacenNOmbre;
                            fImpTra._DestinoNombre = _AlmacenDestino;
                            fImpTra._NoTransferencia = _FolioTransferencia;
                            fImpTra.ShowDialog();
                            transaction.Commit();
                            //Enviar Correo con los supersacos enviados
                            mensaje = "Se ha realizado la transferencia " + _FolioTransferencia + " del almacén "
                                + _AlmacenNOmbre + " al almacén " + _AlmacenDestino + " de los siguientes supersacos:";

                            string QueryHTML = "<!DOCTYPE html>";
                            QueryHTML += "<html>";
                            QueryHTML += "    <head>";
                            QueryHTML += "        <title>Transferencia</title>";
                            QueryHTML += "        <meta charset=\"UTF - 8\">";
                            QueryHTML += "        <meta name=\"viewport\" content=\"width = device-width, initial - scale = 1.0\">";
                            QueryHTML += "        <style>";
                            QueryHTML += "            body";
                            QueryHTML += "            {";
                            QueryHTML += "                background-color: #dee3e5;";
                            QueryHTML += "            }";
                            QueryHTML += "            .table-body";
                            QueryHTML += "            {";
                            QueryHTML += "                width: 50%;";
                            QueryHTML += "            }";
                            QueryHTML += "            .table-body td";
                            QueryHTML += "            {";
                            QueryHTML += "                padding: 5px;";
                            QueryHTML += "                width: 50%;";
                            QueryHTML += "            }";
                            QueryHTML += "            .table-body .td-title";
                            QueryHTML += "            {";
                            QueryHTML += "                background-color: #004C88;";
                            QueryHTML += "                color: #FFF;";
                            QueryHTML += "                font-weight: bold;";
                            QueryHTML += "                text-align: right;";
                            QueryHTML += "                border-radius: 20px 0 0 20px;";
                            QueryHTML += "            }";
                            QueryHTML += "            .table-body .td-title2";
                            QueryHTML += "            {";
                            QueryHTML += "                background-color: #004C88;";
                            QueryHTML += "                color: #FFF;";
                            QueryHTML += "                font-weight: bold;";
                            QueryHTML += "                text-align: center;";
                            QueryHTML += "                border-radius: 20px 0 0 20px;";
                            QueryHTML += "            }";
                            QueryHTML += "              p";
                            QueryHTML += "            {";
                            QueryHTML += "                color: #FFFFFF;";
                            QueryHTML += "                text - align: center;";
                            QueryHTML += "                font - family: arial black;";
                            QueryHTML += "            }";
                            QueryHTML += "        </style>";
                            QueryHTML += "    </head>";
                            QueryHTML += "    <body>";
                            QueryHTML += "        <table border=\"0\" width=\"80%\" align=\"center\">";
                            QueryHTML += "            <tr>";
                            QueryHTML += "                <td align=\"center\" height=\"50px\">Transferencia</td>";
                            QueryHTML += "            </tr>";
                            QueryHTML += "            <tr>";
                            QueryHTML += "                <td class=\"td-title\" >";
                            QueryHTML += "                  <p>" + mensaje + "</p>";
                            QueryHTML += "                </td>";
                            QueryHTML += "            </tr>";
                            QueryHTML += "            <tr>";
                            QueryHTML += "                <td align=\"center\" bgcolor=\"#FFFFFF\" style=\"padding: 30px 0 50px 0\">";
                            QueryHTML += "                    <table class=\"table-body\">";
                            QueryHTML += "                <th class=\"td-title2\"> LOTE </th>";
                            QueryHTML += "                <th class=\"td-title2\"> SUPERSACO ID </th>";//Encabezados de tabla
                            QueryHTML += "                <th class=\"td-title2\"> PRODUCTO </th>";
                            QueryHTML += "                <th class=\"td-title2\"> CANTIDAD </th>";
                            QueryHTML += "                        </tr>";
                            QueryHTML += "                            <td align=\"center\" colspan=\"2\">";
                            QueryHTML += "                            </td>";
                            QueryHTML += auxSSTrnaferidos;
                            QueryHTML += "                    </table>";
                            QueryHTML += "                </td>";
                            QueryHTML += "            </tr>";
                            QueryHTML += "        </table>";
                            QueryHTML += "    </body>";
                            QueryHTML += "</html>";


                            RegistrosWindows reg = new RegistrosWindows();
                            reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");

                            CorreoC correo = new CorreoC();
                            correo.ASUNTO = "Transferencia entre almacenes";
                            foreach (string destCorreos in reg.CORREO_DESTIN_DEST.Split(';'))
                                correo.DESTINATARIOS.Add(destCorreos);
                            // correo.DESTINATARIOS.Add("mherrera@soti.com.mx");
                            correo.MENSAJE = QueryHTML;
                            correo.PUERTOSMTP = Convert.ToInt32(reg.CORREO_PUERTO_SMTP);
                            correo.SMTPMAIL = reg.CORREO_SMTP_MAIL;
                            correo.CORREODIRECCION = reg.CORREO_DIRECCION;
                            correo.CORREOCONTRASEÑA = reg.CORREO_CONTRAS;
                            correo.REMITENTE = reg.CORREO_DIRECCION;
                            correo.EnviarCorreo(out msg_local);
                            CRequisiciones requisiciones = new CRequisiciones();
                            requisiciones.ActualizarFolioTrasferencia(_supersacos, Convert.ToInt32(_FolioTransferencia), out msg_local);
                        }
                        else
                            transaction.Rollback();
                        cmdm.Dispose();
                        cn.Desconectar();
                        MessageBox.Show("Se agregaron " + SS_Agregados + " a la salida por transferencia " + _FolioTransferencia);
                        DesconectarBascula();
                        this.Close();
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("No se pudo realizar la transferencia en Microsip \n\r" + msg_local);
                    }
                }
                else
                    MessageBox.Show("No hay supersacos para realizar la salida", "Error");                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
                transaction.Rollback();
                cn.Desconectar();
            }*/
            #endregion

            string msg_local = "" , _FolioTransferencia = "", auxSSTrnaferidos="", _AlmacenDestino="", _AlmacenNombre="",
                mensaje="", _AlmacenID="";
            List<string> _supersacos = new List<string>();
            try
            {
                //Buscar Transferencia en la base de datos con estatus "P" para armar el correo independientemente de la cantidad de ss
                CTransferencias transferencias = new CTransferencias();
                _FolioTransferencia = tBFolioSalida.Text.Trim();
                ConexionSql cn = new ConexionSql();
                SqlTransaction transaction;
                if (_FolioTransferencia.Length > 0)
                {
                    cn.ConectarSQLServer();
                    transaction = cn.SC.BeginTransaction();
                    DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                    _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString();
                    _FolioTransferencia = tBFolioSalida.Text.Trim();
                    _AlmacenNombre = _dRVAlmacen.Row.ItemArray[1].ToString();
                    _AlmacenDestino = Convert.ToString(_DATOSREQ.Rows[0]["ALMACEN_DESTINO_ID"]);

                    DataTable _DatosTransferencia = transferencias.CargarTransferencia(_FolioTransferencia, "P");
                    if (_DatosTransferencia.Rows.Count > 0)
                    {
                        int i = 0;
                        double _pesoTotal = 0.0;
                        foreach (DataRow fila in _DatosTransferencia.Rows)
                        {
                            string _supersaco = Convert.ToString(fila["SUPERSACO_ID"]),
                            auxProdDesc = Convert.ToString(fila["PRODUCTO_DESCRIPCION"]) + " ",
                            auxLote = Convert.ToString(fila["FOLIO"]);
                            _pesoTotal += Convert.ToDouble(fila["SUPERSACO_CANTIDAD"]);
                            _supersacos.Add(_supersaco);
                            auxSSTrnaferidos += "<tr><td class=\"td-title\">" + auxLote + "</td>" +
                          "<td> " + _supersaco + "</td>" +
                          "<td> " + auxProdDesc + "</td>" +
                          "<td> " + Convert.ToDouble(fila["SUPERSACO_CANTIDAD"]).ToString("N2") + "Kg" + "</td></tr>\n\r";
                        }
                        auxSSTrnaferidos += "<tr><td class=\"td - title\"> </td>" +
                                            "<td> </td>" +
                                            "<td> Peso Total</td>" +
                                            "<td> " + _pesoTotal.ToString("N2") + "Kg" + "</td></tr>\n\r";
                        //Imprimir en Etiqueta 4X4 
                        //Revisar almacen origen y almacen destino de MSP
                        //C_FuncionesMSP c_FuncionesMSP = new C_FuncionesMSP();
                        //if (c_FuncionesMSP.TransferenciaEntreAlmacenesMSP(transferencias.AlmacenMSPDesdeSQL(_AlmacenID), transferencias.AlmacenMSPDesdeSQL(_AlmacenDestino)
                        //     , Convert.ToInt32(_FolioTransferencia), out msg_local))
                        //{
                            if (transferencias.ActualizarEstatusTransferencia(_FolioTransferencia, "A", cn, transaction))
                            {
                                if (_AlmacenDestino == "1221")
                                    _AlmacenDestino = "ALMACEN GENERAL";
                                else if (_AlmacenDestino == "1222")
                                    _AlmacenDestino = "BODEGA RENTADA";
                                else if (_AlmacenDestino == "1223")
                                    _AlmacenDestino = "TANQUES";

                                FImpTra fImpTra = new FImpTra();
                                fImpTra.Usuario = _Usuario;
                                fImpTra._Productos = SupersacosEnGrid();
                                fImpTra._BodegaNombre = _AlmacenNombre;
                                fImpTra._DestinoNombre = _AlmacenDestino;
                                fImpTra._NoTransferencia = _FolioTransferencia;
                                fImpTra.ShowDialog();

                                //Enviar Correo con los supersacos enviados
                                mensaje = "Se ha realizado la transferencia " + _FolioTransferencia + " del almacén "
                                    + _AlmacenNombre + " al almacén " + _AlmacenDestino + " de los siguientes supersacos:";

                                string QueryHTML = "<!DOCTYPE html>";
                                QueryHTML += "<html>";
                                QueryHTML += "    <head>";
                                QueryHTML += "        <title>Transferencia</title>";
                                QueryHTML += "        <meta charset=\"UTF - 8\">";
                                QueryHTML += "        <meta name=\"viewport\" content=\"width = device-width, initial - scale = 1.0\">";
                                QueryHTML += "        <style>";
                                QueryHTML += "            body";
                                QueryHTML += "            {";
                                QueryHTML += "                background-color: #dee3e5;";
                                QueryHTML += "            }";
                                QueryHTML += "            .table-body";
                                QueryHTML += "            {";
                                QueryHTML += "                width: 50%;";
                                QueryHTML += "            }";
                                QueryHTML += "            .table-body td";
                                QueryHTML += "            {";
                                QueryHTML += "                padding: 5px;";
                                QueryHTML += "                width: 50%;";
                                QueryHTML += "            }";
                                QueryHTML += "            .table-body .td-title";
                                QueryHTML += "            {";
                                QueryHTML += "                background-color: #004C88;";
                                QueryHTML += "                color: #FFF;";
                                QueryHTML += "                font-weight: bold;";
                                QueryHTML += "                text-align: right;";
                                QueryHTML += "                border-radius: 20px 0 0 20px;";
                                QueryHTML += "            }";
                                QueryHTML += "            .table-body .td-title2";
                                QueryHTML += "            {";
                                QueryHTML += "                background-color: #004C88;";
                                QueryHTML += "                color: #FFF;";
                                QueryHTML += "                font-weight: bold;";
                                QueryHTML += "                text-align: center;";
                                QueryHTML += "                border-radius: 20px 0 0 20px;";
                                QueryHTML += "            }";
                                QueryHTML += "              p";
                                QueryHTML += "            {";
                                QueryHTML += "                color: #FFFFFF;";
                                QueryHTML += "                text - align: center;";
                                QueryHTML += "                font - family: arial black;";
                                QueryHTML += "            }";
                                QueryHTML += "        </style>";
                                QueryHTML += "    </head>";
                                QueryHTML += "    <body>";
                                QueryHTML += "        <table border=\"0\" width=\"80%\" align=\"center\">";
                                QueryHTML += "            <tr>";
                                QueryHTML += "                <td align=\"center\" height=\"50px\">Transferencia</td>";
                                QueryHTML += "            </tr>";
                                QueryHTML += "            <tr>";
                                QueryHTML += "                <td class=\"td-title2\" >";
                                QueryHTML += "                  <p>" + mensaje + "</p>";
                                QueryHTML += "                </td>";
                                QueryHTML += "            </tr>";
                                QueryHTML += "            <tr>";
                                QueryHTML += "                <td align=\"center\" bgcolor=\"#FFFFFF\" style=\"padding: 30px 0 50px 0\">";
                                QueryHTML += "                    <table class=\"table-body\">";
                                QueryHTML += "                <th class=\"td-title2\"> LOTE </th>";
                                QueryHTML += "                <th class=\"td-title2\"> SUPERSACO ID </th>";//Encabezados de tabla
                                QueryHTML += "                <th class=\"td-title2\"> PRODUCTO </th>";
                                QueryHTML += "                <th class=\"td-title2\"> CANTIDAD </th>";
                                QueryHTML += "                        </tr>";
                                QueryHTML += "                            <td align=\"center\" colspan=\"2\">";
                                QueryHTML += "                            </td>";
                                QueryHTML += auxSSTrnaferidos;
                                QueryHTML += "                    </table>";
                                QueryHTML += "                </td>";
                                QueryHTML += "            </tr>";
                                QueryHTML += "        </table>";
                                QueryHTML += "    </body>";
                                QueryHTML += "</html>";


                                RegistrosWindows reg = new RegistrosWindows();
                                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");

                                CorreoC correo = new CorreoC();
                                correo.ASUNTO = "Transferencia entre almacenes";
                                foreach (string destCorreos in reg.CORREO_DESTIN_DEST.Split(';'))
                                    correo.DESTINATARIOS.Add(destCorreos);
                                // correo.DESTINATARIOS.Add("ksalazar@alimentoscazerola.com.mx");
                                correo.MENSAJE = QueryHTML;
                                correo.PUERTOSMTP = Convert.ToInt32(reg.CORREO_PUERTO_SMTP);
                                correo.SMTPMAIL = reg.CORREO_SMTP_MAIL;
                                correo.CORREODIRECCION = reg.CORREO_DIRECCION;
                                correo.CORREOCONTRASEÑA = reg.CORREO_CONTRAS;
                                correo.REMITENTE = reg.CORREO_DIRECCION;
                                correo.EnviarCorreo(out msg_local);
                                transaction.Commit();
                                cn.Desconectar();
                                CRequisiciones requisiciones = new CRequisiciones();
                                requisiciones.ActualizarFolioTrasferencia(_supersacos, Convert.ToInt32(_FolioTransferencia), out msg_local);
                                this.Close();
                            }
                            else
                            {
                                mensajes.Error("No se pudo actualizar el estatus de la transferencia", "Error");
                                transaction.Rollback();
                                if (cn.IsConected())
                                    cn.Desconectar();
                            }
                    //}
                    //else
                    //    mensajes.Error("Transferencia no capturada en MSP\n\r" + msg_local, "Error");
                }
                    else
                    {
                        transaction.Rollback();
                        if(cn.IsConected())
                            cn.Desconectar();
                        mensajes.Informacion("Sin datos encontrados", "Información");
                    }
                }
                else
                {
                    mensajes.Error("Transferencia no capturada", "Error");
                }
            }
            catch(Exception Ex)
            {
                mensajes.Error(Ex.Message,"Error");
            }
    
        }
        private List<string[,]> SupersacosEnGrid()
        {
            List<string[,]> _SS = new List<string[,]>();
            int cont = 0;
            DataGridView _auxTablaDatos = dGVSalida;
            //DataTable _auxTablaDatos =(DataTable) dGVSalida.DataSource;
            for (int i =0; _auxTablaDatos.Rows.Count!=0; i++)
            {
                if (_SS.Count==0)
                {
                    string[,] aux =new string[1, 2];
                    string _producto = Convert.ToString(_auxTablaDatos["PRODUCTO",i].Value);
                    for(int j=0; _auxTablaDatos.Rows.Count !=0; j++)
                    {
                        if (_producto == Convert.ToString(_auxTablaDatos["PRODUCTO", 0].Value))
                        {
                            cont++;
                            _auxTablaDatos.Rows.RemoveAt(0);
                        }
                    }
                    aux[0, 0] = cont.ToString();
                    aux[0, 1] = _producto;
                    _SS.Add(aux);                    
                }else
                {
                    string[,] aux = new string[1, 2];
                    string _producto = Convert.ToString(_auxTablaDatos["PRODUCTO", i].Value);
                    for (int j = 0; _auxTablaDatos.Rows.Count !=0; j++)
                    {
                        if (_producto == Convert.ToString(_auxTablaDatos["PRODUCTO", 0].Value))
                        {
                            cont++;
                            _auxTablaDatos.Rows.RemoveAt(0);
                        }
                    }
                    aux[0, 0] = cont.ToString();
                    aux[0, 1] = _producto;
                    _SS.Add(aux);
                }
            }
            return _SS;
        }
        private void Limpiar()
        {
            dGVSalida.Rows.Clear();
            tBSupersacoID.Text = "";
            CargarSiguienteFolio();
        }
        private bool ExisteSupersacoEnTransferencia(string SUPERSACO_ID,string TRANSFERENCIA_ID,ConexionSql cn,SqlTransaction transaction)
        {
            bool _existe = false;
            try
            {
                string consulta = "select * from transferencia_detalle " +
                    " where " +
                    " transferencia_id = " +TRANSFERENCIA_ID +
                    " and SUPERSACO_id=" + SUPERSACO_ID;

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC,transaction);
                SqlDataReader reader = cmdm.ExecuteReader();
                if (reader.HasRows)
                    _existe = true;
                else
                    _existe = false;
                reader.Close();
                cmdm.Dispose();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
            return _existe;
        }
        private void FSalTra_Load(object sender, EventArgs e)
        {
            string msg_local = "";
            if (_ReqID > 0)
            {
                CRequisiciones requisiciones = new CRequisiciones();
                _DATOSREQ = requisiciones.CargarDatosRequisicion(this._ReqID, out msg_local);
                CargarSiguienteFolio();
            }
            else
            {
                tBFolioSalida.Enabled = true;
                tBSupersacoID.Enabled = false;
                button1.Enabled = false;
                mSOpciones.Enabled = false;
                cBAlmacen.Enabled = false;
            }
                CargarAlmacenes();
        }
        private void CargarSiguienteFolio()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string cadena = "select COUNT(TRANSFERENCIA_ID)+1 AS SIGUIENTE_FOLIO from transferencia", _folio="";
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
               SqlDataReader reader= cmdm.ExecuteReader();
                while (reader.Read())
                    _folio = Convert.ToString(reader["SIGUIENTE_FOLIO"]);
                cmdm.Dispose();
                cn.Desconectar();
                tBFolioSalida.Text = _folio;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        private void CargarAlmacenes()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                cBAlmacen.DataSource = null;
                string cadena = "select infr.INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " inner join USU_ALM as ua on infr.INVENTARIO_FRIMEX_ID = ua.ALMACEN_ID " +
                    " inner join INVENTARIO_SUPERSACO as iss on iss.INVENTARIO_FRIMEX_ID=infr.INVENTARIO_FRIMEX_ID" +
                    " where USU_ALM_ESTATUS = 'A' and USUARIO_ID = " + _Usuario.USUARIOID;
                DataTable Table = new DataTable();
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
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
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Agregar();
        }
        private bool ValidarPesoSupersaco(string SUPERSACOID,double PESO_NUEVO,out string msg)
        {
            bool _exito = false;
            string  msg_local="",consulta="";
            ConexionSql cn = new ConexionSql();
            try
            {
                int _almacenID = 0;
                double _pesoSS = 0.0;
                consulta = "select SUPERSACO_ID, PRODUCTO_ID, SUPERSACO_CANTIDAD,INVENTARIO_SUPERSACO_ID " +
                    " from SUPERSACO where " +
                    " SUPERSACO_ID = " + SUPERSACOID;
                cn.ConectarSQLServer();
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                {
                    _pesoSS = Convert.ToDouble(reader["SUPERSACO_CANTIDAD"]);
                    _almacenID = Convert.ToInt32(reader["INVENTARIO_SUPERSACO_ID"]);
                }
                if (_pesoSS - 10 <= PESO_NUEVO || _pesoSS + 10 >= PESO_NUEVO)
                {
                    _exito = true;
                    CRequisiciones requisiciones = new CRequisiciones();
                    requisiciones.ActualizarPesoSS(Convert.ToInt32(SUPERSACOID),PESO_NUEVO,_almacenID,0,out msg_local);
                }
                else
                    _exito = false;
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

            msg = msg_local;
            return _exito;
        }
        private bool ValidarCantidadSupersacosEnTransferenciasPrevias(int REQUISICIONID,string PRODUCTO_NOMBRE,int CANT_SS ,out string msg)
        {
            bool _exito = false;
            string consulta = "", msg_local = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                consulta = "select RD.PRODUCTO_ID,RD.REQUISICION_DETALLE_CANTIDAD,TD.TRANSFERENCIA_ID,Count(p.PRODUCTO_ID) as CANT_SACOS,p.PRODUCTO_DESCRIPCION " +
                    " from REQ_TRA as RT" +
                    " inner join REQUISICION as R on RT.REQUISICION_ID = R.REQUISICION_ID" +
                    " inner join REQUISICION_DETALLE as RD on RD.REQUISICION_ID=R.REQUISICION_ID" +
                    " inner join TRANSFERENCIA as T on RT.TRANSFERENCIA_ID = T.TRANSFERENCIA_ID" +
                    " inner join TRANSFERENCIA_DETALLE as TD on RT.TRANSFERENCIA_ID = TD.TRANSFERENCIA_ID" +
                    " inner join SUPERSACO as S on Td.SUPERSACO_ID = S.SUPERSACO_ID" +
                    " inner join PRODUCTO as P on S.PRODUCTO_ID = P.PRODUCTO_ID" +
                    " where RT.REQUISICION_ID=" + REQUISICIONID + " AND R.REQUISICION_ESTATUS='A'" +
                    " group by RD.PRODUCTO_ID,RD.REQUISICION_DETALLE_CANTIDAD,TD.TRANSFERENCIA_ID,p.PRODUCTO_DESCRIPCION";
                cn.ConectarSQLServer();
                //SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                DataTable _DATOS = new DataTable();
                SqlDataAdapter _DA=new SqlDataAdapter(consulta, cn.SC);
                _DA.Fill(_DATOS);
                //cmdm.Dispose();
                _DA.Dispose();
                cn.Desconectar();
               // MessageBox.Show("Consulta: "+consulta+ _DATOS.Rows.Count);
                if (_DATOS.Rows.Count == 0)
                {
                    cn.ConectarSQLServer();
                    consulta = "select RD.PRODUCTO_ID,RD.REQUISICION_DETALLE_CANTIDAD,p.PRODUCTO_DESCRIPCION " +
                        " from REQUISICION as R " +
                        " inner join REQUISICION_DETALLE as RD on RD.REQUISICION_ID = R.REQUISICION_ID " +
                        " inner join PRODUCTO as p on rd.PRODUCTO_ID = p.PRODUCTO_ID or rd.PRODUCTO_ID=P.PRODUCTO_ID" +
                        " where R.REQUISICION_ID =  " + REQUISICIONID  +" AND R.REQUISICION_ESTATUS = 'A' " +
                        " group by RD.PRODUCTO_ID,RD.REQUISICION_DETALLE_CANTIDAD,p.PRODUCTO_DESCRIPCION";
                    SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                    SqlDataReader reader = cmdm.ExecuteReader();
                    //MessageBox.Show("Consulta: " + consulta );
                    //if (reader.HasRows)
                    //{
                    //    MessageBox.Show("tiene filas");
                        while (reader.Read())
                        {
                            int _SSSolicitados = Convert.ToInt32(Convert.ToString(reader["REQUISICION_DETALLE_CANTIDAD"]));
                            if (_SSSolicitados >= CANT_SS+1)
                                _exito = true;
                            else
                            {
                                _exito = false;
                                msg_local = "Meta de supersacos solicitados alcanzada";
                            }
                        }
                    //}else
                    //    MessageBox.Show("no tiene filas");
                }
                else if (_DATOS.Rows.Count >0)
                {
                    foreach(DataRow _datos in _DATOS.Rows)
                    {
                        string _AuxPRODNOM = Convert.ToString(_datos["PRODUCTO_DESCRIPCION"]); 
                        if(_AuxPRODNOM == PRODUCTO_NOMBRE )
                        {
                            int _CantSacos= Convert.ToInt32(_datos["CANT_SACOS"])+1,
                                _SacosSol = Convert.ToInt32(_datos["REQUISICION_DETALLE_CANTIDAD"]);
                            if ( _CantSacos > _SacosSol)                            
                                _exito = false;                            
                            else
                                _exito = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

            msg = msg_local;
            return _exito;
        }
        private void Agregar()
        {
            button1.Enabled = false;
            ConexionSql cn = new ConexionSql();
            try
            {
                string aux = "",_datoRecib="",_auxSSID="";
                cn.ConectarSQLServer();
                string consulta = "",_Lote="", _AlmacenOrigenID = "", _SuperSacoID = "", _Producto_desc="",_Producto_id = "", msg_local = "";
                _auxSSID= tBSupersacoID.Text.Trim();
                for (int i = 0; i < _auxSSID.Length; i++)
                {
                    if (correcto == false)
                    {
                        if (!char.IsDigit(_auxSSID[i]) )
                        {
                            correcto = true;
                            break;
                        }
                        else
                            auxSSID += _auxSSID[i];
                    }
                }
                _SuperSacoID = auxSSID;
                tBSupersacoID.Text = auxSSID;
                CRequisiciones requisiciones = new CRequisiciones();
                if (_SuperSacoID.Length > 0)
                {
                    //Validar que el folio no este en el grid
                    DataTable _DatosSS = requisiciones.CargarDatosSupersaco(_SuperSacoID, out msg_local);

                   // _Producto_id = Convert.ToString(_DatosSS.Rows[0]["PRODUCTO_MSP_ID"]);
                    _Producto_desc = Convert.ToString(_DatosSS.Rows[0]["PRODUCTO_DESCRIPCION"]);
                    //MessageBox.Show("Datos ss: "+_Producto_id+" "+_Producto_desc);
                    if (BuscarProducto(_Producto_desc))
                    {
                        //buscar si hay otras transferencias y validar supersacos restantes
                        DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                        _AlmacenOrigenID = _dRVAlmacen.Row.ItemArray[0].ToString();
                        string _AlmacenDestinoID = Convert.ToString(_DATOSREQ.Rows[0]["ALMACEN_DESTINO_ID"]);
                        int SS_EN_GRID = CantidadSupersacoProd(_Producto_desc);
                        //string bodega = Convert.ToString(_DATOSREQ.Rows[0]["INVENTARIO_FRIMEX_ID"]);
                        if (ValidaAlmacenSS(_SuperSacoID, _AlmacenOrigenID))
                        {
                            if (ValidarCantidadSupersacosEnTransferenciasPrevias(_ReqID,_Producto_desc, SS_EN_GRID, out msg_local))
                            {
                                if (_basculaConectada == true)
                                {
                                    double _PesoSS = 0.0;
                                    _datoRecib = _PuertoSerie.DatoRecibido();
                                        aux= ValidarDigitos(_datoRecib);
                                    //mensajes.Informacion("dato recibido "+_datoRecib,"informacion");
                                    //aux= ValidarDigitos(_PuertoSerie.DatoRecibido());
                                    decimal valor;
                                    if (Decimal.TryParse(aux, out valor))
                                    {
                                        _PesoSS = Convert.ToDouble(valor);
                                        if (_PesoSS > 0)
                                        {
                                            if (!BuscarEnGrid(_SuperSacoID))
                                                {
                                                if (ValidarPesoSupersaco(_SuperSacoID, _PesoSS, out msg_local))
                                                {

                                                    //Comparar peso guardado con peso actual

                                                    consulta = "Select ifx.ALMACEN_MSP_DESCRIPCION,iss.INVENTARIO_SUPERSACO_ID,s.SUPERSACO_ID" +
                                                        " ,p.PRODUCTO_DESCRIPCION,SUPERSACO_CANTIDAD,poc.FOLIO,S.LOTE_ID " +
                                                        " from SUPERSACO as s " +
                                                        " left join INVENTARIO_SUPERSACO as iss on s.INVENTARIO_SUPERSACO_ID = iss.INVENTARIO_SUPERSACO_ID " +
                                                        " left join INVENTARIO_FRIMEX as ifx on iss.INVENTARIO_FRIMEX_ID = ifx.INVENTARIO_FRIMEX_ID " +
                                                        " left join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                                                        " left join EMBARQUE AS E ON S.EMBARQUE_ID = E.EMBARQUE_ID" +
                                                        " left join PETICION_OC AS POC ON e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID" +                                                        
                                                        " where iss.INVENTARIO_FRIMEX_ID = " + _AlmacenOrigenID + " and s.SUPERSACO_ESTATUS='A' and s.SUPERSACO_ID = " + _SuperSacoID;

                                                    SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                                                    SqlDataReader reader = cmdm.ExecuteReader();
                                                    string _supersacosID = "", _LOTE = "", _cantidad = "", _descripcion = "",_fecha="";
                                                    int _TransferenciaID=0;
                                                    int i = dGVSalida.Rows.Count;
                                                    _fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                                    if (reader.HasRows)
                                                        while (reader.Read())
                                                        {
                                                            _supersacosID = Convert.ToString(reader["SUPERSACO_ID"]);
                                                            _LOTE = Convert.ToString(reader["FOLIO"]);
                                                             _TransferenciaID =Convert.ToInt32(tBFolioSalida.Text.Trim()) ;
                                                            if(_LOTE.Length==0)
                                                                _LOTE=  Convert.ToString(reader["LOTE_ID"]);
                                                            _cantidad=Convert.ToString(reader["SUPERSACO_CANTIDAD"]);
                                                            if (_cantidad.Length > 0)
                                                                _cantidad = Convert.ToDouble(_cantidad).ToString("N2");
                                                            else
                                                                _cantidad = "0.00";
                                                            _descripcion = Convert.ToString(reader["PRODUCTO_DESCRIPCION"]);
                                                            //Crear el encabezado del embarque si no existe con estatus P

                                                            CTransferencias transferencias = new CTransferencias();
                                                            ConexionSql cn2 = new ConexionSql();
                                                            SqlTransaction transaction;
                                                            try
                                                            {
                                                                cn2.ConectarSQLServer();
                                                                transaction = cn2.SC.BeginTransaction();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                MessageBox.Show(ex.Message, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                return;
                                                            }

                                                            if(!transferencias.ExisteTransferencia(_TransferenciaID))
                                                            {
                                                                if (!transferencias.InsertarEncabezadoTransferencia(_TransferenciaID.ToString(), _fecha, "P",
                                                                    _AlmacenOrigenID, _AlmacenDestinoID, this._ReqID,this._Usuario,cn2, transaction))
                                                                    mensajes.Error("Error al insertar el encabezado de la transferencia","Error");
                                                            }
                                                            //Insertar SS en Base de datos
                                                            if (transferencias.InsertarRenglonTransferencia(_TransferenciaID.ToString(), _supersacosID, cn2, transaction))
                                                            //Agregar fila al grid
                                                            {
                                                                dGVSalida.Rows.Add();
                                                                dGVSalida["SUPERSACOID", i].Value = _supersacosID;
                                                                dGVSalida["LOTE", i].Value = _LOTE;
                                                                dGVSalida["PRODUCTO", i].Value = _descripcion;
                                                                dGVSalida["CANTIDAD", i].Value = _cantidad;
                                                                dGVSalida["QUITAR", i].Value = "QUITAR";
                                                                transaction.Commit();
                                                                transaction.Dispose();
                                                                cn2.Desconectar();
                                                                mensajes.Exito("Se agregó el supersaco \"" + _SuperSacoID + "\" ", "Éxito");
                                                            }
                                                            else
                                                            {
                                                                transaction.Rollback();
                                                                transaction.Dispose();
                                                                cn2.Desconectar();
                                                                mensajes.Error("No se pudo agregar el supersaco a la transferencia, por favor reintente", "Error");
                                                                return;
                                                            }
                                                            i++;
                                                        }
                                                    cmdm.Dispose();
                                                    cn.Desconectar();
                                                }
                                                else
                                                mensajes.Error("Peso de supersaco incorrecto", "Error");
                                            }
                                            else
                                                    mensajes.Error("El supersaco ya fue agregado en esta salida de transferencia", "Error");
                                        }
                                        else
                                        mensajes.Error("Valor de báscula no capturado", "Error");
                                    }
                                    else
                                        mensajes.Error("Error al leer la báscula, favor de reintentar "+"\r\nDato recibido: "+_datoRecib+"\n\rConversion a decimal"+aux, "Error");
                                }
                                else
                                    mensajes.Error("Báscula no conectada", "Error");
                            }
                            else
                                mensajes.Error("El producto completado en esta transferencia o anteriores", "Error");
                        }
                        else
                            mensajes.Error("El supersaco no pertenece a este almacen", "Error");
                    }
                    else
                        mensajes.Error("El producto no coresponde con lo solicitado en la requisición", "Error");
                }
                else
                    mensajes.Error("Favor de especificar el supersaco", "Error");
                tBSupersacoID.Text = "";
                aux = "";
                correcto = false;
                auxSSID = "";
                //MessageBox.Show("SS Agregado");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
            button1.Enabled = true;
        }
        private string ValidarDigitos(string CADENA)
        {
            string resultado = "";
            try
            {
                for (int i = 0; i < CADENA.Length; i++)
                {
                    if (char.IsDigit(CADENA[i]) || CADENA[i] == '.')
                        resultado += CADENA[i];
                }
            }
            catch
            {
            }
            return resultado;
        }
        private void tBSupersacoID_KeyPress(object sender, KeyPressEventArgs e)
       {
            /*
            if (correcto==false)
            {if (e.KeyChar == '\n')
                    correcto = true;
            else
                    auxSSID += e.KeyChar;

            }

                if (e.KeyChar == 13)
            {
                 
                    //KgFRIJOL PINTO SALTILLOlunes, 10 agosto 2020 11Ñ27 a. m. Reimpresión
                    //1,358.80KgFRIJOL PINTO SALTILLOlunes, 10 agosto 2020 11Ñ27 a. m. Reimpresión
                    tBSupersacoID.Text=auxSSID;
                //1,358.80KgFRIJOL PINTO SALTILLOlunes, 10 agosto 2020 11Ñ27 a. m. Reimpresión
                    Agregar();
                correcto = false;
                auxSSID = "";
            }
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            // revisar que solo haya un punto permitido //checks to make sure only 1 decimal is allowed
            */
        }
        private void dGVSalida_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _fila = e.RowIndex, _columna = e.ColumnIndex;
            if(_fila >=0 &&_columna >=0)
            {
                string _contenido = Convert.ToString(dGVSalida[_columna, _fila].Value),
                    _ss = Convert.ToString(dGVSalida["SUPERSACOID", _fila].Value),
                    _prod = Convert.ToString(dGVSalida["PRODUCTO", _fila].Value),
                    _almacenID = "",
                    _transferenciaID = "" ;
                DialogResult _respuesta;
                if (_contenido == "QUITAR")
                {
                    _respuesta = MessageBox.Show("¿Desea quitar el supersaco "+_ss+" del producto\n\r"+_prod+"?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (_respuesta==DialogResult.Yes)
                    {
                        FAutorizacion autorizacion = new FAutorizacion();
                        autorizacion.TipoAutorizacion = "BODEGUERO";
                        if (autorizacion.ShowDialog() == DialogResult.Cancel)
                        {
                            if (autorizacion.TIPO_USU_ID == 2)
                            {
                                //Elimiar registro de sql y si es correcto se elimina del grid
                                CTransferencias transferencia = new CTransferencias();
                                ConexionSql cn = new ConexionSql();
                                cn.ConectarSQLServer();
                                SqlTransaction transaction;
                                transaction = cn.SC.BeginTransaction();
                                _transferenciaID = tBFolioSalida.Text.Trim();
                                if (transferencia.EliminarRenglonTransferencia(_transferenciaID, _ss, cn, transaction))
                                {
                                    dGVSalida.Rows.RemoveAt(_fila);
                                    transaction.Commit();

                                }
                                else
                                    MessageBox.Show("No se pudo eliminar el supersaco en la base de datos, por favor reintente");

                                    transaction.Dispose();
                                cn.Desconectar();
                            }
                            else
                                mensajes.Error("Usuario no válido","Error");
                        }
                    }
                }
            }
        }
        private bool BuscarEnGrid(string SUPERSACO_ID)
        {
            bool _existe = false;
            for (int i =0;dGVSalida.RowCount> i;i++)
            {
                string aux = Convert.ToString(dGVSalida["SUPERSACOID", i].Value);
                if(SUPERSACO_ID==aux)
                {
                    _existe = true;
                    break;
                }
            }
            return _existe;
        }
        private bool BuscarProducto(string NOMBRE)
    {
        bool _existe = false;
        foreach(DataRow fila in  _DATOSREQ.Rows)
            {
                string aux = Convert.ToString(fila["PRODUCTO_DESCRIPCION"]);
               // MessageBox.Show("Datos req: " + aux);
                if (NOMBRE == aux)
                {
                    _existe = true;
                    break;
                }
            }
        return _existe;
    }
        private int CantidadSupersacoProd(string PRODUCTO_ID)
        {
            int _CantProd = 0;
            //foreach (DataRow fila in _DATOSREQ.Rows)
            //{
            //    string aux = Convert.ToString(fila["PRODUCTO_DESCRIPCION"]);
            //    if (PRODUCTO_ID == aux)
            //    {
            //        _CantProd++;
            //    }
            //}
            for (int i=0;dGVSalida.Rows.Count >i;i++)
            {
                string aux = Convert.ToString(dGVSalida["PRODUCTO", i].Value);
                if (PRODUCTO_ID == aux)
                {
                    _CantProd++;
                }
            }
            return _CantProd;
        }
        private void conectarBásculaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pConectarPuerto.Visible = true;
        }
        private void desconectarBásculaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tPesobascula.Stop();
            DesconectarBascula();
        }
        private void DesconectarBascula()
        {
            try
            {
                if (this._basculaConectada == true)
                {
                    if (_PuertoSerie.EstaConectado)
                    {
                        _PuertoSerie.DesconectarPuertoSerie();
                        MessageBox.Show("Báscula desconectada");
                        desconectarBásculaToolStripMenuItem.Visible = false;
                        conectarBásculaToolStripMenuItem.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bBuscarPuertos_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (bBuscarPuertos.Text == "Buscar Puertos ")
                {
                    List<string> _listaPuertos;
                    if (_PuertoSerie.BuscarPuertos(out _listaPuertos) == true)
                    {
                        for (int i = 0; i < _listaPuertos.Count; i++)
                            cBPuertosDisponibles.Items.Add(_listaPuertos[i]);
                        bBuscarPuertos.Text = "Conectar Puerto";
                        List<string> _PuertosDisponibles = new List<string>();
                        _PuertoSerie.BuscarPuertos(out _PuertosDisponibles);
                        cBPuertosDisponibles.SelectedIndex = 0;
                        
                    }
                    else
                        MessageBox.Show("Sin puertos disponibles", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else if (bBuscarPuertos.Text == "Conectar Puerto")
                {
                    _PuertoSerie.ConectarPuerto(cBPuertosDisponibles.SelectedItem.ToString(), "9600");
                    bool _conexion = false;

                    if (_PuertoSerie.EstaConectado)
                    {
                        _conexion = true;
                        MessageBox.Show("Báscula conectada");
                        pConectarPuerto.Visible = false;
                        conectarBásculaToolStripMenuItem.Visible = false;
                        desconectarBásculaToolStripMenuItem.Visible = true;
                        //tPesobascula.Enabled = true;
                        //tPesobascula.Interval = 100;
                        ////tPesobascula += new EventHandler(tPesobascula_Tick(sender,e));
                        //tPesobascula.Start();
                    }
                    else
                        _conexion = false;

                    if (_conexion == true)
                        _basculaConectada = true;
                    else
                        _basculaConectada = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        private void FSalTra_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PuertoSerie.EstaConectado)
            {
                MessageBox.Show("Favor de desconectar el puerto serie", "Informacion");
                e.Cancel = true;
            }
        }

        private void tPesobascula_Tick(object sender, EventArgs e)
        {
            lPesoBascula.Text = "";
                lPesoBascula.Text= ValidarDigitos(_PuertoSerie.DatoRecibido());
            //int i = 0;
        }

        private void tBFolioSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(int) Keys.Enter)
            {
                //Buscar transferencia Pendiente
                DataTable _datos = new DataTable();
                string msg_local = "", _supersacosID="", _LOTE="", consulta = "",_AlmacenID="",
                    _transferencia_id = tBFolioSalida.Text.Trim(); 
                ConexionSql cn = new ConexionSql();
                DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString();
                consulta = "Select  ifx.ALMACEN_MSP_DESCRIPCION,iss.INVENTARIO_SUPERSACO_ID,s.SUPERSACO_ID " +
                    " ,p.PRODUCTO_DESCRIPCION,SUPERSACO_CANTIDAD,poc.FOLIO,S.LOTE_ID,t.TRANSFERENCIA_ESTATUS " +
                    " from TRANSFERENCIA as T " +
                    " inner join TRANSFERENCIA_DETALLE as TD on T.TRANSFERENCIA_ID = td.TRANSFERENCIA_ID " +
                    " inner join SUPERSACO AS S ON TD.SUPERSACO_ID = s.SUPERSACO_ID " +
                    " inner join PRODUCTO as P on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                    " left join INVENTARIO_SUPERSACO as iss on s.INVENTARIO_SUPERSACO_ID = iss.INVENTARIO_SUPERSACO_ID " +
                    " left join INVENTARIO_FRIMEX as ifx on iss.INVENTARIO_FRIMEX_ID = ifx.INVENTARIO_FRIMEX_ID " +
                    " left join EMBARQUE AS E ON S.EMBARQUE_ID = E.EMBARQUE_ID " +
                    " left join PETICION_OC AS POC ON e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where " +
                    " T.TRANSFERENCIA_ID =" + _transferencia_id + " /*AND T.TRANSFERENCIA_ESTATUS='P'*/" +
                    " and iss.INVENTARIO_FRIMEX_ID = "+_AlmacenID ;
                cn.ConectarSQLServer();
                SqlDataAdapter _DA = new SqlDataAdapter(consulta, cn.SC);
                _DA.Fill(_datos);
                if (_datos.Rows.Count>0)
                {
                    string _auxEstatus = "";
                    _auxEstatus=Convert.ToString(_datos.Rows[0]["TRANSFERENCIA_ESTATUS"]);
                    if (_auxEstatus == "P")
                    {
                        tBFolioSalida.Enabled = true;
                        tBSupersacoID.Enabled = false;
                        button1.Enabled = false;
                        mSOpciones.Enabled = false;
                        cBAlmacen.Enabled = false;
                        int i = 0;
                        foreach (DataRow _fila in _datos.Rows)
                        {
                            dGVSalida.Rows.Add();
                            _supersacosID = Convert.ToString(_fila["SUPERSACO_ID"]);
                            _LOTE = Convert.ToString(_fila["FOLIO"]);
                            if (_LOTE.Length == 0)
                                _LOTE = Convert.ToString(_fila["LOTE_ID"]);
                            dGVSalida["SUPERSACOID", i].Value = _supersacosID;
                            dGVSalida["LOTE", i].Value = _LOTE;
                            dGVSalida["PRODUCTO", i].Value = Convert.ToString(_fila["PRODUCTO_DESCRIPCION"]);
                            dGVSalida["CANTIDAD", i].Value = Convert.ToDouble(_fila["SUPERSACO_CANTIDAD"]).ToString("N2");
                            dGVSalida["QUITAR", i].Value = "QUITAR";
                            i++;
                        }
                        //Cargar datos requisicion
                        consulta = "select r.REQUISICION_ID,r.REQUISICION_FECHA_CREACION,r.REQUISICION_FECHA,p.PRODUCTO_ID,P.PRODUCTO_MSP_ID,r.REQUISICION_ID " +
                            " ,p.PRODUCTO_DESCRIPCION " +
                            " ,REQUISICION_DETALLE_CANTIDAD,ifr.INVENTARIO_FRIMEX_ID,ifr.ALMACEN_MSP_DESCRIPCION,r.ALMACEN_DESTINO_ID " +
                            " from REQUISICION as r " +
                            " inner join REQUISICION_DETALLE as rd on r.REQUISICION_ID = rd.REQUISICION_ID " +
                            " inner join PRODUCTO as p on rd.PRODUCTO_ID = p.PRODUCTO_ID or rd.PRODUCTO_ID = P.PRODUCTO_ID " +
                            " inner join INVENTARIO_FRIMEX as ifr on r.ALMACEN_ID = ifr.INVENTARIO_FRIMEX_ID " +
                            " inner join REQ_TRA as RT on R.REQUISICION_ID = RT.REQUISICION_ID " +
                            " where r.REQUISICION_ESTATUS = 'A' AND RT.TRANSFERENCIA_ID = " + _transferencia_id;
                        ConexionSql cn2 = new ConexionSql();
                        cn2.ConectarSQLServer();
                        SqlDataAdapter _DA2 = new SqlDataAdapter(consulta, cn2.SC);
                        _DA2.Fill(_DATOSREQ);
                        foreach (DataRow _fila in _DATOSREQ.Rows)
                            this._ReqID = Convert.ToInt32(_fila["REQUISICION_ID"]);
                        _DA2.Dispose();
                        cn2.Desconectar();
                        tBFolioSalida.Enabled = false;
                        tBSupersacoID.Enabled = true;
                        button1.Enabled = true;
                        mSOpciones.Enabled = true;
                        cBAlmacen.Enabled = true;
                        this.Text = "Salida de supersacos de la requisición- " + this._ReqID + "-";
                    }else if (_auxEstatus == "A")
                        mensajes.Informacion("La transferencia ya fue enviada anteriormente","Información");
                    else if (_auxEstatus == "T")
                        mensajes.Informacion("La transferencia ya fue completada", "Información");
                    else if (_auxEstatus == "C")
                        mensajes.Informacion("La transferencia ya fue cancelada anteriormente", "Información");
                }
                else
                    mensajes.Informacion("Transferencia no encontrada para este almacen", "Error");
                _DA.Dispose();
                cn.Desconectar();
            }
        }

        private bool ValidaAlmacenSS(string SUPERSACOID,string INVENTARIOID)
        {
            bool _existe = false;
            string msg_local = "", consulta = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                string _inventarioID = "";
                consulta = "select SUPERSACO_ID,iss.INVENTARIO_FRIMEX_ID, PRODUCTO_ID, SUPERSACO_CANTIDAD  "+
                    " from SUPERSACO as s "+
                    " inner join INVENTARIO_SUPERSACO as ISS on s.INVENTARIO_SUPERSACO_ID = iss.INVENTARIO_SUPERSACO_ID " +
                    " where s.SUPERSACO_ID = " + SUPERSACOID;
                cn.ConectarSQLServer();
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                    _inventarioID = Convert.ToString(reader["INVENTARIO_FRIMEX_ID"]);
                cmdm.Dispose();
                cn.Desconectar();
                if (_inventarioID == INVENTARIOID)                
                    _existe = true;                
                else
                    _existe = false;
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

            return _existe;
        }
    }
}
