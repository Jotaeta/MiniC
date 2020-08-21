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


            // COMENTARIOS DE UNA LINEA TERMINAR (VIERNES) 
            // STRINGS  (VIERNES)
            // PALABRAS JUNTAS (MATCH O COMO HICIMOS LOS CARACTERES) (VIERNES O SABADO)
            // ESCRIBIR EN ARCHIVO (SABADO)
            // MANEJO DE ERRORES VERIFICARLOS (SABADO) 
            // OPTIMIZAR CODIGO (DEPENDE DE LAS INSTRUCCIONES)

            var listaTokens = new List<string>();
            var ruta = string.Empty;
            var contadorLinea = 1;
            var contadorColumna = 0;
            var esComentario = false;
            var hayCierreComentario = false;

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
                            //Linea con comentario de una linea FALTA COMPLETAR
                            if (A_lexico.regexComentarioLinea.IsMatch(lineaActual))
                            {
                                esComentario = true;
                                var splitBarra = lineaActual.Split("//");
                                var splitEspacio = splitBarra[0].Split(' ');
                                foreach (var item in splitEspacio)
                                {
                                    var inicioColumna = contadorColumna;
                                    contadorColumna += item.Length;
                                    //Implementar metodo para analisis de palabras
                                    contadorColumna += item.Length - 1;
                                    if (A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                    {
                                        listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
                                    }
                                }
                            }
                            //Linea con comentarios de multiple linea  en una linea FALTA COMPLETAR
                            else if (A_lexico.regexComentariosMultipleLine.IsMatch(lineaActual))
                            {
                                var splitInicioComentario = lineaActual.Split("/*");
                                var splitFinComentario = lineaActual.Split("*/");
                                //Agregar analisis
                            }
                            //Linea con comentarios de multiple linea COMPLETO
                            else if (A_lexico.regexComentariosMultipleLineCaso.IsMatch(lineaActual))
                            {
                                var splitInicioComentario = lineaActual.Split("/*");
                                var splitEspacio = splitInicioComentario[0].Split(" ");
                                foreach (var item in splitEspacio)
                                {
                                    var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
                                    //Caracter no permitido no viene 
                                    if (cantidadMatches.Count == item.Length && !esComentario)
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length;
                                        contadorColumna += item.Length - 1;
                                        listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
                                        contadorColumna = contadorColumna + 2;
                                    }
                                    //Caracter no permitido viene solo
                                    else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length;
                                        listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                    }
                                    //Caracter no permitido viene junto
                                    else
                                    {                                       
                                        var palabraConSNP = string.Empty;
                                        var inicioColumna = 0;
                                        foreach (var letra in item)
                                        {
                                            if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
                                            {
                                                palabraConSNP = palabraConSNP.Replace(" ", "");
                                                inicioColumna = contadorColumna;
                                                contadorColumna += palabraConSNP.Length;
                                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                                inicioColumna = contadorColumna;
                                                contadorColumna += letra.ToString().Length;
                                                listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                palabraConSNP = " ";
                                            }
                                            else
                                            {
                                                palabraConSNP += letra;
                                            }
                                        }
                                        if (palabraConSNP != " ")
                                        {
                                            palabraConSNP = palabraConSNP.Replace(" ", "");
                                            inicioColumna = contadorColumna;
                                            contadorColumna += palabraConSNP.Length;
                                            listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                        }
                                    }
                                }
                                //Se busca en archivo hasta encontrar el cierre de comentario
                                while (lineaActual != null)
                                {
                                    if (lineaActual.Contains("*/"))
                                    {
                                        hayCierreComentario = true;
                                        break;
                                    }
                                    lineaActual = reader.ReadLine();
                                }
                                //Si el archivo tiene cierre de comentario
                                if (hayCierreComentario)
                                {
                                    var splitFinComentario = lineaActual.Split("*/");
                                    splitEspacio = splitFinComentario[1].Split(" ");
                                    foreach (var item in splitEspacio)
                                    {
                                        var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
                                        //Caracter no permitido no viene 
                                        if (cantidadMatches.Count == item.Length && !esComentario)
                                        {
                                            var inicioColumna = contadorColumna;
                                            contadorColumna += item.Length;
                                            contadorColumna += item.Length - 1;
                                            listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
                                            contadorColumna = contadorColumna + 2;
                                        }
                                        //Caracter no permitido viene solo
                                        else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                        {
                                            var inicioColumna = contadorColumna;
                                            contadorColumna += item.Length;
                                            listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                        }
                                        //Caracter no permitido viene junto
                                        else
                                        {
                                            var palabraConSNP = string.Empty;
                                            var inicioColumna = 0;
                                            foreach (var letra in item)
                                            {
                                                if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
                                                {
                                                    palabraConSNP = palabraConSNP.Replace(" ", "");
                                                    inicioColumna = contadorColumna;
                                                    contadorColumna += palabraConSNP.Length;
                                                    listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                                    inicioColumna = contadorColumna;
                                                    contadorColumna += letra.ToString().Length;
                                                    listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                    palabraConSNP = " ";
                                                }
                                                else
                                                {
                                                    palabraConSNP += letra;
                                                }
                                            }
                                            if (palabraConSNP != " ")
                                            {
                                                palabraConSNP = palabraConSNP.Replace(" ", "");
                                                inicioColumna = contadorColumna;
                                                contadorColumna += palabraConSNP.Length;
                                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                            }
                                        }
                                    }
                                }
                                //Si el archivo no tiene cierre de comentario hay error
                                else
                                {
                                    listaTokens.Add("       En linea: " +contadorLinea+" abre comentario y nunca cierra");
                                }
                                //Implementar metodo para analisis de palabras
                            }
                            //Linea Normal separada por espacios COMPLETO... hasta el momento
                            else
                            {
                                foreach (var item in lineaActual.Split(' '))
                                {
                                    var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
                                    //No contiene caracter no permitido
                                    if (cantidadMatches.Count == item.Length && !esComentario)
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length - 1;
                                        listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
                                        contadorColumna = contadorColumna + 2;
                                    }
                                    //Caracter no permitido individual
                                    else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                    {
                                        var inicioColumna = contadorColumna;
                                        contadorColumna += item.Length;                                        
                                        listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                        contadorColumna = contadorColumna + 2;
                                    }
                                    //Caracter no permitido viene junto
                                    else
                                    {
                                        var palabraConSNP = string.Empty;
                                        var inicioColumna = 0;
                                        foreach (var letra in item)
                                        {
                                            if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
                                            {
                                                palabraConSNP= palabraConSNP.Replace(" ","");
                                                inicioColumna = contadorColumna;
                                                contadorColumna += palabraConSNP.Length;
                                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                                inicioColumna = contadorColumna;
                                                contadorColumna += letra.ToString().Length;
                                                listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                palabraConSNP = " ";
                                            }
                                            else
                                            {
                                                palabraConSNP += letra;
                                            }
                                        }
                                        if (palabraConSNP!=" ")
                                        {
                                            palabraConSNP= palabraConSNP.Replace(" ", "");
                                            inicioColumna = contadorColumna;
                                            contadorColumna += palabraConSNP.Length;
                                            listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                        }
                                    }
                                }
                            }
                            esComentario = false;
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
                    if (item != string.Empty)
                    {
                    Console.WriteLine(item);    
                    }
                }
                Console.ReadLine();
            }
        }
    }
}
