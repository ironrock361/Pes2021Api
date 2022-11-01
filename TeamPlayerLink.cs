using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class TeamPlayerLink
    {
        public byte[] Bytes { get; set; }
        public int Index
        {
            get => BitConverter.ToInt32(Bytes, 0);
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, Bytes, 0, 4);
            }
        }
        public int PlayerID
        {
            get => BitConverter.ToInt32(Bytes, 4);
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, Bytes, 4, 4);
            }
        }
        public int TeamID
        {
            get => BitConverter.ToInt32(Bytes, 8);
            set
            {
                Array.Copy(BitConverter.GetBytes(value), 0, Bytes, 8, 4);
            }
        }
        public int ShirtNo
        {
            get => Bytes[12] + 1;
            set
            {
                Array.Copy(BitConverter.GetBytes((byte)(value - 1)), 0, Bytes, 12, 1);
            }
        }

        public bool IsCaptain
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[5];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[5] = value;
                Bytes[14] = bitArray.ToByte()[0];

                //var list = Extensions.BitList(Bytes[14]);
                //list[5] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public bool IsPenaltyKicker
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[4];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[4] = value;
                Bytes[14] = bitArray.ToByte()[0];
                //var list = Extensions.BitList(Bytes[14]);
                //list[4] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public bool IsLongFK
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[3];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[3] = value;
                Bytes[14] = bitArray.ToByte()[0];
                //var list = Extensions.BitList(Bytes[14]);
                //list[3] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public bool IsLeftCK
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[2];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[2] = value;
                Bytes[14] = bitArray.ToByte()[0];
                //var list = Extensions.BitList(Bytes[14]);
                //list[2] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public bool IsShortFK
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[1];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[5] = value;
                Bytes[14] = bitArray.ToByte()[0];

                //var list = Extensions.BitList(Bytes[14]);
                //list[1] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public bool IsRightCK
        {
            get
            {
                var list = Extensions.BitList(Bytes[14]);
                return list[0];
            }
            set
            {
                var bitArray = new BitArray(new byte[] { Bytes[14] });
                bitArray[0] = value;
                Bytes[14] = bitArray.ToByte()[0];
                //var list = Extensions.BitList(Bytes[14]);
                //list[0] = value;
                //Array.Copy(list.ToByteArray(), 0, Bytes, 14, 1);
            }
        }
        public int OrderInTeam
        {
            get
            {
                return Bytes[13] >> 2;
                //var list = Extensions.BitList(Bytes[13]).Take(6);
                //return BitConverter.ToInt32(list.Select(b => b ? (byte)1 : (byte)0).ToArray(), 0);
            }
            set
            {
                Bytes[13] = (byte)(value << 2);
                //var list = Extensions.BitList(Bytes[13]);
                //var source = Extensions.BitList(BitConverter.GetBytes(value));
                //list.RemoveRange(2, 6);
                //list.AddRange(source);
                //Array.Copy(list.ToArray(), 0, Bytes, 13, 1);
            }
        }

        public TeamPlayerLink(byte[] bytes = null)
        {
            if (bytes == null)
            {
                bytes = new byte[16];
            }

            Bytes = bytes;
        }
    }
}
