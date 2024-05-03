using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SharpStackConvert
{
    public static class CSVConverter
    {
        /// <summary>
        /// Converts a list of string arrays to a CSV format string.
        /// </summary>
        /// <param name="rows">The list of string arrays to convert.</param>
        /// <param name="splitter">The delimiter to use for separating values (default is comma).</param>
        /// <returns>A string in CSV format.</returns>
        public static string ToCSVFormat(this List<string[]> rows, string splitter = ",")
        {
            var returnValue = "";
            if (rows is not { Count: > 0 }) return returnValue;
            foreach (var r in rows)
            {
                for (var j = 0; j < r.Length; j++)
                {
                    if (j > 0)
                    {
                        returnValue += splitter;
                    }
                    returnValue += r[j];
                }
                returnValue += "\r\n";
            }
            return returnValue;
        }

        /// <summary>
        /// Converts a two-dimensional string array to a CSV format string.
        /// </summary>
        /// <param name="rows">The two-dimensional string array to convert.</param>
        /// <param name="splitter">The character used to separate values in the CSV format. Default is comma (,).</param>
        /// <returns>A string in CSV format.</returns>
        public static string ToCSVFormat(this string[,] rows, string splitter = ",")
        {
            var returnValue = "";
            if (rows is not { Length: > 0 }) return returnValue;
            for (var i = 0; i < rows.GetLength(0); i++)
            {
                for (var j = 0; j < rows.GetLength(1); j++)
                {
                    if (j > 0)
                    {
                        returnValue += splitter;
                    }
                    returnValue += rows[i, j];
                }
                returnValue += "\r\n";
            }
            return returnValue;
        }
        
        /// <summary>
        /// Converts a 2D array of strings to a CSV format string.
        /// </summary>
        /// <param name="rows">The 2D array of strings to convert.</param>
        /// <param name="Splitter">The character used to separate values in the CSV format. Default is ",".</param>
        /// <returns>A string in CSV format.</returns>
        public static string ToCSVFormat(this string[][] rows, string Splitter = ",")
        {
            var returnValue = "";
            if (rows is not { Length: > 0 }) return returnValue;
            foreach (var t in rows)
            {
                for (var j = 0; j < t.Length; j++)
                {
                    if (j > 0)
                    {
                        returnValue += Splitter;
                    }
                    returnValue += t[j];
                }
                returnValue += "\r\n";
            }
            return returnValue;
        }


        //This is an extension method for the string class that splits a string into a list of lines.The method takes two parameters:1. "input" - the string to be split into lines.
        //2. "SkipEmptyLines" - a boolean flag indicating whether empty lines should be skipped or included in the resulting list.
        //The method first creates an empty list called "Lines" to store the lines. If the input string is null or empty, it returns the empty list.
        //If the input string is not null or empty, the method determines the splitting options based on the value of "SkipEmptyLines". If "SkipEmptyLines" is true, it sets the splitting option to "RemoveEmptyEntries", which removes any empty lines from the resulting array. If "SkipEmptyLines" is false, it sets the splitting option to "None", which includes empty lines in the resulting array.
        //The method then creates a character array containing the line separators "\r\n". It uses this array to split the input string into an array of lines using the "Split" method, passing in the splitting options.
        //Finally, the method adds all the lines from the resulting array to the "Lines" list using the "AddRange" method, and returns the list.

        /// <summary>
        /// Splits a string into a list of lines, with the option to skip empty lines.
        /// </summary>
        /// <param name="input">The input string to split.</param>
        /// <param name="skipEmptyLines">A boolean indicating whether to skip empty lines or not.</param>
        /// <returns>A list of strings representing the lines of the input string.</returns>
        public static List<string> SplitLines(this string input, bool skipEmptyLines)
        {
            var lines = new List<string>();
            if (string.IsNullOrEmpty(input))
                return lines;

            var opt = skipEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            var splitters = "\r\n".ToCharArray();

            var arrLines = input.Split(splitters, opt);
            lines.AddRange(arrLines);
            return lines;
        }
        

        /// <summary>
        /// Converts a CSV string to a DataTable.
        /// </summary>
        /// <param name="CSVData">The CSV string to convert.</param>
        /// <param name="TableName">The name of the DataTable.</param>
        /// <param name="FirstLineIsHeader">Indicates whether the first line of the CSV data is a header.</param>
        /// <param name="ColumnSplitter">The character used to split columns in the CSV data.</param>
        /// <returns>The converted DataTable.</returns>
        public static DataTable CSVToTable(this string CSVData, string TableName, bool FirstLineIsHeader = true, char ColumnSplitter = ',')
        {
            var lines = CSVData.SplitLines(true);
            if (lines.Count <= 0) 
                return new DataTable(TableName);
            var headers = lines[0].Split(ColumnSplitter);
            var dt = new DataTable(TableName);
            if (FirstLineIsHeader)
            {
                foreach (var h in headers)
                {
                    dt.Columns.Add(h);
                }

                for (var i = 1; i < lines.Count; i++)
                {
                    dt.Rows.Add(lines[i]?.Split(ColumnSplitter) ?? Array.Empty<string>());
                }
            }
            else
            {
                for (var i = 0; i < headers.Length; i++)
                {
                    dt.Columns.Add("Col" + i);
                }

                foreach (var row in lines.Select(line => line.Split(ColumnSplitter)))
                {
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
    }
}
