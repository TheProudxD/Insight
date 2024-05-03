using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Utilites
{
    public static class ParseUtils
    {
        private static NumberFormatInfo _commaFormatInfo;

        private static NumberFormatInfo CommaFormatInfo =>
            _commaFormatInfo ??= new NumberFormatInfo
            {
                NumberDecimalSeparator = ","
            };

        private static NumberFormatInfo _dotFormatInfo;

        private static NumberFormatInfo DotFormatInfo =>
            _dotFormatInfo ??= new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

        public static float ParseStringToFloat(string s)
        {
            var curFormatInfo = s.Contains(",") ? CommaFormatInfo : DotFormatInfo;
            return float.Parse(s, curFormatInfo);
        }

        public static float SafeParseFloat(string s, float defaultValue = 0)
        {
            var curFormatInfo = s.Contains(",") ? CommaFormatInfo : DotFormatInfo;
            return float.TryParse(s, NumberStyles.Float, curFormatInfo, out var num) ? num : defaultValue;
        }

        public static DateTime ParseDateTimeFromString(string dateString, string formatDateString)
        {
            var canParseDate = DateTime.TryParseExact(dateString, formatDateString, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date);
            if (!canParseDate)
            {
                Debug.LogError("Cant parse date: " + dateString);
            }

            return date;
        }

        public static DateTime GetTimeOfDayFromString(string timeStr)
        {
            var time = DateTime.Today;
            var timeArr = timeStr.Split(':');
            float hours = 0;
            float minutes = 0;
            float seconds = 0;
            if (timeArr.Length > 0)
            {
                hours = float.Parse(timeArr[0]);
                if (timeArr.Length > 1)
                {
                    minutes = float.Parse(timeArr[1]);
                    if (timeArr.Length > 2)
                    {
                        seconds = float.Parse(timeArr[2]);
                    }
                }
            }

            time = time.AddHours(hours);
            time = time.AddMinutes(minutes);
            time = time.AddSeconds(seconds);
            return time;
        }

        public static string StringToUtf8String(string stringToEncode)
        {
            var strBytes = Encoding.Default.GetBytes(stringToEncode);
            var stringUTF8 = Encoding.UTF8.GetString(strBytes);
            return stringUTF8;
        }

        public static int[] IntListFromString(string str)
        {
            var strINT = str.Replace(" ", "").Split(',');
            var ints = new int[strINT.Length];
            for (var i = 0; i < strINT.Length; i++)
            {
                int.TryParse(strINT[i], out ints[i]);
            }

            return ints;
        }

        public static string IntListToString(List<int> intList) => ListToString(intList, ",");

        public static string IntArrayToString(int[] intList)
        {
            var listString = "";
            for (var i = 0; i < intList.Length; i++)
            {
                var separator = (i != intList.Length - 1) ? ", " : "";
                listString += intList[i] + separator;
            }

            return listString;
        }

        public static string StringListToString(List<string> stringList) => StringListToString(stringList, ",");

        public static string StringListToString(List<string> stringList, string separator) => ListToString(stringList, separator);

        public static string ListToString<T>(List<T> objList, string separator)
        {
            var combinedString = "";
            for (var i = 0; i < objList.Count; i++)
            {
                var curSeparator = (i != objList.Count - 1) ? separator : "";
                combinedString += objList[i] + curSeparator;
            }

            return combinedString;
        }

        public static List<int> StringToIntList(string str, char divider = ',')
        {
            str = str.Replace(" ", "");
            if (str == "") return new List<int>();

            var elements = new List<string>(str.Split(divider));
            elements = elements.FindAll(x => x != "");
            var res = new List<int>();

            foreach (var t in elements)
            {
                if (int.TryParse(t, out var temp))
                {
                    res.Add(temp);
                }
                else
                {
                    //UnityEngine.Debug.Log("Wrong '" + elements[i] + "' in " + str);
                }
            }

            return res;
        }

        public static Dictionary<int, int> StringToIntIntDictionary(string str, char pairsDivider = ',', char keyValueDevider = ':')
        {
            var toReturn = new Dictionary<int, int>();
            var streakPairs = new List<string>(str.Split(pairsDivider));
            foreach (var strinckPair in streakPairs)
            {
                if (!string.IsNullOrEmpty(strinckPair))
                {
                    var values = new List<string>(strinckPair.Split(keyValueDevider));
                    var key = SafeParseInt(values[0]);
                    var val = SafeParseInt(values[1]);
                    if (key != 0 || val != 0)
                        toReturn.Add(key, val);
                    else
                    {
                        throw new Exception("Can't parse to int int key val  " + strinckPair);
                    }
                }
            }

            return toReturn;
        }

        public static Vector2 StringToVector2(string str)
        {
            str = str.Replace(" ", "");
            if (str == "") return new Vector2();

            var elements = new List<string>(str.Split(','));
            elements = elements.FindAll(val => val != "");
            var res = new Vector2();

            if (int.TryParse(elements[0], out var x) && int.TryParse(elements[1], out var y))
            {
                res.x = x;
                res.y = y;
            }
            else
            {
                //Debug.Log("Wrong '" + elements[i] + "' in " + str);
            }

            return res;
        }

        public static List<float> StringToFloatList(string str, char divider = ';')
        {
            str = str.Replace(" ", "");
            if (str == "") return new List<float>();

            var elements = new List<string>(str.Split(divider));
            elements = elements.FindAll(x => x != "");
            var res = new List<float>();
            for (var i = 0; i < elements.Count; i++)
            {
                float temp = -1;
                temp = float.Parse(elements[i], CultureInfo.InvariantCulture);
                res.Add(temp);
            }

            return res;
        }

        public static List<string> StringToStringList(string str, char separator = ',')
        {
            str = str.Replace(separator + " ", separator.ToString());
            str = str.Replace(" " + separator, separator.ToString());
            if (str == "") return new List<string>();
            var elements = new List<string>(str.Split(separator));
            return elements;
        }

        public static float[] FloatArayFromString(string str, int expecterdLength = 0)
        {
            var origStr = str;
            str = str.Replace("[", "");
            str = str.Replace("[", "");
            str = str.Replace(" ", "");
            var stringArr = str.Split(',');
            var arrayLength = (expecterdLength > 0) ? expecterdLength : stringArr.Length;
            var floatArr = new float[arrayLength];

            if (expecterdLength != stringArr.Length)
            {
                Debug.LogError("Cant parse string: " + str + ", expecterd_length: " + expecterdLength);
            }
            else
            {
                for (var i = 0; i < arrayLength; i++)
                {
                    var rightVal = float.TryParse(stringArr[i], out var curFloat);
                    if (!rightVal)
                    {
                        Debug.LogError("Cant parse string: " + origStr + " to float[]");
                    }

                    floatArr[i] = curFloat;
                }
            }

            return floatArr;
        }

        public static Color ColorByString(string colorStr, float? alpha = null)
        {
            var color = Color.white;

            color = BaseColorByString(colorStr, alpha);

            /*
        if (Constants.CONSTANTS_COLORS.ContainsKey(color_str)){
            color = Constants.CONSTANTS_COLORS[color_str];
        }
        else{
            color = BaseColorByString(color_str, alpha);
        }
        */
            color.a = alpha.HasValue ? alpha.Value : 1.0f;
            return color;
        }

        public static Color BaseColorByString(string colorStr, float? alpha = null)
        {
            var color = colorStr switch
            {
                "black" => Color.black,
                "blue" => Color.blue,
                "cyan" => Color.cyan,
                "gray" => Color.gray,
                "green" => Color.green,
                "grey" => Color.grey,
                "magenta" => Color.magenta,
                "red" => Color.red,
                "white" => Color.white,
                "yellow" => Color.yellow,
                _ => Color.white
            };

            if (colorStr.Contains("("))
            {
                var tempString = colorStr.Substring(colorStr.IndexOf("("),
                    colorStr.IndexOf(",") - colorStr.IndexOf("("));
                var r = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

                tempString = colorStr.Substring(colorStr.IndexOf(",") + 1).Substring(0,
                    colorStr.Substring(colorStr.IndexOf(",") + 1).IndexOf(",") + 1);
                var g = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

                tempString = colorStr.Substring(colorStr.IndexOf(",") + 1)
                    .Substring(colorStr.Substring(colorStr.IndexOf(",") + 1).IndexOf(",") + 1);
                var b = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

                tempString = colorStr.Substring(colorStr.IndexOf(",") + 1)
                    .Substring(colorStr.Substring(colorStr.IndexOf(",") + 1).IndexOf(",") + 1);
                var a = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

                color = new Color(Convert.ToInt32(r) / 255.0f, Convert.ToInt32(g) / 255.0f, Convert.ToInt32(b) / 255.0f,
                    Convert.ToInt32(a) / 255.0f);
            }

            if (alpha.HasValue)
            {
                color.a = alpha.Value;
            }

            return color;
        }

        public static Color ParseColor(string colorStr)
        {
            colorStr = colorStr.Replace("(", "").Replace(")", "").Replace(" ", "");
            var compArr = colorStr.Split(',');
            var r = SafeParseInt(compArr[0], 1) / 255.0f;
            var g = SafeParseInt(compArr[1], 1) / 255.0f;
            var b = SafeParseInt(compArr[2], 1) / 255.0f;
            var a = 1.0f;
            if (compArr.Length > 3)
            {
                a = SafeParseInt(compArr[3], 1) / 255.0f;
            }

            var color = new Color(r, g, b, a);
            return color;
        }

        public static DayOfWeek ParseDayOfWeekFormString(string dayOfWeek)
        {
            return dayOfWeek switch
            {
                "mon" => DayOfWeek.Monday,
                "tue" => DayOfWeek.Tuesday,
                "wed" => DayOfWeek.Wednesday,
                "thu" => DayOfWeek.Thursday,
                "fri" => DayOfWeek.Friday,
                "sat" => DayOfWeek.Saturday,
                "sun" => DayOfWeek.Sunday,
                _ => throw new ArgumentException()
            };
        }

        public static List<DayOfWeek> ParseDaysOfWeekToArray(string days)
        {
            var result = new List<DayOfWeek>();
            days = days.Replace(" ", "");
            var parseStr = days;
            while (parseStr != "")
            {
                if (parseStr.Substring(0, 1) == ",") parseStr = parseStr.Substring(1);

                var tempStr = parseStr.IndexOf(",") != -1 ? parseStr.Substring(0, parseStr.IndexOf(",")) : parseStr;

                if (tempStr != "")
                {
                    result.Add(ParseDayOfWeekFormString(tempStr));
                    parseStr = parseStr.Replace(tempStr, "").Replace(",,", ",");
                }
            }

            return result;
        }
        
        public static int SafeParseInt(string row, int defaultValue = 0) => 
            int.TryParse(row, out var value) ? value : defaultValue;

        public static Color ParseHexColor(string colorString)
        {
            if (!colorString.StartsWith("#"))
                colorString = "#" + colorString;
            ColorUtility.TryParseHtmlString(colorString, out var tapeColor);
            return tapeColor;
        }
        public static List<float> FloatListFromString(string str)
        {
            var strList = StringToStringList(str);
            var floatList = strList.ConvertAll(s => ParseStringToFloat(s));
            return floatList;
        }
        
        public static Vector3 Vector3ByString(string vectorStr)
        {
            vectorStr = vectorStr.Replace(" ", "");
            var vector = Vector3.zero;
            if (!vectorStr.Contains("(")) 
                return vector;
            var tempString = vectorStr.Substring(vectorStr.IndexOf("("),
                vectorStr.IndexOf(",") - vectorStr.IndexOf("("));
            var x = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

            tempString = vectorStr.Substring(vectorStr.IndexOf(",") + 1).Substring(0,
                vectorStr.Substring(vectorStr.IndexOf(",") + 1).IndexOf(",") + 1);
            var y = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

            tempString = vectorStr.Substring(vectorStr.IndexOf(",") + 1)
                .Substring(vectorStr.Substring(vectorStr.IndexOf(",") + 1).IndexOf(",") + 1);
            var z = tempString.Replace(",", "").Replace(")", "").Replace(" ", "").Replace("(", "");

            vector = new Vector3(ParseStringToFloat(x), ParseStringToFloat(y), ParseStringToFloat(z));

            return vector;
        }
    }
}