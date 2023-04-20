using ETMP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ETMP.Pages
{
    [Authorize(Roles = "Admin, Member")]
    public class BrowseTrainingModel : PageModel
    {
        public List <TrainingModel> trainingModels = new List<TrainingModel>();

        public void OnGet()
        {
            TrainingModel trainingModel = new TrainingModel();
            trainingModels = trainingModel.GetTrainingData();

            if (trainingModels != null && trainingModels.Count > 0)
            {
                // Access the first item in the list
                TrainingModel firstTraining = trainingModels[0];
                // Do something with the item
            }
        }
    }
}




