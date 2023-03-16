using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using WebApp.Constants;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Tests
{
    public class DeleteModel : PageModel
    {
		private readonly ILogger<IndexModel> logger;
		private readonly ITestRepository testRepository;

		[BindProperty]
		public Test Test { get; set; }

		public DeleteModel(ILogger<IndexModel> logger, ITestRepository testRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || HttpContext.Session.GetString("Role") != "Creator")
			{
				return Redirect("/Error/AuthorizedError"); ;
			}
			if (id == null)
            {
                return NotFound();
            }
            Test = await testRepository.GetByIdAsync(Guid.Parse(id));
            if (Test == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || HttpContext.Session.GetString("Role") != "Creator")
			{
				return Redirect("/Error/AuthorizedError"); ;
			}
			if (id == null)
            {
                return NotFound();
            }
            try
            {
                Test = await testRepository.GetByIdAsync(Guid.Parse(id));
                if (Test != null)
                {
                    testRepository.Delete(Test);
                    testRepository.SaveChanges();
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
				logger.LogError($"\nException: {ex.Message}\n\t{ex.InnerException}");
				TempData["Status"] = ErrorConstants.Failed;
				TempData["StatusMessage"] = ErrorConstants.SomethingWentWrong;
				return Page();
            }
        }
    }
}
