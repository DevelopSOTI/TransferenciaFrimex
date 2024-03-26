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
    public partial class FRegEmbImp : Form
    {
        public FRegEmbImp()
        {
            InitializeComponent();
        }
        Mensajes mensaje = new Mensajes();
        UsuariosC _Usuario = new UsuariosC();
        string _Estatus = "";
        private void bCaptCont_Click(object sender, EventArgs e)
        {
           
        }

        private void capturarContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FCapCon fCapCon = new FCapCon();
            bContrato.Visible = false;
            bSeleccionarContrato.Visible = true;
            this.Size = new Size(384, 427);
            CenterToScreen();
            fCapCon.ShowDialog();
            cBContrato.DataSource = CargarContratos();
            cBContrato.DisplayMember = "CONTRATO_FOLIO";
            cBContrato.ValueMember = "CONTRATO_ID";
        }

        private void completarContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bContrato.Text = "Completar";
            bContrato.Visible = true;
            bSeleccionarContrato.Visible = false;
            this.Size = new Size(384, 100);
            CenterToScreen();
            cBContrato.DataSource = CargarContratos();
            cBContrato.DisplayMember = "CONTRATO_FOLIO";
            cBContrato.ValueMember = "CONTRATO_ID";
            _Estatus = "T";
        }

        private void cancelarContratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bContrato.Text = "Cancelar";
            bContrato.Visible = true;
            bSeleccionarContrato.Visible = false;
            this.Size = new Size(384, 100);
            CenterToScreen();
            cBContrato.DataSource = CargarContratos();
            cBContrato.DisplayMember = "CONTRATO_FOLIO";
            cBContrato.ValueMember = "CONTRATO_ID";
            _Estatus = "C";
        }

        private void FRegEmbImp_Load(object sender, EventArgs e)
        {
            cBContrato.DataSource = CargarContratos() ;
            cBContrato.DisplayMember = "CONTRATO_FOLIO";
            cBContrato.ValueMember = "CONTRATO_ID";

            
        }
        private DataTable CargarContratos()
        {
            DataTable _datos = new DataTable();
            string msg_local="", consulta = "";
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = " Select * from contrato where contrato_estatus ='A'";
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
        private void bSeleccionarContrato_Click(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                tBPesoProducto.Enabled = true;
                tBSellos.Enabled = true;
                tBTolva.Enabled = true;
                tBTrasporte.Enabled = true;
                cBProducto.Enabled = true;
                cBContrato.Enabled = false;
                cBProducto.DataSource = CargarArticulos();
                cBProducto.DisplayMember = "ART_GRAN_NOM";
                cBProducto.ValueMember = "ART_GRAN_ID";
            }
            catch (Exception Ex)
            {
                msg = Ex.Message;
            }
            if (msg.Length > 0)
                mensaje.Error(msg, "Error");
        }
        private void bAceptar_Click(object sender, EventArgs e)
        {
            string msg = "",consulta="", FURGON_FOLIO,FURGON_EMPRESA,PRODUCTO_ID,FURGON_PESO,
                FURGON_SELLOS,CONTRATO_ID,FURGON_FECHA_CREACION,FURGON_USUARIO_CREADOR, FURGON_FECHA_APROX_LLEGADA;
                ConexionSql cn = new ConexionSql();
                SqlCommand command;
                SqlTransaction transaction;
            cn.ConectarSQLServer();
            transaction = cn.SC.BeginTransaction();
            try
            {
                DataRowView _dRVProd = (DataRowView)cBContrato.SelectedItem;
                CONTRATO_ID= _dRVProd.Row.ItemArray[0].ToString();
                FURGON_FOLIO = tBTolva.Text.Trim();
                FURGON_EMPRESA = tBTrasporte.Text.Trim();
                _dRVProd = (DataRowView)cBProducto.SelectedItem;
                PRODUCTO_ID= _dRVProd.Row.ItemArray[0].ToString();
                FURGON_PESO = tBPesoProducto.Text.Trim();
                FURGON_SELLOS = tBSellos.Text.Trim();
                FURGON_FECHA_CREACION = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                FURGON_USUARIO_CREADOR = _Usuario.USUARIOID.ToString();
                FURGON_FECHA_APROX_LLEGADA = dTPFechaAproxLlegada.Value.ToString("dd-MM-yyyy HH:mm:ss");
                if (FURGON_FOLIO.Length > 0)
                    if (FURGON_EMPRESA.Length > 0)
                        if (PRODUCTO_ID.Length > 0)
                            if (FURGON_PESO.Length > 0)
                                if (FURGON_SELLOS.Length > 0)
                                    if (CONTRATO_ID.Length > 0)
                                    {
                                        consulta = "INSERT INTO [dbo].[FURGON] " +
                                           " ([FURGON_FOLIO] " +
                                           " ,[FURGON_EMPRESA] " +
                                           " ,[PRODUCTO_ID] " +
                                           " ,[FURGON_PESO] " +
                                           " ,[FURGON_SELLOS] " +
                                           " ,[CONTRATO_ID] " +
                                           " ,[FURGON_FECHA_CREACION] " +
                                           " ,[FURGON_USUARIO_CREADOR]" +
                                           " ,FURGON_FECHA_APROX_LLEGADA) " +
                                           " VALUES " +
                                           " ('" + FURGON_FOLIO + "'" +
                                           " ,'" + FURGON_EMPRESA + "'" +
                                           " , " + PRODUCTO_ID +
                                           " ,'" + FURGON_PESO + "'" +
                                           " ,'" + FURGON_SELLOS + "'" +
                                           " , " + CONTRATO_ID +
                                           " ,'" + FURGON_FECHA_CREACION + "'" +
                                           " , " + FURGON_USUARIO_CREADOR +
                                           " ,'" + FURGON_FECHA_APROX_LLEGADA + "'"+
                                           ")";
                                        
                                        command = new SqlCommand(consulta, cn.SC,transaction);
                                        if (command.ExecuteNonQuery() > 0)
                                        {
                                            transaction.Commit();
                                            mensaje.Exito("Furgón guardado satisfactoriamente", "Éxito");
                                        }
                                        else
                                        {
                                            transaction.Rollback();
                                            mensaje.Advertencia("Favor de reintentar", "Advertencia");
                                        }
                                        command.Cancel();
                                        cn.Desconectar();
                                        Limpiar();
                                    }
                                    else
                                        msg = "Contrato no especificado";
                                else
                                    msg = "Sellos no capturados";
                            else
                                msg = "Peso no capturado";
                        else
                            msg = "Producto no especificado";
                    else
                        msg = "Empresa no especificada";
                else
                    msg="Folio no especificado";      
            }
            catch (Exception Ex)
            {
                msg += Ex.Message;
                transaction.Rollback();
            }
            if (msg.Length > 0)
                mensaje.Error(msg,"Error");
        }
        private void Limpiar()
        {

            tBTolva.Text="";
            tBTrasporte.Text="";
            tBPesoProducto.Text="";
            tBSellos.Text="";
            //¿Actualizar contratos y productos?
            cBProducto.DataSource = null;
            tBTolva.Enabled = false;
            tBTrasporte.Enabled = false;
            tBPesoProducto.Enabled = false;
            tBSellos.Enabled = false;
            cBProducto.Enabled = false;
            cBContrato.Enabled = true;

        }
        private void bContrato_Click(object sender, EventArgs e)
        {
            string msg_local = "", consulta = "",_FolioContrato="",_contratoId="",_update="";
            try
            {
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                DataRowView _dRVProd = (DataRowView)cBContrato.SelectedItem;
                _contratoId = _dRVProd.Row.ItemArray[0].ToString();
                _FolioContrato = _dRVProd.Row.ItemArray[1].ToString();
                    if (_Estatus == "T")
                        _update = "terminar";
                    else if (_Estatus == "C")
                        _update = "cancelar";
                DialogResult _resp= MessageBox.Show("¿Desea "+_update+" el contrato "+_FolioContrato+"?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                if(_resp==DialogResult.Yes)
                {
                    consulta = " UPDATE [dbo].[CONTRATO] " +
                        " SET[CONTRATO_ESTATUS] ='" + _Estatus + "'" +
                        " WHERE[CONTRATO_FOLIO] = '" + _FolioContrato + "' AND[CONTRATO_ID] = " + _contratoId;
                    // SqlDataAdapter _da = new SqlDataAdapter(consulta, cn.SC);
                    SqlCommand com = new SqlCommand(consulta, cn.SC);
                    if (com.ExecuteNonQuery() > 0)
                        mensaje.Exito("Estatus del contrato actualizado correctamente", "Exito");
                    else
                        mensaje.Error("Error al actualizar el estatus del contrato", "Error");
                    com.Cancel();
                    cn.Desconectar();
                    cBContrato.DataSource = CargarContratos();
                    cBContrato.DisplayMember = "CONTRATO_FOLIO";
                    cBContrato.ValueMember = "CONTRATO_ID";
                }
                
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }

        }
        private void bCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private DataTable CargarArticulos()
        {
            DataTable _datos = new DataTable();
            string msg_local = "", consulta = "",_folio="";
            try
            {
                DataRowView _dRVProd = (DataRowView)cBContrato.SelectedItem;
                _folio= _dRVProd.Row.ItemArray[1].ToString();
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                consulta = "  select distinct gptp.ART_GRAN_ID,gptp.ART_GRAN_NOM,p.PRODUCTO_ID from GRANELES_PT_P as gptp " +
                    " inner join PRODUCTO as p on gptp.ART_GRAN_ID = p.PRODUCTO_MSP_ID " +
                    " inner join CONTRATO as c on gptp.ART_GRAN_ID = c.PRODUCTO_ID " +
                    " where c.CONTRATO_FOLIO = '"+_folio+"'";
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

        private void tBPesoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                
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
        //private string ExisteEmbarque(string FOLIO, out string msg)
        //{


        //}
    }
}
