using System;

namespace MnemonicParser
{
    public static class Interpreter
    {
        public static void Execute(Plc plc, string mnemonic)
        {
            plc.WordDevices["DM1"] = 10;
        }
    }
}
