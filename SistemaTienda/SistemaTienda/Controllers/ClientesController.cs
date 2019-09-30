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
    public class ClientesController : Controller
    {
        private crecer db = new crecer();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.tblCliente.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCliente tblCliente = db.tblCliente.Find(id);
            if (tblCliente == null)
            {
                return HttpNotFound();
            }
            return View(tblCliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre_compañia,direccion,telefono,email,ruta_imagen")] tblCliente tblCliente)
        {
            if (ModelState.IsValid)
            {
                db.tblCliente.Add(tblCliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCliente tblCliente = db.tblCliente.Find(id);
            if (tblCliente == null)
            {
                return HttpNotFound();
            }
            return View(tblCliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre_compañia,direccion,telefono,email,ruta_imagen")] tblCliente tblCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCliente tblCliente = db.tblCliente.Find(id);
            if (tblCliente == null)
            {
                return HttpNotFound();
            }
            return View(tblCliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCliente tblCliente = db.tblCliente.Find(id);
            db.tblCliente.Remove(tblCliente);
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
