﻿using System;

namespace SpiceSharpParser.Common.Evaluation
{
    public class UnknownParameterException : Exception
    {
        public UnknownParameterException(string name)
            : base($"Unknown parameter {name}")
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of unknown parameter.
        /// </summary>
        public string Name { get; }
    }
}