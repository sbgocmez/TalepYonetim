using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalepYonetim.Model
{
    public class Talep
    {
        public int Id { get; set; }
        [ForeignKey("AltKategori")]
        [Required(ErrorMessage = "Talep alt kategori seçimi yapınız.")]
        public int AltKategoriId { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        [Required(ErrorMessage = "Adet giriniz.")]
        public int Adet {  get; set; }
        [Required(ErrorMessage = "Talep eden isim bilgisi giriniz.")]
        public string Edenİsim {  get; set; }
        [Required(ErrorMessage = "Talep eden soyisim bilgisi giriniz.")]
        public string EdenSoyisim { get; set; }
        public virtual AltKategori AltKategori { get; set; } = new AltKategori();
        public int Onaylandi { get; set; } = 0;

    }
}
