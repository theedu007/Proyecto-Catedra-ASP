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
    public class ComprasController : Controller
    {
        private crecer db = new crecer();

        // GET: Compras
        public ActionResult Index()
        {
            var tblCompra = db.tblCompra.Include(t => t.tblEmpleado).Include(t => t.tblMetodoPago).Include(t => t.tblProducto).Include(t => t.tblProveedor);
            return View(tblCompra.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = db.tblCompra.Find(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            return View(tblCompra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre");
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre");
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto");
            ViewBag.id_proveedor = new SelectList(db.tblProveedor, "Id", "nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_proveedor,id_empleado,id_producto,id_metodopago,fecha,cantidad_compra,precio_compra")] tblCompra tblCompra)
        {
            if (ModelState.IsValid)
            {
                int? cantidad_actual = db.tblProducto.AsNoTracking().Where(p => p.Id == tblCompra.id_producto).SingleOrDefault().cantidad;
                int? nueva_cantidad = cantidad_actual + tblCompra.cantidad_compra;

                var producto = db.tblProducto.Find(tblCompra.id_producto);
                producto.cantidad = nueva_cantidad;

                db.tblCompra.Add(tblCompra);
                db.SaveChanges();

                new KardexController().AddCompraKardex(tblCompra);

                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblCompra.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblCompra.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblCompra.id_producto);
            ViewBag.id_proveedor = new SelectList(db.tblProveedor, "Id", "nombre", tblCompra.id_proveedor);
            return View(tblCompra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = db.tblCompra.Find(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblCompra.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblCompra.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblCompra.id_producto);
            ViewBag.id_proveedor = new SelectList(db.tblProveedor, "Id", "nombre", tblCompra.id_proveedor);
            return View(tblCompra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_proveedor,id_empleado,id_producto,id_metodopago,fecha,cantidad_compra,precio_compra")] tblCompra tblCompra)
        {
            if (ModelState.IsValid)
            {
                int? cantidad_actual = db.tblCompra.AsNoTracking().SingleOrDefault().cantidad_compra;
                int? nueva_cantidad = tblCompra.cantidad_compra;
                int? diff = nueva_cantidad - cantidad_actual;
                db.tblProducto.Find(tblCompra.id_producto).cantidad += diff;

                db.Entry(tblCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.tblEmpleado, "Id", "nombre", tblCompra.id_empleado);
            ViewBag.id_metodopago = new SelectList(db.tblMetodoPago, "Id", "nombre", tblCompra.id_metodopago);
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblCompra.id_producto);
            ViewBag.id_proveedor = new SelectList(db.tblProveedor, "Id", "nombre", tblCompra.id_proveedor);
            return View(tblCompra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCompra tblCompra = db.tblCompra.Find(id);
            if (tblCompra == null)
            {
                return HttpNotFound();
            }
            return View(tblCompra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCompra tblCompra = db.tblCompra.Find(id);

            int? cantidad_actual = db.tblProducto.Where(p => p.Id == tblCompra.id_producto).SingleOrDefault().cantidad;
            int? nueva_cantidad = cantidad_actual - tblCompra.cantidad_compra;

            var producto = db.tblProducto.Find(tblCompra.id_producto);
            producto.cantidad = nueva_cantidad;

            new KardexController().RemoveCompraKardex(tblCompra);
            db.tblCompra.Remove(tblCompra);
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
