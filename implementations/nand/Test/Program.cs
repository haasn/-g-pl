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
>implying adder isn't >function(x)
    >function(y)
        >add x y;

>implying addfive isn't >adder 5;

>mfw >addfive 3;;";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
