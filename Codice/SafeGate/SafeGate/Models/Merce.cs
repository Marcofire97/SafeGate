namespace SafeGate.Models
{
    public class Merce
    {
        public int Id_Merce { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int Quantità { get; set; }
        public int Id_Controllo { get; set; }
    }
}
