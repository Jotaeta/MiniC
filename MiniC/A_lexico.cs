using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
namespace MiniC
{
    public class A_lexico
    {
        public static Regex regexPalabrasReservadas = new Regex(@"(void|int|double|bool|string|class|const|interface|null|this|for|while|foreach|if|else|return|break|New|NewArray|Console|WriteLine)");
        public static Regex regexIdentificadores = new Regex(@"[a-zA-z]+(\w)*");
        //Falta completar
        public static Regex regexComentarioLinea = new Regex(@"\/\/.*(?!\*\/)\n");
        public static Regex regexComentariosMultipleLine = new Regex(@"\/\*.*\*\/");
        public static Regex regexBool = new Regex(@"(true|false)");
        public static Regex regexDigitos = new Regex(@"\d*");
        public static Regex regexHexadecimal = new Regex(@"0[Xx][a-fA-F0-9+]+");
        public static Regex regexDouble = new Regex(@"\d+\.\d*");
        public static Regex regexDoubleExponencial = new Regex(@"\d+\.E\+\d+");
        //Falta completar
        public static Regex regexString = new Regex("\"([^\"]*)\"");
        public static Regex regexOperadores = new Regex(@"(\+|\-|\*|\/|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\})");
        public static Regex regexSimbolosPermitidos = new Regex(@"(\+|\-|\*|\/|\%|\<|\<\=|\>|\>\=|\=|\=\=|\!\=|\&\&|\|\||\!|\;|\,|\.|\[|\]|\(|\)|\{|\}|\[\]|\(\)|\{\}|[A-Za-z0-9])");
    }
}
