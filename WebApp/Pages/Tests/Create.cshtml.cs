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
using WebApp.Constants;
using Microsoft.AspNetCore.Http;

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
        public List<string> TestBatches { get; set; } = new();
        [BindProperty]
        public string TestBatchMode { get; set; } = "New";

		public CreateModel(ILogger<IndexModel> logger, ITestRepository testRepository, ITestCategoryRepository testCategoryRepository, IQuestionRepository questionRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.testCategoryRepository = testCategoryRepository;
            this.questionRepository = questionRepository;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] string searchString)
        {
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || HttpContext.Session.GetString("Role") != "Creator")
			{
				return Redirect("/Error/AuthorizedError"); ;
			}
			ViewData["TestCategory"] = new SelectList(await testCategoryRepository.GetAllAsync(), "Id", "Category");
            TestBatches = await testRepository.GetAllUniqueBatchesOfTestCreator(Guid.Parse(HttpContext.Session.GetString("UserId")));
			if (!string.IsNullOrEmpty(searchString))
            {
                QuestionList = await questionRepository.GetAllByContent(searchString);
			}
			else
            {
				QuestionList = await questionRepository.GetAllAsync();
			}
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || HttpContext.Session.GetString("Role") != "Creator")
			{
				return Redirect("/Error/AuthorizedError"); ;
			}
			try
            {
                Test test, testWithSameBatch;
                List<TestQuestion> testQuestionList;
				if (TestBatchMode != "New")
                {
					testWithSameBatch = await testRepository.GetByBatch(TestBatchMode);
					if (testWithSameBatch == null)
					{
						TempData["Status"] = ErrorConstants.Failed;
						TempData["StatusMessage"] = ErrorConstants.SomethingWentWrong;
						return RedirectToPage("./Create");
					}
					test = new Test()
					{
						Id = Guid.NewGuid(),
						Name = CreateTestDTO.Name,
						Batch = testWithSameBatch.Batch,
						Duration = testWithSameBatch.Duration,
						EndTime = testWithSameBatch.EndTime,
						GradeFinalizationDate = testWithSameBatch.GradeFinalizationDate,
						GradeReleaseDate = testWithSameBatch.GradeReleaseDate,
						KeyCode = testWithSameBatch.KeyCode,
						StartTime = testWithSameBatch.StartTime,
						TestCategoryId = testWithSameBatch.TestCategoryId,
						TestCreatorId = Guid.Parse(HttpContext.Session.GetString("UserId")),
					};
					testQuestionList = new();
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
                if (!ModelState.IsValid)
                {
					return Page();
				}
				test = new Test()
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
					TestCreatorId = Guid.Parse(HttpContext.Session.GetString("UserId")),
				};
				testWithSameBatch = await testRepository.GetByBatch(test.Batch);
				if (testWithSameBatch != null)
				{
					var sameKeyCode = testWithSameBatch.KeyCode == test.KeyCode;
					if (sameKeyCode == false)
					{
						TempData["Status"] = ErrorConstants.Failed;
						TempData["StatusMessage"] = ErrorConstants.SameBatchDifferentKeyCode;
						return RedirectToPage("./Create");
					}
				}
				testQuestionList = new();
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
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.SomethingWentWrong;
                logger.LogError($"\nException: {ex.Message}\n\t{ex.InnerException}");
                return Page();
			}
		}

        public IActionResult OnPostAddQuestionsAsync([FromBody] CreateTestQuestionDTO dto)
        {
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || HttpContext.Session.GetString("Role") != "Creator")
			{
				return Redirect("/Error/AuthorizedError"); ;
			}
			foreach (var id in dto.QuestionIds)
            {
				questionIdList.Add(id);
            }
			return new JsonResult(questionIdList);
		}
    }
}
