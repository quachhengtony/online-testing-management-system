using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace WebApp.Pages.Tests
{
    public class DetailsModel : PageModel
    {
		private readonly ILogger<IndexModel> logger;
		private readonly ITestRepository testRepository;
		
        public Test Test { get; set; }

		public DetailsModel(ILogger<IndexModel> logger, ITestRepository testRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
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
    }
}
