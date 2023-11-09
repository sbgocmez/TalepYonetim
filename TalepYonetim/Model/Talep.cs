namespace TalepYonetim.Model
{
    public class Talep
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public int Adet {  get; set; }
        public string Edenİsim {  get; set; }
        public string EdenSoyisim { get; set; }
        public AltKategori AltKategori { get; set; }

    }
}
