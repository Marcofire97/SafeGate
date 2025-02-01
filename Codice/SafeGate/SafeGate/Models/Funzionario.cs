namespace SafeGate.Models
{
    public class Funzionario
    {
        public int Id_Funzionario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public DateTime Data_Ora_Login { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Ruolo { get; set; } = string.Empty;
    }
}
