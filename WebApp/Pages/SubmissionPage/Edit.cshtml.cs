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
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;

namespace WebApp.Pages.SubmissionPage
{
    public class EditModel : PageModel
    {
        private readonly ISubmissionRepository submissionRepository;
        public EditModel(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
        }

        [BindProperty]
        public Submission Submission { get; set; }


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            //if (id == null)
            //{
            //    return Redirect("/Error/AuthorizedError");
            //}
            //else if (HttpContext.Session.GetString("Role") != "Admin")
            //{
            //    return Redirect("/Error/AuthorizedError");
            //}
            //else
            //{
                Submission = submissionRepository.GetById(id.Value);

                if (Submission == null)
                {
                    return Redirect("/Error/AuthorizedError");
                }
                return Page();
            //}
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if (HttpContext.Session.GetString("Role") != "Creator")
            {
                return Redirect("/Error/AuthorizedError"); ;
            }
            else
            {
                Submission submission = submissionRepository.GetById(Submission.Id);
                submission.Feedback = Submission.Feedback;
                submissionRepository.Update(submission);
                return RedirectToPage("./Index");
            }
        }
    }
}