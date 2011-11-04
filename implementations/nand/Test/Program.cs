using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GPL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            const string test = "";
            IExpression p = GPLParser.ParseFile(test);

            Scope global = new Scope(null);
            Value v = p.Evaluate(global);
        }
    }
}
