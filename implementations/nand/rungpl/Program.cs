using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPL;

namespace rungpl
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: rungpl <source file>");
                Environment.Exit(1);
            }

            Value v = Interpreter.RunFile(args[0]);

            switch (v.Type)
            {
                case GPLType.String:
                case GPLType.ForeverAlone:
                case GPLType.Function:
                    Environment.Exit(0);
                    break;

                case GPLType.Bool:
                    Environment.Exit(((Bool)v).Value ? 0 : 1);
                    break;

                case GPLType.Integer:
                    Environment.Exit((int)((Integer)v).Value);
                    break;

                case GPLType.Float:
                    Environment.Exit((int)((Float)v).Value);
                    break;
            }
        }
    }
}
