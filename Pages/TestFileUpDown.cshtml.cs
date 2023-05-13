using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ETMP.Pages
{
    public class TestFileUpDownModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public TestFileUpDownModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        public IFormFile FileInput { get; set; }

        public string FileName { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (FileInput == null || FileInput.Length == 0)
            {
                return Page();
            }

            var fileName = Guid.NewGuid().ToString() + ".zip";
            var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await FileInput.CopyToAsync(stream);
            }

            FileName = fileName;

            return Page();
        }
    }
}

