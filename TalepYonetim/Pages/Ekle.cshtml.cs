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

        [BindProperty]
        public Talep Talep { get; set; } = new Talep();
        [BindProperty]
        public AltKategori AltKategori { get; set; } = new AltKategori();

        public EkleModel(ApplicationDbContext db)
        {
            _db = db;
            AltKategoriler = _db.AltKategoriler;
            Kategoriler = _db.Kategoriler;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // direkt altkategori donebilsem cok daha iyi olucak
            var alt = _db.AltKategoriler.First(alt => alt.Id == Talep.AltKategoriId);
            Talep.AltKategori = alt;

            await _db.Talepler.AddAsync(Talep);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

        [HttpGet]
        public IActionResult OnGetAltKategoriler(int talepEdilenKategoriId)
        {
            var altKategoriler = _db.AltKategoriler
                .Where(u => u.KategoriId == talepEdilenKategoriId)
                .ToList();
            return new JsonResult(altKategoriler);
        }

    }
}