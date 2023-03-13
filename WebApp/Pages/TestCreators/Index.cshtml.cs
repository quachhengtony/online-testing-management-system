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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApp.Models;

namespace WebApp.Pages.TestCreators
{
    public class IndexModel : PageModel
    {
        private readonly ITestCreatorRepository testCreatorRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<TestCreator> TestCreatorList { get; set; }

        public IndexModel(ITestCreatorRepository testCreatorRepository, IConfiguration configuration)
        {
            this.testCreatorRepository = testCreatorRepository;
            this.configuration = configuration;
        }

        public IList<TestCreator> TestCreator { get;set; }

        public async Task<IActionResult> OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            var util = new Utils.Utils();
            List<TestCreator> testCreators;
            int pageSize = configuration.GetValue("PageSize", 10);

            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return Redirect("/Error/AuthorizedError");
            }
            else
            {
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
                    testCreators = testCreatorRepository.GetTestCreatorsByName(searchString);
                    ViewData["Search"] = searchString;
                }
                else
                {
                    testCreators = testCreatorRepository.GetAll();
                }

                foreach (var creator in testCreators)
                {
                    creator.Password = util.NotShowPassword(creator.Password);
                }

                TestCreatorList = PaginatedList<TestCreator>.CreateAsync(testCreators, pageIndex ?? 1, pageSize);

                return Page();
            }
           
        }
    }
}
