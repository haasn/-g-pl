using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Scope
    {
        public Dictionary<string, Value> Variables { get; private set; }
        public Scope Parent { get; private set; }

        public Scope(Scope parent)
        {
            this.Parent = parent;
            this.Variables = new Dictionary<string, Value>();
        }

        public Value Find(string name)
        {
            if (Variables.ContainsKey(name))
                return Variables[name];
            else
                if (Parent == null)
                    throw new KeyNotFoundException("Unknown variable: " + name);
                else
                    return Parent.Find(name);
        }
    }
}
