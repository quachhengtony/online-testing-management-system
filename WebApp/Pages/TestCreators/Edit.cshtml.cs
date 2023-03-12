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
using Microsoft.AspNetCore.Http;
using WebApp.DTO;

namespace WebApp.Pages.TestCreators
{
    public class EditModel : PageModel
    {
        private readonly ITestCreatorRepository testCreatorRepository;

        public EditModel(ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
        }

        [BindProperty]
        public CreateTestCreatorDTO TestCreatorDTO { get; set; }

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
                TestCreator creator = testCreatorRepository.GetById(id.Value);

                TestCreatorDTO = new CreateTestCreatorDTO
                {
                    Id = creator.Id,
                    Email = creator.Email,
                    FirstName = creator.FirstName,
                    LastName = creator.LastName,
                    Username = creator.Username
                };

                if (TestCreatorDTO == null)
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
                TestCreator creator = testCreatorRepository.GetById(TestCreatorDTO.Id);

                creator.FirstName = TestCreatorDTO.FirstName;
                creator.Email = TestCreatorDTO.Email;
                creator.LastName = TestCreatorDTO.LastName;
                creator.Username = TestCreatorDTO.Username;

                testCreatorRepository.Update(creator);
                return RedirectToPage("./Index");
            }
        }
    }
}
