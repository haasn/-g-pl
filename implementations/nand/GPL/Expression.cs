using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public interface IExpression
    {
        Value Evaluate(Scope scope);
    }

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
    }
}
