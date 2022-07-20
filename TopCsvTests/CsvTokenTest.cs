﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopCsvProject;

namespace TopCsvTests
{
    [TestFixture]
    public class CsvTokenTest
    {
        // ,,3 -> e, e, 3
        // ,3  -> e ,3
        // 3,  -> 3, e
        // 1,2 -> 1 + 2


        [Test]
        public void TestSplit()
        {
            String line = "11,2,3";
            var tokens = line.GetTokens(new[] { ',' }, new char[] { });
            int index = 0;
            foreach (var token in tokens)
            {
                Console.WriteLine(token.ToString());
                switch (index) 
                {
                    case 0:
                        Assert.That(token.ToString(), Is.EqualTo("11"));
                        break;
                    case 1:
                        Assert.That(token[0], Is.EqualTo('2'));
                        break;
                    case 2:
                        Assert.That(token[0], Is.EqualTo('3'));
                        break;
                }
                index++;
            }
        }

        [Test]
        public void TestSplit2()
        {
            String line = "11,2,3,";
            var tokenizer = line.GetTokens(new[] { ',' }, new char[] { });
            int index = 0;
            foreach (var token in tokenizer)
            {
                Console.WriteLine(token.ToString());
                switch (index)
                {
                    case 0:
                        Assert.That(token.ToString(), Is.EqualTo("11"));
                        break;
                    case 1:
                        Assert.That(token[0], Is.EqualTo('2'));
                        break;
                    case 2:
                        Assert.That(token[0], Is.EqualTo('3'));
                        break;
                    case 3:
                        Assert.That(token.Length, Is.EqualTo(0)); 
                        break;
                }

                index++;
            }
            Assert.That(index, Is.EqualTo(4));
        }

        [Test]
        public void TestSplitEcaped()
        {
            String line = "11,\"2,2\",33";
            var tokens = line.GetTokens(new[] { ',' }, new char[] { '"' });
            int index = 0;
            foreach (var token in tokens)
            {
                Console.WriteLine(token.ToString());
                switch (index)
                {
                    case 0:
                        Assert.That(token.ToString(), Is.EqualTo("11"));
                        break;
                    case 1:
                        Assert.That(token.ToString(), Is.EqualTo("2,2"));
                        break;
                    case 2:
                        Assert.That(token.ToString(), Is.EqualTo("33"));
                        break;
                }
                index++;
            }
        }


        [Test]
        public void TestSplitEmptyValues()
        {
            String line = ",,3,";
            var tokenizer = line.GetTokens(new[] { ',' }, new char[] { });
            int index = 0;
            foreach (var token in tokenizer)
            {
                Console.WriteLine(token.ToString());
                switch (index)
                {
                    case 0:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                    case 1:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                    case 2:
                        Assert.That(token[0], Is.EqualTo('3'));
                        break;
                    case 3:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                }

                index++;
            }
            Assert.That(index, Is.EqualTo(4));
        }


        // "CASH & CASH FUND & FTX CASH (EUR),,,,EUR -0.56,\"-0,56\"",
        [Test]
        public void TestImportCashLine()
        {
            String line = "CASH & CASH FUND & FTX CASH (EUR),,,,EUR -0.56,\"-0,56\"";
            var tokenizer = line.GetTokens(new[] { ',' }, new char[] {'"' });
            int index = 0;
            foreach (ReadOnlySpan<char> token in tokenizer)
            {
                Console.WriteLine(token.ToString());
                switch (index)
                {
                    case 0:
                        String expected = "CASH & CASH FUND & FTX CASH (EUR)";
                        CompareSpan(expected, token);
                        break;
                    case 1:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                    case 2:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                    case 3:
                        Assert.That(token.Length, Is.EqualTo(0));
                        break;
                    case 4:
                        expected = "EUR -0.56";
                        CompareSpan(expected, token);
                        break;
                    case 5:
                        expected = "-0,56";
                        CompareSpan(expected, token);
                        break;

                }

                index++;
            }
            Assert.That(index, Is.EqualTo(6));
        }

        public void CompareSpan(String expected, ReadOnlySpan<char> actual)
        {
            if (actual.Length != expected.Length || !actual.ToString().Equals(expected))
            {
                String msg = $"Expected {expected}, actual:{actual.ToString()}";
                Assert.Fail(msg);
            }

            Console.WriteLine(actual.ToString());
        }
    }
}
