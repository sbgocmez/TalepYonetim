using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
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

        [BindProperty]
        public Kategori aaaKategori { get; set; } = new Kategori();
        
        //[BindProperty]
        //public AltKategori duzenlenecekTalebinAltKategorisi = new AltKategori();

        public EkleModel(ApplicationDbContext db)
        {
            _db = db;
            AltKategoriler = _db.AltKategoriler;
            Kategoriler = _db.Kategoriler;
        }

        public IActionResult OnGet(int? id)
        {
            // Use the 'id' parameter as needed
            if (id.HasValue)
            {
                Talep = _db.Talepler.Find(id);
                var alt_kategori = _db.AltKategoriler.Include(a => a.Kategori).First(u => u.Id == Talep.AltKategoriId);
                var kategori = _db.Kategoriler.First(a => a.Id == alt_kategori.KategoriId);

                Talep.AltKategori = alt_kategori;
                Talep.AltKategori.Kategori = kategori;
            }

            // Your existing code
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            // direkt altkategori donebilsem cok daha iyi olucak suan id ile geliyor
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