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

namespace WebApp.Pages.TestInfo
{
    public class IndexModel : PageModel
    {
        private ITestRepository testRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public PaginatedList<Test> TestList { get; set; }
        public String ErrorMessage { get; set; }
        public int PageIndex { get; set; }

        public IndexModel(ITestRepository testRepository, IConfiguration configuration)
        {
            this.testRepository = testRepository;
            this.configuration = configuration;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
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
        }

        public IActionResult OnPost(String keyCode, Guid id, int pageIndex)
        {
            var testList = testRepository.GetAllByBatchForTestTaker();
            var test = testRepository.GetByIdForTestTakerAsync(id).Result;
            int pageSize = configuration.GetValue("PageSize", 10);
            TestList = PaginatedList<Test>.CreateAsync(testList, pageIndex, pageSize);


            if (test.KeyCode != keyCode)
            {
                ErrorMessage = "Wrong keycode.";
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
                return RedirectToPage("./TestTaking", new { batch = test.Batch});

            }

        }
    }
}
