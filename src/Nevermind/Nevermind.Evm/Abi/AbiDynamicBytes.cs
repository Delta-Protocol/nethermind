﻿using System;
using System.Numerics;
using System.Text;
using Nevermind.Core.Extensions;

namespace Nevermind.Evm.Abi
{
    public class AbiDynamicBytes : AbiType
    {
        private const int PaddingMultiple = 32;

        public static AbiDynamicBytes Instance = new AbiDynamicBytes();

        private AbiDynamicBytes()
        {
        }

        public override bool IsDynamic => true;

        public override string Name => "bytes";

        public override Type CSharpType { get; } = typeof(byte[]);

        public override (object, int) Decode(byte[] data, int position)
        {
            (BigInteger length, int currentPosition) = UInt.DecodeUInt(data, position);
            int paddingSize = (1 + (int) length / PaddingMultiple) * PaddingMultiple;
            return (data.Slice(currentPosition, (int) length), currentPosition + paddingSize);
        }

        public override byte[] Encode(object arg)
        {
            if (arg is byte[] input)
            {
                int paddingSize = (1 + input.Length / PaddingMultiple) * PaddingMultiple;
                byte[] lengthEncoded = UInt.Encode(new BigInteger(input.Length));
                return Bytes.Concat(lengthEncoded, Bytes.PadRight(input, paddingSize));
            }

            if (arg is string stringInput)
            {
                return Encode(Encoding.ASCII.GetBytes(stringInput));
            }

            throw new AbiException(AbiEncodingExceptionMessage);
        }
    }
}