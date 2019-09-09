using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class ProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Produtoes
        public ActionResult Index()
        {
            var produtos = db.Produtos.Include(p => p.TipoProduto);
            return View(produtos.ToList());
        }

        // GET: Produtoes/Create
        public ActionResult Create()
        {
            ViewBag.TipoProdutoId = new SelectList(db.TipoProdutos, "Id", "Tipo");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeProduto,TipoProdutoId,Descricao,Valor,Validade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TipoProdutoId = new SelectList(db.TipoProdutos, "Id", "Tipo", produto.TipoProdutoId);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoProdutoId = new SelectList(db.TipoProdutos, "Id", "Tipo", produto.TipoProdutoId);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeProduto,TipoProdutoId,Descricao,Valor,Validade")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TipoProdutoId = new SelectList(db.TipoProdutos, "Id", "Tipo", produto.TipoProdutoId);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public ActionResult Delete(long? id)
        {
            Produto produto = db.Produtos.Find(id);
            if (produto != null)
            {
                db.Produtos.Remove(produto);
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
