using System.ComponentModel.DataAnnotations;

namespace FormW2SDK.Models.Base
{
    public class EntityBase
    {
        #region TinType
        public enum TinType
        {
            [Display(Name = "EIN")]
            EIN,
            [Display(Name = "SSN")]
            SSN

        }
        #endregion

        #region BusinessType
        public enum BusinessType
        {
            [Display(Name = "Estate")]
            ESTE,
            [Display(Name = "Partnership")]
            PART,
            [Display(Name = "Corporation")]
            CORP,
            [Display(Name = "Exempt Organization")]
            EORG,
            [Display(Name = "Sole Proprietorship")]
            SPRO
        }
        #endregion

        #region KindOfEmployer
        public enum KindOfEmployer
        {
            [Display(Name = "Federl Govt")]
            FEDERALGOVT,
            [Display(Name = "State Or Local501C")]
            STATEORLOCAL501C,
            [Display(Name = "Non Govt 501C")]
            NONGOVT501C,
            [Display(Name = "State Or Local Non 501C")]
            STATEORLOCALNON501C,
            [Display(Name = "None Apply")]
            NONEAPPLY
        }
        #endregion

        #region KindOfPayer
        public enum KindOfPayer
        {
            [Display(Name = "Regular941")]
            REGULAR941,
            [Display(Name = "Regular944")]
            REGULAR944,
            [Display(Name = "Agricultural943")]
            AGRICULTURAL943,
            [Display(Name = "House Hold")]
            HOUSEHOLD,
            [Display(Name = "Military")]
            MILITARY,
            [Display(Name = "Medi Care Qual Govem")]
            MEDICAREQUALGOVEM,
            [Display(Name = "Railroad Form CT-1")]
            RAILROADFORMCT1

        }
        #endregion

        #region CorporationBusinessMembers
        public enum CorporationBusinessMembers
        {
            [Display(Name = "President")]
            PRESIDENT,
            [Display(Name = "Vice President")]
            VICEPRESIDENT,
            [Display(Name = "Treasurer")]
            TREASURER,
            [Display(Name = "Assistant Treasurer")]
            ASSISTANTTREASURER,
            [Display(Name = "Chief Accounting Officer")]
            CHIEFACCOUNTINGOFFICER,
            [Display(Name = "Tax Officer")]
            TAXOFFICER,
            [Display(Name = "Chief Operating Officer")]
            CHIEFOPERATINGOFFICER,
            [Display(Name = "Corporate Secretary")]
            CORPORATESECRETARY,
            [Display(Name = "Secretary Treasurer")]
            SECRETARYTREASURER,
            [Display(Name = "Corporate Officer")]
            CORPORATEOFFICER,
            [Display(Name = "Member")]
            MEMBER
        }
        #endregion

