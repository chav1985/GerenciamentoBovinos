using GerenciamentoBovinos.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class BovinoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Bovino
        public ActionResult Index()
        {
            var bovinos = db.Bovinos.Include(b => b.Raca);
            return View(bovinos.ToList());
        }

        // GET: Bovino/Create
        public ActionResult Create()
        {
            ViewBag.RacaId = new SelectList(db.Racas, "Id", "Nome");
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome");
            return View();
        }

        // POST: Bovino/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brinco,RacaId,FornecedorId,Peso,DtNascimento,VlrUnitario,Descricao")] Bovino bovino)
        {
            if (ModelState.IsValid && bovino.RacaId > 0 && bovino.FornecedorId > 0)
            {
                Confinamento confinamento = new Confinamento();
                confinamento.Bovino = bovino;
                confinamento.BovinoId = bovino.Id;
                confinamento.DtEntrada = DateTime.Now;
                db.Confinamentos.Add(confinamento);

                db.Bovinos.Add(bovino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Thread.Sleep(2000);
            ViewBag.RacaId = new SelectList(db.Racas, "Id", "Nome", bovino.RacaId);
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", bovino.FornecedorId);
            return View(bovino);
        }

        // GET: Bovino/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bovino bovino = db.Bovinos.Find(id);
            if (bovino == null)
            {
                return HttpNotFound();
            }
            ViewBag.RacaId = new SelectList(db.Racas, "Id", "Nome", bovino.RacaId);
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", bovino.FornecedorId);
            return View(bovino);
        }

        // POST: Bovino/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brinco,RacaId,FornecedorId,Peso,DtNascimento,VlrUnitario,Descricao")] Bovino bovino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bovino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RacaId = new SelectList(db.Racas, "Id", "Nome", bovino.RacaId);
            ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", bovino.FornecedorId);
            return View(bovino);
        }

        // GET: Bovino/Delete/5
        public ActionResult Delete(long? id)
        {
            Bovino bovino = db.Bovinos.Find(id);
            if (bovino != null)
            {
                db.Bovinos.Remove(bovino);
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
