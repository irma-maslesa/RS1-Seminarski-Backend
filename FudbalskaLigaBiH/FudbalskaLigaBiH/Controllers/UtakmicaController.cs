using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using Data.EntityModel;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    [Authorize]
    public class UtakmicaController : Controller
    {
        private SignInManager<Korisnik> _signInManager;
        private readonly ApplicationDbContext db;

        public UtakmicaController(SignInManager<Korisnik> signInManager,
            ApplicationDbContext _db)
        {
            _signInManager = signInManager;
            db = _db;
        }

        public IActionResult Index()
        {
            LigeVM model = new LigeVM();

            List<SelectListItem> lige = db.Liga.Select(l => new SelectListItem
            {
                Value=l.ID.ToString(),
                Text=l.Naziv
            }).ToList();
            model.listaliga = lige;
            return View(model);
        }

        //tip 1 i 0 = sve utakmice; tip 2= neodigrane; tip 3= zavrsene
        public IActionResult Prikaz(int tip,int ligaid,string filterKlub)
        {
            UtakmicaPrikazVM model = new UtakmicaPrikazVM();
            if (tip == 1 || tip==0)
            {
                List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica
                    .Where(l=>l.LigaID==ligaid)
                    .Select(u => new UtakmicaPrikazVM.Row
                {
                    UtakmicaID = u.UtakmicaID,
                    KlubDomacin = u.KlubDomacin.Naziv,
                    KlubGost = u.KlubGost.Naziv,
                    RezultatDomacin = u.RezultatDomacin,
                    RezultatGost = u.RezultatGost,
                    IsZavrsena = u.IsZavrsena,
                    IsProduzeci = u.IsProduzeci,
                    IsPoluvrijeme=u.IsPoluvrijeme,
                    MinutaIgre = u.MinutaIgre,
                    slikaDomacin = u.KlubDomacin.Slika,
                    slikaGost = u.KlubGost.Slika,
                    LigaID=u.LigaID
                    }).ToList();
                model.listaUtakmica = listaUtakm;

            }
            else if(tip==2)
            {
                List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica
                    .Where(u=>u.IsZavrsena!=true && u.MinutaIgre<0 && u.LigaID == ligaid)
                    .Select(u => new UtakmicaPrikazVM.Row
                {
                    UtakmicaID = u.UtakmicaID,
                    KlubDomacin = u.KlubDomacin.Naziv,
                    KlubGost = u.KlubGost.Naziv,
                    RezultatDomacin = u.RezultatDomacin,
                    RezultatGost = u.RezultatGost,
                    IsZavrsena = u.IsZavrsena,
                    IsProduzeci = u.IsProduzeci,
                    IsPoluvrijeme = u.IsPoluvrijeme,
                    MinutaIgre = u.MinutaIgre,
                    slikaDomacin = u.KlubDomacin.Slika,
                    slikaGost = u.KlubGost.Slika,
                    LigaID=u.LigaID
                }).ToList();
                model.listaUtakmica = listaUtakm;

            }
            else if (tip == 4)
            {
                List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica
                    .Where(u => u.IsZavrsena != true && u.MinutaIgre >= 0 && u.LigaID == ligaid)
                    .Select(u => new UtakmicaPrikazVM.Row
                    {
                        UtakmicaID = u.UtakmicaID,
                        KlubDomacin = u.KlubDomacin.Naziv,
                        KlubGost = u.KlubGost.Naziv,
                        RezultatDomacin = u.RezultatDomacin,
                        RezultatGost = u.RezultatGost,
                        IsZavrsena = u.IsZavrsena,
                        IsProduzeci = u.IsProduzeci,
                        IsPoluvrijeme = u.IsPoluvrijeme,
                        MinutaIgre = u.MinutaIgre,
                        slikaDomacin = u.KlubDomacin.Slika,
                        slikaGost = u.KlubGost.Slika,
                        LigaID = u.LigaID
                    }).ToList();
                model.listaUtakmica = listaUtakm;

            }
            else
            {
                List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica
                    .Where(u => u.IsZavrsena && u.LigaID == ligaid)
                    .Select(u => new UtakmicaPrikazVM.Row
                    {
                        UtakmicaID = u.UtakmicaID,
                        KlubDomacin = u.KlubDomacin.Naziv,
                        KlubGost = u.KlubGost.Naziv,
                        RezultatDomacin = u.RezultatDomacin,
                        RezultatGost = u.RezultatGost,
                        IsZavrsena = u.IsZavrsena,
                        IsProduzeci = u.IsProduzeci,
                        IsPoluvrijeme = u.IsPoluvrijeme,
                        MinutaIgre = u.MinutaIgre,
                        slikaDomacin = u.KlubDomacin.Slika,
                        slikaGost = u.KlubGost.Slika,
                        LigaID = u.LigaID
                    }).ToList();
                   model.listaUtakmica = listaUtakm;
            }
            //vraca sve utakmice u kojima se pojavljuje ovaj klub
            if(filterKlub != null)
            {
                List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica
                    .Where(k=>k.LigaID==ligaid && (k.KlubDomacin.Naziv== filterKlub || k.KlubGost.Naziv== filterKlub))
                    .Select(u => new UtakmicaPrikazVM.Row
                    {
                        UtakmicaID = u.UtakmicaID,
                        KlubDomacin = u.KlubDomacin.Naziv,
                        KlubGost = u.KlubGost.Naziv,
                        RezultatDomacin = u.RezultatDomacin,
                        RezultatGost = u.RezultatGost,
                        IsZavrsena = u.IsZavrsena,
                        IsProduzeci = u.IsProduzeci,
                        IsPoluvrijeme = u.IsPoluvrijeme,
                        MinutaIgre = u.MinutaIgre,
                        slikaDomacin = u.KlubDomacin.Slika,
                        slikaGost = u.KlubGost.Slika,
                        LigaID = u.LigaID
                    }).ToList();
                model.listaUtakmica = listaUtakm;

            }

            List<SelectListItem> listaklub = db.Klub
                .Where(k=>k.LigaID==ligaid)
                .Select(k => new SelectListItem
            {
                Value=k.ID.ToString(),
                Text=k.Naziv
            }).ToList();

            model.listaKlubova = listaklub;
            model.LigaID = ligaid;

            if (User.IsInRole("Administrator_Utakmica"))
                return View("PrikazAdmin", model);

            return View(model);
        }
        public IActionResult DodajUredi(int UtakmicaID,int LigaID)
        {
            bool nova = false;
            if (UtakmicaID == 0)
                nova = true;

            List<SelectListItem> listaklubova = db.Klub
                .Where(k=>k.LigaID==LigaID)
                .Select(k => new SelectListItem
            {
                Value=k.ID.ToString(),
                Text=k.Naziv
            }).ToList();

            UtakmicaDodajUrediVM model = UtakmicaID == 0 ? new UtakmicaDodajUrediVM() : db.Utakmica.Where(u => u.UtakmicaID == UtakmicaID)
                .Include(u=>u.KlubDomacin).Include(u=>u.KlubGost)
                .Select(u => new UtakmicaDodajUrediVM
                {
                    UtakmicaID=u.UtakmicaID,
                    DatumOdrzavanja=u.DatumOdrzavanja,
                    slikaDomacin=u.KlubDomacin.Slika,
                    slikaGost=u.KlubGost.Slika,
                    KlubDomacinID=u.KlubDomacinID,
                    KlubGostID=u.KlubGostID,
                    LigaID=u.LigaID
                }).FirstOrDefault();


            model.KlubDomacin = listaklubova;
            model.KlubGost = listaklubova;
            model.LigaID = LigaID;
            if (nova == true)
            {
                model.DatumOdrzavanja = DateTime.Now;
            }

            return View(model);
        }
        public IActionResult Snimi(UtakmicaDodajUrediVM model)
        {
            var utakmica = db.Utakmica.Find(model.UtakmicaID);

            if(utakmica==null)
            {
                Utakmica nova = new Utakmica();
                nova.KlubDomacinID = model.KlubDomacinID;
                nova.KlubGostID = model.KlubGostID;
                nova.DatumOdrzavanja = model.DatumOdrzavanja;
                nova.IsProduzeci = false;
                nova.IsZavrsena = false;
                nova.MinutaIgre = -1;
                nova.LigaID = model.LigaID;
                db.Utakmica.Add(nova);
            }
            else
            {
                utakmica.KlubDomacinID = model.KlubDomacinID;
                utakmica.KlubGostID = model.KlubGostID;
                utakmica.DatumOdrzavanja = model.DatumOdrzavanja;
                utakmica.LigaID = model.LigaID;
            }
            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid="+model.LigaID);
        }
        public IActionResult Obrisi(int UtakmicaID, int LigaID)
        {
            var utakmica = db.Utakmica.Find(UtakmicaID);

            db.Remove(utakmica);

            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid="+LigaID);
        }
        public IActionResult Zavrsi(int id)
        {
            var utakmica = db.Utakmica.Find(id);
            utakmica.IsZavrsena = true;
            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid="+utakmica.LigaID);
        }

        public IActionResult Pocni(int id)
        {
            var utakmica = db.Utakmica.Find(id);
            utakmica.MinutaIgre = 0;
            utakmica.IsPoluvrijeme = false;
            utakmica.IsProduzeci = false;
            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid=" + utakmica.LigaID);
        }
        public IActionResult Produzeci(int id)
        {
            var utakmica = db.Utakmica.Find(id);
            utakmica.MinutaIgre = 0;
            utakmica.IsPoluvrijeme = false;
            utakmica.IsProduzeci = true;
            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid=" + utakmica.LigaID);
        }
        public IActionResult Poluvrijeme(int id)
        {
            var utakmica = db.Utakmica.Find(id);
            utakmica.IsPoluvrijeme=true;
            db.SaveChanges();

            return Redirect("/Utakmica/Prikaz?tip=0&ligaid=" + utakmica.LigaID);
        }

        public void UpdateRezultatDomacin(int id, int rezultatDomacin)
        {
            var rezultat = db.Utakmica.Find(id);
            rezultat.RezultatDomacin = rezultatDomacin;
            db.SaveChanges();
        }

        public void UpdateRezultatGost(int id, int rezultatgost)
        {
            var rezultat = db.Utakmica.Find(id);
            rezultat.RezultatGost = rezultatgost;
            db.SaveChanges();
        }
    }
}
