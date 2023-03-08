using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using WebApp.DTO;

namespace WebApp.Pages.Tests
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ITestRepository testRepository;
        private readonly ITestCategoryRepository testCategoryRepository;
        private readonly IQuestionRepository questionRepository;
        private static List<string> questionIdList = new();

        [BindProperty]
		public Test Test { get; set; }
        [BindProperty]
        public CreateTestDTO CreateTestDTO { get; set; }
        public List<Question> QuestionList { get; set; } = new();

		public CreateModel(ILogger<IndexModel> logger, ITestRepository testRepository, ITestCategoryRepository testCategoryRepository, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.testCategoryRepository = testCategoryRepository;
            this.questionRepository = questionRepository;
        }

        public async Task OnGetAsync([FromQuery] string searchString)
        {
            ViewData["TestCategory"] = new SelectList(await testCategoryRepository.GetAllAsync(), "Id", "Category");
            if (!string.IsNullOrEmpty(searchString))
            {
                QuestionList = await questionRepository.GetAllByContent(searchString);
			}
			else
            {
				QuestionList = await questionRepository.GetAllAsync();
			}
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
				if (!ModelState.IsValid)
				{
                    return Page();
				}
				var test = new Test()
				{
					Id = Guid.NewGuid(),
					Name = CreateTestDTO.Name,
					Batch = CreateTestDTO.Batch,
					Duration = CreateTestDTO.Duration,
					EndTime = CreateTestDTO.EndTime,
					GradeFinalizationDate = CreateTestDTO.GradeFinalizationDate,
					GradeReleaseDate = CreateTestDTO.GradeReleaseDate,
					KeyCode = CreateTestDTO.KeyCode,
					StartTime = CreateTestDTO.StartTime,
					TestCategoryId = CreateTestDTO.TestCategoryId,
					TestCreatorId = Guid.Parse("AEC1060F-F755-457E-B6B4-9C2EE79C6214"),
				};
				List<TestQuestion> testQuestionList = new();
                foreach (var question in questionIdList)
				{
                    testQuestionList.Add(new TestQuestion()
                    {
                        QuestionId = Guid.Parse(question),
                        TestId = test.Id
                    });
				}
                test.TestQuestions = testQuestionList;
				testRepository.Create(test);
                testRepository.SaveChanges();
				questionIdList.Clear();
				return RedirectToPage("./Index");
			}
            catch (Exception ex)
            {
                logger.LogInformation($"\nException: {ex.Message}\n\t{ex.InnerException}");
                return Page();
			}
		}

        public IActionResult OnPostAddQuestionsAsync([FromBody] CreateTestQuestionDTO dto)
        {
            foreach (var id in dto.QuestionIds)
            {
				questionIdList.Add(id);
            }
			return new JsonResult(questionIdList);
		}
    }
}
