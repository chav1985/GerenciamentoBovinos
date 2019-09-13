using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciamentoBovinos.Models;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: VendaProduto
        public ActionResult Index()
        {
            return View(db.VendaProdutos.ToList());
        }

        // GET: VendaProduto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaProduto vendaProduto = db.VendaProdutos.Find(id);
            if (vendaProduto == null)
            {
                return HttpNotFound();
            }
            return View(vendaProduto);
        }

        // GET: VendaProduto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendaProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtCompra,PrazoEntrega")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                db.VendaProdutos.Add(vendaProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendaProduto);
        }

        // GET: VendaProduto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaProduto vendaProduto = db.VendaProdutos.Find(id);
            if (vendaProduto == null)
            {
                return HttpNotFound();
            }
            return View(vendaProduto);
        }

        // POST: VendaProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtCompra,PrazoEntrega")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendaProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendaProduto);
        }

        // GET: VendaProduto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaProduto vendaProduto = db.VendaProdutos.Find(id);
            if (vendaProduto == null)
            {
                return HttpNotFound();
            }
            return View(vendaProduto);
        }

        // POST: VendaProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            VendaProduto vendaProduto = db.VendaProdutos.Find(id);
            db.VendaProdutos.Remove(vendaProduto);
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
