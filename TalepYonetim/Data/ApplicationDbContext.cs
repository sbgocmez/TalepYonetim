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

  //      protected override void OnModelCreating(ModelBuilder modelBuilder)
  //      {
  //          // one to many relationships 
  //          modelBuilder.Entity<AltKategori>()
  //              .HasOne(a => a.Kategori)
  //              .WithMany(k => k.AltKategoriler)
  //              .HasForeignKey(a => a.KategoriId)
  //              .IsRequired();
		//	modelBuilder.Entity<Talep>()
		//	   .HasOne(t => t.AltKategori)
		//	   .WithMany(a => a.Talepler)
		//	   .HasForeignKey(t => t.AltKategoriId)
  //             .IsRequired();

		//}

    }

}
