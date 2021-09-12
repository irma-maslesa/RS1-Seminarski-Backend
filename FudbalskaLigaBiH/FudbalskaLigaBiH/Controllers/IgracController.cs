using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.EntityModel;
using System.IO;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    
    public class IgracController : Controller
    {
        private ApplicationDbContext _db;

        public IgracController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult PrikazAjax(int KlubID, int PozicijaID)
        {
            IgracPrikazAjaxVM model = new IgracPrikazAjaxVM();
            List<IgracPrikazAjaxVM.IgracRow> lista;
            if (KlubID == 0 && PozicijaID != 0)
            {
                lista = _db.Igrac.Where(i => i.PozicijaID == PozicijaID)
                .Select(i => new IgracPrikazAjaxVM.IgracRow()
                {
                    ID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    BrojDresa = i.BrojDresa,
                    Pozicija = i.Pozicija.NazivPozicije,
                    KlubNaziv = i.Klub.Naziv,
                    slika = i.Slika
                }).ToList();
            }
            else if (KlubID != 0 && PozicijaID == 0)
            {
                lista = _db.Igrac.Where(i => i.KlubID == KlubID)
                .Select(i => new IgracPrikazAjaxVM.IgracRow()
                {
                    ID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    BrojDresa = i.BrojDresa,
                    Pozicija = i.Pozicija.NazivPozicije,
                    KlubNaziv = i.Klub.Naziv,
                    slika = i.Slika
                }).ToList();
            }
            else if(KlubID==0 && PozicijaID==0)
            {
                lista = _db.Igrac.Select(i => new IgracPrikazAjaxVM.IgracRow()
                {
                    ID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    BrojDresa = i.BrojDresa,
                    Pozicija = i.Pozicija.NazivPozicije,
                    KlubNaziv = i.Klub.Naziv,
                    slika = i.Slika
                }).ToList();
            }
            else
            {
                lista= _db.Igrac.Where(i => i.KlubID == KlubID && i.PozicijaID==PozicijaID)
                .Select(i => new IgracPrikazAjaxVM.IgracRow()
                {
                    ID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    BrojDresa = i.BrojDresa,
                    Pozicija = i.Pozicija.NazivPozicije,
                    KlubNaziv = i.Klub.Naziv,
                    slika = i.Slika
                }).ToList();
            }
                model.ListaIgraca = lista;
                return PartialView(model);
            
        }

        public IActionResult Prikaz()
        {
            IgracPrikazVM prikazIgraca = new IgracPrikazVM();


            List<SelectListItem> klubovi = _db.Klub.Select(g => new SelectListItem
            {
                Value = g.ID.ToString(),
                Text = g.Naziv,
                Selected = false
            }).ToList();
            List<SelectListItem> pozicije = _db.Pozicija.Select(g => new SelectListItem
            {
                Value = g.PozicijaID.ToString(),
                Text = g.NazivPozicije,
                 Selected = false
             }).ToList();

            klubovi.Insert(0, new SelectListItem { Text = "", Value = "" });
            pozicije.Insert(0, new SelectListItem { Text = "", Value = "" });

            prikazIgraca.klubovi = klubovi;
            prikazIgraca.pozicije = pozicije;

            return View(prikazIgraca);
        }


        public IActionResult Detalji(int id)
        {
            IgracDetaljiVM JedanIgrac = _db.Igrac.Where(i => i.IgracID == id).Select(i => new IgracDetaljiVM
            {
                ID = i.IgracID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                DatumRodjenja = i.DatumRodjenja,
                Email = i.Email,
                Visina = i.Visina,
                Tezina = i.Tezina,
                BrojDresa = i.BrojDresa,
                Grad = i.Grad.Naziv,
                Pozicija = i.Pozicija.NazivPozicije,
                KlubNaziv = i.Klub.Naziv,
                slika = i.Slika
            }).Single();

            return View(JedanIgrac);
        }
        

        public IActionResult DodajUredi(int id)
        {
            List<SelectListItem> gradovi = _db.Grad.Select(g => new SelectListItem
            {
                Value = g.ID.ToString(),
                Text = g.Naziv
            }).ToList();

            List<SelectListItem> pozicija = _db.Pozicija.Select(g => new SelectListItem
            {
                Value = g.PozicijaID.ToString(),
                Text = g.NazivPozicije
            }).ToList();

            List<SelectListItem> klublista = _db.Klub.Select(g => new SelectListItem
            {
                Value = g.ID.ToString(),
                Text = g.Naziv
            }).ToList();

            IgracUrediVM igrac;
            if (id == 0)
                igrac = new IgracUrediVM();
            else
            {
                igrac = _db.Igrac.Where(i => i.IgracID == id).Select(i => new IgracUrediVM
                {
                    ID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    DatumRodjenja = i.DatumRodjenja,
                    Email = i.Email,
                    Visina = i.Visina,
                    Tezina = i.Tezina,
                    BrojDresa = i.BrojDresa,
                    GradID = i.GradID,
                    PozicijaID = i.PozicijaID,
                    KlubID = (int)i.KlubID,
                    slika=i.Slika
                }).Single();
            }
            igrac.gradovi = gradovi;
            igrac.pozicije = pozicija;
            igrac.klubovi = klublista;
            return View(igrac);
        }

        public IActionResult Snimi(IgracUrediVM x)
        {
            if (ModelState.IsValid)
            {
                Igrac novi;
                if (x.ID == 0)
                {
                    novi = new Igrac();
                    _db.Igrac.Add(novi);
                }
                else
                {
                    novi = _db.Igrac.Find(x.ID);
                }

                novi.IgracID = x.ID;
                novi.Ime = x.Ime;
                novi.Prezime = x.Prezime;
                novi.DatumRodjenja = x.DatumRodjenja;
                novi.Email = x.Email;
                novi.Tezina = x.Tezina;
                novi.Visina = x.Visina;
                novi.BrojDresa = x.BrojDresa;
                novi.GradID = x.GradID;
                novi.PozicijaID = x.PozicijaID;
                novi.KlubID = x.KlubID;

                if (x.SlikaIgraca != null)
                {
                    string ekstenzija = Path.GetExtension(x.SlikaIgraca.FileName);
                    string contentType = x.SlikaIgraca.ContentType;

                    var fileName = $"{Guid.NewGuid()}{ekstenzija}";
                    string folder = "wwwroot/upload/";
                    bool exist = System.IO.Directory.Exists(folder);
                    if (!exist)
                        System.IO.Directory.CreateDirectory(folder);

                    x.SlikaIgraca.CopyTo(new FileStream(folder + fileName, FileMode.Create));
                    novi.Slika = fileName;
                }
                _db.SaveChanges();
                return Redirect("/Igrac/Prikaz");
            }
            return View("DodajUredi");
        }

        public IActionResult Obrisi(int id)
        {
            Igrac zaBrisanje = _db.Igrac.Find(id);
            _db.Remove(zaBrisanje);
            _db.SaveChanges();
            return Redirect("/Igrac/Prikaz");
        }


     }
}
