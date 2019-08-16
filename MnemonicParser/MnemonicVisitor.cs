using System;

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
    }
}
