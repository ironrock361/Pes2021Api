using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Pes2021Api.decrypter
{
    public static class BinFile
    {
        private static int GetBaseValue()
        {
            return (int)(DateTime.Now.Ticks >> 23) % 255;
        }

        private static int[] GetChunkSizes(byte[] input)
        {
            int[] array = new int[3];
            array[0] = 384;
            array[1] = BitConverter.ToInt32(input, 384);
            array[2] = BitConverter.ToInt32(input, array[0] + array[1] + 4);
            return array;
        }

        private static void GenerateHeader(byte[] input, byte[] output, int[] chunkSize)
        {
            using (MD5 md = MD5.Create())
            {
                output[0] = (byte)GetBaseValue();
                byte[] array = md.ComputeHash(input, 0, chunkSize[0]);
                Buffer.BlockCopy(array, 0, output, 1, array.Length);
                array = md.ComputeHash(input, chunkSize[0] + 4, chunkSize[1]);
                Buffer.BlockCopy(array, 0, output, 17, array.Length);
                array = md.ComputeHash(input, chunkSize[0] + chunkSize[1] + 8, chunkSize[2]);
                Buffer.BlockCopy(array, 0, output, 33, array.Length);
            }
        }

        public static byte[] DecryptFile(byte[] input)
        {
            byte[] array;
            try
            {
                array = new byte[input.Length - 49];
                Buffer.BlockCopy(input, 49, array, 0, array.Length);
                int[] chunkSizes = GetChunkSizes(array);
                int num = 0;
                for (int i = 0; i < 3; i++)
                {
                    int num2 = input[0];
                    for (int j = 0; j < chunkSizes[i]; j++)
                    {
                        int num3 = num2 * 21 + 7;
                        num3 = num2 = num3 % 32768;
                        byte[] array2 = array;
                        int num4 = num;
                        array2[num4] ^= (byte)(num3 % 255);
                        num++;
                    }
                    num += 4;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return null;
            }
            return array;
        }

        public static byte[] EncryptFile(byte[] input)
        {
            byte[] array = null;
            try
            {
                array = new byte[input.Length + 49];
                int[] chunkSizes = GetChunkSizes(input);
                GenerateHeader(input, array, chunkSizes);
                Buffer.BlockCopy(input, 0, array, 49, input.Length);
                int num = 49;
                for (int i = 0; i < 3; i++)
                {
                    int num2 = array[0];
                    for (int j = 0; j < chunkSizes[i]; j++)
                    {
                        int num3 = num2 * 21 + 7;
                        num3 = num2 = num3 % 32768;
                        byte[] array2 = array;
                        int num4 = num;
                        array2[num4] ^= (byte)(num3 % 255);
                        num++;
                    }
                    num += 4;
                }
            }
            catch (Exception)
            {
                return null;
            }
            return array;
        }

        public static bool IsZlibFile(byte[] bytes)
        {
            return bytes.Skip(3).Take(5).ToArray().SequenceEqual(new byte[] { 87, 69, 83, 89, 83 });
        }

        public static byte[] UnZlib(byte[] bytes)
        {
            if (IsZlibFile(bytes))
            {
                try
                {
                    Inflater inflater = new Inflater(false);
                    int int32_1 = Convert.ToInt32(BitConverter.ToUInt32(bytes, 8));
                    int int32_2 = Convert.ToInt32(BitConverter.ToUInt32(bytes, 12));
                    inflater.SetInput(bytes, 16, int32_1);
                    byte[] buffer = new byte[int32_2];
                    inflater.Inflate(buffer);
                    return buffer;
                }
                catch
                {
                    throw new Exception("ZLIB Error !!");
                }
            }
            else
            {
                return bytes;
            }
        }

        public static byte[] Zlib(byte[] bytes)
        {
            uint length1 = (uint)bytes.Length;
            Deflater deflater = new Deflater(9);
            deflater.SetInput(bytes);
            deflater.Finish();
            using (MemoryStream memoryStream1 = new MemoryStream())
            {
                byte[] numArray = new byte[2097152];
                while (!deflater.IsNeedingInput)
                {
                    int count = deflater.Deflate(numArray);
                    memoryStream1.Write(numArray, 0, count);
                    if (deflater.IsFinished)
                        break;
                }
                deflater.Reset();
                uint length2 = (uint)memoryStream1.Length;
                byte[] buffer = new byte[8] { 0, 16, 1, 87, 69, 83, 89, 83 };
                byte[] bytes1 = BitConverter.GetBytes(length2);
                byte[] bytes2 = BitConverter.GetBytes(length1);
                byte[] array = memoryStream1.ToArray();
                MemoryStream memoryStream2 = new MemoryStream();
                memoryStream2.Write(buffer, 0, buffer.Length);
                memoryStream2.Write(bytes1, 0, bytes1.Length);
                memoryStream2.Write(bytes2, 0, bytes2.Length);
                memoryStream2.Write(array, 0, array.Length);
                return memoryStream2.ToArray();
            }
        }
    }
}
