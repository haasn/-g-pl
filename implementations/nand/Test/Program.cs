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
            const string test = ">implying foo isn't >function(x) >function(x) x >>foo 3; 5;";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
