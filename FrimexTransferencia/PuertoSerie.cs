using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;
namespace FrimexTransferencia
{
    class PuertosSerie
    {
        private bool _estatus = false;
        private string strBufferIn;
        private string strBufferOut;
        private SerialPort SpPuertos = new SerialPort();    
        public string BufferIn
        {
            set { strBufferIn = value; }
            get { return strBufferIn; }
        }
        public string BufferOut
        {
            set { strBufferOut = value; }
            get { return strBufferOut; }
        }
        public bool EstaConectado
        {
            set { _estatus = value; }
            get { return _estatus; }
        }
        public SerialPort PuertoSerie
        {
            set { SpPuertos = value; }
            get { return SpPuertos; }
        }
        public PuertosSerie()
        {
            Inicializar();
        }
        private void Inicializar()
        {
            strBufferIn = "";
            strBufferOut = "";
            //BConectar.Enabled= false;
            //BEnviar.Enabled=false;

        }
        public bool BuscarPuertos(out List<string> Puertos)
        {
            bool _existencia = false;
            Puertos = new List<string>();
            string[] PuertosDisponibles = SerialPort.GetPortNames();
            foreach (string puerto_simple in PuertosDisponibles)            
                Puertos.Add(puerto_simple);            
            if (Puertos.Count > 0)
            {
                _existencia = true;
                //BConectar.Enabled=true;
            }
            else
            {
                MessageBox.Show("Ningun puerto de trabajo","Mensaje de la aplicación",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Puertos.Clear();
                Puertos.Add("                    ");
                Inicializar();
                _existencia = false;
            }
            return _existencia;
        }
        public void ConectarPuerto(string Puerto,string baudRate)
        {
            try
            {
                if(true)//(BConectar.Text=="Conectar")
                {
                    PuertoSerie.BaudRate = Int32.Parse(baudRate);
                    PuertoSerie.DataBits = 8;
                    PuertoSerie.Parity = Parity.None;
                    PuertoSerie.StopBits = StopBits.One;
                    PuertoSerie.Handshake = Handshake.None;
                    PuertoSerie.ReadTimeout = 1000;
                    PuertoSerie.PortName = Puerto;
                    try
                    {
                        PuertoSerie.Open();
                        //BConectar.Text="Desconectar";
                        //BEnviaDatos.Enabled=true;
                        if (PuertoSerie.IsOpen)
                            _estatus = true;
                        else
                            _estatus = false;
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        _estatus = false;
                    }
                }
                /*else//if(true)//(BConectar.Text=="Desconectar")
                {
                    PuertoSerie.Close();
                    _estatus = false;
                    //BConectar.Text="Conectar";
                    //BEnviaDatos.Enabled=false;

                }*/
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning  );
                _estatus = false;
            }
        }
        public void DesconectarPuertoSerie()
        {
            try
            {
                if (PuertoSerie.IsOpen)
                { 
                    PuertoSerie.Close();
                    _estatus = false;
                }
                else
                    MessageBox.Show("El puerto está cerrado", "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EnviarDatos(string Enviar)
        {
            try
            {
                PuertoSerie.DiscardOutBuffer();
                BufferOut = Enviar;
                PuertoSerie.Write(BufferOut);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public string DatoRecibido()
        {
            string DatoEntrada = "";
            try
            {
                DatoEntrada = PuertoSerie.ReadExisting();
                if (DatoEntrada.Length >= 34)
                {
                    int intBuffer;
                    intBuffer = PuertoSerie.BytesToRead;
                    byte[] byteBuffer = new byte[intBuffer];
                    PuertoSerie.Read(byteBuffer, 0, intBuffer);
                    PuertoSerie.NewLine = Convert.ToChar('\u0003').ToString();
                    string aux = "";
                    aux=PuertoSerie.ReadLine();
                    DatoEntrada = aux;
                }
                else
                    DatoEntrada = "0.0000";
                 
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return DatoEntrada;
        }
        public string DatoRecibidoAlmacenGeneral()
        {
            string DatoEntrada = "";
            try
            {
                DatoEntrada = PuertoSerie.ReadExisting();
                if (DatoEntrada.Length >= 6)
                {
                    int longitud = DatoEntrada.Length - 7;
                    DatoEntrada = DatoEntrada.Substring(longitud, 7);
                }
                else
                    DatoEntrada = "0.0000";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return DatoEntrada;
        }

        /* private SerialPort PuertoSerie;
         private string ping;
         private string Abriendo;
         private string Cerrando;
         private string regresarToken;
         bool estaRecibiendo;

         public PuertosSerie(string comPort = "Com1", int baud = 9600, System.IO.Ports.Parity parity = System.IO.Ports.Parity.None, int dataBits = 8, System.IO.Ports.StopBits stopBits = System.IO.Ports.StopBits.One, string ping = "*IDN?", string opening = "REMOTE", string closing = "LOCAL", string returnToken = ">")
         {
             this.ping = ping;
             this.Abriendo = opening;
             this.Cerrando = closing; 
             this.regresarToken = returnToken;

             try
             {
                 PuertoSerie = new SerialPort(comPort, baud, parity, dataBits, stopBits);
                 PuertoSerie.NewLine = returnToken;
                 PuertoSerie.ReadTimeout = 1000;
                 PuertoSerie.RtsEnable = true;
                 PuertoSerie.DtrEnable = true;
             }
             catch (Exception e)
             {
                 PuertoSerie = null;
             }
         }

         public string AbrirConexionSerial()
         {
             try
             {
                 PuertoSerie.Open();
                 PuertoSerie.DiscardInBuffer();
                 PuertoSerie.Write(Abriendo + "\r");
                 System.Threading.Thread.Sleep(100);
                 PuertoSerie.DiscardInBuffer(); 
             }
             catch (Exception e)
             {
                 return "No se pudo abrir la conexión del puerto serie. Excepción:" + e.ToString(); ;
             }
             string test = EscribirConexionSerial(ping);
             return test;
         }

         public string EscribirConexionSerial(string serialCommand)
         {
             string recivido = "";
             try
             {
                 PuertoSerie.Write(serialCommand + "\r");
                 System.Threading.Thread.Sleep(100);
                 recivido += PuertoSerie.ReadLine();
                 if (recivido.Contains(">"))
                 {
                     return recivido;
                 }
                 else
                 {
                     throw new Exception("¡La máquina todavía está escribiendo en el búfer!");
                 }
             }
             catch (Exception e)
             {
                 bool sigueReciviendo = true;
                 while (sigueReciviendo)
                 {
                     string prueba = "";
                     try
                     {
                         System.Threading.Thread.Sleep(500);
                         prueba += PuertoSerie.ReadLine();
                         if (prueba == recivido | prueba.Length <= recivido.Length)
                         {
                             sigueReciviendo = false;
                             recivido = "Un error fue encontrado mientras recivia datos de la maquina. Salida Final: " + recivido + " | " + prueba + " | " + e.ToString();
                         }
                     }
                     catch (Exception ex)
                     {
                         if (prueba == recivido | prueba.Length <= recivido.Length)
                         {
                             sigueReciviendo = false;
                             recivido = "Un error fue encontrado mientras recivia datos de la maquina. Salida Final: " + recivido + " | " + prueba + " | " + e.ToString() + " | " + ex.ToString();
                         }
                     }
                 }
             }

             return recivido;
         }

         public bool CerrarConexionSerial()
         {
             try
             {
                 PuertoSerie.Write("LOCAL\r");
                 System.Threading.Thread.Sleep(100);
                 string test = PuertoSerie.ReadLine();
                 PuertoSerie.DiscardInBuffer();
                 PuertoSerie.Close();
                 PuertoSerie.Dispose();
                 return true;
             }
             catch (Exception e)
             {
                 return false;
             }
         }*/
    }
}
