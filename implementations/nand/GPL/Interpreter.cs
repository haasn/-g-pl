using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GPL
{
    public static class Interpreter
    {
        public static Value Run(string s)
        {
            return Run(new MemoryStream(Encoding.UTF8.GetBytes(s)));
        }

        public static Value Run(Stream s)
        {
            try
            {
                return GPLParser.ParseFile(s).Evaluate(Scope.Global);
            }
            catch (Jpg jpg)
            {
                return jpg.Value.Evaluate(Scope.Global);
            }
        }

        public static Value RunFile(string path)
        {
            return Run(File.OpenRead(path));
        }
    }
}
