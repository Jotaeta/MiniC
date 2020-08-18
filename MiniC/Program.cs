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
            
            var listaTokens = new List<string>();
            var ruta = string.Empty;
            var contadorLinea = 1;
            var contadorColumna = 0;
            Console.WriteLine("\n\t Allan Davila 1160118\n\t Jonathan Argueta 1029418\n");            
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Ingrese Ruta de Archivo: ");
            ruta = Console.ReadLine();
            if (!File.Exists(ruta))
            {
                Console.WriteLine("Ingrese un Archivo Valido");
            }
            else
            {
                using (var reader = new StreamReader(new FileStream(ruta, FileMode.Open)))
                {
                    try
                    {
                        var lineaActual = string.Empty;
                        lineaActual = reader.ReadLine();
                        while (lineaActual != null)
                        {
                            foreach (var item in lineaActual.Split(' '))
                            {
                                if (A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                {
                                    if (A_lexico.regexPalabrasReservadas.IsMatch(item))
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna = +item.Length;
                                        var tokenEscribir = (item + " Es Palabra Reservada Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                        listaTokens.Add(tokenEscribir);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Contiene simbolos no permitidos");
                                }
                            }
                            lineaActual = reader.ReadLine();
                            contadorLinea++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Hay error.");
                    }
                    reader.Close();
                }
                foreach (var item in listaTokens)
                {
                    Console.WriteLine(item);
                }
                Console.ReadLine();
            }
        }
    }
}
