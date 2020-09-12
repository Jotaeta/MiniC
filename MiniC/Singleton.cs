using System;
using System.Collections.Generic;
using System.Linq;
namespace MiniC
{
    public class Singleton
    {
        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                {
                    return _instance;
                }
            }
        }
        public string analizada = " ";
        public bool hayError = false;
        public int contadotLinea = 1;
        public List <string> Lista_palabras = new List <string>();
        public int contador=1;
        public void matchtoken(string analizar)
        {
            if (Lista_palabras[contador-1].ToString() == analizar)
            {
                try
                {
                    analizada = Lista_palabras[contador];
                    contador++;
                }
                catch (Exception)
                {

                }                
            }
            else
            {
                Console.WriteLine("Se esperaba ("+ analizar+") Se recibio el simbolo (" + Lista_palabras[contador - 1].ToString() + ") que no corresponde con la gramatica en linea "+contadotLinea);
                hayError = true;
            }
        }
    }
}