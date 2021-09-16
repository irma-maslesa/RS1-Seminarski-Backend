using AspNetCore.Reporting;
using Data;
using FudbalskaLigaBiH.Data;
using FudbalskaLigaBiH.Models;
using Microsoft.AspNetCore.Mvc;
using rep;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FudbalskaLigaBiH.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Class1r> getNovost(ApplicationDbContext db, int ID)
        {
            List<Class1r> podatak = db.Novost
                .Where(n => n.ID == ID)
                .Select(n => new Class1r
                {
                    naslov = n.Naslov,
                    sadrzaj = n.Sadrzaj,
                    datum = n.DatumObjave
                }).ToList();
            return podatak;
        }
        public IActionResult Index(int ID)
        {
            LocalReport _localReport = new LocalReport("Reporti/Report1.rdlc");
            List<Class1r> podatak = getNovost(_context, ID);
            _localReport.AddDataSource("DataSet1", podatak);



            ReportResult result = _localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }


        public static List<Class2> getIzvjestajNovosti(ApplicationDbContext db, DateTime donjaGranica, DateTime gornjaGranica)
        {
            List<Class2> podaciPripremljeni = db.Novost
                .Where(n => n.DatumObjave >= donjaGranica && n.DatumObjave <= gornjaGranica)
                .Select(n => new Class2
                {
                    id = n.ID,
                    naslov = n.Naslov,
                    datum = n.DatumObjave
                }).ToList();
            Class2 novi = new Class2();
            return podaciPripremljeni;
        }
        
        public IActionResult Index1(IzvjestajFilterVM model)
        {
            LocalReport _localReport = new LocalReport("Reporti/Report2.rdlc");
            List<Class2> podatak = getIzvjestajNovosti(_context, model.DonjaGranica, model.GornjaGranica);
            _localReport.AddDataSource("DataSet1", podatak);

            Dictionary<string, string> parametar = new System.Collections.Generic.Dictionary<string, string>();
            parametar.Add("interval", model.DonjaGranica.ToString() + "-" + model.GornjaGranica.ToString());



            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parametar);
            return File(result.MainStream, "application/pdf");
        }
        
    }
}
