using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MiniC
{
    class Program
    {
       
        static void Main(string[] args)
        {
            #region REGEX
            Regex Palabras_Reservadas = new Regex(@"^(void|vo|int|double|bool|string|class|const|interface|null|this|for|while|foreach|if|else|return|break|New|NewArray|Console|WriteLine)$");
            Regex Simbolos_Permitidos = new Regex(@"^(a|b|v|o|i|d|vo)$");
            #endregion

            List <string> Tokens = new List <string>();

            var ruta = string.Empty;
            var extension = string.Empty;
            var aux = string.Empty;
            var texto = string.Empty;
            var linea = 1;

            Console.WriteLine("\n\t Allan Davila 1160118\n\t Jonathan Argueta 1029418");


            Console.WriteLine("Ingrese Archivo: ");
            //ruta = Console.ReadLine();
            ruta = @"C:\Users\jotae\OneDrive\Escritorio\Pruebas.txt";
            extension = Path.GetExtension(ruta);

            if (!extension.Equals(".txt"))
            {
                Console.WriteLine("Archivo de entrada no coincide con extension");
            }
            if (!File.Exists(ruta))
            {
                Console.WriteLine("Ingrese un Archivo Valido");
            }


            TextReader LeerArchivo = new StreamReader(ruta);
            texto = LeerArchivo.ReadToEnd();
            Console.WriteLine(texto);

            foreach (var letra in texto)
            {
                aux += letra;

                if (letra == ' '|| letra == '\t'|| letra == '\r')
                {
                    aux = string.Empty;
                }
                if (aux == " "||aux == "\t"||aux == "\r") {
                    aux = string.Empty;
                }
                else if (letra == '\n')
                {
                    aux = string.Empty;
                    linea++;
                }

                if (aux == " ") 
                {
                    aux += letra;
                }

                if (Simbolos_Permitidos.IsMatch(aux))
                {
                    if (Palabras_Reservadas.IsMatch(aux))
                    {
                        Tokens.Add("Palabra reservada " + aux + " en linea " + linea);
                        aux = string.Empty;
                    }
                }
                else {
                    Tokens.Add("Simbolo No valido " + aux + " en linea " + linea) ;
                    aux = string.Empty;
                }


            }

            foreach (var item in Tokens)
            {
                Console.WriteLine(item);
            }

        }     
    }
}
