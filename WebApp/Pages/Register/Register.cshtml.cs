using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
<<<<<<< HEAD
using Repositories.Interfaces;
=======
>>>>>>> 9300bccaf4ce06ae57cbb429f7256c964eae619b

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

<<<<<<< HEAD
        private ITestCreatorRepository testCreatorRepository;
        private ITestTakerRepository testTakerRepository;

        public RegisterModel(ITestTakerRepository testTakerRepository, ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
            this.testTakerRepository = testTakerRepository;
        }

=======
>>>>>>> 9300bccaf4ce06ae57cbb429f7256c964eae619b
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
<<<<<<< HEAD
=======
            var testCreatorRepository = new TestCreatorRepository();
            var testTakerRepository = new TestTakerRepository();
>>>>>>> 9300bccaf4ce06ae57cbb429f7256c964eae619b
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
