using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    public class Nationality
    {
        public int Index;
        public int ID;
        public string Name;
        public string Abbrv;
    }

    public static class Nationalities
    {
        public static Dictionary<int, Nationality> GetAll()
        {
            var all = @"0;1;IRELAND;IRL
                1;2;NORTHERN IRELAND;NIR
                2;3;SCOTLAND;SCO
                3;4;WALES;WAL
                4;5;ENGLAND;ENG
                5;6;PORTUGAL;PRT
                6;7;SPAIN;ESP
                7;8;FRANCE;FRA
                8;9;BELGIUM;BEL
                9;10;NETHERLANDS;NLD
                10;11;SWITZERLAND;CHE
                11;12;ITALY;ITA
                12;13;CZECH REPUBLIC;CZE
                13;14;GERMANY;DEU
                14;15;DENMARK;DNK
                15;16;NORWAY;NOR
                16;17;SWEDEN;SWE
                17;19;POLAND;POL
                18;20;SLOVAKIA;SVK
                19;21;AUSTRIA;AUT
                20;22;HUNGARY;HUN
                21;23;SLOVENIA;SVN
                22;24;CROATIA;HRV
                23;26;ROMANIA;ROU
                24;27;BULGARIA;BGR
                25;28;GREECE;GRC
                26;29;TURKEY;TUR
                27;30;UKRAINE;UKR
                28;31;RUSSIA;RUS
                29;32;MOROCCO;MAR
                30;33;TUNISIA;TUN
                31;34;EGYPT;EGY
                32;35;NIGERIA;NGA
                33;36;CAMEROON;CMR
                34;37;SOUTH AFRICA;ZAF
                35;38;SENEGAL;SEN
                36;39;UNITED STATES;USA
                37;40;MEXICO;MEX
                38;41;JAMAICA;JAM
                39;42;COSTA RICA;CRI
                40;43;HONDURAS;HND
                41;44;COLOMBIA;COL
                42;45;BRAZIL;BRA
                43;46;PERU;PER
                44;47;CHILE;CHL
                45;48;PARAGUAY;PRY
                46;49;URUGUAY;URY
                47;50;ARGENTINA;ARG
                48;51;ECUADOR;ECU
                49;52;JAPAN;JPN
                50;53;SOUTH KOREA;KOR
                51;54;CHINA;CHN
                52;55;IRAN;IRN
                53;56;SAUDI ARABIA;SAU
                54;57;AUSTRALIA;AUS
                55;1010;IRAQ;IRQ
                56;1011;JORDAN;JOR
                57;1012;NORTH KOREA;PRK
                58;1013;KUWAIT;KWT
                59;1015;LEBANON;LBN
                60;1022;OMAN;OMN
                61;1026;QATAR;QAT
                62;1031;THAILAND;THA
                63;1032;UAE;ARE
                64;1040;ALGERIA;DZA
                65;1044;BURKINA FASO;BFA
                66;1051;CÔTE D'IVOIRE;CIV
                67;1058;GHANA;GHA
                68;1059;GUINEA;GIN
                69;11067;MALI;MLI
                70;1083;ZAMBIA;ZMB
                71;1113;PANAMA;PAN
                72;1128;BOLIVIA;BOL
                73;1129;VENEZUELA;VEN
                74;1141;NEW ZEALAND;NZL
                75;1164;ISRAEL;ISR
                76;1165;ALBANIA;ALB
                77;1170;BOSNIA AND HERZEGOVINA;BIH
                78;1175;ICELAND;ISL
                79;1184;UZBEKISTAN;UZB
                80;2703;EUROPEAN CLASSICS;ECL
                81;2704;WORLD CLASSICS;WCL
                82;1171;CYPRUS;CYP
                83;1172;ESTONIA;EST
                84;1173;FAROE ISLANDS;FRO
                85;18;FINLAND;FIN
                85;1174;GEORGIA;GEO
                86;1176;KAZAKHSTAN;KAZ
                87;2510;KOSOVO;KSV
                88;59;LATVIA;LVA
                89;1177;LIECHTENSTEIN;LIE
                90;1178;LITHUANIA;LTU
                91;1179;LUXEMBOURG;LUX
                92;1181;MALTA;MLT
                93;1182;MOLDOVA;MDA
                94;1151;MONTENEGRO;MTN
                95;1183;SAN MARINO;SMR
                96;1550;SERBIA;SER
                97;1041;ANGOLA;AGO
                98;1042;BENIN;BEN
                99;1043;BOTSWANA;BWA
                100;1045;BURUNDI;BDI
                101;1046;CAPE VERDE;CPV
                102;1047;CENTRAL AFRICA REP.;CAF
                103;1048;CHAD;TCD
                104;1087;CONGO;COG
                105;1050;CONGO DR;COD
                106;1053;EQUATORIAL GUNIEA;GNQ
                107;1054;ERITREA;ERI
                108;1055;ETHIOPIA;ETH
                109;1056;GABON;GAB
                110;1060;GUINEA-BISSAU;GNB
                111;1061;KENYA;KEN
                112;1063;LIBERIA;LBR
                113;1064;LIBAY;LBR
                114;1065;MADAGASCAR;MDG
                115;1066;MALAWI;MWI
                116;1068;MAUROTANIA;MRT
                117;1069;MAURITIUS;MUS
                118;1070;MOZAMBIQUE;MOZ
                119;1071;NAMIBIA;NAM
                120;1072;NIGER;NAM
                121;1057;REPUBLIC OF THE GAMBIA;GMB
                122;1089;REUNION;REU
                123;1073;RWANDA;RWA
                124;1076;SIERRA LEONE;SLE
                125;1077;SOMALIA;SOM
                126;2509;SOUTH SUDAN;SSD
                127;1049;THE CAMAROS;COM
                128;1081;TOGO;TGO
                129;1082;UGANDA;UGA
                130;1084;ZIMBABWE;ZWE
                131;1093;ANTIGUA AND BARBUDA;ATG
                132;1094;ARUBA;ABW
                133;1096;BARBADOS;BRB
                134;1098;BERMUDA;BMU
                135;1049;CANADA;CAN
                136;1101;CUBA;CUB
                137;1124;CURAÇAO;CUW
                138;1103;DOMINICAN REPUBLIC;DOM
                139;1104;EL SALVADOR;SLV
                140;1105;GRENADA;GRD
                141;1106;GUADELOUPE;GLP
                142;1107;GUATEMALA;GTM
                143;1135;GUYANA;GUY
                144;1108;HAITA;HTI
                145;1109;MARTINIQUE;MTQ
                146;1110;MONTSERRAT;MSR
                147;1114;PUERTO RICO;PRI
                148;2238;SINT MAARTEN;SXM
                149;1118;TRINIDAD AND TOBAGO;TTO
                150;1119;TURK AND COICOS;TCA
                151;1001;AFGHANISTAN;AFG
                152;1002;BAHRAIN;BHR
                153;1003;BANGLADESH;BGD
                154;1008;INDIA;IND
                155;1009;INDONESIA;IDN
                156;1035;KYRGYZ;KGZ
                157;1014;LAOS;LAO
                158;1017;MALAYSIA;MYS
                159;1018;MALDIVES;MDV
                160;1022;PAKISTAN;PAK
                161;1024;PALESTINE;PSE
                162;1025;PHILIPPINES;PHL
                163;1027;SINGAPORE;SGP
                164;1028;SRI LANKA;LKA
                165;1029;SYRIA;SYR
                166;1036;TAJIKISTAN;TJK
                167;1037;TURKMENISTAN;TKM
                168;1033;VIETNAM;VNM
                169;1139;FIJI;FJI
                170;1140;NEW CALEDONIA;NCL
                171;1142;PAPUA NEW GUINEA;PNG
                172;1143;SAMOA;WSM
                173;1144;SOLOMON ISLANDS;SLB
                174;1145;TAHITI;TAH
                175;260;OTHERS;OTH";

            var result = new Dictionary<int, Nationality>();

            foreach (var line in all.Split(new[] { '\r', '\n' }))
            {
                if (line.Trim() != "")
                {
                    var parts = line.Trim().Split(';');
                    var nation = new Nationality();
                    nation.Index = Convert.ToInt32(parts[0]);
                    nation.ID = Convert.ToInt32(parts[1]);
                    nation.Name = parts[2];
                    nation.Abbrv = parts[3];
                }
            }

            return result;
        }
    }
}
