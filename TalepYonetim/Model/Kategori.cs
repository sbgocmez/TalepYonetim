using System.ComponentModel.DataAnnotations;

namespace TalepYonetim.Model
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<AltKategori> AltKategoriler { get; set; }
    }
}
