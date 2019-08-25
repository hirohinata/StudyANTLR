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

        public static string NormalizeBitDevice(string text)
        {
            MnemonicResult result = VisitOperand(text);
            return (result as BitDeviceResult)?.ToString() ?? "";
        }

        public static string NormalizeWordDevice(string text)
        {
            MnemonicResult result = VisitOperand(text);
            return (result as WordDeviceResult)?.ToString() ?? "";
        }

        private static MnemonicResult VisitOperand(string text)
        {
            var stream = new AntlrInputStream(text);
            var lexer = new gen.MnemonicLexer(stream);
            var parser = new gen.MnemonicParser(new CommonTokenStream(lexer));
            var visitor = new MnemonicVisitor(null);
            return visitor.Visit(parser.operand());
        }
    }
}
