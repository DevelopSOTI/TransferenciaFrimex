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
using System.Collections;
using System.Globalization;
using System.IO;

namespace AnalizadorSintactico_Seguridad
{
    class MensajesDeError
    {
        //Variables locales:
        private string _E = "";
        //Constructor:
        public MensajesDeError(){
        }

        //Excepciones:

        public void Mostrar_excepcion(){
            MessageBox.Show(_E, "Lista de errores encontrados:", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //1.- Un solo caracter:
        public bool Excepcion_UnCaracter(string Error){
            _E += "Se ha introducido un solo caracter en la cadena de entrada:\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada debe de ser un nombre o conjunto de caracteres (ejemplo), en cambio solo se detecto uno, el cual no es suficiente para realizar validaciones en esta accion.";
            return true;
        }

        //2.- Caracteres no validos:
        public bool Excepcion_CaracterNoValido(string Error)
        {
            _E += "Se ha introducido un caracter no valido en la cadena de entrada:\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada no acepta este tipo de caracteres: solo aceptara letras y numeros como entrada de datos.";
            return true;
        }

        //3.- Cadena no validos:
        public bool Excepcion_CadenaNoValida(string Error)
        {
            _E += "Se ha introducido una cadena entre comillas.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada no acepta este tipo de cadenas de texto (venga la redundancia); por favor, retire las comillas y vuelva a intentarlo.";
            return true;
        }

        //4.- Cadena numerica no validos:
        public bool Excepcion_CadenaNumericaNoValida(string Error)
        {
            _E += "Se ha introducido una cadena no valida.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada solo acepta digitos (puede ser entero: 123, o decimal 123.123); por favor, introdusca de manera correcta los datos de entrada, y vuelva a intentarlo.";
            return true;
        }
        //5.- Cadena no validos:
        public bool Excepcion_CadenaSimpleNoValida(string Error)
        {
            _E += "Se ha introducido una cadena no valida.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada solo acepta letras (esto es un ejemplo); por favor, introdusca de manera correcta los datos de entrada, y vuelva a intentarlo.";
            return true;
        }
        //6.- Cadena no validos:
        public bool Excepcion_FuncionAritmetrica(string Error)
        {
            _E += "Se ha introducido una funcion no valida.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nLa cadena de entrada no acepta operaciones aritmetricas, ni funciones matematicas. por favor, introdusca de manera correcta los datos de entrada, y vuelva a intentarlo.";
            return true;
        }
        //7.- Cadena no validos:
        public bool Excepcion_PalabraReservada(string Error)
        {
            _E += "Se ha detectado una palabra reservada.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nCiertas palabras no pueden . si tiene alguna duda, contacte a soporte, e introdusca palabras que sean validas para el sistema.";
            return true;
        }
        //8.- Cadena no validos:
        public bool Excepcion_CadenaError(string Error)
        {
            _E += "Se ha introducido un caracter no valido dentro de la cadena.\r\nSe encontro: \"" +
                Error + "\".\r\n\r\nPor favor, introdusca de manera correcta los datos de entrada (evite utilizar simbolos y signos).";
            return true;
        }
        //9.- Cadena vacia:
        public bool Excepcion_CadenaVacia()
        {
            _E += "No se encontro entrada en el texto: Puede que el campo sea nulo o este vacio";
            return true;
        }
    }
}
