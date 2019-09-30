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
    public class ProductosController : Controller
    {
        private crecer db = new crecer();

        // GET: Productos
        public ActionResult Index()
        {
            var tblProducto = db.tblProducto.Include(t => t.tblCategoria);
            return View(tblProducto.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.id_categoria = new SelectList(db.tblCategoria, "Id", "nombre_categoria");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_categoria,nombre_producto,precio,cantidad,imagen_producto,descripcion")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                db.tblProducto.Add(tblProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_categoria = new SelectList(db.tblCategoria, "Id", "nombre_categoria", tblProducto.id_categoria);
            return View(tblProducto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_categoria = new SelectList(db.tblCategoria, "Id", "nombre_categoria", tblProducto.id_categoria);
            return View(tblProducto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_categoria,nombre_producto,precio,cantidad,imagen_producto,descripcion")] tblProducto tblProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_categoria = new SelectList(db.tblCategoria, "Id", "nombre_categoria", tblProducto.id_categoria);
            return View(tblProducto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProducto tblProducto = db.tblProducto.Find(id);
            db.tblProducto.Remove(tblProducto);
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
