using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace FudbalskaLigaBiH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private UserManager<Korisnik> _userManager;
        public HomeController(ILogger<HomeController> logger,UserManager<Korisnik>userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public string DodajNovinara(string ime, string prezime)
        //{
        //    var email = ime + "." + prezime + "@sport.ba";
        //    var noviKorisnik = new Novinar
        //    {
        //        Ime = ime,
        //        Prezime = prezime,
        //        Email = email,
        //        UserName = email,
        //        EmailConfirmed = true
               
        //    };
        //    IdentityResult rezultat = _userManager.CreateAsync(noviKorisnik, "Mostar2020!").Result;

        //    if (!rezultat.Succeeded)
        //    {
        //        return "Errors: " + string.Join('|', rezultat.Errors);
        //    }

        //    return "Novinar je uspjesno dodan";
        //}

    }
}
