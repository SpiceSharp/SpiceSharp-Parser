﻿using System;
using System.Collections.Generic;
using SpiceSharpParser.Connector.Context;
using SpiceSharpParser.Connector.Exceptions;
using SpiceSharpParser.Connector.Processors.Controls.Exporters.VoltageExports;
using SpiceSharpParser.Model.SpiceObjects;
using SpiceSharpParser.Model.SpiceObjects.Parameters;
using SpiceSharp;
using SpiceSharp.Simulations;

namespace SpiceSharpParser.Connector.Processors.Controls.Exporters
{
    /// <summary>
    /// Generates voltage <see cref="Export"/>
    /// </summary>
    public class VoltageExporter : Exporter
    {
        /// <summary>
        /// Creates a new voltage export
        /// </summary>
        /// <param name="type">A type of export</param>
        /// <param name="parameters">A parameters of export</param>
        /// <param name="simulation">A simulation for export</param>
        /// <param name="context">A context</param>
        /// <returns>
        /// A new export
        /// </returns>
        public override Export CreateExport(string type, ParameterCollection parameters, Simulation simulation, IProcessingContext context)
        {
            if (parameters.Count != 1 || (!(parameters[0] is VectorParameter) && !(parameters[0] is SingleParameter)))
            {
                throw new WrongParameterException("Voltage exports should have vector or single parameter");
            }

            // Get the nodes
            Identifier node, reference = null;
            if (parameters[0] is VectorParameter vector)
            {
                switch (vector.Elements.Count)
                {
                    case 0:
                        throw new WrongParametersCountException("No nodes for voltage export. Node expected");
                    case 2:
                        reference = new StringIdentifier(context.NodeNameGenerator.Generate(vector.Elements[1].Image));
                        goto case 1;
                    case 1:
                        node = new StringIdentifier(context.NodeNameGenerator.Generate(vector.Elements[0].Image));
                        break;
                    default:
                        throw new WrongParametersCountException("Too many nodes specified for voltage export");
                }
            }
            else
            {
                node = new StringIdentifier(context.NodeNameGenerator.Generate(parameters.GetString(0)));
            }

            Export ve = null;
            switch (type.ToLower())
            {
                case "v": ve = new VoltageExport(simulation, node, reference); break;
                case "vr": ve = new VoltageRealExport(simulation, node, reference); break;
                case "vi": ve = new VoltageImaginaryExport(simulation, node, reference); break;
                case "vm": ve = new VoltageMagnitudeExport(simulation, node, reference); break;
                case "vdb": ve = new VoltageDecibelExport(simulation, node, reference); break;
                case "vph":
                case "vp": ve = new VoltagePhaseExport(simulation, node, reference); break;
            }

            return ve;
        }

        /// <summary>
        /// Gets supported voltage exports
        /// </summary>
        /// <returns>
        /// A list of supported voltage exports
        /// </returns>
        public override ICollection<string> GetSupportedTypes()
        {
            return new List<string>() { "v", "vr", "vi", "vm", "vdb", "vp", "vph" };
        }
    }
}