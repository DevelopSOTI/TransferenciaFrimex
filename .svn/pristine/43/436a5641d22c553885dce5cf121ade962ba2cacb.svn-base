using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrimexTransferencia
{
    class CBascula
    {
        //PuertosSerie _PuertoSerie = new PuertosSerie();
        public bool BuscarPuertos(PuertosSerie _PuertoSerie ,out bool _basculaConectada,out List<string> PuertosDisponibles,out string msg)
            
        {
            bool _exito = false,_estatusBascula=false;
            string msg_local = "";
            List<string> _listaPuertos=new List<string>();
            try
            {   
                if (_PuertoSerie.BuscarPuertos(out _listaPuertos) == true)
                {
                    if(_listaPuertos.Count >0)
                    {
                        PuertosDisponibles = _listaPuertos;
                        _exito = true;
                    }
                    else
                        msg_local = "Puertos no encontrados";
                }
                else
                    msg_local="Sin puertos disponibles";               
            }
            catch (Exception Ex)
            {
                msg_local=Ex.Message;
            }
            PuertosDisponibles = _listaPuertos;
           _basculaConectada= _estatusBascula;
            msg = msg_local;
            return _exito;
        }
        public bool ConectarPuerto(PuertosSerie _PuertoSerie, string COM,string puerto, out string msg)
        {
            string msg_local = "";
                bool _conexion = false;
            try
            {
                _PuertoSerie.ConectarPuerto(COM, puerto);
                if (_PuertoSerie.EstaConectado)
                {
                    _conexion = true;
                }
                else
                    _conexion = false;
            }
            catch (Exception Ex)
            {
                msg_local = Ex.Message;
            }
            msg = msg_local;
            return _conexion;
        }
    }
}
