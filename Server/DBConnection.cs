using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.Diagnostics.Utilities;

namespace Server
{
    public class DBConnection
    {
        private const string DefaultConnectionString = "Server=KJW\\SQLEXPRESS;Database=TCGGameDB;User Id=TCGGameSERVER;Password=123456789123456789;TrustServerCertificate=True";

        private readonly string _connectionString;


        public DBConnection() : this(DefaultConnectionString) { }

        public DBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<(bool,Exception?)> RunSql(string sql, Action<SqlDataReader> callBack = null, Dictionary<string, string> sqlParameters = null)
        {
            try
            {
                using (SqlConnection _connection = new SqlConnection(_connectionString))
                {
                    await _connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(sql, _connection))
                    {
                        if (sqlParameters != null)
                        {
                            foreach (var param in sqlParameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            callBack?.Invoke(reader);
                        }
                    }
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"{nameof(DBConnection)}.{nameof(RunSql)} exception : {ex}");

                return (false, ex);
            }
        }
    }
}
