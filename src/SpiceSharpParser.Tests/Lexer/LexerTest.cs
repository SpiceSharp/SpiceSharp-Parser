using SpiceSharpParser.Lexer;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpiceSharpParser.Tests.Lexer
{
    public class LexerTest
    {
        [Fact]
        public void EmptyGrammarEmptyText()
        {
            LexerGrammar<LexerTestState> grammar = new LexerGrammar<LexerTestState>(new List<LexerTokenRule<LexerTestState>>());
            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar, new LexerOptions(false, null));
            var tokens = lexer.GetTokens(string.Empty);
            Assert.Single(tokens);
        }

        [Fact]
        public void EmptyGrammarNonEmptyText()
        {
            LexerGrammar<LexerTestState> grammar = new LexerGrammar<LexerTestState>(new List<LexerTokenRule<LexerTestState>>());
            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar, new LexerOptions(false, null));
            Assert.Throws<LexerException>(() => lexer.GetTokens("Line1\nLine2\n").Count());
        }

        [Fact]
        public void NonEmptyGrammarNonEmptyText()
        {
            LexerGrammar<LexerTestState> grammar = new LexerGrammar<LexerTestState>(new List<LexerTokenRule<LexerTestState>>()
                {
                    new LexerTokenRule<LexerTestState>(1, "Text", "[a-zA-Z0-9]*"),
                    new LexerTokenRule<LexerTestState>(
                        2,
                        "NewLine",
                        "\n",
                        (LexerTestState state) =>
                        {
                            state.LineNumber++;
                            return LexerRuleResult.ReturnToken;
                        })
                });

            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar, new LexerOptions(false, null));
            var s = new LexerTestState();
            var tokens = lexer.GetTokens("Line1\nLine2\n", s).ToArray();
            Assert.Equal(5, tokens.Count());

            Assert.Equal(2, s.LineNumber);
        }

        public class LexerTestState : LexerState
        {
            public int LineNumber { get; set; } = 0;
        }
    }
}