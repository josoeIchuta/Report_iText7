using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Report_itext7.Reportes;

namespace Report_itext7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        private readonly IWebHostEnvironment _oHostEnvironment;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment oHostEnvironment)
        {
            _logger = logger;
            _oHostEnvironment = oHostEnvironment;
        }

        public void OnGet()
        {
        }
        
        public FileContentResult OnGetReporte(int id)
        {
            // //Este codigo se uso para verificar con el DEBUG si ingresaba a este metodo
            //  var resp = id;
            //  Console.WriteLine("este es el: ", id);
            //  return null

            //// ComponenteCalculadoraReport rpt = new ComponenteCalculadoraReport(_oHostEnvironment);
            // PdfEjemplo1 rpt = new PdfEjemplo1(_oHostEnvironment);
            // var dto = ObtenerComponenteDTO();
            // var pdf = File(rpt.Report(dto), "application/pdf");
            // return pdf;

            ReportePDFejemplo1 rpt = new ReportePDFejemplo1(_oHostEnvironment);
            var dto = "HOLA MUNDO";
            var pdf = File(rpt.Report(dto), "application/pdf");
            return pdf;
            
            // PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream("/myfiles/hello.pdf", FileMode.Create, FileAccess.Write)));
            // Document document = new Document(pdfDocument);
            //
            // String line = "Hello! Welcome to iTextPdf";
            // document.Add(new Paragraph(line));
            // document.Close();
            // Console.WriteLine("Awesome PDF just got created.");
            // return null;
        }
    }
}