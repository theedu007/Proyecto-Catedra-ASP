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
    public class EmpleadosController : Controller
    {
        private crecer db = new crecer();

        // GET: Empleados
        public ActionResult Index()
        {
            var tblEmpleado = db.tblEmpleado.Include(t => t.tblCargo);
            return View(tblEmpleado.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tblEmpleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_cargo = new SelectList(db.tblCargo, "Id", "nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_cargo,nombre,apellido,dui,nit,edad,direccion,telefono,email,ruta_imagen")] tblEmpleado tblEmpleado)
        {
            if (ModelState.IsValid)
            {
                db.tblEmpleado.Add(tblEmpleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cargo = new SelectList(db.tblCargo, "Id", "nombre", tblEmpleado.id_cargo);
            return View(tblEmpleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cargo = new SelectList(db.tblCargo, "Id", "nombre", tblEmpleado.id_cargo);
            return View(tblEmpleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_cargo,nombre,apellido,dui,nit,edad,direccion,telefono,email,ruta_imagen")] tblEmpleado tblEmpleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmpleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cargo = new SelectList(db.tblCargo, "Id", "nombre", tblEmpleado.id_cargo);
            return View(tblEmpleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            if (tblEmpleado == null)
            {
                return HttpNotFound();
            }
            return View(tblEmpleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmpleado tblEmpleado = db.tblEmpleado.Find(id);
            db.tblEmpleado.Remove(tblEmpleado);
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
