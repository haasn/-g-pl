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
>implying assert isn't >function(x, y)
    >is x y; TIER:
        forever alone

    SHIT TIER: {
        >implying str isn't ""Assertion failed! (got ""
        >implying str wasn't >add str x;
        >implying str wasn't >add str "", expected "";
        >implying str wasn't >add str y;
        >mfw >add str "")"";;
    } 100% accurate

>implying adder isn't >function(x)
    >function(y)
        >add x y;

>implying addfive isn't >adder 5;

>assert >addfive 3; 8;

";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
