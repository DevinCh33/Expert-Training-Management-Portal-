using ETMP.Models;
using Microsoft.AspNetCore.Identity;
using PuppeteerSharp;

namespace ETMP.Data
{
	public class TrainingSeedData
	{
		public static async Task InitTrainings(ApplicationDbContext context)
		{
            context.Database.EnsureCreated();

            TrainingModel training = new TrainingModel();
            training.TrainingName = "Training 1";
            training.TrainingPrice = 2000;
            training.TrainingCategory = "Category1";
            training.TrainingVenue = "HotelA";
            training.Availability = true;
            training.TrainingDescription = "Training Description";
            training.TrainingStartDateTime = new DateTime(2023, 10, 10);
            training.TrainingEndDateTime = new DateTime(2023, 10, 11);
            context.Trainings.Add(training);

			TrainingModel training2 = new TrainingModel();
			training2.TrainingName = "Training 2";
			training2.TrainingPrice = 1000;
			training2.TrainingCategory = "Category2";
			training2.TrainingVenue = "HotelB";
			training2.Availability = true;
			training2.TrainingDescription = "Training Description";
			training2.TrainingStartDateTime = new DateTime(2023, 10, 10);
			training2.TrainingEndDateTime = new DateTime(2023, 10, 11);
			context.Trainings.Add(training2);

			TrainingModel training3 = new TrainingModel();
			training3.TrainingName = "Training 3";
			training3.TrainingPrice = 500;
			training3.TrainingCategory = "Category3";
			training3.TrainingVenue = "HotelC";
			training3.Availability = true;
			training3.TrainingDescription = "Training Description";
			training3.TrainingStartDateTime = new DateTime(2023, 10, 10);
			training3.TrainingEndDateTime = new DateTime(2023, 10, 11);
			context.Trainings.Add(training3);

			TrainingModel training4 = new TrainingModel();
			training4.TrainingName = "Training 4";
			training4.TrainingPrice = 100;
			training4.TrainingCategory = "Category4";
			training4.TrainingVenue = "HotelD";
			training4.Availability = true;
			training4.TrainingDescription = "Training Description";
			training4.TrainingStartDateTime = new DateTime(2023, 10, 10);
			training4.TrainingEndDateTime = new DateTime(2023, 10, 11);
            context.Trainings.Add(training4);

			await context.SaveChangesAsync();
		}
	}
}
