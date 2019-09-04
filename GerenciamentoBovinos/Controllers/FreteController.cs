using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class FreteController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Frete
        public ActionResult Index()
        {
            var frete = db.Fretes.Include(b => b.Motorista);
            return View(frete.ToList());

            //return View(db.Fretes.ToList());
        }

     

        // GET: Frete/Create
        public ActionResult Create()
        {
            ViewBag.MotoristaId = new SelectList(db.Motoristas, "Id", "Nome");
            return View();
        }

        // POST: Frete/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateInicio,DateChegada,MotoristaId,Preco,Descricao,PesoSaida,PesoChegada")] Fretes fretes)
        {
            if (ModelState.IsValid)
            {
                db.Fretes.Add(fretes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MotoristaId = new SelectList(db.Motoristas, "Id", "Nome", fretes.MotoristaId);
            return View(fretes);
        }

        // GET: Frete/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fretes fretes = db.Fretes.Find(id);
            if (fretes == null)
            {
                return HttpNotFound();
            }
            ViewBag.MotoristaId = new SelectList(db.Motoristas, "Id", "Nome", fretes.MotoristaId);
            return View(fretes);
        }

        // POST: Frete/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateInicio,DateChegada,MotoristaId,Preco,Descricao,PesoSaida,PesoChegada")] Fretes fretes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fretes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MotoristaId = new SelectList(db.Motoristas, "Id", "Nome", fretes.MotoristaId);
            return View(fretes);
        }

        public ActionResult Delete(long? id)
        {
            Fretes frete = db.Fretes.Find(id);
            if (frete != null)
            {
                db.Fretes.Remove(frete);
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
