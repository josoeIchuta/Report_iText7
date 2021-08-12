using System;
using System.IO;
using System.IO.Enumeration;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Hosting;
using Document = iText.Layout.Document;
using Path = System.IO.Path;

namespace Report_itext7.Reportes
{
    public class ReportePDFejemplo1
    {
        private IWebHostEnvironment _oHostEnvironment;
        public ReportePDFejemplo1(IWebHostEnvironment oHostEnvironment)
        {
            _oHostEnvironment = oHostEnvironment;
        }
        
        #region Declaracion

        private string titulo="REPORTE DE DEUDA TRIBUTARIA.";
        private string subtitulo = "El presente es un cálculo de carácter referencial, considerando los datos registrados.";
        private string parrafo1="Señor Contribuyente: \n Este es el número de trámite en línea que acaba de obtener como constancia de la presentación de Boleta de Pago direccionada a una Facilidad de Pago, realizada a través de la Oficina Virtual del SIN.Tome en cuenta los siguientes aspectos:";
        private string parrafo2 = "*\t Ya que el importe consignado es mayor a cero (0), debe aproximarse a la Entidad Financiera autorizada a efectivizar el pago.";
        private string parrafo3 = "*\t Si la presentación está siendo realizada en día sábado, domingo y/o feriado, los accesorios (intereses y mantenimiento de valor) son calculados al primer día hábil siguiente a la fecha de confirmación de envío y pago";
        private string parrafo4 = "*\t Para la \"Certificación de Boletas de Pago\", ingrese a la Oficina Virtual, elija la opción \"CERTIFICACIÓN DD.JJ.\", seleccione el formulario, N° de orden y periodo fiscal; luego presione el botón \"Consultar\" e imprimir, si así lo requiere para su constancia.";
        private string parrafo5 = "*\t Para consultar el \"Trámite\" o \"Formulario\", ingrese a la Oficina Virtual, seleccione la opción de \"DCLARO - NEWTON / DECLARACIONES JURADAS\" y desde la sección de Consultas ingrese a NEWTON y acceda a la opción Consultas de \"Trámite\" o \"Formulario\". Consigne el código de formulario o el periodo inicial y final, posteriormente presione el botón. \"Consultar\".";
        private string parrafo6 = "*\t Para obtener el \"Extracto Tributario\", ingrese a la Oficina Virtual, seleccione la opción \"EXTRACTO TRIBUTARIO\", ingrese año, mes desde-hasta y presione el botón \"Consultar\" e imprimir, si así lo requiere.";
        private string parrafo7 = "*\t Una vez realizado el pago, vuelva a ingresar a la opción \"Facilidades de Pago / Consultas de Seguimiento\" y verifique que su pago haya sido reconocido por su Plan de Pagos.";
        
        #endregion

        public byte[] Report(string ejemplo)
        {
            var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf, PageSize.LETTER);
            document.SetMargins(30f,75f,20f,75f);
            
            // document.Add(new Paragraph("Hello world!"));
            
            //LOGO
            string path = _oHostEnvironment.WebRootPath + "/Image";
            string imgCombine = Path.Combine(path, "logoImpuestos.jpeg");
            var logo = new Image(ImageDataFactory.Create(imgCombine));
            logo.Scale(3,3);
            //logo.SetAbsolutePosition(500, 650); //El origen esta en la esquina inferior Izquierda (como el plano cartesinano)
            document.Add(logo);
            
            //Muestra el titulo
            document.Add(LetrasBody(titulo,14,true,true, 10,0,TextAlignment.CENTER));
            
            //Muestra el Subtitulo
            document.Add(LetrasBody(subtitulo,10,false,true, 0,0,TextAlignment.CENTER));

            //Muestra el subtitulo de Datos Basicos
            document.Add(LetrasBody("DATOS BÁSICOS",12,true,true, 15,15,TextAlignment.LEFT));
            
            
            
            Table table = new Table(4, true);
            Style subTituloBold = new Style().SetBorder(Border.NO_BORDER).SetBold();    //
            table.SetFontSize(10);
            //table.SetPaddingLeft(15);

