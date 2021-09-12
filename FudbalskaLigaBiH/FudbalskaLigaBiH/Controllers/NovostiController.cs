using Data;
using FudbalskaLigaBiH.Data;
using Data.EntityModel;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace FudbalskaLigaBiH.Controllers
{
    public class NovostiController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;
        private IHubContext<MyHub> _hubContext;
        private SignInManager<Korisnik> _SignInManager;


        public NovostiController(SignInManager<Korisnik> SignInManager,IHubContext<MyHub> hubContext,ApplicationDbContext db, UserManager<Korisnik> userManager)
        {
            _db = db;
            _userManager = userManager;
            _hubContext = hubContext;
            _SignInManager = SignInManager;
        }
        public IActionResult PrikazN(string filter)
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
                        DatumObjaveNovosti = n.DatumObjave,
                        slika = n.Slika
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
                        DatumObjaveNovosti = n.DatumObjave,
                        slika = n.Slika
                    }).ToList();
            }

            NovostiDetaljiVM VMnovosti = new NovostiDetaljiVM();
            VMnovosti.novosti = lista_novosti;
            VMnovosti.granica = DateTime.Now;

            return Ok(VMnovosti);
        }
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
                        DatumObjaveNovosti = n.DatumObjave,
                        slika=n.Slika
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
                        DatumObjaveNovosti = n.DatumObjave,
                        slika=n.Slika
                    }).ToList();
            }

            NovostiDetaljiVM VMnovosti = new NovostiDetaljiVM();
            VMnovosti.novosti = lista_novosti;
            VMnovosti.granica = DateTime.Now;

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
        public IActionResult Obrisi1(int NovostiID)
        {
            Novost vijestZaBrisanje = _db.Novost.Find(NovostiID);
            _db.Remove(vijestZaBrisanje);
            _db.SaveChanges();
            TempData["BrisanjePoruka"] = "Uspješno ste obrisali članak.";
            return Ok();
        }
        public IActionResult Detalji(int NovostiID)
        {
            NovostiDetaljiVM.NovostiRed vijest = _db.Novost.Where(n => n.ID == NovostiID)
                .Select(n => new NovostiDetaljiVM.NovostiRed
                {
                    IDnovosti = n.ID,
                    NaslovNovosti = n.Naslov,
                    SadrzajNovosti = n.Sadrzaj,
                    DatumObjaveNovosti = n.DatumObjave,
                    slika = n.Slika
                }).Single();

            return View(vijest);
        }

        //.net core novinar

        [HttpGet]
        public IActionResult DodajUredi(int NovostiID)
        {
            bool nova = false;
            if (NovostiID == 0)
                nova = true;
            NovostiDetaljiVM.NovostiRed novaVijest = NovostiID == 0 ? new NovostiDetaljiVM.NovostiRed() : _db.Novost.Where(n => n.ID == NovostiID)
                .Select(n => new NovostiDetaljiVM.NovostiRed
                {
                    IDnovosti = n.ID,
                    NaslovNovosti = n.Naslov,
                    SadrzajNovosti = n.Sadrzaj,
                    DatumObjaveNovosti = n.DatumObjave,
                    slika = n.Slika
                }).Single();
            if (nova == true)
            {
                novaVijest.DatumObjaveNovosti = DateTime.Now;
            }

            return View(novaVijest);
        }

        [HttpPost]
        public IActionResult DodajUredi(NovostiDetaljiVM.NovostiRed x)
        {
            if (ModelState.IsValid)
            {
                Novost nova;
                bool dodavanje = false;
                if (x.IDnovosti == 0)
                {
                    nova = new Novost();
                    _db.Novost.Add(nova);
                    dodavanje = true;
                }
                else
                {
                    nova = _db.Novost.Find(x.IDnovosti);
                }
                nova.ID = x.IDnovosti;
                nova.Naslov = x.NaslovNovosti;
                nova.Sadrzaj = x.SadrzajNovosti;
                nova.DatumObjave = x.DatumObjaveNovosti;

                if (x.SlikaNovosti != null)
                {
                    string ekstenzija = Path.GetExtension(x.SlikaNovosti.FileName);
                    string contentType = x.SlikaNovosti.ContentType;

                    var fileName = $"{Guid.NewGuid()}{ekstenzija}";
                    string folder = "wwwroot/upload/";
                    bool exist = System.IO.Directory.Exists(folder);
                    if (!exist)
                        System.IO.Directory.CreateDirectory(folder);

                    x.SlikaNovosti.CopyTo(new FileStream(folder + fileName, FileMode.Create));
                    nova.Slika = fileName;
                }

                _db.SaveChanges();

                if (dodavanje)
                {
                    _hubContext.Clients.All.SendAsync("prijemPoruke", "dodanaNovost");

                    var korisnici = _db.Users.ToList();

                    foreach (var i in korisnici)
                    {
                        i.brojNotifikacija = ++i.brojNotifikacija;
                        _db.Users.Update(i);
                    }

                    _db.SaveChanges();
                    TempData["successMessage"] = "Uspješno ste dodali novost.";
                }
                else
                    TempData["successMessage"] = "Uspješno ste ažurirali novost [ " + nova.Naslov + " ].";

                return Redirect("/Novosti/Prikaz");
            }
            return Redirect("/Novosti/Prikaz");

        }

        //api za angular

        [HttpPost]
        public IActionResult Uredi([FromBody]NovostiDetaljiVM.NovostiRed x)
        {
            if (ModelState.IsValid)
            {
                Novost nova;
                nova = _db.Novost.Find(x.IDnovosti);
                
                nova.ID = x.IDnovosti;
                nova.Naslov = x.NaslovNovosti;
                nova.Sadrzaj = x.SadrzajNovosti;
                nova.DatumObjave = x.DatumObjaveNovosti;

                _db.SaveChanges();
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Dodaj([FromBody]NovostiDetaljiVM.NovostiRed x)
        {
            if (ModelState.IsValid)
            {
                Novost nova;
               
                    nova = new Novost();
                    _db.Novost.Add(nova);
                nova.ID = x.IDnovosti;
                nova.Naslov = x.NaslovNovosti;
                nova.Sadrzaj = x.SadrzajNovosti;
                nova.DatumObjave = x.DatumObjaveNovosti;


                _db.SaveChanges();

                    _hubContext.Clients.All.SendAsync("prijemPoruke", "dodanaNovost");

                    var korisnici = _db.Users.ToList();

                    foreach (var i in korisnici)
                    {
                        i.brojNotifikacija = ++i.brojNotifikacija;
                        _db.Users.Update(i);
                    }

                    _db.SaveChanges();
                    TempData["successMessage"] = "Uspješno ste dodali novost.";                
            }
            return Ok();
        }
        //reset counter-a za notifikacije
        public IActionResult ResetCountKorisnik(string Email)
        {
            var id = _db.Users.Where(i => i.Email == Email).FirstOrDefault().Id;
            var korisnik = _db.Users.Find(id);
            korisnik.brojNotifikacija = 0;

            _db.Update(korisnik);
            _db.SaveChanges();

            return RedirectToAction("/Novosti/Prikaz");
        }

        public IActionResult IzvjestajNovosti()
        {
            IzvjestajFilterVM model = new IzvjestajFilterVM();
            model.DonjaGranica = DateTime.Now;
            model.GornjaGranica = DateTime.Now;

            return View();
        }

    }
}
