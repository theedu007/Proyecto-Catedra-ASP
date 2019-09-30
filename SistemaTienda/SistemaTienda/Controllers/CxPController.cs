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
    public class CxPController : Controller
    {
        private crecer db = new crecer();

        // GET: CxP
        public ActionResult Index()
        {
            var tblCxP = db.tblCxP.Include(t => t.tblCompra);
            return View(tblCxP.ToList());
        }

        // GET: CxP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            return View(tblCxP);
        }

        // GET: CxP/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id");
            return View();
        }

        // POST: CxP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_compra,fecha_limite,abonado")] tblCxP tblCxP)
        {
            if (ModelState.IsValid)
            {
                db.tblCxP.Add(tblCxP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblCxP.id_compra);
            return View(tblCxP);
        }

        // GET: CxP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblCxP.id_compra);
            return View(tblCxP);
        }

        // POST: CxP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_compra,fecha_limite,abonado")] tblCxP tblCxP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCxP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblCxP.id_compra);
            return View(tblCxP);
        }

        // GET: CxP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            return View(tblCxP);
        }

        // POST: CxP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCxP tblCxP = db.tblCxP.Find(id);
            db.tblCxP.Remove(tblCxP);
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
