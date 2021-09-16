using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using Data.EntityModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;

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

        public IActionResult DodajUredi(int sID)
        {
            List<SelectListItem> gradovi = db.Grad
                                                .OrderBy(g => g.Naziv)
                                                    .Select(g => new SelectListItem
                                                    {
                                                        Text = g.Naziv,
                                                        Value = g.ID.ToString()
                                                    }).ToList();



            StadionDodajUrediVM model = sID == 0 ?
                                            new StadionDodajUrediVM() :
                                            db.Stadion.Where(s => s.ID == sID)
                                               .Select(s => new StadionDodajUrediVM
                                               {
                                                   ID = s.ID,
                                                   Naziv = s.Naziv,
                                                   Kapacitet = s.Kapacitet,
                                                   GradID = s.GradID
                                               }).Single();
            model.Gradovi = gradovi;

            return PartialView(model);
        }

        public IActionResult Snimi(StadionDodajUrediVM s)
        {
            Stadion stadion;

            if (s.ID == 0)
            {
                stadion = new Stadion();
                db.Add(stadion);
            }
            else
                stadion = db.Stadion.Find(s.ID);


            stadion.Naziv = s.Naziv;
            stadion.Kapacitet = s.Kapacitet;
            stadion.GradID = s.GradID;

            db.SaveChanges();

            return Redirect("/Stadion/Prikaz");
        }
    }
}
