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
    public class DevolucionesVentasController : Controller
    {
        private crecer db = new crecer();

        // GET: DevolucionesVentas
        public ActionResult Index()
        {
            var tblDevoluciones = db.tblDevoluciones.Include(t => t.tblCompra).Include(t => t.tblVenta);
            return View(tblDevoluciones.ToList());
        }

        // GET: DevolucionesVentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDevoluciones tblDevoluciones = db.tblDevoluciones.Find(id);
            if (tblDevoluciones == null)
            {
                return HttpNotFound();
            }
            return View(tblDevoluciones);
        }

        // GET: DevolucionesVentas/Create
        public ActionResult Create()
        {
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id");
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id");
            return View();
        }

        // POST: DevolucionesVentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_compra,id_venta,cantidad,fecha")] tblDevoluciones tblDevoluciones)
        {
            if (ModelState.IsValid)
            {
                var producto = db.tblProducto.Find(db.tblVenta.Find(tblDevoluciones.id_venta).id_producto);

                int? cantidad_actual = producto.cantidad;
                int? nueva_cantidad = cantidad_actual + tblDevoluciones.cantidad;

                producto.cantidad = nueva_cantidad;



                db.tblDevoluciones.Add(tblDevoluciones);
                db.SaveChanges();


                new KardexController().AddDevolucionVenta(tblDevoluciones);


                return RedirectToAction("Index");
            }

            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblDevoluciones.id_compra);
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblDevoluciones.id_venta);
            return View(tblDevoluciones);
        }

        // GET: DevolucionesVentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDevoluciones tblDevoluciones = db.tblDevoluciones.Find(id);
            if (tblDevoluciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblDevoluciones.id_compra);
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblDevoluciones.id_venta);
            return View(tblDevoluciones);
        }

        // POST: DevolucionesVentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_compra,id_venta,cantidad,fecha")] tblDevoluciones tblDevoluciones)
        {
            if (ModelState.IsValid)
            {
                var devolucion_actual = db.tblDevoluciones.AsNoTracking().Where(d => d.Id == tblDevoluciones.Id).SingleOrDefault();
                var producto_actual = db.tblProducto.Find(db.tblVenta.Find(devolucion_actual.id_venta).id_producto);
                producto_actual.cantidad -= devolucion_actual.cantidad;



                var producto_actualizar = db.tblProducto.Find(db.tblVenta.Find(tblDevoluciones.id_venta).id_producto);
                int? cantidad_actualizar = producto_actualizar.cantidad;
                int? nueva_cantidad = cantidad_actualizar + tblDevoluciones.cantidad;
                producto_actualizar.cantidad = nueva_cantidad;



                db.Entry(tblDevoluciones).State = EntityState.Modified;

                var kardex = db.tblKardex.Where(k => k.id_devolucion == tblDevoluciones.Id).SingleOrDefault();
                var producto_id = db.tblVenta.Find(tblDevoluciones.id_venta).id_producto;
                kardex.id_producto = producto_id;
                kardex.fecha = tblDevoluciones.fecha;


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.id_compra = new SelectList(db.tblCompra, "Id", "Id", tblDevoluciones.id_compra);
            ViewBag.id_venta = new SelectList(db.tblVenta, "Id", "Id", tblDevoluciones.id_venta);
            return View(tblDevoluciones);
        }

        // GET: DevolucionesVentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDevoluciones tblDevoluciones = db.tblDevoluciones.Find(id);
            if (tblDevoluciones == null)
            {
                return HttpNotFound();
            }
            return View(tblDevoluciones);
        }

        // POST: DevolucionesVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDevoluciones tblDevoluciones = db.tblDevoluciones.Find(id);
            var producto = db.tblProducto.Find(db.tblVenta.Find(tblDevoluciones.id_venta).id_producto);
            producto.cantidad -= tblDevoluciones.cantidad;

            var kardex = db.tblKardex.Where(k => k.tblDevoluciones.Id == id).SingleOrDefault();
            db.tblKardex.Remove(kardex);

            db.tblDevoluciones.Remove(tblDevoluciones);
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
