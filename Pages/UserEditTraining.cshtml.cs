    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETMP.Data;
using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;


/*using iTextSharp.text;
using iTextSharp.text.pdf;*/
using System.Data.SqlClient;
using PuppeteerSharp;
using SendGrid.Helpers.Mail;
/*using PuppeteerSharp;
using MimeKit;*/
/*using PdfSharp;
using PdfSharp.Pdf;*/
/*using PdfSharp.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;*/


/*using System.Web.Mvc;
using Rotativa;*/



namespace ETMP.Pages
{
    public class UserEditTrainingModel : PageModel
    {
        [BindProperty]
        public TrainingModel EditTraining { get; set; }
        public string ErrorMsg { get; set; }
        public System.DateTime ReleaseDate { get; set; } = System.DateTime.Now;
        private readonly UserManager<ETMPUser> _userManager;
        private readonly SignInManager<ETMPUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly Services.IMailService _mailService;

        /*private readonly IUserStore<ETMPUser> _userStore;
        private readonly IUserEmailStore<ETMPUser> _emailStore;
        private readonly ILogger<UserEditTrainingModel> _logger;
        private readonly Services.IMailService _mailService;*/

        public UserEditTrainingModel(Services.IMailService mailService, ApplicationDbContext context, UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mailService = mailService;


        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user.PurchasedTraining != null)
            {
                var _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);

