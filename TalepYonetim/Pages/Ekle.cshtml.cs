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

        public AltKategori selectedAltKategori { get; set; } = new AltKategori();

        public Talep Talep { get; set; } = new Talep();

        public EkleModel(ApplicationDbContext db)
        {
            _db = db;
            AltKategoriler = _db.AltKategoriler;
            Kategoriler = _db.Kategoriler;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(Talep talep) 
        {
            // direkt altkategori donebilsem cok daha iyi olucak
            var alt = _db.AltKategoriler.First(alt => alt.Id == talep.AltKategoriId);
            talep.AltKategori = alt;
            
            await _db.Talepler.AddAsync(talep);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
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
