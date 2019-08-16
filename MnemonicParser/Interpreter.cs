using System;
using Antlr4.Runtime;

namespace MnemonicParser
{
    public static class Interpreter
    {
        public static void Execute(Plc plc, string mnemonic)
        {
            var stream = new AntlrInputStream(mnemonic);
            var lexer = new gen.MnemonicLexer(stream);
            var parser = new gen.MnemonicParser(new CommonTokenStream(lexer));
            var visitor = new MnemonicVisitor(plc);
            visitor.Visit(parser.input());
        }
    }
}
