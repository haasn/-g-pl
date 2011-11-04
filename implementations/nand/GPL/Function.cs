using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class FunctionCreation : IExpression
    {
        public IExpression Body { get; private set; }
        public List<string> Params { get; private set; }

        public FunctionCreation(IExpression body, IEnumerable<string> param)
        {
            this.Body = body;
            this.Params = new List<string>(param);
        }

        public Value Evaluate(Scope scope)
        {
            return new Function(Body, scope, Params);
        }
    }

    public class FunctionCall : IExpression
    {
        public IExpression Fun { get; private set; }
        public List<IExpression> Params { get; private set; }

        public FunctionCall(IExpression fun, IEnumerable<IExpression> param)
        {
            this.Fun = fun;
            this.Params = new List<IExpression>(param);
        }

        public Value Evaluate(Scope scope)
        {
            // first we get the function
            Value v = Fun.Evaluate(scope);

            if (v.Type != GPLType.Function)
                throw new Exception("Attempting to call non-function value!");

            Function f = (Function)v;

            if (f.Names.Count != Params.Count)
                throw new Exception("Mismatched parameter count!");

            // create an execution scope
            Scope s = new Scope(f.Execution);

            // assign and evaluate all function parameters
            for (int i = 0; i < Params.Count; i++)
                s.Create(f.Names[i], Params[i].Evaluate(scope));

            // evaluate the function
            return f.Body.Evaluate(s);
        }
    }

    public class Builtin : IExpression
    {
        public Func<Scope, Value> Lambda { get; private set; }

        public Builtin(Func<Scope, Value> lambda)
        {
            this.Lambda = lambda;
        }

        public Value Evaluate(Scope scope)
        {
            return Lambda(scope);
        }
    }
}
