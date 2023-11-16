using Microsoft.EntityFrameworkCore;
using TalepYonetim.Model;

namespace TalepYonetim.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<AltKategori> AltKategoriler { get; set; }
        public DbSet<Talep> Talepler { get; set; }
        public DbSet<Basvuru> Basvurular { get; set;}


    }

}
