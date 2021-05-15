﻿using System;
using System.Collections.Generic;
using System.Linq;
using SpiceSharpParser.Models.Netlist.Spice.Objects;
using SpiceSharpParser.Models.Netlist.Spice.Objects.Parameters;
using SpiceSharpParser.ModelWriters.CSharp;

namespace SpiceSharpParser.ModelWriters.CSharp
{
    /// <summary>
    /// Writer for pulse waveform.
    /// </summary>
    public class PulseWriter : BaseWriter, IWaveformWriter
    {
        /// <summary>
        /// Generates a new waveform.
        /// </summary>
        /// <param name="parameters">Parameters for waveform.</param>
        /// <param name="context">A context.</param>
        /// <returns>
        /// A new waveform.
        /// </returns>
        public List<CSharpStatement> Generate(ParameterCollection parameters, IWriterContext context, out string waveFormId)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (parameters.Count > 7 || parameters.Count == 1 && parameters[0] is VectorParameter vp && vp.Elements.Count > 7)
            {
                waveFormId = null;
                return null;
            }

            var result = new List<CSharpStatement>();
            waveFormId = context.GetNewIdentifier("pulse");
            result.Add(new CSharpNewStatement(waveFormId, "new Pulse()") { IncludeInCollection = false });

            if (parameters.Count == 1 && parameters[0] is VectorParameter v)
            {
                parameters = new ParameterCollection(v.Elements.Select(e => e).Cast<Parameter>().ToList());
            }

            if (parameters.Count >= 1)
            {
                result.Add(SetProperty(waveFormId, "InitialValue", parameters[0].Value, context));
            }

            if (parameters.Count >= 2)
            {
                result.Add(SetProperty(waveFormId, "PulsedValue", parameters[1].Value, context));
            }

            if (parameters.Count >= 3)
            {
                result.Add(SetProperty(waveFormId, "Delay", parameters[2].Value, context));
            }

            if (parameters.Count >= 4)
            {
                result.Add(SetProperty(waveFormId, "RiseTime", parameters[3].Value, context));
            }

            if (parameters.Count >= 5)
            {
                result.Add(SetProperty(waveFormId, "FallTime", parameters[4].Value, context));
            }

            if (parameters.Count >= 6)
            {
                result.Add(SetProperty(waveFormId, "PulseWidth", parameters[5].Value, context));
            }

            if (parameters.Count == 7)
            {
                result.Add(SetProperty(waveFormId, "Period", parameters[6].Value, context));
            }

            return result;
        }
    }
}