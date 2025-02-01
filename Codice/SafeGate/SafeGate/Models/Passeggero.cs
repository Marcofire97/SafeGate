namespace SafeGate.Models
{
    public class Passeggero
    {
        public int Id_Passeggero { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Nazionalità { get; set; } = string.Empty;
        public string Tipo_Identificativo { get; set; } = string.Empty;
        public string N_Identificativo { get; set; } = string.Empty;
        public string A_Partenza { get; set; } = string.Empty;
        public string A_Destinazione { get; set; } = string.Empty;
        public string Motivo_Viaggio { get; set; } = string.Empty;
    }
}
