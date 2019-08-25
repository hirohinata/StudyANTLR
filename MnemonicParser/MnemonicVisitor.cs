using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;

namespace MnemonicParser
{
    internal class MnemonicVisitor : gen.MnemonicBaseVisitor<MnemonicResult>
    {
        private readonly Plc _plc;
        private bool _executeFlg;

        public MnemonicVisitor(Plc plc)
        {
            _plc = plc;
            _executeFlg = false;
        }

        public override MnemonicResult VisitMnemonic([NotNull] gen.MnemonicParser.MnemonicContext context)
        {
            var inst = (InstructionResult)Visit(context.instruction());

            var operands =
                    context
                    .operand_list()
                    .operand()
                    .Select(Visit)
                    .Cast<OperandResult>()
                    .ToList();

            Execute(inst, operands);
            return DefaultResult;
        }

        private void Execute(InstructionResult inst, List<OperandResult> operands)
        {
            switch (inst.Instruction)
            {
                case Instruction.LD:
                    _executeFlg = _plc.BitDevices[operands[0].Text];
                    break;

                case Instruction.LDB:
                    _executeFlg = !_plc.BitDevices[operands[0].Text];
                    break;

                case Instruction.MOV:
                    if (_executeFlg)
                    {
                        _plc.WordDevices[operands[1].Text] = _plc.WordDevices[operands[0].Text];
                    }
                    break;
            }
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

        public override MnemonicResult VisitIndexableOperand([NotNull] gen.MnemonicParser.IndexableOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }

        public override MnemonicResult VisitRelayDeviceOperand([NotNull] gen.MnemonicParser.RelayDeviceOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }

        public override MnemonicResult VisitWordBitDeviceOperand([NotNull] gen.MnemonicParser.WordBitDeviceOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }

        public override MnemonicResult VisitOldIndirectOperand([NotNull] gen.MnemonicParser.OldIndirectOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }

        public override MnemonicResult VisitZDeviceOperand([NotNull] gen.MnemonicParser.ZDeviceOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }

        public override MnemonicResult VisitNoneOperand([NotNull] gen.MnemonicParser.NoneOperandContext context)
        {
            //TODO: 各種対応
            return new OperandResult(context.GetText());
        }
    }
}
