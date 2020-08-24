using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace MiniC
{
    public class A_lexico
    {
        public static Regex regexEspacioEnBlanco = new Regex(@"\s");
        public static Regex regexPalabrasReservadas = new Regex(@"^(void|int|double|bool|string|class|const|interface|null|this|for|while|foreach|if|else|return|break|New|NewArray|Console|WriteLine)$");
        public static Regex regexIdentificadores = new Regex(@"^[a-zA-z]+(\w)*$");
        //Falta completar
        public static Regex regexComentarioLinea = new Regex(@"\/\/.*$");
        public static Regex regexComentariosMultipleLine = new Regex(@"\/\*(.|\t|\n|\r\n|\n\r)*(\*\/)");
        public static Regex regexComentariosMultipleLineCaso = new Regex(@"\/\*(.|\t|\n|\r\n|\n\r)*");
        public static Regex regexBool = new Regex(@"^(true|false)$");
        public static Regex regexDigitos = new Regex(@"^[\d]*$");
        public static Regex regexHexadecimal = new Regex(@"^0[Xx][a-fA-F0-9+]+$");
        public static Regex regexDouble = new Regex(@"^\d+\.\d*$");
        public static Regex regexDoubleExponencial = new Regex(@"^\d+\.E\+\d+$");
        //Falta completar
        public static char asciiComillas = '"';
        public static Regex regexString = new Regex("");
        public static Regex regexOperadores = new Regex(@"^(\+|\-|\*|\/|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\})$");
        public static Regex regexSimbolosPermitidos = new Regex(@"(\+|\s|\-|\*|\/|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\}|\w)");

        public static string AnalisisPalabras(string palabra, int contadorLinea, int inicioColumna, int contadorColumna)
        {
            var resultadoAnalisis = string.Empty;
            if (regexEspacioEnBlanco.IsMatch(palabra) || palabra == "")
            {
                return resultadoAnalisis;
            }
            if (regexPalabrasReservadas.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "        Es Palabra Reservada Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexBool.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "         Es Bool Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDigitos.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "         Es un Digito Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDouble.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "        Es Double Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDoubleExponencial.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "        Es Double Exponencial Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexHexadecimal.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "        Es Hexadecimal Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexOperadores.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "         Es Operador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexIdentificadores.IsMatch(palabra))
            {
                resultadoAnalisis = (palabra + "        Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            return resultadoAnalisis;
        }
    }
}
