using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaBovinoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        private List<ItemsVendaBovino> items = new List<ItemsVendaBovino>();

        // GET: VendaBovino
        public ActionResult Index()
        {
            var vendaBovinos = db.VendaBovinos.Include(v => v.Cliente);
            return View(vendaBovinos.ToList());
        }

        // GET: VendaBovino/Create
        public ActionResult Create()
        {
            Session["Items"] = items;
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");

            List<Confinamento> listaConfinamento = db.Confinamentos.Include(a => a.Bovino).Where(b => b.Vendido == false).ToList();
            List<Bovino> listaBovino = new List<Bovino>();

            foreach (var item in listaConfinamento)
            {
                listaBovino.Add(item.Bovino);
            }

            ViewBag.BovinoId = new SelectList(listaBovino, "Id", "Brinco");

            if (db.Confinamentos != null && db.Confinamentos.Where(b => b.Vendido == false).Count() != 0)
            {
                ViewBag.VlrCusto = db.Confinamentos.Where(x => x.Vendido == false).FirstOrDefault().CustoTotal.ToString("C");
            }

            return View();
        }

        // POST: VendaBovino/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId,TotalPedido")] VendaBovino vendaBovino)
        {
            items = (List<ItemsVendaBovino>)Session["Items"];

            if (ModelState.IsValid)
            {
                List<Confinamento> confinamentos = new List<Confinamento>();
                decimal totalPedido = 0;

                foreach (var item in items)
                {
                    confinamentos.Add(db.Confinamentos.FirstOrDefault(x => x.BovinoId == item.BovinoId));
                    totalPedido += item.ValorUnitario;
                }

                foreach (var item in confinamentos)
                {
                    item.Vendido = true;
                    db.Entry(item).State = EntityState.Modified;
                }

                vendaBovino.TotalPedido = totalPedido;
                db.VendaBovinos.Add(vendaBovino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", vendaBovino.ClienteId);
            return View(vendaBovino);
        }

        //GET
        public JsonResult VlrCustoBovino(int id)
        {
            decimal custo = db.Confinamentos.FirstOrDefault(p => p.BovinoId == id).CustoTotal;

            return Json(custo.ToString("C"), JsonRequestBehavior.AllowGet);
        }

        //GET
        public ActionResult AddBovino(int selected, int margem, int remove)
        {
            items = (List<ItemsVendaBovino>)Session["Items"];

            if (remove != -1 && items.Count > 0)
            {
                items.RemoveAt(remove);
                var excluir = new { lista = items, text = "" };
                return Json(excluir, JsonRequestBehavior.AllowGet);
            }

            ItemsVendaBovino contem = items.FirstOrDefault(x => x.BovinoId == selected);
            if (contem != null)
            {
                var cadastrado = new { lista = items, text = "Bovino já adicionado!" };
                return Json(cadastrado, JsonRequestBehavior.AllowGet);
            }

            ItemsVendaBovino obj = new ItemsVendaBovino();

            Confinamento confinamento = db.Confinamentos.Include(b => b.Bovino).FirstOrDefault(x => x.BovinoId == selected);

            obj.BovinoId = selected;
            obj.Bovino = confinamento.Bovino;
            obj.ValorUnitario = confinamento.CustoTotal * margem / 100 + confinamento.CustoTotal;

            //Adiciona objeto na lista
            items.Add(obj);

            //retorna um objeto JSON
            var result = new { lista = items, text = "" };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //// GET: VendaBovino/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VendaBovino vendaBovino = db.VendaBovinos.Find(id);
        //    if (vendaBovino == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "InscricaoEstadual", vendaBovino.ClienteId);
        //    return View(vendaBovino);
        //}

        //// POST: VendaBovino/Edit/5
        //// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        //// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId,TotalPedido")] VendaBovino vendaBovino)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(vendaBovino).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "InscricaoEstadual", vendaBovino.ClienteId);
        //    return View(vendaBovino);
        //}

        //// GET: VendaBovino/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VendaBovino vendaBovino = db.VendaBovinos.Find(id);
        //    if (vendaBovino == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vendaBovino);
        //}

        //// POST: VendaBovino/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    VendaBovino vendaBovino = db.VendaBovinos.Find(id);
        //    db.VendaBovinos.Remove(vendaBovino);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //// GET: VendaBovino/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VendaBovino vendaBovino = db.VendaBovinos.Find(id);
        //    if (vendaBovino == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vendaBovino);
        //}

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