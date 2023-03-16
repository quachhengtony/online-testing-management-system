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
using System;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.SubmissionPage
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ISubmissionRepository submissionRepository;
        private readonly ITestRepository testRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public List<Submission> SubmissionList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ISubmissionRepository submissionRepository, ITestRepository testRepository, IConfiguration configuration)
        {
            this.logger = logger;
            this.submissionRepository = submissionRepository;
            this.configuration = configuration;
            this.testRepository = testRepository;
        }

        public async Task<IActionResult> OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            if (HttpContext.Session.GetString("Role") != "Creator")
            {
                return Redirect("/Error/AuthorizedError"); ;
            }
            int pageSize = configuration.GetValue("PageSize", 10);
            Guid creatorId = new Guid(HttpContext.Session.GetString("UserId"));
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            List<Test> testList = await testRepository.GetAllByCreatorId(creatorId);
            SubmissionList = new List<Submission>();
            if (testList != null && testList.Count != 0)
                foreach (var test in testList)
                { SubmissionList.AddRange(submissionRepository.GetByTestId(test.Id)); }
            if (!string.IsNullOrEmpty(searchString) && SubmissionList.Count != 0)
            {
                SubmissionList = SubmissionList.FindAll(s => s.TestId.ToString().Contains(searchString) || s.TestTakerId.ToString().Contains(searchString));
            }
            SubmissionList = PaginatedList<Submission>.CreateAsync(SubmissionList, pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}