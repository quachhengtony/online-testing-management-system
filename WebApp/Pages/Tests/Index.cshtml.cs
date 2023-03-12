using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using WebApp.Models;
using Repositories;

namespace WebApp.Pages.Tests
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ITestRepository testRepository;
        private readonly IConfiguration configuration;

		public string NameSort { get; set; }
		public string DateSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }
		public PaginatedList<Test> TestList { get; set; }

		public IndexModel(ILogger<IndexModel> logger, ITestRepository testRepository, IConfiguration configuration)
        {
            this.logger = logger;
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
				testList = await testRepository.GetAllByName(searchString);
			}
			else
			{
				testList = await testRepository.GetAllAsync();
			}
			TestList = PaginatedList<Test>.CreateAsync(testList, pageIndex ?? 1, pageSize);
        }
    }
}
