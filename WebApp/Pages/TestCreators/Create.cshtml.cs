using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Repositories.Interfaces;
using WebApp.DTO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestCreators
{
    public class CreateModel : PageModel
    {
        private readonly ITestCreatorRepository testCreatorRepository;

        public CreateModel(ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateTestCreatorDTO TestCreator { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var util = new Utils.Utils();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if (HttpContext.Session.GetString("Role") != "Admin")
            {
                return Redirect("/Error/AuthorizedError"); ;
            }
            else
            {
                var creator = new TestCreator
                {
                    Id = util.createGuid(),
                    Email = TestCreator.Email,
                    FirstName = TestCreator.FirstName,
                    LastName = TestCreator.LastName,
                    Password = util.RandomPassword(),
                    Username = TestCreator.Username
                };

                testCreatorRepository.Create(creator);

                return RedirectToPage("./Index");
            }
        }
    }
}
