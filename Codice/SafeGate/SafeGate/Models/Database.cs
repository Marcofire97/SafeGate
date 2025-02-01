using Dapper;
using Microsoft.Data.SqlClient;
using SafeGate.Models;

namespace SafeGate.Models
{
    public class Database
    {
        private readonly string _conn;

        public Database()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _conn = configuration.GetConnectionString("Default");
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_conn);
        }

        public Funzionario Login(string username, string passwordHash)
        {
            const string query = "SELECT * FROM Funzionario WHERE Username = @Username AND Password = @PasswordHash";
            using var db = CreateConnection();
            return db.QuerySingleOrDefault<Funzionario>(query, new { Username = username, PasswordHash = passwordHash });
        }

        public bool ValidazioneFunzionario(string username, string password)
        {
            const string query = "SELECT 1 FROM Funzionario WHERE Username = @Username AND Password = @Password";
            using var db = CreateConnection();
            return db.QueryFirstOrDefault<int>(query, new { Username = username, Password = password }) == 1;
        }
    }
}
