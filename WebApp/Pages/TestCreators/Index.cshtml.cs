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

namespace WebApp.Pages.TestCreators
{
    public class IndexModel : PageModel
    {
        private readonly ITestCreatorRepository testCreatorRepository;

        public IndexModel(ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
        }

        public IList<TestCreator> TestCreator { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var util = new Utils.Utils();

            if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return Redirect("/Error/AuthorizedError");
            }
            else
            {
                TestCreator = testCreatorRepository.GetAll();

                foreach(var creator in TestCreator)
                {
                    creator.Password = util.NotShowPassword(creator.Password);
                }

                return Page();
            }
        }
    }
}
