using ETMP.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ETMP.Pages
{
    public class BrowseTrainingModel : PageModel
    {
        public List<TrainingModel> trainings { get; set; }

        
    }
}

