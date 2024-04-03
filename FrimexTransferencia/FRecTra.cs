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
using ApiBa = ApisMicrosip.ApiMspBasicaExt;
using ApiIn = ApisMicrosip.ApiMspInventExt;

namespace FrimexTransferencia
{
    public partial class FRecTra : Form
    {
        bool Activo = false;
        public UsuariosC _Usuario = new UsuariosC();
        PuertosSerie _PuertoSerie = new PuertosSerie();
        bool _basculaConectada = false, _activa = false,correcto=false;
        Mensajes mensajes = new Mensajes();
        string auxSSID="";
        string ALMACEN_ORIGEN { set; get; }
        string ALMACEN_DESTINO { set; get; }
        public FRecTra()
        {
            InitializeComponent();
        }

        private void FRecTra_Load(object sender, EventArgs e)
        {
            CargarAlmacen(cBAlmacen);
        }
        private void CargarAlmacen(ComboBox cBAlmacen)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();

                cBAlmacen.DataSource = null;
                string cadena = "select infr.INVENTARIO_FRIMEX_ID,ALMACEN_MSP_DESCRIPCION,INVENTARIO_SUPERSACO_ID " +
                    " from INVENTARIO_FRIMEX as infr " +
                    " inner join USU_ALM as ua on infr.INVENTARIO_FRIMEX_ID = ua.ALMACEN_ID " +
                    " inner join INVENTARIO_SUPERSACO as iss on iss.INVENTARIO_FRIMEX_ID=infr.INVENTARIO_FRIMEX_ID " +
                    " where ESTATUS = 'A' AND ua.USUARIO_ID = " + _Usuario.USUARIOID.ToString();
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
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void bIniciarRecepción_Click(object sender, EventArgs e)
        {
            string _folio = Convert.ToString(tBTransferenciaID.Text);
            if (_folio.Length > 0 )
            {                
                if (EstatusTransferencia(_folio))
                {
                    if (Activo == false)
                    {
                        Activo = true;
                        bIniciarRecepción.Text = "Terminar Recepción";
                        label4.Visible = true;
                        tBSupersacoID.Visible = true;
                        tBSupersacoID.Enabled = true;
                        bLeerSS.Visible = true;
                        bLeerSS.Enabled = true;
                        tBTransferenciaID.Enabled = false;
                        if (_folio.Length > 0)
                        {
                            //CargarSupersacosTransferidos(_folio);
                            CargarSupersacosTransferencia(_folio);

                        }

                    }
                    else if (Activo == true)
                    {
                        string _transferenciaID = tBTransferenciaID.Text.Trim();
                        DialogResult result = MessageBox.Show("¿Desea terminar la recepción de la transferencia " + _transferenciaID + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            //Validar Que el traspaso este completado, de ser así cambiar estatus de la requisision a Terminada en caso que este surtida
                            //Hacer el impacto a microsip
                            string  _AlmacenID="", _AlmacenDestino="", msg_local="";
                            CTransferencias transferencias = new CTransferencias();
                            C_FuncionesMSP c_FuncionesMSP = new C_FuncionesMSP();
                            AlmacenesID(_transferenciaID);
                            _AlmacenID = ALMACEN_ORIGEN;
                            _AlmacenDestino = ALMACEN_DESTINO;

                            if (ValidaTraspasoCompletado(_transferenciaID))
                            {
                                if (c_FuncionesMSP.TransferenciaEntreAlmacenesMSP(transferencias.AlmacenMSPDesdeSQL(_AlmacenID), transferencias.AlmacenMSPDesdeSQL(_AlmacenDestino)
                                     , Convert.ToInt32(_transferenciaID), out msg_local))
                                {
                                    mensajes.Exito("Transferencia completada con éxito", "Éxito");
                                    bIniciarRecepción.Text = "Iniciar Recepción";
                                    Activo = false;
                                    label4.Visible = false;
                                    tBSupersacoID.Visible = false;
                                    tBSupersacoID.Enabled = true;
                                    bLeerSS.Visible = false;
                                    bLeerSS.Enabled = true;
                                    tBTransferenciaID.Enabled = true;
                                    Limpiar();

                                }
                                else
                                    mensajes.Error("Transferencia no capturada en MSP\n\r" + msg_local, "Error");
                            }
                            else
                                mensajes.Error("Supersacos pendientes por recibir de la transferencia " + _transferenciaID, "Error");
                        }
                    }

                }
                else
                {

                    mensajes.Informacion("Transferencia recibida anteriormente", "Información");
                    tBTransferenciaID.Text = "";
                }
             }
            else
            {

                MessageBox.Show("Favor de especificar la trasferencia a recibir", "Mensaje de la aplicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Limpiar()
        {
            dGVAsignados.Rows.Clear();
            dGVRecibidos.Rows.Clear();
            tBSupersacoID.Text = "";
            tBTransferenciaID.Text = "";
        }
        private bool ValidaEstatusTransferencia(string TRANSFERENCIA_ID)
        {
            bool _existe = false;
            try
            {
                string _transferenicaEstatus = "";
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();

                string cadena = "select TRANSFERENCIA_ESTATUS from TRANSFERENCIA as t " +
                    " where t.TRANSFERENCIA_ID = " + TRANSFERENCIA_ID;
                SqlCommand cmdm = new SqlCommand(cadena, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _transferenicaEstatus = Convert.ToString(reader["TRANSFERENCIA_ESTATUS"]);
                    }
                }
                if (_transferenicaEstatus == "A")
                    _existe = true;
                else
                    _existe = false;
                cn.Desconectar();
            }
            catch
            {
                _existe = false;
            }
            return _existe;
        }
            private bool ValidaTraspasoCompletado(string TRANSFERENCIA_ID)
        { 
            bool _existe = false;
            try
            {                
                int _Recibidos = 0,_Asignados=0;
                string msg = "",_requisicion="";
                _Recibidos = dGVRecibidos.Rows.Count;
                _Asignados = dGVAsignados.Rows.Count;
                ConexionSql cn = new ConexionSql();
                cn.ConectarSQLServer();
                if (_Recibidos==_Asignados)
                {
                    string cadena = "";
                    SqlCommand cmdm;
                   cadena = "UPDATE [dbo].[TRANSFERENCIA] " +
                       " SET[TRANSFERENCIA_ESTATUS] = 'T' " + 
                       " WHERE[TRANSFERENCIA_ID] = " + TRANSFERENCIA_ID;
                    cmdm = new SqlCommand(cadena, cn.SC);
                    cmdm.ExecuteNonQuery();                    
                    cmdm.Dispose();
                    cadena = "select distinct RT.REQUISICION_ID from REQ_TRA RT " +
                        " inner join TRANSFERENCIA as t on RT.TRANSFERENCIA_ID = t.TRANSFERENCIA_ID " +
                        " where t.TRANSFERENCIA_ID = "+TRANSFERENCIA_ID;
                    cmdm = new SqlCommand(cadena,cn.SC);
                    SqlDataReader reader = cmdm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            _requisicion = Convert.ToString(reader["REQUISICION_ID"]);
                        }
                        CRequisiciones requisiciones = new CRequisiciones();
                        if(requisiciones.ValidaRequisicionCompletada(Convert.ToInt32(_requisicion),out msg))
                        {
                            requisiciones.CambiarEstatusRequisicion(Convert.ToInt32(_requisicion), "T", out msg);
                        }
                    }
                    _existe = true;
                }
                else
                    _existe = false;
                cn.Desconectar();
            }
            catch 
            {
                _existe = false;
            }
            return _existe;
        }
        private bool BuscarSupersacoEnRecibidos(string SUPERSACO_ID)
        {
            bool _existe = false;
            try
            {
                if (dGVRecibidos.Rows.Count == 0)
                    _existe = false;
                else if (dGVRecibidos.Rows.Count > 0)
                {
                    for (int i = 0; dGVRecibidos.Rows.Count > i; i++)
                    {
                        string aux = Convert.ToString(dGVRecibidos["SUPERSACO_IDR", i].Value);
                        if (aux == SUPERSACO_ID)
                        {
                            _existe = true;
                            break;
                        }
                    }
                }
            }
            catch
            {

            }
            return _existe;
        }
        private bool EstatusTransferencia(string TRANSFERENCIA_ID)
        {
            bool _existe = false;
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = " select TRANSFERENCIA_ESTATUS from TRANSFERENCIA " +
                        " where TRANSFERENCIA_ID =  " + TRANSFERENCIA_ID,_estatus="";

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
               // int i = 0;
                //dGVAsignados.Rows.Clear();
                while (reader.Read())
                {
                   _estatus = Convert.ToString(reader["TRANSFERENCIA_ESTATUS"]);
                    if (_estatus == "A")
                        _existe = true;
                    else if (_estatus == "T")
                        _existe = false;
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return _existe;
        }
        private void CargarSupersacosTransferencia(string TRANSFERENCIA_ID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "Select t.transferencia_id,s.SUPERSACO_ID,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD,t.TRANSFERENCIA_ESTATUS " +
                    " from [TRANSFERENCIA] as t " +
                    " inner join transferencia_detalle as td on t.transferencia_id = td.transferencia_id " +
                    " inner join SUPERSACO as s on td.supersaco_id=s.SUPERSACO_ID " +
                    " inner join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                    " where " +
                    " t.TRANSFERENCIA_ESTATUS='A' and t.transferencia_id = " + TRANSFERENCIA_ID+ " order by s.SUPERSACO_ID asc";

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                int i = 0;
                dGVAsignados.Rows.Clear();
                while (reader.Read())
                {
                    dGVAsignados.Rows.Add();
                    dGVAsignados["SUPERSACO_ID", i].Value = Convert.ToString(reader["SUPERSACO_ID"]);
                    dGVAsignados["PRODUCTO", i].Value = Convert.ToString(reader["PRODUCTO_DESCRIPCION"]);
                    dGVAsignados["PESO", i].Value = Convert.ToString(reader["SUPERSACO_CANTIDAD"]);
                    if (!BuscarSupersacoEnRecibidos(Convert.ToString(reader["SUPERSACO_ID"])))
                        dGVAsignados.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else
                        dGVAsignados.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    i++;
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CargarSupersacosTransferidos(string TRANSFERENCIA_ID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "Select t.transferencia_id,s.SUPERSACO_ID,p.PRODUCTO_DESCRIPCION,s.SUPERSACO_CANTIDAD from [TRANSFERENCIA] as t " +
                    " inner join transferencia_detalle as td on t.transferencia_id = td.transferencia_id " +
                    " inner join SUPERSACO as s on td.supersaco_id=s.SUPERSACO_ID " +
                    " inner join PRODUCTO as p on s.PRODUCTO_ID = p.PRODUCTO_ID or s.PRODUCTO_ID = p.PRODUCTO_MSP_ID " +
                   // " inner join INVENTARIO_FRIMEX as iv on s.INVENTARIO_SUPERSACO_ID=iv.INVENTARIO_FRIMEX_ID " +
                   " inner join INVENTARIO_FRIMEX as iv on  t.ALMACEN_DESTINO_ID =iv.INVENTARIO_FRIMEX_ID " +
                    " where " +
                    " t.transferencia_id = " + TRANSFERENCIA_ID +
                    " order by s.SUPERSACO_ID asc";

                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                int i = 0;
                dGVRecibidos.Rows.Clear();
                while (reader.Read())
                {
                    dGVRecibidos.Rows.Add();
                    dGVRecibidos["SUPERSACO_IDR", i].Value = Convert.ToString(reader["SUPERSACO_ID"]);
                    dGVRecibidos["PRODUCTO_R", i].Value = Convert.ToString(reader["PRODUCTO_DESCRIPCION"]);
                    dGVRecibidos["PESO_R", i].Value = Convert.ToString(reader["SUPERSACO_CANTIDAD"]);
                    if (!BuscarSupersacoEnRecibidos(Convert.ToString(reader["SUPERSACO_ID"])))
                        dGVRecibidos.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    else
                    {
                        dGVRecibidos.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                        //pintamos el dgv de recibidos a verde tambien
                        for (int j = 0; j < dGVAsignados.RowCount; j++)
                        {
                            if (dGVAsignados["SUPERSACO_ID", j].Value.ToString().Equals(Convert.ToString(reader["SUPERSACO_ID"])))
                            {
                                dGVAsignados.Rows[j].DefaultCellStyle.BackColor = Color.LightGreen;
                                break;
                            }
                                
                        }
                    }
                    i++;
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void tBTransferenciaID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string _folio = Convert.ToString(tBTransferenciaID.Text);
                if (_folio.Length > 0)
                {
                    if (EstatusTransferencia(_folio))
                    {
                        if (Activo == false)
                        {
                            Activo = true;
                            bIniciarRecepción.Text = "Terminar Recepción";
                            label4.Visible = true;
                            tBSupersacoID.Visible = true;
                            tBSupersacoID.Enabled = true;
                            bLeerSS.Visible = true;
                            bLeerSS.Enabled = true;
                            tBTransferenciaID.Enabled = false;
                            if (_folio.Length > 0)
                            {
                                //CargarSupersacosTransferidos(_folio);
                                CargarSupersacosTransferencia(_folio);

                            }
                        }
                    }
                    else
                    {
                        mensajes.Informacion("Transferencia recibida anteriormente", "Información");
                        tBTransferenciaID.Text = "";
                    }
                }
            }
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void conectarBásculaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pConectarPuerto.Visible = true;
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
                        MessageBox.Show("Báscula desconectada","Mensaje de la apliacicón",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
        private bool ValidarPesoSupersaco(string SUPERSACOID, double PESO_NUEVO,int ALMACEN_ID_NUEVO,out string PRODUCTO_DESCRIPCION,out string msg)
        {
            bool _exito = false;
            string msg_local = "", consulta = "",_ProductoDesc="";
            ConexionSql cn = new ConexionSql();
            try
            {
                int _almacenID = 0;
                double _pesoSS = 0.0;
                consulta = "select s.SUPERSACO_ID, s.PRODUCTO_ID, s.SUPERSACO_CANTIDAD,s.INVENTARIO_SUPERSACO_ID,p.PRODUCTO_DESCRIPCION " +
                    " from SUPERSACO as s " +
                    " inner join PRODUCTO as p on s.PRODUCTO_ID=p.PRODUCTO_ID or s.PRODUCTO_Id= p.PRODUCTO_MSP_ID" +
                    " where " +
                    " SUPERSACO_ID = " + SUPERSACOID;
                cn.ConectarSQLServer();
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                {
                    _pesoSS = Convert.ToDouble(reader["SUPERSACO_CANTIDAD"]);
                    _almacenID = Convert.ToInt32(reader["INVENTARIO_SUPERSACO_ID"]);
                    _ProductoDesc= Convert.ToString(reader["PRODUCTO_DESCRIPCION"]);
                }
                if (_pesoSS - 10 <= PESO_NUEVO || _pesoSS + 10 >= PESO_NUEVO)
                {
                    _exito = true;
                    CRequisiciones requisiciones = new CRequisiciones();
                    requisiciones.ActualizarPesoSS(Convert.ToInt32(SUPERSACOID), PESO_NUEVO, ALMACEN_ID_NUEVO, 0, out msg_local);
                }
                else
                {
                    DialogResult resp = MessageBox.Show("Variación de peso fuera de rango\n\r¿Desea que alguen autorice el peso?", "Autorización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resp == DialogResult.Yes)
                    {
                        FAutorizacion autorizacion = new FAutorizacion();
                        autorizacion.TipoAutorizacion = "SUPERVISOR";
                        if (autorizacion.ShowDialog() == DialogResult.Cancel)
                        {
                            if (autorizacion.TIPO_USU_ID == 2) // Usuario Administrador
                            {
                                _exito = true;
                                CRequisiciones requisiciones = new CRequisiciones();
                                requisiciones.ActualizarPesoSS(Convert.ToInt32(SUPERSACOID), PESO_NUEVO, _almacenID, autorizacion.USU_ID, out msg_local);
                            }
                            else
                                _exito = false;
                        }
                    }
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            PRODUCTO_DESCRIPCION = _ProductoDesc;
            msg = msg_local;
            return _exito;
        }
        private void bLeerSS_Click(object sender, EventArgs e)
        {
            //tBSupersacoID.Text= InvertirCadena(_PuertoSerie.DatoRecibido());
            string _auxSSID = tBSupersacoID.Text.Trim();
            for (int i = 0; i < _auxSSID.Length; i++)
            {
                if (correcto == false)
                {
                    if (!char.IsDigit(_auxSSID[i]))
                    {
                        correcto = true;
                        break;
                    }
                    else
                        auxSSID += _auxSSID[i];
                }
            }
            tBSupersacoID.Text = auxSSID;
            string msg_local = "", _ss = "",_Descripcion="";
            try
            {
                _ss = tBSupersacoID.Text.Trim();
                if (_ss.Length > 0)
                {
                    DialogResult result = MessageBox.Show("¿Desea capturar el super saco " + _ss + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataRowView _dRVAlmacen = (DataRowView)cBAlmacen.SelectedItem;
                        string _AlmacenID = _dRVAlmacen.Row.ItemArray[2].ToString(), _transferenciaID = tBTransferenciaID.Text.Trim();

                        if (_basculaConectada == true)
                        {
                            double _PesoSS = 0.0;
                            string aux = "";
                            //if (_AlmacenID == "1221")
                            //    aux = ValidarDigitos(InvertirCadena(_PuertoSerie.DatoRecibidoAlmacenGeneral()));
                            //else
                                aux = ValidarDigitos(_PuertoSerie.DatoRecibido());
                            decimal valor;
                            
                            
                            if (Decimal.TryParse(aux, out valor))
                            {
                                _PesoSS = Convert.ToDouble(valor);
                                if (_PesoSS > 0)
                                {
                                    if (ValidarPesoSupersaco(_ss, _PesoSS,Convert.ToInt32( _AlmacenID),out _Descripcion,out msg_local))
                                    {
                                        CargarSupersacosTransferidos(_transferenciaID);
                                        //dGVRecibidos.Rows.Add();
                                        //int fila = dGVRecibidos.RowCount - 1;
                                        //dGVRecibidos["SUPERSACO_IDR",fila].Value = _ss;
                                        //dGVRecibidos["PRODUCTO_R", fila].Value = _Descripcion;
                                        //dGVRecibidos["PESO_R", fila].Value = _PesoSS;
                                        
                                        //    dGVRecibidos.Rows[fila].DefaultCellStyle.BackColor = Color.LightGreen;
                                        mensajes.Exito("Se agregó el supersaco \"" + _ss + "\" ", "Éxito");
                                        tBSupersacoID.Text = "";
                                        correcto = false;
                                        _auxSSID = "";
                                        auxSSID = "";
                                        _ss = "";
                                    }
                                    else
                                        mensajes.Error("Peso de supersaco incorrecto", "Error");
                                }
                                else
                                    mensajes.Error("Valor de báscula no capturado", "Error");
                            }
                            else
                                mensajes.Error("Error al leer la báscula, favor de reintentar", "Error");
                        }
                        else
                            mensajes.Error("Báscula no conectada", "Error");
                    }
                }
                else
                    MessageBox.Show("Favor de capturar el supersaco", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            if (msg_local.Length > 0)
                MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void FRecTra_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_PuertoSerie.EstaConectado)
            {
                MessageBox.Show("Favor de desconectar el puerto serie", "Informacion",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                e.Cancel = true;
            }
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
                        MessageBox.Show("Báscula conectada","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
                MessageBox.Show(Ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void AlmacenesID(string TRANSFERENCIA_ID)
        {
            ConexionSql cn = new ConexionSql();
            try
            {
                cn.ConectarSQLServer();
                string consulta = "select ALMACEN_ORIGEN_ID, ALMACEN_DESTINO_ID from TRANSFERENCIA  where TRANSFERENCIA_ID =  "+TRANSFERENCIA_ID;
                SqlCommand cmdm = new SqlCommand(consulta, cn.SC);
                SqlDataReader reader = cmdm.ExecuteReader();
                while (reader.Read())
                {
                    ALMACEN_ORIGEN = Convert.ToString(reader["ALMACEN_ORIGEN_ID"]);
                    ALMACEN_DESTINO= Convert.ToString(reader["ALMACEN_DESTINO_ID"]);
                }
                cmdm.Dispose();
                cn.Desconectar();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //private bool TransferenciaMSP(DataGridView dGVTransferir,int ALMACEN_ORIGEN,int ALMACEN_DESTINO,out string msg)
        //    {

        //    }
    }
}
