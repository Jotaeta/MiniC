using System;
using System.Text.RegularExpressions;

namespace MiniC
{
    public class A_lexico
    {
        public static Regex regexEspacioEnBlanco = new Regex(@"\s");
        public static Regex regexPalabrasReservadas = new Regex(@"^(void|int|double|bool|string|class|const|interface|null|this|for|while|foreach|if|else|return|break|New|NewArray|Console|WriteLine)$");
        public static Regex regexPalabrasReservadas2 = new Regex(@"(void|int|double|bool|string|class|const|interface|null|this|for|while|foreach|if|else|return|break|New|NewArray|Console|WriteLine)");
        public static Regex regexIdentificadores = new Regex(@"^[a-zA-z]+(\w)*$");
        public static Regex regexIdentificadores2 = new Regex(@"[a-zA-z]+(\w)*");
        //Falta completar
        public static Regex regexComentarioLinea = new Regex(@"\/\/.*$");
        public static Regex regexComentariosMultipleLine = new Regex(@"\/\*(.|\t|\n|\r\n|\n\r)*(\*\/)");
        public static Regex regexComentariosMultipleLineCaso = new Regex(@"\/\*(.|\t|\n|\r\n|\n\r)*");
        public static Regex regexBool = new Regex(@"^(true|false)$");
        public static Regex regexDigitos = new Regex(@"^[0-9]*$");
        public static Regex regexHexadecimal = new Regex(@"^0[Xx][a-fA-F0-9+]+$");
        public static Regex regexDouble = new Regex(@"^[0-9]+\.[0-9]*$");
        public static Regex regexDoubleExponencial = new Regex(@"^[0-9]+\.([0-9])*(E|e)(\+|\-)?[0-9]+$");
        //Falta completar
        public static char asciiComillas = '"';
        public static Regex regexOperadores = new Regex(@"^(\+|\-|\*|\/|\:|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\})$");
        public static Regex regexSimbolosPermitidos = new Regex(@"(\+|\s|\:|\-|\*|\/|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\}|\w)");
        public static Regex regexIden = new Regex(@"^(\w)(;)$");
        public static Regex regexFinComentario = new Regex(@"^*/$");
      
        public static string AnalisisPalabras(string palabra, int contadorLinea, int inicioColumna, int contadorColumna)
        {
            var resultadoAnalisis = string.Empty;
            if (regexEspacioEnBlanco.IsMatch(palabra) || palabra == "")
            {
                return resultadoAnalisis;
            }
            if (regexPalabrasReservadas.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "        Es Palabra Reservada Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexBool.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "         Es Bool Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDigitos.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "         Es un Digito Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDouble.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "        Es Double Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexDoubleExponencial.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "        Es Double Exponencial Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexHexadecimal.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "        Es Hexadecimal Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexOperadores.IsMatch(palabra))
            {
                return resultadoAnalisis = (palabra + "         Es Operador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexFinComentario.IsMatch(palabra))
            {
                Console.WriteLine(palabra + "        Es Fin de Comentario Sin Emparejar Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                return resultadoAnalisis = (palabra+ "        Es Fin de Comentario Sin Emparejar Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexIdentificadores.IsMatch(palabra))
            {
                if (palabra.Length>31)
                {
                    return resultadoAnalisis = (palabra.Substring(0,31) + "        Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");

                }
                return resultadoAnalisis = (palabra + "        Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            else if (regexIden.IsMatch(palabra))
            {
                if (palabra.Length > 31)
                {
                    return resultadoAnalisis = (palabra.Substring(0, 31) + "        Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");

                }
                return resultadoAnalisis = (palabra + "        Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            }
            return "" ;
        }

        public static string AnalisisJuntas(string palabra)
        {
            if (regexPalabrasReservadas2.IsMatch(palabra))
            {
                return regexPalabrasReservadas2.Match(palabra).Value.ToString();
            }
            else if (regexIdentificadores2.IsMatch(palabra))
            {
                return regexIdentificadores2.Match(palabra).Value.ToString();
            }
            return "";
        }

        public static int AnalisisTodo(string palabra)
        {
            var resultadoAnalisis = 0;
            if (regexEspacioEnBlanco.IsMatch(palabra) || palabra == "")
            {
                return resultadoAnalisis;
            }
            if (regexPalabrasReservadas.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexBool.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexDigitos.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexDouble.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexDoubleExponencial.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexHexadecimal.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexOperadores.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            else if (regexIdentificadores.IsMatch(palabra))
            {
                resultadoAnalisis++;
            }
            return resultadoAnalisis;
        }
    }
}
