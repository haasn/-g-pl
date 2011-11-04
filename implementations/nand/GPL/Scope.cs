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

        public Scope Copy()
        {
            Scope cp = new Scope(Parent);

            foreach (var kvp in Variables)
                cp.Variables.Add(kvp.Key, kvp.Value.Copy());

            return cp;
        }

        public Value Find(string name, Value newval = null)
        {
            if (Variables.ContainsKey(name))
            {
                if (newval != null)
                    Variables[name] = newval;

                return Variables[name];
            }
            else
                if (Parent == null)
                    throw new KeyNotFoundException("Unknown variable: " + name);
                else
                    return Parent.Find(name, newval);
        }

        public Value Create(string name, Value value)
        {
            if (Variables.ContainsKey(name))
                Variables[name] = value;
            else
                Variables.Add(name, value);

            return value;
        }

        public static Scope Global = new Scope(null);

        static Scope() // static constructor
        {
            Builtins.AddBuiltins();
        }
    }
}
