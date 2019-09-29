using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        private List<ItemsVendaProduto> items = new List<ItemsVendaProduto>();

        // GET: VendaProduto
        public ActionResult Index()
        {
            return View(db.VendaProdutos.ToList());
        }

        // GET: VendaProduto/Create
        public ActionResult Create()
        {
            Session["Items"] = items;
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome");

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
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,MargemVenda,ClienteId")] VendaProduto vendaProduto)
        {
            items = (List<ItemsVendaProduto>)Session["Items"];

            if (ModelState.IsValid && items.Count > 0 && vendaProduto.ClienteId > 0)
            {
                foreach (var item in items)
                {
                    item.Produto = null;
                }

                vendaProduto.Items = items;

                db.VendaProdutos.Add(vendaProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Thread.Sleep(2000);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", vendaProduto.ClienteId);

            if (db.Produtos != null && db.Produtos.Count() != 0)
            {
                ViewBag.ProdutoQtd = db.Produtos.FirstOrDefault().Qtd;
                ViewBag.VlrCusto = db.Produtos.FirstOrDefault().Valor.ToString("C");
            }

            return View(vendaProduto);
        }

        // GET: VendaProduto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaProduto vendaProduto = db.VendaProdutos.Find(id);
            if (vendaProduto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", vendaProduto.ClienteId);
            return View(vendaProduto);
        }

        // POST: VendaProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtVenda,PrazoEntrega,MargemVenda,ClienteId")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendaProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", vendaProduto.ClienteId);
            return View(vendaProduto);
        }

        //GET
        public JsonResult QtdProdutos(int id)
        {
            var qtd = db.Produtos.FirstOrDefault(p => p.Id == id).Qtd;
            var precoProd = db.Produtos.FirstOrDefault(p => p.Id == id).Valor;
            items = (List<ItemsVendaProduto>)Session["Items"];
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
        public JsonResult AddItem(int qtd, int selected, int margem)
        {
            Produto prod = db.Produtos.Find(selected);

            ItemsVendaProduto obj = new ItemsVendaProduto();
            obj.ProdutoId = selected;
            obj.Produto = prod;
            obj.Qtd = qtd;
            obj.ValorUnitario = prod.Valor * margem / 100 + prod.Valor;
            obj.ValorTotal = obj.ValorUnitario * qtd;

            items = (List<ItemsVendaProduto>)Session["Items"];
            //Adiciona objeto na lista
            items.Add(obj);

            //retorna um objeto JSON
            return Json(obj, JsonRequestBehavior.AllowGet);
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
