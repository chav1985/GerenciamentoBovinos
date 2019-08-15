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
    public class TipoProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: TipoProdutoes
        public ActionResult Index()
        {
            return View(db.TipoProdutos.ToList());
        }

        // GET: TipoProdutoes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduto tipoProduto = db.TipoProdutos.Find(id);
            if (tipoProduto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tipo,Descricao")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                db.TipoProdutos.Add(tipoProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduto tipoProduto = db.TipoProdutos.Find(id);
            if (tipoProduto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tipo,Descricao")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoProduto tipoProduto = db.TipoProdutos.Find(id);
            if (tipoProduto == null)
            {
                return HttpNotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TipoProduto tipoProduto = db.TipoProdutos.Find(id);
            db.TipoProdutos.Remove(tipoProduto);
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
