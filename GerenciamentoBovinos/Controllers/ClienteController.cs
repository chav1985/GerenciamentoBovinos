using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class ClienteController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Cliente
        public ActionResult Index(string retorno = null)
        {
            ViewBag.Retorno = retorno;
            return View(db.Clientes.ToList());
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InscricaoEstadual,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InscricaoEstadual,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(long? id)
        {
            Cliente cliente = db.Clientes.Find(id);

            List<VendaProduto> listaVendaProdutos = db.VendaProdutos.Where(x => x.ClienteId == id).ToList();

            if (listaVendaProdutos != null && listaVendaProdutos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Cliente esta relacionado a alguma venda de produto cadastrado!" });
            }

            if (cliente != null)
            {
                db.Clientes.Remove(cliente);
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