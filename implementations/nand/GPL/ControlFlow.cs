﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPL
{
    public class Gb2 : IExpression
    {
        public IExpression Value { get; private set; }

        public Gb2(IExpression value)
        {
            this.Value = value;
        }

        public Value Evaluate(Scope scope)
        {
            throw new Gb2Exception(Value.Evaluate(scope));
        }
    }

    public class Gb2Exception : Exception
    {
        public Value Value { get; private set; }

        public Gb2Exception(Value value) : base("Unhandled gb2 expression!")
        {
            this.Value = value;
        }
    }
}