        #region USStates
        public enum USStates
        {
            [Display(Name = "Alabama")]
            AL,
            [Display(Name = "Alaska")]
            AK,
            [Display(Name = "Arizona")]
            AZ,
            [Display(Name = "Arkansas")]
            AR,
            [Display(Name = "California")]
            CA,
            [Display(Name = "Colorado")]
            CO,
            [Display(Name = "Connecticut")]
            CT,
            [Display(Name = "Delaware")]
            DE,
            [Display(Name = "DistrictofColumbia")]
            DC,
            [Display(Name = "Florida")]
            FL,
            [Display(Name = "Georgia")]
            GA,
            [Display(Name = "Hawaii")]
            HI,
            [Display(Name = "Idaho")]
            ID,
            [Display(Name = "Illinois")]
            IL,
            [Display(Name = "Indiana")]
            IN,
            [Display(Name = "Iowa")]
            IA,
            [Display(Name = "Kansas")]
            KS,
            [Display(Name = "Kentucky")]
            KY,
            [Display(Name = "Louisiana")]
            LA,
            [Display(Name = "Maine")]
            ME,
            [Display(Name = "MaryLand")]
            MD,
            [Display(Name = "Massachusetts")]
            MA,
            [Display(Name = "Michigan")]
            MI,
            [Display(Name = "Minnesota")]
            MN,
            [Display(Name = "Mississippi")]
            MS,
            [Display(Name = "Missouri")]
            MO,
            [Display(Name = "Montana")]
            MT,
            [Display(Name = "Nebraska")]
            NE,
            [Display(Name = "Nevada")]
            NV,
            [Display(Name = "NewHampshire")]
            NH,
            [Display(Name = "NewJersey")]
            NJ,
            [Display(Name = "NewMexico")]
            NM,
            [Display(Name = "NewYork")]
            NY,
            [Display(Name = "NorthCarolina")]
            NC,
            [Display(Name = "NorthDakota")]
            ND,
            [Display(Name = "Ohio")]
            OH,
            [Display(Name = "Oklahoma")]
            OK,
            [Display(Name = "Oregon")]
            OR,
            [Display(Name = "Pennsylvania")]
            PA,
            [Display(Name = "RhodeIsland")]
            RI,
            [Display(Name = "SouthCarolina")]
            SC,
            [Display(Name = "SouthDakota")]
            SD,
            [Display(Name = "Tennessee")]
            TN,
            [Display(Name = "Texas")]
            TX,
            [Display(Name = "Utah")]
            UT,
            [Display(Name = "Vermont")]
            VT,
            [Display(Name = "Virginia")]
            VA,
            [Display(Name = "Washington")]
            WA,
            [Display(Name = "WestVirginia")]
            WV,
            [Display(Name = "Wisconsin")]
            WI,
            [Display(Name = "Wyoming")]
            WY
        }
        #endregion

