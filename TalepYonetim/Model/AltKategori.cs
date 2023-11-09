namespace TalepYonetim.Model
{
    public class AltKategori
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Kategori Kategori { get; set; }
        public ICollection<Talep> Talepler{ get; set; }
    }
}
