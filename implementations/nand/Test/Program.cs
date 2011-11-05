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
            const string test = @"

>implying foo isn't >function(x) {
     x TIER: {
         >mfw ""true"";
         >forever alone.jpg
     } 100% accurate

     >mfw ""false"";
     gb2 -1
 }

>foo true;

";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
