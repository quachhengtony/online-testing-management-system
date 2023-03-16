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
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Pages.TestInfo
{
    public class TestTakingModel : PageModel
    {
        private ITestRepository testRepository;
        private IQuestionRepository questionRepository;
        private ISubmissionRepository submissionRepository;
        private readonly IConfiguration configuration;

        public PaginatedList<Question> QuestionList { get; set; }
        public int PageIndex { get; set; }
        public List<String> CheckedAnswer { get; set; } = new List<String>();
        public int Duration { get; set; }

        public TestTakingModel(ITestRepository testRepository, IQuestionRepository questionRepository, ISubmissionRepository submissionRepository, IConfiguration configuration)
        {
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
            this.submissionRepository = submissionRepository;
            this.configuration = configuration;
        }


        public async void OnGet(string batch)
        {
            var questionList = new List<Question>();
            //int pageSize = configuration.GetValue("PageSize", 10);
            int pageSize = 2;

            if (HttpContext.Session.GetString("QuestionListId") == null)
            {
                var testNames = testRepository.GetTestNamesByBatchForTestTaker(batch).Result;
                var name = testNames[new Random().Next(testNames.Count)];
                var test = testRepository.GetByTestNameAndBatchForTestTakerAsync(batch, name).Result;
                foreach (var testQuestion in test.TestQuestions)
                {
                    questionList.Add(testQuestion.Question);
                }
                
                HttpContext.Session.SetString("QuestionListId", JsonSerializer.Serialize(questionList.Select(i => i.Id).ToList()));
                HttpContext.Session.SetString("TestId", test.Id.ToString());
                HttpContext.Session.SetString("GradeFinalDate", test.GradeFinalizationDate.ToString());
                HttpContext.Session.SetString("StartTime", DateTime.Now.ToString());
                HttpContext.Session.SetString("TestContent", JsonSerializer.Serialize(new Dictionary<Guid, String>()));
                Duration = test.Duration * 60;

                var submission = new Submission()
                {
                    Id = Guid.NewGuid(),
                    TestTakerId = Guid.Parse("FEABE0FB-4518-4E94-9F70-2D2616D1BF25"),
                    TestId = Guid.Parse(HttpContext.Session.GetString("TestId")),
                    SubmittedDate = DateTime.Now,
                    GradedDate = DateTime.Parse(HttpContext.Session.GetString("GradeFinalDate")),
                    TimeTaken =  Byte.MinValue,
                    Score = 0.0m,
                    Feedback = null,
                    IsGraded = false,
                    Content = ""
                };
                HttpContext.Session.SetString("CurrentSubmissionId", submission.Id.ToString());
                submissionRepository.Create(submission);
            }
            else
            {
                //error
                var testId = Guid.Parse(HttpContext.Session.GetString("TestId"));
                foreach (var testQuestion in testRepository.GetByIdAsync(testId).Result.TestQuestions)
                {
                    questionList.Add(testQuestion.Question);
                }
            }
            //init CheckedAnswer
            for (int i = 0; i < pageSize; i++)
            {
                CheckedAnswer.Add("");
            }

            QuestionList = PaginatedList<Question>.CreateAsync(questionList, 1, pageSize);
            PageIndex = 1;
            
        }

        public IActionResult OnPostPrevPage()
        {
            //int pageSize = configuration.GetValue("PageSize", 10);
            int pageSize = 2;
            var QuestionListId = JsonSerializer.Deserialize<List<Guid>>(HttpContext.Session.GetString("QuestionListId"));
            var TestAnswer = JsonSerializer.Deserialize<Dictionary<Guid, String>> (HttpContext.Session.GetString("TestContent"));
            //load answer for prev page
            var questionList = new List<Question>();
            foreach (var quesId in QuestionListId)
            {
                questionList.Add(questionRepository.GetByIdAsync(quesId).Result);
            }
            PageIndex = Int32.Parse(Request.Form["PageIndex"]) - 1;
            QuestionList = PaginatedList<Question>.CreateAsync(questionList, PageIndex, pageSize);
            //store answer information
            foreach (var ques in PaginatedList<Question>.CreateAsync(questionList, PageIndex + 1, pageSize))
            {
                if (TestAnswer.ContainsKey(ques.Id))
                {
                    TestAnswer[ques.Id] = String.IsNullOrEmpty(Request.Form[ques.Id.ToString()]) ? "" : Request.Form[ques.Id.ToString()];
                }
                else
                {
                    TestAnswer.Add(ques.Id, String.IsNullOrEmpty(Request.Form[ques.Id.ToString()]) ? "" : Request.Form[ques.Id.ToString()]);
                }

            }
            HttpContext.Session.SetString("TestContent", JsonSerializer.Serialize(TestAnswer));

            //render checked answer
            foreach (var q in QuestionList)
            {
                if (TestAnswer.ContainsKey(q.Id))
                {
                    CheckedAnswer.Add(TestAnswer[q.Id]);
                } else
                {
                    CheckedAnswer.Add("");
                }
            }
            //update duration
            Duration = Int32.Parse(Request.Form["Duration"]);
            return Page();
        }

        public IActionResult OnPostNextPage()
        {
            //int pageSize = configuration.GetValue("PageSize", 10);
            int pageSize = 2;
            var QuestionListId = JsonSerializer.Deserialize<List<Guid>>(HttpContext.Session.GetString("QuestionListId"));
            var TestAnswer = JsonSerializer.Deserialize<Dictionary<Guid, String>>(HttpContext.Session.GetString("TestContent"));
            //load answer for next page
            var questionList = new List<Question>();
            foreach (var quesId in QuestionListId)
            {
                questionList.Add(questionRepository.GetByIdAsync(quesId).Result);
            }
            PageIndex = Int32.Parse(Request.Form["PageIndex"]) + 1;
            QuestionList = PaginatedList<Question>.CreateAsync(questionList, PageIndex, pageSize);
            //store answer information
            foreach (var ques in PaginatedList<Question>.CreateAsync(questionList, PageIndex - 1, pageSize))
            {
                if (TestAnswer.ContainsKey(ques.Id))
                {
                    TestAnswer[ques.Id] = String.IsNullOrEmpty(Request.Form[ques.Id.ToString()]) ? "" : Request.Form[ques.Id.ToString()];
                }
                else
                {
                    TestAnswer.Add(ques.Id, String.IsNullOrEmpty(Request.Form[ques.Id.ToString()]) ? "" : Request.Form[ques.Id.ToString()]);
                }
            }
            HttpContext.Session.SetString("TestContent", JsonSerializer.Serialize(TestAnswer));

            //render checked answer
            foreach (var q in QuestionList)
            {
                if (TestAnswer.ContainsKey(q.Id))
                {
                    CheckedAnswer.Add(TestAnswer[q.Id]);
                } else
                {
                    CheckedAnswer.Add("");
                }
                
            }
            //update duration
            string durationStr = Request.Form["Duration"];
            Duration = Int32.Parse(durationStr);

            return Page();
        }

        public IActionResult OnPostSubmit()
        {
            var QuestionListId = JsonSerializer.Deserialize<List<Guid>>(HttpContext.Session.GetString("QuestionListId"));
            var TestAnswer = JsonSerializer.Deserialize<Dictionary<Guid, String>>(HttpContext.Session.GetString("TestContent"));

            var submission = new Submission()
            {
                Id = Guid.Parse(HttpContext.Session.GetString("CurrentSubmissionId")),
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
            submissionRepository.Update(submission);

            HttpContext.Session.Remove("QuestionListId");
            HttpContext.Session.Remove("TestId");
            HttpContext.Session.Remove("GradeFinalDate");
            HttpContext.Session.Remove("StartTime");
            HttpContext.Session.Remove("CurrentSubmissionId");
            HttpContext.Session.Remove("TestContent");
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
                        if (ansId.Trim() != "" && ansIdList.Contains(Guid.Parse(ansId)))
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
