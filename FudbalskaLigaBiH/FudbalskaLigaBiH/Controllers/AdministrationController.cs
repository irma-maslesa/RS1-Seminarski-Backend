using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using Data.EntityModel;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<Korisnik> userManager;
        private readonly ApplicationDbContext _context;
        private IHubContext<MyHub> _hubContext;

        public AdministrationController(IHubContext<MyHub> hubContext,RoleManager<IdentityRole> roleManager, UserManager<Korisnik> userManager, ApplicationDbContext _context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._context = _context;
            _hubContext = hubContext;
        }

     
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            await _hubContext.Clients.All.SendAsync("prijemPoruke", User, "porukica");

            return View(model);
        }

  
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            PagedList<IdentityRole> model = new PagedList<IdentityRole>(roles, 1, 5);
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa ID = {id} nije pronađena.";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.Ime + " " + user.Prezime + " | " + user.Email);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa ID = {model.Id} nije pronađena.";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    //return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id = {id} nije pronađena.";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View("ListRoles");
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorTitle = $"» {role.Name} « uloga se trenutno koristi!";
                    ViewBag.ErrorMessage = $"Uloga » {role.Name} « ne može biti obrisana dok je dodijeljena korisnicima." +
                        $"Ako još uvijek želite obrisati » {role.Name} « ulogu, prvo uklonite ulogu kod korisnika koji je koriste, pa pokušajte ponovo.";
                    return View("Error");
                }
            }
        }

        //dodavanje i brisanje odredjenih korisnika odredjenoj roli 
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            ViewBag.roleName = role.Name;

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa ID = {roleId} nije pronađena.";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserInfo = user.Ime + " " + user.Prezime + " | " + user.Email
                };

                userRoleViewModel.IsSelected = await userManager.IsInRoleAsync(user, role.Name);

                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa ID = {roleId} nije pronađena.";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))//provjera da li je vec taj korisnik pod tom rolom
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpGet]
        public async Task<IActionResult> ListUsersAsync()
        {
            List<Korisnik> users=new List<Korisnik>();

            foreach (var u in userManager.Users)
            {
                if (!await userManager.IsInRoleAsync(u, "SuperAdmin"))
                    users.Add(u);
            }
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id = {id} nije pronađen.";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUsers");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View("ListUsers");
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorTitle = $"» {user.Ime+" "+user.Prezime} « korisnik trenutno ima dodijeljenu ulogu!";
                    ViewBag.ErrorMessage = $"Uloga » {user.Ime + " " + user.Prezime} « ne može biti obrisan dok je dodijeljena uloga." +
                        $"Ako još uvijek želite obrisati » {user.Ime + " " + user.Prezime} « korisnika, prvo uklonite ulogu kod korisnika koji je koriste, pa pokušajte ponovo.";
                    return View("Error");
                }
                
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} couldn't be found.";
                return View("NotFound");
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.Ime,
                LastName = user.Prezime,
                PhoneNumber=user.PhoneNumber,
                Claims = userClaims.Select(c => c.Type + " :: " + c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id = {model.Id} nije pronađen.";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Ime = model.FirstName;
                user.Prezime = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id = {userId} nije pronađen.";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                    userRolesViewModel.IsSelected = await userManager.IsInRoleAsync(user, role.Name);

                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sad Id = {userId} nije pronađen.";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Nije moguće ukloniti trenutnu korisničku ulogu.");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Nije moguće dodijeliti korisniku odabranu ulogu.");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

}
