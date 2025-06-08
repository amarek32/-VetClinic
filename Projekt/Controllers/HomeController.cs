using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private VetClinicContext db = new VetClinicContext();

        public ActionResult Index()
        {
            ViewBag.LiczbaKlientow = db.Klienci.Count();
            ViewBag.LiczbaZwierzat = db.Zwierzeta.Count();
            ViewBag.LiczbaWizyt = db.Wizyty.Count();
            ViewBag.LiczbaLeczen = db.Leczenia.Count();
            ViewBag.LiczbaWeterynarzy = db.Weterynarze.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}