﻿using NSubstitute;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Context;
using SpiceSharpParser.Models.Netlist.Spice.Objects;
using SpiceSharpParser.Models.Netlist.Spice.Objects.Parameters;
using System.Collections.Generic;
using NSubstitute.Core;
using SpiceSharpParser.ModelReaders.Netlist.Spice;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Context.Names;
using Xunit;
using SpiceSharpParser.ModelReaders.Netlist.Spice.Readers.Controls;

namespace SpiceSharpParser.Tests.ModelReaders.Spice.Readers.Controls.Simulations
{
    public class NodeSetControlTests
    {
        [Fact]
        public void Read()
        {
            // prepare
            var control = new Control()
            {
                Name = "nodeset",
                Parameters = new ParameterCollection()
                {
                    new AssignmentParameter()
                    {
                        Name = "V",
                        Arguments = new List<string>()
                        {
                            "input"
                        },
                        Value = "12"
                    },
                    new AssignmentParameter()
                    {
                        Name = "V",
                        Arguments = new List<string>()
                        {
                            "x"
                        },
                        Value = "13"
                    }
                }
            };

            var readingContext = Substitute.For<ICircuitContext>();
            readingContext.CaseSensitivity = new SpiceNetlistCaseSensitivitySettings();
            readingContext.NameGenerator = Substitute.For<INameGenerator>();
            readingContext.NameGenerator.GenerateNodeName(Arg.Any<string>()).Returns(x => x[0].ToString());
            readingContext.SimulationPreparations.Returns(Substitute.For<ISimulationPreparations>());
            // act
            var nodeSetControl = new NodeSetControl();
            nodeSetControl.Read(control, readingContext);

            // assert
            readingContext.SimulationPreparations.Received().SetNodeSetVoltage("input", "12", readingContext);
            readingContext.SimulationPreparations.Received().SetNodeSetVoltage("x", "13", readingContext);
        }
    }
}
