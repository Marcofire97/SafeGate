using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SafeGate.Models;
using System.Reflection.Metadata.Ecma335;

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

        public Funzionario Login(string username, string password)
        {
            const string query = "SELECT * FROM Funzionario WHERE Username = @Username AND Password = @Password";
            using var db = CreateConnection();
            return db.QuerySingleOrDefault<Funzionario>(query, new { Username = username, Password = password });
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

        public List<PuntoControllo_Dazi> GetCheckPoints()
        {
            const string query = @"SELECT 
                PC.Id_Punto_Controllo,
                PC.Nome AS Punto_Controllo, 
                PC.Descrizione,
                PC.Tipo,
                ISNULL(SUM(C.Dazio_Doganale), 0) AS Totale_Dazi,
                ISNULL(
                AVG(CASE 
                WHEN CAST(C.Data_Ora_Inizio AS DATE) = CAST(GETDATE() AS DATE) 
                THEN DATEDIFF(MINUTE, C.Data_Ora_Inizio, C.Data_Ora_Fine)
                ELSE NULL
                END), 0) AS Durata_Media_Minuti
                FROM Punto_Controllo PC
                LEFT JOIN Effettuato_in EFIN ON PC.Id_Punto_Controllo = EFIN.Id_Punto_Controllo
                LEFT JOIN Controllo C ON EFIN.Id_Controllo = C.Id_Controllo
                GROUP BY PC.Id_Punto_Controllo, PC.Nome, PC.Descrizione, PC.Tipo;";

            using var db = CreateConnection();
            var result = db.Query<PuntoControllo_Dazi>(query).ToList();
            return result;
        }

        public int InsertCheckPoint(string nome, string descrizione, string tipo)
        {
            const string query = "INSERT INTO Punto_Controllo (Nome, Descrizione, Tipo) VALUES (@Nome, @Descrizione, @Tipo); SELECT SCOPE_IDENTITY();";

            using var db = CreateConnection();
            var id = db.ExecuteScalar<int>(query, new { Nome = nome, Descrizione = descrizione, Tipo = tipo });

            return id;
        }

        public bool UpdateCheckPoint(int id, string campo, string nuovovalore)
        {
            using var db = CreateConnection();
            string query = $"UPDATE Punto_Controllo SET {campo} = @nuovovalore WHERE id_Punto_Controllo = @id";
            var result = db.Execute(query, new { nuovovalore, id });
            return result > 0;
        }
        public bool DeleteCheckPoint(int id)
        {
            using var db = CreateConnection();
            string query = $"DELETE FROM Punto_Controllo WHERE Id_Punto_Controllo = @id";
            var result = db.Execute(query, new { id });
            return result > 0;
        }

        public List<Funzionario> GetOfficers()
        {
            const string query = @"
                SELECT *, 
                CASE 
                WHEN CAST(Data_Ora_Login AS DATE) = CAST((SYSDATETIMEOFFSET() AT TIME ZONE 'UTC' AT TIME ZONE 'Central European Standard Time') AS DATE) THEN 'SI' 
                ELSE 'NO' 
                END AS In_Servizio
                FROM Funzionario";

            using var db = CreateConnection();
            var result = db.Query<Funzionario>(query).ToList();
            return result;
        }

        public int InsertOfficer(string nome, string cognome, string username, string password, string ruolo)
        {
            const string query = @"
                INSERT INTO Funzionario (Nome, Cognome, Data_Ora_Login, Username, Password, Ruolo) 
                VALUES (@Nome, @Cognome, @Data_Ora_Login, @Username, @Password, @Ruolo); 
                SELECT SCOPE_IDENTITY();";

            using var db = CreateConnection();

            var id = db.ExecuteScalar<int>(query, new
            {
                Nome = nome,
                Cognome = cognome,
                Data_Ora_Login = DateTime.Now,
                Username = username,
                Password = password,
                Ruolo = ruolo
            });

            return id;
        }


        public bool UpdateOfficer(int id, string campo, string nuovovalore)
        {
            using var db = CreateConnection();
            string query = $"UPDATE Funzionario SET {campo} = @nuovovalore WHERE Id_Funzionario = @id";
            var result = db.Execute(query, new { nuovovalore, id });
            return result > 0;
        }

        public bool DeleteOfficer(int id)
        {
            using var db = CreateConnection();
            string query = $"DELETE FROM Funzionario WHERE Id_Funzionario = @id";
            var result = db.Execute(query, new { id });
            return result > 0;
        }

        public List<Addetto> GetAttendants()
        {
            const string query = @"
                SELECT 
                A.Id_Addetto,
                A.Nome, 
                A.Cognome, 
                A.Username, 
                A.Password, 
                A.Ruolo, 
                A.Data_Ora_Login, 
                A.Id_Funzionario,
                COALESCE(COUNT(E.Id_Esito), 0) AS Numero_Contestazioni
                FROM 
                Addetto A
                LEFT JOIN 
                Esegue Ese ON A.Id_Addetto = Ese.Id_Addetto
                LEFT JOIN 
                Esito E ON Ese.Id_Controllo = E.Id_Controllo AND E.Descrizione = 'Contestato'
                GROUP BY 
                A.Id_Addetto, A.Nome, A.Cognome, A.Username, A.Password, A.Ruolo, A.Data_Ora_Login, A.Id_Funzionario;";

            using var db = CreateConnection();
            var result = db.Query<Addetto>(query).ToList();
            return result;
        }


        public int InsertAttendant(string nome, string cognome, string username, string password, string ruolo, int idFunzionario)
        {
            const string query = @"
                INSERT INTO Addetto (Nome, Cognome, Data_Ora_Login, Username, Password, Ruolo, Id_Funzionario) 
                VALUES (@Nome, @Cognome, @Data_Ora_Login, @Username, @Password, @Ruolo, @Id_Funzionario); 
                SELECT SCOPE_IDENTITY();";

            using var db = CreateConnection();

            var id = db.ExecuteScalar<int>(query, new
            {
                Nome = nome,
                Cognome = cognome,
                Data_Ora_Login = DateTime.Now,
                Username = username,
                Password = password,
                Ruolo = ruolo,
                Id_Funzionario = idFunzionario
            });

            return id;
        }

        public bool UpdateAttendant(int id, string campo, string nuovovalore)
        {
            using var db = CreateConnection();
            string query = $"UPDATE Addetto SET {campo} = @nuovovalore WHERE Id_Addetto = @id";
            var result = db.Execute(query, new { nuovovalore, id });
            return result > 0;
        }

        public bool DeleteAttendant(int id)
        {
            using var db = CreateConnection();
            string query = $"DELETE FROM Addetto WHERE Id_Addetto = @id";
            var result = db.Execute(query, new { id });
            return result > 0;
        }

        public List<Categoria> GetCategories()
        {
            const string query = @"SELECT 
                Cat.Id_Categoria,
                Cat.Nome,
                Cat.Descrizione,
                COALESCE(SUM(CASE WHEN E.Descrizione = 'Respinto' THEN 1 ELSE 0 END), 0) AS Merci_Respinte
                FROM 
                Categoria Cat
                LEFT JOIN 
                Fa_Parte FP ON Cat.Id_Categoria = FP.Id_Categoria
                LEFT JOIN 
                Merce M ON FP.Id_Merce = M.Id_Merce
                LEFT JOIN 
                Controllo C ON M.Id_Controllo = C.Id_Controllo
                LEFT JOIN 
                Esito E ON C.Id_Controllo = E.Id_Controllo AND E.Descrizione = 'Respinto' AND YEAR(C.Data_Ora_Inizio) = YEAR(GETDATE())
                GROUP BY 
                Cat.Id_Categoria, Cat.Nome, Cat.Descrizione";


            using var db = CreateConnection();
            var result = db.Query<Categoria>(query).ToList();
            return result;
        }

        public int InsertCategory(string nome, string descrizione)
        {
            const string query = @"
                INSERT INTO Categoria (Nome, Descrizione) 
                VALUES (@Nome, @Descrizione); 
                SELECT SCOPE_IDENTITY();";

            using var db = CreateConnection();
            var id = db.ExecuteScalar<int>(query, new
            {
                Nome = nome,
                Descrizione = descrizione
            });

            return id;
        }

        public bool UpdateCategory(int id, string campo, string nuovovalore)
        {
            using var db = CreateConnection();
            string query = $"UPDATE Categoria SET {campo} = @nuovovalore WHERE Id_Categoria = @id";
            var result = db.Execute(query, new { nuovovalore, id });
            return result > 0;
        }


    }
}
