using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TalepYonetim.Data;
using TalepYonetim.Model;
using ClosedXML.Excel;
using System;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Security.Principal;

namespace TalepYonetim.Pages
{
    public class BasvuruIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<Basvuru> Basvurular { get; set; }
        public Dictionary<int, string> BeyanDict { get; set; }
        public Dictionary<int, string> OnKontrolDurumDict { get; set; }
        public Dictionary<int, string> BasvuruDurumDict { get; set; }
        public IEnumerable<Basvuru> ExcelBasvurular { get; set; } = new List<Basvuru>();

        public XLWorkbook workbook = new XLWorkbook();
        public IXLWorksheet worksheet;
        public IXLRange range;

        [BindProperty]
        public int totalRows { get; set; }

        public BasvuruIndexModel(ApplicationDbContext db)
        {
            _db = db;

            BeyanDict = new Dictionary<int, string>
            { 
                { 0, "Yok" }, { 1, "Var" } 
            };

            OnKontrolDurumDict = new Dictionary<int, string>
            {
                {0,"�n De�erleme Bekliyor"}, {1, "Amir onay�nda"}, {2, "Amir onay� al�nd�"}
            };

            BasvuruDurumDict = new Dictionary<int, string>
            {
                {0,"Ba�vuru Bekleniyor"}, {1, "Ba�vuru onayland�"}, {2, "Ba�vuru reddedildi"}, {3, "KPSS puan s�ralamas�na giremedi"}
            };

        }
        // ana sayfa talepleri goster
        public void OnGet()
        {
            Basvurular = _db.Basvurular;

        }

        public void InitializeWorksheet()
        {
            worksheet = workbook.Worksheets.Add("BekleyenBa�vurular");
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
        }

        public void createSampleExcel()
        {
            var index = 2;
            worksheet.Cell(index, 1).Value = 1002863;
            worksheet.Cell(index, 2).Value = 0;
            worksheet.Cell(index, 3).Value = "Test �smi";
            worksheet.Cell(index, 4).Value = "Testsoyad";
            worksheet.Cell(index, 5).Value = "Ba�vuru Bekliyor";
            worksheet.Cell(index, 6).Value = "Yok";
            worksheet.Cell(index, 7).Value = "Yok";
            worksheet.Cell(index, 8).Value = "Yok";
            worksheet.Cell(index, 9).Value = "�n De�erleme";
            worksheet.Cell(index, 10).Value = "-";
            worksheet.Cell(index, 11).Value = 1;
            worksheet.Cell(index, 12).Value = "";
            worksheet.Row(index).AdjustToContents();



            range = worksheet.Range("A1:L2");
            //IXLRange range2 = worksheet.Range("A2:L2");
            //range2.Style.Alignment.WrapText = true;

        }

        public void createFullExcel()
        {
            var data = _db.Basvurular;
            var totalEntryNumber = data.Count() + 1;
            range = worksheet.Range($"A1:L{totalEntryNumber}");
            int index = 1;

            //range.Style.Alignment.WrapText = true;

            foreach (var item in data)
            {
                index++;
                worksheet.Cell(index, 1).Value = item.SiraNo;
                worksheet.Cell(index, 2).Value = item.TCIdentity;
                worksheet.Cell(index, 3).Value = item.Ad;
                worksheet.Cell(index, 4).Value = item.Soyad;
                worksheet.Cell(index, 5).Value = BasvuruDurumDict[item.BasvuruDurum];
                worksheet.Cell(index, 6).Value = BeyanDict[item.LiseMezuniyet];
                worksheet.Cell(index, 7).Value = BeyanDict[item.UniversiteMezuniyet];
                worksheet.Cell(index, 8).Value = BeyanDict[item.YabanciDil];
                worksheet.Cell(index, 9).Value = OnKontrolDurumDict[item.OnKontrolDurumu];
                worksheet.Cell(index, 10).Value = item.OnKontrolIptalAciklamasi;
                worksheet.Cell(index, 11).Value = item.OnayDurumu;
                worksheet.Cell(index, 12).Value = item.IptalAciklamasi;
                worksheet.Row(index).AdjustToContents();
            }
            //range.Style.Alignment.WrapText = true;
        }

        public void organizeTable()
        {
            IXLRange textRange = worksheet.Range("R1:W16");
            textRange.Merge();
            textRange.Value = "1. �ptal edilmek �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 0 giriniz.\r\n2.Onaylanmak �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 1 giriniz.\r\n3.Aday�n ba�vuru durumunu 'KPSS Puan S�ralamas�na Giremedi'olarak �zere amir onay�na g�nderilecek ba�vurular i�in 'Onay Durumu' s�tununa 2 giriniz.\r\n4.�ptal edilmek �zere onaya g�nderilen ba�vurular�n iptal nedenini '�ptal A��klamas�' s�tununa giriniz. Girilen iptal nedeni adaya g�sterilecektir.�ptal nedeni 450 karakterden k�sa olmal�d�r.\r\n";

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
                string fileName = string.Format("BekleyenBa�vurular.xlsx");
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

        public IActionResult OnPostImportExcel(IFormFile excelFile)
        {
            var successMessage = "";
            if (excelFile == null || excelFile.Length == 0)
            {
                // Handle the case where no file is selected for upload
                // You can add appropriate error messages or redirection here
                successMessage = "Excel dosyas� y�klenirken bir hata ile kar��la��ld�: [Dosya null, bo�].";
                return Page();
            }
            var totalCols = 0;
            using (var stream = new MemoryStream())
            {
                // Copy the uploaded file's content to the memory stream
                excelFile.CopyTo(stream);
                stream.Position = 0;
                

                // Load the Excel file from the stream
                using (var importedWorkbook = new XLWorkbook(stream))
                {
                    var importedWorksheet = importedWorkbook.Worksheet(1);

                    Console.WriteLine(importedWorksheet.LastRowUsed().RowNumber());

                    totalRows = importedWorksheet.LastRowUsed().RowNumber();
                    //totalCols = importedWorksheet.LastColumnUsed().ColumnNumber();

                }
                
            }
            successMessage = $"Yuklediginiz dosya {totalRows-1} satirdan olusmaktadir.";
            TempData["ImportSuccessMessage"] = successMessage;

            //Redirect to the index page or any other page after successful import
            return RedirectToPage("/BasvuruIndex");
        }

    }
}