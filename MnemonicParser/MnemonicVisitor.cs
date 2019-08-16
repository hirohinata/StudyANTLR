using System;
using System.Linq;
using Antlr4.Runtime.Misc;

namespace MnemonicParser
{
    internal class MnemonicVisitor : gen.MnemonicBaseVisitor<MnemonicResult>
    {
        private readonly Plc _plc;

        public MnemonicVisitor(Plc plc)
        {
            _plc = plc;

            _plc.WordDevices["DM1"] = 10;
        }

        public override MnemonicResult VisitMnemonic([NotNull] gen.MnemonicParser.MnemonicContext context)
        {
            var inst = (InstructionResult)Visit(context.instruction());
            var operands = context.operand_list().Select(Visit);
            return base.VisitMnemonic(context);
        }

        public override MnemonicResult VisitInstruction([NotNull] gen.MnemonicParser.InstructionContext context)
        {
            return new InstructionResult(
                GetInstruction(context.name.Text),
                GetSuffix(context.suffix())
            );
        }

        private Instruction GetInstruction([NotNull]string instructionName)
        {
            switch (instructionName.ToUpper())
            {
                case "LD": return Instruction.LD;
                case "LDB": return Instruction.LDB;
                case "MOV": return Instruction.MOV;
                default: throw new InvalidOperationException($"存在しない命令語( {instructionName} )です。");
            }
        }

        private Suffix GetSuffix([Nullable]gen.MnemonicParser.SuffixContext context)
        {
            if (context == null) return Suffix.S;
            switch (context.IDENTIFIER().GetText().ToUpper())
            {
                case "U": return Suffix.U;
                case "S": return Suffix.S;
                case "D": return Suffix.D;
                case "L": return Suffix.L;
                case "F": return Suffix.F;
                case "DF": return Suffix.DF;
                default: throw new InvalidOperationException($"存在しないサフィックス( {context.GetText()} )です。");
            }
        }
    }
}
