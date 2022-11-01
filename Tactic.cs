using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class Tactic
    {
        private byte[] Bytes;
        private readonly int Index;
        public List<TacticFormation> Formations = new List<TacticFormation>();

        public uint TeamID
        {
            get => BitConverter.ToUInt32(Bytes, 4);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 4, 4);
            }
        }
        public uint TacticsID
        {
            get => BitConverter.ToUInt32(Bytes, 0);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 0, 4);
            }
        }
        public uint Compactness
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                return (uint)val[sect1];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                val[sect1] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 6, 2);
            }
        }
        public uint SupportRange
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                return (uint)val[sect2];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                val[sect2] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 6, 2);
            }
        }
        public uint DefensiveLine
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                return (uint)val[sect3];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 6));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                val[sect3] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 6, 2);
            }
        }
        public uint Unk2
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                return (uint)val[sect1];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                val[sect1] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint Unk3
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                return (uint)val[sect2];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                val[sect2] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint Unk4
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                return (uint)val[sect3];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                val[sect3] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint Positioning
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                return (uint)val[sect4];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                val[sect4] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint StrategyType
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                return (uint)val[sect5];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                val[sect5] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint AttackingStyle
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                return (uint)val[sect6];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                val[sect6] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint Pressure
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                return (uint)val[sect7];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                val[sect7] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint ContainmentArea
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                return (uint)val[sect8];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                val[sect8] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint AttackingArea
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                return (uint)val[sect9];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                val[sect9] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint DefensiveStyle
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                return (uint)val[sect10];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                val[sect10] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public uint BuildUp
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                return (uint)val[sect11];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 8));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b11, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                val[sect11] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 8, 2);
            }
        }
        public void ReadFromBytes(byte[] bytes)
        {
            Bytes = bytes;

#if DEBUG
            
            //Console.WriteLine($"TeamID: {TeamID}");
            //Console.WriteLine($"TacticsID: {TacticsID}");
            //Console.WriteLine($"Compactness: {Compactness}");
            //Console.WriteLine($"SupportRange: {SupportRange}");
            //Console.WriteLine($"DefensiveLine: {DefensiveLine}");
            //Console.WriteLine($"Positioning: {Positioning}");
            //Console.WriteLine($"StrategyType: {StrategyType}");
            //Console.WriteLine($"AttackingStyle: {AttackingStyle}");
            //Console.WriteLine($"Pressure: {Pressure}");
            //Console.WriteLine($"ContainmentArea: {ContainmentArea}");
            //Console.WriteLine($"AttackingArea: {AttackingArea}");
            //Console.WriteLine($"DefensiveStyle: {DefensiveStyle}");
            //Console.WriteLine($"BuildUp: {BuildUp}");

            //Console.WriteLine();

#endif

        }
        public Tactic(int index, byte[] bytes = null)
        {
            Index = index;

            if (bytes != null)
            {
                ReadFromBytes(bytes);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Tactic tactic && tactic.TacticsID == TacticsID;
        }
    }
}
