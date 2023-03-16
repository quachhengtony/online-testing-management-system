using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Repositories.Interfaces;
using WebApp.DTO;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestTakers
{
    public class EditModel : PageModel
    {
        private readonly ITestTakerRepository testTakerRepository;

        public EditModel(ITestTakerRepository testTakerRepository)
        {
            this.testTakerRepository = testTakerRepository;
        }

        [BindProperty]
        public CreateTestTakerDTO TestTakerDTO { get; set; }

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
                TestTaker taker = testTakerRepository.GetById(id.Value);

                TestTakerDTO = new CreateTestTakerDTO
                {
                    Id = taker.Id,
                    Email = taker.Email,
                    FirstName = taker.FirstName,
                    LastName = taker.LastName,
                    Username = taker.Username
                };

                if (TestTakerDTO == null)
                {
                    return Redirect("/Error/AuthorizedError");
                }
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
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
                TestTaker taker = testTakerRepository.GetById(TestTakerDTO.Id);

                taker.FirstName = TestTakerDTO.FirstName;
                taker.Email = TestTakerDTO.Email;
                taker.LastName = TestTakerDTO.LastName;
                taker.Username = TestTakerDTO.Username;

                testTakerRepository.Update(taker);
                return RedirectToPage("./Index");
            }
        }
    }
}
