using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GerenciamentoBovinos.Models;

namespace GerenciamentoBovinos.Controllers
{
    public class ComissionadoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Comissionado
        public ActionResult Index()
        {
            return View(db.Comissionados.ToList());
        }

        // GET: Comissionado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comissionado/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Telefone,Endereco,CPFCNPJ")] Comissionado comissionado)
        {
            if (ModelState.IsValid)
            {
                db.Comissionados.Add(comissionado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comissionado);
        }

        // GET: Comissionado/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comissionado comissionado = db.Comissionados.Find(id);
            if (comissionado == null)
            {
                return HttpNotFound();
            }
            return View(comissionado);
        }

        // POST: Comissionado/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Telefone,Endereco,CPFCNPJ")] Comissionado comissionado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comissionado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comissionado);
        }

        // GET: Comissionado/Delete/9
        public ActionResult Delete(long? id)
        {
            Comissionado comissionado = db.Comissionados.Find(id);
            if (comissionado != null)
            {
                db.Comissionados.Remove(comissionado);
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
