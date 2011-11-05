using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Sprache;

namespace GPL
{
    public static class GPLParser
    {
        public static Block ParseFile(string source)
        {
            return ParseFile(new MemoryStream(Encoding.UTF8.GetBytes(source)));
        }

        public static Block ParseFile(Stream source)
        {
            var sb = new StringBuilder();
            var sr = new StreamReader(source);
            bool instring = false, incomment = false, inescape = false;
            char[] buffer = new char[5];

            while (!sr.EndOfStream)
            {
                char c = (char)sr.Read();

                if (instring)
                {
                    if (inescape)
                    {
                        switch (c)
                        {
                            case '\\':
                            case '"':
                                sb.Append(c);
                                inescape = false;
                                break;

                            default:
                                throw new Exception("Unrecognized escape sequence: \\" + c);
                        }
                    }
                    else if (c == '\\')
                        inescape = true;
                    else if (c == '"')
                    {
                        sb.Append('”');
                        instring = false;
                    }
                    else
                        sb.Append(c);
                }
                else
                {
                    if (c == '"')
                    {
                        instring = true;
                        sb.Append('“');
                    }
                    else
                    {
                        sb.Append(c);

                        if (c == 'e') // could be inane
                        {
                            sb.CopyTo(sb.Length - 5, buffer, 0, 5);

                            if (new string(buffer) == "inane")
                            {
                                sb.Remove(sb.Length - 5, 5);
                                sr.ReadLine();
                            }
                        }
                    }
                }
            }

            return SourceFile.Parse(sb.ToString());
        }

        // Whitespace
        static Parser<IEnumerable<char>> space = Parse.WhiteSpace.Many();

        // Names
        static Parser<char> nameStart = Parse.CharExcept(c => "->.\"{}();,".Contains(c) || Char.IsControl(c) ||
                                                              Char.IsDigit(c) || Char.IsWhiteSpace(c), "name chars");

        static Parser<string> nameParser = (from start in nameStart
                                            from rest in nameStart.Or(Parse.Digit).Or(Parse.Char('-')).Many().Text()
                                            where !Name.Reserved.Contains(start + rest)
                                            select start + rest).Token();

        // Using a variable
        static Parser<Name> Variable = from name in nameParser
                                       select new Name(name);

        // Literals
        static Parser<ForeverAlone> ForeverAloneLiteral = from _ in Parse.String("forever alone")
                                                          select new ForeverAlone();

        static Parser<Bool> BoolLiteral = from v in Parse.String("true").Or(Parse.String("false")).Or(Parse.String("on")).Or(
                                                    Parse.String("off")).Or(Parse.String("yes")).Or(Parse.String("no")).Or(Parse.String("SHIT")).Text()
                                          select Bool.Parse(v);

        static Parser<String> StringLiteral = from open in Parse.Char('“')
                                              // TODO: add escaping
                                              from contents in Parse.CharExcept(c => "“”".Contains(c), "string character").Many().Text()
                                              from close in Parse.Char('”')
                                              select new String(contents);

        static Parser<Integer> IntLiteral = from neg in Parse.Char('-').Or(Parse.Return(' '))
                                            from digits in Parse.Digit.AtLeastOnce().Text()
                                            select new Integer(long.Parse(neg + digits));

        static Parser<Float> FloatLiteral = from neg in Parse.Char('-').Or(Parse.Return(' '))
                                            from pre in Parse.Digit.Many().Text()
                                            from dot in Parse.Char('.')
                                            from post in Parse.Digit.AtLeastOnce().Text()
                                            select new Float(decimal.Parse(neg + pre + dot + post));

        static Parser<Value> Literal = ForeverAloneLiteral.Or<Value>(BoolLiteral).Or(StringLiteral).Or(IntLiteral).Or(FloatLiteral);

        // Blocks
        static Parser<Block> BlockParser = from lead in Parse.Char('{')
                                           from exp in Expression.Many()
                                           from trail in Parse.Char('}')
                                           select new Block(exp);

        // TODO: implement gb2
        //static Parser<IExpression> BlockExpression = Expression;

        // Implications
        static Parser<Implication> Declaration = from lead in Parse.String(">implying")
                                                 from name in nameParser
                                                 select new Implication(name);

        static Parser<ImplicationIsnt> Creation = from lead in Parse.String(">implying")
                                                  from name in nameParser
                                                  from isnt in Parse.String("isn't")
                                                  from val in Expression
                                                  select new ImplicationIsnt(name, val);

        static Parser<ImplicationWasnt> Assignment = from lead in Parse.String(">implying")
                                                     from name in nameParser
                                                     from wasnt in Parse.String("wasn't")
                                                     from val in Expression
                                                     select new ImplicationWasnt(name, val);

        static Parser<Implication> ImplicationParser = Creation.Or<Implication>(Assignment).Or(Declaration);

        // Functions
        static Parser<FunctionCall> FunctionApplication = from lead in Parse.Char('>')
                                                          from func in expfun
                                                          from args in Expression.Many()
                                                          from stop in Parse.Char(';')
                                                          select new FunctionCall(func, args);

        static Parser<IEnumerable<string>> FunctionNames = from lead in Parse.Char('(')
                                                           from first in nameParser
                                                           from rest in
                                                               (from comma in Parse.Char(',')
                                                                from name in nameParser
                                                                select name).Many()
                                                           from trail in Parse.Char(')')
                                                           select new string[] { first }.Concat(rest);


        static Parser<FunctionCreation> FunctionCreation = from lead in Parse.String(">function")
                                                           from names in FunctionNames.Or(Parse.Return(new List<string>()))
                                                           from body in Expression
                                                           select new FunctionCreation(body, names);

        static Parser<IExpression> FunctionParser = FunctionApplication.Or<IExpression>(FunctionCreation);

        // Limited expression: Everything but tier lists
        static Parser<IExpression> explim = Literal.Or<IExpression>(FunctionParser).Or<IExpression>(BlockParser).Or(ImplicationParser).Or(Variable).Token();
        static Parser<IExpression> expfun = FunctionParser.Or<IExpression>(BlockParser).Or(ImplicationParser).Or(Variable).Token();

        // TIER lists
        static Parser<Tuple<IExpression, IExpression>> Tier = from cond in explim
                                                              from text in Parse.String("TIER:")
                                                              from res in Expression
                                                              select new Tuple<IExpression, IExpression>(cond, res);

        static Parser<TierList> Tiers = from list in Tier.Token().AtLeastOnce()
                                        from term in Parse.String("100% accurate")
                                        select new TierList(list);

        // Expressions
        static Parser<IExpression> Expression = Tiers.Or(explim).Token();

        // Entire source file
        static Parser<Block> SourceFile = from be in Expression.Many().End()
                                          select new Block(be);
    }
}
