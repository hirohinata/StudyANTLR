using System;
using System.Collections.Generic;
using MnemonicParser;
using NUnit.Framework;

namespace MnemonicParserTest
{
    [TestFixture]
    public class PlcTest
    {
        private const bool ON = true;
        private const bool OFF = false;

        private BitDevice GetBitDevice(Plc plc, string deviceText)
            => plc.BitDevices[Interpreter.NormalizeBitDevice(deviceText)];

        private WordDevice GetWordDevice(Plc plc, string deviceText)
            => plc.WordDevices[Interpreter.NormalizeWordDevice(deviceText)];

        private ZDevice GetZDevice(Plc plc, string deviceText)
            => plc.ZDevices[Interpreter.NormalizeZDevice(deviceText)];

        [Test, TestCaseSource(nameof(TestCaseSource))]
        public void Test(
            Dictionary<string, bool> bitDevices,
            Dictionary<string, ushort> wordDevices,
            Dictionary<string, uint> zDevices,
            string mnemonic,
            Dictionary<string, bool> expectedBitDevices,
            Dictionary<string, ushort> expectedWordDevices,
            Dictionary<string, uint> expectedZDevices)
        {
            var plc = new Plc();

            // 実行前のセットアップ
            foreach (var device in bitDevices)
            {
                GetBitDevice(plc, device.Key).Value = device.Value;
            }
            foreach (var device in wordDevices)
            {
                GetWordDevice(plc, device.Key).Value = device.Value;
            }
            foreach (var device in zDevices)
            {
                GetZDevice(plc, device.Key).Value = device.Value;
            }

            // 構文解析＆意味解析してニモニックの実行
            Interpreter.Execute(plc, mnemonic);

            // 実行結果の確認
            foreach (var device in expectedBitDevices)
            {
                Assert.AreEqual(device.Value, GetBitDevice(plc, device.Key).Value);
            }
            foreach (var device in expectedWordDevices)
            {
                Assert.AreEqual(device.Value, GetWordDevice(plc, device.Key).Value);
            }
            foreach (var device in expectedZDevices)
            {
                Assert.AreEqual(device.Value, GetZDevice(plc, device.Key).Value);
            }
        }

        private static object[] TestCaseSource =
        {
            // 基本パターン
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 10 } },
                new Dictionary<string, uint> {}
            },
            // LD OFF の場合はMOVしないことの確認
            new object[] {
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {}
            },
            // LDB
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {},
                "LDB R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {}
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                new Dictionary<string, uint> {},
                "LDB R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 10 } },
                new Dictionary<string, uint> {}
            },
            // サフィックス
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.U DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.S DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.D DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.L DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.F DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                new Dictionary<string, uint> {},
                "LD R0\nMOV.DF DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 30 }, { "DM13", 40 } },
                new Dictionary<string, uint> {},
            },
            // Zデバイスへの書き込み
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 0x0010 }, { "DM1", 0x0020 } },
                new Dictionary<string, uint> { { "Z1", 0x0000_0000 } },
                "LD R0\nMOV.D DM0 Z1",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 0x0010 }, { "DM1", 0x0020 }, },
                new Dictionary<string, uint> { { "Z1", 0x0020_0010 } },
            },
            // Zデバイスからの読み込み
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 0 }, { "DM1", 0 } },
                new Dictionary<string, uint> { { "Z1", 0x0020_0010 } },
                "LD R0\nMOV.D Z1 DM0",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 0x0010 }, { "DM1", 0x0020 }, },
                new Dictionary<string, uint> { { "Z1", 0x0020_0010 } },
            },
            // Zデバイスによるインデックス修飾
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 42 }, { "DM10", 0 } },
                new Dictionary<string, uint> { { "Z1", 10 } },
                "LD R0\nMOV DM0 DM0:Z1",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 42 }, { "DM10", 42 }, },
                new Dictionary<string, uint> { { "Z1", 10 } },
            },
        };
    }
}
