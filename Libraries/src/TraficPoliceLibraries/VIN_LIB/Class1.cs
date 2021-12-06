using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficPoliceLibraries
{
    public class Class1
    {
        private const byte VINMaxLength = 17;
        private const string VINAllowedSymbols = "0123456789ABCDEFGHJKLMNPRSTUVWXYZ";
        private const string VINForbiddenSymbols = "IOQ";
        private const string VINControlSumAllowedSymbols = "0123456789X";
        private const byte VINControlSumPosition = 8;

        private readonly Dictionary<char, int> _controlSumTable = new Dictionary<char, int>
        {
            ['A'] = 1,
            ['B'] = 2,
            ['C'] = 3,
            ['D'] = 4,
            ['E'] = 5,
            ['F'] = 6,
            ['G'] = 7,
            ['H'] = 8,
            ['J'] = 1,
            ['K'] = 2,
            ['L'] = 3,
            ['M'] = 4,
            ['N'] = 5,
            ['P'] = 7,
            ['R'] = 9,
            ['S'] = 2,
            ['T'] = 3,
            ['U'] = 4,
            ['V'] = 5,
            ['W'] = 6,
            ['X'] = 7,
            ['Y'] = 8,
            ['Z'] = 9
        };

        private readonly Dictionary<int, int> _controlSumVINCharWeight = new Dictionary<int, int>
        {
            [1] = 8,
            [2] = 7,
            [3] = 6,
            [4] = 5,
            [5] = 4,
            [6] = 3,
            [7] = 2,
            [8] = 10,
            [10] = 9,
            [11] = 8,
            [12] = 7,
            [13] = 6,
            [14] = 5,
            [15] = 4,
            [16] = 3,
            [17] = 2
        };

        private readonly Dictionary<string, int> _modelYears = new Dictionary<string, int>
        {
            ["A"] = 1980,
            ["L"] = 1990,
            ["Y"] = 2000,
            ["A"] = 2010,
            ["L"] = 2020,
            ["Y"] = 2030,
            ["B"] = 1981,
            ["M"] = 1991,
            ["1"] = 2001,
            ["B"] = 2011,
            ["M"] = 2021,
            ["1"] = 2031,
            ["C"] = 1982,
            ["N"] = 1992,
            ["2"] = 2002,
            ["C"] = 2012,
            ["N"] = 2022,
            ["2"] = 2032,
            ["D"] = 1983,
            ["P"] = 1993,
            ["3"] = 2003,
            ["D"] = 2013,
            ["P"] = 2023,
            ["3"] = 2033,
            ["E"] = 1984,
            ["R"] = 1994,
            ["4"] = 2004,
            ["E"] = 2014,
            ["R"] = 2024,
            ["4"] = 2034,
            ["F"] = 1985,
            ["S"] = 1995,
            ["5"] = 2005,
            ["F"] = 2015,
            ["S"] = 2025,
            ["5"] = 2035,
            ["G"] = 1986,
            ["T"] = 1996,
            ["6"] = 2006,
            ["G"] = 2016,
            ["T"] = 2026,
            ["6"] = 2036,
            ["H"] = 1987,
            ["V"] = 1997,
            ["7"] = 2007,
            ["H"] = 2017,
            ["V"] = 2027,
            ["7"] = 2037,
            ["J"] = 1988,
            ["W"] = 1998,
            ["8"] = 2008,
            ["J"] = 2018,
            ["W"] = 2028,
            ["8"] = 2038,
            ["K"] = 1989,
            ["X"] = 1999,
            ["9"] = 2009,
            ["K"] = 2019,
            ["X"] = 2029,
            ["9"] = 2039,

        };

        public bool CheckVIN(string vin)
        {
            if (vin.Length != VINMaxLength)
            {
                return false;
            }

            for (int i = 0; i < VINForbiddenSymbols.Length; i++)
            {
                if (vin.Contains(VINForbiddenSymbols[i]))
                {
                    return false;
                }
            }

            if (!VINControlSumAllowedSymbols.Contains(vin.Substring(8, 1)))
            {
                return false;
            }

            for (int i = 0; i < vin.Substring(13).Length; i++)
            {
                if (!char.IsDigit(vin.Substring(13)[i]))
                {
                    return false;
                }
            }

            #region CheckControlSumOfVIN
            int preControlSum = default(int);
            for (int i = 0; i < vin.Length; i++)
            {
                if (char.IsDigit(vin[i]))
                {
                    if (i != VINControlSumPosition)
                    {
                        preControlSum += Convert.ToInt32(vin[i].ToString()) * _controlSumVINCharWeight[i + 1];
                    }
                }
                else
                {
                    if (i != VINControlSumPosition)
                    {
                        preControlSum += _controlSumTable[vin[i]] * _controlSumVINCharWeight[i + 1];
                    }
                }
            }

            int tempControlSum = (preControlSum / 11) * 11;
            string vinControlSum = preControlSum - tempControlSum == 10 ? "X" : (preControlSum - tempControlSum).ToString();

            if (vin.Substring(VINControlSumPosition, 1) != vinControlSum)
            {
                return false;
            }
            #endregion

            return true;
        }

        public string GetVINCountry(string vin)
        {
            string wmi = vin.Substring(0, 3);
            if (char.IsDigit(wmi.Last()) && wmi.Last() == '9')
            {
                return vin.Substring(11, 3);
            }
            return "unassigned";
        }

        public int GetTransportYear(string vin)
        {
            if (CheckVIN(vin))
            {
                return _modelYears[vin[9].ToString()];
            }
            return -1;
        }
    }
}