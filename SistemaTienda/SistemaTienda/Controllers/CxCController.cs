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
    public class CxCController : Controller
    {
        private crecer db = new crecer();

        // GET: CxC
        public ActionResult Index()
        {
            var tblCxC = db.tblCxC.Include(t => t.tblVenta);
            return View(tblCxC.ToList());
        }

        // GET: CxC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxC tblCxC = db.tblCxC.Find(id);
            if (tblCxC == null)
            {
                return HttpNotFound();
            }
            return View(tblCxC);
        }

        // GET: CxC/Create
        public ActionResult Create()
        {
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id");
            return View();
        }

        // POST: CxC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_venta,fecha_limite,abonado")] tblCxC tblCxC)
        {
            if (ModelState.IsValid)
            {
                db.tblCxC.Add(tblCxC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblCxC.id_venta);
            return View(tblCxC);
        }

        // GET: CxC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxC tblCxC = db.tblCxC.Find(id);
            if (tblCxC == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblCxC.id_venta);
            return View(tblCxC);
        }

        // POST: CxC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_venta,fecha_limite,abonado")] tblCxC tblCxC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCxC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblCxC.id_venta);
            return View(tblCxC);
        }

        // GET: CxC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxC tblCxC = db.tblCxC.Find(id);
            if (tblCxC == null)
            {
                return HttpNotFound();
            }
            return View(tblCxC);
        }

        // POST: CxC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCxC tblCxC = db.tblCxC.Find(id);
            db.tblCxC.Remove(tblCxC);
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
