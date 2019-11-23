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
    public class AcessoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Acesso
        public ActionResult Index()
        {
            return View(db.Acessoes.ToList());
        }

        // GET: Acesso/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acesso acesso = db.Acessoes.Find(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            return View(acesso);
        }

        // GET: Acesso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acesso/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_login,Email,Senha,Ativo,Perfil,Nome,Sobrenome")] Acesso acesso)
        {
            if (ModelState.IsValid)
            {
                db.Acessoes.Add(acesso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acesso);
        }

        // GET: Acesso/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acesso acesso = db.Acessoes.Find(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            return View(acesso);
        }

        // POST: Acesso/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_login,Email,Senha,Ativo,Perfil,Nome,Sobrenome")] Acesso acesso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acesso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acesso);
        }

        // GET: Acesso/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acesso acesso = db.Acessoes.Find(id);
            if (acesso == null)
            {
                return HttpNotFound();
            }
            return View(acesso);
        }

        // POST: Acesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Acesso acesso = db.Acessoes.Find(id);
            db.Acessoes.Remove(acesso);
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
