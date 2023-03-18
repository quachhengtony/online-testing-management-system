﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebApp.Constants;

namespace WebApp.Pages.Submissions
{
    public class DetailsModel : PageModel
    {
        private IQuestionRepository questionRepository;
        private ISubmissionRepository submissionRepository;

        public DetailsModel(IQuestionRepository questionRepository, ISubmissionRepository submissionRepository)
        {
            this.questionRepository = questionRepository;
            this.submissionRepository = submissionRepository;
        }

        public Submission Submission { get; set; }
        public Dictionary<Guid, String> TestContent { get; set; }
        public List<Question> QuestionList { get; set; } = new List<Question>();
        public List<String> CheckedAnswer { get; set; } = new List<String>();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Role"))
                || !HttpContext.Session.GetString("Role").Equals("Taker"))
            {
                return Redirect("/Error/AuthorizedError");
            }


            if (id == null)
            {
                return NotFound();
            }

            Submission = submissionRepository.GetByIdAsync(id.Value).Result;
            if (Submission == null)
            {
                return NotFound();
            }

            if (!Submission.IsGraded.Value)
            {
                TempData["Status"] = ErrorConstants.Failed;
                TempData["StatusMessage"] = ErrorConstants.InvalidSubmitTest;

            }
            else
            {
                TestContent = JsonSerializer.Deserialize<Dictionary<Guid, String>>(Submission.Content);
                foreach (var i in TestContent)
                {
                    QuestionList.Add(questionRepository.GetById(i.Key));
                    CheckedAnswer.Add(i.Value);
                }

            }
            return Page();
        }
    }
}
