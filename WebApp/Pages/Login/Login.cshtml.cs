using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Repositories;
<<<<<<< HEAD
using Repositories.Interfaces;
=======
>>>>>>> 9300bccaf4ce06ae57cbb429f7256c964eae619b

namespace WebApp.Pages.Login
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

<<<<<<< HEAD
        private ITestCreatorRepository testCreatorRepository;
        private ITestTakerRepository testTakerRepository;

        public LoginModel(ITestTakerRepository testTakerRepository, ITestCreatorRepository testCreatorRepository)
        {
            this.testCreatorRepository = testCreatorRepository;
            this.testTakerRepository = testTakerRepository;
        }

=======
>>>>>>> 9300bccaf4ce06ae57cbb429f7256c964eae619b
        private bool LoginByAdminAccount(String email, String password)
        {
            String _email, _password;

            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                _email = config["account:defaultAccount:email"];
                _password = config["account:defaultAccount:password"];
            }

            if (email.Equals(_email) && password.Equals(_password))
            {
                HttpContext.Session.SetString("Role", "Admin");
                return true;
            }
            else
            {
                return false;
            }
        }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                if(LoginByAdminAccount(Email, Password))
                {
                    return Redirect("/Error");
                }
                else if(testCreatorRepository.Login(Email, Password) != null)
                {
                    HttpContext.Session.SetString("Role", "Creator");

                    return Redirect("/Error");
                }
                else if(testTakerRepository.Login(Email, Password) != null)
                {
                    HttpContext.Session.SetString("Role", "Taker");

                    return Redirect("/Error");
                }
                else
                {
                    ViewData["Message"] = "Username or Password is not correct!";
                    ViewData["Email"] = Email;
                    return Page();
                }
            }
        }
    }
}
