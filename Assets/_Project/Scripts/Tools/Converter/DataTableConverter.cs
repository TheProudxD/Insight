using System.Collections.Generic;
using System.Data;

namespace SharpStackConvert
{
    public static class DataTableConverter
    {
        /// <summary>
        /// Converts a jagged array of strings into a DataTable.
        /// </summary>
        /// <param name="rows">The jagged array of strings to convert.</param>
        /// <returns>A DataTable containing the converted data.</returns>
        public static DataTable ToTable(this string[][] rows)
        {
            var dt = new DataTable("Tbl");

            if (rows.Length <= 0) return dt;
            for (var i = 0; i < rows[0].Length; i++)
            {
                dt.Columns.Add("Col" + i.ToString());
            }

            foreach (var row in rows)
            {
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Converts a two-dimensional string array to a DataTable.
        /// </summary>
        /// <param name="rows">The two-dimensional string array to convert.</param>
        /// <returns>A DataTable containing the data from the two-dimensional string array.</returns>
        public static DataTable ToTable(this string[,] rows)
        {
            var dt = new DataTable("Tbl");

            if (rows.Length <= 0) return dt;
            for (var i = 0; i < rows.GetLength(1); i++)
            {
                dt.Columns.Add("Col" + i.ToString());
            }

            for (var i = 0; i < rows.GetLength(0); i++)
            {
                var row = new string[rows.GetLength(1)];
                for (var j = 0; j < rows.GetLength(1); j++)
                {
                    row[j] = rows[i, j];
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Converts an array of strings into a DataTable.
        /// </summary>
        /// <param name="rows">The array of strings to convert.</param>
        /// <returns>A DataTable containing the converted data.</returns>
        public static DataTable ToTable(this string[] rows)
        {
            var dt = new DataTable("Tbl");

            if (rows.Length > 0)
            {
                for (var i = 0; i < rows.Length; i++)
                {
                    dt.Columns.Add("Col" + i.ToString());
                }

                dt.Rows.Add(rows);
            }

            return dt;
        }

        /// <summary>
        /// Converts an array of strings into a DataTable with the specified table name.
        /// </summary>
        /// <param name="rows">The array of strings to convert.</param>
        /// <param name="tableName">The name of the DataTable.</param>
        /// <returns>The converted DataTable.</returns>
        public static DataTable ToTable(this string[] rows, string tableName)
        {
            var dt = new DataTable(tableName);

            if (rows.Length > 0)
            {
                for (var i = 0; i < rows.Length; i++)
                {
                    dt.Columns.Add("Col" + i.ToString());
                }

                dt.Rows.Add(rows);
            }

            return dt;
        }

        /// <summary>
        /// Converts a two-dimensional string array into a DataTable.
        /// </summary>
        /// <param name="rows">The two-dimensional string array to convert.</param>
        /// <param name="FirstRowIsHeader">A boolean value indicating whether the first row of the array should be treated as the header row of the DataTable.</param>
        /// <returns>A DataTable containing the data from the string array.</returns>
        public static DataTable ToTable(this string[,] rows, bool FirstRowIsHeader)
        {
            var dt = new DataTable("Tbl");

            if (rows.Length > 0)
            {
                if (FirstRowIsHeader)
                {
                    for (var i = 0; i < rows.GetLength(1); i++)
                    {
                        dt.Columns.Add(rows[0, i]);
                    }

                    for (var i = 1; i < rows.GetLength(0); i++)
                    {
                        var row = new string[rows.GetLength(1)];
                        for (var j = 0; j < rows.GetLength(1); j++)
                        {
                            row[j] = rows[i, j];
                        }

                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    for (var i = 0; i < rows.GetLength(1); i++)
                    {
                        dt.Columns.Add("Col" + i.ToString());
                    }

                    for (var i = 0; i < rows.GetLength(0); i++)
                    {
                        var row = new string[rows.GetLength(1)];
                        for (var j = 0; j < rows.GetLength(1); j++)
                        {
                            row[j] = rows[i, j];
                        }

                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }


        /// <summary>
        /// Converts a list of string arrays into a DataTable.
        /// </summary>
        /// <param name="rows">The list of string arrays to convert.</param>
        /// <param name="FirstRowIsHeader">Indicates whether the first row should be treated as the header row.</param>
        /// <param name="tableName">The name of the resulting DataTable.</param>
        /// <returns>The converted DataTable.</returns>
        public static DataTable ToTable(this List<string[]> rows, bool FirstRowIsHeader = true,
            string tableName = "Tbl")
        {
            var dt = new DataTable(tableName);

            if (rows is not { Count: > 0 }) return dt;
            if (FirstRowIsHeader)
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    dt.Columns.Add(rows[0][i]);
                }

                for (var i = 1; i < rows.Count; i++)
                {
                    var row = new string[rows[0].Length];
                    for (var j = 0; j < rows[0].Length; j++)
                    {
                        row[j] = rows[i][j];
                    }

                    dt.Rows.Add(row);
                }
            }
            else
            {
                for (var i = 0; i < rows[0].Length; i++)
                {
                    dt.Columns.Add("Col" + i.ToString());
                }

                foreach (var row in rows)
                {
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }
    }
}