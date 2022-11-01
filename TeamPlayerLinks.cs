using Pes2021Api.decrypter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public static class TeamPlayerLinks
    {
        public static bool Unzlibed;
        private static byte[] data;
        public static ObservableCollection<TeamPlayerLink> List { get; set; } = new ObservableCollection<TeamPlayerLink>();
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

        public static byte[] ZlibbedData()
        {
            byte[] rv = new byte[List.Count * 16];
            int offset = 0;
            int globalIndex = 1;
            //var newList = new List<TeamPlayerLink>();

            //var grouped = from tpl in List
            //              //orderby tpl.OrderInTeam
            //              group tpl by tpl.TeamID into g
            //              select new { TeamID = g.Key, TPLs = g.ToList() };

            //foreach(var teamGroup in grouped)
            //{
            //    var team = Teams.List.FirstOrDefault(t => t.ID == teamGroup.TeamID);

            //    if (team != null)
            //    {
            //        var count = team.IsNationalTeam ? 23 : 40;

            //        for (int i = 0; i < count; i++)
            //        {
            //            if (i < teamGroup.TPLs.Count)
            //            {
            //                var tpl = teamGroup.TPLs[i];
            //                tpl.Index = globalIndex;
            //                newList.Add(tpl);
            //            }

            //            globalIndex++;
            //        }
            //    }
            //    else
            //    {
            //        throw new ArgumentNullException("Değer hatası");
            //    }
            //}

            foreach (var tpl in List)
            {
                tpl.Index = globalIndex++;
                Buffer.BlockCopy(tpl.Bytes, 0, rv, offset, tpl.Bytes.Length);
                offset += tpl.Bytes.Length;
            }

            return rv;
        }

        public static void ReadAll()
        {
            int i = 0;
            int offset = 0;
            List.Clear();

            while (Data.Length - offset >= 16)
            {
                var playerAssignmentData = new ArraySegment<byte>(Data, offset, 16).ToArray();
                var tpl = new TeamPlayerLink(playerAssignmentData);
                offset += 16;
                List.Add(tpl);
                i++;

                //if (tpl.TeamID == 273)
                //{
                //    Console.WriteLine($"Order: {tpl.OrderInTeam}, Index: {tpl.Index}, PlayerID: {tpl.PlayerID}");
                //}
                //var player = Players.GetPlayer(tpl.PlayerID);
                //var team = Teams.GetTeam(tpl.TeamID);

                //if (player != null && team != null)
                //{
                //    if (team.IsNationalTeam)
                //    {
                //        player.NationalTeam = team;
                //    }
                //    else
                //    {
                //        player.Team = team;
                //    }

                //    team.Players.Add(player);
                //}
                //else
                //{
                //    Console.WriteLine($"{player.ID};{player.PlayerName} takim bulunamadı: {tpl.TeamID}");
                //}
            }

            OnBinaryReadCompleted();
        }

        public static Team GetClubTeamForPlayer(Player player)
        {
            var list = List.Where(tpl => tpl.PlayerID == player.ID).ToList();

            foreach(var link in list)
            {
                var team = Teams.GetTeam(link.TeamID);

                if (team != null)
                {
                    if (!team.IsNationalTeam)
                    {
                        return team;
                    }
                }
            }

            return null;
        }

        public static TeamPlayerLink GetClubTeamLinkForPlayer(Player player)
        {
            var list = List.Where(tpl => tpl.PlayerID == player.ID).ToList();
            foreach (var link in list)
            {
                var team = Teams.GetTeam(link.TeamID);

                if (team != null)
                {
                    if (!team.IsNationalTeam)
                    {
                        return link;
                    }
                }
            }

            return null;
        }

        public static Team GetNationalTeamForPlayer(Player player)
        {
            var list = List.Where(tpl => tpl.PlayerID == player.ID).ToList();
            foreach (var link in list)
            {
                var team = Teams.GetTeam(link.TeamID);

                if (team != null)
                {
                    if (team.IsNationalTeam)
                    {
                        return team;
                    }
                }
            }

            return null;
        }

        public static TeamPlayerLink GetNationalTeamLinkForPlayer(Player player)
        {
            var list = List.Where(tpl => tpl.PlayerID == player.ID).ToList();
            foreach (var link in list)
            {
                var team = Teams.GetTeam(link.TeamID);

                if (team != null)
                {
                    if (team.IsNationalTeam)
                    {
                        return link;
                    }
                }
            }

            return null;
        }
    }
}
