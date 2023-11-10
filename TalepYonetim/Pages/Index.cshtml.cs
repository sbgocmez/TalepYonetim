using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Talep> Talepler { get; set; }
		public IEnumerable<AltKategori> AltKategoris { get; set; }
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public Kategori deneme {  get; set; }
		public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
			//         Kategori new_kategori = new Kategori();
			//         new_kategori.Name = "hizmet";
			//         _db.Add(new_kategori);
			//         _db.SaveChanges();
			//         //Kategoriler.Append(new_kategori);

			//Kategori new_kategori2 = new Kategori();
			//new_kategori2.Name = "elektronik";
			//         //Kategoriler.Append(new_kategori2);
			//         _db.Add(new_kategori2);
			//         _db.SaveChanges();

			//Kategoriler = _db.Kategoriler;

			//AltKategori new_alt = new AltKategori();
			//new_alt.Name = "Bilgisayar";
			//Kategori new_kat = new Kategori();
			//new_kat = Kategoriler.First(d => d.Name == "Kırtasiye");
			//Kategoriler.Append(new_kat);
			//new_alt.Kategori = _db.Kategoriler.First(d => d.Name == "Kırtasiye");


			//var kategori = new Kategori
			//{
			//	Name = "Category 1"
			//};

			//var altKategori = new AltKategori
			//{
			//	Name = "Subcategory 1",
			//	Kategori = kategori
			//};

			//_db.Kategoriler.Add(kategori);
			//_db.AltKategoriler.Add(altKategori);

			//_db.SaveChanges();

			Talepler = _db.Talepler.Include(a => a.AltKategori).ThenInclude(b=>b.Kategori);

			//var newaltkate = new AltKategori { Name = "Mouse" };
			//         var kategori = _db.Kategoriler.Find(1);
			//         kategori.AltKategoriler.Add(newaltkate);

			//         _db.SaveChanges();

			//_db.Add(new_alt);
			//_db.SaveChanges();
			//AltKategori new_alt = new AltKategori();
			//new_alt.Name = "Organizer";



			//AltKategoris = _db.AltKategoriler.Include(a => a.Kategori); 
		}

    }
}