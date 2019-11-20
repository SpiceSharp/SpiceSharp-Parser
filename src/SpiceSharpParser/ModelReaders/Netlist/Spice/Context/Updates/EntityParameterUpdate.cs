﻿using SpiceSharp.Simulations;
using SpiceSharpParser.Common.Evaluation;

namespace SpiceSharpParser.ModelReaders.Netlist.Spice.Context.Updates
{
    public abstract class EntityParameterUpdate
    {
        public string ParameterName { get; set; }

        public abstract double GetValue(ExpressionContext context);
    }
}
