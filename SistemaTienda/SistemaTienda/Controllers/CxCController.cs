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
            UpdateEstadosCxC();
            var tblCxC = db.tblCxC.Include(t => t.tblVenta);
            return View(tblCxC.ToList());
        }

        private void UpdateEstadosCxC()
        {
            var cuentas_vencidas = db.tblCxC.Where(c => c.fecha_limite < DateTime.Now && c.estado.Equals("Activo"));
            var cuentas_prorrogadas = db.tblCxC.Where(c => c.fecha_limite > DateTime.Now);

            if (cuentas_vencidas.Any())
            {
                foreach (var cuenta in cuentas_vencidas)
                {
                    cuenta.estado = "Vencido";
                }
            }

            if (cuentas_prorrogadas.Any())
            {
                foreach (var cuenta in cuentas_prorrogadas)
                {
                    cuenta.estado = "Activo";
                    if (cuenta.abonado == db.tblVenta.Find(cuenta.id_venta).precio_final)
                        cuenta.estado = "Completado";
                }
            }
            db.SaveChanges();
        }

        // GET: CxC/Details/5
        public ActionResult Details(int? id)
        {
            UpdateEstadosCxC();
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
            UpdateEstadosCxC();
            ViewBag.id_venta = new SelectList(db.tblVenta.Where(c => c.id_metodopago == 2 && !db.tblCxC.Where(p => p.id_venta == c.Id).Any()), "Id", "Id");
            return View();
        }

        // POST: CxC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_venta,fecha_limite,abono_inicial")] tblCxC tblCxC)
        {
            if (ModelState.IsValid)
            {
                if (tblCxC.fecha_limite >= DateTime.Now)
                    tblCxC.estado = "Activo";
                else
                    tblCxC.estado = "Vencido";

                tblCxC.abonado = tblCxC.abono_inicial;
                db.tblCxC.Add(tblCxC);

                db.tblCobros.Add(new tblCobros
                {
                    id_cxc = tblCxC.Id,
                    fecha = db.tblVenta.Find(tblCxC.id_venta).fecha,
                    abono = tblCxC.abono_inicial
                });

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblCxC.id_venta);
            return View(tblCxC);
        }

        // GET: CxC/Edit/5
        public ActionResult Edit(int? id)
        {
            UpdateEstadosCxC();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxC tblCxC = db.tblCxC.Find(id);
            if (tblCxC == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_venta = new SelectList(db.tblVenta.Where(v => v.Id == tblCxC.id_venta || v.id_metodopago == 2 && !db.tblCxC.Where(c => c.id_venta == v.Id).Any()), "Id", "Id", tblCxC.id_venta);
            return View(tblCxC);
        }

        // POST: CxC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_venta,fecha_limite,abono_inicial")] tblCxC tblCxC)
        {
            if (ModelState.IsValid)
            {
                if (tblCxC.fecha_limite >= DateTime.Now)
                    tblCxC.estado = "Activo";
                else
                    tblCxC.estado = "Vencido";

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
            UpdateEstadosCxC();
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
            db.tblCobros.RemoveRange(db.tblCobros.Where(p => p.id_cxc == id));
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

        [HttpPost]
        public JsonResult getCantidadTrans(string data)
        {
            int id_venta = Convert.ToInt32(data);
            var venta = db.tblVenta.Find(id_venta);
            decimal cantidad_max = venta.precio_final;
            DateTime fecha_venta = venta.fecha;

            var fechas = new String[]
            {
                fecha_venta.AddDays(30).ToShortDateString(),
                fecha_venta.AddDays(60).ToShortDateString(),
                fecha_venta.AddDays(90).ToShortDateString(),
                fecha_venta.AddDays(120).ToShortDateString()
            };

            return Json(new { cantidad_max, fechas });
        }
    }
}
