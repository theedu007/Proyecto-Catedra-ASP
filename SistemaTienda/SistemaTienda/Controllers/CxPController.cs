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
    public class CxPController : Controller
    {
        private crecer db = new crecer();
        // GET: CxP
        public ActionResult Index()
        {
            UpdateEstadosCxP();
            var tblCxP = db.tblCxP.Include(t => t.tblCompra);
            return View(tblCxP.ToList());
        }

        private void UpdateEstadosCxP()
        {
            var cuentas_vencidas = db.tblCxP.Where(c => c.fecha_limite < DateTime.Now && c.estado.Equals("Activo"));
            var cuentas_prorrogadas = db.tblCxP.Where(c => c.fecha_limite > DateTime.Now);

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
                    if (cuenta.abonado == db.tblCompra.Find(cuenta.id_compra).precio_compra)
                        cuenta.estado = "Completado";
                }
            }
            db.SaveChanges();
        }

        // GET: CxP/Details/5
        public ActionResult Details(int? id)
        {
            UpdateEstadosCxP();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            return View(tblCxP);
        }

        // GET: CxP/Create
        public ActionResult Create()
        {
            UpdateEstadosCxP();
            ViewBag.id_compra = new SelectList(db.tblCompra.Where(c => c.id_metodopago == 2 && !db.tblCxP.Where(p=>p.id_compra == c.Id).Any()), "Id", "Id");
            return View();
        }

        // POST: CxP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_compra,fecha_limite,abono_inicial")] tblCxP tblCxP)
        {
            UpdateEstadosCxP();
            if (ModelState.IsValid)
            {

                if(tblCxP.fecha_limite >= DateTime.Now)
                    tblCxP.estado = "Activo";
                else
                    tblCxP.estado = "Vencido";

                tblCxP.abonado = tblCxP.abono_inicial;
                db.tblCxP.Add(tblCxP);

                db.tblPagos.Add(new tblPagos
                {
                    id_cxp = tblCxP.Id,
                    fecha = db.tblCompra.Find(tblCxP.id_compra).fecha,
                    abono = tblCxP.abono_inicial
                });

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblCxP.id_compra);
            return View(tblCxP);
        }

        // GET: CxP/Edit/5
        public ActionResult Edit(int? id)
        {
            UpdateEstadosCxP();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.tblCompra.Where(c => c.Id == tblCxP.id_compra || c.id_metodopago == 2 && !db.tblCxP.Where(p => p.id_compra == c.Id).Any()), "Id", "Id", tblCxP.id_compra);
            
            return View(tblCxP);
        }

        // POST: CxP/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_compra,fecha_limite,abono_inicial")] tblCxP tblCxP)
        {
            if (ModelState.IsValid)
            {

                if (tblCxP.fecha_limite >= DateTime.Now)
                    tblCxP.estado = "Activo";
                else
                    tblCxP.estado = "Vencido";

                db.Entry(tblCxP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblCxP.id_compra);
            return View(tblCxP);
        }

        // GET: CxP/Delete/5
        public ActionResult Delete(int? id)
        {
            UpdateEstadosCxP();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCxP tblCxP = db.tblCxP.Find(id);
            if (tblCxP == null)
            {
                return HttpNotFound();
            }
            return View(tblCxP);
        }

        // POST: CxP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCxP tblCxP = db.tblCxP.Find(id);
            db.tblPagos.RemoveRange(db.tblPagos.Where(p=>p.id_cxp == id));
            db.tblCxP.Remove(tblCxP);
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
            int id_compra = Convert.ToInt32(data);
            var compra = db.tblCompra.Find(id_compra);
            decimal cantidad_max = compra.precio_compra;
            DateTime fecha_compra = compra.fecha;

            var fechas = new String[]
            {
                fecha_compra.AddDays(30).ToShortDateString(),
                fecha_compra.AddDays(60).ToShortDateString(),
                fecha_compra.AddDays(90).ToShortDateString(),
                fecha_compra.AddDays(120).ToShortDateString()
            };

            return Json(new { cantidad_max, fechas });
        }
    }
}
