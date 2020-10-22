using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                        var AccionEstado  = lineaActual.Split(" ");
                        
                        if (AccionEstado.Count() == 2)
                        {
                            Singleton.Instance.Estados.Add(AccionEstado[0],AccionEstado[1]);

                        }
                        else {
                            if (AccionEstado[0].Contains("#"))
                            {
                                ///////AGREGAR A DICCIONARIO
                                ///

                                Singleton.Instance.Estados.Add(AccionEstado[0], "Error\n" );
                            }
                        }
                      lineaActual = reader.ReadLine();
                    }

                    var aux = "perra";


                }
            }
        }
    }
}
