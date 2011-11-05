using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Gb2 : Exception, IExpression
    {
        public IExpression Value { get; private set; }

        public Gb2(IExpression value) : base("Unhandled gb2")
        {
            this.Value = value;
        }

        public Value Evaluate(Scope scope)
        {
            throw new Gb2(Value.Evaluate(scope));
        }
    }

    public class Jpg : Exception, IExpression
    {
        public IExpression Value { get; private set; }

        public Jpg(IExpression value) : base("Unhandled .jpg")
        {
            this.Value = value;
        }

        public Value Evaluate(Scope scope)
        {
            throw new Jpg(Value.Evaluate(scope));
        }
    }

    public class Noko : Exception, IExpression
    {
        public IExpression Value { get; private set; }

        public Noko(IExpression value) : base("Unhandled Noko")
        {
            this.Value = value;
        }

        public Value Evaluate(Scope scope)
        {
            throw new Noko(Value.Evaluate(scope));
        }
    }

    public class Sage : Exception, IExpression
    {
        public IExpression Value { get; private set; }

        public Sage(IExpression value) : base("Unhandled Sage")
        {
            this.Value = value;
        }

        public Value Evaluate(Scope scope)
        {
            throw new Sage(Value.Evaluate(scope));
        }
    }
}
