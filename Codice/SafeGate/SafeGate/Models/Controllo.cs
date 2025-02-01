namespace SafeGate.Models
{
    public class Controllo
    {
        public int Id_Controllo { get; set; }
        public DateTime Data_Ora_Inizio { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Data_Ora_Fine { get; set; }
        public float Dazio_Doganale { get; set; }
        public int Id_Funzionario { get; set; }
        public int Id_Passeggero { get; set; }
    }
}
