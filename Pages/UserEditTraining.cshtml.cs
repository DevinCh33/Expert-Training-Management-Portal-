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

                foreach(var i in _trainingList)
                {
                    if(i.Id == id)
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

            if(EditTraining.TrainingStartDateTime >= EditTraining.TrainingEndDateTime)
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


        public IActionResult MyAction()
        {
            // Connect to the database and retrieve the data
            string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";
            string query = "SELECT TrainingName, TrainingPrice, TrainingVenue, TrainingCategory, Availability, TrainingDescription, TrainingImgURL FROM Identity.Trainings";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Create a new PDF document
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream("output.pdf", FileMode.Create));
            document.Open();

            // Create a new table with 7 columns
            PdfPTable table = new PdfPTable(7);

            // Add column headers to the table
            table.AddCell("Training Name");
            table.AddCell("Training Price");
            table.AddCell("Training Venue");
            table.AddCell("Training Category");
            table.AddCell("Availability");
            table.AddCell("Training Description");
            table.AddCell("Training Image URL");

            // Add data rows to the table
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

            // Add the table to the document and close the document
            document.Add(table);
            document.Close();

            // Return the PDF file as a file download
            byte[] fileBytes = System.IO.File.ReadAllBytes("output.pdf");
            return File(fileBytes, "application/pdf", "output.pdf");
        }


    }

    
}
