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
using Repositories;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Tests
{
    public class DetailsModel : PageModel
    {
		private readonly ILogger<IndexModel> logger;
		private readonly ITestRepository testRepository;
        private readonly IQuestionRepository questionRepository;

        public List<Question> QuestionList { get; set; } = new();
        public Test Test { get; set; }

        public DetailsModel(ILogger<IndexModel> logger, ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
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
    }
}
