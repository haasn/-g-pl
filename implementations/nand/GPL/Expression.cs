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
}
