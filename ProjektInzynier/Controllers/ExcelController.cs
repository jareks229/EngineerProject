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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjektInzynier.Helpers;

namespace ProjektInzynier.Controllers
{
    //kontroler do generowania excela
    public class ExcelController : Controller
    {
        private IHttpContextAccessor _accessor;
        private readonly EFCContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ExcelController(IHostingEnvironment hostingEnvironment, EFCContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _accessor = accessor;
        }

        

        [HttpPost]
        public IActionResult Create(OrderModel orderModel)
        {
            var list = _accessor.HttpContext.Session.GetJson<List<CartLine>>(User.Identity.Name);

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
            sheet.Range["B11"].Text = orderModel.Construction;
            sheet.Range["A12"].Text = "Inwestor";
            sheet.Range["B12"].Text = orderModel.Contractor;
            sheet.Range["A13"].Text = "Nadzór";
            sheet.Range["B13"].Text = orderModel.Supervision;
            sheet.Range["A14"].Text = "Wykonawca";
            sheet.Range["B14"].Text = orderModel.Investor;
            sheet.Range["A15"].Text = "Projektant";
            sheet.Range["B15"].Text = orderModel.Name;
            sheet.Range["A16"].Text = "Projektant branżowy";
            sheet.Range["B16"].Text = orderModel.IndustryEngineer;
            sheet.Range["A17"].Text = "Dane dotyczą wyrobu zgodnego z projektem wykonawczym.";
            sheet.Range["A17"].CellStyle.Font.Bold = true;

            sheet.Range["A20"].Text = "Branża";
            sheet.Range["B20:H20"].Merge(true);
            sheet.Range["B20"].Text = "Sanitarna";
            sheet.Range["B20"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.Range["A20:H20"].BorderAround();

            sheet.Range["A22"].Text = "Nazwa i parametry techniczne materiału:";
            sheet.Range["A22"].WrapText = true;
            sheet.Range["A22:H22"].BorderAround();

            sheet.Range["B22:H22"].Merge(true);
            sheet.Range["B22"].WrapText = true;
            sheet.Range["B22"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.Range["B22"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            sheet.Range["B22"].CellStyle.Font.Bold = true;
            sheet.Range["B22:H22"].BorderAround();

            foreach (var i in list)
            {
                sheet.Range["B22"].Text = i.Product.TechnicalParameters;
            }

            sheet.Range["A24"].Text = "Lokalizacja w obiekcie(osie, sekcje, itp.): ";
            sheet.Range["A24"].WrapText = true;
            sheet.Range["B24:H24"].Merge(true);
            sheet.Range["B24"].WrapText = true;
            sheet.Range["B24"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.Range["B24"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            sheet.Range["B24"].CellStyle.Font.Bold = true;
            foreach (var i in list)
            {
                sheet.Range["B24"].Text = i.Product.Localization;
            }
            sheet.Range["A24:H24"].BorderAround();

            sheet.Range["A26"].Text = "Nazwa i adres producenta: ";
            sheet.Range["A26"].WrapText = true;
            sheet.Range["B26:H26"].Merge(true);
            sheet.Range["B26"].WrapText = true;
            sheet.Range["B26"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            sheet.Range["B26"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
            sheet.Range["B26"].CellStyle.Font.Bold = true;

            foreach (var i in list)
            {
                sheet.Range["B26"].Text = i.Product.ProducentAdress;
            }
            sheet.Range["A26:H26"].BorderAround();

            sheet.Range["D43"].Text = "Zgłaszający - Generalny Wykonawca";
            sheet.Range["D43"].CellStyle.Font.Bold = true;
            sheet.Range["D43"].CellStyle.Font.Underline = ExcelUnderline.Single;
            sheet.Range["D43"].HorizontalAlignment = ExcelHAlign.HAlignCenter;

            sheet.Range["D46"].Text = orderModel.Name;
            sheet.Range["D47"].Text = "osoba";

            sheet.Range["F47"].Text = "Data";
            sheet.Range["F46"].Text = DateTime.Now.ToString("dd/MM/yyyy");

            sheet.Range["H47"].Text = "Podpis";
            
            sheet.Range["D46"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Dashed;
            sheet.Range["F46"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Dashed;
            sheet.Range["H46"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Dashed;

            sheet.Range["A43:H47"].BorderAround();


            string ContentType = null;
            string fileName = null;

            workbook.Version = ExcelVersion.Excel2013;
            ContentType = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            fileName = $"Wniosek o akceptacje materialu {orderModel.Name}.xlsx";

            MemoryStream ms = new MemoryStream();
            workbook.SaveAs(ms);
            ms.Position = 0;

            return File(ms, ContentType, fileName);
        }
    }
}
