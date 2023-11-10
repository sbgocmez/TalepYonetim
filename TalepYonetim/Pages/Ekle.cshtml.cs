using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
    public class EkleModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<AltKategori> AltKategoriler { get; set; }
        public IEnumerable<Kategori> Kategoriler { get; set; }
        public Talep Talep { get; set; } = new Talep();

        public EkleModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            AltKategoriler = _db.AltKategoriler;
            Kategoriler = _db.Kategoriler;
        }

        public IActionResult OnPost() 
        {
            AltKategoriler = _db.AltKategoriler;
            Kategoriler = _db.Kategoriler;
            return Page();
        }

        [HttpGet]
        public IActionResult OnGetAltKategoriler(int talepEdilenKategoriId)
        {
            // Bu kategoriye ait alt kategorileri getirme (tüm özellikleriyle)
            var subCategories = _db.AltKategoriler
                .Where(u => u.KategoriId == talepEdilenKategoriId)
                .ToList();

            return new JsonResult(subCategories);
        }

    }
}
