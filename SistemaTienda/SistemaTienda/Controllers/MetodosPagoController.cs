using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaTienda.Models;

namespace SistemaTienda.Controllers
{
    [Authorize]
    public class MetodosPagoController : Controller
    {
        private crecer db = new crecer();

        // GET: MetodosPago
        public ActionResult Index()
        {
            return View(db.tblMetodoPago.ToList());
        }

        // GET: MetodosPago/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMetodoPago tblMetodoPago = db.tblMetodoPago.Find(id);
            if (tblMetodoPago == null)
            {
                return HttpNotFound();
            }
            return View(tblMetodoPago);
        }

        // GET: MetodosPago/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MetodosPago/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre")] tblMetodoPago tblMetodoPago)
        {
            if (ModelState.IsValid)
            {
                db.tblMetodoPago.Add(tblMetodoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMetodoPago);
        }

        // GET: MetodosPago/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMetodoPago tblMetodoPago = db.tblMetodoPago.Find(id);
            if (tblMetodoPago == null)
            {
                return HttpNotFound();
            }
            return View(tblMetodoPago);
        }

        // POST: MetodosPago/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre")] tblMetodoPago tblMetodoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblMetodoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblMetodoPago);
        }

        // GET: MetodosPago/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMetodoPago tblMetodoPago = db.tblMetodoPago.Find(id);
            if (tblMetodoPago == null)
            {
                return HttpNotFound();
            }
            return View(tblMetodoPago);
        }

        // POST: MetodosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMetodoPago tblMetodoPago = db.tblMetodoPago.Find(id);
            db.tblMetodoPago.Remove(tblMetodoPago);
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
