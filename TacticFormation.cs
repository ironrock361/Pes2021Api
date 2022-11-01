using System;
using System.Collections.Specialized;

namespace Pes2021Api
{
    public class TacticFormation
    {
        private byte[] Bytes;
        private readonly int Index;

        public uint TacticsID
        {
            get => BitConverter.ToUInt32(Bytes, 0);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 0, 4);
            }
        }
        public uint PositionRole
        {
            get => BitConverter.ToUInt32(Bytes, 4);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 4, 4);
            }
        }
        public uint PosX
        {
            get => Bytes[8];
            set
            {
                Bytes[8] = (byte)value;
            }
        }
        public uint PosY
        {
            get => Bytes[9];
            set
            {
                Bytes[9] = (byte)value;
            }
        }
        public uint PlayerAssignmentOrderNo
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 10));
                var sect1 = BitVector32.CreateSection(0b1111);
                return (uint)val[sect1];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt16(Bytes, 10));
                var sect1 = BitVector32.CreateSection(0b1111);
                val[sect1] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 10, 2);
            }
        }
        public uint FormationIndex
        {
            get
            {
                var val = new BitVector32(BitConverter.ToUInt16(Bytes, 10));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                return (uint)val[sect2];
            }
            set
            {
                var val = new BitVector32(BitConverter.ToUInt16(Bytes, 10));
                var sect1 = BitVector32.CreateSection(0b1111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                val[sect2] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 10, 2);
            }
        }
        public string Position => Enum.GetName(typeof(Position), PositionRole);
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
        public TacticFormation(int index, byte[] bytes = null)
        {
            Index = index;

            if (bytes != null)
            {
                ReadFromBytes(bytes);
            }
        }
    }
}
