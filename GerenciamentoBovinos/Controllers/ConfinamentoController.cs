using GerenciamentoBovinos.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class ConfinamentoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Confinamento
        public ActionResult Index()
        {
            var confinamentos = db.Confinamentos.Include(c => c.Bovino);

            foreach (var item in confinamentos)
            {
                item.CustoTotal += item.Bovino.VlrUnitario;
            }

            return View(confinamentos.ToList());
        }

        //GET
        public ActionResult ListaProd(long bovinoId)
        {
            ViewBag.Brinco = db.Bovinos.Find(bovinoId).Brinco;
            var listaProd = db.BaixaProdutos.Where(x => x.BovinoId == bovinoId).ToList();

            return View(listaProd);
        }

        //// GET: Confinamento/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(confinamento);
        //}

        //// GET: Confinamento/Create
        //public ActionResult Create()
        //{
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote");
        //    return View();
        //}

        // POST: Confinamento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,BovinoId,DtEntrada,DtSaida,CustoTotal")] Confinamento confinamento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Confinamentos.Add(confinamento);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // GET: Confinamento/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // POST: Confinamento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,BovinoId,DtEntrada,DtSaida,CustoTotal")] Confinamento confinamento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(confinamento).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // GET: Confinamento/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(confinamento);
        //}

        // POST: Confinamento/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    db.Confinamentos.Remove(confinamento);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
