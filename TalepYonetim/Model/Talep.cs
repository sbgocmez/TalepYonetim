using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalepYonetim.Model
{
    public class Talep
    {
        public int Id { get; set; }
        [ForeignKey("AltKategori")]
        public int AltKategoriId { get; set; }
        public string Aciklama { get; set; }
        public int Adet {  get; set; }
        public string Edenİsim {  get; set; }
        public string EdenSoyisim { get; set; }
        public virtual AltKategori AltKategori { get; set; } = new AltKategori();

    }
}
