using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
	public static class TypeParser
	{
        private const char _inCellSeporator = ';';

        private static readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>()
        {
            {"white", Color.white},
            {"black", Color.black},
            {"yellow", Color.yellow},
            {"red", Color.red},
            {"green", Color.green},
            {"blue", Color.blue},
        };

        public static Color ParseColor(string color)
        {
            color = color.Trim();
            Color result = default;
            return _colors.TryGetValue(color, out var c) ? c : result;
        }

        public static Vector3 ParseVector3(string s)
        {
            string[] vectorComponents = s.Split(_inCellSeporator);
            if (vectorComponents.Length < 3)
            {
                Debug.LogError("Can't parse Vector3. Wrong text format");
                return default;
            }

            float x = ParseFloat(vectorComponents[0]);
            float y = ParseFloat(vectorComponents[1]);
            float z = ParseFloat(vectorComponents[2]);
            return new Vector3(x, y, z);
        }

        public static int ParseInt(string s)
        {
            int result = -1;
            if (!int.TryParse(s, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.LogError("Can't parse int, wrong text: " + s);
            }

            return result;
        }

        public static float ParseFloat(string s)
        {
            float result = -1;
            if (!float.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
            {
                Debug.LogError("Can't pars float,wrong text: " +s);
            }

            return result;
        }
    }
}
