namespace WorldBankPaises.Model
{
    public class Pais
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Regiao { get; set; }
        public string Capital { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Complementado { get; set; }
        public double GDPPerCapita { get; set; }
        public double ImpostosPercentual { get; set; }
    }
}
