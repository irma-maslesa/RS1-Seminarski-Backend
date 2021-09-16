using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.EntityModel;
using Microsoft.EntityFrameworkCore;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    public class KlubController : Controller
    {
        ApplicationDbContext db;

        public KlubController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Prikaz()
        {
            List<KlubPrikazVM.Row> klubovi = db.Klub
                .OrderBy(i => i.Naziv)
                .Select(k => new KlubPrikazVM.Row
                {
                    ID = k.ID,
                    Naziv = k.Naziv,
                    Mail = k.Mail,
                    Adresa = k.Adresa,
                    Trener = k.Trener.Ime + " " + k.Trener.Prezime,
                    Liga = k.Liga.Naziv,
                    Stadion = k.Stadion.Naziv
                }).ToList();

            KlubPrikazVM model = new KlubPrikazVM { Klubovi = klubovi };
            return View(model);
        }

        public IActionResult DodajUredi(int kID)
        {
            List<SelectListItem> stadioni = db.Stadion
                .Where(s => s.Klub == null)
                .OrderBy(s => s.Naziv)
                .Select(s => new SelectListItem
                {
                    Text = s.Naziv,
                    Value = s.ID.ToString()
                }).ToList();

            List<SelectListItem> treneri = db.Trener
                .Where(t => t.Klub == null)
                .OrderBy(t => t.Ime)
                .Select(t => new SelectListItem
                {
                    Text = t.Ime + " " + t.Prezime,
                    Value = t.ID.ToString()
                }).ToList();

            List<SelectListItem> lige = db.Liga
                .OrderBy(l => l.Naziv)
                .Select(l => new SelectListItem
                {
                    Text = l.Naziv,
                    Value = l.ID.ToString()
                }).ToList();

            KlubDodajUrediVM model =
                kID == 0 ?
                    new KlubDodajUrediVM() :
                    db.Klub.Where(k => k.ID == kID)
                    .Select(k => new KlubDodajUrediVM
                    {
                        ID = k.ID,
                        Naziv = k.Naziv,
                        Mail = k.Mail,
                        Adresa = k.Adresa,
                        TrenerID = k.TrenerID,
                        StadionID = k.StadionID,
                        LigaID = k.LigaID
                    }).Single();

            if (kID != 0 && model.TrenerID != null)
                treneri.Add(db.Trener
                                .Where(t => t.ID == model.TrenerID)
                                    .Select(
                                        t => new SelectListItem
                                        {
                                            Text = t.Ime + " " + t.Prezime,
                                            Value = t.ID.ToString()
                                        }).Single());

            model.Stadioni = stadioni;
            model.Treneri = treneri;
            model.Lige = lige;

            return PartialView(model);
        }

        public IActionResult Snimi(KlubDodajUrediVM k)
        {
            Klub klub;

            if (k.StadionID == 0 || k.LigaID == 0)
                return Redirect("/Klub/Prikaz");

            if (k.ID == 0)
            {
                klub = new Klub();
                db.Add(klub);
            }
            else
                klub = db.Klub.Find(k.ID);


            klub.Naziv = k.Naziv;
            klub.Mail = k.Mail;
            klub.Adresa = k.Adresa;

            klub.TrenerID = k.TrenerID == 0 ? null : k.TrenerID;
            klub.LigaID = k.LigaID;
            klub.StadionID = k.StadionID;

            db.SaveChanges();

            return Redirect("/Klub/Prikaz");
        }

        public IActionResult Obrisi(int kID)
        {
            Klub klub = db.Klub.Find(kID);

            db.Remove(klub);
            db.SaveChanges();

            return Redirect("/Klub/Prikaz");
        }
    }
}
