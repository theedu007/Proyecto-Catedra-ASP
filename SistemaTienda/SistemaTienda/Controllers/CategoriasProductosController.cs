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
    public class CategoriasProductosController : Controller
    {
        private crecer db = new crecer();

        // GET: CategoriasProductos
        public ActionResult Index()
        {
            return View(db.tblCategoria.ToList());
        }

        // GET: CategoriasProductos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // GET: CategoriasProductos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriasProductos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre_categoria")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tblCategoria.Add(tblCategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCategoria);
        }

        // GET: CategoriasProductos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // POST: CategoriasProductos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre_categoria")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCategoria);
        }

        // GET: CategoriasProductos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // POST: CategoriasProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            db.tblCategoria.Remove(tblCategoria);
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
