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
			Talepler = _db.Talepler.Include(a => a.AltKategori).ThenInclude(b=>b.Kategori);
		}

        public void OnGetTalepler()
        {
            Talepler = _db.Talepler.Include(a => a.AltKategori).ThenInclude(b => b.Kategori);
        }

        //[BindProperty]
        //public int Id { get; set; }

        [HttpPost]
        public IActionResult OnPostTalepSil(int id)
        {
            var talep = _db.Talepler.Find(id);

            if (talep == null)
            {
                return NotFound();
            }

            _db.Talepler.Remove(talep);
            _db.SaveChanges();

            return new JsonResult(new { success = true, talepId = id });
        }

        [HttpPost]
        public IActionResult OnPostTalepOnayla(int id, int onay)
        {
            var talep = _db.Talepler.Find(id);

            if (talep == null)
            {
                return NotFound();
            }
            if (talep.Onaylandi != onay)
            {
                talep.Onaylandi = onay;
            }
            //talep.Onaylandi = talep.Onaylandi ^ onay;
            _db.SaveChanges();

            return new JsonResult(new { success = true, talepId = id });
        }

    }
}