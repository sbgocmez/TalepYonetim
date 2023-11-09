using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages.Talepler
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Talep>  Talepler { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Talepler = _db.Talepler;

        }
    }
}
