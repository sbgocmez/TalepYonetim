using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using TalepYonetim.Data;
using TalepYonetim.Model;

namespace TalepYonetim.Pages
{
    public class BasvuruIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Basvuru> Basvurular { get; set; }
        public Dictionary<int, string> BeyanDict { get; set; }
        public Dictionary<int, string> OnKontrolDurumDict { get; set; }
        public Dictionary<int, string> BasvuruDurumDict { get; set; }

        public BasvuruIndexModel(ApplicationDbContext db)
        {
            _db = db;

            BeyanDict = new Dictionary<int, string>
            { 
                { 0, "Yok" }, { 1, "Var" } 
            };

            OnKontrolDurumDict = new Dictionary<int, string>
            {
                {0,"Ön deðerleme"}, {1, "Amir onayýnda"}, {2, "Amir onayý alýndý"}
            };

            BasvuruDurumDict = new Dictionary<int, string>
            {
                {0,"Baþvuru bekleniyor"}, {1, "Baþvuru onaylandý"}, {2, "Baþvuru reddedildi"}, {3, "KPSS puan sýralamasýna giremedi"}
            };

        }
        // ana sayfa talepleri goster
        public void OnGet()
        {
            Basvurular = _db.Basvurular;
        }

    }
}