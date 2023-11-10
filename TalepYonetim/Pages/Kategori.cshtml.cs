using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
	public class KategoriModel : PageModel
	{
		private readonly ApplicationDbContext _db;
		public IEnumerable<AltKategori> AltKategoriler { get; set; }
		public IEnumerable<Kategori> Kategoriler { get; set; }
		public IEnumerable<Talep> Talepler {  get; set; } 
		public KategoriModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet()
		{
			var python = _db.AltKategoriler.First(u => u.Name == "Python");


			var talep = new Talep
			{
				Aciklama = "Stajyer bilgisayarlarýna yüklenmelidir",
				Adet = 2,
				EdenÝsim = "Erdem",
				EdenSoyisim = "Þimþek",
				AltKategori = python
			};

			//_db.Talepler.Add(talep);
			//_db.SaveChanges();
			//var kirtasiye = new Kategori
			//{
			//	Name = "Kýrtasiye"
			//};
			//var yazilim = new Kategori
			//{
			//	Name = "Yazilim"
			//};
			//var elektronik = new Kategori
			//{
			//	Name = "Elektronik"
			//};
			//var retval_k = _db.Kategoriler.Add(kirtasiye);
			//var retval_y = _db.Kategoriler.Add(yazilim);
			//var retval_e = _db.Kategoriler.Add(elektronik);

			//_db.SaveChanges();

			//var kirt = _db.Kategoriler.First(u => u.Name == "Kýrtasiye");


			//var m2 = new AltKategori
			//{
			//	Name = "Makas",
			//	Kategori = kirt
			//};
			//var organizer = new AltKategori
			//{
			//	Name = "Organizer",
			//	Kategori = kirt
			//};
			//var dosya = new AltKategori
			//{
			//	Name = "Dosya",
			//	Kategori = kirt
			//};


			//var elektronik = _db.Kategoriler.First(u => u.Name == "Elektronik");
			//var bilgisayar = new AltKategori
			//{
			//	Name = "Bilgisayar",
			//	Kategori = elektronik
			//};
			//var mouse = new AltKategori
			//{
			//	Name = "Mouse",
			//	Kategori = elektronik
			//};
			//var klavye = new AltKategori
			//{
			//	Name = "Klavye",
			//	Kategori = elektronik
			//};

			//var yazilim = _db.Kategoriler.First(u => u.Name == "Yazilim");

			//var python = new AltKategori
			//{
			//	Name = "Python",
			//	Kategori = yazilim
			//};
			//var vs = new AltKategori
			//{
			//	Name = "Visual Studio",
			//	Kategori = yazilim
			//};
			//var sqlserver = new AltKategori
			//{
			//	Name = "MS SQL Server",
			//	Kategori = yazilim
			//};


			//_db.AltKategoriler.Add(bilgisayar);
			//_db.AltKategoriler.Add(mouse);
			//_db.AltKategoriler.Add(klavye);
			//_db.AltKategoriler.Add(sqlserver);
			//_db.AltKategoriler.Add(python);
			//_db.AltKategoriler.Add(vs);
			//_db.AltKategoriler.Add(makas);
			//_db.AltKategoriler.Add(dosya);
			//_db.AltKategoriler.Add(organizer);

			_db.SaveChanges();

			//Kategoriler = _db.Kategoriler.Include(k=>k.AltKategoriler).ThenInclude(k=> k.Name);
			Talepler = _db.Talepler.Include(a => a.AltKategori);
			AltKategoriler = _db.AltKategoriler.Include(a => a.Kategori);
			foreach (var altKategori in AltKategoriler)
			{
				Console.WriteLine($"AltKategori Name: {altKategori.Name}");
				Console.WriteLine($"Kategori Name: {altKategori.Kategori?.Name}");
				Console.WriteLine();
			}

			//AltKategoris = _db.AltKategoriler.Include(a => a.Kategori); 
		}

	}
}