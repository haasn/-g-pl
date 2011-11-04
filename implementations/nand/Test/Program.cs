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
            const string test = ">implying test isn't >function(bat,baz) bat >implying foo isn't 40 >implying bar isn't 30 >test foo bar;";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
