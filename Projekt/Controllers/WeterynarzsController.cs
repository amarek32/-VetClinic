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
    public class WeterynarzsController : Controller
    {
        private VetClinicContext db = new VetClinicContext();

        // GET: Weterynarzs
        public ActionResult Index()
        {
            return View(db.Weterynarze.ToList());
        }

        // GET: Weterynarzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weterynarz weterynarz = db.Weterynarze.Find(id);
            if (weterynarz == null)
            {
                return HttpNotFound();
            }
            return View(weterynarz);
        }

        // GET: Weterynarzs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Weterynarzs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Nazwisko,Specjalizacja")] Weterynarz weterynarz)
        {
            if (ModelState.IsValid)
            {
                db.Weterynarze.Add(weterynarz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weterynarz);
        }

        // GET: Weterynarzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weterynarz weterynarz = db.Weterynarze.Find(id);
            if (weterynarz == null)
            {
                return HttpNotFound();
            }
            return View(weterynarz);
        }

        // POST: Weterynarzs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Specjalizacja")] Weterynarz weterynarz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weterynarz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weterynarz);
        }

        // GET: Weterynarzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weterynarz weterynarz = db.Weterynarze.Find(id);
            if (weterynarz == null)
            {
                return HttpNotFound();
            }
            return View(weterynarz);
        }

        // POST: Weterynarzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weterynarz weterynarz = db.Weterynarze.Find(id);
            db.Weterynarze.Remove(weterynarz);
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