        #region CountryList
        public enum CountryList
        {
            [Display(Name = "Canada")] CA,
            [Display(Name = "Mexico")] MX,
            [Display(Name = "Afghanistan")] AF,
            [Display(Name = "Akrotiri")] AX,
            [Display(Name = "Aland Island")] XI,
            [Display(Name = "Albania")] AL,
            [Display(Name = "Algeria")] AG,
            [Display(Name = "American Samoa")] AQ,
            [Display(Name = "Andorra")] AN,
            [Display(Name = "Angola")] AO,
            [Display(Name = "Anguilla")] AV,
            [Display(Name = "Antarctica")] AY,
            [Display(Name = "Antigua and Barbuda")] AC,
            [Display(Name = "Argentina")] AR,
            [Display(Name = "Armenia")] AM,
            [Display(Name = "Aruba")] AA,
            [Display(Name = "Ascension")] XA,
            [Display(Name = "Ashmore and Cartier Islands")] AT,
            [Display(Name = "Australia")] AS,
            [Display(Name = "Austria")] AU,
            [Display(Name = "Azerbaijan")] AJ,
            [Display(Name = "Azores")] XZ,
            [Display(Name = "Bahamas")] BF,
            [Display(Name = "Bahrain")] BA,
            [Display(Name = "Baker Islands")] FQ,
            [Display(Name = "Bangladesh")] BG,
            [Display(Name = "Barbados")] BB,
            [Display(Name = "Bassas da India")] BS,
            [Display(Name = "Belarus")] BO,
            [Display(Name = "Belgium")] BE,
            [Display(Name = "Belize")] BH,
            [Display(Name = "Benin")] BN,
            [Display(Name = "Bermuda")] BD,
            [Display(Name = "Bhutan")] BT,
            [Display(Name = "Bolivia")] BL,
            [Display(Name = "Bosnia-Herzegovina")] BK,
            [Display(Name = "Botswana")] BC,
            [Display(Name = "Bouvet Island")] BV,
            [Display(Name = "Brazil")] BR,
            [Display(Name = "British Indian OceanTerritory")] IO,
            [Display(Name = "British Virgin Islands")] VI,
            [Display(Name = "Brunei")] BX,
            [Display(Name = "Bulgaria")] BU,
            [Display(Name = "Burkina Faso")] UV,
            [Display(Name = "Burma")] BM,
            [Display(Name = "Burundi")] BY,
            [Display(Name = "Cambodia")] CB,
            [Display(Name = "Cameroon")] CM,
            [Display(Name = "Canary Islands")] XY,
            [Display(Name = "Cape Verde")] CV,
            [Display(Name = "Cayman Islands")] CJ,
            [Display(Name = "Central African Republic")] CT,
            [Display(Name = "Chad")] CD,
            [Display(Name = "Channel Islands")] XC,
            [Display(Name = "Chile")] CI,
            [Display(Name = "China")] CH,
            [Display(Name = "Christmas Island")] KT,
            [Display(Name = "Clipperton Island")] IP,
            [Display(Name = "Cocos (Keeling) Islands")] CK,
            [Display(Name = "Colombia")] CO,
            [Display(Name = "Comoros")] CN,
            [Display(Name = "Congo (Brazzaville)")] CF,
            [Display(Name = "Congo (Kinshasa)")] CG,
            [Display(Name = "Cook Islands")] CW,
            [Display(Name = "Coral Sea Islands")] CR,
            [Display(Name = "Corsica")] VP,
            [Display(Name = "Costa Rica")] CS,
            [Display(Name = "Cote D'Ivoire (IvoryCoast)")] IV,
            [Display(Name = "Croatia")] HR,
            [Display(Name = "Cuba")] CU,
            [Display(Name = "Cyprus")] CY,
            [Display(Name = "Czech Republic")] EZ,
            [Display(Name = "Denmark")] DA,
            [Display(Name = "Dhekelia")] DX,
            [Display(Name = "Djibouti")] DJ,
            [Display(Name = "Dominica")] DO,
            [Display(Name = "Dominican Republic")] DR,
            [Display(Name = "East Timor")] TT,
            [Display(Name = "Ecuador")] EC,
            [Display(Name = "Egypt")] EG,
            [Display(Name = "El Salvador")] ES,
            [Display(Name = "England")] UK,
            [Display(Name = "Equatorial Guinea")] EK,
            [Display(Name = "Eritrea")] ER,
            [Display(Name = "Estonia")] EN,
            [Display(Name = "Ethiopia")] ET,
            [Display(Name = "Europa Island")] EU,
            [Display(Name = "Falkland Islands (Islas Malvinas)")] FK,
            [Display(Name = "Faroe Islands")] FO,
            [Display(Name = "Federated States of Micronesia")] FM,
            [Display(Name = "Fiji")] FJ,
            [Display(Name = "Finland")] FI,
            [Display(Name = "France")] FR,
            [Display(Name = "French Guiana")] FG,
            [Display(Name = "French Polynesia")] FP,
            [Display(Name = "French Southern and Antarctic Lands")] FS,
            [Display(Name = "Gabon")] GB,
            [Display(Name = "The Gambia")] GA,
            [Display(Name = "Gaza Strip")] GZ,
            [Display(Name = "Georgia")] GG,
            [Display(Name = "Germany")] GM,
            [Display(Name = "Ghana")] GH,
            [Display(Name = "Gibraltar")] GI,
            [Display(Name = "Glorioso Islands")] GO,
            [Display(Name = "Greece")] GR,
            [Display(Name = "Greenland")] GL,
            [Display(Name = "Grenada")] GJ,
            [Display(Name = "Guadeloupe")] GP,
            [Display(Name = "Guam")] GQ,
            [Display(Name = "Guatemala")] GT,
            [Display(Name = "Guernsey")] GK,
            [Display(Name = "Guinea")] GV,
            [Display(Name = "Guinea-Bissau")] PU,
            [Display(Name = "Guyana")] GY,
            [Display(Name = "Haiti")] HA,
            [Display(Name = "Heard Island and McDonald Islands")] HM,
            [Display(Name = "Honduras")] HO,
            [Display(Name = "Hong Kong")] HK,
            [Display(Name = "Howland Island")] HQ,
            [Display(Name = "Hungary")] HU,
            [Display(Name = "Iceland")] IC,
            [Display(Name = "India")] IN,
            [Display(Name = "Indonesia")] ID,
            [Display(Name = "Iran")] IR,
            [Display(Name = "Iraq")] IZ,
            [Display(Name = "Ireland")] EI,
            [Display(Name = "Israel")] IS,
            [Display(Name = "Italy")] IT,
            [Display(Name = "Jamaica")] JM,
            [Display(Name = "Jan Mayen")] JN,
            [Display(Name = "Japan")] JA,
            [Display(Name = "Jarvis Island")] DQ,
            [Display(Name = "Jersey")] JE,
            [Display(Name = "Johnston Atoll")] JQ,
            [Display(Name = "Jordan")] JO,
            [Display(Name = "Juan de Nova Island")] JU,
            [Display(Name = "Kazakhstan")] KZ,
            [Display(Name = "Kenya")] KE,
            [Display(Name = "Kingman Reef")] KQ,
            [Display(Name = "Kiribati")] KR,
            [Display(Name = "Korea, Democratic People's Republic of (North)")] KN,
            [Display(Name = "Korea, Republic of (South)")] KS,
            [Display(Name = "Kuwait")] KU,
            [Display(Name = "Kyrgyzstan")] KG,
            [Display(Name = "Laos")] LA,
            [Display(Name = "Latvia")] LG,
            [Display(Name = "Lebanon")] LE,
            [Display(Name = "Lesotho")] LT,
            [Display(Name = "Liberia")] LI,
            [Display(Name = "Libya")] LY,
            [Display(Name = "Liechtenstein")] LS,
            [Display(Name = "Lithuania")] LH,
            [Display(Name = "Luxembourg")] LU,
            [Display(Name = "Macau")] MC,
            [Display(Name = "Macedonia")] MK,
            [Display(Name = "Madagascar")] MA,
            [Display(Name = "Malawi")] MI,
            [Display(Name = "Malaysia")] MY,
            [Display(Name = "Maldives")] MV,
            [Display(Name = "Mali")] ML,
            [Display(Name = "Malta")] MT,
            [Display(Name = "Man, Isle of")] IM,
            [Display(Name = "Marshall Islands")] RM,
            [Display(Name = "Martinique")] MB,
            [Display(Name = "Mauritania")] MR,
            [Display(Name = "Mauritius")] MP,
            [Display(Name = "Mayotte")] MF,
            [Display(Name = "Midway Islands")] MQ,
            [Display(Name = "Moldova")] MD,
            [Display(Name = "Monaco")] MN,
            [Display(Name = "Mongolia")] MG,
            [Display(Name = "Montenegro")] MJ,
            [Display(Name = "Montserrat")] MH,
            [Display(Name = "Morocco")] MO,
            [Display(Name = "Mozambique")] MZ,
            [Display(Name = "Myanmar")] XM,
            [Display(Name = "Namibia")] WA,
            [Display(Name = "Nauru")] NR,
            [Display(Name = "Navassa Island")] BQ,
            [Display(Name = "Nepal")] NP,
            [Display(Name = "Netherlands")] NL,
            [Display(Name = "Netherlands Antilles")] NT,
            [Display(Name = "New Caledonia")] NC,
            [Display(Name = "New Zealand")] NZ,
            [Display(Name = "Nicaragua")] NU,
            [Display(Name = "Niger")] NG,
            [Display(Name = "Nigeria")] NI,
            [Display(Name = "Niue")] NE,
            [Display(Name = "Norfolk Island")] NF,
            [Display(Name = "Northern Ireland")] XN,
            [Display(Name = "Northern Mariana Islands")] CQ,
            [Display(Name = "Norway")] NO,
            [Display(Name = "Oman")] MU,
            [Display(Name = "Other Country (country not identified elsewhere)")] OC,
            [Display(Name = "Pakistan")] PK,
            [Display(Name = "Palmyra Atoll")] LQ,
            [Display(Name = "Palau")] PS,
            [Display(Name = "Panama")] PM,
            [Display(Name = "Papua-New Guinea")] PP,
            [Display(Name = "Paracel Islands")] PF,
            [Display(Name = "Paraguay")] PA,
            [Display(Name = "Peru")] PE,
            [Display(Name = "Philippines")] RP,
            [Display(Name = "Pitcairn Islands")] PC,
            [Display(Name = "Poland")] PL,
            [Display(Name = "Portugal")] PO,
            [Display(Name = "Puerto Rico")] RQ,
            [Display(Name = "Qatar")] QA,
            [Display(Name = "Reunion")] RE,
            [Display(Name = "Romania")] RO,
            [Display(Name = "Russia")] RS,
            [Display(Name = "Rwanda")] RW,
            [Display(Name = "Samoa and Western Samoa")] WS,
            [Display(Name = "San Marino")] SM,
            [Display(Name = "Sao Tome and Principe")] TP,
            [Display(Name = "Saudi Arabia")] SA,
            [Display(Name = "Scotland")] XS,
            [Display(Name = "Senegal")] SG,
            [Display(Name = "Serbia")] RI,
            [Display(Name = "Seychelles")] SE,
            [Display(Name = "Sierra Leone")] SL,
            [Display(Name = "Singapore")] SN,
            [Display(Name = "Slovak Republic")] XR,
            [Display(Name = "Slovakia")] LO,
            [Display(Name = "Slovenia")] SI,
            [Display(Name = "Solomon Islands")] BP,
            [Display(Name = "Somalia")] SO,
            [Display(Name = "South Africa")] SF,
            [Display(Name = "South Georgia and the South Sandwich Islands")] SX,
            [Display(Name = "Spain")] SP,
            [Display(Name = "Spratly Islands")] PG,
            [Display(Name = "Sri Lanka")] CE,
            [Display(Name = "St. Helena")] SH,
            [Display(Name = "St. Kitts and Nevis")] SC,
            [Display(Name = "St. Lucia Island")] ST,
            [Display(Name = "St. Pierre and Miquelon")] SB,
            [Display(Name = "St. Vincent and the Grenadines")] VC,
            [Display(Name = "Sudan")] SU,
            [Display(Name = "Suriname")] NS,
            [Display(Name = "Svalbard")] SV,
            [Display(Name = "Swaziland")] WZ,
            [Display(Name = "Sweden")] SW,
            [Display(Name = "Switzerland")] SZ,
            [Display(Name = "Syria")] SY,
            [Display(Name = "Taiwan")] TW,
            [Display(Name = "Tajikistin")] TI,
            [Display(Name = "Tanzania")] TZ,
            [Display(Name = "Thailand")] TH,
            [Display(Name = "Togo")] TO,
            [Display(Name = "Tokelau")] TL,
            [Display(Name = "Tonga")] TN,
            [Display(Name = "Trinidad and Tobago")] TD,
            [Display(Name = "Tristan Da Cunha")] XT,
            [Display(Name = "Tromelin Island")] TE,
            [Display(Name = "Tunisia")] TS,
            [Display(Name = "Turkey")] TU,
            [Display(Name = "Turkmenistan")] TX,
            [Display(Name = "Turks and Caicos Islands")] TK,
            [Display(Name = "Tuvalu")] TV,
            [Display(Name = "Uganda")] UG,
            [Display(Name = "Ukraine")] UP,
            [Display(Name = "United Arab Emirates")] AE,
            [Display(Name = "Uruguay")] UY,
            [Display(Name = "Uzbekistan")] UZ,
            [Display(Name = "Vanuatu")] NH,
            [Display(Name = "Vatican City")] VT,
            [Display(Name = "Venezuela")] VE,
            [Display(Name = "Vietnam")] VM,
            [Display(Name = "Virgin Islands")] VQ,
            [Display(Name = "Wake Island")] WQ,
            [Display(Name = "Wales")] XW,
            [Display(Name = "Wallis and Futuna")] WF,
            [Display(Name = "West Bank")] WE,
            [Display(Name = "Western Sahara")] WI,
            [Display(Name = "Yemen (Aden)")] YM,
            [Display(Name = "Yugoslavia")] YI,
            [Display(Name = "Zambia")] ZA,
            [Display(Name = "Zimbabwe")] ZI,
            [Display(Name = "Saint Barthelemy")] TB,
        }
        #endregion

