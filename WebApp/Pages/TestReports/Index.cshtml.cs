using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using WebApp.Models;
using Repositories;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestReports
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ITestRepository testRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Test> TestList { get; set; }

        public List<decimal> AveScore { get; set; } = new List<decimal>();

        public IndexModel(ILogger<IndexModel> logger, ITestRepository testRepository, ISubmissionRepository submissionRepository, IConfiguration configuration)
        {
            this.logger = logger;
            this.testRepository = testRepository;
            this.submissionRepository = submissionRepository;
            this.configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            if (HttpContext.Session.GetString("Role") != "Creator")
            {
                return Redirect("/Error/AuthorizedError"); ;
            }
            Guid creatorId = new Guid(HttpContext.Session.GetString("UserId"));
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
                testList = testRepository.GetBySearchForTestCreator(searchString, creatorId);
            }
            else
            {
                testList = testRepository.GetAllByBatchForTestCreator(creatorId);
            }
            TestList = PaginatedList<Test>.CreateAsync(testList, pageIndex ?? 1, pageSize);
            foreach (var t in TestList)
            {
                AveScore.Add(Math.Round(submissionRepository.GetAverageScoreByBatch(t.Batch), 2));
            }

            return Page();
        }
    }
}
