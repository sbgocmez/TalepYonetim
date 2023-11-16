using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalepYonetim.Data;
using TalepYonetim.Model;
using ClosedXML.Excel;

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
                {0,"�n de�erleme"}, {1, "Amir onay�nda"}, {2, "Amir onay� al�nd�"}
            };

            BasvuruDurumDict = new Dictionary<int, string>
            {
                {0,"Ba�vuru bekleniyor"}, {1, "Ba�vuru onayland�"}, {2, "Ba�vuru reddedildi"}, {3, "KPSS puan s�ralamas�na giremedi"}
            };

        }
        // ana sayfa talepleri goster
        public void OnGet()
        {
            Basvurular = _db.Basvurular;
        }

        public FileResult OnGetExportExcel()
        {
            var data = _db.Basvurular;
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("BekleyenBa�vurular");
                worksheet.Cell(1, 1).Value = "Ba�vuru S�ra No";
                worksheet.Cell(1, 2).Value = "TCKN";
                worksheet.Cell(1, 3).Value = "Ad";
                worksheet.Cell(1, 4).Value = "Soyad";
                worksheet.Cell(1, 5).Value = "Durum";
                worksheet.Cell(1, 6).Value = "Lise Mezuniyet Beyan� Var/Yok";
                worksheet.Cell(1, 7).Value = "�niversite Mezuniyet Beyan� Var/Yok";
                worksheet.Cell(1, 8).Value = "Yabanc� Dil S�nav Sonucu Beyan� Var/Yok";
                worksheet.Cell(1, 9).Value = "�n Kontrol Durumu";
                worksheet.Cell(1, 10).Value = "�n Kontrol �ptal A��klamas�";
                worksheet.Cell(1, 11).Value = "Onay Durumu";
                worksheet.Cell(1, 12).Value = "�ptal A��klamas�";

                //IXLRange range = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, 12).Address);
                //range.Style.Fill.SetBackgroundColor(XLColor.Almond);
                //IXLRange range = worksheet.Range(1, 1, 1 + 1, 12);

                var totalEntryNumber = data.Count();
                totalEntryNumber++;

                IXLRange range = worksheet.Range($"A1:L{totalEntryNumber}");
                int index = 1;
                var excelTable = range.CreateTable();
                excelTable.Theme = XLTableTheme.TableStyleMedium2;

                excelTable.ShowAutoFilter = false;

                foreach (var item in data)
                {
                    index++;
                    worksheet.Cell(index, 1).Value = item.SiraNo;
                    worksheet.Cell(index, 2).Value = item.TCIdentity;
                    //worksheet.Cell(index, 2).RichText.Add(item.TCIdentity);
                    //worksheet.Cell(index, 2).DataType = XLDataType.Text;
                    worksheet.Cell(index, 3).Value = item.Ad;
                    worksheet.Cell(index, 4).Value = item.Soyad;
                    worksheet.Cell(index, 5).Value = item.BasvuruDurum;
                    worksheet.Cell(index, 6).Value = item.LiseMezuniyet;
                    worksheet.Cell(index, 7).Value = item.UniversiteMezuniyet;
                    worksheet.Cell(index, 8).Value = item.YabanciDil;
                    worksheet.Cell(index, 9).Value = item.OnKontrolDurumu;
                    worksheet.Cell(index, 10).Value = item.OnKontrolIptalAciklamasi;
                    worksheet.Cell(index, 11).Value = item.OnayDurumu;
                    worksheet.Cell(index, 12).Value = item.IptalAciklamasi;

                }

                

                IXLRange textRange = worksheet.Range("R1:W16");
                textRange.Merge();
                textRange.Value = "1. �ptal edilmek �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 0 giriniz.\r\n2.Onaylanmak �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 1 giriniz.\r\n3.Aday�n ba�vuru durumunu 'KPSS Puan S�ralamas�na Giremedi'olarak �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 2 giriniz.\r\n4.�ptal edilmek �zere onaya g�nderilen ba�vurular�n iptal nedenini '�ptal A��klamas�' s�tununa giriniz. Girilen iptal nedeni adaya g�sterilecektir.�ptal nedeni 450 karakterden k�sa olmal�d�r.\r\n";

                textRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                textRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                textRange.Style.Font.FontColor = XLColor.Red;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    //var strDate = DateTime.Now.ToString("h");
                    string fileName = string.Format("BekleyenBa�vurular.xlsx");
                    return File(content, contentType, fileName);
                }

            }
        }

    }
}