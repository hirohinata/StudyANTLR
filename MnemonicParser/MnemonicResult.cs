using System;

namespace MnemonicParser
{
    internal enum Suffix { U, S, D, L, F, DF }
    internal enum Instruction { LD, LDB, MOV }
    internal enum BitDeviceType { R, DR, MR, LR, B, CR }
    internal enum WordDeviceType { DM, EM, FM, W, ZF, CM }

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

    internal class OperandResult : MnemonicResult {}

    internal class BitDeviceResult : OperandResult
    {
        public BitDeviceType Type { get; }
        public uint No { get; }
        public bool IsLocal { get; }

        public BitDeviceResult(BitDeviceType type, uint no, bool isLocal)
        {
            Type = type;
            No = no;
            IsLocal = isLocal;
        }

        public override string ToString()
        {
            string local = IsLocal ? "@" : "";
            return $"{local}{DeviceName}{DeviceNo}";
        }

        private string DeviceName
        {
            get
            {
                switch (Type)
                {
                    case BitDeviceType.R: return "R";
                    case BitDeviceType.DR: return "DR";
                    case BitDeviceType.MR: return "MR";
                    case BitDeviceType.LR: return "LR";
                    case BitDeviceType.B: return "B";
                    case BitDeviceType.CR: return "CR";
                    default: throw new InvalidOperationException();
                }
            }
        }

        private string DeviceNo
            => $"{No / 16}{No % 16:D02}";
    }

    internal class WordDeviceResult : OperandResult
    {
        public WordDeviceType Type { get; }
        public uint No { get; }
        public bool IsLocal { get; }

        public WordDeviceResult(WordDeviceType type, uint no, bool isLocal)
        {
            Type = type;
            No = no;
            IsLocal = isLocal;
        }

        public override string ToString()
        {
            string local = IsLocal ? "@" : "";
            return $"{local}{DeviceName}{DeviceNo}";
        }

        private string DeviceName
        {
            get
            {
                switch (Type)
                {
                    case WordDeviceType.DM: return "DM";
                    case WordDeviceType.EM: return "EM";
                    case WordDeviceType.FM: return "FM";
                    case WordDeviceType.W: return "W";
                    case WordDeviceType.ZF: return "ZF";
                    case WordDeviceType.CM: return "CM";
                    default: throw new InvalidOperationException();
                }
            }
        }

        private string DeviceNo => No.ToString();
    }

    internal class UnimplementedOperandResult : OperandResult
    {
        public string Text { get; }

        public UnimplementedOperandResult(string text)
            => Text = text;
    }
}
