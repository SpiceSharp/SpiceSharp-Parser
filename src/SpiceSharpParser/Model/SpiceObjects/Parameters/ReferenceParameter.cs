﻿namespace SpiceSharpParser.Model.SpiceObjects.Parameters
{
    /// <summary>
    /// A reference parameter
    /// </summary>
    public class ReferenceParameter : SingleParameter
    {
        public ReferenceParameter(string reference)
            : base(reference)
        {
        }

        /// <summary>
        /// Closes the object.
        /// </summary>
        /// <returns>A clone of the object</returns>
        public override SpiceObject Clone()
        {
            return new ReferenceParameter(this.Image);
        }
    }
}
