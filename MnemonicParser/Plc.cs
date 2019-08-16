using System;
using System.Collections.Generic;

namespace MnemonicParser
{
    public class Plc
    {
        public Dictionary<string, bool> BitDevices { get; } = new Dictionary<string, bool>();
        public Dictionary<string, ushort> WordDevices { get; } = new Dictionary<string, ushort>();
    }
}
