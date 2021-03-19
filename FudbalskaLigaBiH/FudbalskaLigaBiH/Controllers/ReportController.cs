using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Mvc;
using rep;
using AspNetCore.Reporting;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;

namespace FudbalskaLigaBiH.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Class1> getNovost(ApplicationDbContext db, int ID)
        {
            List<Class1> podatak = db.Novost
                .Where(n=>n.ID==ID)
                .Select(n => new Class1
            {
                    naslov=n.Naslov,
                    sadrzaj=n.Sadrzaj,
                    datum=n.DatumObjave
            }).ToList();
            return podatak;
        }

        public IActionResult Index(int ID)
        {
           LocalReport _localReport = new LocalReport("Reporti/Report1.rdlc");
           List<Class1> podatak = getNovost(_context, ID);
            _localReport.AddDataSource("DataSet1", podatak);

            ReportResult result = _localReport.Execute(RenderType.Pdf);
            return File(result.MainStream, "application/pdf");
        }
    }
}
