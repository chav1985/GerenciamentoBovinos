using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class ConsultaController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Consulta
        public ActionResult Index()
        {
            var consultas = db.Consultas.Include(c => c.Bovino).Include(c => c.Veterinario);
            return View(consultas.ToList());
        }

        // GET: Consulta/Create
        public ActionResult Create()
        {
            ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Brinco");
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "Id", "Nome");
            return View();
        }

        // POST: Consulta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BovinoId,VeterinarioId,Descricao,DtServico,Valor")] Consulta consulta)
        {
            if (ModelState.IsValid && consulta.BovinoId > 0 && consulta.VeterinarioId > 0)
            {
                db.Consultas.Add(consulta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Thread.Sleep(2000);
            ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Brinco", consulta.BovinoId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "Id", "Nome", consulta.VeterinarioId);
            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = db.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Brinco", consulta.BovinoId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "Id", "Nome", consulta.VeterinarioId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BovinoId,VeterinarioId,Descricao,DtServico,Valor")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consulta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Brinco", consulta.BovinoId);
            ViewBag.VeterinarioId = new SelectList(db.Veterinarios, "Id", "Nome", consulta.VeterinarioId);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
        public ActionResult Delete(long? id)
        {
            Consulta consulta = db.Consultas.Find(id);
            if (consulta != null)
            {
                db.Consultas.Remove(consulta);
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
