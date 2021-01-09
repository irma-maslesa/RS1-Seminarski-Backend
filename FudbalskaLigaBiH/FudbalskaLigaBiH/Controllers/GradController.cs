﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Models;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FudbalskaLigaBiH.Controllers
{
    public class GradController : Controller
    {
        private ApplicationDbContext db;

        public GradController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Dodaj()
        {
            List<SelectListItem> entiteti = db.Entitet
                                                .OrderBy(e => e.Naziv)
                                                    .Select(e => new SelectListItem
                                                        {
                                                            Text = e.Naziv,
                                                            Value = e.ID.ToString()
                                                        }).ToList();

            GradDodajVM model = new GradDodajVM { Entiteti = entiteti };

            return View(model);
        }

        public IActionResult Snimi(GradDodajVM g)
        {
            Grad grad = new Grad
            {
                Naziv = g.Naziv,
                EntitetID = g.EntitetID
            };

            db.Grad.Add(grad);
            db.SaveChanges();

            return Redirect("/");
        }
    }
}
