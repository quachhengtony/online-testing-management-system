using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using WebApp.DTO;

namespace WebApp.Pages.Tests
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> logger;
        private readonly ITestRepository testRepository;
        private readonly ITestCategoryRepository testCategoryRepository;

        [BindProperty]
        public Test Test { get; set; }
        [BindProperty]
        public UpdateTestDTO UpdateTestDTO { get; set; } = new();

		public EditModel(ILogger<EditModel> logger, ITestRepository testRepository, ITestCategoryRepository testCategoryRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.testCategoryRepository = testCategoryRepository;
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
            ViewData["TestCategory"] = new SelectList(await testCategoryRepository.GetAllAsync(), "Id", "Category", Test.TestCategoryId);
            UpdateTestDTO.StartTime = Test.StartTime;
            UpdateTestDTO.EndTime = Test.EndTime;
            UpdateTestDTO.GradeReleaseDate = Test.GradeReleaseDate;
            UpdateTestDTO.GradeFinalizationDate = Test.GradeFinalizationDate;
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
                Test = await testRepository.GetByIdAsync(Test.Id);
                Test.Batch = UpdateTestDTO.Batch;
                Test.Duration = UpdateTestDTO.Duration;
                Test.EndTime = Test.EndTime;
                Test.GradeFinalizationDate = UpdateTestDTO.GradeFinalizationDate;
                Test.GradeReleaseDate = UpdateTestDTO.GradeReleaseDate;
                Test.KeyCode = UpdateTestDTO.KeyCode;
                Test.Name = UpdateTestDTO.Name;
                Test.StartTime = UpdateTestDTO.StartTime;
                Test.TestCategoryId = UpdateTestDTO.TestCategoryId;
                testRepository.Update(Test);
                testRepository.SaveChanges();
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
