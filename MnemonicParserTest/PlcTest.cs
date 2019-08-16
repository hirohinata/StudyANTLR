using System;
using MnemonicParser;
using NUnit.Framework;

namespace MnemonicParserTest
{
    [TestFixture]
    public class PlcTest
    {
        private const bool ON = true;
        private const bool OFF = false;

        [Test]
        public void Test()
        {
            var plc = new Plc();

            // 実行前のセットアップ
            plc.BitDevices["R0"] = ON;
            plc.WordDevices["DM0"] = 10;
            plc.WordDevices["DM1"] = 0;

            // 構文解析＆意味解析してニモニックの実行
            Interpreter.Execute(plc, "LD R0\nMOV DM0 DM1");

            // 実行結果の確認
            Assert.AreEqual(10, plc.WordDevices["DM1"]);
        }
    }
}
