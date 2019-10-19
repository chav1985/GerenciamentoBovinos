using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class ConfinamentoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        private List<ItemsBaixaProduto> items = new List<ItemsBaixaProduto>();

        // GET: Confinamento
        public ActionResult Index()
        {
            var confinamentos = db.Confinamentos.Include(c => c.Bovino);

            foreach (var item in confinamentos)
            {
                item.CustoTotal += item.Bovino.VlrUnitario;
            }

            return View(confinamentos.ToList());
        }

        //GET
        public ActionResult ListaProd(long bovinoId)
        {
            ViewBag.BovinoId = bovinoId;
            ViewBag.Brinco = db.Bovinos.Find(bovinoId).Brinco;
            var listaProd = db.BaixaProdutos.Where(x => x.BovinoId == bovinoId).ToList();

            return View(listaProd);
        }

        //GET
        public ActionResult AddProd(long bovinoId)
        {
            Session["Items"] = items;
            ViewBag.BovinoId = bovinoId;
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.Brinco = db.Bovinos.Find(bovinoId).Brinco;

            if (db.Produtos != null && db.Produtos.Count() != 0)
            {
                ViewBag.ProdutoQtd = db.Produtos.FirstOrDefault().Qtd;
                ViewBag.VlrCusto = db.Produtos.FirstOrDefault().Valor.ToString("C");
            }

            return View();
        }

        // POST: VendaProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProd([Bind(Include = "Id,DtUtilizacao,BovinoId")] BaixaProduto baixaProduto)
        {
            items = (List<ItemsBaixaProduto>)Session["Items"];

            if (ModelState.IsValid && items.Count > 0 && baixaProduto.BovinoId > 0)
            {
                bool retorno = true;
                var listaProd = db.Produtos.ToList();

                foreach (var item in items)
                {
                    foreach (var prod in listaProd)
                    {
                        if (item.ProdutoId == prod.Id)
                        {
                            if (item.Qtd > prod.Qtd)
                            {
                                retorno = false;
                            }
                            prod.Qtd -= item.Qtd;
                        }
                    }
                }

                if (retorno)
                {
                    foreach (var item in items)
                    {
                        item.Produto = null;
                    }

                    baixaProduto.Items = items;

                    //Persistindo os items de venda e dando baixa no estoque de produtos
                    foreach (var item in listaProd)
                    {
                        db.Entry(item).State = EntityState.Modified;
                    }
                    db.BaixaProdutos.Add(baixaProduto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            Thread.Sleep(2000);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", baixaProduto.BovinoId);

            if (db.Produtos != null && db.Produtos.Count() != 0)
            {
                ViewBag.ProdutoQtd = db.Produtos.FirstOrDefault().Qtd;
                ViewBag.VlrCusto = db.Produtos.FirstOrDefault().Valor.ToString("C");
            }

            return View(baixaProduto);
        }

        //GET
        public JsonResult QtdProdutos(int id)
        {
            var qtd = db.Produtos.FirstOrDefault(p => p.Id == id).Qtd;
            var precoProd = db.Produtos.FirstOrDefault(p => p.Id == id).Valor;
            items = (List<ItemsBaixaProduto>)Session["Items"];
            int qtdLista = 0;

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.ProdutoId == id)
                        qtdLista += item.Qtd;
                }
            }

            string[] retorno = new string[2];
            retorno[0] = (qtd - qtdLista).ToString();
            retorno[1] = precoProd.ToString("C");

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        //GET
        public JsonResult AddItem(int qtd, int selected, int remove)
        {
            items = (List<ItemsBaixaProduto>)Session["Items"];

            if (remove != -1 && items.Count > 0)
            {
                items.RemoveAt(remove);
                return Json(items, JsonRequestBehavior.AllowGet);
            }

            Produto prod = db.Produtos.Find(selected);

            ItemsBaixaProduto obj = new ItemsBaixaProduto();
            obj.ProdutoId = selected;
            obj.Produto = prod;
            obj.Qtd = qtd;
            obj.ValorUnitario = prod.Valor;
            obj.ValorTotal = obj.ValorUnitario * qtd;

            //Adiciona objeto na lista
            items.Add(obj);

            //retorna um objeto JSON
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        //// GET: Confinamento/Details/5
        //public ActionResult Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(confinamento);
        //}

        //// GET: Confinamento/Create
        //public ActionResult Create()
        //{
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote");
        //    return View();
        //}

        // POST: Confinamento/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,BovinoId,DtEntrada,DtSaida,CustoTotal")] Confinamento confinamento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Confinamentos.Add(confinamento);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // GET: Confinamento/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // POST: Confinamento/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,BovinoId,DtEntrada,DtSaida,CustoTotal")] Confinamento confinamento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(confinamento).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.BovinoId = new SelectList(db.Bovinos, "Id", "Lote", confinamento.BovinoId);
        //    return View(confinamento);
        //}

        // GET: Confinamento/Delete/5
        //public ActionResult Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    if (confinamento == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(confinamento);
        //}

        // POST: Confinamento/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    Confinamento confinamento = db.Confinamentos.Find(id);
        //    db.Confinamentos.Remove(confinamento);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
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
