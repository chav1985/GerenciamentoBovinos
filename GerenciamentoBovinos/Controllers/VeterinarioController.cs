using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VeterinarioController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Veterinario
        public ActionResult Index()
        {
            return View(db.Veterinarios.ToList());
        }

        // GET: Veterinario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veterinario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CRMV,Email,Telefone,Endereco,Cidade,Estado,CPFCNPJ")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Veterinarios.Add(veterinario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }

        // GET: Veterinario/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = db.Veterinarios.Find(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CRMV,Email,Telefone,Endereco,Cidade,Estado,CPFCNPJ")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veterinario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veterinario);
        }

        // GET: Bovino/Delete/5
        public ActionResult Delete(long? id)
        {
            Veterinario veterinario = db.Veterinarios.Find(id);
            if (veterinario != null)
            {
                db.Veterinarios.Remove(veterinario);
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
