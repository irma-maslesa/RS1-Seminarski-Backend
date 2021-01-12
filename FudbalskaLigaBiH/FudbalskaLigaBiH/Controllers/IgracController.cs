using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.Data;

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
                Ime=i.Ime,
                Prezime=i.Prezime,
                DatumRodjenja=i.DatumRodjenja,
                Email=i.Email,
                Visina=i.Visina,
                Tezina=i.Tezina,
                BrojDresa=i.BrojDresa,
                Grad=i.Grad.Naziv,
                Pozicija=i.Pozicija.NazivPozicije,
                Klub=i.Klub.Naziv
            }).ToList();
            prikazIgraca.ListaIgraca = lista;
            return View(prikazIgraca);
        }

        public IActionResult Detalji(int id)
        {
            IgracPrikazVM.IgracRow JedanIgrac = _db.Igrac.Where(i => i.IgracID == id).Select(i => new IgracPrikazVM.IgracRow
            {
                ID = i.IgracID,
                Ime=i.Ime,
                Prezime=i.Prezime,
                DatumRodjenja=i.DatumRodjenja,
                Email=i.Email,
                Visina=i.Visina,
                Tezina=i.Tezina,
                BrojDresa=i.BrojDresa,
                Grad=i.Grad.Naziv,
                Pozicija=i.Pozicija.NazivPozicije,
                Klub=i.Klub.Naziv
            }).FirstOrDefault();

            return View(JedanIgrac);
        }
    }
}
