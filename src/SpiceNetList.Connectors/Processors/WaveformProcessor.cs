﻿using SpiceNetlist.SpiceObjects.Parameters;
using SpiceSharp.Components;

namespace SpiceNetlist.SpiceSharpConnector.Processors
{
    public class WaveformProcessor
    {
        public WaveformProcessor(WaveformRegistry registry)
        {
            Registry = registry;
        }

        public WaveformRegistry Registry { get; }

        public Waveform Generate(BracketParameter cp, ProcessingContext context)
        {
            string type = cp.Name.ToLower();
            if (!Registry.Supports(cp.Name))
            {
                throw new System.Exception("Unsupported waveform");
            }

            return Registry.GetGenerator(type).Generate(cp, context);
        }
    }
}
