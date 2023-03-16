using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using WebApp.DTO;
using WebApp.Constants;

namespace WebApp.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly IQuestionCategoryRepository questionCategoryRepository;

        [BindProperty]
        public Question Question { get; set; }
        [BindProperty]
        public UpdateQuestionDTO UpdateQuestionDTO { get; set; }

        public EditModel(ILogger<EditModel> logger, IQuestionRepository questionRepository, IQuestionCategoryRepository questionCategoryRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
            this.questionCategoryRepository = questionCategoryRepository;
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
            ViewData["QuestionCategory"] = new SelectList(await questionCategoryRepository.GetAllAsync(), "Id", "Category", Question.QuestionCategoryId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                Question = await questionRepository.GetByIdAsync(Question.Id);
                Question.Content = UpdateQuestionDTO.Content;
                Question.Weight = UpdateQuestionDTO.Weight;
                questionRepository.Update(Question);
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
