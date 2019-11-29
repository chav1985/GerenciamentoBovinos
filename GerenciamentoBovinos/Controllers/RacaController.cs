using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    [Authorize(Roles = "Sistema,Administrativo,Comercial")]
    public class RacaController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();

        // GET: Raca
        /// <summary>
        /// Retorna a lista de Raças cadastradas
        /// </summary>
        /// <param name="retorno"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Index(string retorno = null)
        {
            ViewBag.Retorno = retorno;
            return View(db.Racas.ToList());
        }

        // GET: Raca/Create
        /// <summary>
        /// Gera a view para cadastrar raça
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Raca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Cadastra um raça
        /// </summary>
        /// <param name="raca"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                db.Racas.Add(raca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(raca);
        }

        // GET: Raca/Edit/5
        /// <summary>
        /// Gera a view para editar raça
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raca raca = db.Racas.Find(id);
            if (raca == null)
            {
                return HttpNotFound();
            }
            return View(raca);
        }

        // POST: Raca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edita um raça
        /// </summary>
        /// <param name="raca"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao")] Raca raca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(raca);
        }

        // GET: Raca/Delete/5
        /// <summary>
        /// Exclui uma raça
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(long? id)
        {
            Raca raca = db.Racas.Find(id);

            List<Bovino> listaBovinos = db.Bovinos.Where(x => x.RacaId == id).ToList();

            if (listaBovinos != null && listaBovinos.Count > 0)
            {
                return RedirectToAction("Index", new { retorno = "Esta Raça esta relacionado a algum bovino cadastrado!" });
            }

            if (raca != null)
            {
                db.Racas.Remove(raca);
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