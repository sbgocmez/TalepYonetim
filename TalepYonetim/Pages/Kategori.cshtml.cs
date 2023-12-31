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
			var bilg = _db.AltKategoriler.First(u => u.Name == "Visual Studio");


			var talep = new Talep
			{
				Aciklama = "Bilgisayarıma yüklenmelidir",
				Adet = 1,
				Edenİsim = "Kayra",
				EdenSoyisim = "Boyacı",
				AltKategori = bilg
			};

			//_db.Add(talep);
			//_db.SaveChanges();

		}

	}
}