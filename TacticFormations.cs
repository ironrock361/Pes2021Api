using Pes2021Api.decrypter;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pes2021Api
{
    public static class TacticFormations
    {
        public static bool Unzlibed;
        private static byte[] data;
        public static ObservableCollection<TacticFormation> List { get; set; } = new ObservableCollection<TacticFormation>();
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
                var tacticFormationData = new ArraySegment<byte>(Data, offset, 12).ToArray();
                var p = new TacticFormation(i++, tacticFormationData);
                offset += 12;
                List.Add(p);
            }

            OnBinaryReadCompleted();
        }
    }
}
