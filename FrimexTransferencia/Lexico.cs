using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrimexTransferencia
{
    public class Lexico
    {
        //Variables locales:
        ArrayList lexemas;
        AnalizadorSintactico_Seguridad.MensajesDeError MDE;
        string texto_entrada;
        char[] simbolos = {
        '"','\'','*','+','-','/','\\','|','%','~','{',
        '}', '[',']','(',')','=', '¡','?','¿','!','=',
        '_', '&','#','@','.',',', '^','`','>','<','$'
    };
        string[] PalabrasReservadas = {
        "ABORT","DEC","LEADING","RESET","ADMIN","DECIMAL","LEFT","REUSE","AGGREGATE","DECODE",
        "LIKE","RIGHT","ALIGN","DEFAULT","LIMIT","ROWS","ALL","DEFERRABLE","LISTEN","ROWSETLIMIT",
        "ALLOCATE","DESC","LOAD","RULE","ANALIZAR","DISTINCT","LOCAL","SEARCH","ANALYZE","DISTRIBUTE",
        "LOCK","SELECT","AND","DO","MATERIALIZED","SEQUENCE","ANY","ELSE","MINUS","SESSION_USER",
        "AS","END","MOVE","SETOF","ASC","EXCEPT","NATURAL","SHOW","BETWEEN","EXCLUDE",
        "NCHAR","SOME","BINARY","EXISTS","NEW","SUBSTRING","BIT","EXPLAIN","NOT","SYSTEM",
        "BOTH","EXPRESS","NONULL","TABLE","CASE","EXTEND","NULL","THEN","CAST","EXTERNAL",
        "NULLIF","TIES","CHAR","EXTRACT","NULLS","TIME","CHARACTER","FALSE","NUMERIC","TIMESTAMP",
        "CHECK","FIRST","NVL","TO","CLUSTER","FLOAT","NVL2","TRAILING","COALESCE","FOLLOWING",
        "OFF","TRANSACTION","COLLATE","FOR","OFFSET","TRIGGER","COLLATION","FOREIGN","OLD","TRIM",
        "COLUMN","FROM","ON","TRUE","CONSTRAINT","FULL","ONLINE","UNBOUNDED","COPY","FUNCTION",
        "ONLY","UNION","CROSS","GENSTATS","OR","UNIQUE","CURRENT","GLOBAL","ORDER","USER",
        "CURRENT_CATALOG","GROUP","OTHERS","USING","CURRENT_DATE","HAVING","OUT","VACÍO","CURRENT_DB","IDENTIFIER_CASE",
        "OUTER","VARCHAR","CURRENT_SCHEMA","ILIKE","OVER","VERBOSE","CURRENT_SID","IN","OVERLAPS",
        "VERSION","CURRENT_TIME","INDEX","PARTITION","VIEW","CURRENT_TIMESTAMP","INITIALLY","POSITION","WHEN","CURRENT_USER",
        "INNER","PRECEDING","WHERE","CURRENT_USERID","INOUT","PRECISION","WITH","CURRENT_USEROID","INTERSECT","PRESERVE",
        "WRITE","DEALLOCATE","INTERVAL","PRIMARY","RESET","INTO","REUSE","DELETE","FORMAT","CLEAR",
        "RELEASE","RENEW","DROP","TRUNCATE","INT","DOUBLE","FLOAT","CHAR","PRIMARY","KEY",
        "NOT","NULL","EMPTY","BOOLEAN","BOOL","TRUE","FALSE","YES","NO","DATABASE"
    };

        //Constructor:
        public Lexico(string Entrada)
        {
            lexemas = new ArrayList();
            texto_entrada = Entrada;
            MDE = new AnalizadorSintactico_Seguridad.MensajesDeError();
        }

        private char SigCar(int i)
        {
            return texto_entrada[i];                                        //Retorna un caracter
        }

        private bool generar_arreglo()
        {
            int i = 0;
            string lexema = "";
            if (i < (texto_entrada.Length))
            {
                do
                {
                    char c = SigCar(i);                                     //Conseguimos el siguiente caracter
                    if (c == ' ' && i < (texto_entrada.Length - 1))         //Si es un espacio en blanco y aun no sobrepasa el tamaño de la cadena, lo saltamos:
                        i++;
                    else if (c == ' ' && i == (texto_entrada.Length - 1))   //Si no conseguimos saltarnos este espacio, terminamos el arreglo de cadenas y retornamos:
                        return true;
                    else
                    {                                                       //Si tiene un caracter (cual sea), verificamos que se puede sacar de ahi:
                        bool next_word = false;
                        bool is_a_string = false;
                        char value_selected = '"';
                        if (SigCar(i) == '\'')
                        {
                            value_selected = '\'';
                            is_a_string = true;
                        }
                        else if (SigCar(i) == '"')
                        {
                            value_selected = '"';
                            is_a_string = true;
                        }
                        do
                        {
                            if (is_a_string)                                //Si es una cadena de texto con "" o ''
                            {
                                if ((i + 1) < texto_entrada.Length)
                                {        //Si el sig. valor del contador (+1) aun no sobrepasa el tamaño del texto, aumentamos su valor
                                    lexema += SigCar(i);                    //vamos construyendo la palabra:
                                    if (SigCar(i + 1) == value_selected)
                                    {   //Si encontramos las comillas faltantes, lo agregamos y terminamos la instruccion
                                        i++;
                                        lexema += SigCar(i++);
                                        next_word = true;
                                    }
                                    else                                    //Si aun no encuentra los caracteres para cerrar, que termine aqui
                                        i++;
                                }
                                else
                                {                                       //Si llego a su limite, retornamos el arreglo
                                    lexema += SigCar(i);
                                    lexemas.Add(lexema);
                                    lexema = "";
                                    return true;
                                }

                            }

                            else                                            //Si es una cadena de texto sin "" o ''
                            {
                                if ((i + 1) < texto_entrada.Length)
                                {        //Si el sig. valor del contador (+1) aun no sobrepasa el tamaño del texto, aumentamos su valor
                                    lexema += SigCar(i);                    //vamos construyendo la palabra:
                                    if (SigCar(i + 1) != ' ')
                                        i++;
                                    else
                                    {
                                        i++;                                //saltamos el espacio
                                        next_word = true;
                                    }
                                }
                                else
                                {                                       //Si llego a su limite, retornamos el arreglo
                                    lexema += SigCar(i);
                                    lexemas.Add(lexema);
                                    lexema = "";
                                    return true;
                                }
                            }
                        } while (next_word == false);
                        lexemas.Add(lexema);
                        lexema = "";
                    }
                } while (i < texto_entrada.Length);
            }
            return true;
        }

        //Este es el que se manda a llamar:
        public bool Analiza(char tipo_analisis)
        {
            if (texto_entrada.Contains(' ') && (tipo_analisis == 'b' || tipo_analisis == 'c' || tipo_analisis == 'd'))
            {
                return true;
            }

            if (texto_entrada.Length == 0)
            {
                //Esta vacio
                return true;
            }
            else
            {
                generar_arreglo();
                for (int i = 0; i < lexemas.Count; i++)
                {
                    switch (noAuto(lexemas[i].ToString()))
                    {
                        //Analizaremos los casos de entrada:
                        /*============================================================================================//
                                                                No validos
                        //============================================================================================*/

                        //Simbolo
                        /*--------------------------------------------------------------------------------------------*/
                        case 0:
                            return true;
                        // break;

                        //Caracter (ej: 'A')
                        /*--------------------------------------------------------------------------------------------*/
                        case 3:
                            return true;
                        // break;

                        //Cadena ("ejemplo" / 'ejemplo')
                        /*--------------------------------------------------------------------------------------------*/
                        case 4:
                            return true;
                        // break;

                        //Palabra reservada
                        /*--------------------------------------------------------------------------------------------*/
                        case 5:
                            return true;
                        // break;

                        /*============================================================================================//
                                                                Validos
                        //============================================================================================*/

                        //Letra
                        /*--------------------------------------------------------------------------------------------*/
                        case 1:
                        //Numero
                        /*--------------------------------------------------------------------------------------------*/
                        case 2:
                        //Cadena (ejemplo)
                        /*--------------------------------------------------------------------------------------------*/
                        case 6:

                            //Comenzamos la revision a profundidad de las palabras presentes para validar si pasaran a la siguiente etapa.
                            string _string = lexemas[i].ToString();
                            int number_of_dots = 0;
                            bool es_oR = false;

                            //Verificamos si las palabras aqui no tienen simbologia:
                            for (int j = 0; j < _string.Length; j++)
                            {
                                switch (tipo_analisis)
                                {
                                    //Verificamos si es una cadena de texto (exclusivamete con puras letras: example)
                                    /*--------------------------------------------------------------------------------------------*/
                                    case 'a':
                                        if (!char.IsLetter(_string[j]))
                                            return true;
                                        break;
                                    //Verificamos si es un numero (exclusivamete con puros numeros y puntos: 123.45)
                                    /*--------------------------------------------------------------------------------------------*/
                                    case 'b':
                                        if (noAuto(lexemas[i].ToString()) == 2)
                                        {
                                            if ((!char.IsNumber(_string[j]) || (_string[j] != '.')) && number_of_dots > 1)
                                                return true;
                                            else if (_string[j] == '.')
                                                number_of_dots++;
                                        }
                                        else
                                            return true;
                                        break;
                                    //Verificamos si es una cadena de caracteres (extenciones maximas para correo my_cuttie-example@example.com)
                                    /*--------------------------------------------------------------------------------------------*/
                                    case 'c':
                                        if (es_oR == false)
                                        {
                                            if (es_opRel(lexemas[i].ToString()))
                                                return true;
                                            es_oR = true;
                                        }

                                        else
                                        {
                                            if (!char.IsNumber(_string[j]))
                                                if (!char.IsLetter(_string[j]))
                                                    if (_string[j] != '@')
                                                        if (_string[j] != '_')
                                                            if (_string[j] != '-')
                                                                if (_string[j] != '.')
                                                                    return true;
                                        }
                                        break;
                                    //Ahora solo acepta texto sin simbologia
                                    /*--------------------------------------------------------------------------------------------*/
                                    case 'd':
                                        if (es_oR == false)
                                        {
                                            if (lexemas[i].ToString().Contains("*+-*/%"))
                                                return true;
                                            es_oR = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Se encontro: \"" + _string[j] + "\"");
                                            if (!char.IsNumber(_string[j]))
                                                if (!char.IsLetter(_string[j]))
                                                    return true;
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return false;
            }
        }

        private bool es_opRel(string lex)
        {
            bool es_opRel = false;
            bool primera_validacion = true;
            for (int i = 0; i < lex.Length; i++)
            {
                if (primera_validacion)
                {
                    if (char.IsNumber(lex[i]))
                        es_opRel = true;
                    else if ((lex[i] == '+') || (lex[i] == '-') || (lex[i] == '*') || (lex[i] == '/') || (lex[i] == '%'))
                        es_opRel = true;
                    //Si contiene otros caracteres que no sean numeros, se trata de una cadena, o contraseña con simbologia
                    else
                    {
                        primera_validacion = false;
                        es_opRel = false;
                    }
                }
            }
            return es_opRel;
        }

        private int noAuto(string _lex)
        {
            //Variables de apoyo:
            double _rd;
            int _ri;

            //Primero verificamos si es un simbolo:
            if (_lex.Length == 1)
            {
                for (int i = 0; i < simbolos.Length; i++)
                    if (_lex == simbolos[i].ToString())
                        return 0;

                //o verificamos si es una letra:
                if (char.IsLetter(char.Parse(_lex)))
                    return 1;
            }

            //Ahora verificamos si no es un numero:
            else if (double.TryParse(_lex, out _rd) || int.TryParse(_lex, out _ri))
                return 2;

            //Ahora verificamos si no es un caracter entre comillas (por lo general la long. de este sera de 3):
            else if (_lex.Length == 3 && _lex[0] == '\'' && _lex[_lex.Length - 1] == '\'')
                return 3;

            //Ahora verificamos si no es una cadena entre comillas simples o dobles:
            else if (_lex[0] == '"' || _lex[0] == '\'')
            {
                return 4;
            }

            //Ahora verificamos si no es una palabra reservada la que se muestra en la lista
            else
                for (int i = 0; i < PalabrasReservadas.Length; i++)
                    if (_lex.ToUpper() == PalabrasReservadas[i])
                        return 5;

            //si no encontramos alguna anomalia:
            return 6;
        }
    }
}
