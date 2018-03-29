﻿using SpiceSharpParser.Connector.Context;
using SpiceSharpParser.Connector.Exceptions;
using SpiceSharpParser.Model.SpiceObjects;

namespace SpiceSharpParser.Connector.Processors.Controls
{
    /// <summary>
    /// Processes .NODESET <see cref="Control"/> from spice netlist object model.
    /// </summary>
    public class NodeSetControl : BaseControl
    {
        /// <summary>
        /// Gets name of Spice element
        /// </summary>
        public override string TypeName => "nodeset";

        /// <summary>
        /// Processes <see cref="Control"/> statement and modifies the context
        /// </summary>
        /// <param name="statement">A statement to process</param>
        /// <param name="context">A context to modify</param>
        public override void Process(Control statement, IProcessingContext context)
        {
            foreach (var param in statement.Parameters)
            {
                if (param is Model.SpiceObjects.Parameters.AssignmentParameter ap)
                {
                    string type = ap.Name.ToLower();
                    string value = ap.Value;

                    if (type == "v" && ap.Arguments.Count == 1)
                    {
                        context.SetNodeSetVoltage(ap.Arguments[0], value);
                    }
                    else
                    {
                        throw new GeneralConnectorException(".NODESET supports only V(X)=Y");
                    }
                }
            }
        }
    }
}
