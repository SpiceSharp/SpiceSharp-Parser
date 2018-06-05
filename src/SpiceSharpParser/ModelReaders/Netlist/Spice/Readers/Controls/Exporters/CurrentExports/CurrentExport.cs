﻿using SpiceSharpParser.ModelsReaders.Netlist.Spice.Exceptions;
using SpiceSharp;
using SpiceSharp.Simulations;

namespace SpiceSharpParser.ModelsReaders.Netlist.Spice.Readers.Controls.Exporters.CurrentExports
{
    /// <summary>
    /// Current export.
    /// </summary>
    public class CurrentExport : Export
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentExport"/> class.
        /// </summary>
        /// <param name="simulation">A simulation</param>
        /// <param name="source">A identifier</param>
        public CurrentExport(Simulation simulation, Identifier source)
            : base(simulation)
        {
            if (simulation == null)
            {
                throw new System.ArgumentNullException(nameof(simulation));
            }

            Source = source ?? throw new System.NullReferenceException(nameof(source));

            if (simulation is FrequencySimulation)
            {
                ExportImpl = new ComplexPropertyExport(simulation, source, "i");
            }
            else
            {
                ExportRealImpl = new RealPropertyExport(simulation, source, "i");
            }

            Name = "i(" + Source + ")";
        }

        /// <summary>
        /// Gets the main node
        /// </summary>
        public Identifier Source { get; }

        /// <summary>
        /// Gets the type name
        /// </summary>
        public override string TypeName => "current";

        /// <summary>
        /// Gets the quantity unit
        /// </summary>
        public override string QuantityUnit => "Current (A)";

        /// <summary>
        /// Gets the real exporter
        /// </summary>
        protected RealPropertyExport ExportRealImpl { get; }

        /// <summary>
        /// Gets the complex exporter
        /// </summary>
        protected ComplexPropertyExport ExportImpl { get; }

        /// <summary>
        /// Extracts the current value
        /// </summary>
        /// <returns>
        /// A current value at the source
        /// </returns>
        public override double Extract()
        {
            if (ExportRealImpl != null)
            {
                if (!ExportRealImpl.IsValid)
                {
                    if (ExceptionsEnabled)
                    {
                        throw new GeneralReaderException($"Current export {Name} is invalid");
                    }
                    return double.NaN;
                }

                return ExportRealImpl.Value;
            }
            else
            {
                if (!ExportImpl.IsValid)
                {
                    if (ExceptionsEnabled)
                    {
                        throw new GeneralReaderException($"Current export {Name} is invalid");
                    }
                    return double.NaN;
                }
                return ExportImpl.Value.Real;
            }
        }
    }
}