                foreach (var i in _trainingList)
                {
                    if (i.Id == id)
                    {
                        EditTraining = i;
                        break;
                    }
                }
            }
            return Page();
        }

        public IActionResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest();
            }

            var path = Path.Combine("uploads", filePath);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

        public IActionResult OnPostCancelButton()
        {
            return RedirectToPage("/UserListOfTraining");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            var _trainingList = JsonConvert.DeserializeObject<List<TrainingModel>>(user.PurchasedTraining);

            if (EditTraining.TrainingStartDateTime >= EditTraining.TrainingEndDateTime)
            {
                ErrorMsg = "Please select an appropriate date";
                return Page();
            }

            foreach (var edited in _trainingList)
            {
                if (edited.Id == id)
                {
                    edited.TrainingItinerary = "To be Removed";
                    edited.TrainingVenue = EditTraining.TrainingVenue;
                    edited.TrainingStartDateTime = EditTraining.TrainingStartDateTime;
                    edited.TrainingEndDateTime = EditTraining.TrainingEndDateTime;

                    

                    foreach (var role in _context.UserRoles.ToList())
                    {
                        foreach (var person in _context.Users.ToList())
                        {
                            if (person.Id == role.UserId)
                            {
                                if (role.RoleId == "af6b77bf-e0f4-4df2-839b-b1c1fd3b62c4")
                                {
                                    Notification notiToAdmin = new Notification();
                                    notiToAdmin.ToUserId = person.Id;
                                    notiToAdmin.ToUserName = person.UserName;
                                    notiToAdmin.NotificationHeader = "Training Venue and/or Training date changed";
                                    notiToAdmin.NotificationBody = user.UserName + " with the id of " + user.Id + " has changed his/her training!\n" +
                                    "Training Edited: " + edited.TrainingName + "\n" +
                                    "New Training Venue " + edited.TrainingVenue + "\n" +
                                    "New Training Start Date " + edited.TrainingStartDateTime + "\n" +
                                    "New Training End Date " + edited.TrainingEndDateTime + "\n";

                                    notiToAdmin.NotificationDate = DateTime.Now;
                                    _context.Notification.Add(notiToAdmin);

                                    string subj = notiToAdmin.NotificationHeader;
                                    string Body = notiToAdmin.NotificationBody;
                                    string toAdminEmail = person.Email;
                                    var mailRequest = new MailRequest(toAdminEmail, subj, Body, null);
                                    await _mailService.SendEmailAsync(mailRequest);

                                }
                            }
                        }
                    }
                }

                
            }
            var toSave = JsonConvert.SerializeObject(_trainingList);
            
            user.PurchasedTraining = toSave;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("./UserListOfTraining");
        }


        /*
                public IActionResult MyAction()
                {
                    string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";
                    string query = "SELECT TrainingName, TrainingPrice, TrainingVenue, TrainingCategory, Availability, TrainingDescription, TrainingImgURL FROM Identity.Trainings";
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Document document = new Document();
                    MemoryStream stream = new MemoryStream();
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    PdfPTable table = new PdfPTable(7);
                    table.AddCell("Training Name");
                    table.AddCell("Training Price");
                    table.AddCell("Training Venue");
                    table.AddCell("Training Category");
                    table.AddCell("Availability");
                    table.AddCell("Training Description");
                    table.AddCell("Training Image URL");

                    while (reader.Read())
                    {
                        table.AddCell(reader["TrainingName"].ToString());
                        table.AddCell(reader["TrainingPrice"].ToString());
                        table.AddCell(reader["TrainingVenue"].ToString());
                        table.AddCell(reader["TrainingCategory"].ToString());
                        table.AddCell(reader["Availability"].ToString());
                        table.AddCell(reader["TrainingDescription"].ToString());
                        table.AddCell(reader["TrainingImgURL"].ToString());
                    }

                    document.Add(table);
                    document.Close();

                    byte[] bytes = stream.ToArray();
                    stream.Close();

                    return File(bytes, "application/pdf", "output.pdf");
                }*/

        /*public async Task<IActionResult> DownloadAndEmailPdf()*/








        /* public async Task<IActionResult> OnPostDownloadAndEmailPdfAsync()
         {
             // Connection string
             var connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";

             // SQL query
             var query = "SELECT TrainingName, TrainingPrice, TrainingVenue, TrainingCategory, Availability, TrainingDescription, TrainingImgURL FROM Identity.Trainings";

             // Create and open database connection
             using var connection = new SqlConnection(connectionString);
             await connection.OpenAsync();

             // Create and execute SQL command
             using var command = new SqlCommand(query, connection);
             using var reader = await command.ExecuteReaderAsync();

             // Create PDF document
             using var document = new Document();
             using var stream = new MemoryStream();
             PdfWriter.GetInstance(document, stream);
             document.Open();

             // Create table
             var table = new PdfPTable(7);
             table.AddCell("Training Name");
             table.AddCell("Training Price");
             table.AddCell("Training Venue");
             table.AddCell("Training Category");
             table.AddCell("Availability");
             table.AddCell("Training Description");
             table.AddCell("Training Image URL");

             // Add data to table
             while (await reader.ReadAsync())
             {
                 table.AddCell(reader["TrainingName"].ToString());
                 table.AddCell(reader["TrainingPrice"].ToString());
                 table.AddCell(reader["TrainingVenue"].ToString());
                 table.AddCell(reader["TrainingCategory"].ToString());
                 table.AddCell(reader["Availability"].ToString());
                 table.AddCell(reader["TrainingDescription"].ToString());
                 table.AddCell(reader["TrainingImgURL"].ToString());
             }

             // Add table to document
             document.Add(table);
             document.Close();

             // Get PDF bytes
             var bytes = stream.ToArray();

             // TODO: Email PDF

             // Return PDF as file result
             return File(bytes, "application/pdf", "Trainings.pdf");
         }*/





        /*document.getElementById('download-btn').addEventListener('click', function () {
            fetch('https://localhost:7107/UserEditTraining/DownloadAndEmailPdf', { method: 'POST' })
            .then(function(response) {
                if (!response.ok)
                {
                    throw new Error('Network response was not ok');
                }
                return response.blob();
            })
            .then(function(blob) {
                var url = window.URL.createObjectURL(blob);
                var a = document.createElement('a');
                a.href = url;
                a.download = 'Trainings.pdf';
                document.body.appendChild(a);
                a.click();
                a.remove();
            })
            .catch(function(error) {
                console.error('There was a problem with the fetch operation:', error);
            });
        });*/




        /* public async Task<IActionResult> OnPostDownloadAndEmailPdfAsync()
         {
             // Connection string
             var connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";

             // SQL query
             var query = "SELECT UserName FROM Identity.User";

             // Create and open database connection
             using var connection = new SqlConnection(connectionString);
             await connection.OpenAsync();

             // Create and execute SQL command
             using var command = new SqlCommand(query, connection);
             using var reader = await command.ExecuteReaderAsync();

             // Get email address from database
             string emailAddress = null;
             if (await reader.ReadAsync())
             {
                 emailAddress = reader["UserName"].ToString();
             }

             if (emailAddress == null)
             {
                 // Handle case where email address is not found
                 return Page();
             }



             // Generate PDF of page


             *//*await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);*//*


             using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });

             using var page = await browser.NewPageAsync();

             await page.GoToAsync("https://localhost:7107/UserListOfTraining");

             var pdfData = await page.PdfDataAsync();

             // Email PDF to user
             var message = new MimeMessage();

             message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));

             *//*message.To.Add(new MailboxAddress(emailAddress));*//*
             new MailboxAddress("Your Name", emailAddress);

             message.Subject = "Your Itinerary";

             var builder = new BodyBuilder();

             builder.TextBody = "Here is your itinerary.";

             builder.Attachments.Add("Itinerary.pdf", pdfData);

             message.Body = builder.ToMessageBody();

             return Page();
         }
     }*/



        /// <summary>
        /// //////////
        /// </summary>
        /// <returns></returns>
        /* public IActionResult OnGetDownloadPdf()
         {
             // Set up the PDF document
             var pdfDocument = new PdfDocument();
             var pdfPage = pdfDocument.AddPage();
             var pdfGraphics = XGraphics.FromPdfPage(pdfPage);
             var pdfFont = new XFont("Arial", 14);

             // Add your page content to the PDF document
             pdfGraphics.DrawString("Your page content goes here", pdfFont, XBrushes.Black, new XRect(0, 0, pdfPage.Width, pdfPage.Height), XStringFormats.Center);

             // Save the PDF document to a byte array
             using (var memoryStream = new MemoryStream())
             {
                 pdfDocument.Save(memoryStream);
                 var pdfBytes = memoryStream.ToArray();

                 // Return the generated PDF file
                 return File(pdfBytes, "application/pdf", "UserListOfTraining.pdf");
             }
         }*/



        /*        public IActionResult OnGetDownloadPdf()
                {
                    try
                    {
                        // Set up the PDF document
                        var pdfDocument = new PdfDocument();
                        var pdfPage = pdfDocument.AddPage();
                        var pdfGraphics = XGraphics.FromPdfPage(pdfPage);
                        var pdfFont = new XFont("Arial", 14);

                        // Add your page content to the PDF document
                        pdfGraphics.DrawString("Your page content goes here", pdfFont, XBrushes.Black, new XRect(0, 0, pdfPage.Width, pdfPage.Height), XStringFormats.Center);

                        // Save the PDF document to a byte array
                        using (var memoryStream = new MemoryStream())
                        {
                            pdfDocument.Save(memoryStream);
                            var pdfBytes = memoryStream.ToArray();

                            // Return the generated PDF file
                            return File(pdfBytes, "application/pdf", "UserListOfTraining.pdf");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception for debugging purposes
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);

                        // Return an error message to the user
                        return Content("An error occurred while generating the PDF file.");
                    }
                }*/

        /*        protected void Button1_Click(object sender, EventArgs e)
                {
                    //Initialize HTML to PDF converter 
                    HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

                    //Get the current URL
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;

                    //Convert URL to PDF document
                    PdfDocument document = htmlConverter.Convert(url);

                    //Save the document
                    document.Save("Sample.pdf", HttpContext.Current.Response, HttpReadType.Save);
                }*/

        /*        public class HomeController : Controller
                {
                    public IActionResult Index()
                    {
                        return View();
                    }
                    public IActionResult ExportToPDF()
                    {
                        HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

                        //Convert URL to PDF document
                        PdfDocument document = htmlConverter.Convert("https://www.syncfusion.com");

                        //Create memory stream
                        MemoryStream stream = new MemoryStream();

                        //Save the document
                        document.Save(stream);

                        return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "HTML-to-PDF.pdf");
                    }


                }*/
/*
        namespace Rotativa_Simple_Example.Controllers
    {
        public class HomeController : Controller
        {
            //Other Action Method

            public ActionResult ConvertToPDF()
            {
                var printpdf = new ActionAsPdf("Index");
                return printpdf;
            }
        }
    }*/
}  
}
