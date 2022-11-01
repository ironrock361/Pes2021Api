using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class Competition
    {
        public uint ID;
        public ushort Index;
        public string Name;
        public int CountryID;
        public List<Player> Teams = new List<Player>();
        public Competition(byte[] bytes = null)
        {
            if (bytes != null)
            {
                ReadFromBytes(bytes);
            }
        }
        public override string ToString()
        {
            return $"{Name} [{ID}]";
        }

        public void ReadFromBytes(byte[] bytes)
        {
            ID = BitConverter.ToUInt32(bytes, 0x00);
            Index = BitConverter.ToUInt16(bytes, 22);
            Name = Encoding.UTF8.GetString(bytes, 0x22, 50).TrimEnd((char)0);
        }

        public void ReadTeamsFromBytes(byte[] bytes, ref Dictionary<int, Player> teams)
        {
            for (int i = 0; i < 20; i++)
            {
                var tid = (int) BitConverter.ToUInt32(bytes, i * 4);
                if (teams.ContainsKey(tid))
                {
                    var t = teams[tid];
                    Teams.Add(t);
                }
            }
        }
    }
}
