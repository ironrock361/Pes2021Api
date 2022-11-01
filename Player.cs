using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class Player
    {
        private byte[] Bytes;
        public List<bool> bitArray;
        public int Index;
        public Team Team => TeamPlayerLinks.GetClubTeamForPlayer(this);
        public Team NationalTeam => TeamPlayerLinks.GetNationalTeamForPlayer(this);
        public uint YouthClubID
        {
            get => BitConverter.ToUInt32(Bytes, 0);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 0, 4);
            }
        }
        public uint LoanFromClubID
        {
            get => BitConverter.ToUInt32(Bytes, 4);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 4, 4);
            }
        }
        public uint ID
        {
            get => BitConverter.ToUInt32(Bytes, 8);
            set
            {
                Buffer.BlockCopy(BitConverter.GetBytes(value), 0, Bytes, 8, 4);
            }
        }

        #region int no 4
        public uint ContractExpiryDate
        {
            get => (BitConverter.ToUInt32(Bytes, 12) << 5) >> 5;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 12) >> 27 << 27;
                src |= value;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 12, 4);
            }
        }
        public uint FreekickMotion
        {
            get => BitConverter.ToUInt32(Bytes, 12) >> 27;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 12) & (1 << 27);
                src |= value << 27;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 12, 4);
            }
        }
        #endregion

        #region int no 5
        public uint LoanExpiryDate
        {
            get => (BitConverter.ToUInt32(Bytes, 16) << 5) >> 5;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 16) >> 27 << 27;
                src |= value;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 16, 4);
            }
        }
        public uint PlayingStyle
        {
            get => BitConverter.ToUInt32(Bytes, 16) >> 27;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 16) & (1 << 27);
                src |= value << 27;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 16, 4);
            }
        }
        #endregion

        #region int no 6
        public uint MarketValue
        {
            get => ((BitConverter.ToUInt32(Bytes, 20) << 5) >> 5) * 100;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 20) >> 27 << 27;
                src |= value / 100;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 20, 4);
            }
        }
        public uint NationalTeamCaps
        {
            get => BitConverter.ToUInt32(Bytes, 20) >> 27;
            set
            {
                var src = BitConverter.ToUInt32(Bytes, 20) & (1 << 27);
                src |= value << 27;
                Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 20, 4);
            }
        }
        #endregion

        #region int no 7
        //public uint MarketValue
        //{
        //    get => ((BitConverter.ToUInt32(Bytes, 20) << 5) >> 5) * 100;
        //    set
        //    {
        //        var src = BitConverter.ToUInt32(Bytes, 20) >> 27 << 27;
        //        src |= value / 100;
        //        Buffer.BlockCopy(BitConverter.GetBytes(src), 0, Bytes, 20, 4);
        //    }
        //}
        public uint Height
        {

            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 24));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b11111111, sect3);
                return (uint)val[sect4] + 100;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 24));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b11111111, sect3);
                val[sect4] = (int)value - 100;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 24, 4);
            }
        }
        #endregion
        
        #region int no 8
        public uint SecondNationalityID
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                return (uint)val[sect1];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                val[sect1] = (int)value;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 28, 4);
            }
        }

        public uint NationalityID
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                return (uint)val[sect2];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                val[sect2] = (int)value;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 28, 4);
            }
        }

        public uint GoalCelebrationMotion
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b11111111, sect2);
                return (uint)val[sect3];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b11111111, sect2);
                val[sect3] = (int)value;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 28, 4);
            }
        }

        public uint PlaceKicking
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b11111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;

                //return BitConverter.ToUInt32(Bytes, 28) >> 26 + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 28));
                var sect1 = BitVector32.CreateSection(0b111111111);
                var sect2 = BitVector32.CreateSection(0b111111111, sect1);
                var sect3 = BitVector32.CreateSection(0b11111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                //var src = BitConverter.ToUInt32(Bytes, 28) & (1 << 26);
                //src |= (value - 40) << 26;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 28, 4);
            }
        }
        #endregion

        #region int no 9
        public uint Weight
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                return (uint)val[sect1] + 30;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                val[sect1] = (int)value - 30;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        public uint LowPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                return (uint)val[sect2] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                val[sect2] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        public uint GkCleaning
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                return (uint)val[sect3] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                val[sect3] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        public uint DefensiveProveness
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        public uint BallControl
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                return (uint)val[sect5] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                val[sect5] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        public bool CrossoverTurn
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                return val[sect6] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 32));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                val[sect6] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 32, 4);
            }
        }
        #endregion

        #region int no 10
        public uint Heading
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                return (uint)val[sect1] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                val[sect1] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        public uint Jump
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                return (uint)val[sect2] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                val[sect2] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        public uint GkCoverage
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                return (uint)val[sect3] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                val[sect3] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        public uint Speed
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        public uint BallWinning
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                return (uint)val[sect5] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                val[sect5] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        public uint LB
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                return (uint)val[sect6];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 36));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                val[sect6] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 36, 4);
            }
        }
        #endregion

        #region int no 11
        public uint GkReflexes
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                return (uint)val[sect1] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                val[sect1] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        public uint Goalkeeping
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                return (uint)val[sect2] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                val[sect2] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        public uint Curl
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                return (uint)val[sect3] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                val[sect3] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        public uint Stamina
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        public uint Acceleration
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                return (uint)val[sect5] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                val[sect5] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        public uint GK
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                return (uint)val[sect6];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 40));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                val[sect6] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 40, 4);
            }
        }
        #endregion

        #region int no 12
        public uint Dribbling
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                return (uint)val[sect1] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                val[sect1] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        public uint KickingPower
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                return (uint)val[sect2] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                val[sect2] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        public uint GkCatching
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                return (uint)val[sect3] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                val[sect3] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        public uint OffensiveProveness
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        public uint Balance
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                return (uint)val[sect5] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                val[sect5] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        public uint Attitude
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                return (uint)val[sect6] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 44));
                var sect1 = BitVector32.CreateSection(0b1111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                val[sect6] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 44, 4);
            }
        }
        #endregion

        #region int no 13
        public uint Aggression
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                return (uint)val[sect1] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                val[sect1] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        public uint PhysicalContact
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                return (uint)val[sect2] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                val[sect2] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        public uint Finishing
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                return (uint)val[sect3] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                val[sect3] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        public uint LoftedPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                return (uint)val[sect4] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                val[sect4] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        public uint Age
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                return (uint)val[sect5] + 15;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                val[sect5] = (int)value - 15;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        public uint DMF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                return (uint)val[sect6];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 48));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b111111, sect1);
                var sect3 = BitVector32.CreateSection(0b111111, sect2);
                var sect4 = BitVector32.CreateSection(0b111111, sect3);
                var sect5 = BitVector32.CreateSection(0b111111, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                val[sect6] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 48, 4);
            }
        }
        #endregion

        #region int no 14
        public uint TightPossession
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                return (uint)val[sect1] + 40;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                val[sect1] = (int)value - 40;

                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint CKMotion
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                return (uint)val[sect2];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                val[sect2] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint DribblingAimMovement
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                return (uint)val[sect3];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                val[sect3] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint RunArmMovement
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                return (uint)val[sect4];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                val[sect4] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint RegisteredPosition
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                return (uint)val[sect5];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                val[sect5] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint Form
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                return (uint)val[sect6] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                val[sect6] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint DribblingHunch
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                return (uint)val[sect7];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                val[sect7] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public uint PKMotion
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                var sect8 = BitVector32.CreateSection(0b111, sect7);
                return (uint)val[sect8];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                var sect8 = BitVector32.CreateSection(0b111, sect7);
                val[sect8] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        public bool EarlyCross
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                var sect8 = BitVector32.CreateSection(0b111, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                return val[sect9] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 52));
                var sect1 = BitVector32.CreateSection(0b111111);
                var sect2 = BitVector32.CreateSection(0b1111, sect1);
                var sect3 = BitVector32.CreateSection(0b1111, sect2);
                var sect4 = BitVector32.CreateSection(0b1111, sect3);
                var sect5 = BitVector32.CreateSection(0b1111, sect4);
                var sect6 = BitVector32.CreateSection(0b111, sect5);
                var sect7 = BitVector32.CreateSection(0b111, sect6);
                var sect8 = BitVector32.CreateSection(0b111, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                val[sect9] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 52, 4);
            }
        }
        #endregion

        #region int no 15
        public uint RunHunch
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                return (uint)val[sect1];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                val[sect1] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint Reputation
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                return (uint)val[sect2] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                val[sect2] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint WeakFootUsage
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                return (uint)val[sect3] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                val[sect3] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint CMF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                return (uint)val[sect4];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                val[sect4] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint InjuryResistance
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                return (uint)val[sect5] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                val[sect5] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint RMF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                return (uint)val[sect6];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                val[sect6] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint WeakFootAccuracy
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                return (uint)val[sect7] + 1;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                val[sect7] = (int)value - 1;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint AMF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                return (uint)val[sect8];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                val[sect8] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint LMF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                return (uint)val[sect9];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                val[sect9] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint CB
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                return (uint)val[sect10];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                val[sect10] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint CF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                return (uint)val[sect11];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                val[sect11] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint LWF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                return (uint)val[sect12];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                val[sect12] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint RB
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                return (uint)val[sect13];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                val[sect13] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint RWF
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                var sect14 = BitVector32.CreateSection(0b11, sect13);
                return (uint)val[sect14];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                var sect14 = BitVector32.CreateSection(0b11, sect13);
                val[sect14] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        public uint SS
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                var sect14 = BitVector32.CreateSection(0b11, sect13);
                var sect15 = BitVector32.CreateSection(0b11, sect14);
                return (uint)val[sect15];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 56));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b111, sect1);
                var sect3 = BitVector32.CreateSection(0b11, sect2);
                var sect4 = BitVector32.CreateSection(0b11, sect3);
                var sect5 = BitVector32.CreateSection(0b11, sect4);
                var sect6 = BitVector32.CreateSection(0b11, sect5);
                var sect7 = BitVector32.CreateSection(0b11, sect6);
                var sect8 = BitVector32.CreateSection(0b11, sect7);
                var sect9 = BitVector32.CreateSection(0b11, sect8);
                var sect10 = BitVector32.CreateSection(0b11, sect9);
                var sect11 = BitVector32.CreateSection(0b11, sect10);
                var sect12 = BitVector32.CreateSection(0b11, sect11);
                var sect13 = BitVector32.CreateSection(0b11, sect12);
                var sect14 = BitVector32.CreateSection(0b11, sect13);
                var sect15 = BitVector32.CreateSection(0b11, sect14);
                val[sect15] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 56, 4);
            }
        }
        #endregion

        #region int no 16
        public bool DribbleMotion
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect1] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect1] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Sombrero
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect2] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect2] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool PinpointCrossing
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect3] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect3] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool WeightedPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect4] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect4] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool FlipFlap
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect5] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect5] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool FightingSpirit
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect6] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect6] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool ThroughPassing
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect7] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect7] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool LowLoftedPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect8] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect8] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Trickster
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect9] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect9] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool GkLowPunt
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect10] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect10] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Gamesmanship
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect11] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect11] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Captaincy
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect12] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect12] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool OutsideCurler
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect13] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect13] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool DippingShot
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect14] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect14] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Header
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect15] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect15] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool GkHighPunt
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect16] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect16] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool MarseilleTurn
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect17] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect17] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool WonBallonDor
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect18] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect18] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool RisingShot
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect19] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect19] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool StepOnSkillControl
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect20] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect20] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool PenaltySpecialist
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect21] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect21] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool GkPenaltySaver
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect22] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect22] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool Interception
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect23] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect23] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool ManMarking
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect24] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect24] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool HeelTrick
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect25] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect25] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool ChipShotControl
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect26] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect26] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool OneTouchPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect27] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect27] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool LegendImagine
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                return val[sect28] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                val[sect28] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool StrongerHand
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                return val[sect29] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                val[sect29] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool IncisiveRun
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                return val[sect30] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                val[sect30] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        public bool FirstTimeShot
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b11);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect31] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 60));
                var sect1 = BitVector32.CreateSection(0b111);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect31] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 60, 4);
            }
        }
        #endregion

        #region int no 17
        public bool NoLookPass
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect1] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect1] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool KnuckleShot
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect2] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect2] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public uint StrongerFoot
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return (uint)val[sect3];
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect3] = (int)value;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool Rabona
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect4] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect4] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool SuperSub
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect5] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect5] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool TrackBack
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect6] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect6] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool LongRangeShooting
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect7] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect7] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool ScissorsFeint
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect8] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect8] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool LongRanger
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect9] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect9] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool LongThrow
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect10] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect10] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool GkLongThrow
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect11] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect11] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool DoubleTouch
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect12] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect12] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool AcrobaticFinishing
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect13] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect13] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool ScotchMove
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect14] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect14] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool SpeedingBullet
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect15] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect15] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool LongRangeDrive
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect16] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect16] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool CutBehindAndTurn
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect17] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect17] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool LongBallExpert
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect18] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect18] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool AcrobaticClear
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect19] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect19] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        public bool MazingRun
        {
            get
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                return val[sect20] == 1 ? true : false;
            }
            set
            {
                var val = new BitVector32((int)BitConverter.ToUInt32(Bytes, 64));
                var sect1 = BitVector32.CreateSection(0b1);
                var sect2 = BitVector32.CreateSection(0b1, sect1);
                var sect3 = BitVector32.CreateSection(0b1, sect2);
                var sect4 = BitVector32.CreateSection(0b1, sect3);
                var sect5 = BitVector32.CreateSection(0b1, sect4);
                var sect6 = BitVector32.CreateSection(0b1, sect5);
                var sect7 = BitVector32.CreateSection(0b1, sect6);
                var sect8 = BitVector32.CreateSection(0b1, sect7);
                var sect9 = BitVector32.CreateSection(0b1, sect8);
                var sect10 = BitVector32.CreateSection(0b1, sect9);
                var sect11 = BitVector32.CreateSection(0b1, sect10);
                var sect12 = BitVector32.CreateSection(0b1, sect11);
                var sect13 = BitVector32.CreateSection(0b1, sect12);
                var sect14 = BitVector32.CreateSection(0b1, sect13);
                var sect15 = BitVector32.CreateSection(0b1, sect14);
                var sect16 = BitVector32.CreateSection(0b1, sect15);
                var sect17 = BitVector32.CreateSection(0b1, sect16);
                var sect18 = BitVector32.CreateSection(0b1, sect17);
                var sect19 = BitVector32.CreateSection(0b1, sect18);
                var sect20 = BitVector32.CreateSection(0b1, sect19);
                var sect21 = BitVector32.CreateSection(0b1, sect20);
                var sect22 = BitVector32.CreateSection(0b1, sect21);
                var sect23 = BitVector32.CreateSection(0b1, sect22);
                var sect24 = BitVector32.CreateSection(0b1, sect23);
                var sect25 = BitVector32.CreateSection(0b1, sect24);
                var sect26 = BitVector32.CreateSection(0b1, sect25);
                var sect27 = BitVector32.CreateSection(0b1, sect26);
                var sect28 = BitVector32.CreateSection(0b1, sect27);
                var sect29 = BitVector32.CreateSection(0b1, sect28);
                var sect30 = BitVector32.CreateSection(0b1, sect29);
                var sect31 = BitVector32.CreateSection(0b1, sect30);
                val[sect20] = value ? 1 : 0;
                Buffer.BlockCopy(BitConverter.GetBytes(val.Data), 0, Bytes, 64, 4);
            }
        }
        #endregion

        public string RegisteredPositionName => Enum.GetName(typeof(Position), RegisteredPosition);

        
        public string Name 
        { 
            get => Encoding.UTF8.GetString(Bytes, 251, 61).TrimEnd((char)0);
            set
            {
                for (int i = value.Length; i <= 40; i++)
                {
                    value += '\0';
                }

                Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, Bytes, 251, 61);
            }
        }
        public string PrintNameClub
        {
            get => Encoding.UTF8.GetString(Bytes, 129, 61).TrimEnd((char)0);
            set
            {
                for (int i = value.Length; i <= 40; i++)
                {
                    value += '\0';
                }

                Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, Bytes, 129, 61);
            }
        }
        public string PrintNameNationalTeam
        {
            get => Encoding.UTF8.GetString(Bytes, 190, 61).TrimEnd((char)0);
            set
            {
                for (int i = value.Length; i <= 40; i++)
                {
                    value += '\0';
                }

                Buffer.BlockCopy(Encoding.UTF8.GetBytes(value), 0, Bytes, 190, 61);
            }
        }
        public string TooltipText { get; set; }
        


        public override string ToString()
        {
            return $"{Name} [{ID}]";
        }

        public Player(int index, byte[] bytes = null)
        {
            Index = index;

            if (bytes != null)
            {
                ReadFromBytes(bytes);
            }
        }

        public Player()
        {
        }
        public int ShirtNoClub { get; set; }
        public int ShirtNoNational { get; set; }
        public int PosClub { get; set; } = -1;
        public int PosNational { get; set; } = -1;
        public object NationalityName { get; set; }
        public string PosClubStr => Enum.GetName(typeof(Position), PosClub);
        public string PosNationalStr => Enum.GetName(typeof(Position), PosNational);

        public void ReadFromBytes(byte[] bytes)
        {
            Bytes = bytes;

#if DEBUG
            //Console.WriteLine(Name);

            //Console.WriteLine($"ContractExpiryDate: {ContractExpiryDate}");
            //Console.WriteLine($"FreeKickMotion: {FreekickMotion}");
            //Console.WriteLine($"LoanExpiryDate: {LoanExpiryDate}");
            //Console.WriteLine($"PlayingStyle: {PlayingStyle}");
            //Console.WriteLine($"MarketValue: {MarketValue}");
            //Console.WriteLine($"NationalTeamCaps: {NationalTeamCaps}");
            //Console.WriteLine($"GoalCelebrationMotion: {GoalCelebrationMotion}");
            //Console.WriteLine($"RunHunch: {RunHunch}");
            //Console.WriteLine($"Reputation: {Reputation}");
            //Console.WriteLine($"WeakFootUsage: {WeakFootUsage}");
            //Console.WriteLine($"WeakFootAccuracy: {WeakFootAccuracy}");
            //Console.WriteLine($"InjuryResistance: {InjuryResistance}");
            //Console.WriteLine($"GK: {GK}");
            //Console.WriteLine($"CB: {CB}");
            //Console.WriteLine($"RB: {RB}");
            //Console.WriteLine($"LB: {LB}");
            //Console.WriteLine($"DMF: {DMF}");
            //Console.WriteLine($"CMF: {CMF}");
            //Console.WriteLine($"AMF: {AMF}");
            //Console.WriteLine($"LMF: {LMF}");
            //Console.WriteLine($"RMF: {RMF}");
            //Console.WriteLine($"LWF: {LWF}");
            //Console.WriteLine($"RWF: {RWF}");
            //Console.WriteLine($"SS: {SS}");
            //Console.WriteLine($"CF: {CF}");

            //Console.WriteLine($"Height: {Height}");
            //Console.WriteLine($"Weight: {Weight}");
            //Console.WriteLine($"Age: {Age}");
            //Console.WriteLine($"NationalityID: {NationalityID}");
            //Console.WriteLine($"SecondNationalityID: {SecondNationalityID}");
            //Console.WriteLine($"Attitude: {Attitude}");
            //Console.WriteLine($"Form: {Form}");

            //Console.WriteLine($"\nOffensiveProveness: {OffensiveProveness}");
            //Console.WriteLine($"BallControl: {BallControl}");
            //Console.WriteLine($"Dribbling: {Dribbling}");
            //Console.WriteLine($"TightPossession: {TightPossession}");
            //Console.WriteLine($"LowPass: {LowPass}");
            //Console.WriteLine($"LoftedPass: {LoftedPass}");
            //Console.WriteLine($"Finishing: {Finishing}");
            //Console.WriteLine($"Heading: {Heading}");
            //Console.WriteLine($"PlaceKicking: {PlaceKicking}");
            //Console.WriteLine($"Curl: {Curl}");
            //Console.WriteLine($"Speed: {Speed}");
            //Console.WriteLine($"Acceleration: {Acceleration}");
            //Console.WriteLine($"KickingPower: {KickingPower}");
            //Console.WriteLine($"Jump: {Jump}");
            //Console.WriteLine($"PhysicalContact: {PhysicalContact}");
            //Console.WriteLine($"Balance: {Balance}");
            //Console.WriteLine($"Stamina: {Stamina}");
            //Console.WriteLine($"DefensiveProveness: {DefensiveProveness}");
            //Console.WriteLine($"BallWinning: {BallWinning}");
            //Console.WriteLine($"Aggression: {Aggression}");
            //Console.WriteLine($"Goalkeeping: {Goalkeeping}");
            //Console.WriteLine($"GkCatching: {GkCatching}");
            //Console.WriteLine($"GkCleaning: {GkCleaning}");
            //Console.WriteLine($"GkReflexes: {GkReflexes}");
            //Console.WriteLine($"GkCoverage: {GkCoverage}");

            //Console.WriteLine($"GK: {Rating(Position.GK)}");
            //Console.WriteLine($"CB: {Rating(Position.CB)}");
            //Console.WriteLine($"RB: {Rating(Position.RB)}");
            //Console.WriteLine($"LB: {Rating(Position.LB)}");
            //Console.WriteLine();

#endif

        }

        public double Rating(Position position)
        {
            switch (position)
            {
                case Position.GK:
                    //return (Goalkeeping - 25) * 0.52 +
                    //    ((GkReflexes + GkCatching + GkReflexes + GkCoverage) / 4 - 25) * 0.52 +
                    //    ((PhysicalContact + Balance) / 2 - 25) * 0.12 +
                    //    (Jump - 25) * 0.12 + 7; 
                    
                    return checked((int)Math.Round(Math.Round(unchecked((double)(GkCoverage - 24) * 0.56 + (double)(GkCatching - 24) * 0.59 + (double)(Goalkeeping - 24) * 0.11 + (double)(Balance - 25) * 0.10 + (double)(Jump - 25) * 0.13 + 7.0), MidpointRounding.AwayFromZero)));


                case Position.CB:
                    //return (Heading - 25) * 0.11 +
                    //    (DefensiveProveness - 25) * 0.25 +
                    //    (BallWinning - 25) * 0.25 +
                    //    (Speed - 25) * 0.13 +
                    //    ((PhysicalContact + Balance)/2 - 25) * 0.21 +
                    //    (Jump - 25) * 0.27 +
                    //    (Stamina - 25) * 0.27 + 
                    //    7;
                    return checked((int)Math.Round(Math.Round(unchecked((double)(Heading - 25) * 0.2 + (double)(DefensiveProveness - 25) * 0.27 + (double)(BallWinning - 25) * 0.27 + (double)(Speed - 25) * 0.11 + (double)(Balance - 25) * 0.21 + (double)(Jump - 25) * 0.21 + (double)(Stamina - 25) * 0.1 + 7.0), MidpointRounding.AwayFromZero)));

                case Position.LB:
                case Position.RB:
                    //return (OffensiveProveness - 25) * 0.06 +
                    //    (BallControl - 25) * 0.1 +
                    //    (Dribbling - 25) * 0.15 +
                    //    (LoftedPass - 25) * 0.15 +
                    //    (DefensiveProveness - 25) * 0.15 +
                    //    (BallWinning - 25) * 0.14 +
                    //    (Speed - 25) * 0.15 +
                    //    (Acceleration - 25) * 0.15 +
                    //    ((PhysicalContact + Balance) / 2 - 25) * 0.12 +
                    //    (Jump - 25) * 0.12 +
                    //    (Stamina - 25) * 0.13 +
                    //    11;
                    return checked((int)Math.Round(Math.Round(unchecked((double)(OffensiveProveness - 25) * 0.06 + (double)(BallControl - 25) * 0.1 + (double)(Dribbling - 25) * 0.15 + (double)(LoftedPass - 25) * 0.15 + (double)(DefensiveProveness - 25) * 0.15 + (double)(BallWinning - 25) * 0.14 + (double)(Speed - 25) * 0.15 + (double)(Aggression - 25) * 0.15 + (double)(Balance - 25) * 0.12 + (double)(Jump - 25) * 0.12 + (double)(Stamina - 25) * 0.13 + 8.0), MidpointRounding.AwayFromZero)));

                default:
                    return 0;
            }
        }
    }}
