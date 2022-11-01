using Pes2021Api.decrypter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public static class Players
    {
        public static bool Unzlibed;
        private static byte[] data;
        public static ObservableCollection<Player> List { get; set; } = new ObservableCollection<Player>();
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

            while (Data.Length - offset >= 312)
            {
                var playerData = new ArraySegment<byte>(Data, offset, 312).ToArray();
                var p = new Player(i++, playerData);
                offset += 312;
                lock (List)
                {
                    List.Add(p);
                }
            }

            OnBinaryReadCompleted();
        }

        public static Player GetPlayer(int playerID)
        {
            lock (List)
            {
                return List.ToList().FirstOrDefault(p => p.ID == playerID);
            }
        }
    }
}
