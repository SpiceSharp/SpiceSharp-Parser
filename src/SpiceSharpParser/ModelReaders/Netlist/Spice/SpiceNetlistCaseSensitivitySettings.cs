﻿using SpiceSharpParser.Lexers.Netlist.Spice;

namespace SpiceSharpParser.ModelReaders.Netlist.Spice
{
    /// <summary>
    /// Case-sensitivity settings for netlist reader.
    /// </summary>
    public class SpiceNetlistCaseSensitivitySettings
    {
        private readonly SpiceLexerSettings _lexerSettings;

        public SpiceNetlistCaseSensitivitySettings()
        {
        }

        public SpiceNetlistCaseSensitivitySettings(SpiceLexerSettings lexerSettings)
        {
            _lexerSettings = lexerSettings;
        }

        /// <summary>
        /// Gets or sets a value indicating whether entity names are case-sensitive.
        /// </summary>
        public bool IsEntityNameCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether entity parameter names are case-sensitive.
        /// </summary>
        public bool IsEntityParameterNameCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether dot statements names are case-sensitive.
        /// </summary>
        public bool IsDotStatementNameCaseSensitive => _lexerSettings?.IsDotStatementNameCaseSensitive ?? false;

        /// <summary>
        /// Gets or sets a value indicating whether model types names are case-sensitive.
        /// </summary>
        public bool IsModelTypeCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether node names are case-sensitive.
        /// </summary>
        public bool IsNodeNameCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether function names are case-sensitive.
        /// </summary>
        public bool IsFunctionNameCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether parameter names are case-sensitive.
        /// </summary>
        public bool IsParameterNameCaseSensitive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether let names are case-sensitive.
        /// </summary>
        public bool IsLetExpressionNameCaseSensitive { get; set; } = false;
    }
}