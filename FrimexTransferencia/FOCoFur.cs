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
using FirebirdSql.Data.FirebirdClient;

namespace FrimexTransferencia
{
    public partial class FOCoFur : Form
    {
        public FOCoFur()
        {
            InitializeComponent();
        }
        public UsuariosC _Usuario = new UsuariosC();
        Mensajes mensaje = new Mensajes();
        List<string[,]> _Productos = new List<string[,]>();
        bool _calidad = false, _peso = false, _sellosCorrectos = false, _sellosIntegros = false, _tiempoEntrega = false;
        string _IdProveedor = "";
        public string _centroCompraID, _centroCompraNombre, _embarqueID = "";
        string _FolioOrdenCompra = "";
        string _FolioCompra = "", _OC_ID = "";
        string _LineasArticulos = "";
        string _DOCTO_CM_ID = "";


        DataTable _DatosOCMsp = new DataTable();
        private void tBCantReciv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //Ejecutar
            }
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            // revisar que solo haya un punto permitido //checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private bool AgregarFugonLote(string DocumetoID,string FugronFolio,string ContratoFolio)
        {
            bool _exito = false;
            try
            {
                string consulta = "update LIBRES_OCO_CM " +
                    " set " +
                    " FURGONASIGNADO = 'S', " +
                    " FURGON = '"+FugronFolio+"', " +
                    " CONTRATO = '"+ContratoFolio+"' " +
                    " where(DOCTO_CM_ID = "+DocumetoID+")";
                ConexionMicrosip con_msp = new ConexionMicrosip();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FbCommand fbCommand = new FbCommand(consulta, con_msp.FBC);
                
                fbCommand.ExecuteNonQuery();
                fbCommand.Cancel();
                con_msp.Desconectar();
                _exito = true;
            }
            catch 
            {
                _exito = false;
            }
            return _exito;
        }
        private void bAceptar_Click(object sender, EventArgs e)
        {
            string msg_local = "",_folio="",_cantidadRecibir="";
            _folio = tBFolioMSP.Text.Trim();
            _cantidadRecibir = tBCantReciv.Text.Trim();
            DialogResult result = MessageBox.Show("¿Desea agregar el furgón seleccionado a la orden de compra especificada?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                if (_folio.Length > 0)
                {
                    if (_cantidadRecibir.Length >= 0)
                    {
                        //Buscar();
                        //MessageBox.Show("Se asignará el folio de MSP al furgón especificado");
                        ConexionSql cn = new ConexionSql();
                        DataRowView _dRV = (DataRowView)cBFurgon.SelectedItem;
                        string _FurgonID = _dRV.Row.ItemArray[0].ToString();
                        string _FurgonFolio= _dRV.Row.ItemArray[1].ToString();
                        string _Contrato = _dRV.Row.ItemArray[5].ToString();
                        string _folioMSP = "", consulta = "select Folio_MSP from Furgon where furgon_id=" + _FurgonID;
                        if (AgregarFugonLote(_DOCTO_CM_ID, _FurgonFolio, _Contrato))
                        {
                            cn.ConectarSQLServer();
                            SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                                while (reader.Read())
                                {
                                    _folioMSP = Convert.ToString(reader["Folio_MSP"]);
                                }
                            reader.Close();
                            cmd.Dispose();
                            cn.Desconectar();
                            if (_folioMSP.Length == 0)
                            {
                                consulta = "UPDATE [dbo].[FURGON] " +
                                    " SET[FOLIO_MSP] = '" + tBFolioMSP.Text + "'" +
                                    " WHERE FURGON_ID = " + _FurgonID;
                                cn.ConectarSQLServer();
                                cmd = new SqlCommand(consulta, cn.SC);
                                if (cmd.ExecuteNonQuery() > 0)
                                    MessageBox.Show("Folio del furgón actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Folio del furgón actualizado correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("El folio del furgron ya había sido actualizado \r\n Folio asignado: " + _folioMSP,"Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show("El furgon no se pudo actualizar en MSP","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    else
                        mensaje.Error("El folio no ingresado", "Error");
                }
                else
                    mensaje.Error("El folio no ingresado", "Error");                
            }            
        }

        private void FOCoFur_Load(object sender, EventArgs e)
        {
            cBFurgon.DataSource = CargarFurgones();
            cBFurgon.DisplayMember = "DETALLE";
            cBFurgon.ValueMember = "FURGON_ID";
        }
        private DataTable CargarFurgones()
        {
            DataTable _furgones = new DataTable();
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "SELECT f.FURGON_ID,f.FURGON_FOLIO,p.PRODUCTO_DESCRIPCION" +
                    " ,f.FURGON_FOLIO+' - '+p.PRODUCTO_DESCRIPCION as DETALLE,p.PRODUCTO_MSP_ID,C.CONTRATO_FOLIO" +
                    " FROM FURGON AS F " +
                    " left join PRODUCTO as p on f.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                    " LEFT JOIN FURGON_EMBARQUE AS FE ON F.FURGON_ID = FE.FURGON_ID " +
                    " left join CONTRATO as c on f.CONTRATO_ID=c.CONTRATO_ID" +
                    " WHERE " +
                    " F.FURGON_ESTATUS IS NULL or F.FURGON_ESTATUS='P'";

                SqlDataAdapter _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_furgones);
                _da.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _furgones;
        }
        private void Buscar()
        {
            Mensajes Mensaje = new Mensajes();
            try
            {
                bool _valido = true;
                DialogResult _resp = new DialogResult();

                if (_valido == true)
                {
                    _resp = MessageBox.Show("¿Desea hacer recepcíon del producto indicado\nEn la orden de compra?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (_resp == DialogResult.Yes)
                    {
                        double Cant = Convert.ToDouble((tBCantReciv.Text.Trim().Length > 0 ? tBCantReciv.Text.Trim() : "0"));
                        if (Cant > 0)
                        {
                            InsertarOC();
                        }
                        else
                            MessageBox.Show("Cantidades no han sido especificadas", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("El detalle no corresponde a la orden de compra seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception Ex)
            {
                Mensaje.Error(Ex.Message, "Error");
            }
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            LimpiarForma();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _ArticuloID = "";
            DataRowView _dRVProducto = (DataRowView)cBFurgon.SelectedItem;
            if (_dRVProducto is null)
                mensaje.Informacion("Producto no seleccionado", "Información");
            else
            {
                _ArticuloID = _dRVProducto.Row.ItemArray[4].ToString();
                FOCoMSP _OC = new FOCoMSP();
                _OC._Usuarios = _Usuario;
                _OC._ArticulioID = _ArticuloID;
                _OC.ShowDialog();
                tBFolioMSP.Text = _OC._OCSeleccionada;
                _DOCTO_CM_ID= _OC._DOCTO_CM_ID;
            }
        }

        private void tBFolioMSP_TextChanged(object sender, EventArgs e)
        {
            string msg_local = "";
            int folio = 0;
            folio = Convert.ToInt32(tBFolioMSP.Text);
            //Buscar folio en microsip
            DataTable _datos = BuscarPedimento(folio, out msg_local);
            // llenar datos de tB
            if (_datos.Rows.Count > 0)
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                // int i = 0;
                foreach (DataRow _fila in _datos.Rows)
                {
                    string q = "select * from PRODUCTO as p " +
                        " where p.PRODUCTO_MSP_ID = " + Convert.ToString(_fila["ARTICULO_ID"]);
                    SqlCommand cmd = new SqlCommand(q, cn.SC);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            string[,] nuevo = new string[1, 2];
                            nuevo[0, 0] = Convert.ToString(reader["PRODUCTO_ID"]);
                            nuevo[0, 1] = Convert.ToString(_fila["ARTICULO_ID"]);
                            //_datos[i,""] = Convert.ToString(_fila["PRODUCTO_ID"]);
                            _Productos.Add(nuevo);
                        }
                }
                cn.Desconectar();
                bool _igual = false;
                DataRowView _dRV = (DataRowView)cBFurgon.SelectedItem;
                string aux = _dRV.Row.ItemArray[4].ToString(), productoMSP = "", productofurgon = _dRV.Row.ItemArray[2].ToString();
                foreach (DataRow fila in _datos.Rows)
                {
                    string _aux = fila[0].ToString();
                    productoMSP = fila[1].ToString();
                    if (_aux == aux)
                    {
                        _igual = true;
                        break;
                    }
                }
                if (_igual == true)
                {
                    cBProducto.DataSource = _datos;
                    cBProducto.DisplayMember = "NOMBRE";
                    cBProducto.ValueMember = "ARTICULO_ID";
                    //bGuardar.Text = "Guardar";
                    //bCancelar.Visible = true;
                    tBFolioMSP.Enabled = false;
                    cBFurgon.Enabled = false;
                    cBProducto.Enabled = false;
                }
                else
                    msg_local = "El producto de la orden de compra no corresponde con el especificado en el furgón\n\r\n\rProducto del furgón: " + productofurgon + " \n\rProducto de la orden de compra: " + productoMSP;
            }
            else
                msg_local = "Folio no encontrado";
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tBFolioOCMSP_KeyPress(object sender, KeyPressEventArgs e)
        {
            string msg_local = "";
            if (e.KeyChar == 13)
            {
                int folio = 0;
                folio = Convert.ToInt32(tBFolioMSP.Text);
                //Buscar folio en microsip
                DataTable _datos = BuscarPedimento(folio, out msg_local);
                // llenar datos de tB
                if (_datos.Rows.Count > 0)
                {
                    ConexionSql cn = new ConexionSql();
                    cn.ConectarSQLServer();
                    // int i = 0;
                    foreach (DataRow _fila in _datos.Rows)
                    {
                        string q = "select * from PRODUCTO as p " +
                            " where p.PRODUCTO_MSP_ID = " + Convert.ToString(_fila["ARTICULO_ID"]);
                        SqlCommand cmd = new SqlCommand(q, cn.SC);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                            while (reader.Read())
                            {
                                string[,] nuevo = new string[1, 2];
                                nuevo[0, 0] = Convert.ToString(reader["PRODUCTO_ID"]);
                                nuevo[0, 1] = Convert.ToString(_fila["ARTICULO_ID"]);
                                //_datos[i,""] = Convert.ToString(_fila["PRODUCTO_ID"]);
                                _Productos.Add(nuevo);
                            }
                    }
                    cn.Desconectar();
                    bool _igual = false;
                    DataRowView _dRV = (DataRowView)cBFurgon.SelectedItem;
                    string aux = _dRV.Row.ItemArray[4].ToString(), productoMSP = "",productofurgon= _dRV.Row.ItemArray[2].ToString();
                    foreach(DataRow fila in _datos.Rows)
                    {
                        string _aux = fila[0].ToString();
                        productoMSP= fila[1].ToString();
                        if (_aux==aux)
                        {
                            _igual = true;
                            break;
                        }
                    }
                    if (_igual == true)
                    {
                        cBProducto.DataSource = _datos;
                        cBProducto.DisplayMember = "NOMBRE";
                        cBProducto.ValueMember = "ARTICULO_ID";
                        //bGuardar.Text = "Guardar";
                        //bCancelar.Visible = true;
                        tBFolioMSP.Enabled = false;
                        cBFurgon.Enabled = false;
                        cBProducto.Enabled = false;
                    }
                    else
                        msg_local = "El producto de la orden de compra no corresponde con el especificado en el furgón\n\r\n\rProducto del furgón: " + productofurgon+" \n\rProducto de la orden de compra: "+productoMSP;
                }
                else
                    msg_local = "Folio no encontrado";

            } else             if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
            // revisar que solo haya un punto permitido //checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        DataTable BuscarPedimento(int folio, out string msg)
        {
            string consulta = "", msg_local = "";
            DataTable _datos = new DataTable();
            try
            {
                ConexionMicrosip con_msp = new ConexionMicrosip();
                string folio_cadena = "";
                if ((folio.ToString().Length > 0) && 9 > (folio.ToString().Length))
                    for (int i = folio.ToString().Length; 9 > i; i++)
                        folio_cadena += "0";
                consulta = "select a.articulo_id AS ARTICULO_ID,a.nombre as NOMBRE,p.clave as NO_PEDIMENTO,p.fecha as FECHA_PEDIMENTO " +
                    " ,dcm.docto_cm_id as OC_ID,dcm.folio as FOLIO,dcm.fecha as FECHA_OC,dcm.descripcion as DESCRIPCION ,dcm.importe_neto as IMPORTE " +
                    " ,dcmd.DOCTO_CM_DET_ID " +
                    " ,DCMD.ARTICULO_ID as PRODUCTO_ID,CLAVE_ARTICULO " +
                    " ,dcmd.UNIDADES " +
                    " ,PRECIO_UNITARIO AS PRECIO,UNIDADES_REC_DEV,UNIDADES_A_REC " +
                    " ,UMED,PRECIO_TOTAL_NETO,dcm.proveedor_id,dcm.ESTATUS,dcm.DOCTO_CM_ID " +
                    " from doctos_cm as dcm " +
                    " left join doctos_cm_det as dcmd on(dcm.docto_cm_id = dcmd.docto_cm_id) " +
                    " left join doctos_cm_ligas as dcml on(dcml.docto_cm_fte_id = dcm.docto_cm_id) " +
                    " left join doctos_cm as dcm2 on(dcml.docto_cm_dest_id = dcm2.docto_cm_id) " +
                    " left join pedimentos as p on(dcm2.pedimento_id = p.pedimento_id) " +
                    " left join articulos as a on(a.articulo_id = dcmd.articulo_id) " +
                    " where dcm.tipo_docto = 'O' " +
                    " and dcm.folio = '" + folio_cadena + folio + "' ";
                RegistrosWindows registros = new RegistrosWindows();
                RegistrosWindows reg = new RegistrosWindows();
                reg.LeerRegistros("SOFTWARE\\SOTI\\FrimexTransferencias");
                con_msp.ConectarMicrosip(reg.MICRO_BD);
                FbDataAdapter _da = new FbDataAdapter(consulta, con_msp.FBC);
                _da.Fill(_datos);
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _datos;
        }
        private void InsertarOC()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                //Buscar Orden de compra por folio

                //Insertar Orden de compra
                cn.ConectarSQLServer();
                string _EstatusId = "4", PETICION_OC_ID = "";
                DataRowView _dRVProd = (DataRowView)cBProducto.SelectedItem;
                string _IdProveedor = _dRVProd.Row.ItemArray[18].ToString();
                string _fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string _ProductoMsp = _dRVProd.Row.ItemArray[0].ToString();
                string _msg = "";
                string _FolioMsp = tBFolioMSP.Text.Trim();
                string _FolioOCId = "";
                string _Prod_Id = "";
                //Insertar Lote Embarque
                if (_FolioMsp.Length == 0)
                    mensaje.Error("Folio no especificado", "Error");
                else
                {
                    OrdenesCompra _OC = new OrdenesCompra();
                    if (!_OC.ExisteOC(Convert.ToString(_FolioMsp), out _FolioOCId))
                    {
                        for (int i = 0; i < _Productos.Count; i++)
                        {
                            if (_Productos[i][0, 1] == _ProductoMsp)
                            {
                                _Prod_Id = _Productos[i][0, 0];
                                _OC_ID = _OC.InsertarOCSQL(_Usuario, _IdProveedor,
                                    Convert.ToString(_dRVProd["FOLIO"]),
                                    Convert.ToString(_dRVProd["FECHA_OC"]),
                                    Convert.ToString(_dRVProd["ESTATUS"]),
                                    Convert.ToString(_dRVProd["DESCRIPCION"]),
                                    Convert.ToString(_dRVProd["IMPORTE"]),
                                    _Prod_Id,
                                    Convert.ToString(_dRVProd["DOCTO_CM_DET_ID"]),
                                    Convert.ToString(_dRVProd["DOCTO_CM_ID"]),
                                    Convert.ToString(_dRVProd["ARTICULO_ID"]),
                                    Convert.ToString(_dRVProd["CLAVE_ARTICULO"]),
                                    Convert.ToString(_dRVProd["UNIDADES"]),
                                    Convert.ToString(_dRVProd["PRECIO"]),
                                    Convert.ToString(_dRVProd["UNIDADES_REC_DEV"]),
                                    Convert.ToString(_dRVProd["UNIDADES_A_REC"]),
                                    Convert.ToString(_dRVProd["UMED"]),
                                    Convert.ToString(_dRVProd["PRECIO_TOTAL_NETO"]),
                                    Convert.ToString(_dRVProd["NOMBRE"])
                                );
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("La peticion de compra con el folio: \"" + Convert.ToString(_dRVProd["FOLIO"]) + "\" ya existe\nSe realizará el embarque","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    string EMBARQUE_ID = ExisteEmbarque(_FolioMsp, out _msg);
                    if (EMBARQUE_ID.Length == 0)
                    {
                        //insertar lotes
                        if (_OC_ID.Length == 0)
                            _OC_ID = _FolioOCId;


                        string _cant = "", _pesoRecibir = "";
                        LotesEmbarque Lotes = new LotesEmbarque();

                        _cant = tBCantReciv.Text.Trim();//Convert.ToString(dGVDetalleOC["UNIDADES", h].Value);
                        _pesoRecibir = tBCantReciv.Text.Trim();

                        string loteID = InsertarLotes(_OC_ID, _Prod_Id, _pesoRecibir);
                        //insertar Embarque
                        _embarqueID = InsertarEmbarqueOC(_cant, "O", _OC_ID, _Prod_Id, _pesoRecibir);//Traer el id de embarque no el del documento
                        Lotes.ActualizaLote(_embarqueID, loteID);

                        _tiempoEntrega = _calidad = _peso = _sellosCorrectos = _sellosIntegros = true;
                        //Vincular Embarque con Furgon
                        DataRowView _dRVfurg = (DataRowView)cBFurgon.SelectedItem;
                        string _furgonID= Convert.ToString(_dRVProd.Row.ItemArray[0]);

                        string consulta = "INSERT INTO [dbo].[FURGON_EMBARQUE] " +
                                   " ([FURGON_ID] " +
                                   " ,[EMBARQUE_ID] " +
                                   " ,[FURGON_EMBARQUE_ESTATUS] " +
                                   " ,[FURGON_FECHA_CREACION] " +
                                   " ,[FURGON_USUARIO_CREADOR]) " +
                                   " VALUES " +
                                   " ( " + _furgonID +
                                   " , " + _embarqueID +
                                   " ,'A'" +
                                   " ,'" + _fecha + "'"+
                                   " ,"+_Usuario.USUARIOID+")";
                        cn.ConectarSQLServer();
                        SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                        if( cmdm.ExecuteNonQuery() >0)
                            mensaje.Exito("Embarque capturado con exito Id para recepción de mercancia: " + _embarqueID + "\n\rFavor de notificar al personal de bodegas", "");
                                               
                        
                        cmdm.Dispose();
                        cn.Desconectar();
                        LimpiarForma();
                    }
                    else
                    {
                        MessageBox.Show("El embarque para el folio seleccionado " + _FolioMsp + " ya existe es el Embarque ID" + EMBARQUE_ID,"Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }                    
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (cn.IsConected())
                    cn.Desconectar();
            }
        }
        private string ExisteEmbarque(string FOLIO, out string msg)
        {

            string msg_local = "", consulta = "", EMBARQUE_ID = "";
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = "Select EMBARQUE_ID from EMBARQUE as e " +
                    " inner join PETICION_OC as poc on e.EMBARQUE_DOCUMENTO_ID = poc.PETICION_OC_ID " +
                    " where " +
                    " poc.FOLIO = " + FOLIO;
                SqlCommand cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    EMBARQUE_ID = Convert.ToString(reader["EMBARQUE_ID"]);
                reader.Close();
                cmd.Cancel();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return EMBARQUE_ID;
        }

        private string InsertarLotes(string _OCID, string _productoID, string Cantidad)
        {
            ConexionSql cn = new ConexionSql();
            cn.ConectarSQLServer();
            string lote = cn.ObtenerSigID("LOTEEMBARQUE").ToString();
            //insertar lote

            string _SERIE = "O", _FECHA = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), _TIPODOC = "O";
            string consulta = "INSERT INTO [dbo].[LOTE_EMBARQUE] " +
                   " ([LOTE_EMBARQUE_ID]" +
                   " ,[LOTE_EMBARQUE_SERIE] " +
                   " ,[LOTE_EMBARQUE_FECHA_CREACION] " +
                   " ,[LOTE_EMBARQUE_USUARIO_CREA] " +
                   " ,[TIPO_DOCUMENTO] " +
                   " ,[DOCUMENTO_ID] " +
                   " ,[EMBARQUE_ID]" +
                   " ,[PRODUCTO_ID]" +
                   " ,[LOTE_EMBARQUE_CANTIDAD]" +
                   ") " +
                   " VALUES " +
                   " ( " + lote +
                   "  ,'" + _SERIE +
                   "','" + _FECHA +
                   "', " + _Usuario.USUARIOID.ToString() +
                   " ,'" + _TIPODOC +
                   "' ," + _OCID +
                   " , NULL" +
                   " , " + _productoID +
                   " , " + Cantidad +
                   " )";

            SqlCommand cmd = new SqlCommand(consulta, cn.SC);
            cmd.ExecuteNonQuery();
            cn.Desconectar();
            return lote;
        }
        private string InsertarEmbarqueOC(string _pesoNeto, string _tipo, string _peticion, string _producto, string _cantSacos)
        {
            // Actualizar lotes
            ConexionSql ConexionSQL = new ConexionSql();
            string EmbarqueID = "", consulta = "";
            try
            {
                //insertar embarque
                SqlCommand cmd;
                string DocumentoID = ConexionSQL.ObtenerSigID("LOTEEMBARQUE").ToString();
                string _boleta1 = "", _boleta2 = "", _pesada1 = "0", _pesada2 = "0", _conductor1 = "", _conductor2 = "", _nombre = "", _placa = "", _fecha = "",
                    _sellos = "", _estatus = "", _bodega = "0";
                _estatus = "13";
                _fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                consulta = "INSERT INTO [dbo].[EMBARQUE] " +
                    " ([EMBARQUE_ID]" +
                    " ,[EMBARQUE_PESO_NETO] " +
                    " ,[EMBARQUE_BOLETA1] " +
                    " ,[EMBARQUE_BOLETA2] " +
                    " ,[EMBARQUE_PESO1] " +
                    " ,[EMBARQUE_PESO2] " +
                    " ,[EMBARQUE_CONDUCTORP1] " +
                    " ,[EMBARQUE_CONDUCTORP2] " +
                    " ,[EMBARQUE_CHOFER] " +
                    " ,[EMBARQUE_PLACA] " +
                    " ,[EMBARQUE_FECHA] " +
                    " ,[EMBARQUE_SELLOS] " +
                    " ,[ESTATUS_ID] " +
                    " ,[PRODUCTO_ID]" +
                    " ,[BODEGA_ID]" +
                    " ,[EMBARQUE_TIPO]" +
                    " ,[EMBARQUE_DOCUMENTO_ID] " +
                    " ,[EMBARQUE_NO_SACOS]" +
                    ") " +
                    " VALUES " +
                    " ( " + DocumentoID +
                    " , " + _cantSacos +
                    " ,'" + _boleta1 +
                    "','" + _boleta2 +
                    "', " + _pesada1 +
                    " , " + _pesada2 +
                    " ,'" + _conductor1 +
                    "','" + _conductor2 +
                    "','" + _nombre +
                    "','" + _placa +
                    "','" + _fecha +
                    "','" + _sellos +
                    "', " + _estatus +
                    " , " + _producto +
                    " , " + _bodega +
                    " ,'" + _tipo +
                    "', " + _peticion +
                    " , " + "1" +
                    " ) ";
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                cmd.ExecuteNonQuery();
                ConexionSQL.Desconectar();

                consulta = "select EMBARQUE_ID from EMBARQUE WHERE " +
                    " EMBARQUE_PESO_NETO= " + _cantSacos +
                    " AND EMBARQUE_BOLETA1='" + _boleta1 +
                    "'AND EMBARQUE_BOLETA2='" + _boleta2 +
                    "'AND EMBARQUE_PESO1= " + _pesada1 +
                    " AND EMBARQUE_PESO2= " + _pesada2 +
                    " AND EMBARQUE_CONDUCTORP1='" + _conductor1 +
                    "'AND EMBARQUE_CONDUCTORP2='" + _conductor2 +
                    "'AND EMBARQUE_CHOFER='" + _nombre +
                    "'AND EMBARQUE_PLACA='" + _placa +
                    "'AND EMBARQUE_FECHA='" + _fecha +
                    "'AND EMBARQUE_SELLOS='" + _sellos +
                    "'AND ESTATUS_ID=" + _estatus +
                    " AND PRODUCTO_ID=" + _producto +
                    " AND BODEGA_ID=" + _bodega +
                    " AND EMBARQUE_TIPO='" + _tipo +
                    "'AND EMBARQUE_DOCUMENTO_ID= " + _peticion +
                    " AND EMBARQUE_NO_SACOS= " + "1" /*+
                                                            _complementoValues*/;
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmbarqueID = reader["EMBARQUE_ID"].ToString();
                }
                cmd.Dispose();
                reader.Close();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                ConexionSQL.Desconectar();
            }
            return EmbarqueID;
        }

        private void LimpiarForma()
        {
            tBCantReciv.Text = "";
            tBFolioMSP.Text = "";
            cBProducto.DataSource = null;
            cBProducto.Enabled = true;
            cBFurgon.Enabled = true;
            tBFolioMSP.Enabled = true;
            _DOCTO_CM_ID = "";            
        }

        //private void InsertarOC()
        //{
        //    ConexionSql cn = new ConexionSql();
        //    try
        //    {
        //        //Buscar Orden de compra por folio

        //        //Insertar Orden de compra
        //        cn.ConectarSQLServer();
        //        string _EstatusId = "4", PETICION_OC_ID = "";
        //        DataRowView _dRVProd = (DataRowView)cBProducto.SelectedItem;
        //        string _IdProveedor = _dRVProd.Row.ItemArray[18].ToString();
        //        string _fecha = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
        //        string _ProductoMsp = _dRVProd.Row.ItemArray[0].ToString();
        //        string _msg = "";
        //        string _FolioMsp = tBFolioMSP.Text.Trim();
        //        string _FolioOCId = "";
        //        string _Prod_Id = "";
        //        //Insertar Lote Embarque
        //        if (_FolioMsp.Length == 0)
        //            mensaje.Error("Folio no especificado", "Error");
        //        else
        //        {
        //            OrdenesCompra _OC = new OrdenesCompra();
        //            if (!_OC.ExisteOC(Convert.ToString(_FolioMsp), out _FolioOCId))
        //            {
        //                for (int i = 0; i < _DatosOCMsp.Rows.Count; i++)
        //                {
        //                    if (Convert.ToString(_DatosOCMsp.Rows[i][""]) == _ProductoMsp)
        //                    {
        //                        _Prod_Id = _Productos[i][0, 0];
        //                        _OC_ID = _OC.InsertarOCSQL(_Usuario, _IdProveedor,
        //                            Convert.ToString(_dRVProd["FOLIO"]),
        //                            Convert.ToString(_dRVProd["FECHA_OC"]),
        //                            Convert.ToString(_dRVProd["ESTATUS"]),
        //                            Convert.ToString(_dRVProd["DESCRIPCION"]),
        //                            Convert.ToString(_dRVProd["IMPORTE"]),
        //                            _Prod_Id,
        //                            Convert.ToString(_dRVProd["DOCTO_CM_DET_ID"]),
        //                            Convert.ToString(_dRVProd["DOCTO_CM_ID"]),
        //                            Convert.ToString(_dRVProd["ARTICULO_ID"]),
        //                            Convert.ToString(_dRVProd["CLAVE_ARTICULO"]),
        //                            Convert.ToString(_dRVProd["UNIDADES"]),
        //                            Convert.ToString(_dRVProd["PRECIO"]),
        //                            Convert.ToString(_dRVProd["UNIDADES_REC_DEV"]),
        //                            Convert.ToString(_dRVProd["UNIDADES_A_REC"]),
        //                            Convert.ToString(_dRVProd["UMED"]),
        //                            Convert.ToString(_dRVProd["PRECIO_TOTAL_NETO"]),
        //                            Convert.ToString(_dRVProd["NOMBRE"])
        //                        );
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("La peticion de compra con el folio: \"" + Convert.ToString(_dRVProd["FOLIO"]) + "\" ya existe,\nSe realizará el embarque");
        //            }
        //            string EMBARQUE_ID = ExisteEmbarque(_FolioMsp, out _msg);
        //            if (EMBARQUE_ID.Length == 0)
        //            {
        //                //insertar lotes
        //                if (_OC_ID.Length == 0)
        //                    _OC_ID = _FolioOCId;


        //                string _cant = "", _pesoRecibir = "";
        //                LotesEmbarque Lotes = new LotesEmbarque();

        //                _cant = tBCantReciv.Text.Trim();//Convert.ToString(dGVDetalleOC["UNIDADES", h].Value);
        //                _pesoRecibir = tBCantReciv.Text.Trim();

        //                string loteID = InsertarLotes(_OC_ID, _Prod_Id, _pesoRecibir);
        //                //insertar Embarque
        //                _embarqueID = InsertarEmbarqueOC(_cant, "O", _OC_ID, _Prod_Id, _pesoRecibir);//Traer el id de embarque no el del documento
        //                Lotes.ActualizaLote(_embarqueID, loteID);

        //                _tiempoEntrega = _calidad = _peso = _sellosCorrectos = _sellosIntegros = true;

        //                mensaje.Exito("Embarque capturado con exito Id para recepción de mercancia: " + _embarqueID + "\n\rFavor de notificar al personal de bodegas", "");
        //                LimpiarFroma();
        //            }
        //            else
        //            {
        //                MessageBox.Show("El embarque para el folio seleccionado " + _FolioMsp + " ya existe es el Embarque ID" + EMBARQUE_ID);
        //            }
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message, "Error");
        //        if (cn.IsConected())
        //            cn.Desconectar();
        //    }
        //}

    }
}
