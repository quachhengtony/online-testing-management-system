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
using Microsoft.Extensions.Configuration;
using WebApp.Models;
using Microsoft.AspNetCore.Http;
using WebApp.Constants;

namespace WebApp.Pages.TestInfo
{
    public class IndexModel : PageModel
    {
        private ITestRepository testRepository;
        private ISubmissionRepository submissionRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public PaginatedList<Test> TestList { get; set; }
        public String ErrorMessage { get; set; }
        public int PageIndex { get; set; }

        public IndexModel(ITestRepository testRepository, ISubmissionRepository submissionRepository, IConfiguration configuration)
        {
            this.testRepository = testRepository;
            this.configuration = configuration;
            this.submissionRepository = submissionRepository;
        }

        public async Task<IActionResult> OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Role")) || 
                !HttpContext.Session.GetString("Role").Equals("Taker"))
            {
                return Redirect("/Error/AuthorizedError");
            }

            List<Test> testList;
            int pageSize = configuration.GetValue("PageSize", 10);
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                testList = testRepository.GetBySearchForTestTaker(searchString);
            }
            else
            {
                testList = testRepository.GetAllByBatchForTestTaker();
            }
            TestList = PaginatedList<Test>.CreateAsync(testList, pageIndex ?? 1, pageSize);
            PageIndex = pageIndex == null ? 1 : pageIndex.Value;
            return Page();
        }

        public IActionResult OnPost(String keyCode, Guid id, int pageIndex)
        {
            var testList = testRepository.GetAllByBatchForTestTaker();
            var test = testRepository.GetByIdForTestTakerAsync(id).Result;
            int pageSize = configuration.GetValue("PageSize", 10);
            TestList = PaginatedList<Test>.CreateAsync(testList, pageIndex, pageSize);


            if (!IsValid(keyCode, test))
            {
                return Page();
            } else
            {

                if (HttpContext.Session.GetString("CurrentSubmissionId") != null)
                {
                    HttpContext.Session.Remove("QuestionListId");
                    HttpContext.Session.Remove("TestId");
                    HttpContext.Session.Remove("GradeFinalDate");
                    HttpContext.Session.Remove("StartTime");
                    HttpContext.Session.Remove("CurrentSubmissionId");
                    HttpContext.Session.Remove("TestContent");

                }
                HttpContext.Session.SetString("TestJoinedId", test.Batch);
                return RedirectToPage("./TestTaking", new { batch = test.Batch});

            }

        }

        private bool IsValid(String keyCode, Test test)
        {
            if (test.KeyCode != keyCode)
            {
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.InvalidKeyCode;
                return false;
            }
            if (DateTime.Compare(test.StartTime, DateTime.Now) > 0)
            {
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.TestNotStart;
                return false;
            }
            if (DateTime.Compare(test.EndTime, DateTime.Now) < 0)
            {
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.TestEnded;
                return false;
            }
            if (submissionRepository.IsBatchTakenByTestTaker(Guid.Parse(HttpContext.Session.GetString("UserId")), test.Batch))
            {
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.InvalidRetakeTest;
                return false;
            }

            return true;
        }
    }
}
