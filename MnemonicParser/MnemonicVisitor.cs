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
            BitDevice GetBitDevice(OperandResult operand)
            {
                if (operand is BitDeviceResult bitDevice)
                    return _plc.BitDevices[bitDevice.ToString()];
                throw new InvalidOperationException($"( {operand} )はリレーデバイスではありません。");
            }

            WordDevice GetWordDevice(OperandResult operand)
            {
                if (operand is WordDeviceResult wordDevice)
                    return _plc.WordDevices[wordDevice.ToString()];
                throw new InvalidOperationException($"( {operand} )はワードデバイスではありません。");
            }

            switch (inst.Instruction)
            {
                case Instruction.LD:
                    _executeFlg = GetBitDevice(operands[0]).Value;
                    break;

                case Instruction.LDB:
                    _executeFlg = !GetBitDevice(operands[0]).Value;
                    break;

                case Instruction.MOV:
                    if (_executeFlg)
                    {
                        GetWordDevice(operands[1]).Value = GetWordDevice(operands[0]).Value;
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
            return new UnimplementedOperandResult(context.GetText());
        }

        public override MnemonicResult VisitRelayDeviceOperand([NotNull] gen.MnemonicParser.RelayDeviceOperandContext context)
        {
            uint GetDeviceNo(string text, int index)
                => uint.Parse(text.Substring(index));

            (BitDeviceType, uint) ParseRelayDevice(string text)
            {
                if (text.Length < 2)
                    throw new InvalidOperationException($"不正なオペランド( {context.GetText()} )です。");

                switch (text[0])
                {
                    case 'R':
                        return (BitDeviceType.R, GetDeviceNo(text, 1));
                    case 'B':
                        return (BitDeviceType.B, GetDeviceNo(text, 1));
                    case 'D':
                        if (text[1] != 'R') break;
                        return (BitDeviceType.DR, GetDeviceNo(text, 2));
                    case 'M':
                        if (text[1] != 'R') break;
                        return (BitDeviceType.MR, GetDeviceNo(text, 2));
                    case 'L':
                        if (text[1] != 'R') break;
                        return (BitDeviceType.LR, GetDeviceNo(text, 2));
                    case 'C':
                        if (text[1] != 'R') break;
                        return (BitDeviceType.CR, GetDeviceNo(text, 2));
                }
                throw new InvalidOperationException($"不正なオペランド( {context.GetText()} )です。");
            }

            var (type, no) = ParseRelayDevice(context.RELAY_DEVICE().Symbol.Text);
            var isLocal = context.AT() != null;
            return new BitDeviceResult(type, no, isLocal);
        }

        public override MnemonicResult VisitWordDeviceOperand([NotNull] gen.MnemonicParser.WordDeviceOperandContext context)
        {
            uint GetDeviceNo(string text, int index)
                => uint.Parse(text.Substring(index));

            (WordDeviceType, uint) ParseWordDevice(string text)
            {
                if (text.Length < 2)
                    throw new InvalidOperationException($"不正なオペランド( {context.GetText()} )です。");

                switch (text[0])
                {
                    case 'D':
                        if (text[1] != 'M') break;
                        return (WordDeviceType.DM, GetDeviceNo(text, 2));
                    case 'E':
                        if (text[1] != 'M') break;
                        return (WordDeviceType.EM, GetDeviceNo(text, 2));
                    case 'F':
                        if (text[1] != 'M') break;
                        return (WordDeviceType.FM, GetDeviceNo(text, 2));
                    case 'W':
                        return (WordDeviceType.W, GetDeviceNo(text, 1));
                    case 'Z':
                        if (text[1] != 'F') break;
                        return (WordDeviceType.ZF, GetDeviceNo(text, 2));
                    case 'C':
                        if (text[1] != 'M') break;
                        return (WordDeviceType.CM, GetDeviceNo(text, 2));
                }
                throw new InvalidOperationException($"不正なオペランド( {context.GetText()} )です。");
            }

            //TODO: 実装
            if (context.ASTERISK() != null)
                throw new NotImplementedException();

            var (type, no) = ParseWordDevice(context.device.Text);
            var isLocal = context.AT() != null;
            return new WordDeviceResult(type, no, isLocal);
        }

        public override MnemonicResult VisitWordBitDeviceOperand([NotNull] gen.MnemonicParser.WordBitDeviceOperandContext context)
        {
            //TODO: 各種対応
            return new UnimplementedOperandResult(context.GetText());
        }

        public override MnemonicResult VisitOldIndirectOperand([NotNull] gen.MnemonicParser.OldIndirectOperandContext context)
        {
            //TODO: 各種対応
            return new UnimplementedOperandResult(context.GetText());
        }

        public override MnemonicResult VisitZDeviceOperand([NotNull] gen.MnemonicParser.ZDeviceOperandContext context)
        {
            //TODO: 各種対応
            return new UnimplementedOperandResult(context.GetText());
        }

        public override MnemonicResult VisitNoneOperand([NotNull] gen.MnemonicParser.NoneOperandContext context)
        {
            var indexableOperand = context.indexable_operand();
            if (indexableOperand != null)
                return Visit(indexableOperand);

            return Visit(context.literal());
        }
    }
}
