using System;
using System.Collections.Generic;

namespace MnemonicParser
{
    public class BitDevice
    {
        public bool Value { get; set; }
    }

    public class WordDevice
    {
        public virtual ushort Value { get; set; }
    }

    public class ZDevice
    {
        public uint Value { get; set; }

        public WordDevice ToLowWordDevice() => new LowWordDevice(this);
        public WordDevice ToHighWordDevice() => new HighWordDevice(this);

        private class LowWordDevice : WordDevice
        {
            private readonly ZDevice _src;
            public LowWordDevice(ZDevice src) => _src = src;

            public override ushort Value {
                get => (ushort)(_src.Value & 0x0000FFFF);
                set => _src.Value = (_src.Value & 0xFFFF0000) | value;
            }
        }

        private class HighWordDevice : WordDevice
        {
            private readonly ZDevice _src;
            public HighWordDevice(ZDevice src) => _src = src;

            public override ushort Value
            {
                get => (ushort)((_src.Value & 0xFFFF0000) >> 16);
                set => _src.Value = (_src.Value & 0x0000FFFF) | (uint)(value << 16);
            }
        }
    }

    public class Plc
    {
        private Dictionary<string, BitDevice> _bitDevices = new Dictionary<string, BitDevice>();
        private Dictionary<string, WordDevice> _wordDevices { get; } = new Dictionary<string, WordDevice>();
        private Dictionary<string, ZDevice> _zDevices { get; } = new Dictionary<string, ZDevice>();

        public DeviceCollection<BitDevice> BitDevices => new DeviceCollection<BitDevice>(_bitDevices);
        public DeviceCollection<WordDevice> WordDevices => new DeviceCollection<WordDevice>(_wordDevices);
        public DeviceCollection<ZDevice> ZDevices => new DeviceCollection<ZDevice>(_zDevices);

        public class DeviceCollection<T> where T : new()
        {
            private readonly Dictionary<string, T> _source;

            public DeviceCollection(Dictionary<string, T> source)
                => _source = source;

            public T this[string name]
            {
                get
                {
                    if (!_source.ContainsKey(name))
                    {
                        _source.Add(name, new T());
                    }
                    return _source[name];
                }
            }
        }
    }
}
