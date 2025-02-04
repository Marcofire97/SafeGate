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

        public Statistica GetStatistics()
        {
            const string query = @"SELECT 
                COUNT(DISTINCT C.Id_Controllo) AS numero_Controlli_Totali,
                COUNT(DISTINCT C.Id_Passeggero) AS numero_Passeggeri_Controllati,
                COUNT(E.Id_Esito) AS merci_Respinte_Sequestrate,
                COALESCE(SUM(C.Dazio_Doganale), 0) AS totale_Dazi_Riscossi,
                COUNT(DISTINCT M.Id_Merce) AS numero_Merci_Dichiarate,
                (SELECT COUNT(*) FROM Punto_Controllo) AS numero_Punti_Controllo
                FROM Controllo C
                LEFT JOIN Esito E ON C.Id_Controllo = E.Id_Controllo 
                AND E.Descrizione IN ('Respinto', 'Sequestrato')
                LEFT JOIN Merce M ON C.Id_Controllo = M.Id_Controllo;";

            using var db = CreateConnection();
            return db.QuerySingleOrDefault<Statistica>(query);
        }

    }
}
