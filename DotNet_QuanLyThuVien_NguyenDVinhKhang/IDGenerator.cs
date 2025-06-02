using DotNet_QuanLyThuVien_NguyenDVinhKhang;
using System;
using System.Data;

public static class IDGenerator
{
    /// <summary>
    /// Generates the next code given a prefix and the current code.
    /// If currentCode is null or invalid, it will start at 001.
    /// </summary>
    public static string IDNextCode(string prefix, string currentCode)
    {
        if (string.IsNullOrEmpty(currentCode) || !currentCode.StartsWith(prefix))
        {
            return prefix + "001";
        }

        string numericPart = currentCode.Substring(prefix.Length);
        if (!int.TryParse(numericPart, out int number))
        {
            number = 0;
        }

        number++;
        return prefix + number.ToString("D3");
    }

    /// <summary>
    /// Queries the database to get the maximum code with the specified prefix from a table and column,
    /// then returns the next code by incrementing it.
    /// </summary>
    /// <param name="connectionString">Connection string for the SQL Server.</param>
    /// <param name="tableName">Database table name.</param>
    /// <param name="codeColumn">The column that holds the code values (e.g., "MaSach").</param>
    /// <param name="prefix">The prefix for the code (e.g., "S").</param>

    public static string GetNextIDFromDatabase(string connectionString, string tableName, string codeColumn, string prefix)
    {
        string query = $"SELECT MAX({codeColumn}) FROM {tableName} WHERE {codeColumn} LIKE '{prefix}%'";
        string currentCode = null;

        SqlServerConnection db = new SqlServerConnection();
        DataTable dt = db.ExecuteQuery(query);

        if (dt != null && dt.Rows.Count > 0)
        {
            object result = dt.Rows[0][0];
            if (result != DBNull.Value && result != null)
            {
                currentCode = Convert.ToString(result);
            }
        }

        return IDNextCode(prefix, currentCode);
    }

}