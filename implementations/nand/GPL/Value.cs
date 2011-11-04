using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public abstract class Value : IExpression
    {
        public abstract GPLType Type { get; }

        public Value Evaluate(Scope scope)
        {
            return this;
        }

        public abstract Value Copy();
    }

    public class String : Value
    {
        public override GPLType Type { get { return GPLType.String; } }
        public string Value { get; private set; }

        public String(string value)
        {
            this.Value = value;
        }

        public override Value Copy()
        {
            return new String(Value);
        }
    }

    public class Integer : Value
    {
        public override GPLType Type { get { return GPLType.Integer; } }
        public Int64 Value { get; private set; } // TODO: Use BigNums

        public Integer(Int64 value)
        {
            this.Value = value;
        }

        public override Value Copy()
        {
            return new Integer(Value);
        }
    }

    public class Float : Value
    {
        public override GPLType Type { get { return GPLType.Float; } }
        public decimal Value { get; private set; }

        public Float(decimal value)
        {
            this.Value = value;
        }

        public override Value Copy()
        {
            return new Float(Value);
        }
    }

    public class Bool : Value
    {
        public override GPLType Type { get { return GPLType.Bool; } }
        public bool Value { get; private set; }

        public Bool(bool value)
        {
            this.Value = value;
        }

        public override Value Copy()
        {
            return new Bool(Value);
        }

        public static Bool Parse(string val)
        {
            switch (val)
            {
                case "true":
                case "on":
                case "yes":
                case "SHIT":
                    return new Bool(true);

                case "false":
                case "off":
                case "no":
                    return new Bool(false);

                default:
                    throw new FormatException();
            }
        }
    }

    public class Function : Value
    {
        public override GPLType Type { get { return GPLType.Function; } }
        public List<string> Names { get; private set; }
        public IExpression Body { get; private set; }
        public Scope Execution { get; private set; }

        public Function(IExpression body, Scope scope, List<string> names)
        {
            this.Body = body;
            this.Execution = scope;
            this.Names = names;
        }

        public static Function Make(Func<Scope, Value> exp, params string[] args)
        {
            return new Function(new Builtin(exp), Scope.Global, new List<string>(args));
        }

        public override Value Copy()
        {
            return new Function(Body, Execution.Copy(), Names);
        }
    }

    public class ForeverAlone : Value
    {
        public override GPLType Type { get { return GPLType.ForeverAlone; } }
        public ForeverAlone() { }
        public override Value Copy() { return new ForeverAlone(); }
    }
}
