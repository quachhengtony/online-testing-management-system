using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using WebApp.DTO;
using System.Text.Json;
using WebApp.Constants;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Questions
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> logger;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IQuestionCategoryRepository questionCategoryRepository;

		[BindProperty]
		public CreateQuestionDTO CreateQuestionDTO { get; set; }
		[BindProperty]
		public CreateAnswerDTO CreateAnswerDTO { get; set; }
		public static List<CreateAnswerDTO> CreateAnswerDTOList { get; set; } = new();

		public CreateModel(ILogger<CreateModel> logger, IQuestionRepository questionRepository, IAnswerRepository answerRepository, IQuestionCategoryRepository questionCategoryRepository)
        {
            this.logger = logger;
            this.questionRepository = questionRepository;
            this.answerRepository = answerRepository;
            this.questionCategoryRepository = questionCategoryRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "Creator")
            {
				return Redirect("/Error/AuthorizedError"); ;
			}
            ViewData["QuestionCategory"] = new SelectList(await questionCategoryRepository.GetAllAsync(), "Id", "Category");
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            List<Answer> answerList = new();
            try
            {
				var question = new Question()
				{
					Id = Guid.NewGuid(),
					Content = CreateQuestionDTO.Content,
					QuestionCategoryId = CreateQuestionDTO.QuestionCategoryId,
					QuestionCreatorId = Guid.Parse(HttpContext.Session.GetString("UserId")),
					Weight = CreateQuestionDTO.Weight,
				};
                switch (question.QuestionCategoryId)
                {
                    case 1:
                        foreach (var item in CreateAnswerDTOList)
                        {
                            var answer = new Answer()
                            {
                                Id = item.Id,
                                Content = item.Content,
                                IsCorrect = item.IsCorrect,
                                QuestionId = question.Id
                            };
                            answerList.Add(answer);
                        }
                        break;
                    case 2:
                        foreach (var item in CreateAnswerDTOList)
                        {
                            var answer = new Answer()
                            {
                                Id = item.Id,
                                Content = item.Content,
                                IsCorrect = item.IsCorrect,
                                QuestionId = question.Id
                            };
                            answerList.Add(answer);
                        }
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                question.Answers = answerList;
                questionRepository.Create(question);
                questionRepository.SaveChanges();
                CreateAnswerDTOList.Clear();
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

        public IActionResult OnGetAddAnswer(string type, string content, string isCorrect)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrEmpty(type))
            {
                return Page();
            }

			try
			{
                switch (type)
                {
                    case "1":
                        if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(isCorrect))
                        {
                            return Page();
                        }
                        CreateAnswerDTOList.Add(new CreateAnswerDTO { Id = Guid.NewGuid(), Content = content, IsCorrect = bool.Parse(isCorrect) });
                        break;
                    case "2":
                        if (string.IsNullOrEmpty(isCorrect))
                        {
                            return Page();
                        }
                        if (isCorrect == "true")
                        {
                            CreateAnswerDTOList.Add(new CreateAnswerDTO { Id = Guid.NewGuid(), Content = "True", IsCorrect = true });
                            CreateAnswerDTOList.Add(new CreateAnswerDTO { Id = Guid.NewGuid(), Content = "False", IsCorrect = false });
                        }
                        else
                        {
                            CreateAnswerDTOList.Add(new CreateAnswerDTO { Id = Guid.NewGuid(), Content = "True", IsCorrect = false });
                            CreateAnswerDTOList.Add(new CreateAnswerDTO { Id = Guid.NewGuid(), Content = "False", IsCorrect = true });
                        }
                        break;
                    case "3":
                        break;
                    default:
                        break;
                }
                var answers = JsonSerializer.Serialize(CreateAnswerDTOList, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return new JsonResult(answers);
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