        #region EstateBusinessMembers
        public enum EstateBusinessMembers
        {
            [Display(Name = "Administrator")]
            ADMINISTRATOR,
            [Display(Name = "Executor")]
            EXECUTOR,
            [Display(Name = "Trustee")]
            TRUSTEE,
            [Display(Name = "Fiduciary")]
            FIDUCIARY
        }
        #endregion

        #region PartnershipBusinessMembers
        public enum PartnershipBusinessMembers
        {
            [Display(Name = "Partner")]
            PARTNER,
            [Display(Name = "General Partner")]
            GENERALPARTNER,
            [Display(Name = "Limited Partner")]
            LIMITEDPARTNER,
            [Display(Name = "LLC Member")]
            LLCMEMBER,
            [Display(Name = "Manager")]
            MANAGINGMEMBER,
            [Display(Name = "Member")]
            MEMBER,
            [Display(Name = "Managing Member")]
            MANAGER,
            [Display(Name = "President")]
            PRESIDENT,
            [Display(Name = "Owner")]
            OWNER,
            [Display(Name = "Tax Matter Partner")]
            TAXMATTERPARTNER,
        }
        #endregion

        #region SoleProprietorshipBusinessMembers
        public enum SoleProprietorshipBusinessMembers
        {
            [Display(Name = "Owner")]
            OWNER,
            [Display(Name = "Sole Poprietor")]
            SOLEPROPRIETOR,
            [Display(Name = "Member")]
            MEMBER,
            [Display(Name = "Sole Member")]
            SOLEMEMBER
        }
        #endregion

