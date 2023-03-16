using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        private ISubmissionRepository submissionRepository;
        private readonly IConfiguration configuration;


        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Submission> SubmissionList { get; set; }

        public IndexModel(ISubmissionRepository submissionRepository, IConfiguration configuration)
        {
            this.submissionRepository = submissionRepository;
            this.configuration = configuration;
        }


        public async Task OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            List<Submission> submissionList;
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
                submissionList = submissionRepository.GetByTestTakerIdAndBatchAsync(Guid.Parse("411DEE92-915C-44DE-A892-61A468A27985"), searchString).Result;
            }
            else
            {
                submissionList = submissionRepository.GetByTestTakerIdAsync(Guid.Parse("411DEE92-915C-44DE-A892-61A468A27985")).Result; ;
            }
            SubmissionList = PaginatedList<Submission>.CreateAsync(submissionList, pageIndex ?? 1, pageSize);
        }
    }
}
