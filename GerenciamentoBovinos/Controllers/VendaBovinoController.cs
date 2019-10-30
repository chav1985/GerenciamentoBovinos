using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaBovinoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: VendaBovino
        public ActionResult Index()
        {
            var vendaBovinos = db.VendaBovinos.Include(v => v.Cliente);
            return View(vendaBovinos.ToList());
        }

        // GET: VendaBovino/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaBovino vendaBovino = db.VendaBovinos.Find(id);
            if (vendaBovino == null)
            {
                return HttpNotFound();
            }
            return View(vendaBovino);
        }

        // GET: VendaBovino/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");
            return View();
        }

        // POST: VendaBovino/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId,TotalPedido")] VendaBovino vendaBovino)
        {
            if (ModelState.IsValid)
            {
                db.VendaBovinos.Add(vendaBovino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "InscricaoEstadual", vendaBovino.ClienteId);
            return View(vendaBovino);
        }

        // GET: VendaBovino/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaBovino vendaBovino = db.VendaBovinos.Find(id);
            if (vendaBovino == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "InscricaoEstadual", vendaBovino.ClienteId);
            return View(vendaBovino);
        }

        // POST: VendaBovino/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId,TotalPedido")] VendaBovino vendaBovino)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendaBovino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "InscricaoEstadual", vendaBovino.ClienteId);
            return View(vendaBovino);
        }

        // GET: VendaBovino/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaBovino vendaBovino = db.VendaBovinos.Find(id);
            if (vendaBovino == null)
            {
                return HttpNotFound();
            }
            return View(vendaBovino);
        }

        // POST: VendaBovino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            VendaBovino vendaBovino = db.VendaBovinos.Find(id);
            db.VendaBovinos.Remove(vendaBovino);
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
