using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;

namespace WebApp.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> logger;
        private readonly IQuestionRepository questionRepository;

        [BindProperty]
        public Question Question { get; set; }

        public DeleteModel(ILogger<DeleteModel> logger, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
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
                if (Question != null)
                {
                    questionRepository.Delete(Question);
                    questionRepository.SaveChanges();
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                logger.LogInformation($"\nException: {ex.Message}\n\t{ex.InnerException}");
                return Page();
            }
        }
    }
}
