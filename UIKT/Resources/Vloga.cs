namespace UIKT.Resources
{
    public class Vloga
    {
        public string ime { get; set; }
        public string priimek { get; set; }
        public string emso { get; set; }
        public string dst { get; set; }
        public string iban { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }

        public Vloga() { }

        public Vloga(string ime, string priimek, string emso, string dst, string iban, string naziv, string opis)
        {
            this.ime = ime;
            this.priimek = priimek;
            this.emso = emso;
            this.dst = dst;
            this.iban = iban;
            this.naziv = naziv;
            this.opis = opis;
        }
    }
}
