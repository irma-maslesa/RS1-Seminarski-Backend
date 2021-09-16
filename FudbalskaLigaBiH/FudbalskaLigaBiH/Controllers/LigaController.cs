using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using Data;

namespace FudbalskaLigaBiH.Controllers
{
    public class LigaController : Controller
    {
        private ApplicationDbContext db;

        public LigaController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Prikaz()
        {
            List<LigaPrikazVM.Row> lige = db.Liga
                                            .Select(l => new LigaPrikazVM.Row
                                            {
                                                ID = l.ID,
                                                Naziv = l.Naziv
                                            }).ToList();

            LigaPrikazVM model = new LigaPrikazVM { Lige = lige };

            return View(model);
        }
    }
}
