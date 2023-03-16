﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DbContexts;
using BusinessObjects.Models;
using Repositories.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebApp.Pages.TestCreators
{
    public class DetailsModel : PageModel
    {
        private readonly ITestCreatorRepository testCreatorRepository;

        public DetailsModel(ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
        }

        public TestCreator TestCreator { get; set; }

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
                TestCreator = testCreatorRepository.GetById(id.Value);

                if (TestCreator == null)
                {
                    return Redirect("/Error/AuthorizedError"); 
                }
                return Page();
            } 
        }
    }
}