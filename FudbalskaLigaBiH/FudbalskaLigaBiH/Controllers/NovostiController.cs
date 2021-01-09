using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Identity;

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

        public IActionResult Prikaz()
        {
            return View();
        }

        //public IActionResult Prikaz(string filter)
        //{
        //    List<NovostiDetaljiVM.NovostiRed> lista_novosti;
        //    if (filter == "1")
        //    {
        //        lista_novosti = _db.Novosti.OrderBy(n => n.DatumObjave)
        //            .Select(n => new NovostiDetaljiVM.NovostiRed
        //            {
        //                IDnovosti = n.NovostiID,
        //                NaslovNovosti = n.Naslov,
        //                SadrzajNovosti = n.Sadrzaj,
        //                DatumObjaveNovosti = n.DatumObjave
        //            }).ToList();
        //    }
        //    else
        //    {
        //        lista_novosti = _db.Novosti.OrderByDescending(n => n.DatumObjave)
        //            .Select(n => new NovostiDetaljiVM.NovostiRed
        //            {
        //                IDnovosti = n.NovostiID,
        //                NaslovNovosti = n.Naslov,
        //                SadrzajNovosti = n.Sadrzaj,
        //                DatumObjaveNovosti = n.DatumObjave
        //            }).ToList();
        //    }

        //    NovostiDetaljiVM VMnovosti = new NovostiDetaljiVM();
        //    VMnovosti.novosti = lista_novosti;

        //    if (_userManager.GetUserAsync(User).Result is Novinar)
        //        return View("PrikazNovinar", VMnovosti);

        //    return View(VMnovosti);
        //}
        ////public IActionResult PrikazNovinar()
        ////{
        ////    List<NovostiDetaljiVM.NovostiRed> lista_novosti = _db.Novosti.OrderByDescending(n => n.DatumObjave)
        ////        .Select(n => new NovostiDetaljiVM.NovostiRed
        ////        {
        ////            IDnovosti = n.NovostiID,
        ////            NaslovNovosti = n.Naslov,
        ////            SadrzajNovosti = n.Sadrzaj,
        ////            DatumObjaveNovosti = n.DatumObjave
        ////        }).ToList();

        ////    NovostiDetaljiVM VMnovosti = new NovostiDetaljiVM();
        ////    VMnovosti.novosti = lista_novosti;

        ////    return View(VMnovosti);
        ////}
        //public IActionResult Obrisi(int NovostiID)
        //{
        //    Novosti vijestZaBrisanje = _db.Novosti.Find(NovostiID);
        //    _db.Remove(vijestZaBrisanje);
        //    _db.SaveChanges();
        //    TempData["BrisanjePoruka"] = "Uspješno ste obrisali članak.";
        //    return Redirect("/Novosti/Prikaz");
        //}

        //public IActionResult Detalji(int NovostiID)
        //{
        //    NovostiDetaljiVM.NovostiRed vijest = _db.Novosti.Where(n => n.NovostiID == NovostiID)
        //        .Select(n => new NovostiDetaljiVM.NovostiRed
        //        {
        //            IDnovosti = n.NovostiID,
        //            NaslovNovosti = n.Naslov,
        //            SadrzajNovosti = n.Sadrzaj,
        //            DatumObjaveNovosti = n.DatumObjave
        //        }).Single();

        //    return View(vijest);
        //}
        //public IActionResult DodajUredi(int NovostiID)
        //{
        //    NovostiDetaljiVM.NovostiRed novaVijest = NovostiID == 0 ? new NovostiDetaljiVM.NovostiRed() : _db.Novosti.Where(n => n.NovostiID == NovostiID)
        //        .Select(n => new NovostiDetaljiVM.NovostiRed
        //        {
        //            IDnovosti = n.NovostiID,
        //            NaslovNovosti = n.Naslov,
        //            SadrzajNovosti = n.Sadrzaj,
        //            DatumObjaveNovosti = n.DatumObjave
        //        }).Single();

        //    return View("DodajUredi", novaVijest);
        //}
        //public IActionResult Snimi(NovostiDetaljiVM.NovostiRed x)
        //{
        //    Novosti nova;
        //    if (x.IDnovosti == 0)
        //    {
        //        nova = new Novosti();
        //        _db.Novosti.Add(nova);
        //    }
        //    else
        //    {
        //        nova = _db.Novosti.Find(x.IDnovosti);
        //    }
        //    nova.NovostiID = x.IDnovosti;
        //    nova.Naslov = x.NaslovNovosti;
        //    nova.Sadrzaj = x.SadrzajNovosti;
        //    nova.DatumObjave = x.DatumObjaveNovosti;

        //    _db.SaveChanges();

        //    return Redirect("/Novosti/Prikaz");
        //}
    }
}