        #region ExemptOrganizationBusinessMembers
        public enum ExemptOrganizationBusinessMembers
        {
            [Display(Name = "President")]
            PRESIDENT,
            [Display(Name = "Vice President")]
            VICEPRESIDENT,
            [Display(Name = "Corporate Treasurer")]
            CORPORATETREASURER,
            [Display(Name = "Treasurer")]
            TREASURER,
            [Display(Name = "Assistant Treasurer")]
            ASSISTANTTREASURER,
            [Display(Name = "Cheif Accounting Officer")]
            CHIEFACCOUNTINGOFFICER,
            [Display(Name = "Cheif Executive Officer")]
            CHIEFEXECUTIVEOFFICER,
            [Display(Name = "Cheif Financial Officer")]
            CHIEFFINANCIALOFFICER,
            [Display(Name = "Tax Officer")]
            TAXOFFICER,
            [Display(Name = "Cheif Operating Officer")]
            CHIEFOPERATINGOFFICER,
            [Display(Name = "Corporate Officer")]
            CORPORATEOFFICER,
            [Display(Name = "Executive Director")]
            EXECUTIVEDIRECTOR,
            [Display(Name = "Director")]
            DIRECTOR,
            [Display(Name = "Chairman")]
            CHAIRMAN,
            [Display(Name = "Executive Administrator")]
            EXECUTIVEADMINISTRATOR,
            [Display(Name = "Administrator")]
            ADMINISTRATOR,
            [Display(Name = "Receiver")]
            RECEIVER,
            [Display(Name = "Trustee")]
            TRUSTEE,
            [Display(Name = "Pastor")]
            PASTOR,
            [Display(Name = "Assistant Toreligious Leader")]
            ASSISTANTTORELIGIOUSLEADER,
            [Display(Name = "Reverend")]
            REVEREND,
            [Display(Name = "Priest")]
            PRIEST,
            [Display(Name = "Minister")]
            MINISTER,
            [Display(Name = "Rabbi")]
            RABBI,
            [Display(Name = "Leader Of Religious Organization")]
            LEADEROFRELIGIOUSORGANIZATION,
            [Display(Name = "Secretary")]
            SECRETARY,
            [Display(Name = "Director Of Taxation")]
            DIRECTOROFTAXATION,
            [Display(Name = "Director Of Personnel")]
            DIRECTOROFPERSONNEL
        }
        #endregion

