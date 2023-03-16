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

namespace WebApp.Pages.SubmissionPage
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> logger;
        private readonly ISubmissionRepository submissionRepository;
        private readonly IQuestionRepository questionRepository;

        public List<Question> QuestionList { get; set; } = new();
        public Submission Submission { get; set; }

        public DetailsModel(ILogger<DetailsModel> logger, ISubmissionRepository submissionRepository, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.submissionRepository = submissionRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Submission = submissionRepository.GetById(id);
            if (Submission == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}