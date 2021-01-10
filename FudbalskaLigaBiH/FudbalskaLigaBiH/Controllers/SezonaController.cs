using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;

namespace FudbalskaLigaBiH.Controllers
{
    public class SezonaController : Controller
    {
        private ApplicationDbContext db;

        public SezonaController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Prikaz(int lID)
        {
            List<SezonaPrikazVM.Row> sezone = db.Sezona.Where(s => s.LigaID == lID)
                                            .Select(s => new SezonaPrikazVM.Row
                                            {
                                                ID = s.ID,
                                                Naziv = s.DatumPocetka.Year.ToString() + '/' + s.DatumZavrsetka.Year.ToString(),
                                                DatumPocetka = s.DatumPocetka.ToString("dd.MM.yyyy."),
                                                DatumZavrsetka = s.DatumZavrsetka.ToString("dd.MM.yyyy.")
                                            }).ToList();

            SezonaPrikazVM model = new SezonaPrikazVM { Sezone = sezone };

            return PartialView(model);
        }
    }
}
