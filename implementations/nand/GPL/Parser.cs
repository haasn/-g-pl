using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace GPL
{
    public static class GPLParser
    {
        public static Block ParseFile(string source)
        {
            // TODO: remove whitespace and comments
            return SourceFile.Parse(source);
        }

        // Whitespace
        static Parser<IEnumerable<char>> space = Parse.WhiteSpace.Many();

        // Names
        static Parser<char> nameStart = Parse.CharExcept(c => "->.\"{}".Contains(c) || Char.IsControl(c) ||
                                                              Char.IsDigit(c) || Char.IsWhiteSpace(c), "name chars");

        static Parser<Name> NameParser = from start in nameStart
                                         from rest in nameStart.Or(Parse.Digit).Or(Parse.Char('-')).Many().Text()
                                         where !Name.Reserved.Contains(start + rest)
                                         select new Name(start + rest);

        // Literals
        static Parser<ForeverAlone> ForeverAloneLiteral = from _ in Parse.String("forever alone")
                                                          select new ForeverAlone();

        static Parser<Bool> BoolLiteral = from v in Parse.String("true").Or(Parse.String("false")).Or(Parse.String("on")).Or(
                                                    Parse.String("off")).Or(Parse.String("yes")).Or(Parse.String("no")).Text()
                                          select Bool.Parse(v);

        static Parser<String> StringLiteral = from open in Parse.Char('"')
                                              // TODO: add escaping
                                              from contents in Parse.CharExcept(c => "\\\"".Contains(c), "string character").Many().Text()
                                              from close in Parse.Char('"')
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
                                           from exp in BlockExpression.Many()
                                           from trail in Parse.Char('}')
                                           select new Block(exp);

        // TODO: implement gb2
        static Parser<IExpression> BlockExpression = from lead in space
                                                     from body in Expression
                                                     from trail in space
                                                     select body;

        // Implications
        static Parser<Implication> Declaration = from lead in Parse.String(">implying ")
                                                 from name in NameParser
                                                 select new Implication(name.Identifier);

        static Parser<ImplicationIsnt> Creation = from lead in Parse.String(">implying ")
                                                  from name in NameParser
                                                  from isnt in Parse.String(" isn't ")
                                                  from val in Expression
                                                  select new ImplicationIsnt(name.Identifier, val);

        static Parser<ImplicationWasnt> Assignment = from lead in Parse.String(">implying ")
                                                     from name in NameParser
                                                     from wasnt in Parse.String(" wasn't ")
                                                     from val in Expression
                                                     select new ImplicationWasnt(name.Identifier, val);

        static Parser<Implication> ImplicationParser = Creation.Or<Implication>(Assignment).Or(Declaration);

        // Expressions
        static Parser<IExpression> Expression = Literal.Or<IExpression>(NameParser).Or(BlockParser).Or(ImplicationParser);

        // Entire source file
        static Parser<Block> SourceFile = from be in BlockExpression.Many().End()
                                          select new Block(be);
    }
}
