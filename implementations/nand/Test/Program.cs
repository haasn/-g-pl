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

>mfw >>function(x) >add x x; 5;;

>implying factorial isn't >function(n)
    >is n 0; TIER: 1
    SHIT TIER: >mul n >this >sub n 1;;;
100% accurate

inane >mfw >factorial 5;;

>>function(x)
   >is x 0; TIER:
     forever alone
   SHIT TIER: {
     >mfw x;
     >this >sub x 1;;
     >mfw x;
   } 100% accurate
 5;

";

            Value v = Interpreter.Run(test);
        }
    }
}
