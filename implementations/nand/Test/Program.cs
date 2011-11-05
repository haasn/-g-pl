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

>implying f2c isn't >function(t) >mul >sub t 32; >div 5.0 9;;

>mfw >f2c 0;;
>mfw >f2c 32;;
>mfw >f2c 100;;
";

            Value v = Interpreter.Run(test);
        }
    }
}
