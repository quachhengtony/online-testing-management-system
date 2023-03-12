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
        public List<Submission> submissionList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ISubmissionRepository submissionRepository, ITestRepository testRepository, IConfiguration configuration)
        {
            this.logger = logger;
            this.submissionRepository = submissionRepository;
            this.configuration = configuration;
            this.testRepository = testRepository;
        }

        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            int pageSize = configuration.GetValue("PageSize", 10);
            Guid creatorId = new Guid("411DEE92-915C-44DE-A892-61A468A27985");
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
            submissionList = new List<Submission>();
            if (testList != null && testList.Count != 0)
                foreach (var test in testList)
                { submissionList.AddRange(submissionRepository.GetByTestId(test.Id)); }
            if (!string.IsNullOrEmpty(searchString) && submissionList.Count != 0)
            {
                submissionList = submissionList.FindAll(s => s.TestId.ToString().Contains(searchString) || s.TestTakerId.ToString().Contains(searchString));
            }
            submissionList = PaginatedList<Submission>.CreateAsync(submissionList, pageIndex ?? 1, pageSize);
        }
    }
}