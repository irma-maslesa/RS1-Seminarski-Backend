using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.EntityModels;

namespace FudbalskaLigaBiH.Controllers
{
    public class TrenerController : Controller
    {
        public IActionResult Dodaj()
        {
            return View();
        }

        public IActionResult Snimi(TrenerDodajVM t)
        {
            //ApplicationDbContext dbc = new ApplicationDbContext();

            Trener trener = new Trener
            {
                Ime = t.Ime,
                Prezime = t.Prezime,
                Mail = t.Mail,
                DatumRodjenja = t.DatumRodjenja
            };


            return Redirect("/");
        }
    }
}
