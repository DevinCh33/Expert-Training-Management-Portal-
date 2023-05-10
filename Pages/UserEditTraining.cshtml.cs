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


using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using PuppeteerSharp;
using MimeKit;



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

        /*private readonly IUserStore<ETMPUser> _userStore;
        private readonly IUserEmailStore<ETMPUser> _emailStore;
        private readonly ILogger<UserEditTrainingModel> _logger;
        private readonly Services.IMailService _mailService;*/

        public UserEditTrainingModel(UserManager<ETMPUser> userManager, SignInManager<ETMPUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;


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
                    edited.TrainingItinerary = EditTraining.TrainingItinerary;
                    edited.TrainingVenue = EditTraining.TrainingVenue;
                    edited.TrainingStartDateTime = EditTraining.TrainingStartDateTime;
                    edited.TrainingEndDateTime = EditTraining.TrainingEndDateTime;
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




        public async Task<IActionResult> OnPostDownloadAndEmailPdfAsync()
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


            /*await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);*/


            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });

            using var page = await browser.NewPageAsync();

            await page.GoToAsync("https://localhost:7107/UserListOfTraining");

            var pdfData = await page.PdfDataAsync();

            // Email PDF to user
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));

            /*message.To.Add(new MailboxAddress(emailAddress));*/
            new MailboxAddress("Your Name", emailAddress);

            message.Subject = "Your Itinerary";

            var builder = new BodyBuilder();

            builder.TextBody = "Here is your itinerary.";

            builder.Attachments.Add("Itinerary.pdf", pdfData);

            message.Body = builder.ToMessageBody();

            return Page();
        }
    }
    
}
