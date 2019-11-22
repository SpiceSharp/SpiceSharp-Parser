﻿namespace SpiceSharpParser.Models.Netlist.Spice.Objects.Parameters
{
    /// <summary>
    /// An expression parameter.
    /// </summary>
    public class ExpressionParameter : SingleParameter
    {
        public ExpressionParameter(string expression)
            : base(expression)
        {
        }

        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <returns>A clone of the object.</returns>
        public override SpiceObject Clone()
        {
            return new ExpressionParameter(Image) { LineNumber = LineNumber };
        }
    }
}