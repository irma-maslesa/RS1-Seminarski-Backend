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
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger,UserManager<Korisnik>userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var SuperUser = new Korisnik
            {
                Ime = "SuperAdmin",
                Prezime = "FIT",
                UserName = "SuperAdmin.ba",
                Email = "SuperAdmin@sport.ba",
                PhoneNumber = "",
                PasswordHash = "SuperAdmin123.",
                EmailConfirmed = true
            };
            if (!_userManager.Users.Contains(SuperUser))
            {
                var result = await _userManager.CreateAsync(SuperUser, SuperUser.PasswordHash);
                var ManageRole = await _userManager.AddToRolesAsync(SuperUser, roleManager.Roles.Where(x => x.Name == "SuperAdmin").Select(y => y.Name));
            }

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
    }
}
