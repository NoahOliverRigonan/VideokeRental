using VideokeRental.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace VideokeRental.Controllers
{
    public class SoftwareController : UserAccountController {
        
        [Authorize]
        // GET: Software
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Product()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Customer()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Users()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Message()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Calendar()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult Reports()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult VideokeDetails()
        {
            return View();
        }
        [Authorize]
        // GET: Software
        public ActionResult CalendarReservePDF(String date)
        {
            // Start of the PDF
            MemoryStream workStream = new MemoryStream();
            Rectangle rec = new Rectangle(PageSize.A3);
            Document document = new Document(rec, 72, 72, 72, 72);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(line);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        [Authorize]
        // GET: Software
        public ActionResult CalendarRentalPDF(String date)
        {
            // Start of the PDF
            MemoryStream workStream = new MemoryStream();
            Rectangle rec = new Rectangle(PageSize.A3);
            Document document = new Document(rec, 72, 72, 72, 72);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(line);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
    }
}