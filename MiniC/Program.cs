using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
            var rutaNuevoArchivo = string.Empty;
            var nombreArchivo = string.Empty;
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
                                        //Implementar metodo para analisis de palabras
                                        contadorColumna += textoString.Length - 1;
                                        textoString = textoString.Trim();
                                        listaTokens.Add(A_lexico.AnalisisPalabras(textoString, contadorLinea, inicioColumna, contadorColumna));
                                        textoString = string.Empty;
                                    }
                                    else if (textoString.Contains('"') && cantidadComillas == 2)
                                    {
                                        //Implementar metodo para analisis de palabras
                                        contadorColumna += textoString.Length - 1;
                                        listaTokens.Add(textoString + "        Es un String Linea: " + contadorLinea + " Columna: " + inicioColumna + "-" + contadorColumna + "\n");
                                        textoString = string.Empty;
                                    }
                                    else if (textoString.Contains("//"))
                                    {
                                        textoString = string.Empty;
                                        break;
                                    }
                                }
                                if (textoString != string.Empty)
                                {
                                    listaTokens.Add("Error un String nunca Cierra Linea: " + contadorLinea + "\n");
                                    Console.WriteLine("Error un String nunca Cierra Linea: " + contadorLinea + "\n");
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
                                        Console.WriteLine(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
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
                                                Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
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
                                    else
                                    {
                                        contadorLinea++;
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
                                            Console.WriteLine(item + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
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
                                                    Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
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
                                    Console.WriteLine("En linea: " + contadorLinea + " abre comentario y nunca cierra");

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
                                            //
                                            try
                                            {
                                                var palabraExtraida = A_lexico.AnalisisJuntas(palabraConSNP);
                                                listaTokens.Add(A_lexico.AnalisisPalabras(palabraExtraida, contadorLinea, inicioColumna, palabraExtraida.Length));

                                                var concatenadaSinExtraida = palabraConSNP.Replace(palabraExtraida, "");

                                                listaTokens.Add(A_lexico.AnalisisPalabras(concatenadaSinExtraida, contadorLinea, inicioColumna, concatenadaSinExtraida.Length));
                                                palabraConSNP = string.Empty;
                                                palabraConSNP += letra;
                                                inicioColumna = contadorColumna;
                                                contadorColumna += letra.ToString().Length;
                                                Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                palabraConSNP = string.Empty;
                                            }
                                            catch
                                            {
                                                inicioColumna = contadorColumna;
                                                contadorColumna += letra.ToString().Length;
                                                Console.WriteLine(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                listaTokens.Add(letra + "        Es Simbolo No Permitido Linea: " + contadorLinea + " Columna " + inicioColumna + "-" + contadorColumna + "\n");
                                                palabraConSNP = string.Empty;
                                            }
                                            //                                            
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
                                        var concatenada = string.Empty;
                                        var concatenarDigitosPuntuacion = string.Empty;
                                        if (A_lexico.AnalisisTodo(palabraConSNP) != 1)
                                        {
                                            foreach (var letra in palabraConSNP)
                                            {
                                                if (concatenada != string.Empty)
                                                {
                                                    if (concatenada.Any(char.IsDigit) || concatenada.Any(char.IsPunctuation))
                                                    {
                                                        var palabraExtraida = A_lexico.AnalisisJuntas(concatenada);
                                                        listaTokens.Add(A_lexico.AnalisisPalabras(palabraExtraida, contadorLinea, inicioColumna, palabraExtraida.Length));

                                                        try
                                                        {
                                                            var concatenadaSinExtraida = concatenada.Replace(palabraExtraida, "");

                                                            listaTokens.Add(A_lexico.AnalisisPalabras(concatenadaSinExtraida, contadorLinea, inicioColumna, concatenadaSinExtraida.Length));
                                                            concatenada = string.Empty;
                                                            concatenada += letra;
                                                        }
                                                        catch (Exception)
                                                        {
                                                            concatenada += letra;
                                                            if (concatenada.Contains(';'))
                                                            {
                                                                var textoSinPC = concatenada.Replace(";", "");
                                                                listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
                                                                listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
                                                            }
                                                            else
                                                            {
                                                                listaTokens.Add(A_lexico.AnalisisPalabras(concatenada, contadorLinea, inicioColumna, concatenada.Length));
                                                            }
                                                            concatenada = string.Empty;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        concatenada += letra;

                                                        if (concatenada.Contains(';'))
                                                        {
                                                            var textoSinPC = concatenada.Replace(";", "");
                                                            listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
                                                            listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
                                                            concatenada = string.Empty;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    concatenada += letra;
                                                    if (concatenada.Contains(';'))
                                                    {
                                                        var textoSinPC = concatenada.Replace(";", "");
                                                        listaTokens.Add(A_lexico.AnalisisPalabras(textoSinPC, contadorLinea, inicioColumna, textoSinPC.Length));
                                                        listaTokens.Add(A_lexico.AnalisisPalabras(";", contadorLinea, inicioColumna, 1));
                                                        concatenada = string.Empty;
                                                    }
                                                }
                                            }
                                            if (concatenada != string.Empty)
                                            {
                                                listaTokens.Add(A_lexico.AnalisisPalabras(concatenada, contadorLinea, inicioColumna, contadorColumna));
                                            }
                                        }
                                        else
                                        {
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
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Ingrese la ruta de creacion de archivo:\n");
                rutaNuevoArchivo = Console.ReadLine();
                nombreArchivo = Path.GetFileNameWithoutExtension(ruta);
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
                Console.WriteLine("Su archivo ha sido procesado \nCreado en: \n" + rutaNuevoArchivo + nombreArchivo + ".out");
                Console.ReadLine();
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
                while (lectura != null)
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
                    var analisis = pilaEstados.Peek() + "#" + listaEntrada[0].ToString();
                    analisis = Convert.ToString(analisis);
                    byte[] asciiString = Encoding.ASCII.GetBytes(analisis);
                    string s2 = Encoding.ASCII.GetString(asciiString);

                    Singleton.Instance.Estados.TryGetValue(s2, out estadoActual);
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
                        var reduccion = Singleton.Instance.Gramatica.Keys.Where(key => key.Contains(estadoTarget + "#")).FirstOrDefault();
                        var produccionReducir = Singleton.Instance.Gramatica[reduccion];
                        produccionReducir = produccionReducir.TrimStart();
                        produccionReducir = produccionReducir.TrimEnd();
                        var cantidadReduccion = 0;
                        if (produccionReducir != "")
                        {
                            cantidadReduccion = produccionReducir.Split(' ').Count();
                        }
                        for (int i = 0; i < cantidadReduccion; i++)
                        {
                            listaSimbolos.RemoveAt(listaSimbolos.Count() - 1);
                        }
                        listaSimbolos.Add(reduccion.Split('#')[1]);
                        for (int i = 0; i < cantidadReduccion; i++)
                        {
                            pilaEstados.Pop();
                        }
                        estadoAnterior = instruccion + estadoTarget;
                        Singleton.Instance.Estados.TryGetValue(pilaEstados.Peek() + "#" + listaSimbolos[listaSimbolos.Count() - 1], out estadoActual);
                        pilaEstados.Push(estadoActual);

                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Entrada no aceptada.");
            }

            //Obtener los simbolos de todo el archivos.
            using (var reader = new StreamReader(new FileStream(ruta, FileMode.Open)))
            {
                var lectura = reader.ReadLine();
                lectura = lectura.TrimStart();
                lectura = lectura.TrimEnd();
                var bloque = 0;
                var activo = 0;
                while (lectura != null)
                {
                    lectura = lectura.TrimStart();
                    lectura = lectura.TrimEnd();
                    if (lectura.Contains('{'))
                    {
                        bloque++;
                        activo = 1;
                    }
                    else if (lectura.Contains('}'))
                    {
                        bloque--;
                        activo = 0;
                    }
                    if (lectura.Contains("double") || lectura.Contains("int")|| lectura.Contains("string")|| lectura.Contains("bool"))
                    {
                        //Funcion
                        if (lectura.Contains('(')&& lectura.Contains(')'))
                        {
                            if (lectura.Contains('{'))
                            {
                                bloque--;
                            }
                            if (lectura.Contains("double"))
                            {
                                var temp = lectura.Split("double");
                                var nombre = "";
                                nombre = temp[0].Remove(';');
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "funcion";
                                objeto.Nombre = nombre;
                                var temp2 = temp[0].Split('(')[0];
                                objeto.Parametros = "("+temp2;
                                objeto.Tipo = "double";
                                objeto.Valor = "";
                                objeto.Activo = "1";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("int"))
                            {
                                var temp = lectura.Split("(");
                                var nombre = temp[0].Split("int");
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "funcion";
                                objeto.Nombre = nombre[1];
                                objeto.Parametros = ('(' + temp[1]);
                                objeto.Tipo = "int";
                                objeto.Valor = "";
                                objeto.Activo = "1";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("string"))
                            {
                                var temp = lectura.Split("string");
                                var nombre = "";
                                nombre = temp[0].Remove(';');
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "funcion";
                                objeto.Nombre = nombre;
                                var temp2 = temp[0].Split('(')[0];
                                objeto.Parametros = "(" + temp2;
                                objeto.Tipo = "string";
                                objeto.Valor = "";
                                objeto.Activo = "1";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("bool"))
                            {
                                var temp = lectura.Split("bool");
                                var nombre = "";
                                nombre = temp[0].Remove(';');
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "funcion";
                                objeto.Nombre = nombre;
                                var temp2 = temp[0].Split('(')[0];
                                objeto.Parametros = "(" + temp2;
                                objeto.Tipo = "bool";
                                objeto.Valor = "";
                                objeto.Activo = "1";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            if (lectura.Contains('{'))
                            {
                                bloque++;
                            }
                        }
                        //Variable
                        else
                        {
                            if (lectura.Contains("double"))
                            {
                                var temp = lectura.Split("double");
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "variable";
                                objeto.Nombre = temp[1];
                                objeto.Tipo = "double";
                                objeto.Valor = "";
                                objeto.Activo = "0";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("int"))
                            {
                                var temp = lectura.Split("int");
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "variable";
                                objeto.Nombre = temp[1];
                                objeto.Tipo = "int";
                                objeto.Valor = "";
                                objeto.Activo = "0";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("string"))
                            {
                                var temp = lectura.Split("string");
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "variable";
                                objeto.Nombre = temp[1];
                                objeto.Tipo = "string";
                                objeto.Valor = "";
                                objeto.Activo = "0";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                            else if (lectura.Contains("bool"))
                            {
                                var temp = lectura.Split("bool");
                                var objeto = new Simbolo();
                                objeto.TipoSimbolo = "variable";
                                objeto.Nombre = temp[1];
                                objeto.Tipo = "bool";
                                objeto.Valor = "";
                                objeto.Activo = "0";
                                objeto.Bloque = bloque.ToString();
                                Singleton.Instance.TablaSimbolos.Add(objeto);
                            }
                        }
                    }                   
                    lectura = reader.ReadLine();
                }
                reader.Close();
            }

            using (var reader = new StreamReader(new FileStream(ruta, FileMode.Open)))
            {
                var lectura = reader.ReadLine();
                lectura = lectura.TrimStart();
                lectura = lectura.TrimEnd();
                while (lectura != null)
                {
                    var contadorLineaSimbolos = 0;
                    lectura = lectura.TrimStart();
                    lectura = lectura.TrimEnd();
                    var existe = false;
                    if (lectura.Contains('='))
                    {
                        var variable = lectura.Split('=')[0];
                        var asignacion = lectura.Split('=')[1];
                        foreach (var item in Singleton.Instance.TablaSimbolos)
                        {
                            if (item.Nombre.Contains(variable))
                            {
                                existe=true;
                                if (asignacion.Contains('+')|| asignacion.Contains('-')|| asignacion.Contains('*')|| asignacion.Contains('/'))
                                {
                                    if (asignacion.Contains('+'))
                                    {
                                        var operandos = asignacion.Split('+');
                                        var numeros = new Regex("[0-9]+(.[0-9]+)?");
                                        if (numeros.IsMatch(operandos[0])&& numeros.IsMatch(operandos[1]))
                                        {
                                            var resultado = Convert.ToDouble(operandos[0]) + Convert.ToDouble(operandos[1]);
                                            if (item.Tipo.Contains("double")|| item.Tipo.Contains("int"))
                                            {
                                                item.Valor = resultado.ToString();
                                            }
                                            else
                                            {
                                                Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                            }
                                        }
                                        else if (numeros.IsMatch(operandos[0]) || numeros.IsMatch(operandos[1]))
                                        {
                                            if (numeros.IsMatch(operandos[0]))
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[1]))
                                                    {
                                                        if (item3.Tipo.Contains("int")|| item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(operandos[0]) + Convert.ToDouble(item3.Valor.Split(';')[0]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[0]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(item3.Valor.Split(';')[0]) + Convert.ToDouble(operandos[1]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var aux1 = "";
                                            var aux2 = "";
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[0]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux1 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[1]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux2=(item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            var resultado = Convert.ToDouble(aux1) + Convert.ToDouble(aux2);
                                            item.Valor = resultado.ToString();
                                        }

                                    }
                                    if (asignacion.Contains('-'))
                                    {
                                        var operandos = asignacion.Split('-');
                                        var numeros = new Regex("[0-9]+(.[0-9]+)?");
                                        if (numeros.IsMatch(operandos[0]) && numeros.IsMatch(operandos[1]))
                                        {
                                            var resultado = Convert.ToDouble(operandos[0]) - Convert.ToDouble(operandos[1]);
                                            if (item.Tipo.Contains("double") || item.Tipo.Contains("int"))
                                            {
                                                item.Valor = resultado.ToString();
                                            }
                                            else
                                            {
                                                Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                            }
                                        }
                                        else if (numeros.IsMatch(operandos[0]) || numeros.IsMatch(operandos[1]))
                                        {
                                            if (numeros.IsMatch(operandos[0]))
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[1]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(operandos[0]) - Convert.ToDouble(item3.Valor.Split(';')[0]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[0]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(item3.Valor.Split(';')[0]) - Convert.ToDouble(operandos[1]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var aux1 = "";
                                            var aux2 = "";
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[0]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux1 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[1]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux2 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            var resultado = Convert.ToDouble(aux1) - Convert.ToDouble(aux2);
                                            item.Valor = resultado.ToString();
                                        }

                                    }
                                    if (asignacion.Contains('*'))
                                    {
                                        var operandos = asignacion.Split('*');
                                        var numeros = new Regex("[0-9]+(.[0-9]+)?");
                                        if (numeros.IsMatch(operandos[0]) && numeros.IsMatch(operandos[1]))
                                        {
                                            var resultado = Convert.ToDouble(operandos[0]) * Convert.ToDouble(operandos[1]);
                                            if (item.Tipo.Contains("double") || item.Tipo.Contains("int"))
                                            {
                                                item.Valor = resultado.ToString();
                                            }
                                            else
                                            {
                                                Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                            }
                                        }
                                        else if (numeros.IsMatch(operandos[0]) || numeros.IsMatch(operandos[1]))
                                        {
                                            if (numeros.IsMatch(operandos[0]))
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[1]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(operandos[0]) * Convert.ToDouble(item3.Valor.Split(';')[0]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[0]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(item3.Valor.Split(';')[0]) * Convert.ToDouble(operandos[1]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var aux1 = "";
                                            var aux2 = "";
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[0]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux1 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[1]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux2 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            var resultado = Convert.ToDouble(aux1) * Convert.ToDouble(aux2);
                                            item.Valor = resultado.ToString();
                                        }

                                    }
                                    if (asignacion.Contains('/'))
                                    {
                                        var operandos = asignacion.Split('/');
                                        var numeros = new Regex("[0-9]+(.[0-9]+)?");
                                        if (numeros.IsMatch(operandos[0]) && numeros.IsMatch(operandos[1]))
                                        {
                                            var resultado = Convert.ToDouble(operandos[0]) / Convert.ToDouble(operandos[1].Split(';')[0]);
                                            if (item.Tipo.Contains("double") || item.Tipo.Contains("int"))
                                            {
                                                item.Valor = resultado.ToString();
                                            }
                                            else
                                            {
                                                Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                            }
                                        }
                                        else if (numeros.IsMatch(operandos[0]) || numeros.IsMatch(operandos[1]))
                                        {
                                            if (numeros.IsMatch(operandos[0]))
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[1]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(operandos[0]) / Convert.ToDouble(item3.Valor.Split(';')[0]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                                {
                                                    if (item3.Nombre.Contains(operandos[0]))
                                                    {
                                                        if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                        {
                                                            var resultado = Convert.ToDouble(item3.Valor.Split(';')[0]) / Convert.ToDouble(operandos[1]);
                                                            item.Valor = resultado.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var aux1 = "";
                                            var aux2 = "";
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[0]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux1 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            foreach (var item3 in Singleton.Instance.TablaSimbolos)
                                            {
                                                if (item3.Nombre.Contains(operandos[1]))
                                                {
                                                    if (item3.Tipo.Contains("int") || item3.Tipo.Contains("double"))
                                                    {
                                                        aux2 = (item3.Valor.Split(';')[0]);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                                    }
                                                }
                                            }
                                            var resultado = Convert.ToDouble(aux1) / Convert.ToDouble(aux2);
                                            item.Valor = resultado.ToString();
                                        }

                                    }
                                }
                                else
                                {
                                    var numeros = new Regex("[0-9]+");
                                    if (asignacion.Contains('"'))
                                    {
                                        if (item.Tipo=="string")
                                        {
                                            item.Valor = asignacion;
                                        }
                                        else
                                        {
                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                        }
                                    }
                                    else if (asignacion.Contains('.'))
                                    {
                                        if (item.Tipo == "double")
                                        {
                                            item.Valor = asignacion;
                                        }
                                        else
                                        {
                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                        }
                                    }
                                    else if (asignacion.Contains("true")|| asignacion.Contains("false"))
                                    {
                                        if (item.Tipo == "bool")
                                        {
                                            item.Valor = asignacion;
                                        }
                                        else
                                        {
                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                        }
                                    }
                                    else if (numeros.IsMatch(asignacion))
                                    {
                                        if (item.Tipo == "int")
                                        {
                                            item.Valor = asignacion;
                                        }
                                        else
                                        {
                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un " + item.Tipo + " LINEA: " + contadorLineaSimbolos);
                                        }
                                    }
                                    else
                                    {
                                        var existeD = false;
                                        foreach (var item2 in Singleton.Instance.TablaSimbolos)
                                        {
                                            if (item2.Nombre.Contains(asignacion))
                                            {
                                                if (item2.Tipo==item.Tipo)
                                                {
                                                    item.Valor = item2.Valor;
                                                    existeD = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!existeD)
                                        {
                                            Console.WriteLine(variable + " <- No concuerda con la asignacion, se esperaba un "+item.Tipo+ " LINEA: "+contadorLineaSimbolos );
                                        }
                                    }
                                }
                            }
                            if (existe)
                            {
                                break;
                            }
                        }
                        if (!existe)
                        {
                            Console.WriteLine(variable + " <- No ha sido declarada dentro del programa. LINEA: "+contadorLineaSimbolos);
                        }
                    }

                    //buscar funciones

                    contadorLineaSimbolos++;
                    lectura = reader.ReadLine();
                }
                reader.Close();
            }

            using (var archivo = new StreamWriter(rutaNuevoArchivo +"TS" +nombreArchivo + ".out"))
            {

                foreach (var item in Singleton.Instance.TablaSimbolos)
                {
                    archivo.WriteLine(item.Nombre+" - " +item.Parametros + " - " + item.Tipo + " - " + item.TipoSimbolo + " - " + item.Valor + " - " + item.Activo + " - " + item.Bloque);
                }
                archivo.Close();
            }

            Console.Write("La entrada ha sido aceptada correctamente");
            Console.ReadLine();
        }
    }
}
