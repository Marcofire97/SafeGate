namespace SafeGate.Models
{
    public class PuntoControllo_Dazi
    {
        public int Id_Punto_Controllo { get; set; }
        public string Punto_Controllo { get; set; }
        public string Descrizione { get; set; }
        public string Tipo { get; set; }
        public float Totale_Dazi { get; set; }
        public float Durata_Media_Minuti { get; set; }
    }
}
