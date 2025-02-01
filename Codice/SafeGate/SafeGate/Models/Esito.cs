namespace SafeGate.Models
{
    public class Esito
    {
        public int Id_Esito { get; set; }
        public string Descrizione { get; set; } = string.Empty;
        public int Id_Controllo { get; set; }
    }
}
