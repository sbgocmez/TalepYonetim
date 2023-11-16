using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalepYonetim.Model
{
    public class Basvuru
    {
        public int Id { get; set; }
        [Required]
        public string SiraNo { get; set; } = string.Empty;
        [Required]
        public int TCIdentity { get; set; }
        public string Ad { get; set; } = "";
        public string Soyad { get; set; } = "";
        public int BasvuruDurum { get; set; } = 0;
        // 0=Basvuru Bekliyor
        // 1=KPSS Puan Siralamasina Giremedi
        // 2=Basvuru Onaylandi
        // 3=Basvuru Reddedildi
        public int LiseMezuniyet { get; set; } = 0;
        // 0=Yok 1=Var
        public int UniversiteMezuniyet { get; set; } = 0;
        // 0=Yok 1=Var
        public int YabanciDil { get; set; } = 0;
        // 0=Yok 1=Var

        public int OnKontrolDurumu { get; set; } = 0;
        // 0=On degerleme
        // 1=Amir onayina gonderildi
        // 2=Amir onayindan geldi
        public int OnayDurumu { get; set; } = 3;
        public string OnKontrolIptalAciklamasi { get; set; } = "";
        // eger Onay durumu 0 secildiyse max 450 karakter girilecek.
        // eger onay durumu 1 secildiyse bos olacak
        // eger onay durumu 2 secildiyse "Kpss puani yetersiz" default.
        public string IptalAciklamasi { get; set; } = "";
        // amir onayindan geldikten sonra burasi dolacak.
        // iptal istegi onaylandiysa on kontrol iptal aciklamasi yazacak
        // iptal istegi onaylanmadiysa bos
        // onay istegi onaylandiysa bos
        // onay istegi onaylanmadiysa amirden gelen iptal aciklamasi yazacak.


    }
}
