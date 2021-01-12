using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using FudbalskaLigaBiH.EntityModels;

namespace FudbalskaLigaBiH.Controllers
{
    
    public class IgracController : Controller
    {
        private ApplicationDbContext _db;

        public IgracController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Prikaz()
        {
            IgracPrikazVM prikazIgraca = new IgracPrikazVM();
            List<IgracPrikazVM.IgracRow> lista = _db.Igrac.Select(i => new IgracPrikazVM.IgracRow
            {
                ID = i.IgracID,
                Ime = i.Ime,
                Prezime = i.Prezime,
                BrojDresa = i.BrojDresa,
                Pozicija = i.Pozicija.NazivPozicije,
                KlubNaziv = i.Klub.Naziv
            }).ToList();
            prikazIgraca.ListaIgraca = lista;
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
                KlubNaziv = i.Klub.Naziv
            }).Single();

            return View(JedanIgrac);
        }
        //prebacit ce se jedan if u Prikaz akciju kada se dodaju role, pa nece biti ponavljanje koda
        public IActionResult PrikazAdmin()
        {
            IgracPrikazVM prikazIgraca = new IgracPrikazVM();
            List<IgracPrikazVM.IgracRow> lista = _db.Igrac.Select(i => new IgracPrikazVM.IgracRow
            {
                ID = i.IgracID,
                Ime = i.Ime,
                Prezime = i.Prezime,                
                BrojDresa = i.BrojDresa,
                Pozicija = i.Pozicija.NazivPozicije,
                KlubNaziv = i.Klub.Naziv
            }).ToList();
            prikazIgraca.ListaIgraca = lista;

            return View(prikazIgraca);
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
            IgracUrediVM igrac = _db.Igrac.Where(i => i.IgracID == id).Select(i => new IgracUrediVM
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
                KlubID = (int)i.KlubID
            }).Single();
            igrac.gradovi = gradovi;
            igrac.pozicije = pozicija;
            igrac.klubovi = klublista;
            return View(igrac);
        }

        public IActionResult Snimi(IgracUrediVM x)
        {
            Igrac novi;

            novi = _db.Igrac.Find(x.ID);

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
            _db.SaveChanges();
            return Redirect("/Igrac/PrikazAdmin");
        }

        //public IActionResult Obrisi(int id)
        //{
        //    Igrac zaBrisanje = _db.Igrac.Find(id);
        //    _db.Remove(zaBrisanje);
        //    _db.SaveChanges();
        //    return Redirect("/Igrac/PrikazAdmin");
        //}


    }
}
