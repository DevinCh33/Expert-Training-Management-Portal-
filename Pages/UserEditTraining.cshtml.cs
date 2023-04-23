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
    }
}
