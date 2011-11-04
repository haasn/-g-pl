using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Implication : IExpression
    {
        public string Name { get; private set; }

        public Implication(string name)
        {
            this.Name = name;
        }

        public virtual Value Evaluate(Scope scope)
        {
            return scope.Create(Name, new ForeverAlone());
        }
    }

    public class ImplicationIsnt : Implication
    {
        public IExpression Value { get; private set; }

        public ImplicationIsnt(string name, IExpression val) : base(name)
        {
            this.Value = val;
        }

        public override Value Evaluate(Scope scope)
        {
            return scope.Create(Name, Value.Evaluate(scope));
        }
    }

    public class ImplicationWasnt : Implication
    {
        public IExpression Value { get; private set; }

        public ImplicationWasnt(string name, IExpression val) : base(name)
        {
            this.Value = val;
        }

        public override Value Evaluate(Scope scope)
        {
            return scope.Find(Name, Value.Evaluate(scope));
        }
    }
}
