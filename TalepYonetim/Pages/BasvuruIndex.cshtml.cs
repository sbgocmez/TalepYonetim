using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalepYonetim.Data;
using TalepYonetim.Model;
using ClosedXML.Excel;
using System;

namespace TalepYonetim.Pages
{
    public class BasvuruIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Basvuru> Basvurular { get; set; }
        public Dictionary<int, string> BeyanDict { get; set; }
        public Dictionary<int, string> OnKontrolDurumDict { get; set; }
        public Dictionary<int, string> BasvuruDurumDict { get; set; }

        public XLWorkbook workbook = new XLWorkbook();
        public IXLWorksheet worksheet;
        public IXLRange range;

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

        public void InitializeWorksheet()
        {
            worksheet = workbook.Worksheets.Add("BekleyenBaþvurular");
            worksheet.Cell(1, 1).Value = "Baþvuru Sýra No";
            worksheet.Cell(1, 2).Value = "TCKN";
            worksheet.Cell(1, 3).Value = "Ad";
            worksheet.Cell(1, 4).Value = "Soyad";
            worksheet.Cell(1, 5).Value = "Durum";
            worksheet.Cell(1, 6).Value = "Lise Mezuniyet Beyaný Var/Yok";
            worksheet.Cell(1, 7).Value = "Üniversite Mezuniyet Beyaný Var/Yok";
            worksheet.Cell(1, 8).Value = "Yabancý Dil Sýnav Sonucu Beyaný Var/Yok";
            worksheet.Cell(1, 9).Value = "Ön Kontrol Durumu";
            worksheet.Cell(1, 10).Value = "Ön Kontrol Ýptal Açýklamasý";
            worksheet.Cell(1, 11).Value = "Onay Durumu";
            worksheet.Cell(1, 12).Value = "Ýptal Açýklamasý";
        }

        public void createSampleExcel()
        {
            var index = 2;
            worksheet.Cell(index, 1).Value = 1002863;
            worksheet.Cell(index, 2).Value = 0;
            worksheet.Cell(index, 3).Value = "Test Ýsmi";
            worksheet.Cell(index, 4).Value = "Testsoyad";
            worksheet.Cell(index, 5).Value = "Baþvuru Bekliyor";
            worksheet.Cell(index, 6).Value = "Yok";
            worksheet.Cell(index, 7).Value = "Yok";
            worksheet.Cell(index, 8).Value = "Yok";
            worksheet.Cell(index, 9).Value = "Ön Deðerleme";
            worksheet.Cell(index, 10).Value = "-";
            worksheet.Cell(index, 11).Value = 1;
            worksheet.Cell(index, 12).Value = "";

            range = worksheet.Range("A1:L2");

        }

        public void createFullExcel()
        {
            var data = _db.Basvurular;
            var totalEntryNumber = data.Count() + 1;
            range = worksheet.Range($"A1:L{totalEntryNumber}");
            int index = 1;
            foreach (var item in data)
            {
                index++;
                worksheet.Cell(index, 1).Value = item.SiraNo;
                worksheet.Cell(index, 2).Value = item.TCIdentity;
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
        }

        public void organizeTable()
        {
            IXLRange textRange = worksheet.Range("R1:W16");
            textRange.Merge();
            textRange.Value = "1. Ýptal edilmek üzere amir onayýna gönderilecek baþvurular için 'Onay Durumu' sütununa 0 giriniz.\r\n2.Onaylanmak üzere amir onayýna gönderilecek baþvurular için 'Onay Durumu' sütununa 1 giriniz.\r\n3.Adayýn baþvuru durumunu 'KPSS Puan Sýralamasýna Giremedi'olarak üzere amir onayýna gönderilecek baþvurular için 'Onay Durumu' sütununa 2 giriniz.\r\n4.Ýptal edilmek üzere onaya gönderilen baþvurularýn iptal nedenini 'Ýptal Açýklamasý' sütununa giriniz. Girilen iptal nedeni adaya gösterilecektir.Ýptal nedeni 450 karakterden kýsa olmalýdýr.\r\n";

            textRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            textRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            textRange.Style.Font.FontColor = XLColor.Red;

            var excelTable = range.CreateTable();
            excelTable.Theme = XLTableTheme.TableStyleMedium2;

            excelTable.ShowAutoFilter = false;

        }

        public FileResult downloadExcel()
        {
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                //var strDate = DateTime.Now.ToString("h");
                string fileName = string.Format("BekleyenBaþvurular.xlsx");
                return File(content, contentType, fileName);
            }

        }

        public FileResult OnGetExportExcel(int param)
        {
            using (workbook)
            {
                InitializeWorksheet();
                if (param == 0)
                {
                    createSampleExcel();
                }
                else
                {
                    createFullExcel();
                }

                organizeTable();
                FileResult download = downloadExcel();
                return download;
            }
        }

    }
}