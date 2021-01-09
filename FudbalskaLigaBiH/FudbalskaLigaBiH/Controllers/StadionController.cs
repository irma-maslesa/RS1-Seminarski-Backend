using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FudbalskaLigaBiH.Controllers
{
    public class StadionController : Controller
    {
        private ApplicationDbContext db;

        public StadionController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Prikaz(string filter)
        {
            StadionPrikazVM model = new StadionPrikazVM();

            List<StadionPrikazVM.Row> stadioni = db.Stadion
                                                   .Where(s => filter == null || filter == "" ||
                                                   (s.Naziv.ToLower().StartsWith(filter.ToLower())) ||
                                                   (s.Grad.Naziv.ToLower().StartsWith(filter.ToLower())))
                                                   .Select(s => new StadionPrikazVM.Row
                                                   {
                                                       ID = s.ID,
                                                       Naziv = s.Naziv,
                                                       Kapacitet = s.Kapacitet,
                                                       Grad = s.Grad.Naziv
                                                   }).ToList();

            model.Stadioni = stadioni;
            model.filter = filter;

            return View(model);
        }

        public IActionResult Obrisi(int sID)
        {
            Stadion s = db.Stadion.Find(sID);

            db.Remove(s);
            db.SaveChanges();

            return Redirect("/Stadion/Prikaz");
        }

        public IActionResult Dodaj()
        {
            List<SelectListItem> gradovi = db.Grad
                                                .OrderBy(g => g.Naziv)
                                                    .Select(g => new SelectListItem
                                                    {
                                                        Text = g.Naziv,
                                                        Value = g.ID.ToString()
                                                    }).ToList();

            StadionDodajVM model = new StadionDodajVM { Gradovi = gradovi};

            return View(model);
        }

        public IActionResult Snimi(StadionDodajVM s)
        {
            Stadion stadion = new Stadion
            {
                Naziv = s.Naziv,
                Kapacitet = s.Kapacitet,
                GradID = s.GradID
            };

            db.Stadion.Add(stadion);
            db.SaveChanges();

            return Redirect("/Stadion/Prikaz");
        }
    }
}
