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

>implying churchTrue isn't >function(a, b) a
>implying churchFalse isn't >function(a, b) b

>implying not isn't >function(x) >x churchFalse churchTrue;
>implying and isn't >function(x, y) >x y x;
>implying or isn't >function(x, y) >x x y;

>implying church isn't >function(x) x TIER: churchTrue SHIT TIER: churchFalse 100% accurate
>implying unchurch isn't >function(x) >x true false;
>implying print isn't >function(x) >mfw >unchurch x;;

>print churchTrue;
>print >not churchFalse;;
>print >and churchTrue churchFalse;;

>implying closure isn't 1
>implying addone isn't >function(x) >add closure x;

>implying closure isn't 2
>implying addtwo isn't >function(x) >add closure x;

>implying foo isn't >function >function >function >mfw forever alone;
>>>foo;;;

>mfw >addone 3;;
>mfw >addtwo 3;;

>implying oldmfw isn't >function(x) >mfw x;
>implying mfw isn't forever alone

>oldmfw false;
";

            IExpression p = GPLParser.ParseFile(test);

            Value v = p.Evaluate(Scope.Global);
        }
    }
}