        #region IndianaStateCounties
        public enum IndianaStateCounties
        {
            ADAMS = 1,
            ALLEN = 2,
            BARTHOLOMEW = 3,
            BENTON = 4,
            BLACKFORD = 5,
            BOONE = 6,
            BROWN = 7,
            CARROLL = 8,
            CASS = 9,
            CLARK = 10,
            CLAY = 11,
            CLINTON = 12,
            CRAWFORD = 13,
            DAVIESS = 14,
            DEARBORN = 15,
            DECATUR = 16,
            DEKALB = 17,
            DELAWARE = 18,
            DUBOIS = 19,
            ELKHART = 20,
            FAYETTE = 21,
            FLOYD = 22,
            FOUNTAIN = 23,
            FRANKLIN = 24,
            FULTON = 25,
            GIBSON = 26,
            GRANT = 27,
            GREENE = 28,
            HAMILTON = 29,
            HANCOCK = 30,
            HARRISON = 31,
            HENDRICKS = 32,
            HENRY = 33,
            HOWARD = 34,
            HUNTINGTON = 35,
            JACKSON = 36,
            JASPER = 37,
            JAY = 38,
            JEFFERSON = 39,
            JENNINGS = 40,
            JOHNSON = 41,
            KNOX = 42,
            KOSCIUSKO = 43,
            LAGRANGE = 44,
            LAKE = 45,
            LAPORTE = 46,
            LAWRENCE = 47,
            MADISON = 48,
            MARION = 49,
            MARSHALL = 50,
            MARTIN = 51,
            MIAMI = 52,
            MONROE = 53,
            MONTGOMERY = 54,
            MORGAN = 55,
            NEWTON = 56,
            NOBLE = 57,
            OHIO = 58,
            ORANGE = 59,
            OWEN = 60,
            PARKE = 61,
            PERRY = 62,
            PIKE = 63,
            PORTER = 64,
            POSEY = 65,
            PULASKI = 66,
            PUTNAM = 67,
            RANDOLPH = 68,
            RIPLEY = 69,
            RUSH = 70,
            STJOSEPH = 71,
            SCOTT = 72,
            SHELBY = 73,
            SPENCER = 74,
            STARKE = 75,
            STEUBEN = 76,
            SULLIVAN = 77,
            SWITZERLAND = 78,
            TIPPECANOE = 79,
            TIPTON = 80,
            UNION = 81,
            VANDERBURGH = 82,
            VERMILLION = 83,
            VIGO = 84,
            WABASH = 85,
            WARREN = 86,
            WARRICK = 87,
            WASHINGTON = 88,
            WAYNE = 89,
            WELLS = 90,
            WHITE = 91,
            WHITLEY = 92,
        }
        #endregion

