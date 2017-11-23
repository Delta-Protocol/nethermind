﻿using System;
using System.IO;
using Nevermind.Core.Crypto;
using Nevermind.Core.Encoding;
using NUnit.Framework;

namespace Nevermind.Core.Test
{
    [TestFixture]
    public class PrivateKeyTests
    {
        private const string TestPrivateKeyHex = "0x3a1076bf45ab87712ad64ccb3b10217737f7faacbf2872e88fdd9a537d8fe266";

        [OneTimeSetUp]
        public void SetUp()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        [Test]
        public void Can_generate_new_through_constructor()
        {
            PrivateKey privateKey = new PrivateKey();
            PrivateKey zeroKey = new PrivateKey(new byte[32]);
            Assert.AreNotEqual(privateKey.ToString(), zeroKey.ToString());
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(16)]
        [TestCase(31)]
        [TestCase(33)]
        public void Cannot_be_initialized_with_array_of_length_different_than_32(int length)
        {
            byte[] bytes = new byte[length];
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new PrivateKey(bytes));
        }

        [Test]
        public void Cannot_be_initialized_with_null_bytes()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new PrivateKey((byte[])null));
        }

        [Test]
        public void Cannot_be_initialized_with_null_string()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new PrivateKey((string)null));
        }

        [Test]
        public void HEx_is_stored_correctly()
        {
            byte[] bytes = new byte[32];
            new System.Random(12).NextBytes(bytes);
            PrivateKey privateKey = new PrivateKey(bytes);
            Assert.AreEqual(new Hex(bytes), privateKey.Hex);
        }

        [TestCase(TestPrivateKeyHex)]
        public void String_representation_is_correct(string hexString)
        {
            PrivateKey privateKey = new PrivateKey(hexString);
            string privateKeyString = privateKey.ToString();
            Assert.AreEqual(hexString, privateKeyString);
        }

        [TestCase("3a1076bf45ab87712ad64ccb3b10217737f7faacbf2872e88fdd9a537d8fe266", "0xc2d7cf95645d33006175b78989035c7c9061d3f9")]
        [TestCase("56e044e40c2d225593bc0a4ae3fd4a31ab11f9351f98e60109c1fb429b52e876", "0xd1dc4a77be62d06f0760187be2e505d270c170fd")]
        public void Address_as_expected(string privateKeyHex, string addressHex)
        {
            PrivateKey privateKey = new PrivateKey(privateKeyHex);
            Address address = privateKey.Address;
            Assert.AreEqual(addressHex, address.ToString());
        }

        [Test]
        public void Address_returns_the_same_value_when_called_twice()
        {
            PrivateKey privateKey = new PrivateKey(TestPrivateKeyHex);
            Address address1 = privateKey.Address;
            Address address2 = privateKey.Address;
            Assert.AreSame(address1, address2);
        }
    }
}
