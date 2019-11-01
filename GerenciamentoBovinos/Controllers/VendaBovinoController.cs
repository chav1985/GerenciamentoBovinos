using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            Session["Items"] = items;
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");
            List<Bovino> listaBovino = db.Confinamentos.Select(x => x.Bovino).ToList();
            ViewBag.BovinoId = new SelectList(listaBovino, "Id", "Brinco");

            if (db.Confinamentos != null && db.Confinamentos.Count() != 0)
            {
                ViewBag.VlrCusto = db.Confinamentos.FirstOrDefault().CustoTotal.ToString("C");
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

        //GET
        public JsonResult VlrCustoBovino(int id)
        {
            decimal custo = db.Confinamentos.FirstOrDefault(p => p.BovinoId == id).CustoTotal;

            return Json(custo.ToString("C"), JsonRequestBehavior.AllowGet);
        }

        //GET
        public JsonResult AddBovino(int selected, int margem, int remove)
        {
            items = (List<ItemsVendaBovino>)Session["Items"];

            if (remove != -1 && items.Count > 0)
            {
                items.RemoveAt(remove);
                return Json(items, JsonRequestBehavior.AllowGet);
            }

            ItemsVendaBovino obj = new ItemsVendaBovino();

            Confinamento confinamento = db.Confinamentos.Include(b => b.Bovino).FirstOrDefault(x => x.BovinoId == selected);

            obj.BovinoId = selected;
            obj.Bovino = confinamento.Bovino;
            obj.ValorUnitario = confinamento.CustoTotal;

            //Adiciona objeto na lista
            items.Add(obj);

            //retorna um objeto JSON
            return Json(items, JsonRequestBehavior.AllowGet);
            //items = (List<ItemsVendaProduto>)Session["Items"];

            //if (remove != -1 && items.Count > 0)
            //{
            //    items.RemoveAt(remove);
            //    return Json(items, JsonRequestBehavior.AllowGet);
            //}

            //Produto prod = db.Produtos.Find(selected);

            //ItemsVendaProduto obj = new ItemsVendaProduto();
            //obj.ProdutoId = selected;
            //obj.Produto = prod;
            //obj.Qtd = qtd;
            //obj.ValorUnitario = prod.Valor * margem / 100 + prod.Valor;
            //obj.ValorTotal = obj.ValorUnitario * qtd;

            ////Adiciona objeto na lista
            //items.Add(obj);

            ////retorna um objeto JSON
            //return Json(items, JsonRequestBehavior.AllowGet);
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
