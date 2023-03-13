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
            HttpContext.Session.SetString("StartTime", DateTime.Now.ToString());
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
                TimeTaken = (byte)((DateTime.Now - DateTime.Parse(HttpContext.Session.GetString("StartTime"))).Minutes + 1),
                Score = GetScore(TestAnswer),
                Feedback = null,
                IsGraded = true,
                Content = JsonSerializer.Serialize(TestAnswer)
            };
            submissionRepository.Create(submission);
            return RedirectToPage("/Submissions/Index");
        }

        public decimal GetScore(Dictionary<Guid, String> testAnswer)
        {
            decimal score = 0.0m;
            decimal totalWeight = 0;
            var questionList = new List<Question>();
            foreach (var key in testAnswer.Keys)
            {
                var question = questionRepository.GetByIdAsync(key).Result;
                questionList.Add(question);
                totalWeight += question.Weight;
            }

            foreach (var pair in testAnswer)
            {
                var question = questionList.Where(q => q.Id == pair.Key).FirstOrDefault();
                if (question.QuestionCategoryId == 1)
                {
                    int quesScore = 0;
                    int totalCorrectAns = question.Answers.Where(a => a.IsCorrect).ToList().Count;
                    var ansIdList = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToList();
                    foreach (var ansId in pair.Value.Split(','))
                    {
                        if (ansIdList.Contains(Guid.Parse(ansId)))
                        {
                            quesScore++;
                        } else
                        {
                            quesScore--;
                        }
                    }
                    quesScore = quesScore < 0 ? 0 : quesScore;
                    score += (decimal)quesScore / (decimal)totalCorrectAns * (question.Weight / totalWeight) * 100.0m;
                } else if (question.QuestionCategoryId == 2)
                {
                    if (question.Answers.Where(a => a.IsCorrect).First().Id.ToString() == pair.Value)
                    {
                        score += question.Weight / totalWeight * 100.0m;
                    }
                }
            }

            return decimal.Round(score, 2, MidpointRounding.AwayFromZero); ;
        }
    }
}
