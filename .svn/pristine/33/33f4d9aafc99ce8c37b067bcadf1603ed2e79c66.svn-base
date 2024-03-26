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
    public partial class FReq : Form
    {
        public FReq()
        {
            InitializeComponent();
        }
       public UsuariosC _Usuario = new UsuariosC();
        Mensajes mensajes = new Mensajes();
        private void Freq_Load(object sender, EventArgs e)
        {
            cBProducto.DataSource = CargarProducto();
            cBProducto.DisplayMember = "ART_GRAN_NOM";
            cBProducto.ValueMember = "ART_GRAN_ID";
            tBNoRequisicion.Text = SiguienteRequisicion();
            CargarAlmacen(cbAlmacen);
            CargarAlmacen(cBAlmacenDestino);
        }

        private void CargarAlmacen(ComboBox cBAlmacen)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
               
                cBAlmacen.DataSource = null;
                string cadena = "select INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " where ESTATUS = 'A'";
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
        private string SiguienteRequisicion()
        {
            string _SigReq = ""; 
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "SELECT COUNT(REQUISICION_ID)+1 AS SIG_REQ FROM REQUISICION";

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                {
                    _SigReq =Convert.ToString( reader["SIG_REQ"]);
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
            return _SigReq;
        }
        private DataTable CargarProducto()
        {
            DataTable _datos = new DataTable();
            string msg_local = "", consulta = "", _folio = "";
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = "  select distinct P.producto_id,gptp.ART_GRAN_ID,gptp.ART_GRAN_NOM,p.PRODUCTO_ID from GRANELES_PT_P as gptp " +
                    " inner join PRODUCTO as p on gptp.ART_GRAN_ID = p.PRODUCTO_MSP_ID where p.PRODUCTO_ESTATUS_ID=21 order by gptp.ART_GRAN_NOM " +
                    "";
                SqlDataAdapter _da = new SqlDataAdapter(consulta, cn.SC);
                _da.Fill(_datos);
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            return _datos;
        }      

        private void bAgregar_Click(object sender, EventArgs e)
        {
            string msg = "",_ProductoID="",_ProductoDescripcion="",_cantidad="";
            try
            {
                DataRowView _dRV = (DataRowView)cBProducto.SelectedItem;
                _ProductoID = _dRV.Row.ItemArray[0].ToString();
                _ProductoDescripcion = _dRV.Row.ItemArray[2].ToString();
                _cantidad = Convert.ToString(nUDCantidadSS.Value);
                dGVRequisicion.Rows.Add();
                int i = dGVRequisicion.Rows.Count - 1;
                dGVRequisicion["PRODUCTO_ID", i].Value = _ProductoID;
                dGVRequisicion["PRODUCTO_NOMBRE", i].Value = _ProductoDescripcion;
                dGVRequisicion["CANTIDAD", i].Value = _cantidad;
                dGVRequisicion["QUITAR", i].Value = "QUITAR";
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            if (msg.Length > 0)
                mensajes.Error(msg,"Error");
        }

        private void dGVRequisicion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            int _fila = e.RowIndex, _columna = e.ColumnIndex;
            if (_fila >= 0 && _columna >= 0)
            {
                string _contenido = Convert.ToString(dGVRequisicion[_columna, _fila].Value),
                    _productoID = Convert.ToString(dGVRequisicion["PRODUCTO_ID", _fila].Value),
                     _cantidad = Convert.ToString(dGVRequisicion["CANTIDAD", _fila].Value),
                    _productoDesc = Convert.ToString(dGVRequisicion["PRODUCTO_NOMBRE", _fila].Value);
                DialogResult _respuesta;
                if (_contenido == "QUITAR")
                {
                    _respuesta = MessageBox.Show("¿Desea quitar la solicitud de " + _cantidad + " del producto\n\r" + _productoDesc + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_respuesta == DialogResult.Yes)
                    {
                        dGVRequisicion.Rows.RemoveAt(_fila);
                    }
                }
            }

            }
            catch (Exception Ex)
            {
                mensajes.Error(Ex.Message,"Error");
            }
        }

        private void bCancelar_Click(object sender, EventArgs e)
        {
            dGVRequisicion.Rows.Clear();
            nUDCantidadSS.Value = 1;
        }

        private void bTerminar_Click(object sender, EventArgs e)
        {

            ConexionSql cn = new ConexionSql();
            try
            {
                if (dGVRequisicion.Rows.Count > 0)
                {
                    int _bandera = 0;
                    string _productoID, _productoDesc;
                    int _cant = 0;
                    List<string[,,]> _Productos = new List<string[,,]>();
                    for (int i = 0; i < dGVRequisicion.Rows.Count; i++)
                    {
                        _productoID = Convert.ToString(dGVRequisicion["PRODUCTO_ID", i].Value);
                        _cant = Convert.ToInt32(dGVRequisicion["CANTIDAD", i].Value);
                        _productoDesc = Convert.ToString(dGVRequisicion["PRODUCTO_NOMBRE", i].Value);
                        string[,,] _aux = new string[,,] { { { _productoID, _productoDesc, _cant.ToString() } } };
                        if (_Productos.Count == 0)
                            _Productos.Add(_aux);
                        else
                        {
                            bool _ExisteProducto = false;
                            for (int j = 0; _Productos.Count > j; j++)
                            {
                                string _auxJ = _Productos[j][0, 0, 1];
                                if (_auxJ == _productoDesc && _ExisteProducto == false)
                                {
                                    _ExisteProducto = true;
                                    int _CantAux = Convert.ToInt32(_Productos[j][0, 0, 2]),
                                        _CantIngresar = Convert.ToInt32(_aux[0, 0, 2]);

                                    _Productos[j][0, 0, 2] = Convert.ToString(_CantAux + _CantIngresar);
                                }
                            }
                            if (_ExisteProducto == false)
                                _Productos.Add(_aux);
                        }
                    }
                    //Mostrar productos en Message box
                    string _msgPRod = "";

                    for (int i = 0; i < _Productos.Count; i++)
                        _msgPRod += "Prod ID " + _Productos[i][0, 0, 0] + " desc " + _Productos[i][0, 0, 1] + " cant " + _Productos[i][0, 0, 2] + "\n\r";
                    DialogResult _resp = MessageBox.Show("Produtos a requisitar \r\n" + _msgPRod + "¿Desea crear la requisición?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (_resp == DialogResult.Yes)
                    {
                        DataRowView _dRV = (DataRowView)cbAlmacen.SelectedItem;
                        string _almacenID = _dRV.Row.ItemArray[0].ToString();
                        _dRV = (DataRowView)cBAlmacenDestino.SelectedItem;
                        string _almacenDestinoID = _dRV.Row.ItemArray[0].ToString();
                        if (_almacenID != _almacenDestinoID)
                        {
                            cn.ConectarSQLServer();
                            string consulta = "INSERT INTO [dbo].[REQUISICION] " +
                                   " ([REQUISICION_ID] " +
                                   " ,[REQUISICION_FECHA] " +
                                   " ,[ALMACEN_ID] " +
                                   " ,[ALMACEN_DESTINO_ID] " +
                                   " ,[REQUISICION_USUARIO_CREADOR] " +
                                   " ,[REQUISICION_FECHA_CREACION]" +
                                   " ,[REQUISICION_ESTATUS]) " +
                                   " VALUES " +
                                   " ( " + tBNoRequisicion.Text +
                                   " ,'" + dTPFechaReq.Value.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                                   " , " + _almacenID +
                                   " , " + _almacenDestinoID +
                                   " , " + _Usuario.USUARIOID +
                                   " ,'" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "'" +
                                   " ,'A')";//Insertar encabezado con transaccion
                            SqlTransaction transaction = cn.SC.BeginTransaction();

                            SqlCommand cmdm = new SqlCommand(consulta, cn.SC, transaction);
                            if (cmdm.ExecuteNonQuery() > 0)
                            {
                                //Insertar detalle de requisicion
                                for (int i = 0; _Productos.Count > i; i++)
                                {
                                    consulta = "INSERT INTO [dbo].[REQUISICION_DETALLE] " +
                                       " ([REQUISICION_ID] " +
                                       " ,[PRODUCTO_ID] " +
                                       " ,[REQUISICION_DETALLE_CANTIDAD]) " +
                                       " VALUES " +
                                       " ( " + tBNoRequisicion.Text +
                                       " , " + _Productos[i][0, 0, 0] +
                                       " , " + _Productos[i][0, 0, 2] +
                                       ")";
                                    cmdm = new SqlCommand(consulta, cn.SC, transaction);
                                    if (cmdm.ExecuteNonQuery() > 0)
                                    {
                                        // mensajes.Exito("Requisición creada con éxito","Éxito");
                                        _bandera++;
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                transaction.Rollback();
                            }
                            cmdm.Dispose();
                            if (_bandera == _Productos.Count)
                            {

                                transaction.Commit();
                                Limpiar();
                            }
                            else
                                transaction.Rollback();
                            transaction.Dispose();
                            cn.Desconectar();
                            this.Close();
                        }
                        else
                        {
                            mensajes.Error("El almacen origen y destino tienen que ser diferentes", "Error");
                        }
                    }
                }
                else
                    mensajes.Error("Favor de especificar el producto a solicitar","Error"); 
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
        }
        private void Limpiar()
        {
            tBNoRequisicion.Text = SiguienteRequisicion();
            dGVRequisicion.Rows.Clear();
            nUDCantidadSS.Value = 0;
            cBProducto.SelectedIndex = 1;
        }
    }
}
