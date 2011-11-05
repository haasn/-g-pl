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

>foo false;

";

            Value v = Interpreter.Run(test);
        }
    }
}
