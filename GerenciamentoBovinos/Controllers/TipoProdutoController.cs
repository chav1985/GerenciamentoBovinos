using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    [Authorize(Roles = "Sistema,Administrativo")]
    public class TipoProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: TipoProdutoes
        /// <summary>
        /// Retorna a lista de tipos de produto cadastrados
        /// </summary>
        /// <param name="retorno"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string retorno = null)
        {
            ViewBag.Retorno = retorno;
            return View(db.TipoProdutos.ToList());
        }

        // GET: TipoProdutoes/Create
        /// <summary>
        /// Gera a view para cadastrar um tipo de produto
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Adiciona um tipo de produto
        /// </summary>
        /// <param name="tipoProduto"></param>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Gera a view para editar um tipo de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Edita um tipo de produto
        /// </summary>
        /// <param name="tipoProduto"></param>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Exclui um tipo de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(long? id)
        {
            TipoProduto tipo = db.TipoProdutos.Find(id);


            List<Produto> listaProdutos = db.Produtos.Where(x => x.TipoProdutoId == id).ToList();

            if (listaProdutos != null && listaProdutos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Tipo de Produto esta relacionado a algum produto cadastrado!" });
            }

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