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
    public class ProveedoresController : Controller
    {
        private crecer db = new crecer();

        // GET: Proveedores
        public ActionResult Index()
        {
            return View(db.tblProveedor.ToList());
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedor tblProveedor = db.tblProveedor.Find(id);
            if (tblProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tblProveedor);
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,nombre_contacto,cargo_contacto,direccion,telefono,email,ruta_imagen")] tblProveedor tblProveedor)
        {
            if (ModelState.IsValid)
            {
                db.tblProveedor.Add(tblProveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProveedor);
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedor tblProveedor = db.tblProveedor.Find(id);
            if (tblProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tblProveedor);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,nombre_contacto,cargo_contacto,direccion,telefono,email,ruta_imagen")] tblProveedor tblProveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProveedor);
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProveedor tblProveedor = db.tblProveedor.Find(id);
            if (tblProveedor == null)
            {
                return HttpNotFound();
            }
            return View(tblProveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProveedor tblProveedor = db.tblProveedor.Find(id);
            db.tblProveedor.Remove(tblProveedor);
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
