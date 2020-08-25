using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
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
                            //Linea contiene comillas
                            if (lineaActual.Contains(A_lexico.asciiComillas))
                            {
                                var textoString = string.Empty;
                                var cantidadComillas = 0;
                                foreach (var item in lineaActual)
                                {
                                    var inicioColumna = contadorColumna;
                                    contadorColumna += textoString.Length;
                                    //Implementar metodo para analisis de palabras
                                    contadorColumna += textoString.Length - 1;
                                    if (item == '"' && cantidadComillas == 0)
                                    {
                                        cantidadComillas++;
                                        textoString += item;
                                    }
                                    else if (item != '"')
                                    {
                                        textoString += item;
                                    }
                                    else if (item == '"' && cantidadComillas != 0)
                                    {
                                        cantidadComillas++;
                                        textoString += item;
                                    }

                                    if (!textoString.Contains('"') && textoString.Contains(' '))
                                    {
                                        textoString = textoString.Trim();
                                        listaTokens.Add(A_lexico.AnalisisPalabras(textoString, contadorLinea, inicioColumna, contadorColumna));
                                        textoString = string.Empty;
                                    }
                                    else if (textoString.Contains('"') && cantidadComillas == 2)
                                    {
                                        listaTokens.Add(textoString + "        Es un String Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
                                        textoString = string.Empty;
                                    }
                                    else if (textoString.Contains("//"))
                                    {
                                        textoString = string.Empty;
                                        break;
                                    }
                                }
                            }
                            //Linea con comentario de una linea
                            else if (A_lexico.regexComentarioLinea.IsMatch(lineaActual))
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
                            //Linea con comentarios de multiple linea  en una linea 
                            else if (A_lexico.regexComentariosMultipleLine.IsMatch(lineaActual))
                            {
                                var splitInicioComentario = lineaActual.Split("/*");
                                var splitFinComentario = lineaActual.Split("*/");
                                //Agregar analisis
                            }
                            //Linea con comentarios de multiple linea 
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
                                    listaTokens.Add("En linea: " + contadorLinea + " abre comentario y nunca cierra");
                                }

                                //Implementar metodo para analisis de palabras
                                hayCierreComentario = false;
                            }
                            //Analisis palabras separadas con espacio
                            else
                            {
                                foreach (var item in lineaActual.Split(' '))
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
                                            palabraConSNP = string.Empty;
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
                                        //var resultado = A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna);
                                        //if (resultado != "")
                                        //{
                                        //    listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                        //}
                                        //else
                                        //{
                                        //    var segundaPalabra = string.Empty;
                                        //    foreach (var item2 in palabraConSNP)
                                        //    {
                                        //        segundaPalabra += item2;
                                        //        if (A_lexico.AnalisisPalabrasSegundo(segundaPalabra, contadorLinea, inicioColumna, contadorColumna)!="")
                                        //        {
                                        //            listaTokens.Add(A_lexico.AnalisisPalabrasSegundo(segundaPalabra, contadorLinea, inicioColumna, contadorColumna));
                                        //            segundaPalabra = string.Empty;
                                        //        }
                                        //    }
                                        //}
                                    }

                                    #region Opcion
                                    //var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
                                    ////No contiene caracter no permitido
                                    //if (cantidadMatches.Count == item.Length && !esComentario)
                                    //{
                                    //    var inicioColumna = contadorColumna;
                                    //    contadorColumna += item.Length - 1;
                                    //    listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
                                    //    contadorColumna = contadorColumna + 2;
                                    //}

                                    ////Caracter no permitido individual
                                    //else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
                                    //{
                                    //    var inicioColumna = contadorColumna;
                                    //    contadorColumna += item.Length;
                                    //    listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                    //    contadorColumna = contadorColumna + 2;
                                    //}

                                    ////Caracter no permitido viene junto
                                    //else
                                    //{
                                    //    var palabraConSNP = string.Empty;
                                    //    var inicioColumna = 0;
                                    //    foreach (var letra in item)
                                    //    {
                                    //        if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
                                    //        {
                                    //            palabraConSNP = palabraConSNP.Replace(" ", "");
                                    //            inicioColumna = contadorColumna;
                                    //            contadorColumna += palabraConSNP.Length;
                                    //            listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                    //            inicioColumna = contadorColumna;
                                    //            contadorColumna += letra.ToString().Length;
                                    //            listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                    //            palabraConSNP = " ";
                                    //        }
                                    //        else
                                    //        {
                                    //            palabraConSNP += letra;
                                    //        }
                                    //    }
                                    //    if (palabraConSNP != " ")
                                    //    {
                                    //        palabraConSNP = palabraConSNP.Replace(" ", "");
                                    //        inicioColumna = contadorColumna;
                                    //        contadorColumna += palabraConSNP.Length;
                                    //        listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
                                    //    }
                                    //}
                                    #endregion
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

                Console.Clear();
                Console.WriteLine("Ingrese la ruta de creacion de archivo:\n");
                var rutaNuevoArchivo = Console.ReadLine();
                var nombreArchivo = Path.GetFileNameWithoutExtension(ruta);
                #region ESCRITURA ARCHIVO TXT
                using (var archivo = new StreamWriter(rutaNuevoArchivo + nombreArchivo + ".out"))
                {

                    foreach (var item in listaTokens)
                    {
                        if (item != string.Empty)
                        {
                            archivo.WriteLine(item);
                        }
                    }
                    archivo.Close();
                }
                #endregion
                Console.WriteLine("Su archivo ha sido procesado, creado en " + rutaNuevoArchivo + nombreArchivo + ".out");
                Console.ReadLine();
            }
        }
    }
}
