using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class CompraProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: CompraProduto
        public ActionResult Index()
        {
            return View(db.CompraProdutos.ToList());
        }

        // GET: CompraProduto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompraProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtCompra,PrazoEntrega")] CompraProdutos compraProdutos)
        {
            if (ModelState.IsValid)
            {
                db.CompraProdutos.Add(compraProdutos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compraProdutos);
        }

        // GET: CompraProduto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraProdutos compraProdutos = db.CompraProdutos.Find(id);
            if (compraProdutos == null)
            {
                return HttpNotFound();
            }
            return View(compraProdutos);
        }

        // POST: CompraProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtCompra,PrazoEntrega")] CompraProdutos compraProdutos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compraProdutos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compraProdutos);
        }

        // GET: CompraProduto/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraProdutos compraProdutos = db.CompraProdutos.Find(id);
            if (compraProdutos == null)
            {
                return HttpNotFound();
            }
            return View(compraProdutos);
        }

        // POST: CompraProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CompraProdutos compraProdutos = db.CompraProdutos.Find(id);
            db.CompraProdutos.Remove(compraProdutos);
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
