using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineTestingManagementSystem.Repository;
using Repositories.Interfaces;

namespace WebApp.Pages.Submissions
{
    public class SubmissionListModel : PageModel
    {
        private  ISubmissionRepository submissionRepository;

        public SubmissionListModel()
        {
            submissionRepository = new SubmissionRepository();
        }

        [BindProperty(SupportsGet = true)]
        public string TestTakerId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TestId { get; set; }

        public List<Submission> Submissions { get; set; }

        public void OnGet()
        {
            Guid testTakerId = new Guid();
            Submissions = submissionRepository.GetByTestTakerId(testTakerId);
        }
    }
}
