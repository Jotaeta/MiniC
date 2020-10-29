using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MiniC
{
    class Program
    {
        static void Main()
        {


            //Carga de archivo para analisis sintactico
            Analisis_Sintactico AS = new Analisis_Sintactico();
            AS.ListadoPalabras();
            AS.CargarGramatica();

            var listaTokens = new List<string>();
            var ruta = string.Empty;
            var contadorLinea = 1;
            var contadorColumna = 0;
            var esComentario = false;
            var hayCierreComentario = false;





            Console.WriteLine("\tMINI C\n\t Allan Davila 1160118\n\t Jonathan Argueta 1029418\n");
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
            //    using (var reader = new StreamReader(new FileStream(ruta, FileMode.Open)))
            //    {
            //        try
            //        {
            //            var lineaActual = string.Empty;
            //            lineaActual = reader.ReadLine();
            //            while (lineaActual != null)
            //            {
            //                //Linea contiene comillas
            //                if (lineaActual.Contains(A_lexico.asciiComillas))
            //                {
            //                    var textoString = string.Empty;
            //                    var cantidadComillas = 0;
            //                    foreach (var item in lineaActual)
            //                    {
            //                        var inicioColumna = contadorColumna;

            //                        if (item == '"' && cantidadComillas == 0)
            //                        {
            //                            cantidadComillas++;
            //                            textoString += item;
            //                        }
            //                        else if (item != '"')
            //                        {
            //                            textoString += item;
            //                        }
            //                        else if (item == '"' && cantidadComillas != 0)
            //                        {
            //                            cantidadComillas++;
            //                            textoString += item;
            //                        }

            //                        if (!textoString.Contains('"') && textoString.Contains(' '))
            //                        {
            //                            //Implementar metodo para analisis de palabras
            //                            contadorColumna += textoString.Length - 1;
            //                            textoString = textoString.Trim();
            //                            listaTokens.Add(A_lexico.AnalisisPalabras(textoString, contadorLinea, inicioColumna, contadorColumna));
            //                            textoString = string.Empty;
            //                        }
            //                        else if (textoString.Contains('"') && cantidadComillas == 2)
            //                        {
            //                            //Implementar metodo para analisis de palabras
            //                            contadorColumna += textoString.Length - 1;
            //                            listaTokens.Add(textoString + "        Es un String Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
            //                            textoString = string.Empty;
            //                        }
            //                        else if (textoString.Contains("//"))
            //                        {
            //                            textoString = string.Empty;
            //                            break;
            //                        }
            //                    }
            //                    if (textoString != string.Empty)
            //                    {
            //                        listaTokens.Add("Error un String nunca Cierra Linea: " + contadorLinea + "\n");
            //                        Console.WriteLine("Error un String nunca Cierra Linea: " + contadorLinea + "\n");
            //                    }
            //                }
            //                //Linea con comentario de una linea
            //                else if (A_lexico.regexComentarioLinea.IsMatch(lineaActual))
            //                {
            //                    esComentario = true;
            //                    var splitBarra = lineaActual.Split("//");
            //                    var splitEspacio = splitBarra[0].Split(' ');
            //                    foreach (var item in splitEspacio)
            //                    {
            //                        var inicioColumna = contadorColumna;
            //                        contadorColumna += item.Length;
            //                        //Implementar metodo para analisis de palabras
            //                        contadorColumna += item.Length - 1;
            //                        if (A_lexico.regexSimbolosPermitidos.IsMatch(item))
            //                        {
            //                            listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
            //                        }
            //                    }
            //                }
            //                //Linea con comentarios de multiple linea 
            //                else if (A_lexico.regexComentariosMultipleLineCaso.IsMatch(lineaActual))
            //                {
            //                    var splitInicioComentario = lineaActual.Split("/*");
            //                    var splitEspacio = splitInicioComentario[0].Split(" ");
            //                    foreach (var item in splitEspacio)
            //                    {
            //                        var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
            //                        //Caracter no permitido no viene 
            //                        if (cantidadMatches.Count == item.Length && !esComentario)
            //                        {
            //                            var inicioColumna = contadorColumna;
            //                            contadorColumna += item.Length;
            //                            contadorColumna += item.Length - 1;
            //                            listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
            //                            contadorColumna = contadorColumna + 2;
            //                        }
            //                        //Caracter no permitido viene solo
            //                        else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
            //                        {
            //                            var inicioColumna = contadorColumna;
            //                            contadorColumna += item.Length;
            //                            Console.WriteLine(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                            listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                        }
            //                        //Caracter no permitido viene junto
            //                        else
            //                        {
            //                            var palabraConSNP = string.Empty;
            //                            var inicioColumna = 0;
            //                            foreach (var letra in item)
            //                            {
            //                                if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
            //                                {
            //                                    palabraConSNP = palabraConSNP.Replace(" ", "");
            //                                    inicioColumna = contadorColumna;
            //                                    contadorColumna += palabraConSNP.Length;
            //                                    listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
            //                                    inicioColumna = contadorColumna;
            //                                    contadorColumna += letra.ToString().Length;
            //                                    Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    palabraConSNP = " ";
            //                                }
            //                                else
            //                                {
            //                                    palabraConSNP += letra;
            //                                }
            //                            }
            //                            if (palabraConSNP != " ")
            //                            {
            //                                palabraConSNP = palabraConSNP.Replace(" ", "");
            //                                inicioColumna = contadorColumna;
            //                                contadorColumna += palabraConSNP.Length;
            //                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
            //                            }
            //                        }
            //                    }
            //                    //Se busca en archivo hasta encontrar el cierre de comentario
            //                    while (lineaActual != null)
            //                    {
            //                        if (lineaActual.Contains("*/"))
            //                        {

            //                            hayCierreComentario = true;
            //                            break;
            //                        }
            //                        else
            //                        {
            //                            contadorLinea++;
            //                        }
            //                        lineaActual = reader.ReadLine();
            //                    }

            //                    //Si el archivo tiene cierre de comentario
            //                    if (hayCierreComentario)
            //                    {
            //                        var splitFinComentario = lineaActual.Split("*/");
            //                        splitEspacio = splitFinComentario[1].Split(" ");
            //                        foreach (var item in splitEspacio)
            //                        {
            //                            var cantidadMatches = A_lexico.regexSimbolosPermitidos.Matches(item);
            //                            //Caracter no permitido no viene 
            //                            if (cantidadMatches.Count == item.Length && !esComentario)
            //                            {
            //                                var inicioColumna = contadorColumna;
            //                                contadorColumna += item.Length;
            //                                contadorColumna += item.Length - 1;
            //                                listaTokens.Add(A_lexico.AnalisisPalabras(item, contadorLinea, inicioColumna, contadorColumna));
            //                                contadorColumna = contadorColumna + 2;
            //                            }
            //                            //Caracter no permitido viene solo
            //                            else if (!A_lexico.regexSimbolosPermitidos.IsMatch(item))
            //                            {
            //                                var inicioColumna = contadorColumna;
            //                                contadorColumna += item.Length;
            //                                Console.WriteLine(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                listaTokens.Add(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                            }
            //                            //Caracter no permitido viene junto
            //                            else
            //                            {
            //                                var palabraConSNP = string.Empty;
            //                                var inicioColumna = 0;
            //                                foreach (var letra in item)
            //                                {
            //                                    if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
            //                                    {
            //                                        palabraConSNP = palabraConSNP.Replace(" ", "");
            //                                        inicioColumna = contadorColumna;
            //                                        contadorColumna += palabraConSNP.Length;
            //                                        listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
            //                                        inicioColumna = contadorColumna;
            //                                        contadorColumna += letra.ToString().Length;
            //                                        Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                        listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                        palabraConSNP = " ";
            //                                    }
            //                                    else
            //                                    {
            //                                        palabraConSNP += letra;
            //                                    }
            //                                }
            //                                if (palabraConSNP != " ")
            //                                {
            //                                    palabraConSNP = palabraConSNP.Replace(" ", "");
            //                                    inicioColumna = contadorColumna;
            //                                    contadorColumna += palabraConSNP.Length;
            //                                    listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
            //                                }
            //                            }
            //                        }
            //                    }

            //                    //Si el archivo no tiene cierre de comentario hay error
            //                    else
            //                    {
            //                        Console.WriteLine("En linea: " + contadorLinea + " abre comentario y nunca cierra");

            //                        listaTokens.Add("En linea: " + contadorLinea + " abre comentario y nunca cierra");
            //                    }

            //                    //Implementar metodo para analisis de palabras
            //                    hayCierreComentario = false;
            //                }
            //                //Analisis palabras separadas con espacio
            //                else
            //                {
            //                    foreach (var item in lineaActual.Split(' '))
            //                    {
            //                        var palabraConSNP = string.Empty;
            //                        var inicioColumna = 0;
            //                        foreach (var letra in item)
            //                        {
            //                            if (!A_lexico.regexSimbolosPermitidos.IsMatch(letra.ToString()))
            //                            {
            //                                palabraConSNP = palabraConSNP.Replace(" ", "");
            //                                inicioColumna = contadorColumna;
            //                                contadorColumna += palabraConSNP.Length;
            //                                //
            //                                try
            //                                {
            //                                    var palabraExtraida = A_lexico.AnalisisJuntas(palabraConSNP);
            //                                    listaTokens.Add(A_lexico.AnalisisPalabras(palabraExtraida, contadorLinea, inicioColumna, palabraExtraida.Length));

            //                                    var concatenadaSinExtraida = palabraConSNP.Replace(palabraExtraida, "");

            //                                    listaTokens.Add(A_lexico.AnalisisPalabras(concatenadaSinExtraida, contadorLinea, inicioColumna, concatenadaSinExtraida.Length));
            //                                    palabraConSNP = string.Empty;
            //                                    palabraConSNP += letra;
            //                                    inicioColumna = contadorColumna;
            //                                    contadorColumna += letra.ToString().Length;
            //                                    Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    palabraConSNP = string.Empty;
            //                                }
            //                                catch 
            //                                {
            //                                    inicioColumna = contadorColumna;
            //                                    contadorColumna += letra.ToString().Length;
            //                                    Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
            //                                    palabraConSNP = string.Empty;
            //                                }
            //                                //                                            
            //                            }
            //                            else
            //                            {
            //                                palabraConSNP += letra;

            //                            }
            //                        }
            //                        if (palabraConSNP != " ")
            //                        {
            //                            palabraConSNP = palabraConSNP.Replace(" ", "");
            //                            inicioColumna = contadorColumna;
            //                            contadorColumna += palabraConSNP.Length;
            //                            var concatenada = string.Empty;
            //                            var concatenarDigitosPuntuacion = string.Empty;
            //                            if (A_lexico.AnalisisTodo(palabraConSNP) != 1)
            //                            {
            //                                foreach (var letra in palabraConSNP)
            //                                {
            //                                    if (concatenada != string.Empty)
            //                                    {
            //                                        if (concatenada.Any(char.IsDigit) || concatenada.Any(char.IsPunctuation))
            //                                        {
            //                                            var palabraExtraida = A_lexico.AnalisisJuntas(concatenada);
            //                                            listaTokens.Add(A_lexico.AnalisisPalabras(palabraExtraida, contadorLinea, inicioColumna, palabraExtraida.Length));

            //                                            try
            //                                            {
            //                                                var concatenadaSinExtraida = concatenada.Replace(palabraExtraida, "");

            //                                                listaTokens.Add(A_lexico.AnalisisPalabras(concatenadaSinExtraida, contadorLinea, inicioColumna, concatenadaSinExtraida.Length));
            //                                                concatenada = string.Empty;
            //                                                concatenada += letra;
            //                                            }
            //                                            catch (Exception)
            //                                            {
            //                                                concatenada += letra;
            //                                                if (concatenada.Contains(';'))
            //                                                {
            //                                                    var textoSinPC = concatenada.Replace(";", "");
            //                                                    listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
            //                                                    listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
            //                                                }
            //                                                else
            //                                                {
            //                                                    listaTokens.Add(A_lexico.AnalisisPalabras(concatenada, contadorLinea, inicioColumna, concatenada.Length));
            //                                                }
            //                                                concatenada = string.Empty;
            //                                            }
            //                                        }
            //                                        else
            //                                        {
            //                                            concatenada += letra;

            //                                            if (concatenada.Contains(';'))
            //                                            {
            //                                                var textoSinPC = concatenada.Replace(";", "");
            //                                                listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
            //                                                listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
            //                                                concatenada = string.Empty;
            //                                            }
            //                                        }
            //                                    }
            //                                    else
            //                                    {
            //                                        concatenada += letra;
            //                                        if (concatenada.Contains(';'))
            //                                        {
            //                                            var textoSinPC = concatenada.Replace(";", "");
            //                                            listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
            //                                            listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
            //                                            concatenada = string.Empty;
            //                                        }
            //                                    }
            //                                }
            //                                if (concatenada != string.Empty)
            //                                {
            //                                    listaTokens.Add(A_lexico.AnalisisPalabras(concatenada, contadorLinea, inicioColumna, contadorColumna));
            //                                }
            //                            }
            //                            else
            //                            {
            //                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraConSNP, contadorLinea, inicioColumna, contadorColumna));
            //                            }

            //                        }

            //                    }
            //                }
            //                esComentario = false;
            //                lineaActual = reader.ReadLine();
            //                contadorLinea++;
            //                contadorColumna = 0;
            //            }
            //        }
            //        catch
            //        {
            //            Console.WriteLine("Hay error.");
            //        }
            //        reader.Close();
            //    }
            //    Console.ReadLine();
            //    Console.Clear();
            //    Console.WriteLine("Ingrese la ruta de creacion de archivo:\n");
            //    var rutaNuevoArchivo = Console.ReadLine();
            //    var nombreArchivo = Path.GetFileNameWithoutExtension(ruta);
            //    #region ESCRITURA ARCHIVO TXT
            //    using (var archivo = new StreamWriter(rutaNuevoArchivo + nombreArchivo + ".out"))
            //    {

            //        foreach (var item in listaTokens)
            //        {
            //            if (item != string.Empty)
            //            {
            //                archivo.WriteLine(item);
            //            }
            //        }
            //        archivo.Close();
            //    }
            //    #endregion
            //    Console.WriteLine("Su archivo ha sido procesado \nCreado en: \n" + rutaNuevoArchivo + nombreArchivo + ".out");
            //    Console.ReadLine();
            }

            var pilaEstados = new Stack<string>();
            var listaSimbolos = new List<string>();
            var listaEntrada = new List<string>();
            var auxAccion = string.Empty;
            using (var reader = new StreamReader(new FileStream(ruta, FileMode.Open)))
            {
                var lectura = reader.ReadLine();
                lectura = lectura.TrimStart();
                lectura = lectura.TrimEnd();
                lectura += " ";
                while (lectura!=null)
                {
                    lectura = lectura.TrimStart();
                    lectura = lectura.TrimEnd();
                    var dividirEspacios = lectura.Split(" ");
                    foreach (var item in dividirEspacios)
                    {
                        listaEntrada.Add(item);
                    }
                    lectura = reader.ReadLine();
                }
                reader.Close();
            }
            listaEntrada.Add("$");
                //Inicializar pila
            pilaEstados.Push("0");
            var estadoAnterior = string.Empty;
            var estadoActual = string.Empty;

            try
            {
                while ((estadoActual != "acc"))
                {
                    Singleton.Instance.Estados.TryGetValue(pilaEstados.Peek() + "#" + listaEntrada[0], out estadoActual);
                    var instruccion = estadoActual.Substring(0, 1);
                    var estadoTarget = string.Empty;
                    if (instruccion.Contains("s") || instruccion.Contains("r"))
                    {
                        estadoTarget = estadoActual.Remove(0, 1);
                    }
                    else
                    {
                        estadoTarget = instruccion;
                    }

                    if (instruccion.Contains("s"))
                    {
                        pilaEstados.Push(estadoTarget);
                        listaSimbolos.Add(listaEntrada[0]);
                        listaEntrada.RemoveAt(0);
                        estadoAnterior = instruccion + estadoTarget;
                    }
                    else if (instruccion.Contains("r"))
                    {
                        var reduccion=Singleton.Instance.Gramatica.Keys.Where(key => key.Contains(estadoTarget + "#")).FirstOrDefault();
                        var produccionReducir= Singleton.Instance.Gramatica[reduccion];
                        produccionReducir = produccionReducir.TrimStart();
                        produccionReducir = produccionReducir.TrimEnd();
                        var cantidadReduccion = produccionReducir.Split(' ').Count();
                        for (int i = 0; i < cantidadReduccion; i++)
                        {
                            listaSimbolos.RemoveAt(listaSimbolos.Count() - 1);
                        }
                        listaSimbolos.Add(reduccion.Split('#')[1]);
                        for (int i = 0; i < cantidadReduccion; i++)
                        {
                            pilaEstados.Pop();
                        }
                        estadoAnterior= instruccion + estadoTarget;
                        Singleton.Instance.Estados.TryGetValue(pilaEstados.Peek() + "#" + listaSimbolos[listaSimbolos.Count()-1], out estadoActual);
                        pilaEstados.Push(estadoActual);
                                           
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Entrada no aceptada.");
                throw;
            }
            Console.Write("La entrada ha sido aceptada correctamente");
            Console.ReadLine();
        }
    }
}
