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
using Microsoft.AspNetCore.Http;
using WebApp.DTO;

namespace WebApp.Pages.TestTakers
{
    public class CreateModel : PageModel
    {
        private readonly ITestTakerRepository testTakerRepository;

        public CreateModel(ITestTakerRepository testTakerRepository)
        {
            this.testTakerRepository = testTakerRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateTestTakerDTO TestTaker { get; set; }

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
                var taker = new TestTaker
                {
                    Id = util.createGuid(),
                    Email = TestTaker.Email,
                    FirstName = TestTaker.FirstName,
                    LastName = TestTaker.LastName,
                    Password = util.RandomPassword(),
                    Username = TestTaker.Username
                };

                testTakerRepository.Create(taker);

                return RedirectToPage("./Index");
            }
        }
    }
}
