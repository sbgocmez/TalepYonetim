using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalepYonetim.Model
{
    public class AltKategori
    {
        public int Id { get; set; }
        
        public int KategoriId { get; set; }
        public string Name { get; set; }
		[ForeignKey(nameof(KategoriId))]
		public virtual Kategori Kategori { get; set; } = null!;
        public virtual ICollection<Talep> Talepler{ get; set; }
    }
}
