using System.ComponentModel.DataAnnotations;

namespace TalepYonetim.Model
{
    public class Kategori
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<AltKategori> AltKategoriler { get; set; } = new List<AltKategori>();
    }
}
