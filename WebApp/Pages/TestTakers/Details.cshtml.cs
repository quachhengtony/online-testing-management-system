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

namespace WebApp.Pages.TestTakers
{
    public class DetailsModel : PageModel
    {
        private readonly ITestTakerRepository testTakerRepository;

        public DetailsModel(ITestTakerRepository testTakerRepository)
        {
            this.testTakerRepository = testTakerRepository;
        }

        public TestTaker TestTaker { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return Redirect("/Error/AuthorizedError");
            }
            else if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return Redirect("/Error/AuthorizedError");
            }
            else
            {
                TestTaker = testTakerRepository.GetById(id.Value);

                if (TestTaker == null)
                {
                    return Redirect("/Error/AuthorizedError");
                }
                return Page();
            }
        }
    }
}
