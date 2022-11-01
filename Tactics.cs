using Pes2021Api.decrypter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public static class Tactics
    {
        public static bool Unzlibed;
        private static byte[] data;
        public static ObservableCollection<Tactic> List { get; set; } = new ObservableCollection<Tactic>();
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
            List.Clear();

            while (Data.Length - offset >= 12)
            {
                var tacticData = new ArraySegment<byte>(Data, offset, 12).ToArray();
                var p = new Tactic(i++, tacticData);
                offset += 12;
                List.Add(p);
            }

            OnBinaryReadCompleted();
        }
    }
}
