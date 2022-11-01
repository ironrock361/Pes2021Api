using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public static class Extensions
    {
        private static IEnumerable<bool> GetBitsStartingFromLSB(byte b) // A
        {
            for (int i = 0; i < 8; i++)
            {
                yield return b % 2 != 0; // A
                b = (byte)(b >> 1);
            }
        }
        public static int Bits2Int(List<bool> bitArray, int byteOffset, int bitOffset, int bitLength)
        {
            var slice = bitArray.GetRange(byteOffset * 8 + bitOffset, bitLength); //B
            slice.AddRange(Enumerable.Repeat(false, 32 - bitLength).ToList()); //C
            int[] array = new int[1]; //D
            new BitArray(slice.ToArray()).CopyTo(array, 0); //D
            return array[0]; //D
        }

        public static List<bool> BitList(byte[] bytes)
        {
            return bytes.SelectMany(GetBitsStartingFromLSB).ToList(); 
        }
        public static List<bool> BitList(byte b)
        {
            return GetBitsStartingFromLSB(b).ToList();
        }
        public static byte[] ToByteArray(this List<bool> bits)
        {
            byte[] ret = new byte[(bits.Count - 1) / 8 + 1];
            byte[] bitsArray = bits.Select(b => b ? (byte)1 : (byte)0).ToArray();
            bitsArray.CopyTo(ret, 0);
            return ret;
        }

        public static byte[] ToByte(this BitArray bits)
        {
            // Make sure we have enough space allocated even when number of bits is not a multiple of 8
            var bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
    }
}
