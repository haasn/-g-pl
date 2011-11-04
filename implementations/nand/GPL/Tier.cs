using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    class TierList : IExpression
    {
        public List<Tuple<IExpression, IExpression>> Tiers { get; private set; }

        public TierList(IEnumerable<Tuple<IExpression, IExpression>> tiers)
        {
            this.Tiers = new List<Tuple<IExpression, IExpression>>(tiers);
        }

        public Value Evaluate(Scope scope)
        {
            foreach (var t in Tiers)
            {
                if (t.Item1 == null) // SHIT TIER
                    return t.Item2.Evaluate(scope);

                var res = t.Item1.Evaluate(scope);

                if (res.Type != GPLType.Bool)
                    throw new Exception("Non-boolean expression inside TIER list");

                if (((Bool)res).Value) // evaluated to true
                    return t.Item2.Evaluate(scope);
            }

            return new ForeverAlone(); // no conditions met true
        }
    }
}
