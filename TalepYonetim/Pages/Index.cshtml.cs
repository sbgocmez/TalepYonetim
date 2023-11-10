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

        //[BindProperty]
        //public int Id { get; set; }

        [HttpPost]
        public async Task<IActionResult> OnPost(int id)
        {
            var talep = await _db.Talepler.FindAsync(id);

            if (talep == null)
            {
                return NotFound();
            }

            _db.Talepler.Remove(talep);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

    }
}