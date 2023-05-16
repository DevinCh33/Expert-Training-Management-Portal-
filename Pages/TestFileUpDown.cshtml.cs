using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PuppeteerSharp;
using ETMP.Data;
using ETMP.Models;

namespace ETMP.Pages
{
    public class TestFileUpDownModel : PageModel
    {
        public TrainingModel Training { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TestFileUpDownModel> _logger;
        public TestFileUpDownModel(ApplicationDbContext context, ILogger<TestFileUpDownModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var training = _context.Trainings.FirstOrDefault(t => t.TrainingName == "qweqwe");
            
            // Bind the retrieved training data to the model properties
            Training = training;

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
    }

    
}

