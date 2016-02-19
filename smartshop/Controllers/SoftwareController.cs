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
    public class SoftwareController : UserAccountController
    {

        private Data.videokerentalDataContext db = new Data.videokerentalDataContext();

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
        public ActionResult CalendarReservePDF(String DateFrom, String DateTo)
        {
            var calendarResevations = from d in db.tblCalendarReservations
                                      where d.ReservedDate >= Convert.ToDateTime(DateFrom) && d.ReservedDate <= Convert.ToDateTime(DateTo)
                                      select new Models.tblCalendarReservation
                                      {
                                          Id = d.Id,
                                          CustomersIdReserved = d.CustomersIdReserved,
                                          ProductId = d.ProductId,
                                          Product = d.tblProduct.ProductName,
                                          CustomersReserved = d.tblCustomer.Customer,
                                          ReservedDate = d.ReservedDate.ToShortDateString(),
                                      };

            // Start of the PDF
            MemoryStream workStream = new MemoryStream();
            Rectangle rec = new Rectangle(PageSize.A3);
            Document document = new Document(rec, 72, 72, 72, 72);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            // Fonts Customization
            Font headerFont = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font headerDetailFont = FontFactory.GetFont("Arial", 11);
            Font columnFont = FontFactory.GetFont("Arial", 11, Font.BOLD);
            Font cellFont = FontFactory.GetFont("Arial", 11);

            // table main header
            PdfPTable tableHeader = new PdfPTable(2);
            float[] widthscellsheader = new float[] { 100f, 75f };
            tableHeader.SetWidths(widthscellsheader);
            tableHeader.WidthPercentage = 100;
            tableHeader.AddCell(new PdfPCell(new Phrase("Calendar Reservation List - Report", headerFont)) { Border = 0, HorizontalAlignment = 0 });
            tableHeader.AddCell(new PdfPCell(new Phrase("Printed " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt"), headerDetailFont))
            {
                Border = 0,
                HorizontalAlignment = 2,
                PaddingTop = 5f
            });
            tableHeader.AddCell(new PdfPCell(new Phrase("Date from " + DateFrom + " to " + DateTo, cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableHeader.AddCell(new PdfPCell(new Phrase(" ", cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            document.Add(tableHeader);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(line);

            PdfPTable tableForCalendarReservation = new PdfPTable(3);
            float[] widthscellsheaderForCalendarReservation = new float[] { 50f, 100f, 70f };
            tableForCalendarReservation.SetWidths(widthscellsheaderForCalendarReservation);
            tableForCalendarReservation.WidthPercentage = 100;
            tableForCalendarReservation.AddCell(new PdfPCell(new Phrase("Reserve Date", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForCalendarReservation.AddCell(new PdfPCell(new Phrase("Videoke", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForCalendarReservation.AddCell(new PdfPCell(new Phrase("Customer", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

            foreach (var calendarReserve in calendarResevations)
            {
                tableForCalendarReservation.AddCell(new PdfPCell(new Phrase(calendarReserve.ReservedDate.ToString(), cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForCalendarReservation.AddCell(new PdfPCell(new Phrase(calendarReserve.Product, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForCalendarReservation.AddCell(new PdfPCell(new Phrase(calendarReserve.CustomersReserved, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

            }
            document.Add(tableForCalendarReservation);

            document.Add(Chunk.NEWLINE);

            var NumberOfReservation = calendarResevations.Count();

            PdfPTable tableForCalendarReservationFooter = new PdfPTable(3);
            float[] widthscellsheaderForCalendarReservationFooter = new float[] { 50f, 100f, 70f };
            tableForCalendarReservationFooter.SetWidths(widthscellsheaderForCalendarReservationFooter);
            tableForCalendarReservationFooter.WidthPercentage = 100;
            tableForCalendarReservationFooter.AddCell(new PdfPCell(new Phrase("", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableForCalendarReservationFooter.AddCell(new PdfPCell(new Phrase("No of Reservations:", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f, HorizontalAlignment = 2 });
            tableForCalendarReservationFooter.AddCell(new PdfPCell(new Phrase(NumberOfReservation.ToString(), columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });


            document.Add(tableForCalendarReservationFooter);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        [Authorize]
        // GET: Software
        public ActionResult CalendarRentalPDF(String DateFrom, String DateTo)
        {
            var calendarRentals = from d in db.tblCalendarRentals
                                  where d.RentedDate >= Convert.ToDateTime(DateFrom) && d.RentedDate <= Convert.ToDateTime(DateTo)
                                  select new Models.tblCalendarRental
                                  {
                                      Id = d.Id,
                                      CustomersIdRented = d.CustomersIdRented,
                                      CustomersRent = d.tblCustomer.Customer,
                                      ProductId = d.ProductId,
                                      Product = d.tblProduct.ProductName,
                                      RentedDate = d.RentedDate.ToShortDateString(),
                                  };

            // Start of the PDF
            MemoryStream workStream = new MemoryStream();
            Rectangle rec = new Rectangle(PageSize.A3);
            Document document = new Document(rec, 72, 72, 72, 72);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            // Fonts Customization
            Font headerFont = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font headerDetailFont = FontFactory.GetFont("Arial", 11);
            Font columnFont = FontFactory.GetFont("Arial", 11, Font.BOLD);
            Font cellFont = FontFactory.GetFont("Arial", 11);

            // table main header
            PdfPTable tableHeader = new PdfPTable(2);
            float[] widthscellsheader = new float[] { 100f, 75f };
            tableHeader.SetWidths(widthscellsheader);
            tableHeader.WidthPercentage = 100;
            tableHeader.AddCell(new PdfPCell(new Phrase("Calendar Rental List - Report", headerFont)) { PaddingBottom = 5f, Border = 0, HorizontalAlignment = 0 });
            tableHeader.AddCell(new PdfPCell(new Phrase("Printed " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt"), headerDetailFont))
            {
                Border = 0,
                HorizontalAlignment = 2,
                PaddingTop = 5f
            });
            tableHeader.AddCell(new PdfPCell(new Phrase("Date from " + DateFrom + " to " + DateTo, cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableHeader.AddCell(new PdfPCell(new Phrase(" ", cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            document.Add(tableHeader);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(line);

            PdfPTable tableForCalendarRental = new PdfPTable(3);
            float[] widthscellsheaderForCalendarRental = new float[] { 50f, 100f, 70f };
            tableForCalendarRental.SetWidths(widthscellsheaderForCalendarRental);
            tableForCalendarRental.WidthPercentage = 100;
            tableForCalendarRental.AddCell(new PdfPCell(new Phrase("Rental Date", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForCalendarRental.AddCell(new PdfPCell(new Phrase("Videoke", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForCalendarRental.AddCell(new PdfPCell(new Phrase("Customer", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

            foreach (var calendarRental in calendarRentals)
            {
                tableForCalendarRental.AddCell(new PdfPCell(new Phrase(calendarRental.RentedDate.ToString(), cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForCalendarRental.AddCell(new PdfPCell(new Phrase(calendarRental.Product, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForCalendarRental.AddCell(new PdfPCell(new Phrase(calendarRental.CustomersRent, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

            }
            document.Add(tableForCalendarRental);

            document.Add(Chunk.NEWLINE);

            var NumberOfRentees = calendarRentals.Count();

            PdfPTable tableForCalendarRentalFooter = new PdfPTable(3);
            float[] widthscellsheaderForCalendarRentalFooter = new float[] { 50f, 100f, 70f };
            tableForCalendarRentalFooter.SetWidths(widthscellsheaderForCalendarRentalFooter);
            tableForCalendarRentalFooter.WidthPercentage = 100;
            tableForCalendarRentalFooter.AddCell(new PdfPCell(new Phrase("", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableForCalendarRentalFooter.AddCell(new PdfPCell(new Phrase("No of Rentees:", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f, HorizontalAlignment = 2 });
            tableForCalendarRentalFooter.AddCell(new PdfPCell(new Phrase(NumberOfRentees.ToString(), columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });


            document.Add(tableForCalendarRentalFooter);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
        
        [Authorize]
        // GET: Software
        public ActionResult VideokeIncomePDF(String StartDate, String EndDate)
        {
            var videokeIncomes = from d in db.tblVideokeIncomes
                                 where d.DateRent >= Convert.ToDateTime(StartDate) && d.DateRent <= Convert.ToDateTime(EndDate)
                                  select new Models.tblVideokeIncome
                                  {
                                      Id = d.Id,
                                      ProductId = d.ProductId,
                                      Product = d.tblProduct.ProductName,
                                      CustomerId = d.CustomerId,
                                      Customer = d.tblCustomer.Customer,
                                      Price = d.Price,
                                      DateRent = d.DateRent.ToShortDateString(),
                                  };

            // Start of the PDF
            MemoryStream workStream = new MemoryStream();
            Rectangle rec = new Rectangle(PageSize.A3);
            Document document = new Document(rec, 72, 72, 72, 72);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            // Document Starts
            document.Open();

            // Fonts Customization
            Font headerFont = FontFactory.GetFont("Arial", 17, Font.BOLD);
            Font headerDetailFont = FontFactory.GetFont("Arial", 11);
            Font columnFont = FontFactory.GetFont("Arial", 11, Font.BOLD);
            Font cellFont = FontFactory.GetFont("Arial", 11);

            // table main header
            PdfPTable tableHeader = new PdfPTable(2);
            float[] widthscellsheader = new float[] { 100f, 75f };
            tableHeader.SetWidths(widthscellsheader);
            tableHeader.WidthPercentage = 100;
            tableHeader.AddCell(new PdfPCell(new Phrase("Income Report", headerFont)) { PaddingBottom = 5f, Border = 0, HorizontalAlignment = 0 });
            tableHeader.AddCell(new PdfPCell(new Phrase("Printed " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt"), headerDetailFont))
            {
                Border = 0,
                HorizontalAlignment = 2,
                PaddingTop = 5f
            });
            tableHeader.AddCell(new PdfPCell(new Phrase("Date from " + StartDate + " to " + EndDate, cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableHeader.AddCell(new PdfPCell(new Phrase(" ", cellFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            document.Add(tableHeader);

            Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(line);


            PdfPTable tableForVideokeIncome = new PdfPTable(4);
            float[] widthscellsheaderForVideokeIncome = new float[] { 50f, 100f, 70f, 50f };
            tableForVideokeIncome.SetWidths(widthscellsheaderForVideokeIncome);
            tableForVideokeIncome.WidthPercentage = 100;
            tableForVideokeIncome.AddCell(new PdfPCell(new Phrase("Date Rent", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForVideokeIncome.AddCell(new PdfPCell(new Phrase("Product", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForVideokeIncome.AddCell(new PdfPCell(new Phrase("Customer", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });
            tableForVideokeIncome.AddCell(new PdfPCell(new Phrase("Price", columnFont)) { HorizontalAlignment = 1, PaddingBottom = 5f, BackgroundColor = BaseColor.LIGHT_GRAY });

            foreach (var videokeIncome in videokeIncomes)
            {
                tableForVideokeIncome.AddCell(new PdfPCell(new Phrase(videokeIncome.DateRent.ToString(), cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForVideokeIncome.AddCell(new PdfPCell(new Phrase(videokeIncome.Product, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForVideokeIncome.AddCell(new PdfPCell(new Phrase(videokeIncome.Customer, cellFont)) { PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
                tableForVideokeIncome.AddCell(new PdfPCell(new Phrase(videokeIncome.Price.ToString("#,##0.00"), cellFont)) {  HorizontalAlignment = 2, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });

            }
            document.Add(tableForVideokeIncome);

            document.Add(Chunk.NEWLINE);

            var TotalIncomePrice = videokeIncomes.Sum(d => d.Price);

            PdfPTable tableForVideokeIncomeFooter = new PdfPTable(4);
            float[] widthscellsheaderForVideokeIncomeFooter = new float[] { 50f, 100f, 70f, 50f };
            tableForVideokeIncomeFooter.SetWidths(widthscellsheaderForVideokeIncomeFooter);
            tableForVideokeIncomeFooter.WidthPercentage = 100;
            tableForVideokeIncomeFooter.AddCell(new PdfPCell(new Phrase("", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            tableForVideokeIncomeFooter.AddCell(new PdfPCell(new Phrase("", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f, HorizontalAlignment = 2 });
            tableForVideokeIncomeFooter.AddCell(new PdfPCell(new Phrase("Total Income:", columnFont)) { Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f, HorizontalAlignment = 2 });
            tableForVideokeIncomeFooter.AddCell(new PdfPCell(new Phrase(TotalIncomePrice.ToString("#,##0.00"), columnFont)) { HorizontalAlignment = 2, Border = 0, PaddingBottom = 5f, PaddingLeft = 5f, PaddingRight = 5f });
            

            document.Add(tableForVideokeIncomeFooter);

            document.Add(Chunk.NEWLINE);

            // Table for Footer
            PdfPTable tableFooter = new PdfPTable(5);
            tableFooter.WidthPercentage = 100;
            float[] widthsCells2 = new float[] { 100f, 20f, 100f, 20f, 100f };
            tableFooter.SetWidths(widthsCells2);
            tableFooter.AddCell(new PdfPCell(new Phrase("")) { Border = 0, PaddingTop = 10f, HorizontalAlignment = 1, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase("")) { Border = 0, PaddingTop = 10f, HorizontalAlignment = 1, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase("")) { Border = 0, PaddingTop = 10f, HorizontalAlignment = 1, PaddingBottom = 5f });
            tableFooter.AddCell(new PdfPCell(new Phrase("Prepared by:", columnFont)) { Border = 1, HorizontalAlignment = 1 });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase("Checked by:", columnFont)) { Border = 1, HorizontalAlignment = 1 });
            tableFooter.AddCell(new PdfPCell(new Phrase(" ")) { Border = 0 });
            tableFooter.AddCell(new PdfPCell(new Phrase("Approved by:", columnFont)) { Border = 1, HorizontalAlignment = 1 });
            document.Add(tableFooter);

            // Document End
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return new FileStreamResult(workStream, "application/pdf");
        }

    }
}