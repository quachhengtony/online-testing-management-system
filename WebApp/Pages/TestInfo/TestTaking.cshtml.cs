using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Repositories.Interfaces;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestInfo
{
    public class TestTakingModel : PageModel
    {
        private ITestRepository testRepository;
        private IQuestionRepository questionRepository;
        private ISubmissionRepository submissionRepository;

        public TestTakingModel(ITestRepository testRepository, IQuestionRepository questionRepository, ISubmissionRepository submissionRepository)
        {
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.submissionRepository = submissionRepository;
        }

        public List<Question> QuestionList { get; set; } = new();

        public async void OnGet(string batch)
        {
            var testNames = testRepository.GetTestNamesByBatchForTestTaker(batch).Result;
            var name = testNames[new Random().Next(testNames.Count)];


            var test = testRepository.GetByTestNameAndBatchForTestTakerAsync(batch, name).Result;
            foreach (var testQuestion in test.TestQuestions)
            {
                QuestionList.Add(testQuestion.Question);
            }
            var x = QuestionList;
            HttpContext.Session.SetString("QuestionListId", JsonSerializer.Serialize(QuestionList.Select(i => i.Id).ToList()));
            HttpContext.Session.SetString("TestId", test.Id.ToString());
            HttpContext.Session.SetString("GradeFinalDate", test.GradeFinalizationDate.ToString());
        }

        public IActionResult OnPost()
        {
            var QuestionListId = JsonSerializer.Deserialize<List<Guid>>(HttpContext.Session.GetString("QuestionListId"));
            var TestAnswer = new Dictionary<Guid, String>();

            foreach (var id in QuestionListId)
            {
                TestAnswer.Add(id, Request.Form[id.ToString()]);
            }
            var submission = new Submission()
            {
                Id = Guid.NewGuid(),
                TestTakerId = Guid.Parse("FEABE0FB-4518-4E94-9F70-2D2616D1BF25"),
                TestId = Guid.Parse(HttpContext.Session.GetString("TestId")),
                SubmittedDate = DateTime.Now,
                GradedDate = DateTime.Parse(HttpContext.Session.GetString("GradeFinalDate")),
                TimeTaken = 50,
                Score = 0,
                Feedback = null,
                IsGraded = false,
                Content = JsonSerializer.Serialize(TestAnswer)
            };
            submissionRepository.Create(submission);
            return RedirectToPage("/Submissions/Index");
        }
    }
}
