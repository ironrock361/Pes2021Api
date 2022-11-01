using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class Team: INotifyPropertyChanged
    {
        public List<Tactic> Tactics = new List<Tactic>();

        public string JapanName { get; set; }
        public string SpanishName { get; set; }
        public string LatinSpanishName { get; set; }
        public string EnglishName { get; set; }

        public int ID { get; set; }

        public List<Player> Players => PlayerAssignments
            .Select(tpl => Pes2021Api.Players.List.FirstOrDefault(p => p.ID == tpl.PlayerID)).ToList();

        public List<TeamPlayerLink> PlayerAssignments => TeamPlayerLinks.List.Where(tpl =>
        {
            return tpl.TeamID == ID;

        }).OrderBy(tpl => tpl.OrderInTeam).ThenBy(tpl => tpl.PlayerID).ToList();

        public uint ParentTeamID { get; set; }
        public uint StadiumID { get; set; }
        public uint Modifier { get; set; }

        public bool HasLicensedPlayers
        {
            get => (ushort)(Modifier << 5) >> 31==1;
        }
        public bool IsLicensedTeam
        {
            get => (ushort)(Modifier << 6) >> 31==1;
        }
        public bool IsInNonPlayableLeague
        {
            get => (ushort)(Modifier << 19) >> 28==1;
        }
        public bool IsNationalTeam { get; set; }
        //{
        //    get => (Modifier & (1 << 7)) >> 7 == 1;
        //}

        public bool IsNationalTeamSofa { get; set; }

        public uint ManagerID;

        public uint FeederTeamID { get; set; }

        public int TeamEmblem;
        public int HomeStadium;
        public int StadiumImage;
        public uint TeamNationality;
        public int TeamCallname;
        public int GoalNetColour1Red;
        public int GoalNetColour1Green;
        public int GoalNettingDesign;
        public int GoalNetColour2Red;
        public int GoalNetColour2Green;
        public int GoalNetColour2Blue;
        public int TeamColor1Red;
        public int TeamColor1Green;
        public int EditedTeamNameAbbreviation;
        public int EditedTeamEmblem;
        public int TeamColor2Green;
        public int TeamColor2Blue;
        public int MediaBackdropColourRed;
        public int MediaBackdropColourGreen;
        public int MediaBackdropColourBlue;
        public int EditedStadiumSelection;
        public int EditedStadiumName;
        public int StadiumGoalStyle;
        public int StadiumTurfPattern;
        public int StadiumSidelineColour;
        public int StadiumSeatColour;
        public int TeamColor1Blue;
        public int TeamColor2Red;
        public int GoalNetDesign;
        public int GoalNetColour1Blue;
        public int EditedTeamSettings;
        public int UnknownC;
        public int OfficialPartnersImage;
        public int TeamStrip;
        public int RivalTeam1;
        public int RivalTeam2;
        public int RivalTeam3;
        public int Banner1EditedFlag;
        public int Banner2EditedFlag;
        public int Banner3EditedFlag;
        public int Banner4EditedFlag;
        public string Scoreboardname;
        public string CustomStadiumName;
        public int Banner1Text;
        public int Banner2Text;
        public int Banner3Text;
        public int Banner4Text;
        public int UnknownD;
        public int Index;
        public long GlobalOffset;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string TeamName { get; set; }
        public string UsEnglishName { get; set; }
        public string ItalianName { get; set; }
        public string PortugueseName { get; set; }
        public string BrazilianName { get; set; }
        public string FrenchName { get; set; }
        public string GermanName { get; set; }
        public string DutchName { get; set; }
        public string SwedishName { get; set; }
        public string RussianName { get; set; }
        public string GreekName { get; set; }
        public string TurkishName { get; set; }
        public string ChineseName { get; set; }
        public string Empty1Name { get; set; }
        public string Empty2Name { get; set; }
        public string Empty3Name { get; set; }
        public string Empty4Name { get; set; }
        public string ShortLicensedName { get; set; }
        public string ShortFakeName { get; set; }
        public int StartingOffet { get; set; }


        public Team(int index, byte[] bytes = null, long globalOffset=-1)
        {
            Index = index;
            GlobalOffset = globalOffset;
            if (bytes != null)
            {
                ReadFromBytes(bytes);
            }
        }
        public override string ToString()
        {
            return $"{TeamName} [{ID}]";
        }
        

        public void ReadFromBytes(byte[] bytes)
        {

            ManagerID = BitConverter.ToUInt32(bytes, 0);
            FeederTeamID = BitConverter.ToUInt32(bytes, 4);
            ID = (int) BitConverter.ToUInt32(bytes, 8);
            ParentTeamID = BitConverter.ToUInt32(bytes, 12);
            StadiumID = BitConverter.ToUInt16(bytes, 16);
            Modifier = BitConverter.ToUInt32(bytes, 83);

            Modifier = BitConverter.ToUInt16(bytes, 70);
            //TeamNationality = (Modifier << 7) >> 23;
            var bits = new BitArray((int)Modifier);
            var bitstring = string.Join("", bits);
            //TeamNationality = (uint)Extensions.Bits2Int(bitArray, 70, 2, 9);
            //TeamNationality = (uint)Bits2Int(70, 2, 8);
            //TeamNationality = (uint)Bits2Int(70, 6, 8);
            IsNationalTeam = (bytes[83] & (1 << 7)) >> 7 == 1;

            JapanName = Encoding.UTF8.GetString(bytes, 88, 45).TrimEnd((char)0);
            SpanishName = Encoding.UTF8.GetString(bytes, 158, 45).TrimEnd((char)0);
            SwedishName = Encoding.UTF8.GetString(bytes, 228, 45).TrimEnd((char)0);
            GreekName = Encoding.UTF8.GetString(bytes, 298, 45).TrimEnd((char)0);
            EnglishName = Encoding.UTF8.GetString(bytes, 368, 45).TrimEnd((char)0);
            LatinSpanishName = Encoding.UTF8.GetString(bytes, 438, 45).TrimEnd((char)0);
            FrenchName = Encoding.UTF8.GetString(bytes, 508, 45).TrimEnd((char)0);
            TurkishName = Encoding.UTF8.GetString(bytes, 578, 45).TrimEnd((char)0);
            Empty1Name = Encoding.UTF8.GetString(bytes, 648, 45).TrimEnd((char)0);
            PortugueseName = Encoding.UTF8.GetString(bytes, 718, 45).TrimEnd((char)0);
            TeamName = Encoding.UTF8.GetString(bytes, 788, 24).TrimEnd((char)0);
            GermanName = Encoding.UTF8.GetString(bytes, 812, 45).TrimEnd((char)0);
            ShortLicensedName = Encoding.UTF8.GetString(bytes, 882, 4).TrimEnd((char)0);
            BrazilianName = Encoding.UTF8.GetString(bytes, 892, 45).TrimEnd((char)0);
            ChineseName = Encoding.UTF8.GetString(bytes, 962, 45).TrimEnd((char)0);
            Empty2Name = Encoding.UTF8.GetString(bytes, 1032, 45).TrimEnd((char)0);
            ItalianName = Encoding.UTF8.GetString(bytes, 1102, 45).TrimEnd((char)0);
            Empty3Name = Encoding.UTF8.GetString(bytes, 1172, 45).TrimEnd((char)0);
            RussianName = Encoding.UTF8.GetString(bytes, 1242, 45).TrimEnd((char)0);
            DutchName = Encoding.UTF8.GetString(bytes, 1312, 45).TrimEnd((char)0);
            ShortFakeName = Encoding.UTF8.GetString(bytes, 1382, 4).TrimEnd((char)0);
            Empty4Name = Encoding.UTF8.GetString(bytes, 1392, 45).TrimEnd((char)0);
            UsEnglishName = Encoding.UTF8.GetString(bytes, 1462, 45).TrimEnd((char)0);


            //ID = BitConverter.ToUInt16(bytes, 0x00);
            //ManagerID = BitConverter.ToUInt16(bytes, 0x04);
            //TeamEmblem = BitConverter.ToUInt16(bytes, 0x08);
            //HomeStadium = BitConverter.ToUInt16(bytes, 0x0A);
            //StadiumImage = BitConverter.ToUInt16(bytes, 0x0C);
            //TeamNationality = BitConverter.ToUInt16(bytes, 0x0E);
            //TeamCallname = BitConverter.ToUInt16(bytes, 0x10);
            //GoalNetColour1Red = Bits2Int(0x12, 0, 6);
            //GoalNetColour1Green = Bits2Int(0x12, 6, 6);
            //GoalNettingDesign = Bits2Int(0x13, 4, 4);
            //GoalNetColour2Red = Bits2Int(0x14, 0, 6);
            //GoalNetColour2Green = Bits2Int(0x14, 6, 6);
            //GoalNetColour2Blue = Bits2Int(0x15, 4, 6);
            //TeamColor1Red = Bits2Int(0x16, 2, 6);
            //TeamColor1Green = Bits2Int(0x17, 0, 6);
            //EditedTeamNameAbbreviation = Bits2Int(0x17, 6, 1);
            //EditedTeamEmblem = Bits2Int(0x17, 7, 1);
            //TeamColor2Green = Bits2Int(0x18, 0, 6);
            //TeamColor2Blue = Bits2Int(0x18, 6, 6);
            //MediaBackdropColourRed = Bits2Int(0x19, 4, 6);
            //MediaBackdropColourGreen = Bits2Int(0x1A, 2, 6);
            //MediaBackdropColourBlue = Bits2Int(0x1B, 0, 6);
            //EditedStadiumSelection = Bits2Int(0x1B, 6, 1);
            //EditedStadiumName = Bits2Int(0x1B, 7, 1);
            //StadiumGoalStyle = Bits2Int(0x1C, 0, 4);
            //StadiumTurfPattern = Bits2Int(0x1C, 4, 4);
            //StadiumSidelineColour = Bits2Int(0x1D, 0, 4);
            //StadiumSeatColour = Bits2Int(0x1D, 4, 4);
            //TeamColor1Blue = Bits2Int(0x1E, 0, 6);
            //TeamColor2Red = Bits2Int(0x1E, 6, 6);
            //GoalNetDesign = Bits2Int(0x1F, 4, 4);
            //GoalNetColour1Blue = Bits2Int(0x20, 0, 6);
            //EditedTeamSettings = Bits2Int(0x20, 6, 10);
            //UnknownC = BitConverter.ToUInt16(bytes, 0x22);
            //OfficialPartnersImage = BitConverter.ToUInt16(bytes, 0x24);
            //TeamStrip = BitConverter.ToUInt16(bytes, 0x30);
            //RivalTeam1 = BitConverter.ToUInt16(bytes, 0x58);
            //RivalTeam2 = BitConverter.ToUInt16(bytes, 0x5C);
            //RivalTeam3 = BitConverter.ToUInt16(bytes, 0x60);
            //Banner1EditedFlag = Bits2Int(0x64, 0, 1);
            //Banner2EditedFlag = Bits2Int(0x65, 0, 1);
            //Banner3EditedFlag = Bits2Int(0x66, 0, 1);
            //Banner4EditedFlag = Bits2Int(0x67, 0, 1);
            //TeamName = Encoding.UTF8.GetString(bytes, 0x68, 70).TrimEnd((char)0);   
            //Scoreboardname = Encoding.UTF8.GetString(bytes, 0xAE, 4).TrimEnd((char)0);
            //CustomStadiumName = Encoding.UTF8.GetString(bytes, 0xB2, 181).TrimEnd((char)0);
            //Banner1Text = BitConverter.ToUInt16(bytes, 0x167);
            //Banner2Text = BitConverter.ToUInt16(bytes, 0x177);
            //Banner3Text = BitConverter.ToUInt16(bytes, 0x187);
            //Banner4Text = BitConverter.ToUInt16(bytes, 0x197);
            //UnknownD = BitConverter.ToUInt16(bytes, 0x1A7);

        }

        public void AddPlayer(Player player)
        {
            if (!Players.Contains(player))
            {
                var teamLink = IsNationalTeam ? TeamPlayerLinks.GetNationalTeamLinkForPlayer(player) : TeamPlayerLinks.GetClubTeamLinkForPlayer(player);
                int shirtNo = PlayerAssignments.Max(p => p.ShirtNo) + 1;

                if (teamLink == null)
                {
                    teamLink = new TeamPlayerLink();
                    teamLink.Index = TeamPlayerLinks.List.Count + 1;
                    teamLink.PlayerID = (int)player.ID;
                }

                teamLink.TeamID = ID;
                teamLink.OrderInTeam = PlayerAssignments.Max(p => p.OrderInTeam) + 1;
                teamLink.ShirtNo = PlayerAssignments.Max(p => p.ShirtNo) + 1;
                teamLink.IsCaptain = false;
                teamLink.IsLeftCK = false;
                teamLink.IsLongFK = false;
                teamLink.IsPenaltyKicker = false;
                teamLink.IsShortFK = false;
                teamLink.IsRightCK = false;

                if (!TeamPlayerLinks.List.Contains(teamLink))
                {
                    TeamPlayerLinks.List.Add(teamLink);
                }

                OnPropertyChanged(nameof(Players));
            }
        }
        public void RemovePlayer(Player player)
        {
            if (Players.Contains(player))
            {
                var tpls = TeamPlayerLinks.List.Where(tpl => tpl.PlayerID == player.ID && tpl.TeamID == ID).ToList();

                foreach (var tpl in tpls)
                {
                    TeamPlayerLinks.List.Remove(tpl);
                }

                tpls = TeamPlayerLinks.List.Where(tpl => tpl.PlayerID == player.ID).ToList();

                OnPropertyChanged(nameof(Players));
            }
        }
    }
}
