﻿using SpiceSharp.Components;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Common;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Context;
using SpiceSharpParser.Models.Netlist.Spice.Objects.Parameters;

namespace SpiceSharpParser.ModelReaders.Netlist.Spice.Readers.Waveforms
{
    /// <summary>
    /// Generates a waveform
    /// </summary>
    public abstract class WaveformGenerator : ISpiceObjectReader
    {
        /// <summary>
        /// Gets the type name of generated waveform
        /// </summary>
        public abstract string SpiceCommandName { get; }

        /// <summary>
        /// Generats a new waveform
        /// </summary>
        /// <param name="bracketParameter">A parameter for waveform</param>
        /// <param name="context">A context</param>
        /// <returns>
        /// A new waveform
        /// </returns>
        public abstract Waveform Generate(BracketParameter bracketParameter, IReadingContext context);
    }
}
