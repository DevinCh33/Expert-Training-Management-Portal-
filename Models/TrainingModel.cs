using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ETMP.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public string? TrainingName { get; set; }
        public int TrainingPrice { get; set; }
        public string? TrainingItinerary { get; set; }
        public string? TrainingVenue { get; set; }
        public string? TrainingCategory { get; set; }
        public bool Availability { get; set; }
        public string? TrainingDescription { get; set; }

        public List<TrainingModel> GetTrainingData()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-ETMP-53bc9b9d-9d6a-45d4-8429-2a2761773502;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string sqlQuerry = "SELECT * FROM [Identity].[Trainings]";

            SqlCommand cmd = new SqlCommand(sqlQuerry, con);

            SqlDataReader dr = cmd.ExecuteReader();

            List<TrainingModel> trainingList = new List<TrainingModel>();
            while (dr.Read())
            {
                TrainingModel trainingModel = new TrainingModel();
                trainingModel.Id = int.Parse((dr["Id"].ToString()));
                trainingModel.TrainingName = dr["TrainingName"].ToString();
                trainingModel.TrainingPrice = int.Parse((dr["TrainingPrice"].ToString()));
                trainingModel.TrainingItinerary = dr["TrainingItinerary"].ToString();
                trainingModel.TrainingVenue = dr["TrainingVenue"].ToString();
                trainingModel.TrainingCategory = dr["TrainingCategory"].ToString();
                trainingModel.Availability = bool.Parse(dr["Availability"].ToString());
                trainingModel.TrainingDescription = dr["TrainingDescription"].ToString();
                trainingList.Add(trainingModel);
            }
            con.Close();
            return trainingList;
        }


    }

}

