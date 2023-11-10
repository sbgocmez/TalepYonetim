// Delete.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public int Id { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            return Page();
        }

        [HttpPost]
        public IActionResult OnPostDeleteTalep(int id)
        {
            var talep = _db.Talepler.Find(id);

            if (talep == null)
            {
                return NotFound();
            }

            _db.Talepler.Remove(talep);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
