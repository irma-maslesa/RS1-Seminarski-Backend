using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Data.DTOs;
using Data.EntityModel;
using Data.Interfaces;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Korisnik> userManager;
        private readonly SignInManager<Korisnik> signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> logger;
        private readonly IOptions<EmailOptionsDTO> emailOptions;
        private readonly IEmail _email;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<Korisnik> userManager,SignInManager<Korisnik> signInManager,
            ApplicationDbContext context,ILogger<AccountController>logger, IOptions<EmailOptionsDTO> emailOptions, IEmail email,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this.logger = logger;
            this.emailOptions = emailOptions;
            _email = email;
            this.roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

                    var emailBody = $"Ponovo postavi lozinku klikom na link: </br>{passwordResetLink}";
                    await _email.Send(model.Email, emailBody, emailOptions.Value);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError(string.Empty, "Neispravan token za ponovno postavljanje lozinke.");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model);
                }

                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View();
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);

                var emailBody = $"Vaša lozinka je upravo promijenjena. Ako Vi niste tražili ono, molimo Vas da regujete što je prije moguće i ponovno postavite lozinku klikom na sljedeći link: </br>{passwordResetLink}";
                await _email.Send(user.Email, emailBody, emailOptions.Value);

                await signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email još uvijek nije potvrđen.");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        TempData["Message"] = "Uspješno ste se prijavili. Dobrodošli!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                ModelState.AddModelError(string.Empty, "Neuspješan pokušaj prijave!");
                
            }

            return View(model);
        }

        //akcija koja se poziva nakon sto se potvrdi Email
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id = {userId} nije pronađen.";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ViewBag.Email = $"{user.Email}";
                return View();
            }

            ViewBag.ErrorTitle = "Greška prilikom potvrde Email-a.";
            ViewBag.ErrorMessage = $"Email {user.Email} ne može biti potvrđen.";
            return View("Error");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Korisnik
                {
                    Ime = model.FirstName,
                    Prezime = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await userManager.CreateAsync(user, model.Password);

                //if (signInManager.IsSignedIn(User) && User.IsInRole("SuperAdmin"))
                //{
                    var ManageRole = await userManager.AddToRolesAsync(user, roleManager.Roles.Where(x => x.Name == "GostKorisnik").Select(x => x.Name));


                    if (!ManageRole.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Nije moguće dodijeliti korisniku odabranu ulogu.");
                        return View(model);
                    }
                //}

                if (result.Succeeded)
                {
                    //kreiranje tokena
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    //kreiranje linka za potvrdu maila
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    //kreiranje teksta koji ce biti sadrzaj maila
                    var emailBody = $"Molimo potvrdite Email klikom na link: </br>{confirmationLink}";

                    //poziv funckije Send iz klase MailJet
                    await _email.Send(model.Email, emailBody, emailOptions.Value);

                    if (signInManager.IsSignedIn(User) && User.IsInRole("SuperAdmin"))
                    {
                        ViewBag.Title = "Potvrdite Email";
                        return View("EmailConfirmationNeeded", confirmationLink);

                        //return RedirectToAction("ListUsers", "Administration");

                    }

                    ViewBag.Title = "Potvrdite Vaš Email za nastavak.";
                    ViewBag.Message = "Molimo provjerite Vašu poštu za potrebe potvrde Email-a." +"\n"+
                        "Ukoliko se ne radi o pravom Email-u za potrebe demonstracije Email možete potvrditi klikom na >>Potvrdite Email<<";
                    return View("EmailConfirmationNeeded",confirmationLink);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        //Akcija kojom se provjerava da li je uneseni Email vec u upotrebi
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} je već u upotrebi.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProfileAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Korisnik nije pronađen.");
            }

            ProfileViewModel model = new ProfileViewModel();

            model.UserID = user.Id;
            model.FirstName = user.Ime;
            model.LastName = user.Prezime;
            model.PhoneNumber = user.PhoneNumber;
            model.Email = user.Email;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Korisnik nije pronađen.");
            }

          
            user.Ime = model.FirstName;
            user.Prezime = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
           
            _context.SaveChanges();
             TempData["StatusMessage"]="Vaš profil je ažuriran";
            return Redirect("/Account/Profile");
        }
    }
}
