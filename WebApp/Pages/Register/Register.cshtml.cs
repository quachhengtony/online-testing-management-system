using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Interfaces;

namespace WebApp.Pages.Register
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        public string FirstName { get; set; }

        [BindProperty]
        [Required]
        public string LastName { get; set; }

        [BindProperty]
        [Required]
        public string Role { get; set; }

        private ITestCreatorRepository testCreatorRepository;
        private ITestTakerRepository testTakerRepository;

        public RegisterModel(ITestTakerRepository testTakerRepository, ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
            this.testTakerRepository = testTakerRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var utils = new Utils.Utils();

            if (!ModelState.IsValid)
            {
                ViewData["Email"] = Email;
                ViewData["UserName"] = Username;
                ViewData["FirstName"] = FirstName;
                ViewData["LastName"] = LastName;

                return Page();
            }
            else
            {
                if(Role == "creator")
                {
                    Console.WriteLine("Role: " + Role);
                    TestCreator testCreator = new TestCreator {
                        Id = utils.createGuid(),
                        Username = Username,
                        Email = Email,
                        FirstName = FirstName,
                        LastName = LastName,
                        Password = Password
                    };
                    testCreatorRepository.Create(testCreator);
                    return Redirect("/Login/Login");
                }
                else if (Role == "taker")
                {
                    Console.WriteLine("Role: " + Role);
                    TestTaker testTaker = new TestTaker
                    {
                        Id = utils.createGuid(),
                        Username = Username,
                        Email = Email,
                        FirstName = FirstName,
                        LastName = LastName,
                        Password = Password
                    };
                    testTakerRepository.Create(testTaker);
                    return Redirect("/Login/Login");
                }
                else
                {
                    ViewData["Message"] = "Register unsuccessfully!";
                    ViewData["Email"] = Email;
                    ViewData["UserName"] = Username;
                    ViewData["FirstName"] = FirstName;
                    ViewData["LastName"] = LastName;
                    return Page();
                }
            }
        }
    }
}
