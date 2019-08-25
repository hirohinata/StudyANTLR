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

        [Test, TestCaseSource(nameof(TestCaseSource))]
        public void Test(
            Dictionary<string, bool> bitDevices,
            Dictionary<string, ushort> wordDevices,
            string mnemonic,
            Dictionary<string, bool> expectedBitDevices,
            Dictionary<string, ushort> expectedWordDevices)
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
        }

        private static object[] TestCaseSource =
        {
            // 基本パターン
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                "LD R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 10 } }
            },
            // LD OFF の場合はMOVしないことの確認
            new object[] {
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } },
                "LD R0\nMOV DM0 DM1",
                new Dictionary<string, bool> { { "R0", OFF } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 0 } }
            },
            // サフィックス
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.U DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.S DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.D DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.L DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.F DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 0 }, { "DM13", 0 } },
            },
            new object[] {
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 0 }, { "DM11", 0 }, { "DM12", 0 }, { "DM13", 0 } },
                "LD R0\nMOV.DF DM0 DM10",
                new Dictionary<string, bool> { { "R0", ON } },
                new Dictionary<string, ushort> { { "DM0", 10 }, { "DM1", 20 }, { "DM2", 30 }, { "DM3", 40 }, { "DM10", 10 }, { "DM11", 20 }, { "DM12", 30 }, { "DM13", 40 } },
            },
        };
    }
}
