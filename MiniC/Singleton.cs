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
        public List <string> Lista_palabras = new List <string>();
        public void matchtoken(string analizar)
        {
            if (analizada == analizar)
            {

            }
            else
            {
                Console.WriteLine("Se esperaba " + analizar + "Se recibio " + analizada);
            }
        }
    }
}