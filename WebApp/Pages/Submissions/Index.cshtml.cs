using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        private ISubmissionRepository submissionRepository;

        public IndexModel(ISubmissionRepository submissionRepository)
        {
            this.submissionRepository = submissionRepository;
        }

        public IList<Submission> Submission { get;set; }

        public async Task OnGetAsync()
        {
            Submission = submissionRepository.GetByTestTakerIdAsync(Guid.Parse(HttpContext.Session.GetString("TestTakerId"))).Result;

        }
    }
}
