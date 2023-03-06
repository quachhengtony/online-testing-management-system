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

namespace WebApp.Pages.Tests
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly ITestRepository testRepository;
		
        public IList<Test> TestList { get; set; }

		public IndexModel(ILogger<IndexModel> logger, ITestRepository testRepository)
        {
            this.logger = logger;
            this.testRepository = testRepository;
        }

        public async Task OnGetAsync()
        {
            TestList = await testRepository.GetAllAsync();
        }
    }
}