        #region Box12Codes
        public enum Box12Codes
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            J,
            K,
            L,
            M,
            N,
            P,
            Q,
            R,
            S,
            T,
            V,
            W,
            Y,
            Z,
            AA,
            BB,
            CC,
            DD,
            EE,
            FF,
            GG,
            HH
        }
        #endregion

        #region TypeOfTax
        public enum TypeOfTax
        {
            [Display(Name = "City Income Tax (C)")]
            CityIncomeTax = 1,
            [Display(Name = "County Income Tax (D)")]
            CountyIncomeTax = 2,
            [Display(Name = "School District Income Tax (E)")]
            SchoolDistrictIncomeTax = 3,
            [Display(Name = "Other Income Tax (F)")]
            OtherIncomeTax = 4,
        }
        #endregion

        #region OverPaymentType
        public enum OverPaymentType
        {
            Refund,
            Credit
        }
        #endregion

        #region PaymentMethod
        public enum PaymentMethod
        {
            [Display(Name = "EFT DEBIT")]
            EftDebit,
            [Display(Name = "EFT CREDIT")]
            EftCredit

        }
        #endregion

        #region AccountTypes
        public enum AccountTypes
        {
            Checking,
            Saving
        }
        #endregion

        #region AccountTypes
        public enum FilingCycle
        {
            Quarterly,
            Monthly,
            SemiMonthly,
            Annual


        }
        #endregion

        #region FilingPeriodType
        public enum FilingPeriodType
        {
            NONE = 0,
            QUADMONTHLY = 1,
            SEMIMONTHLY = 2,
            MONTHLY = 3,
            QUARTERLY = 4,
            ANNUAL = 5
        }
        #endregion

        #region PayFrequencyType

        public enum PayFrequencyType
        {
            MONTHLY,
            SEMIWEEKLY,
            QUARTERLY
        }
        #endregion

    }
}

