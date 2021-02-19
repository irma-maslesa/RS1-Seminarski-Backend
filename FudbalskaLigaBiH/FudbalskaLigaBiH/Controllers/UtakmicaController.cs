using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.EntityModels;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Prikaz()
        {
            //UtakmicaPrikazVM model = new UtakmicaPrikazVM();

            //List<UtakmicaPrikazVM.Row> listaUtakm = db.Utakmica.Select(u => new UtakmicaPrikazVM.Row
            //{
            //    UtakmicaID=u.UtakmicaID,
            //    KlubDomacin=u.KlubDomacin.Naziv,
            //    KlubGost=u.KlubGost.Naziv,
            //    RezultatDomacin=u.RezultatDomacin,
            //    RezultatGost=u.RezultatGost,
            //    IsZavrsena=u.IsZavrsena,
            //    IsProduzeci=u.IsProduzeci,
            //    MinutaIgre=u.MinutaIgre,
            //    IsOmiljena=u.IsOmiljena
            //}).ToList();

            //List<Klub> listaklub = db.Klub.ToList();

            //model.listaKlubova = listaklub;
            return View(/*model*/);
        }
    }
}
