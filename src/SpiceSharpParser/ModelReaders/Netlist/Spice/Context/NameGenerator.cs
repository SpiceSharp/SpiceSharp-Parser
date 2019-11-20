﻿using System.Collections.Generic;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Context.Names;

namespace SpiceSharpParser.ModelReaders.Netlist.Spice.Context
{
    public class NameGenerator : INameGenerator
    {
        public NameGenerator(INodeNameGenerator nodeNameGenerator, IObjectNameGenerator objectNameGenerator)
        {
            NodeNameGenerator = nodeNameGenerator;
            ObjectNameGenerator = objectNameGenerator;
        }

        protected INodeNameGenerator NodeNameGenerator { get; }

        protected IObjectNameGenerator ObjectNameGenerator { get; }

        public IEnumerable<string> Globals => NodeNameGenerator.Globals;

        public string ParseNodeName(string nodePath)
        {
            return NodeNameGenerator.Parse(nodePath);
        }

        public string GenerateObjectName(string entityName)
        {
            return ObjectNameGenerator.Generate(entityName);
        }

        public IObjectNameGenerator CreateChildNameGenerator(string name)
        {
            return ObjectNameGenerator.CreateChildGenerator(name);
        }

        public string GenerateNodeName(string nodeName)
        {
            return NodeNameGenerator.Generate(nodeName);
        }

        public void AddChild(INodeNameGenerator nodeNameGenerator)
        {
            NodeNameGenerator.Children.Add(nodeNameGenerator);
        }

        public void SetGlobal(string spImage)
        {
            NodeNameGenerator.SetGlobal(spImage);
        }
    }
}
