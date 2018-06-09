using System;
using System.Collections.Generic;
using Xunit;

namespace SpiceSharpParser.IntegrationTests
{
    public class StTest : BaseTest
    {
        [Fact]
        public void ListModelParamCountTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 -1",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".ST LIST 1N914(N) 1.752 1.234 1.2 1.0 0.1",
                ".END");

            Assert.Equal(5, result.Exports.Count);
            Assert.Equal(5, result.Simulations.Count);
        }

        [Fact]
        public void ListVoltageCountTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 -1",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".ST LIST V1 1.752 1.234 1.2 1.0 0.1",
                ".END");

            Assert.Equal(5, result.Exports.Count);
            Assert.Equal(5, result.Simulations.Count);
        }

        [Fact]
        public void ListParamWithoutDeclarationCountTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 {X}",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".ST LIST X 1 2 3 4 5",
                ".END");

            Assert.Equal(5, result.Exports.Count);
            Assert.Equal(5, result.Simulations.Count);
        }

        [Fact]
        public void ListParamCountTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 {X}",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".PARAM X=1",
                ".ST LIST X 1 2 3 4 5",
                ".END");

            Assert.Equal(5, result.Exports.Count);
            Assert.Equal(5, result.Simulations.Count);
        }

        [Fact]
        public void ListParamMultipleStCountTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 {X}",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".PARAM X=1",
                ".ST LIST X 1 2 3 4 5",
                ".ST LIST Y 10 20",
                ".END");

            Assert.Equal(10, result.Exports.Count);
            Assert.Equal(10, result.Simulations.Count);
        }

        [Fact]
        public void ListParamTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 {X}",
                "R1 1 0 100",
                ".OP",
                ".SAVE i(R1)",
                ".PARAM X=0",
                ".ST LIST X 1 2",
                ".END");

            Assert.Equal(2, result.Exports.Count);
            Assert.Equal(2, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            Assert.Equal(-0.01, exports[0]);
            Assert.Equal(-0.02, exports[1]);
        }

        [Fact]
        public void LinParamTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 {X}",
                "R1 1 0 100",
                ".OP",
                ".SAVE i(R1)",
                ".PARAM X=0",
                ".ST X 1 100 1",
                ".END");

            Assert.Equal(99, result.Exports.Count);
            Assert.Equal(99, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            for (var i = 0; i < exports.Count; i++)
            {
                Assert.Equal(-0.01 * (i+1), exports[i]);
            }
        }

        [Fact]
        public void ListVoltageTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1",
                "R1 1 0 100",
                ".OP",
                ".SAVE i(R1)",
                ".ST LIST V1 1 2",
                ".END");

            Assert.Equal(2, result.Exports.Count);
            Assert.Equal(2, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            Assert.Equal(-0.01, exports[0]);
            Assert.Equal(-0.02, exports[1]);
        }

        [Fact]
        public void ListModelParameterTest()
        {
            var result = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 10",
                ".model 1N914 D(Is=2.52e-9 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".ST LIST 1N914(N) 1.752 1.753 1.754",
                ".END");

            Assert.Equal(3, result.Exports.Count);
            Assert.Equal(3, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            Assert.Equal(-0.092108909328949162, exports[0]);
            Assert.Equal(-0.09210442734453031, exports[1]);
            Assert.Equal(-0.09209994538622851, exports[2]);
        }

        [Fact]
        public void ListDeviceParameterTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 1",
                "R1 1 0 100",
                ".OP",
                ".SAVE i(R1)",
                ".ST LIST @R1[resistance] 100 200 400",
                ".END");

            Assert.Equal(3, result.Exports.Count);
            Assert.Equal(3, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            Assert.Equal(-0.01, exports[0]);
            Assert.Equal(-0.005, exports[1]);
            Assert.Equal(-0.0025, exports[2]);
        }

        [Fact]
        public void LinParamWithTableTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 100",
                "R1 1 0 {R}",
                ".OP",
                ".SAVE i(R1)",
                ".PARAM N=0",
                ".PARAM R={table(N, 1, 10, 2, 20, 3, 30)}",
                ".ST N 1 4 1",
                ".END");

            Assert.Equal(3, result.Exports.Count);
            Assert.Equal(3, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            for (var i = 0; i < exports.Count; i++)
            {
                Assert.Equal(-100 /(10.00 * (i + 1)), exports[i]);
            }
        }

        [Fact]
        public void LinParamWithAdvancedTableTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 100",
                "R1 1 0 {R}",
                ".OP",
                ".SAVE i(R1)",
                ".PARAM N=0",
                ".PARAM R={table(N, 1, pow(10, 1), 2*0+2-0, 20 + 0, 3, 30)}",
                ".ST N 1 4 1",
                ".END");

            Assert.Equal(3, result.Exports.Count);
            Assert.Equal(3, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            for (var i = 0; i < exports.Count; i++)
            {
                Assert.Equal(-100 / (10.00 * (i + 1)), exports[i]);
            }
        }

        [Fact]
        public void LinParamWithDependedTableTest()
        {
            var result = ParseNetlist(
                "Test circuit",
                "V1 0 1 100",
                "R1 1 0 {R}",
                ".OP",
                ".SAVE i(R1)",
                ".PARAM N=0",
                ".PARAM M=N",
                ".PARAM R={table(M, 1, 10, 2, 20, 3, 30)}",
                ".ST N 1 4 1",
                ".END");

            Assert.Equal(3, result.Exports.Count);
            Assert.Equal(3, result.Simulations.Count);

            var exports = RunSimulationsAndReturnExports(result);

            for (var i = 0; i < exports.Count; i++)
            {
                Assert.Equal(-100 / (10.00 * (i + 1)), exports[i]);
            }
        }

        [Fact]
        public void DCParameterSweepWithStTest()
        {
            var result = ParseNetlist(
                "Sweeping parameters",
                "V1 in gnd 11",
                "R1 in out {a}",
                "R2 out gnd {R}",
                ".param R = 0",
                ".param a = 0",
                ".DC a 1 2 0.5",
                ".st LIST R 10 100",
                ".SAVE v(out)",
                ".END");

            var exports = RunSimulationsAndReturnExports(result);

            var r10 = ((List<Double>)exports[0]);
            EqualsWithTol(10.0 / (10.0 + 1) * 11, r10[0]);
            EqualsWithTol(10.0 / (10.0 + 1.5) * 11, r10[1]);
            EqualsWithTol(10.0 / (10.0 + 2) * 11, r10[2]);

            var r100 = ((List<Double>)exports[1]);
            EqualsWithTol(100.0 / (100.0 + 1) * 11, r100[0]);
            EqualsWithTol(100.0 / (100.0 + 1.5) * 11, r100[1]);
            EqualsWithTol(100.0 / (100.0 + 2) * 11, r100[2]);
        }
    

        [Fact]
        public void ListIcParameterTest()
        {
            double dcVoltage = 10;
            double resistorResistance = 10e3; // 10000;
            double capacitance = 1e-6; // 0.000001;
            double tau = resistorResistance * capacitance;

            var result = ParseNetlist(
                "The initial voltage on capacitor is {X} V. The result should be an exponential converging to dcVoltage.",
                "C1 OUT 0 1e-6",
                "R1 IN OUT 10e3",
                "V1 IN 0 10",
                ".IC V(OUT)={X}",
                ".TRAN 1e-8 10e-6",
                ".SAVE V(OUT)",
                ".PARAM X = 0",
                ".ST LIST X 0.0 1.0 2.0",
                ".END");

            var exports = RunSimulationsAndReturnExports(result);

            for (var i = 0; i < exports.Count; i++)
            {
                Func<double, double>[] references = { t => i + dcVoltage * (1.0 - Math.Exp(-t / tau)) };
                EqualsWithTol((IEnumerable<Tuple<double, double>>)exports[i], references);
            }
        }

        [Fact]
        public void StTempCountTest()
        {
            var netlist = ParseNetlist(
                "Diode circuit",
                "D1 OUT 0 1N914",
                "R1 OUT 1 100",
                "V1 1 0 -1",
                ".model 1N914 D(Is=2.52e-9 Rs=0.568 N=1.752 Cjo=4e-12 M=0.4 tt=20e-9)",
                ".OP",
                ".SAVE i(V1)",
                ".ST TEMP 30 100 1",
                ".END");

            Assert.Equal(70, netlist.Simulations.Count);
        }
    }
}
