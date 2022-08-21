using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBSDeveloperAssesment1.Models;
using SBSDeveloperAssesment1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SBSDeveloperAssesment1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPeopleRepository _peopleRepo;
        private readonly IInfoRepository _infoRepo;

        public HomeController(ILogger<HomeController> logger, IPeopleRepository peopleRepository, IInfoRepository infoRepository)
        {
            _logger = logger;
            _peopleRepo = peopleRepository;
            _infoRepo = infoRepository;
        }

        // Info page
        public IActionResult Index() 
        {
            if (HttpContext.Session.IsAvailable && HttpContext.Session.Keys.Contains("personId"))
            {
                int? personId = HttpContext.Session.GetInt32("personId");
                if (!personId.HasValue)
                {
                    // Redirect to login page.
                    return RedirectToAction(nameof(Login)); 
                }

                Person person = _peopleRepo.Find(personId.Value);
                Info info = _infoRepo.Find(person.Id);

                return View(new PersonInfoViewModel
                {
                    Person = person,
                    Info = info
                });
            }
            else
            {
                // Redirect to login page.
                return RedirectToAction(nameof(Login)); 
            }
        }

        [HttpPost]
        public IActionResult Index(PersonInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!HttpContext.Session.IsAvailable || !HttpContext.Session.Keys.Contains("personId"))
            {
                return RedirectToAction(nameof(Login));
            }

            Person person = _peopleRepo.Find(model.Person.Id);
            if (person == null)
            {
                ModelState.AddModelError(string.Empty, "Person not found!");
                return View(model);
            }

            // Check if password changed.
            if (!string.IsNullOrEmpty(model.Person.Password) && model.Person.Password.Length > 0)
            {
                _peopleRepo.Save(model.Person);
            }

            // Update info.
            _infoRepo.Save(model.Info);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Person user = _peopleRepo.Find(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials!");
                    return View(model);
                }

                if (user.Password != model.Password)
                {
                    ModelState.AddModelError(string.Empty, "Invalid password!");
                    return View(model);
                }

                HttpContext.Session.SetInt32("personId", user.Id);
                _peopleRepo.UpdateLastLogin(user);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials!");
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
