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
    public class CobrosController : Controller
    {
        private crecer db = new crecer();

        // GET: Cobros
        public ActionResult Index(int? id)
        {
            IQueryable<tblCobros> tblCobros;
            if (db.tblCxC.Find(id) == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblCobros = db.tblCobros.Where(c => c.id_cxc == id).Include(t => t.tblCxC);
            }
            ViewBag.id_cxc = id;
            return View(tblCobros.ToList());
        }

        // GET: Cobros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCobros tblCobros = db.tblCobros.Find(id);
            if (tblCobros == null)
            {
                return HttpNotFound();
            }
            return View(tblCobros);
        }

        private void ActualizarSaldoAbonado(int? id_cxc)
        {
            var cuentas = db.tblCobros.Where(c => c.id_cxc == id_cxc);
            Decimal? saldo_abonado = 0;
            if (cuentas.Any())
            {
                foreach (var cuenta in cuentas)
                {
                    saldo_abonado += cuenta.abono;
                }
            }
            db.tblCxC.Find(id_cxc).abonado = saldo_abonado;
            db.SaveChanges();
        }

        private void SendMaxMinPago(int? id_cxc)
        {
            int? id_venta = db.tblCxC.Find(id_cxc).id_venta;
            decimal? costo_total = db.tblVenta.Find(id_venta).precio_final;
            decimal? abonado_actual = 0;

            foreach (var pago in db.tblCobros.Where(p => p.id_cxc == id_cxc))
            {
                abonado_actual += pago.abono;
            }

            decimal? costo_remanente = costo_total - abonado_actual;

            ViewBag.max = costo_remanente;
        }

        private void sendLimitFecha(int? id_cxc)
        {
            string min_fecha = db.tblVenta.Find(db.tblCxC.Find(id_cxc).id_venta).fecha.ToString("yyyy-MM-dd");
            string max_fecha = db.tblCxC.Find(id_cxc).fecha_limite?.ToString("yyyy-MM-dd");
            ViewBag.min_fecha = min_fecha;
            ViewBag.max_fecha = max_fecha;
        }

        // GET: Cobros/Create
        public ActionResult Create(int? id)
        {
            if (db.tblCxC.Find(id) == null)
            {
                return HttpNotFound();
            }

            SendMaxMinPago(id);
            sendLimitFecha(id);
            ViewBag.id_cxc = id;
            return View();
        }

        // POST: Cobros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_cxc,fecha,abono")] tblCobros tblCobros)
        {
            if (ModelState.IsValid)
            {
                db.tblCobros.Add(tblCobros);
                db.SaveChanges();
                ActualizarSaldoAbonado(tblCobros.id_cxc);
                return RedirectToAction("Index", new { id = tblCobros.id_cxc });
            }

            ViewBag.id_cxc = new SelectList(db.tblCxC, "Id", "estado", tblCobros.id_cxc);
            return View(tblCobros);
        }

        // GET: Cobros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCobros tblCobros = db.tblCobros.Find(id);
            if (tblCobros == null)
            {
                return HttpNotFound();
            }

            var cxc = db.tblCxC.Find(tblCobros.id_cxc);
            var venta = db.tblVenta.Find(cxc.id_venta);

            if (venta.precio_final == cxc.abonado)
                ViewBag.max = tblCobros.abono;
            else
                ViewBag.max = (venta.precio_final - cxc.abonado) + tblCobros.abono;

            sendLimitFecha(tblCobros.id_cxc);
            ViewBag.id_cxc = tblCobros.id_cxc;
            ViewBag.fecha_value = tblCobros.fecha?.ToString("yyyy-MM-dd");
            return View(tblCobros);
        }

        // POST: Cobros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_cxc,fecha,abono")] tblCobros tblCobros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCobros).State = EntityState.Modified;
                db.SaveChanges();
                ActualizarSaldoAbonado(tblCobros.id_cxc);
                return RedirectToAction("Index", new { id = tblCobros.id_cxc });
            }
            return View(tblCobros);
        }

        // GET: Cobros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCobros tblCobros = db.tblCobros.Find(id);
            if (tblCobros == null)
            {
                return HttpNotFound();
            }
            return View(tblCobros);
        }

        // POST: Cobros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCobros tblCobros = db.tblCobros.Find(id);
            int? id_cxc = tblCobros.id_cxc;
            db.tblCobros.Remove(tblCobros);
            db.SaveChanges();
            ActualizarSaldoAbonado(id_cxc);
            return RedirectToAction("Index", new { id = id_cxc });
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
