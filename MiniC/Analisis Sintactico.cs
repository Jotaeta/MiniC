using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace MiniC
{
    public class Analisis_Sintactico
    {

        public void ListadoPalabras()
        {
            string ruta = string.Empty;
            if (!File.Exists("COMPIS ESTADOS.TXT"))
            {
                Console.WriteLine("Ingrese un Archivo Valido");
            }
            else
            {
                using (var reader = new StreamReader(new FileStream("COMPIS ESTADOS.TXT", FileMode.Open)))
                {
                    var lineaActual = string.Empty;
                    lineaActual = reader.ReadLine();

                    while (lineaActual != null)
                    {
                        lineaActual = lineaActual.TrimEnd();
                        lineaActual = lineaActual.TrimStart();

                        var AccionEstado  = lineaActual.Split(" ");
                 
                        if (AccionEstado.Count() == 2)
                        {
                            AccionEstado[0] = AccionEstado[0].TrimStart();
                            AccionEstado[0] = AccionEstado[0].TrimEnd();
                            AccionEstado[1] = AccionEstado[1].TrimStart();
                            AccionEstado[1] = AccionEstado[1].TrimEnd();
                            if (AccionEstado[0].Contains("{"))
                            {
                                var split = AccionEstado[0].Split('#');
                                Singleton.Instance.Estados.Add(split[0] + "#{", AccionEstado[1]);
                            }
                            else if (AccionEstado[0].Contains("}"))
                            {
                                var split = AccionEstado[0].Split('#');
                                Singleton.Instance.Estados.Add(split[0] + "#}", AccionEstado[1]);
                            }
                            else
                            {
                                byte[] asciiString = Encoding.ASCII.GetBytes(AccionEstado[0]);
                                string s2 = Encoding.ASCII.GetString(asciiString);
                                byte[] asciiString2 = Encoding.ASCII.GetBytes(AccionEstado[1]);
                                string s1 = Encoding.ASCII.GetString(asciiString2);
                                Singleton.Instance.Estados.Add(s2, s1);
                            }
                    

                        }
                        else {
                            if (AccionEstado[0].Contains("#"))
                            {
                                ///////AGREGAR A DICCIONARIO
                                ///
                                if (AccionEstado[0].Contains("{"))
                                {
                                    var split= AccionEstado[0].Split('#');
                                    Singleton.Instance.Estados.Add(split[0]+"#{", "null");
                                }
                                else if (AccionEstado[0].Contains("}"))
                                {
                                    var split = AccionEstado[0].Split('#');
                                    Singleton.Instance.Estados.Add(split[0] + "#}", "null");
                                }
                                else
                                {
                                    AccionEstado[0] = AccionEstado[0].TrimStart();
                                    AccionEstado[0] = AccionEstado[0].TrimEnd();
                                    byte[] asciiString = Encoding.ASCII.GetBytes(AccionEstado[0]);
                                    string s2 = Encoding.ASCII.GetString(asciiString);
                                    Singleton.Instance.Estados.Add(s2, "null");
                                }
                               
                            }
                        }
                      lineaActual = reader.ReadLine();
                    }
                }
            }
        }

        public void CargarGramatica()
        {
            string ruta = string.Empty;
            if (!File.Exists("GRAMATICA.TXT"))
            {
                Console.WriteLine("Ingrese un Archivo Valido");
            }
            else
            {
                using (var reader = new StreamReader(new FileStream("GRAMATICA.TXT", FileMode.Open)))
                {
                    var lineaActual = string.Empty;
                    lineaActual = reader.ReadLine();

                    while (lineaActual != null)
                    {
                        lineaActual = lineaActual.TrimEnd();
                        lineaActual = lineaActual.TrimStart();

                        var AccionEstado = lineaActual.Split(new string[] { "->" }, StringSplitOptions.None);
                       
                        if (AccionEstado.Count() == 2)
                        {
                            AccionEstado[0] = AccionEstado[0].TrimStart();
                            AccionEstado[0] = AccionEstado[0].TrimEnd();
                            AccionEstado[1] = AccionEstado[1].TrimStart();
                            AccionEstado[1] = AccionEstado[1].TrimEnd();
                            byte[] asciiString = Encoding.ASCII.GetBytes(AccionEstado[0]);
                            string s2 = Encoding.ASCII.GetString(asciiString);
                            byte[] asciiString2 = Encoding.ASCII.GetBytes(AccionEstado[1]);
                            string s1 = Encoding.ASCII.GetString(asciiString2);
                            Singleton.Instance.Gramatica.Add(s2,s1);

                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }
                        lineaActual = reader.ReadLine();
                    }
                }
            }
        }
    }
}
