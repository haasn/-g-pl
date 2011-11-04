using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Name : IExpression
    {
        public string Identifier { get; private set; }

        public Name(string id)
        {
            this.Identifier = id;
        }

        public Value Evaluate(Scope scope)
        {
            return scope.Find(Identifier);
        }

        public static string[] Reserved = new string[]
        {
            "implying", "function", "SHIT", "TIER", "gb2", "forever", "alone", "true", "false", "yes", "no", "on", "off"
        };
    }
}
