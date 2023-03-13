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

namespace WebApp.Pages.TestInfo
{
    public class IndexModel : PageModel
    {
        private ITestRepository testRepository;
        public String ErrorMessage { get; set; }

        public IndexModel(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public IList<Test> Test { get;set; }

        public async Task OnGetAsync()
        {
            Test = testRepository.GetAllByBatch();
        }

        public IActionResult OnPost(String keyCode, Guid id)
        {
            Test = testRepository.GetAllByBatch();
            var test = testRepository.GetById(id);


            if (test.KeyCode != keyCode)
            {
                ErrorMessage = "Wrong keycode.";
                return Page();
            } else
            {
                return RedirectToPage("./TestTaking", new { batch = test.Batch});

            }

        }
    }
}
