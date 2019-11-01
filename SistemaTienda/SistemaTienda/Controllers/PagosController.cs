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
    public class PagosController : Controller
    {
        private crecer db = new crecer();

        // GET: Pagos
        public ActionResult Index(int? id)
        {
            IQueryable<tblPagos> tblPagos;
            if (db.tblCxP.Find(id) == null)
            {
                return HttpNotFound();
            }
            else
            {
                tblPagos = db.tblPagos.Where(p=>p.id_cxp == id).Include(t => t.tblCxP);
            }
            ViewBag.id_cxp = id;
            return View(tblPagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPagos tblPagos = db.tblPagos.Find(id);
            if (tblPagos == null)
            {
                return HttpNotFound();
            }
            return View(tblPagos);
        }

        private void sendMaxMinPago(int? id_cxp)
        {
            int? id_compra = db.tblCxP.Find(id_cxp).id_compra;
            decimal? costo_total = db.tblCompra.Find(id_compra).precio_compra;
            decimal? abonado_actual = 0;

            foreach(var pago in db.tblPagos.Where(p=>p.id_cxp == id_cxp))
            {
                abonado_actual += pago.abono;
            }

            decimal? costo_remanente = costo_total - abonado_actual;

            ViewBag.max = costo_remanente;
        }

        private void sendLimitFecha(int? id_cxp)
        {
            string min_fecha = db.tblCompra.Find(db.tblCxP.Find(id_cxp).id_compra).fecha.ToString("yyyy-MM-dd");
            string max_fecha = db.tblCxP.Find(id_cxp).fecha_limite?.ToString("yyyy-MM-dd");
            ViewBag.min_fecha = min_fecha;
            ViewBag.max_fecha = max_fecha;
        }

        private  void actualizarSaldoAbonado(int? id_cxp)
        {
            var cuentas = db.tblPagos.Where(c => c.id_cxp == id_cxp);
            Decimal? saldo_abonado = 0;
            if (cuentas.Any())
            {
                foreach (var cuenta in cuentas)
                {
                    saldo_abonado += cuenta.abono;
                }
            }
            db.tblCxP.Find(id_cxp).abonado = saldo_abonado;
            db.SaveChanges();
        }


        // GET: Pagos/Create
        public ActionResult Create(int? id)
        {
            if (db.tblCxP.Find(id) == null)
            {
                return HttpNotFound();
            }

            sendMaxMinPago(id);
            sendLimitFecha(id);
            ViewBag.id_cxp = id;
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_cxp,fecha,abono")] tblPagos tblPagos)
        {
            if (ModelState.IsValid)
            {
                db.tblPagos.Add(tblPagos);
                db.SaveChanges();
                actualizarSaldoAbonado(tblPagos.id_cxp);
                return RedirectToAction("Index", new { id = tblPagos.id_cxp });
            }

            ViewBag.id_cxp = new SelectList(db.tblCxP, "Id", "estado", tblPagos.id_cxp);
            return View(tblPagos);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPagos tblPagos = db.tblPagos.Find(id);
            if (tblPagos == null)
            {
                return HttpNotFound();
            }

            var cxp = db.tblCxP.Find(tblPagos.id_cxp);
            var compra = db.tblCompra.Find(cxp.id_compra);

            if (compra.precio_compra == cxp.abonado)
                ViewBag.max = tblPagos.abono;
            else
                ViewBag.max = (compra.precio_compra - cxp.abonado) + tblPagos.abono;

            sendLimitFecha(tblPagos.id_cxp);
            ViewBag.id_cxp = tblPagos.id_cxp;
            ViewBag.fecha_value = tblPagos.fecha?.ToString("yyyy-MM-dd");
            return View(tblPagos);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_cxp,fecha,abono")] tblPagos tblPagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPagos).State = EntityState.Modified;
                db.SaveChanges();
                actualizarSaldoAbonado(tblPagos.id_cxp);
                return RedirectToAction("Index", new { id = tblPagos.id_cxp });
            }
            ViewBag.id_cxp = new SelectList(db.tblCxP, "Id", "estado", tblPagos.id_cxp);
            return View(tblPagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPagos tblPagos = db.tblPagos.Find(id);
            if (tblPagos == null)
            {
                return HttpNotFound();
            }
            return View(tblPagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPagos tblPagos = db.tblPagos.Find(id);
            int? id_cxp = tblPagos.id_cxp;
            db.tblPagos.Remove(tblPagos);
            db.SaveChanges();
            actualizarSaldoAbonado(id_cxp);
            return RedirectToAction("Index", new { id = id_cxp });
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
