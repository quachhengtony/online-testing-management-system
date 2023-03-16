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

namespace WebApp.Pages.TestTakers
{
    public class IndexModel : PageModel
    {
        private readonly ITestTakerRepository testTakerRepository;
        private readonly IConfiguration configuration;

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<TestTaker> TestTakerList { get; set; }


        public IndexModel(ITestTakerRepository testTakerRepository, IConfiguration configuration)
        {
            this.testTakerRepository = testTakerRepository;
            this.configuration = configuration;
        }

        public IList<TestTaker> TestTaker { get;set; }

        public async Task<IActionResult> OnGetAsync(string currentFilter, string searchString, int? pageIndex)
        {
            var util = new Utils.Utils();
            List<TestTaker> testTakers;
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
                    testTakers = testTakerRepository.GetTestTakersByName(searchString);
                    ViewData["Search"] = searchString;
                }
                else
                {
                    testTakers = testTakerRepository.GetAll();
                }

                foreach (var taker in testTakers)
                {
                    taker.Password = util.NotShowPassword(taker.Password);
                }

                TestTakerList = PaginatedList<TestTaker>.CreateAsync(testTakers, pageIndex ?? 1, pageSize);

                return Page();
            }
        }
    }
}
