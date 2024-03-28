using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrimexTransferencia
{
    public partial class FAdmLot : Form
    {
        public UsuariosC _Usuario = new UsuariosC();
        DataTable _datosMSP;
        List<string[,]> _pesosMSP = new List<string[,]>();
        public FAdmLot()
        {
            InitializeComponent();
        }

        private void bBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
        private void Buscar()
        {
            if(tBLoteID.Text.Trim().Length>0)
            {
                string msg_local = "", _lotesConCeros = "",
                _folios = tBLoteID.Text;
                C_Funciones funciones = new C_Funciones();
                tBInfoMSP.Text = "";
                _lotesConCeros += "'" + funciones.RellenarConCeros(_folios, 9) + "'";
                _datosMSP = new DataTable();
                _datosMSP = funciones.CargarDatosMSP(_lotesConCeros, out msg_local);
                dGVSupersacos.Rows.Clear();
                dGVSupersacos.Columns.Clear();
                _pesosMSP = new List<string[,]>();
                string auxCant = "";
                double auxCantDoble = 0.0;
                if (msg_local.Length > 0)
                    MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //if (_datosMSP.Rows.Count == 0)
                //    MessageBox.Show("Sin datos encontrados", "Información");
                //else
                {
                    tBInfoMSP.Text = "Información del lote MSP:";
                    foreach (DataRow fila in _datosMSP.Rows)
                    {
                        auxCant = Convert.ToString(fila[4]);
                        if (auxCant.Length > 0)
                            if (Double.TryParse(auxCant, out auxCantDoble))
                                auxCantDoble = Convert.ToDouble(auxCant);
                            else
                                auxCantDoble = 0.0;
                        else
                            auxCantDoble = 0.0;
                        auxCant = auxCantDoble.ToString("N2");
                        tBInfoMSP.Text += "\r\nFolio OC: " + Convert.ToString(fila[0])
                            + "\r\nFolio compra: " + Convert.ToString(fila[1])+ " - Lote MSP: "+Convert.ToString(fila[5])
                            + "\r\nProducto: " + Convert.ToString(fila[2])
                            + "\r\nCatidad comprada: " + auxCant;
                        string [,]auxPeso=new string[,]{ { Convert.ToString(fila[2]),auxCant} };
                        _pesosMSP.Add(auxPeso);
                    }
                }
                if (_datosMSP.Rows.Count >= 0)
                {
                    bool _exito = false;
                    string msg = "", _Lotes = _folios;
                    DateTime _fechaInicio = new DateTime();//new DateTime(dTPInicio.Value.Year, dTPInicio.Value.Month, dTPInicio.Value.Day, 0, 0, 0);
                    DateTime _fechaFin = new DateTime(); //new DateTime(dTPFin.Value.Year, dTPFin.Value.Month, dTPFin.Value.Day, 23, 59, 59);                                                     
                    C_Funciones _Funciones = new C_Funciones();
                    DataTable _Datos = _Funciones.CargarDatosGridXModulosConcentrado(_Lotes, _fechaInicio, _fechaFin, out msg);
                    if (msg.Length > 0)
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (_Datos.Rows.Count > 0)
                    {
                        dGVSupersacos.Columns.Clear();
                        dGVSupersacos.Rows.Clear();

                        dGVSupersacos.ColumnCount = 6;

                        dGVSupersacos.Columns[0].Name = "SS_ID";
                        dGVSupersacos.Columns[0].HeaderText = "SS_ID";
                        dGVSupersacos.Columns[1].Width = 60;

                        //dGVSupersacos.Columns[1].Name = "PRODUCTO_ID";
                        //dGVSupersacos.Columns[1].HeaderText = "PRODUCTO_ID";


                        dGVSupersacos.Columns[1].Name = "SUPERSACO_PRODUCTO";
                        dGVSupersacos.Columns[1].HeaderText = "SUPERSACO_PRODUCTO";
                        dGVSupersacos.Columns[1].Width = 200;

                        dGVSupersacos.Columns[2].Name = "LOTE_ID";
                        dGVSupersacos.Columns[2].HeaderText = "LOTE_ID";
                        dGVSupersacos.Columns[2].Width = 60;

                        dGVSupersacos.Columns[3].Name = "PESO_SS";
                        dGVSupersacos.Columns[3].HeaderText = "PESO_SS";
                        dGVSupersacos.Columns[3].Width = 100;

                        dGVSupersacos.Columns[4].Name = "FECHA_CREACION";
                        dGVSupersacos.Columns[4].HeaderText = "FECHA_CREACION";
                        dGVSupersacos.Columns[4].Width = 150;

                        dGVSupersacos.Columns[5].Name = "ESTATUS";
                        dGVSupersacos.Columns[5].HeaderText = "ESTATUS";
                        dGVSupersacos.Columns[5].Width = 70;

                        int i = 0, _producto_ID = 0;
                        double _sumaTanques = 0.0, _sumaOC = 0.0, _sumatotal = 0.0;
                        string _ProductoDesc = "", _Lote = "", _ssid = "", _fechacreacion = "",_estatus="";
                        tBInfoMSP.Text += "\r\n-------------------------------------------------------------------------------------------------" +
                            "\r\nDatos del sistema:";
                        List<string[,]> _pesosSQL = new List<string[,]>();
                        for (i = 0; i < _Datos.Rows.Count; i++)
                        {
                            _ssid = Convert.ToString(Convert.ToString(_Datos.Rows[i]["SUPERSACO_ID"]));
                            _producto_ID = Convert.ToInt32(Convert.ToString(_Datos.Rows[i]["PRODUCTO_ID"]));
                            _sumaTanques = Convert.ToDouble(_Datos.Rows[i]["MOD_REC_TANQ"]);
                            _sumaOC = Convert.ToDouble(_Datos.Rows[i]["MOD_POC_MSP"]);
                            _Lote = Convert.ToString(_Datos.Rows[i]["FOLIO"]);
                            _ProductoDesc = Convert.ToString(_Datos.Rows[i]["PRODUCTO_DESCRIPCION"]);
                            _fechacreacion = Convert.ToDateTime(_Datos.Rows[i]["SUPERSACO_FECHA"]).ToString("dd/MM/yyyy HH:mm:ss");
                            _estatus= Convert.ToString(_Datos.Rows[i]["ESTATUS"]);
                            dGVSupersacos.Rows.Add();
                            dGVSupersacos["SS_ID", i].Value = _ssid;
                            //dGVSupersacos["PRODUCTO_ID", i].Value = _producto_ID;
                            dGVSupersacos["SUPERSACO_PRODUCTO", i].Value = _ProductoDesc;
                            dGVSupersacos["LOTE_ID", i].Value = _Lote;
                            dGVSupersacos["ESTATUS", i].Value = _estatus;
                            dGVSupersacos["PESO_SS", i].Value = (_sumaOC + _sumaTanques).ToString("N2");
                            _sumatotal += _sumaOC + _sumaTanques;
                            //dGVSupersacos["CANT_REC_PETICIONOC", i].Value = _sumaOC.ToString("N2");
                            dGVSupersacos["FECHA_CREACION", i].Value = _fechacreacion;
                            if (_pesosSQL.Count == 0)
                            {
                                string[,] _Producto = new string[,] { { _ProductoDesc, (_sumaOC + _sumaTanques).ToString("N2") } };
                                _pesosSQL.Add(_Producto);
                            }
                            else
                            {
                                bool Existe = false;
                                for (int j = 0; j < _pesosSQL.Count; j++)
                                {
                                    string auxProd = _pesosSQL[j][0,0];
                                    if(auxProd==_ProductoDesc)
                                    {
                                        Existe = true;
                                        break;
                                    }
                                }
                                if (Existe == false)
                                {
                                    string[,] _Producto = new string[,] { { _ProductoDesc, (_sumaOC + _sumaTanques).ToString("N2") } };
                                    _pesosSQL.Add(_Producto);
                                }
                                else
                                {
                                    //Sumar a lo ya existente en la lista
                                    int q = 0;
                                    foreach (string[,] _pesoGuardado in _pesosSQL)
                                    {
                                        string auxProd = _pesoGuardado[0, 0];
                                        if (auxProd == _ProductoDesc)
                                        {
                                        double _sumTotales = Convert.ToDouble(_pesoGuardado[0, 1]);
                                        _pesosSQL[q][0, 1]=(_sumTotales+(_sumaOC + _sumaTanques)).ToString();
                                            break;
                                        }
                                            q++;
                                    }
                                }
                            }
                        }
                        if (_pesosSQL.Count > 0)
                        {
                            for (int j = 0; j < _pesosSQL.Count; j++)
                            {
                                dGVSupersacos.Rows.Add();
                                dGVSupersacos.Rows[i].Cells["SS_ID"].Style.BackColor = dGVSupersacos.BackgroundColor;
                                dGVSupersacos.Rows[i].Cells["SUPERSACO_PRODUCTO"].Value = _pesosSQL[j][0, 0];
                                //dGVSupersacos.Rows[i].Cells[""].Style.BackColor = dGVSupersacos.BackgroundColor;
                                dGVSupersacos.Rows[i].Cells["FECHA_CREACION"].Style.BackColor = dGVSupersacos.BackgroundColor;
                                dGVSupersacos.Rows[i].Cells["ESTATUS"].Style.BackColor = dGVSupersacos.BackgroundColor;
                                dGVSupersacos["LOTE_ID", i].Value = "TOTAL:";
                                dGVSupersacos.Rows[i].Cells["LOTE_ID"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                _sumatotal = Convert.ToDouble(_pesosSQL[j][0, 1]);
                                dGVSupersacos["PESO_SS", i].Value = (_sumatotal).ToString("N2");
                                int q = 0;
                                auxCantDoble = 0.0;
                                foreach (string[,] _pesoGuardado in _pesosMSP)
                                {
                                    string auxProd = _pesoGuardado[0, 0];
                                    if (auxProd == Convert.ToString(dGVSupersacos.Rows[i].Cells["SUPERSACO_PRODUCTO"].Value))
                                    {
                                        auxCantDoble = Convert.ToDouble(_pesoGuardado[0, 1]);
                                        break;
                                    }
                                    q++;
                                }

                                string info = "";
                                if (auxCantDoble - _sumatotal < 0)
                                    info = "(" + ((-1) * (auxCantDoble - _sumatotal)).ToString("N2") + " kg recibidos de más.)";
                                else if (auxCantDoble - _sumatotal > 0)
                                    info = "(" + (auxCantDoble - _sumatotal).ToString("N2") + " kg pendientes de recibir.)";
                                else if (auxCantDoble - _sumatotal == 0)
                                    info = "Lote recibido completamente.";
                                tBInfoMSP.Text += "\r\nProducto:"+ Convert.ToString(dGVSupersacos.Rows[i].Cells["SUPERSACO_PRODUCTO"].Value) 
                                    + "\r\nTotal kgs recibidos: " + (_sumatotal).ToString("N2")
                                    + "\r\nDiferencia: " + (auxCantDoble - _sumatotal).ToString("N2")
                                    + " " + info;
                                i++;
                            }
                        }
                        foreach (DataGridViewColumn Col in dGVSupersacos.Columns)
                        {
                            Col.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        _exito = true;
                    }
                    else
                        MessageBox.Show("Sin datos encontrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Sin datos para mostrar","Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Lote no especificado","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
        }

        private void bExportar_Click(object sender, EventArgs e)
        {
            DialogResult _resp = MessageBox.Show("¿Desea exportar la informacion de los super sacos a Excel?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (_resp == DialogResult.Yes)
            {
                string _titulo = "Reporte de súper sacos por lote",msg_local="",_datoMSP="";
                C_Funciones _Funciones = new C_Funciones();
                for(int i=0;i<_pesosMSP.Count;i++)
                {

                    _datoMSP += _pesosMSP[i][0, 0]+": "+ _pesosMSP[i][0, 1]+"\n\r";
                }

                _Funciones.ReporteLotesSS(_titulo, _datoMSP,pBAvance, sFDGuardar, dGVSupersacos, _Usuario.USUARIO, out msg_local);
            }
        }

        private void tBLoteID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Buscar();
            }

            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 44))
            {
                e.Handled = true;
                return;
            }
        }

        private void bRecepcionLote_Click(object sender, EventArgs e)
        {
            //Confirmaciones
            string _lote = tBLoteID.Text.ToString();
            DialogResult _fechaFumigacion= DialogResult.No, _estatusRecepcion = DialogResult.No, _estatusLote = DialogResult.No;
            DateTime _fechFum = new DateTime();
            string _estRecep = "", _estLot = "",msg_local="";
            if (_lote.Length > 0)
            {
                if (cBOpcionesLote.SelectedIndex > -1)
                {
                    _estRecep = Convert.ToString(cBOpcionesLote.SelectedItem);
                    _estatusRecepcion = MessageBox.Show("¿Desea asignar el estatus " + _estRecep + " a la recepcion del lote \"" + _lote + "\"", "Confirmación"
                        ,MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                }
                if (cBOcionConsumoLote.SelectedIndex > -1)
                {
                    _estLot = Convert.ToString(cBOcionConsumoLote.SelectedItem);
                    _estatusLote = MessageBox.Show("¿Desea asignar el estatus " + _estLot + " al lote \"" + _lote + "\"", "Confirmación"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                }

                if (dTPFechaFumigacion.Value != new DateTime())
                {
                    _fechFum = dTPFechaFumigacion.Value;
                    _fechaFumigacion = MessageBox.Show("¿Desea asignar la fecha de fumigacion " + _fechFum.ToString("dd-MM-yyyy") + " al lote \"" + _lote + "\"", "Confirmación"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                }
                if (_estatusRecepcion == DialogResult.No)
                    _estRecep = "";
                else
                {
                    if (_estRecep == "Terminar")
                        _estRecep = "T"+ _estRecep;
                    else if (_estRecep == "Cancelar")
                        _estRecep = "C" + _estRecep;
                }
                if (_estatusLote == DialogResult.No)
                    _estLot = "";
                else
                {
                    if (_estLot == "Consumido")
                        _estLot = "T"+ _estLot;
                    else if (_estLot == "Disponible")
                        _estLot = "A"+_estLot;
                    else if (_estLot == "Cancelado")
                        _estLot = "C"+ _estLot;
                }
                if (_fechaFumigacion == DialogResult.No)
                    _fechFum = new DateTime();
                DialogResult _cambios = MessageBox.Show("Se realizarán los siguientes cambios\n\r"
                    +"Estatus de recepcion: "+_estRecep.Substring(1,_estRecep.Length-1)+"\n\r" 
                    +"Estatus de lote: "+_estLot.Substring(1, _estLot.Length - 1) + "\n\r"
                    +"Fecha de fumigación:"+ _fechFum.ToString("dd-MM-yyyy")+"\n\r"
                    + "\r\n¿Desea Continuar?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if(_cambios==DialogResult.Yes)
                {
                    C_Funciones _Funciones = new C_Funciones();
                    if (_Funciones.ActualizarLote(_lote, _fechFum, _estRecep.Substring(0, 1), _estLot.Substring(0, 1), _Usuario, out msg_local))
                        MessageBox.Show("Datos actualizados correctamente","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    if(msg_local.Length>0)
                        MessageBox.Show(msg_local, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
