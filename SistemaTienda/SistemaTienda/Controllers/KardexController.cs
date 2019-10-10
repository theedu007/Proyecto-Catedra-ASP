using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaTienda.Models;

namespace SistemaTienda.Controllers
{
    [Authorize]
    public class KardexController : Controller
    {
        private crecer db = new crecer();

        [HttpGet]
        public ActionResult Index()
        {
            sendFillData();
            var tblKardex = db.tblKardex.Include(t => t.tblProducto);
            return View(tblKardex.ToList());
        }

        [HttpPost]
        public ActionResult Index(string inicio, string final, int producto)
        {
            sendFillData();
            DateTime fecha_inicio = Convert.ToDateTime(inicio);
            DateTime fecha_final = Convert.ToDateTime(final);

            var filteredData = db.tblKardex.Where(p=>p.tblProducto.Id == producto).Where(t => t.fecha >= fecha_inicio && t.fecha <= fecha_final);
            if (filteredData.Any())
            {
                List<KardexRow> kardexRows = new List<KardexRow>();
                KardexRow inventario_inicial = new KardexRow();
                inventario_inicial.fecha = filteredData.FirstOrDefault().fecha;
                inventario_inicial.saldo_Q = filteredData.FirstOrDefault().cantidad_inicial;
                inventario_inicial.saldo_cu = filteredData.FirstOrDefault().tblProducto.precio;
                inventario_inicial.saldo_ct = filteredData.FirstOrDefault().tblProducto.precio * filteredData.FirstOrDefault().cantidad_inicial;
                kardexRows.Add(inventario_inicial);

                List<tblKardex> kardex_list = filteredData.ToList();

                for(int i=0; i<kardex_list.Count; i++)
                {
                        kardexRows.Add(toKardexRow(kardex_list[i], kardexRows[i]));
                }

                foreach (var trans in kardexRows)
                {
                    ViewBag.tbody += "<tr>";
                    ViewBag.tbody += "<td>"+trans.fecha.Value.ToShortDateString()+"</td>";
                    ViewBag.tbody += "<td>"+trans.entrada_Q+"</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.entrada_cu).Replace(".00", "")+ "</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.entrada_ct).Replace(".00", "") + "</td>";
                    ViewBag.tbody += "<td>"+trans.salida_Q+"</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.salida_cu).Replace(".00", "") + "</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.salida_ct).Replace(".00", "") + "</td>";
                    ViewBag.tbody += "<td>"+trans.saldo_Q+"</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.saldo_cu).Replace(".00", "") + "</td>";
                    ViewBag.tbody += "<td>"+ string.Format("{0:0.00}", trans.saldo_ct).Replace(".00", "") + "</td>";
                    ViewBag.tbody += "</tr>";
                }
            }

            var tblKardex = db.tblKardex.Include(t => t.tblProducto);
            return View(tblKardex.ToList());
        }

        private void sendFillData()
        {
            List<DateTime?> fechas = new List<DateTime?>();

            foreach(var k in db.tblKardex)
            {
                if (!fechas.Contains(k.fecha))
                    fechas.Add(k.fecha);
            }

            ViewBag.productos = db.tblProducto;
            ViewBag.fechas = fechas;
        }

        private KardexRow toKardexRow(tblKardex actual_kardex , KardexRow previous_kardex)
        {
            KardexRow kr = new KardexRow();
            kr.fecha = actual_kardex.fecha;
            if(actual_kardex.id_compra != null)
            {
                tblCompra compra = db.tblCompra.Where(c => c.Id == actual_kardex.id_compra).SingleOrDefault();
                kr.entrada_Q = compra.cantidad_compra;
                kr.entrada_cu = (compra.precio_compra / compra.cantidad_compra);
                kr.entrada_ct = compra.precio_compra;

                kr.saldo_Q = previous_kardex.saldo_Q + compra.cantidad_compra;
                kr.saldo_cu = (previous_kardex.saldo_ct + compra.precio_compra) / kr.saldo_Q; //Formula de Promedio ponderado
                kr.saldo_ct = kr.saldo_Q * kr.saldo_cu;
            }
            else if(actual_kardex.id_venta != null)
            {
                tblVenta venta = db.tblVenta.Where(v => v.Id == actual_kardex.id_venta).SingleOrDefault();
                kr.salida_Q = venta.cantidad_venta;
                kr.salida_cu = previous_kardex.saldo_cu;
                kr.salida_ct = kr.salida_Q * kr.salida_cu;

                kr.saldo_Q = previous_kardex.saldo_Q - venta.cantidad_venta;
                kr.saldo_cu = previous_kardex.saldo_cu;
                kr.saldo_ct = kr.saldo_Q * kr.saldo_cu;
            }
            return kr;
        }

        // GET: Kardex/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            return View(tblKardex);
        }

        public void AddVentaKardex(tblVenta tblVenta)
        {
            db.tblKardex.Add(new tblKardex
            {
                id_producto = tblVenta.id_producto,
                id_compra = null,
                id_venta = tblVenta.Id,
                fecha = tblVenta.fecha,
                cantidad_inicial = db.tblProducto.Where(p => p.Id == tblVenta.id_producto).SingleOrDefault().cantidad + tblVenta.cantidad_venta
            });
            db.SaveChanges();
        }

        public void RemoveVentaKardex(tblVenta tblVenta)
        {
            try
            {
                tblKardex tblKardex = db.tblKardex.Where(k => k.id_venta == tblVenta.Id).SingleOrDefault();
                db.tblKardex.Remove(tblKardex);
                db.SaveChanges();
            }
            catch (System.ArgumentNullException)
            {
                Debug.WriteLine("No hay kardex asociado a esta venta");
            }
        }

        public void AddCompraKardex(tblCompra tblCompra)
        {
            db.tblKardex.Add(new tblKardex
            {
                id_producto = tblCompra.id_producto,
                id_compra = tblCompra.Id,
                id_venta = null,
                fecha = tblCompra.fecha,
                cantidad_inicial = db.tblProducto.Where(p => p.Id == tblCompra.id_producto).SingleOrDefault().cantidad - tblCompra.cantidad_compra
            });
            db.SaveChanges();
        }

        public void RemoveCompraKardex(tblCompra tblCompra)
        {
            try
            {
                tblKardex tblKardex = db.tblKardex.Where(k => k.id_compra == tblCompra.Id).SingleOrDefault();
                db.tblKardex.Remove(tblKardex);
                db.SaveChanges();
            }
            catch (System.ArgumentNullException)
            {
                Debug.WriteLine("No hay kardex asociado a esta compra");
            }
        }

        // GET: Kardex/Create
        public ActionResult Create()
        {
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto");
            return View();
        }

        // POST: Kardex/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,id_producto,id_compra,id_venta,fecha")] tblKardex tblKardex)
        {
            if (ModelState.IsValid)
            {
                db.tblKardex.Add(tblKardex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // GET: Kardex/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // POST: Kardex/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,id_producto,id_compra,id_venta,fecha")] tblKardex tblKardex)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKardex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_producto = new SelectList(db.tblProducto, "Id", "nombre_producto", tblKardex.id_producto);
            return View(tblKardex);
        }

        // GET: Kardex/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblKardex tblKardex = db.tblKardex.Find(id);
            if (tblKardex == null)
            {
                return HttpNotFound();
            }
            return View(tblKardex);
        }

        // POST: Kardex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblKardex tblKardex = db.tblKardex.Find(id);
            db.tblKardex.Remove(tblKardex);
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
