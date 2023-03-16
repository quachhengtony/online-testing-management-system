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
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;

        public Question Question { get; set; }
        public List<Answer> AnswerList { get; set; }

        public DetailsModel(ILogger<DetailsModel> logger, IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
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
            Question = await questionRepository.GetByIdAsync(Guid.Parse(id));
            AnswerList = await answerRepository.GetAllByQuestionId(Guid.Parse(id));
            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
