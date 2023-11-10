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
				Aciklama = "Bilgisayar�ma y�klenmelidir",
				Adet = 1,
				Eden�sim = "Kayra",
				EdenSoyisim = "Boyac�",
				AltKategori = bilg
			};

		}

	}
}