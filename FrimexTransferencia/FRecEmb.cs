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
using System.Collections;


namespace FrimexTransferencia
{
    public partial class FRecEmb : Form
    {
        Mensajes mensaje = new Mensajes();
        public UsuariosC _Usuario = new UsuariosC();
        public string _taraLlena = "", _BodegaFrimexID = "", _embarqueID = "", _BodegaFrimexDesc = "", _loteID = "",
            _AlmacenID = "", _Almacen_desc = "", _Serie = "", _tipoDocumento = "", _Documento = "", _RecepcionID = "";
        string _LOTE_ID = "";
        DataTable _EMBARQUE = new DataTable();
        private void bAsignar_Click(object sender, EventArgs e)
        {
            int _Fila = dGVSupersacos.Rows.GetRowCount(DataGridViewElementStates.Selected);
            DataRowView _dRVProducto = (DataRowView)cBProducto.SelectedItem;
            List<int> _filas = new List<int>();
            int selectedCellCount = dGVSupersacos.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedCellCount > 0)
            {
                for (int i = 0; i < selectedCellCount; i++)
                {
                    bool _existe = false;
                    int _selected = dGVSupersacos.SelectedCells[i].RowIndex;
                    for (int j = 0; j < _filas.Count; j++)
                        if (_selected == _filas[j])
                            _existe = true;
                    if (_existe == false)
                        _filas.Add(_selected);
                }
            }
            string _productoID = _dRVProducto.Row.ItemArray[0].ToString();
            string _productoDescripcion = _dRVProducto.Row.ItemArray[1].ToString();
            for (int i = 0; i < _filas.Count; i++)
            {
                if (dGVSupersacos["Agregar", _filas[i]].Value.ToString() == "Agregar")
                {
                    dGVSupersacos["Producto", _filas[i]].Value = _productoDescripcion;
                    dGVSupersacos["ProductoID", _filas[i]].Value = _productoID;
                }
                else
                    MessageBox.Show("No se puede modificar un supersaco guardado","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void dGVSupersacos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int _fila = e.RowIndex, _columna = e.ColumnIndex;
            string _contenido = Convert.ToString(dGVSupersacos[_columna, _fila].Value);
            DialogResult _respuesta = DialogResult.No;
            if (_contenido == "Agregar")
            {
                if (dGVSupersacos["CapturarPeso", _fila].Value.ToString() == "Capturado")
                {

                    double _cantidad = Convert.ToDouble(dGVSupersacos["Peso", _fila].Value.ToString() != "" ? dGVSupersacos["Peso", _fila].Value.ToString() : "0");
                    if (_cantidad > 10 && _cantidad < 2000)
                    {
                        if (tBSerie.Text.Length > 0)
                        {
                            try
                            {
                                CLotesMSP cLotes = new CLotesMSP();
                                AgregarSupersacos(dGVSupersacos);
                                if(cLotes.EsExcedente(_LOTE_ID,Convert.ToString(dGVSupersacos["Producto", _fila].Value) , _cantidad))
                                {
                                    //Actualizar el estatus del supersaco como excedente
                                    string ss_id = Convert.ToString(dGVSupersacos["SupersacoID", e.RowIndex].Value);
                                    cLotes.ActualizarEstatusExcedente(ss_id);
                                }
                                    
                                dGVSupersacos[_columna, _fila].Value = "Agregado";
                                ContarKilosGrid();
                                //dGVSupersacos.Rows[e.RowIndex].Cells[0].Selected = true;
                                //dGVSupersacos.BeginEdit(true);
                                dGVSupersacos.CurrentCell = dGVSupersacos.Rows[dGVSupersacos.Rows.Count - 1].Cells[0];
                                dGVSupersacos.BeginEdit(true);
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                            MessageBox.Show("Favor de agregar la serie del supersaco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Peso incorrecto del Supersaco #" + dGVSupersacos["Numero", _fila].Value.ToString() + " del producto:\"" + dGVSupersacos["Producto", _fila].Value.ToString() + "\"", "Peso Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("No se puede agragar un supersaco cuyo peso no ha sido capturado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (_contenido == "Capturar...")
            {
                if (_basculaConectada == true)
                {
                    //dGVSupersacos["Peso", e.RowIndex].Value = InvertirCadena(_PuertoSerie.DatoRecibido());
                    string aux = ValidarDigitos(_PuertoSerie.DatoRecibido());
                    decimal valor;
                    if (Decimal.TryParse(aux, out valor))
                    {
                        if (valor > 10)
                        {
                            dGVSupersacos["Peso", e.RowIndex].Value = valor;
                            dGVSupersacos["CapturarPeso", e.RowIndex].Value = "Capturado";
                        }
                    }
                    else
                        MessageBox.Show("Error al leer la báscula, favor de reintentar", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("No se puede realizar la operacion la báscula no esta conectada","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (_contenido == "Capturado")
            {
                _respuesta = MessageBox.Show("Peso ya capturado, ¿Desea recapturarlo?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_respuesta == DialogResult.Yes)
                {
                    string aux = Convert.ToString(dGVSupersacos["Agregar", e.RowIndex].Value);
                    if (aux == "Agregado")
                        MessageBox.Show("No se puede recapturar un producto agregado", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (aux == "Agregar")
                        dGVSupersacos["CapturarPeso", e.RowIndex].Value = "Capturar...";

                }
            }
            else if (_contenido == "Imprimir")
            {
                //_respuesta = MessageBox.Show("Peso ya capturado, ¿Desea recapturarlo?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                _respuesta = MessageBox.Show("¿Desea imprimir la etiqueta del supersaco seleccionado?", "información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (_respuesta == DialogResult.Yes)
                {
                    //DialogResult _resp = MessageBox.Show("Autorizar usuario", "Autorizacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (_resp == DialogResult.Yes)
                    //{
                    //FAutorizacion autorizacion = new FAutorizacion();
                    //autorizacion.TipoAutorizacion = "EMPLAYE";
                    //// fAutorizacion.usuario = Usuario;

                    //if (autorizacion.ShowDialog() == DialogResult.Cancel)
                    //{

                    //    if (autorizacion.TIPO_USU_ID == 9) // Usuario Emplayador
                    //    {
                    FAutorizacion autorizacion = new FAutorizacion();
                    autorizacion.TipoAutorizacion = "BODEGUERO";
                    // fAutorizacion.usuario = Usuario;

                    if (autorizacion.ShowDialog() == DialogResult.Cancel)
                    {

                        if (autorizacion.TIPO_USU_ID == 3) // Usuario BOdeguero
                        {
                            string aux = Convert.ToString(dGVSupersacos["Agregar", e.RowIndex].Value);
                            if (aux == "Agregado")
                            {
                                double _cantidad = Convert.ToDouble(dGVSupersacos["Peso", e.RowIndex].Value);
                                if (_cantidad > 0)
                                {
                                    string _supersacoID = Convert.ToString(dGVSupersacos["SupersacoID", e.RowIndex].Value);
                                    string _productoNombre = Convert.ToString(dGVSupersacos["Producto", e.RowIndex].Value);
                                    string _usuarioCreador = autorizacion.usuario.USUARIO;
                                    string _usuarioCreador_ID = autorizacion.USU_ID.ToString();
                                    string _folioMSP = _LOTE_ID;
                                    string _reimpresion = "",_excedente="";

                                    // string _centroCompra = Convert.ToString(dGVSupersacos["Agregar", e.RowIndex].Value);
                                    //DateTime _fechaCreacion = DateTime.Today.;
                                    //Invocar form para imprimir ticket
                                    //FCodigoBarras _cb = new FCodigoBarras();
                                    if (_supersacoID.Length > 0)
                                    {

                                    CLotesMSP cLotes = new CLotesMSP();
                                    cLotes.ActualizarEstatusImpresion(_supersacoID);

                                        if (cLotes.EsReimpresion(_supersacoID, autorizacion.USU_ID.ToString()))
                                            _reimpresion = "Reimpresión";
                                        
                                        FCodigoQR _cb = new FCodigoQR();
                                        _cb._supersacoID =_supersacoID;
                                        _cb._productoNombre = _productoNombre;
                                        _cb._usuarioCreador = autorizacion.usuario.USUARIO;
                                        _cb._folioMSP = _folioMSP;
                                        _cb._reimpresion = _reimpresion;
                                        //_cb._fechaCreacion = _fechaCreacion;
                                        // _cb._centroCompra = _centroCompra;
                                        _cb._cantidad = _cantidad;
                                        _cb.ShowDialog();
                                    }
                                    else
                                        MessageBox.Show("No se puede imprimir la etiqueta supersaco ID no asignado", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                    MessageBox.Show("Cantidad no especificada", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else if (aux == "Agregar")
                                MessageBox.Show("No se puede imprimir un supersaco que no haya sido guardado en base de datos", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                        //    }
                        //}
                   // }
                }
            }
        }
        private string ValidarDigitos(string CADENA)
        {
            string resultado = "";
            try
            {
                for(int i=0;i <CADENA.Length;i++)
                {
                    if (char.IsDigit(CADENA[i]) || CADENA[i] == '.')
                        resultado += CADENA[i];
                }

            }catch
            {

            }
            return resultado;
        }
        private void FRecEmb_Load(object sender, EventArgs e)
        {
            //LimpiarForma();
            //CambiarSeleccionEmbarque();
            dGVSupersacos.Visible = false;
            CargarAlmacenes();
        }
        private string InvertirCadena(string cadena)
        {
            string aux = "";
            for (int i = cadena.Length - 1; i >= 0; i--)
            {
                aux += cadena[i];
            }
            return aux;
        }       

        double _PesoTara = 0, _PesoTara2 = 0;
        bool _basculaConectada = false, _activa = false;

        private void dGVEmbarques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_activa == false)
            {
                CambiarSeleccionEmbarque();
                dGVSupersacos.Visible = false;
                ContarKilosGrid();
            }
            else
                MessageBox.Show("No se puede cambiar a otro embarque mientras un embarque siga activo", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bCerrarRecepcion_Click(object sender, EventArgs e)
        {
            string MateriaPrima = Convert.ToString(dGVSupersacos["Producto", 0].Value);
            DialogResult _resp = MessageBox.Show("¿Desea cerrar el embarque " + _embarqueID + "\n\rDel producto:" + MateriaPrima + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (_resp == DialogResult.Yes)
            {
                ActualizarEstatusRecepcion(_embarqueID);
                LimpiarForma();
                dGVSupersacos.Visible = false;
                tBEmbarqueRecibir.Enabled = true;
                _activa = false;
                bActivar.Enabled = true;
            }
        }
        private void ActualizarEstatusRecepcion(string EmbarqueID)
        {
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                SqlCommand cmd;
                string _estatus = "23", consulta = "UPDATE [dbo].[EMBARQUE] " +
                    " SET [ESTATUS_ID] = " + _estatus +
                    " WHERE [EMBARQUE_ID] = " + EmbarqueID;
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                if (ConexionSQL.IsConected())
                    ConexionSQL.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void bActivar_Click(object sender, EventArgs e)
        {
            /*habilitar el datagrid para la captura de los supersacos*/
            if (bActivar.Text == "Activar")
            {
                string _embarque = tBEmbarqueRecibir.Text.Trim();
                if(_embarque.Length==0)
                {
                    MessageBox.Show("Favor de especificar el embarque","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                cBAlmacen.Enabled = false;
                    //LlenarTabla
                    CargarEmbarques();
                    //string _aux = Convert.ToString(_EMBARQUE.Rows[0]["EMBARQUE_ID"]);
                    if (_EMBARQUE.Rows.Count > 0)
                    {
                        dGVSupersacos.Visible = true;
                        tBEmbarqueRecibir.Enabled = false;
                        _activa = true;
                        //bActivar.Enabled = false;
                        tBEmbarqueRecibir.Enabled = false;
                        bActivar.Text = "Desactivar";
                        _embarqueID = tBEmbarqueRecibir.Text.Trim();
                        _LOTE_ID = Convert.ToString(_EMBARQUE.Rows[0]["FOLIO_MSP"]);
                        CargarProductos();
                        CargarSupersacosEmbarque(tBEmbarqueRecibir.Text.Trim());
                        if (dGVSupersacos.Rows.Count == 0)
                        {
                            dGVSupersacos.Rows.Add();
                            dGVSupersacos["Numero", 0].Value = "1";
                            dGVSupersacos["CapturarPeso", 0].Value = "Capturar...";
                            dGVSupersacos["Agregar", 0].Value = "Agregar";
                            dGVSupersacos["Imprimir", 0].Value = "Imprimir";
                        }
                    }
                    else
                        mensaje.Error("El embarque "+ _embarque+" ya fue completado","Error");
                   // cBAlmacen.SelectedIndex = 1;
                    DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                    _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString();
                    _Almacen_desc= _dRVAlmacen.Row.ItemArray[1].ToString();
                }                
            }
            else if (bActivar.Text == "Desactivar")
            {
                dGVSupersacos.Visible = false;
                tBEmbarqueRecibir.Enabled = true;
                _activa = false;
                //bActivar.Enabled = true;
                tBEmbarqueRecibir.Enabled = true;
                dGVSupersacos.DataSource = null;
                bActivar.Text = "Activar";
                cBAlmacen.Enabled = true;
            }
        }
        private bool ExisteEmbarque(string EmbarqueID)
        {
            bool _existe = false;
            string msg = "",consulta="";
            try
            {
                consulta = "select * from EMBARQUE where EMBARQUE_ID="+EmbarqueID;
                ConexionSql ConexionSQL = new ConexionSql();
                SqlCommand cmd;
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            if (msg.Length > 0)
                mensaje.Error(msg,"Error");
            return _existe;
        }
        PuertosSerie _PuertoSerie = new PuertosSerie();
        public FRecEmb()
        {
            InitializeComponent();
        }

        private void conectarBásculaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pConectarPuerto.Visible=true;
        }

        private void bBuscarPuertos_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Báscula conectada", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pConectarPuerto.Visible = false;
                        conectarBásculaToolStripMenuItem.Visible = false;
                        desconectarBásculaToolStripMenuItem.Visible = true;
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
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void desconectarBásculaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._basculaConectada == true)
                {
                    if (_PuertoSerie.EstaConectado)
                    {
                        _PuertoSerie.DesconectarPuertoSerie();
                        MessageBox.Show("Báscula desconectada", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        desconectarBásculaToolStripMenuItem.Visible = false;
                        conectarBásculaToolStripMenuItem.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CargarSupersacosEmbarque(string EmbarqueID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "select distinct S.SUPERSACO_ID,P.PRODUCTO_DESCRIPCION,S.SUPERSACO_CANTIDAD,S.PRODUCTO_ID,E.EMBARQUE_ID, " +
                    " E.ESTATUS_ID,LR.LOTE_RECEPCION_SERIE,LR.RECEPCION_ID,SUPERSACO_FECHA from SUPERSACO AS S " +
                    " INNER JOIN PRODUCTO AS P ON P.PRODUCTO_ID = S.PRODUCTO_ID or s.PRODUCTO_ID=p.PRODUCTO_MSP_ID" +
                    " INNER JOIN EMBARQUE AS E ON E.EMBARQUE_ID=S.EMBARQUE_ID " +
                    " inner join LOTE_RECEPCION as LR on (LR.TIPO_DOCUMENTO=e.EMBARQUE_TIPO and LR.DOCUMENTO_ID=e.EMBARQUE_DOCUMENTO_ID)" +
                    " WHERE S.EMBARQUE_ID=" + EmbarqueID + " and (LR.LOTE_RECEPCION_SERIE is not null and len(LR.LOTE_RECEPCION_SERIE)>0)" +
                    " order by SUPERSACO_FECHA asc";
                SqlCommand cmd;
                cn.ConectarSQLServer();
                cmd = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    dGVSupersacos.Rows.Clear();
                    while (reader.Read())
                    {
                        dGVSupersacos.Rows.Add();
                        dGVSupersacos["Numero", i].Value = (i + 1).ToString();
                        dGVSupersacos["Peso", i].Value = reader["SUPERSACO_CANTIDAD"].ToString();
                        dGVSupersacos["Producto", i].Value = reader["PRODUCTO_DESCRIPCION"].ToString();
                        dGVSupersacos["ProductoID", i].Value = reader["PRODUCTO_ID"].ToString();
                        dGVSupersacos["CapturarPeso", i].Value = "Capturado";
                        dGVSupersacos["Agregar", i].Value = "Agregado";
                        dGVSupersacos["Imprimir", i].Value = "Imprimir";
                        dGVSupersacos["Imprimir", i].ReadOnly = false;
                        dGVSupersacos["EmbarqueID", i].Value = reader["EMBARQUE_ID"].ToString();
                        dGVSupersacos["Estatus", i].Value = reader["ESTATUS_ID"].ToString();
                        dGVSupersacos["SupersacoID", i].Value = reader["SUPERSACO_ID"].ToString();
                        tBSerie.Text = reader["LOTE_RECEPCION_SERIE"].ToString();
                        if (i == 0)
                        {
                            tBSerie.Enabled = false;
                            _RecepcionID = reader["RECEPCION_ID"].ToString();
                        }
                        i++;
                    }
                }
                nUDSupersacos.Minimum = i;
                reader.Close();
                cmd.Cancel();
                cn.Desconectar();
              
            }
            catch (Exception Ex)
            {
                if (cn.IsConected())
                    cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AgregarSupersacos(DataGridView DGVSupersacos)
        {
            string FECHA = "", _inventarioID = "";
            FECHA = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            //_tipoDocumento = dGVEmbarques["TipoDocumento", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
            _tipoDocumento = _EMBARQUE.Rows[0]["EMBARQUE_TIPO"].ToString();
            _Documento = _EMBARQUE.Rows[0]["EMBARQUE_DOCUMENTO_ID"].ToString(); //dGVEmbarques["Documento", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
            if (_Serie.Length == 0)
                _Serie = tBSerie.Text;
            if(_RecepcionID.Length==0)
            {
               if( InsertarPeticion(_embarqueID,_tipoDocumento,_Documento,DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), ObtenerLote(_embarqueID),"0"))
                {
                    _RecepcionID = ObtenerRecepcion(_embarqueID, _tipoDocumento, _Documento);
                }
            }
            _loteID = ObtenerLote(_Serie, _tipoDocumento, _Documento, _RecepcionID);
            DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
            _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString();
            _Almacen_desc = _dRVAlmacen.Row.ItemArray[1].ToString();

            _inventarioID = ObtieneInventarioID(_AlmacenID, _Almacen_desc);
            InsertarSupersacos(ObtieneInventarioSuperSacos(_inventarioID), FECHA, DGVSupersacos);
            InsertarInventarioFrimex(_inventarioID, FECHA, DGVSupersacos);
            CargarSupersacosEmbarque(_embarqueID);
        }
        private string ObtenerRecepcion(string EmbarqueID, string TipoDocumento, string DocumentoID)
        {
            // 
            string _lote = "";
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                SqlCommand cmd;
                SqlDataReader reader;
                string consulta = "select top 1 * from RECEPCION" +
                    " where " +
                    " EMBARQUE_ID = "+EmbarqueID+" and TIPO_DOCUMENTO = '"+TipoDocumento+"' AND DOCUMENTO_ID = " + DocumentoID;
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    _lote = reader["RECEPCION_ID"].ToString();
                cmd.Cancel();
                reader.Close();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _lote;
        }
        private string ObtenerLote(string EmbarqueId)
        {
            string _lote = "";
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                SqlCommand cmd;
                SqlDataReader reader;
                string consulta = "select LOTE_EMBARQUE_ID from LOTE_EMBARQUE as LE " +
                    " inner join EMBARQUE as E on E.EMBARQUE_ID = LE.EMBARQUE_ID " +
                    " where E.EMBARQUE_ID =" + EmbarqueId;
                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    _lote = reader["LOTE_EMBARQUE_ID"].ToString();
                cmd.Cancel();
                reader.Close();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _lote;
        }
        private void FInventarioFrimex_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PuertoSerie.EstaConectado)
            {
                MessageBox.Show("Favor de desconectar el puerto serie", "Informacion",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void tBSerie_TextChanged(object sender, EventArgs e)
        {
            _Serie = tBSerie.Text;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private double ContarKilosGrid()
        {
            double TotalKg = 0;
            try
            {
                for (int i = 0; i < dGVSupersacos.RowCount; i++)
                {
                    if (Convert.ToString(dGVSupersacos["Agregar", i].Value) == "Agregado")
                    {
                        TotalKg += Convert.ToDouble(dGVSupersacos["Peso", i].Value);
                    }
                }
                //lCantidadRecibida.Text = "Cantidad recibida hasta el momento: " + TotalKg.ToString("N2") + "Kg";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return TotalKg;
        }
        private void GuardarDatos()
        {
            string FECHA = "", _inventarioID = "";
            FECHA = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            _loteID = ObtenerLote(_Serie, _tipoDocumento, _Documento, _RecepcionID);
            _inventarioID = ObtieneInventarioID(_AlmacenID, _Almacen_desc);
            InsertarSupersacos(ObtieneInventarioSuperSacos(_inventarioID), FECHA, dGVSupersacos);
            InsertarInventarioFrimex(_inventarioID, FECHA, dGVSupersacos);
            MessageBox.Show("Alta de almacen excitosa", "Ingormacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /*DialogResult _pregunta = MessageBox.Show("¿Desea realizar más operaciones?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (_pregunta == DialogResult.No)
                this.Close();*/
        }
        private double CalculaMerma(double peso1, double peso2)
        {
            double _merma = 0;
            _merma = (peso2 * 100) / peso1;
            _merma = 100 - _merma;
            return _merma;
        }
        private void InsertarSupersacos(string INVENTARIO_SUPERSACO_ID, string FECHA, DataGridView DGVSupersacos)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                //Variables
                string consulta = "", PRODUCTO_ID = "", SUPERSACO_CANTIDAD = "", SUPERSACO_ID = "", SUPERSACO_ESTATUS = "A";
                SqlCommand cmd;
                //Insertar superSacos
                for (int i = 0; i < DGVSupersacos.Rows.Count; i++)
                {
                    if (DGVSupersacos["SupersacoID", i].Value == null)
                    {
                        if (DGVSupersacos["ProductoID", i].Value != null)
                        {
                            PRODUCTO_ID = DGVSupersacos["ProductoID", i].Value.ToString();
                            SUPERSACO_CANTIDAD = DGVSupersacos["Peso", i].Value.ToString();
                            consulta = "INSERT INTO [dbo].[SUPERSACO] " +
                                " ([SUPERSACO_ID]" +
                                " ,[INVENTARIO_SUPERSACO_ID] " +
                                " ,[PRODUCTO_ID] " +
                                " ,[SUPERSACO_CANTIDAD] " +
                                " ,[SUPERSACO_FECHA] " +
                                " ,[LOTE_ID] " +
                                " ,[SUPERSACO_ESTATUS]" +
                                " ,[EMBARQUE_ID]" +
                                " ,[SUPERSACO_IMPRESO]) " +
                                " VALUES " +
                                " ( " + cn.ObtenerSigID("SUPERSACO").ToString() +
                                " ," + INVENTARIO_SUPERSACO_ID +
                                " ," + PRODUCTO_ID +
                                " ," + SUPERSACO_CANTIDAD +
                                ", @Fecha "  +
                                "," + _loteID +
                                ",'" + SUPERSACO_ESTATUS +
                                "'," + _embarqueID + "" +
                                ",'N')";
                            cn.ConectarSQLServer();
                            cmd = new SqlCommand(consulta, cn.SC);
                            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = FECHA;
                            cmd.ExecuteNonQuery();
                            //buscar el Id de supersaco
                            consulta = "SELECT SUPERSACO_ID FROM  [dbo].[SUPERSACO] WHERE " +
                                " INVENTARIO_SUPERSACO_ID= " + INVENTARIO_SUPERSACO_ID +
                                " AND PRODUCTO_ID= " + PRODUCTO_ID +
                                " AND SUPERSACO_CANTIDAD= " + SUPERSACO_CANTIDAD +
                                " AND SUPERSACO_FECHA= @Fecha " +
                                " AND LOTE_ID= " + _loteID +
                                " AND SUPERSACO_ESTATUS= '" + SUPERSACO_ESTATUS + "'";
                            cmd = new SqlCommand(consulta, cn.SC);
                            cmd.Parameters.Add("@Fecha",SqlDbType.Date).Value = FECHA;
                            SqlCommand sc = new SqlCommand(consulta, cn.SC);
                            sc.Parameters.Add("@Fecha",SqlDbType.Date).Value = FECHA;
                            SqlDataReader reader = sc.ExecuteReader();
                            while (reader.Read())
                            {
                                SUPERSACO_ID = reader["SUPERSACO_ID"].ToString();
                            }

                            //Crear Tabla historico Supersacos en SQLServer
                            consulta = "INSERT INTO [dbo].[HIST_SUPSAC] " +
                                " ([HIST_SUPERSACO_ID]" +
                                " ,[INVENTARIO_SUPERSACO_ID] " +
                                " ,[SUPERSACO_ID] " +
                                " ,[PRODUCTO_ID] " +
                                " ,[SUPERSACO_CANTIDAD] " +
                                " ,[SUPERSACO_FECHA] " +
                                " ,[SUPERSACO_ESTATUS] " +
                                " ,[HIST_SUPERSACO_USUARIO_MODIF] " +
                                " ,[HIST_SUPERSACO_FECHA_MODIF]) " +
                                " VALUES " +
                                " ( " + cn.ObtenerSigID().ToString() +
                                " ," + INVENTARIO_SUPERSACO_ID +
                                " , " + SUPERSACO_ID +
                                " , " + PRODUCTO_ID +
                                " , " + SUPERSACO_CANTIDAD +
                                " , @Fecha " +
                                ", '" + SUPERSACO_ESTATUS +
                                "' ," + _Usuario.USUARIOID +
                                " , @Fecha2 " +
                                ") ";
                            cmd = new SqlCommand(consulta, cn.SC);
                            cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = FECHA;
                            cmd.Parameters.Add("@Fecha2", SqlDbType.Date).Value = FECHA;
                            cmd.ExecuteNonQuery();
                            cn.ConectarSQLServer();
                            cmd.Dispose();
                            cn.Desconectar();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InsertarInventarioFrimex(string InventarioFrimexID, string Fecha, DataGridView DGVSupersacos)
        {

            List<string> _Productos = new List<string>();
            double _cantidad = 0, _cantidad2 = 0;
            //_ProductoID= dGVSupersacos["ProductoID", 0].Value.ToString();
            for (int i = 0; i < DGVSupersacos.RowCount; i++)
            {
                if (DGVSupersacos["ProductoID", i].Value != null)
                {
                    string aux = DGVSupersacos["ProductoID", i].Value.ToString();
                    if (_Productos.Count == 0)
                        _Productos.Add(aux);
                    else if (_Productos.Count > 0)
                    {
                        for (int j = 0; j < _Productos.Count; j++)
                        {
                            if (_Productos[j] != aux)
                                _Productos.Add(aux);
                        }
                    }
                }
            }
            if (_Productos.Count == 1)
            {
                for (int i = 0; i < DGVSupersacos.Rows.Count; i++)
                    _cantidad += Convert.ToDouble(DGVSupersacos["Peso", i].Value);
            }
            else if (_Productos.Count == 2)
            {
                for (int i = 0; i < DGVSupersacos.Rows.Count; i++)
                {
                    string aux = DGVSupersacos["ProductoID", 0].Value.ToString();
                    if (aux == _Productos[0])
                        _cantidad += Convert.ToDouble(DGVSupersacos["Peso", i].Value);
                    else if (aux == _Productos[1])
                        _cantidad += Convert.ToDouble(DGVSupersacos["Peso", i].Value);
                }
            }
            if (_Productos.Count == 1)
                InsertaInventarioFrimex(InventarioFrimexID, _Productos[0], _cantidad.ToString(), _loteID);
            else if (_Productos.Count == 2)
            {
                InsertaInventarioFrimex(InventarioFrimexID, _Productos[0], _cantidad.ToString(), _loteID);
                InsertaInventarioFrimex(InventarioFrimexID, _Productos[1], _cantidad2.ToString(), _loteID);
            }
        }

        private void InsertaInventarioFrimex(string Inventario_Frimex_ID, string Producto_ID, string cantidad, string LoteID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "", _tipoMovimiento = "E";
                double _cantidad = 0;
                bool _existe = false;
                SqlCommand cmd;
                //Buscar Ultimo Lote
                consulta = "select INV_FRIMEX_DET_CANTIDAD from[dbo].[INV_FRIMEX_DET]" +
                     " WHERE INVENTARIO_FRIMEX_ID= " + Inventario_Frimex_ID +
                     " AND PRODUCTO_ID=" + Producto_ID;
                cmd = new SqlCommand(consulta, cn.SC);
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();

                //select* from[dbo].[INV_FRIMEX_DET_HIST]
                while (reader.Read())
                {
                    //_RecepcionID = Convert.ToInt32();
                    _existe = true;
                    _cantidad = Convert.ToDouble(reader["INV_FRIMEX_DET_CANTIDAD"]);
                }
                //Si existe lo actualiza si no lo inserta
                if (_existe == true)
                {
                    consulta = "UPDATE[dbo].[INV_FRIMEX_DET] " +
                        " SET [INV_FRIMEX_DET_CANTIDAD] = " + (Convert.ToDouble(cantidad) + _cantidad).ToString() +
                        " WHERE INVENTARIO_FRIMEX_ID= " + Inventario_Frimex_ID +
                        " AND PRODUCTO_ID=" + Producto_ID;
                }
                else
                {
                    consulta = "INSERT INTO[dbo].[INV_FRIMEX_DET] " +
                        " ([INV_FRIMEX_DET_ID]" +
                        " ,[INVENTARIO_FRIMEX_ID] " +
                        " ,[PRODUCTO_ID] " +
                        " ,[INV_FRIMEX_DET_CANTIDAD]) " +
                        " VALUES " +
                        " (" + cn.ObtenerSigID().ToString() +
                        " ," + Inventario_Frimex_ID +
                        " ," + Producto_ID +
                        " ," + cantidad +
                        " )";
                }
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.ExecuteNonQuery();
                consulta = "INSERT INTO [dbo].[INV_FRIMEX_DET_HIST] " +
                    " ([INV_FRIMEX_DET_HIST_ID]" +
                    " ,[INVENTARIO_FRIMEX_ID] " +
                    " ,[PRODUCTO_ID] " +
                    " ,[INV_FRIMEX_DET_CANTIDAD] " +
                    " ,[INV_FRIMEX_DET_HIST_MOVIMIENTO] " +
                    " ,[INV_FRIMEX_DET_HIST_FECHA_R] " +
                    " ,[LOTE_ID]) " +
                    " VALUES " +
                    " ( " + cn.ObtenerSigID().ToString() +
                    " , " + Inventario_Frimex_ID +
                    " , " + Producto_ID +
                    " , " + cantidad +
                    " ,'" + _tipoMovimiento + "' " +
                    " ,@Fecha" +
                    " ," + LoteID + ")";
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.Parameters.Add("@Fecha",SqlDbType.Date).Value = DateTime.Now;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ObtieneInventarioID(string Almacen_id, string Almacen_desc)
        {
            string _InvenarioID = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "";
                bool _Existe = false;
                cn.ConectarSQLServer();
                SqlCommand cmd;
                consulta = "select * from INVENTARIO_FRIMEX " +
                    " WHERE INVENTARIO_FRIMEX_ID = " + Almacen_id +
                    " AND ALMACEN_MSP_DESCRIPCION = '" + Almacen_desc + "'";
                cmd = new SqlCommand(consulta, cn.SC);
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _Existe = true;
                }
                if (_Existe == false)
                {
                    consulta = "INSERT INTO [dbo].[INVENTARIO_FRIMEX] " +
                        " ([INVENTARIO_FRIMEX_ID]" +
                        " ,[INVENTARIO_FRIMEX_USUARIO_CREA] " +
                        " ,[INVENTARIO_FRIMEX_FECHA_CREA] " +
                        " ,[ALMACEN_MSP_ID] " +
                        " ,[ALMACEN_MSP_DESCRIPCION]) " +
                        " VALUES " +
                        " ( " + cn.ObtenerSigID() +
                        " , " + _Usuario.USUARIOID.ToString() +
                        " , @Fecha " +
                        " ," + Almacen_id +
                        " ,'" + Almacen_desc +
                        "') ";
                    cmd = new SqlCommand(consulta, cn.SC);
                    cmd.Parameters.Add("@Fecha",SqlDbType.Date).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();
                }
                consulta = "select * from INVENTARIO_FRIMEX " +
                   " WHERE INVENTARIO_FRIMEX_ID = " + Almacen_id +
                   " AND ALMACEN_MSP_DESCRIPCION = '" + Almacen_desc + "'";
                cmd = new SqlCommand(consulta, cn.SC);
                sc = new SqlCommand(consulta, cn.SC);
                reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _InvenarioID = reader["INVENTARIO_FRIMEX_ID"].ToString();
                }
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                _InvenarioID = "";
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _InvenarioID;
        }

        private string ObtieneInventarioSuperSacos(string InventarioFrimexId)
        {
            string _InvsuperSacoID = "", _Fecha = "";
            ConexionSql cn = new ConexionSql();
            try
            {
                string consulta = "select INVENTARIO_SUPERSACO_ID " +
                    " from inventario_supersaco " +
                    " WHERE INVENTARIO_FRIMEX_ID = " + InventarioFrimexId;
                bool _Existe = false;
                _Fecha = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");
                cn.ConectarSQLServer();
                SqlCommand cmd;
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _Existe = true;
                }
                if (_Existe == false)
                {
                    consulta = "INSERT INTO [dbo].[INVENTARIO_SUPERSACO] " +
                    " ([INVENTARIO_SUPERSACO_ID]" +
                    " ,[INVENTARIO_SUPERSACO_FECHA_CREACION] " +
                    " ,[INVENTARIO_SUPERSACO_USUARIO_CREACION] " +
                    " ,[INVENTARIO_FRIMEX_ID]) " +
                    " VALUES " +
                    " ( " + cn.ObtenerSigID().ToString() +
                    " , @Fecha "  +
                    " ," + _Usuario.USUARIOID.ToString() +
                    " , " + InventarioFrimexId + ")";
                }
                reader.Close();
                cmd = new SqlCommand(consulta, cn.SC);
                if (!_Existe)
                    cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = _Fecha;
                cmd.ExecuteNonQuery();
                consulta = "select INVENTARIO_SUPERSACO_ID " +
                   " from inventario_supersaco " +
                   " WHERE INVENTARIO_FRIMEX_ID = " + InventarioFrimexId;
                cmd = new SqlCommand(consulta, cn.SC);

                reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _InvsuperSacoID = reader["INVENTARIO_SUPERSACO_ID"].ToString();
                }
                cmd.Dispose();
                sc.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                cn.Desconectar();
                _InvsuperSacoID = "";
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _InvsuperSacoID;
        }

        private void nUDSupersacos_ValueChanged(object sender, EventArgs e)
        {
            int _filas = Convert.ToInt32(nUDSupersacos.Value);
            _filas -= dGVSupersacos.Rows.Count;
            if (_filas >= 0)
                for (int i = dGVSupersacos.Rows.Count; i < Convert.ToInt32(nUDSupersacos.Value); i++)
                {
                    dGVSupersacos.Rows.Add();
                    dGVSupersacos["Numero", i].Value = (i + 1).ToString();
                    dGVSupersacos["CapturarPeso", i].Value = "Capturar...";
                    dGVSupersacos["Agregar", i].Value = "Agregar";
                    dGVSupersacos["Imprimir", i].Value = "Imprimir";
                }
            else
            {
                _filas *= -1;
                for (int i = 0; i < _filas; i++)
                    dGVSupersacos.Rows.Remove(dGVSupersacos.Rows[dGVSupersacos.Rows.Count - 1]);
            }
            dGVSupersacos.CurrentCell = dGVSupersacos.Rows[dGVSupersacos.Rows.Count-1].Cells[0];
            dGVSupersacos.BeginEdit(true);
        }

        private void FRecEmb_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PuertoSerie.EstaConectado)
            {
                MessageBox.Show("Favor de desconectar el puerto serie", "Informacion",MessageBoxButtons.OK,MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }
        private void CambiarSeleccionEmbarque()
        {
            try
            {
                dGVSupersacos.Rows.Clear();
                //buscar superSacos ppertenecientes al embarque seleccionado que esten en la base de datos y mostrar su informacion
                //BuscarSupersacosCapturados(dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString());
                lEmbarqueSeleccionado.Text = "(Embarque seleccionado: \"" + _EMBARQUE.Rows[0]["EMBARQUE_ID"].ToString()/*dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString()*/ + "\")";
                CargarSupersacosEmbarque(_EMBARQUE.Rows[0]["EMBARQUE_ID"].ToString()/*dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString()*/);
                dGVSupersacos.Rows.Add();
                int fila = dGVSupersacos.Rows.Count - 1;
                dGVSupersacos["Numero", fila].Value = dGVSupersacos.RowCount.ToString();//dGVSupersacos["Numero", fila].Value==null? "1":(Convert.ToInt32(dGVSupersacos["Numero", fila].Value)+1).ToString();
                dGVSupersacos["CapturarPeso", fila].Value = "Capturar...";
                dGVSupersacos["Agregar", fila].Value = "Agregar";
                dGVSupersacos["Imprimir", fila].Value = "Imprimir";
                //tBTaraLlena.Text = _taraLlena;
                //tBTaraLlena.Text = "";
                //tBTaraVacia.Text = "";
                //tBTaraVacia2.Text = "";
                //CargarAlmacenes();
                //PesosTara();
                this._Documento = _EMBARQUE.Rows[0]["Documento"].ToString(); //dGVEmbarques["Documento", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
                this._tipoDocumento = _EMBARQUE.Rows[0]["TipoDocumento"].ToString(); //dGVEmbarques["TipoDocumento", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
                this._embarqueID = _EMBARQUE.Rows[0]["EMBARQUE_ID"].ToString();//dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
                this._RecepcionID = _EMBARQUE.Rows[0]["RECEPCION_ID"].ToString();//dGVEmbarques["RecepcionID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
                CargarProductos();
            }
            catch
            {

            }
        }

        private void tBEmbarqueRecibir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar==(int)Keys.Enter)
            {
                if (bActivar.Text == "Activar")
                {
                    string _embarque = tBEmbarqueRecibir.Text.Trim();
                    if (_embarque.Length == 0)
                    {
                        MessageBox.Show("Favor de especificar el embarque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //LlenarTabla
                        CargarEmbarques();
                        //string _aux = Convert.ToString(_EMBARQUE.Rows[0]["EMBARQUE_ID"]);
                        if (_EMBARQUE.Rows.Count > 0)
                        {
                            dGVSupersacos.Visible = true;
                            tBEmbarqueRecibir.Enabled = false;
                            _activa = true;
                            //bActivar.Enabled = false;
                            tBEmbarqueRecibir.Enabled = false;
                            bActivar.Text = "Desactivar";
                            _LOTE_ID = Convert.ToString(_EMBARQUE.Rows[0]["FOLIO_MSP"]);
                            _embarqueID = tBEmbarqueRecibir.Text.Trim();
                            CargarProductos();
                            CargarSupersacosEmbarque(tBEmbarqueRecibir.Text.Trim());
                            if (dGVSupersacos.Rows.Count == 0)
                            {
                                dGVSupersacos.Rows.Add();
                                dGVSupersacos["Numero", 0].Value = "1";
                                dGVSupersacos["CapturarPeso", 0].Value = "Capturar...";
                                dGVSupersacos["Agregar", 0].Value = "Agregar";
                                dGVSupersacos["Imprimir", 0].Value = "Imprimir";
                            }
                        }
                        else
                            mensaje.Error("El embarque " + _embarqueID + " ya fue completado", "Error");
                        DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                        _AlmacenID = _dRVAlmacen.Row.ItemArray[0].ToString();
                        _Almacen_desc = _dRVAlmacen.Row.ItemArray[1].ToString();
                    }
                }
                else if (bActivar.Text == "Desactivar")
                {
                    dGVSupersacos.Visible = false;
                    tBEmbarqueRecibir.Enabled = true;
                    _activa = false;
                    //bActivar.Enabled = true;
                    tBEmbarqueRecibir.Enabled = true;
                    dGVSupersacos.DataSource = null;
                    bActivar.Text = "Activar";
                }
            }
        }

        private void LimpiarForma()
        {
            try
            {
                //DatosPrueba();
                //CargarEmbarques();
                CargarAlmacenes();
                tBEmbarqueRecibir.Text = "";
                bActivar.Text = "Activar";
                bActivar.Enabled = true;
                cBProducto.DataSource = null;
                tBSerie.Text = "";
                nUDSupersacos.Minimum = 1;
                nUDSupersacos.Value = 1;
                //int fila = dGVEmbarques.CurrentCell != null ? dGVEmbarques.CurrentCell.RowIndex : 0;
                //if (fila > 0)
                //{
                //    _embarqueID = _EMBARQUE.Rows[0]["EMBARQUE_ID"].ToString();//dGVEmbarques["EmbID", fila].Value != null ? dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString() : "";
                //CargarProductos();
                //    CargarSupersacosEmbarque(_embarqueID);
                //}
                dGVSupersacos.Rows.Add();
                dGVSupersacos["Numero", 0].Value = "1";
                dGVSupersacos["CapturarPeso", 0].Value = "Capturar...";
                dGVSupersacos["Agregar", 0].Value = "Agregar";
                dGVSupersacos["Imprimir", 0].Value = "Imprimir";
                //tBTaraLlena.Text = "";
                //tBTaraVacia.Text = "";
                //tBTaraVacia2.Text = "";
                //tBTaraLlena.Text = _taraLlena;
                // PesosTara();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarEmbarque()
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                //Variables
                cn.ConectarSQLServer();
                string consulta = "", _producto = "";
                SqlCommand cmd;
                SqlDataAdapter DA = new SqlDataAdapter();
                DataTable _Productos = new DataTable();
                consulta = "select EMBARQUE_ID, EMBARQUE_PESO_NETO, EMBARQUE_BOLETA1, EMBARQUE_BOLETA2, EMBARQUE_PESO1, " +
                       " EMBARQUE_PESO2, EMBARQUE_CONDUCTORP1, EMBARQUE_CONDUCTORP2, EMBARQUE_CHOFER, " +
                       " EMBARQUE_PLACA, EMBARQUE_FECHA, EMBARQUE_SELLOS, ESTATUS_ID,PRODUCTO_ID,BODEGA_ID, " +
                       " EMBARQUE_TIPO, EMBARQUE_DOCUMENTO_ID, EMBARQUE_NO_SACOS, " +//Nuevos
                       " EMBARQUE_PRODUCTOADICIONAL, EMBARQUE_BOLETA3, EMBARQUE_PESO3, EMBARQUE_CONDUCTORP3, " +//Nuevos
                       " PRODUCTO_ID2, BODEGA_ID2, EMBARQUE_NO_SACOS2" +//Nuevos
                       " from EMBARQUE ";// +
                                         //" WHERE EMBARQUE_ID=" + _embarqueID;
                cmd = new SqlCommand(consulta, cn.SC);
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _producto = reader["PRODUCTO_ID"].ToString();
                    string _2PRoductos = reader["EMBARQUE_PRODUCTOADICIONAL"] != null ? reader["EMBARQUE_PRODUCTOADICIONAL"].ToString() : "0";
                    //chB2Productos.Checked = _2PRoductos == "1" ? true : false;
                    //if (chB2Productos.Checked == true)
                    //    _producto += "," + reader["PRODUCTO_ID2"].ToString();
                }

                consulta = "select PRODUCTO_ID,PRODUCTO_MSP_ID,PRODUCTO_DESCRIPCION " +
                    " from PRODUCTO " +
                    " WHERE PRODUCTO_ID IN(" + _producto + ")";
                cmd = new SqlCommand(consulta, cn.SC);
                DA = new SqlDataAdapter(consulta, cn.SC);
                DA.Fill(_Productos);
                cBProducto.DataSource = null;
                if (_Productos != null)
                {
                    cBProducto.DataSource = _Productos.DefaultView;
                    cBProducto.ValueMember = "PRODUCTO_ID";
                    cBProducto.DisplayMember = "PRODUCTO_DESCRIPCION";
                }
                cmd.Dispose();
                cn.Desconectar();

            }
            catch (Exception Ex)
            {
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarAlmacenes()
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
                string cadena = "select infr.INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION,iss.INVENTARIO_SUPERSACO_ID " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " inner join USU_ALM as ua on infr.INVENTARIO_FRIMEX_ID = ua.ALMACEN_ID " +
                    " inner join INVENTARIO_SUPERSACO as iss on iss.INVENTARIO_FRIMEX_ID=infr.INVENTARIO_FRIMEX_ID " +
                    " where USU_ALM_ESTATUS = 'A' and ua.USUARIO_ID = "+_Usuario.USUARIOID;
                DataTable Table = new DataTable();
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                cmdm.ExecuteNonQuery();
                // FbDataReader readerm = cmdm.ExecuteReader();
                SqlDataAdapter DA = new SqlDataAdapter(cadena, cn.SC);
                DA.Fill(Table);
                if (Table != null)
                {
                    cBAlmacen.DataSource = Table.DefaultView;
                    cBAlmacen.ValueMember = ".INVENTARIO_FRIMEX_ID";
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

        private void CargarProductos()
        {
            //if (dGVEmbarques.Rows.Count > 0)
            //{
                ConexionSql cn = new ConexionSql();
                try
                {

                    //string _EmbarqueID = dGVEmbarques["EmbID", dGVEmbarques.CurrentCell.RowIndex].Value.ToString();
                    string consulta = " select E.PRODUCTO_ID,p.PRODUCTO_DESCRIPCION from EMBARQUE as E " +
                        " inner join PRODUCTO as P on P.PRODUCTO_ID = E.PRODUCTO_ID " +
                        " where EMBARQUE_ID = " + _embarqueID;
                    cn.ConectarSQLServer();
                    DataTable table = new DataTable("PRODUCTO");
                    table.Columns.Add(new DataColumn("PRODUCTO_ID", typeof(int)));
                    table.Columns.Add(new DataColumn("PRODUCTO_DESCRIPCION", typeof(string)));
                    SqlCommand cmd;
                    SqlDataReader reader;

                    cmd = new SqlCommand(consulta, cn.SC);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        table.Rows.Add(reader["PRODUCTO_ID"].ToString().Trim(), reader["PRODUCTO_DESCRIPCION"]);
                    cmd.Dispose();
                    reader.Close();
                    cn.Desconectar();
                    consulta = "select E.PRODUCTO_ID2,P2.PRODUCTO_DESCRIPCION from EMBARQUE as E " +
                        " inner join PRODUCTO as P2 on P2.PRODUCTO_ID = E.PRODUCTO_ID2 " +
                        "where EMBARQUE_ID = " + _embarqueID;
                    cn.ConectarSQLServer();
                    cmd = new SqlCommand(consulta, cn.SC);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        table.Rows.Add(reader["PRODUCTO_ID"].ToString().Trim(), reader["PRODUCTO_DESCRIPCION"]);
                    cmd.Dispose();
                    reader.Close();
                    cn.Desconectar();
                    if (table.Rows.Count > 0)
                    {
                        cBProducto.DataSource = table.DefaultView;
                        cBProducto.ValueMember = "PRODUCTO_ID";
                        cBProducto.DisplayMember = "PRODUCTO_DESCRIPCION";
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (cn.IsConected() == true)
                        cn.Desconectar();
                }
            //}
        }
        private void BuscarRecepcion(string EMBARQUEID)
        {
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                //buscar recepcion
                string consulta = "select R.RECEPCION_ID from RECEPCION as R " +
                    " where r.EMBARQUE_ID = " + EMBARQUEID,_RECEPCIONID="";
                ConexionSQL.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    _RECEPCIONID = Convert.ToString(reader["RECEPCION_ID"]);
                cmd.Cancel();
                reader.Close();
                ConexionSQL.Desconectar();
                if (_RECEPCIONID.Length == 0)
                {
                    string  tipoDocumento = "", Documento = "";
                    consulta = "select E.EMBARQUE_TIPO,E.EMBARQUE_DOCUMENTO_ID from EMBARQUE as E " +
                        " where E.EMBARQUE_ID = "+EMBARQUEID;
                    ConexionSQL.ConectarSQLServer();
                    cmd = new SqlCommand(consulta, ConexionSQL.SC);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tipoDocumento= Convert.ToString(reader["EMBARQUE_TIPO"]);
                        Documento = Convert.ToString(reader["EMBARQUE_DOCUMENTO_ID"]);
                    }
                    cmd.Cancel();
                    reader.Close();
                    ConexionSQL.Desconectar();
                    if (EMBARQUEID.Length > 0 && tipoDocumento.Length > 0 && Documento.Length > 0)
                        InsertarPeticion(EMBARQUEID, tipoDocumento, Documento, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), ObtenerLote(EMBARQUEID), "0");
                    //if (InsertarPeticion(EMBARQUEID, tipoDocumento, Documento, DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), ObtenerLote(_embarqueID), "0"))
                    //{
                    //    _RecepcionID = ObtenerRecepcion(_embarqueID, _tipoDocumento, _Documento);
                    //}
                }
                //insertar recepcion
                //InsertarPeticion()
            }
            catch
            {

            }
        }
        private void CargarEmbarques()
        {
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                BuscarRecepcion(tBEmbarqueRecibir.Text.Trim());
               
                string consulta = "SELECT E.EMBARQUE_ID, POC.FOLIO AS FOLIO_MSP, E.EMBARQUE_PESO_NETO, E.EMBARQUE_BOLETA1, E.EMBARQUE_BOLETA2, E.EMBARQUE_PESO1, " +
                    " E.EMBARQUE_PESO2, E.EMBARQUE_CONDUCTORP1, E.EMBARQUE_CONDUCTORP2, E.EMBARQUE_CHOFER, E.EMBARQUE_PLACA,  " +
                    " E.EMBARQUE_FECHA, E.EMBARQUE_SELLOS, E.ESTATUS_ID, E.PRODUCTO_ID, E.BODEGA_ID, E.EMBARQUE_TIPO,  " +
                    " E.EMBARQUE_DOCUMENTO_ID, E.EMBARQUE_NO_SACOS, E.EMBARQUE_PRODUCTOADICIONAL, E.EMBARQUE_BOLETA3,  " +
                    " E.EMBARQUE_PESO3, E.EMBARQUE_CONDUCTORP3, E.PRODUCTO_ID2, E.BODEGA_ID2, E.EMBARQUE_NO_SACOS2,R.RECEPCION_ID " +
                    " FROM EMBARQUE AS E " +
                    " INNER JOIN RECEPCION AS R ON R.EMBARQUE_ID=E.EMBARQUE_ID " +
                    " INNER JOIN PETICION_OC AS POC ON POC.PETICION_OC_ID=E.EMBARQUE_DOCUMENTO_ID " +
                    " WHERE E.ESTATUS_ID in (13) AND E.EMBARQUE_ID=" + tBEmbarqueRecibir.Text.Trim();
                    ;
                _EMBARQUE = new DataTable();
                ConexionSQL.ConectarSQLServer();
                DataTable Table = new DataTable();
                SqlDataAdapter DA = new SqlDataAdapter(consulta, ConexionSQL.SC);
                DA.Fill(_EMBARQUE);
                
                //if (Table.Rows.Count > 0)
                //{
                //    int i = 0;
                //    foreach (DataRow fila in Table.Rows)
                //    {
                //        dGVEmbarques.Rows.Add();
                //        string aux = Convert.ToString(fila[22]);
                //        dGVEmbarques["EmbID", i].Value = fila[0].ToString();
                //        dGVEmbarques["ProdID", i].Value = fila[14].ToString();
                //        dGVEmbarques["ProdDesc", i].Value = ObrtenerProductoDescripcion(fila[13].ToString());
                //        if (aux.Length > 0 && aux != "0")
                //            dGVEmbarques["ProdDesc", i].Value += "\n\r" + ObrtenerProductoDescripcion(aux);
                //        if (fila[12].ToString() == "13")
                //            CambiarColor(dGVEmbarques, i, Color.Yellow);
                //        dGVEmbarques["Documento", i].Value = fila[16].ToString();
                //        dGVEmbarques["TipoDocumento", i].Value = fila[15].ToString();
                //        dGVEmbarques["RecepcionID", i].Value = fila[25].ToString();
                //        i++;
                //    }
                //}
                DA.Dispose();
                ConexionSQL.Desconectar();
            }
            catch (Exception EX)
            {
                ConexionSQL.Desconectar();
                MessageBox.Show(EX.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void CambiarColor(DataGridView visor, int fila, Color c)
        {
            visor.Rows[fila].Cells[0].Style.BackColor = c;
            visor.Rows[fila].Cells[1].Style.BackColor = c;
            visor.Rows[fila].Cells[2].Style.BackColor = c;
        }

        private string ObrtenerProductoDescripcion(string ProductoID)
        {
            string _ProductoDesc = "";
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                string consulta = "select PRODUCTO_DESCRIPCION from PRODUCTO where PRODUCTO_ID=" + ProductoID;
                ConexionSQL.ConectarSQLServer();
                SqlCommand cmd = new SqlCommand(consulta, ConexionSQL.SC);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    _ProductoDesc = reader["PRODUCTO_DESCRIPCION"].ToString();
                cmd.Cancel();
                reader.Close();
                ConexionSQL.Desconectar();
            }
            catch (Exception Ex)
            {
                if (ConexionSQL.IsConected())
                    ConexionSQL.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _ProductoDesc;
        }
        private bool InsertarPeticion(string EmbarqueID, string TipoDocumento, string DocumentoID, string Fecha, string LoteID, string CentroCompraID)
        {
            bool _exito = false;
            ConexionSql ConexionSQL = new ConexionSql();
            try
            {
                SqlCommand cmd;
                string consulta = "";

                consulta = "INSERT INTO [dbo].[RECEPCION] " +
                    " ([RECEPCION_ID] " +
                    " ,[EMBARQUE_ID] " +
                    " ,[TIPO_DOCUMENTO] " +
                    " ,[DOCUMENTO_ID] " +
                    " ,[RECEPCION_USUARIO_ID] " +
                    " ,[RECEPCION_FECHA] " +
                    " ,[LOTE_ID] " +
                    " ,[CENTRO_COMPRA_ID]) " +
                    " VALUES " +
                    " ( " + ConexionSQL.ObtenerSigID("RECEPCIONPRODUCTO") +
                    " , " + EmbarqueID +
                    " ,'" + TipoDocumento + "'" +
                    " , " + DocumentoID +
                    " , " + _Usuario.USUARIOID.ToString() +
                    " ,@Fecha" +
                    " , " + LoteID +
                    " , " + CentroCompraID + ")";

                ConexionSQL.ConectarSQLServer();
                cmd = new SqlCommand(consulta, ConexionSQL.SC);
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = Fecha;
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                ConexionSQL.Desconectar();
                _exito = true;
            }
            catch (Exception EX)
            {
                if (ConexionSQL.IsConected())
                    ConexionSQL.Desconectar();
                MessageBox.Show(EX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _exito = false;
            }
            return _exito;
        }
        private string ObtenerLote(string Serie, string TipoDocumento, string Documento, string RecepcionID)
        {
            int _loteId = 0;
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "", FECHA = "";
                SqlCommand cmd;
                FECHA = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                //Buscar Ultimo Lote
                consulta = " select top 1 LOTE_RECEPCION_ID " +
                    " from LOTE_RECEPCION " +
                    " order by LOTE_RECEPCION_ID desc";
                cmd = new SqlCommand(consulta, cn.SC);
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _loteId = Convert.ToInt32(reader["LOTE_RECEPCION_ID"]);
                }
                //Id Consecutivo nuevo Lote
                _loteId += 1;

                // insertar Nuevo Lote
                consulta = " INSERT INTO[dbo].[LOTE_RECEPCION] " +
                    " ([LOTE_RECEPCION_ID]" +
                    " ,[LOTE_RECEPCION_SERIE] " +
                    " ,[LOTE_RECEPCION_FECHA_CREACION] " +
                    " ,[LOTE_RECEPCION_USUARIO_CREA] " +
                    " ,[TIPO_DOCUMENTO] " +
                    " ,[DOCUMENTO_ID] " +
                    " ,[RECEPCION_ID]) " +
                    " VALUES " +
                    " ( " + cn.ObtenerSigID("LOTERECEPCION").ToString() +
                    " ,'" + Serie + "'" +
                    " , @Fecha " +
                    " , " + _Usuario.USUARIOID.ToString() +
                    " ,'" + TipoDocumento + "'" +
                    " , " + Documento +
                    " , " + RecepcionID +
                    " ) ";
                cmd = new SqlCommand(consulta, cn.SC);
                cmd.Parameters.Add("@Fecha", SqlDbType.Date).Value = FECHA;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                _loteId = 0;
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _loteId.ToString();
        }

        private string CalcularRecepcion()
        {
            int _RecepcionID = 0;
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "", FECHA = "";
                SqlCommand cmd;
                FECHA = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                //Buscar Ultimo Lote
                consulta = " select top 1 RECEPCION_ID " +
                    " from RECEPCION " +
                    " order by RECEPCION_ID desc";
                cmd = new SqlCommand(consulta, cn.SC);
                SqlCommand sc = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = sc.ExecuteReader();
                while (reader.Read())
                {
                    _RecepcionID = Convert.ToInt32(reader["RECEPCION_ID"]);
                }
                //Id Consecutivo nuevo Lote
                _RecepcionID += 1;


                cmd.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                _RecepcionID = 0;
                cn.Desconectar();
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return _RecepcionID.ToString();
        }

        private void DatosPrueba()
        {
            _taraLlena = "12300";
            _BodegaFrimexID = "19";
            _embarqueID = "12";
            _BodegaFrimexDesc = "ALMACEN GENERAL";
            _loteID = "";
            _AlmacenID = "19";
            _Almacen_desc = "ALMACEN GENERAL";
            _Serie = "G";
            _tipoDocumento = "C";
            _Documento = "4028";
        }
    }
}
