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
    public partial class FCapCon : Form
    {
        public FCapCon()
        {
            InitializeComponent();
        }
        UsuariosC _Usuario = new UsuariosC();
        Mensajes mensaje = new Mensajes();
        private bool ExisteFolio(string FOLIO)
        {
            bool _existe = false;
            string msg = "", consulta = "";
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = "Select * from CONTRATO as c where c.CONTRATO_FOLIO='"+FOLIO+"'";
                SqlCommand command = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)                
                    _existe = true;                
                else
                    _existe = false;
                reader.Close();
                command.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
                _existe = false;
            }
            if (msg.Length > 0)
                mensaje.Error(msg, "Error");
            return _existe;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (tBFolioContrato.Text.Trim().Length > 0)
                if (tBObservaciones.Text.Trim().Length >= 0)
                {
                    //Buscar si el folio existe
                    string _folio = tBFolioContrato.Text.Trim();
                    if(!ExisteFolio(_folio))
                    {
                        string _FolioContrato, _Observaciones, _ProductoID,
                        _ProductoDescripcion, _FechaContrato, _FechaCreacion, _UsuarioCreador, consulta;
                        DataRowView _dRVProd = (DataRowView)cBProducto.SelectedItem;
                        _ProductoID = _dRVProd.Row.ItemArray[0].ToString();
                        _ProductoDescripcion = _dRVProd.Row.ItemArray[1].ToString();
                        _FolioContrato = tBFolioContrato.Text.Trim();
                        _Observaciones = tBObservaciones.Text.Trim();
                        _FechaContrato = dTPFEchaContrato.Value.ToString("dd-MM-yyyy");
                        _FechaCreacion = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        _UsuarioCreador = _Usuario.USUARIOID.ToString();
                        ConexionSql cn = new ConexionSql();
                        cn.ConectarSQLServer();
                        SqlCommand command;

                        consulta = "INSERT INTO [dbo].[CONTRATO] " +
                           "([CONTRATO_FOLIO] " +
                           " ,[CONTRATO_FECHA] " +
                           " ,[CONTRATO_OBSERVACIONES] " +
                           " ,[PRODUCTO_ID] " +
                           " ,[PRODUCTO_DESCRIPCION] " +
                           " ,[CONTRATO_ESTATUS] " +
                           " ,[FECHA_CREACION] " +
                           " ,[USUARIO_CREACION]) " +
                           " VALUES " +
                           " ('" + _FolioContrato + "'" +
                           " ,'" + _FechaContrato + "'" +
                           " ,'" + _Observaciones + "'" +
                           " , " + _ProductoID +
                           " ,'" + _ProductoDescripcion + "'" +
                           " ,'A'" +
                           " ,'" + _FechaCreacion + "'" +
                           " , " + _UsuarioCreador + ") ";
                        command = new SqlCommand(consulta, cn.SC);
                        if (command.ExecuteNonQuery() > 0)
                            mensaje.Exito("Contrato guardado satisfactoriamente", "Éxito");
                        else
                            mensaje.Advertencia("Favor de reintentar", "Advertencia");
                        Limpiar();
                    }
                    else
                        mensaje.Advertencia("El folio de contrato "+_folio+" ya existe", "Información");
                }
                else
                    mensaje.Error("Favor de capturar el folio del contraro","Error");
            else
                mensaje.Error("No se han especificado observaciones", "Error");
        }

        private void FCapCon_Load(object sender, EventArgs e)
        {
            //Cargar Productos
            string msg = "";
            try
            {
                cBProducto.DataSource = CargarProductos();
                cBProducto.DisplayMember = "ART_GRAN_NOM";
                cBProducto.ValueMember = "ART_GRAN_ID";
            }
            catch(Exception Ex)
            {
                msg = Ex.Message;
            }
            if (msg.Length > 0)
                mensaje.Error(msg,"Error");
        }
        private DataTable CargarProductos()
        {
            string msg = "",consulta="";
            DataTable _productos = new DataTable();
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = "Select distinct ART_GRAN_ID,ART_GRAN_NOM from GRANELES_PT_P as gptp order by ART_GRAN_NOM";
                SqlDataAdapter _Da = new SqlDataAdapter(consulta, cn.SC);
                _Da.Fill(_productos);
                _Da.Dispose();
                cn.Desconectar();
            
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            if (msg.Length > 0)
                mensaje.Error(msg, "Error");
            return _productos;
        }

        private void Limpiar()
        {
            tBFolioContrato.Text = "";
            tBObservaciones.Text = "";
            dTPFEchaContrato.Value = DateTime.Now;
        }
    }
}
