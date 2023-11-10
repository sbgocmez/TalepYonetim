using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalepYonetim.Model
{
    public class AltKategori
    {
        public int Id { get; set; }
        
        //public int KategoriId { get; set; }
        public string Name { get; set; }
		[ForeignKey(nameof(Kategori))]
        public int KategoriId { get; set; }
		public virtual Kategori Kategori { get; set; } = new Kategori ();
        public virtual ICollection<Talep> Talepler{ get; set; } = new List<Talep> ();
    }
}
