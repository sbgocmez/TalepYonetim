using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
    public class BasvuruEkleModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Basvuru Basvuru { get; set; }
        public IEnumerable<Basvuru> Basvurular { get; set; }
        public Dictionary<int, string> BeyanDict { get; set; }
        public Dictionary<int, string> OnKontrolDurumDict { get; set; }
        public Dictionary<int, string> BasvuruDurumDict { get; set; }

        public BasvuruEkleModel(ApplicationDbContext db)
        {
            _db = db;
            Basvurular = _db.Basvurular;

            BeyanDict = new Dictionary<int, string>
            {{ 0, "Yok" }, { 1, "Var" }};

            OnKontrolDurumDict = new Dictionary<int, string>
            {{0,"Ön deðerleme"}, {1, "Amir onayýnda"}, {2, "Amir onayý alýndý"}};

            BasvuruDurumDict = new Dictionary<int, string>
            {{0,"Baþvuru bekleniyor"}, {1, "Baþvuru onaylandý"}, {2, "Baþvuru reddedildi"}, {3, "KPSS puan sýralamasýna giremedi"}};
        }

        public IActionResult OnGet(int? id)
        {
            // Use the 'id' parameter as needed
            if (id.HasValue)
            {
                Basvuru = _db.Basvurular.Find(id);
            }

            // Your existing code
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (Basvuru.Id != 0)
            {
                var basvuru = _db.Basvurular.Find(Basvuru.Id);
                basvuru.SiraNo = Basvuru.SiraNo;
                basvuru.TCIdentity = Basvuru.TCIdentity;
                basvuru.Ad = Basvuru.Ad;
                basvuru.Soyad = Basvuru.Soyad;
                basvuru.BasvuruDurum = Basvuru.BasvuruDurum;
                basvuru.LiseMezuniyet = Basvuru.LiseMezuniyet;
                basvuru.UniversiteMezuniyet = Basvuru.UniversiteMezuniyet;
                basvuru.YabanciDil = Basvuru.YabanciDil;
                basvuru.OnKontrolDurumu = Basvuru.OnKontrolDurumu;
                basvuru.OnayDurumu = Basvuru.OnayDurumu;
                basvuru.OnKontrolIptalAciklamasi = Basvuru.OnKontrolIptalAciklamasi;
                basvuru.IptalAciklamasi = Basvuru.IptalAciklamasi;

                _db.SaveChanges();
                return RedirectToPage("BasvuruIndex");

            }

            await _db.Basvurular.AddAsync(Basvuru);
            await _db.SaveChangesAsync();
            return RedirectToPage("BasvuruIndex");
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