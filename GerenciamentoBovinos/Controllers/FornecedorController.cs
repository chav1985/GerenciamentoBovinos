using GerenciamentoBovinos.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class FornecedorController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        [Authorize(Roles = "View")]
        // GET: Fornecedor
        public ActionResult Index()
        {
            return View(db.Fornecedores.ToList());
        }
        [Authorize(Roles = "Create")]
        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InscricaoEstadual,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Fornecedores.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fornecedor);
        }
        [Authorize(Roles = "Edit")]
        // GET: Fornecedor/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InscricaoEstadual,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }
        [Authorize(Roles = "Delete")]
        // GET: Fornecedor/Delete/5
        public ActionResult Delete(long? id)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor != null)
            {
                db.Fornecedores.Remove(fornecedor);
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
