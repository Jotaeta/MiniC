using System;
using System.Collections.Generic;
using System.Text;

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
        public Dictionary <string , string> Estados = new Dictionary <string, string> ();
            }
}
