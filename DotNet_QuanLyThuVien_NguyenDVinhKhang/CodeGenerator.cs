using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public static class CodeGenerator
    {
        /// <summary>
        /// Generates the next code given a prefix and the current code.
        /// If currentCode is null or invalid, it will start at 001.
        /// </summary>
        public static string GenerateNextCode(string prefix, string currentCode)
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
        /// Queries the database to get the maximum code from a specified table and column,
        /// then returns the next code by incrementing it.
        /// </summary>
        /// <param name="connectionString">Connection string for the SQL Server.</param>
        /// <param name="tableName">Database table name.</param>
        /// <param name="codeColumn">The column that holds the code values (e.g., "MaSach").</param>
        /// <param name="prefix">The prefix for the code (e.g., "S").</param>
        public static string GetNextCodeFromDatabase(string connectionString, string tableName, string codeColumn, string prefix)
        {
            string query = $"SELECT MAX({codeColumn}) FROM {tableName}";
            string currentCode = null;

            SqlServerConnection db = new SqlServerConnection();
            object result = db.ExecuteScalar(query);
            if (result != DBNull.Value && result != null)
            {
                currentCode = Convert.ToString(result);
            }

            return GenerateNextCode(prefix, currentCode);
        }
    }
}
