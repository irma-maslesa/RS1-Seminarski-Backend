using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.EntityModel;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    public class SezonaController : Controller
    {
        private ApplicationDbContext db;

        public SezonaController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Prikaz(int lID)
        {
            List<SezonaPrikazVM.Row> sezone = db.Sezona.Where(s => s.LigaID == lID)
                                            .Select(s => new SezonaPrikazVM.Row
                                            {
                                                ID = s.ID,
                                                Naziv = s.DatumPocetka.Year.ToString() + '/' + s.DatumZavrsetka.Year.ToString(),
                                                DatumPocetka = s.DatumPocetka.ToString("dd.MM.yyyy."),
                                                DatumZavrsetka = s.DatumZavrsetka.ToString("dd.MM.yyyy.")
                                            }).ToList();

            Liga liga = db.Liga.Find(lID);

            SezonaPrikazVM model = new SezonaPrikazVM { Sezone = sezone, NazivLige = liga.Naziv };

            return PartialView(model);
        }

        public IActionResult Dodaj(int lID)
        {
            List<SelectListItem> lige = db.Liga
                                             .OrderBy(g => g.Naziv)
                                             .Select(g => new SelectListItem
                                             {
                                                Text = g.Naziv,
                                                Value = g.ID.ToString()
                                             }).ToList();

            SezonaDodajUrediVM model = new SezonaDodajUrediVM { LigaID = lID, Lige = lige };

            return PartialView(model);
        }

        public IActionResult Uredi(int sID)
        {
            List<SelectListItem> lige = db.Liga
                                             .OrderBy(l => l.Naziv)
                                             .Select(l => new SelectListItem
                                             {
                                                 Text = l.Naziv,
                                                 Value = l.ID.ToString()
                                             }).ToList();

            SezonaDodajUrediVM model = db.Sezona
                                            .Where(s => s.ID == sID)
                                            .Select(s => new SezonaDodajUrediVM()
                                            {
                                                ID = s.ID,
                                                DatumPocetka = s.DatumPocetka,
                                                DatumZavrsetka = s.DatumZavrsetka,
                                                LigaID = s.LigaID
                                            }).SingleOrDefault();
            model.Lige = lige;

            return PartialView(model);
        }

        public IActionResult Snimi(SezonaDodajUrediVM s)
        {
            Sezona sezona;

            if (s.ID == 0)
            {
                sezona = new Sezona();
                db.Add(sezona);
            }
            else
                sezona = db.Sezona.Find(s.ID);


            sezona.DatumPocetka = s.DatumPocetka;
            sezona.DatumZavrsetka = s.DatumZavrsetka;
            sezona.LigaID = s.LigaID;

            db.SaveChanges();

            return Redirect("/Liga/Prikaz");
        }

        public IActionResult Obrisi(int sID)
        {
            Sezona s = db.Sezona.Find(sID);

            db.Remove(s);
            db.SaveChanges();

            return Redirect("/Liga/Prikaz");
        }
    }
}
