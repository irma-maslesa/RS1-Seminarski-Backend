using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FudbalskaLigaBiH.Controllers
{
    public class NovostiController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;


        public NovostiController(ApplicationDbContext db, UserManager<Korisnik> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Prikaz(string filter)
        {
            List<NovostiDetaljiVM.NovostiRed> lista_novosti;
            if (filter == "1")
            {
                lista_novosti = _db.Novost.OrderBy(n => n.DatumObjave)
                    .Select(n => new NovostiDetaljiVM.NovostiRed
                    {
                        IDnovosti = n.ID,
                        NaslovNovosti = n.Naslov,
                        SadrzajNovosti = n.Sadrzaj,
                        DatumObjaveNovosti = n.DatumObjave
                    }).ToList();
            }
            else
            {
                lista_novosti = _db.Novost.OrderByDescending(n => n.DatumObjave)
                    .Select(n => new NovostiDetaljiVM.NovostiRed
                    {
                        IDnovosti = n.ID,
                        NaslovNovosti = n.Naslov,
                        SadrzajNovosti = n.Sadrzaj,
                        DatumObjaveNovosti = n.DatumObjave
                    }).ToList();
            }

            NovostiDetaljiVM VMnovosti = new NovostiDetaljiVM();
            VMnovosti.novosti = lista_novosti;

            if (User.IsInRole("Novinar"))
                return View("PrikazNovinar", VMnovosti);

            return View(VMnovosti);
        }

        public IActionResult Obrisi(int NovostiID)
        {
            Novost vijestZaBrisanje = _db.Novost.Find(NovostiID);
            _db.Remove(vijestZaBrisanje);
            _db.SaveChanges();
            TempData["BrisanjePoruka"] = "Uspješno ste obrisali članak.";
            return Redirect("/Novosti/Prikaz");
        }

        public IActionResult Detalji(int NovostiID)
        {
            NovostiDetaljiVM.NovostiRed vijest = _db.Novost.Where(n => n.ID == NovostiID)
                .Select(n => new NovostiDetaljiVM.NovostiRed
                {
                    IDnovosti = n.ID,
                    NaslovNovosti = n.Naslov,
                    SadrzajNovosti = n.Sadrzaj,
                    DatumObjaveNovosti = n.DatumObjave
                }).Single();

            return View(vijest);
        }
        public IActionResult DodajUredi(int NovostiID)
        {
            NovostiDetaljiVM.NovostiRed novaVijest = NovostiID == 0 ? new NovostiDetaljiVM.NovostiRed() : _db.Novost.Where(n => n.ID == NovostiID)
                .Select(n => new NovostiDetaljiVM.NovostiRed
                {
                    IDnovosti = n.ID,
                    NaslovNovosti = n.Naslov,
                    SadrzajNovosti = n.Sadrzaj,
                    DatumObjaveNovosti = n.DatumObjave
                }).Single();

            return View("DodajUredi", novaVijest);
        }
        public IActionResult Snimi(NovostiDetaljiVM.NovostiRed x)
        {
            Novost nova;
            if (x.IDnovosti == 0)
            {
                nova = new Novost();
                _db.Novost.Add(nova);
            }
            else
            {
                nova = _db.Novost.Find(x.IDnovosti);
            }
            nova.ID = x.IDnovosti;
            nova.Naslov = x.NaslovNovosti;
            nova.Sadrzaj = x.SadrzajNovosti;
            nova.DatumObjave = x.DatumObjaveNovosti;

            _db.SaveChanges();

            return Redirect("/Novosti/Prikaz");
        }
    }
}
