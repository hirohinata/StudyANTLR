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
        public ushort Value { get; set; }
    }

    public class Plc
    {
        private Dictionary<string, BitDevice> _bitDevices = new Dictionary<string, BitDevice>();
        private Dictionary<string, WordDevice> _wordDevices { get; } = new Dictionary<string, WordDevice>();

        public DeviceCollection<BitDevice> BitDevices => new DeviceCollection<BitDevice>(_bitDevices);
        public DeviceCollection<WordDevice> WordDevices => new DeviceCollection<WordDevice>(_wordDevices);

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
