﻿using System.Collections.Generic;
using SpiceSharp.Circuits;
using SpiceSharp.Components;

namespace SpiceNetlist.SpiceSharpConnector.Processors.EntityGenerators.Models
{
    public class RLCModelGenerator : ModelGenerator
    {
        public override List<string> GetGeneratedSpiceTypes()
        {
            return new List<string>() { "r", "c" };
        }

        internal override Entity GenerateModel(string name, string type)
        {
            switch (type)
            {
                case "r": return new ResistorModel(name); 
                case "c": return new CapacitorModel(name);
            }

            return null;
        }
    }
}