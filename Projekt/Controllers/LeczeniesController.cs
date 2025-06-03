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
    public class LeczeniesController : Controller
    {
        private VetClinicContext db = new VetClinicContext();

        // GET: Leczenies
        public ActionResult Index()
        {
            var leczenia = db.Leczenia.Include(l => l.Wizyta);
            return View(leczenia.ToList());
        }

        // GET: Leczenies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leczenie leczenie = db.Leczenia.Find(id);
            if (leczenie == null)
            {
                return HttpNotFound();
            }
            return View(leczenie);
        }

        // GET: Leczenies/Create
        public ActionResult Create()
        {
            ViewBag.WizytaId = new SelectList(db.Wizyty, "Id", "Opis");
            return View();
        }

        // POST: Leczenies/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Opis,Lek,Koszt,WizytaId")] Leczenie leczenie)
        {
            if (ModelState.IsValid)
            {
                db.Leczenia.Add(leczenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WizytaId = new SelectList(db.Wizyty, "Id", "Opis", leczenie.WizytaId);
            return View(leczenie);
        }

        // GET: Leczenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leczenie leczenie = db.Leczenia.Find(id);
            if (leczenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.WizytaId = new SelectList(db.Wizyty, "Id", "Opis", leczenie.WizytaId);
            return View(leczenie);
        }

        // POST: Leczenies/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Opis,Lek,Koszt,WizytaId")] Leczenie leczenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leczenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WizytaId = new SelectList(db.Wizyty, "Id", "Opis", leczenie.WizytaId);
            return View(leczenie);
        }

        // GET: Leczenies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leczenie leczenie = db.Leczenia.Find(id);
            if (leczenie == null)
            {
                return HttpNotFound();
            }
            return View(leczenie);
        }

        // POST: Leczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leczenie leczenie = db.Leczenia.Find(id);
            db.Leczenia.Remove(leczenie);
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
