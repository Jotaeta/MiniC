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
                            if (A_lexico.regexComentarioLinea.IsMatch(lineaActual))
                            {
                                var inicioColumna = contadorColumna;
                                contadorColumna += lineaActual.Length;
                                var tokenEscribir = (lineaActual + " Es Comentario Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                listaTokens.Add(tokenEscribir);
                            }
                            else if (A_lexico.regexString.IsMatch(lineaActual))
                            {
                                // modificar
                            }
                            else
                            {
                                foreach (var item in lineaActual.Split(' '))
                                {
                                    var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
                                    if (cantidadMatches.Count == item.Length)
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length - 1;
                                        if (A_lexico.regexPalabrasReservadas.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Palabra Reservada Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexBool.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Bool Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        //Implementar comentario multiples lineas
                                        else if (A_lexico.regexComentariosMultipleLine.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Bool Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexDigitos.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es un Digito Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexDouble.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Double Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexDoubleExponencial.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Double Exponencial Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexHexadecimal.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Hexadecimal Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexOperadores.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Operador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        else if (A_lexico.regexIdentificadores.IsMatch(item))
                                        {
                                            var tokenEscribir = (item + " Es Identificador Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                            listaTokens.Add(tokenEscribir);
                                        }
                                        contadorColumna = contadorColumna + 2;
                                    }
                                    else
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length;
                                        var tokenEscribir = (item + " Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                    }
                                }
                            }

                            lineaActual = reader.ReadLine();
                            contadorLinea++;
                            contadorColumna = 0;
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
