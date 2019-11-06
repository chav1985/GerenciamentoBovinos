using GerenciamentoBovinos.Models;
using System;
using System.Collections.Generic;
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
        public ActionResult Index(string retorno = null)
        {
            ViewBag.Retorno = retorno;
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
                Bovino validaBrinco = db.Bovinos.FirstOrDefault(x => x.Brinco == bovino.Brinco);

                if (validaBrinco != null)
                {
                    ModelState.AddModelError("Brinco", "Brinco já Cadastrado!");
                    ViewBag.RacaId = new SelectList(db.Racas, "Id", "Nome", bovino.RacaId);
                    ViewBag.FornecedorId = new SelectList(db.Fornecedores, "Id", "Nome", bovino.FornecedorId);
                    return View(bovino);
                }

                Confinamento confinamento = new Confinamento();
                confinamento.Bovino = bovino;
                confinamento.BovinoId = bovino.Id;
                confinamento.DtEntrada = DateTime.Now;
                confinamento.CustoTotal = bovino.VlrUnitario;
                confinamento.Vendido = false;
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

            List<ItemsVendaBovino> listaItemsVendaBovinos = db.ItemsVendaBovinos.Where(x => x.BovinoId == id).ToList();
            List<BaixaProduto> listaBaixaProdutos = db.BaixaProdutos.Where(x => x.BovinoId == id).ToList();
            List<Consulta> listaAtendimentos = db.Consultas.Where(x => x.BovinoId == id).ToList();

            if (listaItemsVendaBovinos != null && listaItemsVendaBovinos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Bovino esta relacionado a alguma venda de bovino cadastrada!" });
            }
            else if (listaBaixaProdutos != null && listaBaixaProdutos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Bovino esta relacionado a algum produto consumido em confinamento!" });
            }
            else if (listaAtendimentos != null && listaAtendimentos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Bovino esta relacionado a algum atendimento cadastrado!" });
            }

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