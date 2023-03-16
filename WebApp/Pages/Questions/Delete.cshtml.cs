using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Constants;

namespace WebApp.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly ITestRepository testRepository;
        private readonly ITestQuestionRepository testQuestionRepository;

        [BindProperty]
        public Question Question { get; set; }

        public DeleteModel(ILogger<DeleteModel> logger, IQuestionRepository questionRepository, ITestRepository testRepository, ITestQuestionRepository testQuestionRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
            this.testRepository = testRepository;
            this.testQuestionRepository = testQuestionRepository;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Question = await questionRepository.GetByIdAsync(Guid.Parse(id));
            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                Question = await questionRepository.GetByIdAsync(Guid.Parse(id));
                if (Question == null)
                {
                    return Page();
                }
                var testGuids = await testQuestionRepository.GetAllTestsByQuestionId(Question.Id);
                if (!testGuids.Any())
                {
                    questionRepository.Delete(Question);
                    questionRepository.SaveChanges();
                    return RedirectToPage("./Index");
                }
                foreach (var testGuid in testGuids)
                {
                    var isDue = await testRepository.IsDue(testGuid);
                    if (isDue == true)
                    {
                        return Page();
                    }
                }
                questionRepository.Delete(Question);
                questionRepository.SaveChanges();
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
