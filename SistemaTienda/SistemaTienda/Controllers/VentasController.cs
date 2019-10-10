using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SistemaTienda.Models;

namespace SistemaTienda.Controllers
{
    [Authorize]
    public class VentasController : Controller
    {
        private crecer db = new crecer();

        // GET: Ventas
        public ActionResult Index()
        {
            var tblVenta = db.tblVenta.Include(t => t.tblCliente).Include(t => t.tblEmpleado).Include(t => t.tblMetodoPago).Include(t => t.tblProducto);
            return View(tblVenta.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            return View(tblVenta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.tblCliente, "Id", "nombre_compañia");
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre");
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre");
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_cliente,id_empleado,id_producto,id_metodopago,fecha,cantidad_venta,precio_final")] tblVenta tblVenta)
        {
            if (ModelState.IsValid)
            {
                int? cantidad_actual = db.tblProducto.AsNoTracking().Where(p => p.Id == tblVenta.id_producto).SingleOrDefault().cantidad;
                int? nueva_cantidad = cantidad_actual - tblVenta.cantidad_venta;

                var producto = db.tblProducto.Find(tblVenta.id_producto);
                producto.cantidad = nueva_cantidad;

                db.tblVenta.Add(tblVenta);
                db.SaveChanges();

                new KardexController().AddVentaKardex(tblVenta);

                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.tblCliente, "Id", "nombre_compañia", tblVenta.id_cliente);
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblVenta.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblVenta.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblVenta.id_producto);
            return View(tblVenta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.tblCliente, "Id", "nombre_compañia", tblVenta.id_cliente);
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblVenta.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblVenta.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblVenta.id_producto);
            return View(tblVenta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_cliente,id_empleado,id_producto,id_metodopago,fecha,cantidad_venta,precio_final")] tblVenta tblVenta)
        {
            if (ModelState.IsValid)
            {
                
                var venta_actual = db.tblVenta.AsNoTracking().Where(c => c.Id == tblVenta.Id).SingleOrDefault();
                var producto_actual = db.tblProducto.Find(venta_actual.id_producto);
                producto_actual.cantidad += venta_actual.cantidad_venta;

                var producto_actualizar = db.tblProducto.Find(tblVenta.id_producto);
                producto_actualizar.cantidad -= tblVenta.cantidad_venta;



                var kardex = db.tblKardex.Where(k => k.id_venta == tblVenta.Id).SingleOrDefault();
                kardex.id_producto = tblVenta.id_producto;
                kardex.fecha = tblVenta.fecha;
                kardex.cantidad_inicial = db.tblProducto.Where(p => p.Id == tblVenta.id_producto).SingleOrDefault().cantidad + tblVenta.cantidad_venta;



                db.Entry(tblVenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.tblCliente, "Id", "nombre_compañia", tblVenta.id_cliente);
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblVenta.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblVenta.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblVenta.id_producto);
            return View(tblVenta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            return View(tblVenta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVenta tblVenta = db.tblVenta.Find(id);

            int? cantidad_actual = db.tblProducto.Where(p => p.Id == tblVenta.id_producto).SingleOrDefault().cantidad;
            int? nueva_cantidad = cantidad_actual + tblVenta.cantidad_venta;

            var producto = db.tblProducto.Find(tblVenta.id_producto);
            producto.cantidad = nueva_cantidad;

            new KardexController().RemoveVentaKardex(tblVenta);
            db.tblVenta.Remove(tblVenta);
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
        public JsonResult getProductbyId(string data)
        {
            int id = Convert.ToInt32(data);
            tblProducto producto = db.tblProducto.Where(p => p.Id == id).SingleOrDefault();
            return Json(new {cantidad = producto.cantidad, precio = producto.precio });
        }
    }
}
