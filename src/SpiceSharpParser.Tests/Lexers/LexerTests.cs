using SpiceSharpParser.Lexers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpiceSharpParser.Tests.Lexers
{
    public class LexerTests
    {
        [Fact]
        public void EmptyGrammarEmptyText()
        {
            LexerGrammar<LexerTestState> grammar = new LexerGrammar<LexerTestState>(new List<LexerTokenRule<LexerTestState>>(), new List<LexerDynamicRule>());
            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar);
            var tokens = lexer.GetTokens(string.Empty);
            Assert.Single(tokens);
        }

        [Fact]
        public void EmptyGrammarNonEmptyText()
        {
            LexerGrammar<LexerTestState> grammar = new LexerGrammar<LexerTestState>(new List<LexerTokenRule<LexerTestState>>(), new List<LexerDynamicRule>());
            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar);
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
                        (LexerTestState state, string lexem) =>
                        {
                            state.LineNumber++;
                            return LexerRuleReturnDecision.ReturnToken;
                        })
                }, new List<LexerDynamicRule>());

            Lexer<LexerTestState> lexer = new Lexer<LexerTestState>(grammar);
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