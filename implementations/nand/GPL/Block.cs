using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Block : IExpression
    {
        public List<IExpression> Body { get; private set; }

        public Block(IEnumerable<IExpression> body)
        {
            this.Body = new List<IExpression>(body);
        }

        public Value Evaluate(Scope scope)
        {
            // create child scope
            Scope child = new Scope(scope);
            Value final = new ForeverAlone();

            foreach (var ie in Body)
                final = ie.Evaluate(child);

            return final;
        }
    }
}
