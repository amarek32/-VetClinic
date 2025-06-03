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
    public class WizytasController : Controller
    {
        private VetClinicContext db = new VetClinicContext();

        // GET: Wizytas
        public ActionResult Index()
        {
            var wizyty = db.Wizyty.Include(w => w.Weterynarz).Include(w => w.Zwierze);
            return View(wizyty.ToList());
        }

        // GET: Wizytas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // GET: Wizytas/Create
        public ActionResult Create()
        {
            ViewBag.WeterynarzId = new SelectList(db.Weterynarze, "Id", "Imie");
            ViewBag.ZwierzeId = new SelectList(db.Zwierzeta, "Id", "Imie");
            return View();
        }

        // POST: Wizytas/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,Opis,ZwierzeId,WeterynarzId")] Wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                db.Wizyty.Add(wizyta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WeterynarzId = new SelectList(db.Weterynarze, "Id", "Imie", wizyta.WeterynarzId);
            ViewBag.ZwierzeId = new SelectList(db.Zwierzeta, "Id", "Imie", wizyta.ZwierzeId);
            return View(wizyta);
        }

        // GET: Wizytas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            ViewBag.WeterynarzId = new SelectList(db.Weterynarze, "Id", "Imie", wizyta.WeterynarzId);
            ViewBag.ZwierzeId = new SelectList(db.Zwierzeta, "Id", "Imie", wizyta.ZwierzeId);
            return View(wizyta);
        }

        // POST: Wizytas/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Opis,ZwierzeId,WeterynarzId")] Wizyta wizyta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wizyta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WeterynarzId = new SelectList(db.Weterynarze, "Id", "Imie", wizyta.WeterynarzId);
            ViewBag.ZwierzeId = new SelectList(db.Zwierzeta, "Id", "Imie", wizyta.ZwierzeId);
            return View(wizyta);
        }

        // GET: Wizytas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wizyta wizyta = db.Wizyty.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
            return View(wizyta);
        }

        // POST: Wizytas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wizyta wizyta = db.Wizyty.Find(id);
            db.Wizyty.Remove(wizyta);
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
