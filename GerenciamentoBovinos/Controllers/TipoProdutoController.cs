using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
            TipoProduto tipo = db.TipoProdutos.Find(id);
            if (tipo != null)
            {
                db.TipoProdutos.Remove(tipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
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