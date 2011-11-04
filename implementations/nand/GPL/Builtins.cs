using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    static class Builtins
    {
        public static void AddBuiltins()
        {
            Func<Value, string> ToString = value =>
            {
                StringBuilder sb = new StringBuilder();

                switch (value.Type)
                {
                    case GPLType.String: sb.Append(((String)value).Value); break;
                    case GPLType.Integer: sb.Append(((Integer)value).Value); break;
                    case GPLType.Float: sb.Append(((Float)value).Value); break;
                    case GPLType.Bool: sb.Append(((Bool)value).Value); break;
                    case GPLType.ForeverAlone: sb.Append("forever alone"); break;

                    case GPLType.Function:
                        var f = (Function)value;
                        sb.Append("function(");

                        for (int i = 0; i < f.Names.Count; i++)
                        {
                            if (i > 0)
                                sb.Append(',');

                            sb.Append(f.Names[i]);
                        }

                        sb.Append(')');
                        break;
                }

                return sb.ToString();
            };

            // IO
            Scope.Global.Variables.Add("mfw", Function.Make(scope =>
            {
                var value = scope.Find("value");
                Console.WriteLine(ToString(value));

                return value;
            }, "value"));

            Scope.Global.Variables.Add("yfw", Function.Make(scope => new String(Console.ReadLine())));

            // Conversions
            Scope.Global.Variables.Add("float", Function.Make(scope =>
            {
                var value = scope.Find("value");

                switch (value.Type)
                {
                    case GPLType.Float: return value;
                    case GPLType.Integer: return new Float(((Integer)value).Value);
                    case GPLType.String: return new Float(decimal.Parse(((String)value).Value));
                    default: throw new Exception("Tried to convert invalid type to float");
                }
            }, "value"));

            Scope.Global.Variables.Add("int", Function.Make(scope =>
            {
                var value = scope.Find("value");

                switch (value.Type)
                {
                    case GPLType.Integer: return value;
                    case GPLType.Float: return new Integer((long)((Float)value).Value);
                    case GPLType.String: return new Integer(long.Parse(((String)value).Value));
                    default: throw new Exception("Tried to convert invalid type to int");
                }
            }, "value"));

            Scope.Global.Variables.Add("bool", Function.Make(scope =>
            {
                var value = scope.Find("value");

                switch (value.Type)
                {
                    case GPLType.Bool: return value;
                    case GPLType.String: return Bool.Parse(((String)value).Value);
                    default: throw new Exception("Tried to convert invalid type to bool");
                }
            }, "value"));

            Scope.Global.Variables.Add("string", Function.Make(scope => new String(ToString(scope.Find("value"))), "value"));

            // Comparisons
            Scope.Global.Variables.Add("is", Function.Make(scope =>
            {
                var a = scope.Find("a");
                var b = scope.Find("b");

                if (a.Type == GPLType.Function || b.Type == GPLType.Function)
                    return new Bool(false);

                switch (a.Type)
                {
                    case GPLType.Bool:
                        if (a.Type != b.Type)
                            return new Bool(false);

                        return new Bool(((Bool)a).Value == ((Bool)b).Value);

                    case GPLType.String:
                        if (a.Type != b.Type)
                            return new Bool(false);

                        return new Bool(((String)a).Value == ((String)b).Value);

                    case GPLType.ForeverAlone:
                        return new Bool(a.Type == b.Type);

                    case GPLType.Float:
                        if (b.Type == GPLType.Float)
                            return new Bool(((Float)a).Value == ((Float)b).Value);
                        else if (b.Type == GPLType.Integer)
                            return new Bool(((Float)a).Value == ((Integer)b).Value);
                        else
                            return new Bool(false);

                    case GPLType.Integer:
                        if (b.Type == GPLType.Float)
                            return new Bool(((Integer)a).Value == ((Float)b).Value);
                        else if (b.Type == GPLType.Integer)
                            return new Bool(((Integer)a).Value == ((Integer)b).Value);
                        else
                            return new Bool(false);
                }

                return new Bool(false);
            }, "a", "b"));

            Scope.Global.Variables.Add("smaller", Function.Make(scope =>
            {
                var a = scope.Find("a");
                var b = scope.Find("b");
                var ex = new Exception("Attempting to compare non-numeric type!");

                if (a.Type == GPLType.Float)
                {
                    if (b.Type == GPLType.Float)
                        return new Bool(((Float)a).Value < ((Float)b).Value);
                    else if (b.Type == GPLType.Integer)
                        return new Bool(((Float)a).Value < ((Integer)b).Value);
                    else
                        throw ex;
                }
                else if (a.Type == GPLType.Integer)
                {
                    if (b.Type == GPLType.Float)
                        return new Bool(((Integer)a).Value < ((Float)b).Value);
                    else if (b.Type == GPLType.Integer)
                        return new Bool(((Integer)a).Value < ((Integer)b).Value);
                    else
                        throw ex;
                }

                throw ex;
            }, "a", "b"));

            // Arithmetic
            Scope.Global.Variables.Add("add", Function.Make(scope =>
            {
                var x = scope.Find("x");
                var y = scope.Find("y");

                if (x.Type == GPLType.String && y.Type == GPLType.String) // concat
                    return new String(((String)x).Value + ((String)y).Value);

                if (x.Type == GPLType.Integer)
                {
                    if (y.Type == GPLType.Integer)
                        return new Integer(((Integer)x).Value + ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Integer)x).Value + ((Float)y).Value);
                }
                else if (x.Type == GPLType.Float)
                {
                    if (y.Type == GPLType.Integer)
                        return new Float(((Float)x).Value + ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Float)x).Value + ((Float)y).Value);
                }

                throw new Exception("Attempting to add mismatched types!");
            }, "x", "y"));

            Scope.Global.Variables.Add("sub", Function.Make(scope =>
            {
                var x = scope.Find("x");
                var y = scope.Find("y");

                if (x.Type == GPLType.Integer)
                {
                    if (y.Type == GPLType.Integer)
                        return new Integer(((Integer)x).Value - ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Integer)x).Value - ((Float)y).Value);
                }
                else if (x.Type == GPLType.Float)
                {
                    if (y.Type == GPLType.Integer)
                        return new Float(((Float)x).Value - ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Float)x).Value - ((Float)y).Value);
                }

                throw new Exception("Attempting to subtract non-numeric types!");
            }, "x", "y"));

            Scope.Global.Variables.Add("mul", Function.Make(scope =>
            {
                var x = scope.Find("x");
                var y = scope.Find("y");

                if (x.Type == GPLType.Integer)
                {
                    if (y.Type == GPLType.Integer)
                        return new Integer(((Integer)x).Value * ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Integer)x).Value * ((Float)y).Value);
                }
                else if (x.Type == GPLType.Float)
                {
                    if (y.Type == GPLType.Integer)
                        return new Float(((Float)x).Value * ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Float)x).Value * ((Float)y).Value);
                }

                throw new Exception("Attempting to multiply non-numeric types!");
            }, "x", "y"));

            Scope.Global.Variables.Add("div", Function.Make(scope =>
            {
                var x = scope.Find("x");
                var y = scope.Find("y");

                if (x.Type == GPLType.Integer)
                {
                    if (y.Type == GPLType.Integer)
                        return new Integer(((Integer)x).Value / ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Integer)x).Value / ((Float)y).Value);
                }
                else if (x.Type == GPLType.Float)
                {
                    if (y.Type == GPLType.Integer)
                        return new Float(((Float)x).Value / ((Integer)y).Value);
                    else if (y.Type == GPLType.Float)
                        return new Float(((Float)x).Value / ((Float)y).Value);
                }

                throw new Exception("Attempting to divide non-numeric types!");
            }, "x", "y"));

            // TODO: add pow
        }
    }
}