            // Cell headerProductId = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("NIT")).AddStyle(subTituloBold);
            // Cell headerProduct = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Product")).SetPaddingLeft(20);
            // Cell headerProductPrice = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("PERIODO")).AddStyle(subTituloBold);
            // Cell headerProductQty = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("Qty")).SetPaddingLeft(20);
            // //Casilla del Numero de ORDEN
            // Cell numOrden = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("NÚMERO DE ORDEN")).AddStyle(subTituloBold);
            // Cell datoNumOrden = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("dato"))
            //     .SetPaddingLeft(20);
            // //Casilla del IMPORTE A PAGAR
            // Cell importePagar = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("NÚMERO DE ORDEN")).AddStyle(subTituloBold);
            // Cell datoImportePagar = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("dato")).SetPaddingLeft(20);
            //
            // //Casilla del FORMULARIO
            // Cell numFormulario = new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("NÚMERO DE ORDEN")).AddStyle(subTituloBold);
            // Cell datoNumFormulario = new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).Add(new Paragraph("dato")).SetPaddingLeft(20);

            table.AddCell(DatosCelda("NIT", true));
            table.AddCell(DatosCelda("12345678", false));
            
            table.AddCell(DatosCelda("PERIODO", true));
            table.AddCell(DatosCelda("03", false));
            
            table.AddCell(DatosCelda("NÚMERO DE ORDEN", true));
            table.AddCell(DatosCelda("12345", false));
            
            table.AddCell(DatosCelda("IMPORTE A PAGAR", true));
            table.AddCell(DatosCelda("Bs. 500", false));
            
            table.AddCell(DatosCelda("FORMULARIO", true));
            table.AddCell(DatosCelda("200", false));
            // table.AddCell(headerProductId);
            // table.AddCell(headerProduct);
            // table.AddCell(headerProductPrice);
            // table.AddCell(headerProductQty);
            //
            // table.AddCell(numOrden);
            // table.AddCell(datoNumOrden);
            //
            // table.AddCell(importePagar);
            // table.AddCell(datoImportePagar);
            //
            // table.AddCell(numFormulario);
            // table.AddCell(datoNumFormulario);

            document.Add(table);
            table.Flush();
            table.Complete();
            
            
            
            //Mostrar subtitulo de Numero de Tramite
            document.Add(LetrasBody("NÚMERO DE TRÁMITE",14,true,false, 20,0,TextAlignment.CENTER));
            
            //Mostrar dato del NUMERO DE TRAMITE
            document.Add(LetrasBody("789456561",48,true,false, 0,0,TextAlignment.CENTER));
            //Parrafos
            document.Add(LetrasBody(parrafo1,10,true,false, 5,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo2,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo3,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo4,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo5,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo6,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            document.Add(LetrasBody(parrafo7,10,false,false, 0,0,TextAlignment.JUSTIFIED));
            

            document.Close();

            return stream.ToArray();
        }

        public Paragraph LetrasBody(string letras, int sizeLetra, bool isBold,bool isBlue, int separacionTop, int separacionBottom, TextAlignment posicionLetra)
        {
            Paragraph letrasBody = new Paragraph(letras);
            letrasBody.SetTextAlignment(posicionLetra);
            letrasBody.SetFontSize(sizeLetra);
            letrasBody.SetPaddingTop(separacionTop); //separacion con bloque de arriba
            letrasBody.SetPaddingBottom(separacionBottom);
            if(isBlue)
                letrasBody.SetFontColor(new DeviceRgb(0, 64, 128));
            if(isBold)
                letrasBody.SetBold();
            return letrasBody;
        }

        private Cell DatosCelda(string contenidoCelda, bool isBold)
        {
            Cell datosCelda = new Cell(1,1);
            //Style subTituloBold = new Style().SetBorder(Border.NO_BORDER).SetBold();
            Style letraBold = new Style().SetBorder(Border.NO_BORDER).SetBold().SetTextAlignment(TextAlignment.LEFT).SetPaddingLeft(10);
            Style letrasNoBold = new Style().SetBorder(Border.NO_BORDER).SetPaddingLeft(20).SetTextAlignment(TextAlignment.LEFT);
            datosCelda.Add(new Paragraph(contenidoCelda));
            if (isBold)
                datosCelda.AddStyle(letraBold);
            else
            {
                datosCelda.AddStyle(letrasNoBold);
            }

            return datosCelda;
        }
    }
}