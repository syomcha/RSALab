using NUnit.Framework;
using RSALab;
using System.Collections.Generic;
using System.Numerics;

namespace RSATests
{
    [TestFixture]
    class Tests
    {
        [TestCase("5", "10", "15")]
        [TestCase("-5", "78", "73")]
        [TestCase("-25", "-75", "-100")]
        [TestCase("124", "-78", "46")]
        [TestCase("428", "188", "616")]
        [TestCase("6388621096273670239963", "863763027620937623093", "7252384123894607863056")]
        [TestCase("6388621096273670239963", "-863763027620937623093", "5524858068652732616870")]
        [TestCase("-6388621096273670239963", "863763027620937623093", "-5524858068652732616870")]
        [TestCase("-6388621096273670239963", "-863763027620937623093", "-7252384123894607863056")]
        public void BigIntSumTest(string n1, string n2, string expected)
        {
            var number1 = BigInteger.Parse(n1);
            var number2 = BigInteger.Parse(n2);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = number1 + number2;

            Assert.AreEqual(expectedBigInt, actual);
        }

        [TestCase("127", "40", "87")]
        [TestCase("-5", "98", "-103")]
        [TestCase("28", "-76", "104")]
        [TestCase("-965", "-1200", "235")]
        [TestCase("7637839836813656103351", "63731037513865334555", "7574108799299790768796")]
        [TestCase("7637839836813656103351", "-63731037513865334555", "7701570874327521437906")]
        [TestCase("-7637839836813656103351", "63731037513865334555", "-7701570874327521437906")]
        [TestCase("-7637839836813656103351", "-63731037513865334555", "-7574108799299790768796")]
        public void BigIntSubTest(string n1, string n2, string expected)
        {
            var number1 = BigInteger.Parse(n1);
            var number2 = BigInteger.Parse(n2);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = number1 - number2;

            Assert.AreEqual(expectedBigInt, actual);
        }

        [TestCase("3", "18", "54")]
        [TestCase("-8", "5", "-40")]
        [TestCase("-4", "246", "-984")]
        [TestCase("-76", "-78", "5928")]
        [TestCase("631968613290", "5325536356", "3365571825926799771240")]
        [TestCase("631968613290", "-5325536356", "-3365571825926799771240")]
        [TestCase("-631968613290", "5325536356", "-3365571825926799771240")]
        [TestCase("-631968613290", "-5325536356", "3365571825926799771240")]
        [TestCase("9633628563269854238965", "96683286823682386832", "931410873535438581411209905053424097308880")]
        public void BigIntMultTest(string n1, string n2, string expected)
        {
            var number1 = BigInteger.Parse(n1);
            var number2 = BigInteger.Parse(n2);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = number1 * number2;

            Assert.AreEqual(expectedBigInt, actual);
        }

        [TestCase("256", "2", "128")]
        [TestCase("-624", "78", "-8")]
        [TestCase("672", "-12", "-56")]
        [TestCase("-952", "-68", "14")]
        [TestCase("3365571825926799771240", "5325536356", "631968613290")]
        [TestCase("3365571825926799771240", "631968613290", "5325536356")]
        [TestCase("3365571825926799771240", "-5325536356", "-631968613290")]
        [TestCase("-3365571825926799771240", "631968613290", "-5325536356")]
        [TestCase("931410873535438581411209905053424097308880", "96683286823682386832", "9633628563269854238965")]
        [TestCase("931410873535438581411209905053424097308880", "9633628563269854238965", "96683286823682386832")]
        public void BigIntDivTest(string n1, string n2, string expected)
        {
            var number1 = BigInteger.Parse(n1);
            var number2 = BigInteger.Parse(n2);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = number1 / number2;

            Assert.AreEqual(expectedBigInt, actual);
        }

        [TestCase("67", "5", "2")]
        [TestCase("-127", "24", "-7")]
        [TestCase("-100", "-10", "0")]
        [TestCase("56", "66", "56")]
        [TestCase("67", "67", "0")]
        [TestCase("99999999999999999999", "1000000000000000", "999999999999999")]
        [TestCase("931410873535438581411209905053424097308880", "100000000000000000", "5053424097308880")]
        [TestCase("931410873535438581411209905053424097308880", "999999999999999999", "778588862679651500")]
        public void BigIntModTest(string n1, string n2, string expected)
        {
            var number1 = BigInteger.Parse(n1);
            var number2 = BigInteger.Parse(n2);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = number1 % number2;

            Assert.AreEqual(expectedBigInt, actual);
        }

        [TestCase("5", "43", "26")]
        [TestCase("679", "423", "385")]
        public void BigIntModIverseTest(string valueStr, string moduleStr, string expected)
        {
            var value = BigInteger.Parse(valueStr);
            var module = BigInteger.Parse(moduleStr);

            var expectedBigInt = BigInteger.Parse(expected);
            var actual = BigIntegerUtils.ModInverse(value, module);

            Assert.AreEqual(expectedBigInt, actual);
        }

        private static readonly object[] encodeTests =
        {
            new object[] {new List<string> { "Привет" }, "271", "277", "74509", new List<string> { "42247", "40943", "22471", "13306", "7365", "21430"} },
            new object[] {new List<string> { "Hello World" }, "163", "167", "26881", new List<string> { "205", "2925", "13051", "13051", "7221", "22034", "12768", "7221", "16053", "13051", "4512"} },
            new object[] {new List<string> { "0", "1", "2", "a", "b", "c", "а", "б", "в", "!", ".", "," }, "127", "179", "21211", new List<string> { "21519", "8932", "22148", "4410", "3386", "8688", "21867", "15751", "20406", "12479", "14574", "20140" } }
        };

        [TestCaseSource("encodeTests")]
        public void EncodeTest(List<string> inputMessage, string pStr, string qStr, string eStr, List<string> expectedMessage)
        {
            var p = BigInteger.Parse(pStr);
            var q = BigInteger.Parse(qStr);
            var e = BigInteger.Parse(eStr);

            var actual = RSA.Encode(p, q, e, inputMessage);

            Assert.IsTrue(IsEqual(actual, expectedMessage));
        }


        private static readonly object[] decodeTests =
        {
            new object[] {new List<string> { "42247", "40943", "22471", "13306", "7365", "21430" }, "13549", "75067", new List<string> { "Привет" } },
            new object[] {new List<string> { "205", "2925", "13051", "13051", "7221", "22034", "12768", "7221", "16053", "13051", "4512" }, "17113", "27221", new List<string> { "Hello World"} },
            new object[] {new List<string> { "21519", "8932", "22148", "4410", "3386", "8688", "21867", "15751", "20406", "12479", "14574", "20140" }, "7519", "22733", new List<string> { "012abcабв!.," } }
        };

        [TestCaseSource("decodeTests")]
        public void DecodeTest(List<string> inputMessage, string dStr, string nStr, List<string> expectedMessage)
        {
            var d = BigInteger.Parse(dStr);
            var n = BigInteger.Parse(nStr);

            var actual = RSA.Decode(d, n, inputMessage);

            Assert.IsTrue(IsEqual(actual, expectedMessage));
        }

        private static bool IsEqual(List<string> a, List<string> b)
        {
            if (a.Count != b.Count)
                return false;

            for (var i = 0; i < a.Count; i++)
            {
                if (string.Compare(a[i], b[i]) != 0)
                    return false;
            }

            return true;
        }
    }
}