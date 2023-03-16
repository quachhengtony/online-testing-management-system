﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Repositories;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestInfo
{
    public class DetailsModel : PageModel
    {
        private ITestRepository testRepository;

        public DetailsModel(ITestRepository testRepository)
        {
            this.testRepository = testRepository;
        }

        public Test Test { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Role"))
                || !HttpContext.Session.GetString("Role").Equals("Taker"))
            {
                return Redirect("/Error/AuthorizedError");
            }

            Test = testRepository.GetByIdForTestTakerAsync(id).Result;

            if (Test == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
