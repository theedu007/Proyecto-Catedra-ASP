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
    public class KardexController : Controller
    {
        private crecer db = new crecer();

        // GET: Kardex
        public ActionResult Index()
        {
            var tblKardex = db.tblKardex.Include(t => t.tblProducto);
            return View(tblKardex.ToList());
        }

        // GET: Kardex/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            return View(tblKardex);
        }

        // GET: Kardex/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto");
            return View();
        }

        // POST: Kardex/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_producto,id_compra,id_venta,fecha")] tblKardex tblKardex)
        {
            if (ModelState.IsValid)
            {
                db.tblKardex.Add(tblKardex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // GET: Kardex/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // POST: Kardex/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_producto,id_compra,id_venta,fecha")] tblKardex tblKardex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKardex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // GET: Kardex/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            return View(tblKardex);
        }

        // POST: Kardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblKardex tblKardex = db.tblKardex.Find(id);
            db.tblKardex.Remove(tblKardex);
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
