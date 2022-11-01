using Pes2021Api.decrypter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public static class Teams
    {
        public static bool Unzlibed;
        private static byte[] data;
        public static ObservableCollection<Team> List { get; set; } = new ObservableCollection<Team>();
        public static event EventHandler BinaryReadCompleted;

        public static void OnBinaryReadCompleted()
        {
            BinaryReadCompleted?.Invoke(null, EventArgs.Empty);
        }

        public static byte[] Data
        {
            get
            {
                if (!Unzlibed)
                {
                    data = BinFile.UnZlib(data);
                    Unzlibed = true;
                }

                return data;
            }
            set
            {
                data = value;
                Unzlibed = false;
                ReadAll();
            }
        }

        public static void ReadAll()
        {
            int i = 0;
            int offset = 0;

            lock (List)
            {
                List.Clear();
            }

            while (Data.Length - offset >= 1532)
            {
                var teamData = new ArraySegment<byte>(Data, offset, 1532).ToArray();
                var t = new Team(i++, teamData);
                t.StartingOffet = offset;

                lock (List)
                {
                    List.Add(t);
                }

                offset += 1532;
            }

            OnBinaryReadCompleted();
        }

        public static Team GetTeam(int teamID)
        {
            lock (List)
            {
                return List.ToList().FirstOrDefault(t => t.ID == teamID);
            }
        }

        public static Team GetTeam(uint teamID)
        {
            return GetTeam((int)teamID);
        }
    }
}
