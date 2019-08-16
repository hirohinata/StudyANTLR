using System;

namespace MnemonicParser
{
    internal enum Suffix { U, S, D, L, F, DF }
    internal enum Instruction { LD, LDB, MOV }

    internal class MnemonicResult {}

    internal class InstructionResult : MnemonicResult
    {
        public Instruction Instruction { get; }
        public Suffix Suffix { get; }

        public InstructionResult(Instruction instruction, Suffix suffix)
        {
            Instruction = instruction;
            Suffix = suffix;
        }
    }

    internal class OperandResult : MnemonicResult
    {
        public string Text { get; }

        public OperandResult(string text)
        {
            Text = text;
        }
    }
}
