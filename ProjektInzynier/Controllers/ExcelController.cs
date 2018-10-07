using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using Syncfusion.Drawing;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExcelController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult Create(OrderModel orderModel)
        {
            //Step 1 : Instantiate the spreadsheet creation engine.
            ExcelEngine excelEngine = new ExcelEngine();
            //Step 2 : Instantiate the excel application object.
            IApplication application = excelEngine.Excel;

            IWorkbook workbook = application.Workbooks.Create(3);
            IWorksheet sheet = workbook.Worksheets[0];

            sheet.Range["A6:H6"].Merge(true);
            sheet.Range["A6"].Text = "WNIOSEK O AKCEPTACJĘ MATERIAŁÓW";
            sheet.Range["A6"].CellStyle.Font.FontName = "Arial";
            sheet.Range["A6"].CellStyle.Font.Bold = true;
            sheet.Range["A6"].CellStyle.Font.Size = 14;
            sheet.Range["A6"].HorizontalAlignment = ExcelHAlign.HAlignCenter;

            sheet.Range["A8:B17"].CellStyle.Font.FontName = "Calibri";
            sheet.Range["A8:B17"].CellStyle.Font.Size = 11;

            sheet.Range["A8"].Text = "Nr porządkowy:";
            sheet.Range["B9"].Text = DateTime.Now.ToString("dd/MM/yyyy");
            sheet.Range["A9"].Text = "Data:";

            sheet.Range["A11"].Text = "Budowa";
            sheet.Range["A12"].Text = "Inwestor";
            sheet.Range["A13"].Text = "Nadzór";
            sheet.Range["A14"].Text = "Wykonawca";
            sheet.Range["A15"].Text = "Projektant";
            sheet.Range["B15"].Text = orderModel.Name;
            sheet.Range["A16"].Text = "Projektant branżowy";
            sheet.Range["A17"].Text = "Dane dotyczą wyrobu zgodnego z projektem wykonawczym.";
            sheet.Range["A17"].CellStyle.Font.Bold = true;

            sheet.Range["A20"].Text = "Branża";


            string ContentType = null;
            string fileName = null;

            workbook.Version = ExcelVersion.Excel2013;
            ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            fileName = "Sample.xlsx";

            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;

            return File(ms, ContentType, fileName);
        }
    }
}
