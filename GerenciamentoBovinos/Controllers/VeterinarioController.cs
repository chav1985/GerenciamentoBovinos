﻿using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    [Authorize(Roles = "Sistema,Administrativo")]
    public class VeterinarioController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Veterinario
        /// <summary>
        /// Retorna a lista de veterinários cadastrados
        /// </summary>
        /// <param name="retorno"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string retorno = null)
        {
            ViewBag.Retorno = retorno;
            return View(db.Veterinarios.ToList());
        }

        // GET: Veterinario/Create
        /// <summary>
        /// Gera a view para cadastrar um veterinário
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veterinario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Adiciona um veterinário
        /// </summary>
        /// <param name="veterinario"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InscricaoEstadual,CRMV,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Veterinario veterinario)
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
        /// <summary>
        /// Gera a view para editar um veterinário
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Edita um veterinário
        /// </summary>
        /// <param name="veterinario"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InscricaoEstadual,CRMV,Nome,Email,Telefone,Rua,Numero,Estado,Cidade,Cep,Complemento,CPFCNPJ")] Veterinario veterinario)
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
        /// <summary>
        /// Exclui um veterinário
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(long? id)
        {
            Veterinario veterinario = db.Veterinarios.Find(id);

            List<Consulta> listaAtendimento = db.Consultas.Where(x => x.VeterinarioId == id).ToList();

            if (listaAtendimento != null && listaAtendimento.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Este Veterinário esta relacionado a algum atendimento cadastrado!" });
            }

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