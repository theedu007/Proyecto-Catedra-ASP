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
    public class CargosEmpleadosController : Controller
    {
        private crecer db = new crecer();

        // GET: CargosEmpleados
        public ActionResult Index()
        {
            return View(db.tblCargo.ToList());
        }

        // GET: CargosEmpleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCargo tblCargo = db.tblCargo.Find(id);
            if (tblCargo == null)
            {
                return HttpNotFound();
            }
            return View(tblCargo);
        }

        // GET: CargosEmpleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargosEmpleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre")] tblCargo tblCargo)
        {
            if (ModelState.IsValid)
            {
                db.tblCargo.Add(tblCargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCargo);
        }

        // GET: CargosEmpleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCargo tblCargo = db.tblCargo.Find(id);
            if (tblCargo == null)
            {
                return HttpNotFound();
            }
            return View(tblCargo);
        }

        // POST: CargosEmpleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre")] tblCargo tblCargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCargo);
        }

        // GET: CargosEmpleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCargo tblCargo = db.tblCargo.Find(id);
            if (tblCargo == null)
            {
                return HttpNotFound();
            }
            return View(tblCargo);
        }

        // POST: CargosEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCargo tblCargo = db.tblCargo.Find(id);
            db.tblCargo.Remove(tblCargo);
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
