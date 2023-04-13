using ETMP.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETMP.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<TrainingModel> Trainings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}