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
>implying factorial isn't >function(n)
    >is n 0; TIER: 1
    SHIT TIER: >mul n >this >sub n 1;;;
100% accurate

>mfw >factorial 20;;
";

            Value v = Interpreter.Run(test);
        }
    }
}
