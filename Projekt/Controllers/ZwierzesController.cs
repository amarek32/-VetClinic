using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class ZwierzesController : Controller
    {
        private VetClinicContext db = new VetClinicContext();

        // GET: Zwierzes
        public ActionResult Index()
        {
            var zwierzeta = db.Zwierzeta.Include(z => z.Klient);
            return View(zwierzeta.ToList());
        }

        // GET: Zwierzes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Zwierze zwierze = db.Zwierzeta
                .Include(z => z.Klient)
                .Include(z => z.Wizyty.Select(w => w.Weterynarz))
                .FirstOrDefault(z => z.Id == id);

            if (zwierze == null)
            {
                return HttpNotFound();
            }
            return View(zwierze);
        }

        // GET: Zwierzes/Create
        public ActionResult Create()
        {
            ViewBag.KlientId = new SelectList(
                db.Klienci
                .ToList()
                .Select(k => new
                {
                    Id = k.Id,
                    ImieNazwisko = k.Imie + " " + k.Nazwisko
                }),
            "Id", "ImieNazwisko"
            );
            return View();
        }

        // POST: Zwierzes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Gatunek,Rasa,DataUrodzenia,KlientId")] Zwierze zwierze)
        {
            if (ModelState.IsValid)
            {
                db.Zwierzeta.Add(zwierze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwisko", zwierze.KlientId);
            return View(zwierze);
        }

        // GET: Zwierzes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zwierze zwierze = db.Zwierzeta.Find(id);
            if (zwierze == null)
            {
                return HttpNotFound();
            }
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwisko", zwierze.KlientId);
            return View(zwierze);
        }

        // POST: Zwierzes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Gatunek,Rasa,DataUrodzenia,KlientId")] Zwierze zwierze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zwierze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KlientId = new SelectList(db.Klienci, "Id", "Nazwisko", zwierze.KlientId);
            return View(zwierze);
        }

        // GET: Zwierzes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zwierze zwierze = db.Zwierzeta
                .Include(z => z.Klient)
                .FirstOrDefault(z => z.Id == id);

            if (zwierze == null)
            {
                return HttpNotFound();
            }
            return View(zwierze);
        }

        // POST: Zwierzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zwierze zwierze = db.Zwierzeta.Find(id);
            db.Zwierzeta.Remove(zwierze);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
