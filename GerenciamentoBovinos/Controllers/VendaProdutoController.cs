using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaProdutoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        private List<ItemsVendaProduto> items = new List<ItemsVendaProduto> { new ItemsVendaProduto { Produto = new Produto { NomeProduto = "Produto Teste" }, Qtd = 30, ValorUnitario = 10, ValorTotal = 120 } };

        // GET: VendaProduto
        public ActionResult Index()
        {
            return View(db.VendaProdutos.ToList());
        }

        // GET: VendaProduto/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtos, "Id", "NomeProduto");
            ViewBag.Items = items;

            if (db.Produtos != null && db.Produtos.Count() != 0)
            {
                ViewBag.ProdutoQtd = db.Produtos.FirstOrDefault().Qtd;
            }
            return View();
        }

        // POST: VendaProduto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,MargemVenda")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                db.VendaProdutos.Add(vendaProduto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendaProduto);
        }

        //GET
        public ActionResult AddItem(int qtd, int selected, int margem)
        {
            Produto prod = db.Produtos.Find(selected);

            ItemsVendaProduto obj = new ItemsVendaProduto();
            obj.ProdutoId = selected;
            obj.Produto = prod;
            obj.Qtd = qtd;
            obj.ValorUnitario = prod.Valor * margem / 100 + prod.Valor;
            obj.ValorTotal = obj.ValorUnitario * qtd;

            items.Add(obj);
            ViewBag.Items = items;

            //if (ModelState.IsValid)
            //{
            //    db.ItemsVendaProdutos.Add(obj);
            //    db.SaveChanges();
            //    //return RedirectToAction("Index");
            //}

            //db.VendaProdutos.FirstOrDefault().Items.Add(obj); 

            //return RedirectToAction("Create");
            return PartialView("_PartialItems");
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
            return View(vendaProduto);
        }

        // POST: VendaProduto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DtVenda,PrazoEntrega,MargemVenda")] VendaProduto vendaProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendaProduto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendaProduto);
        }

        // GET: VendaProduto/Delete/5
        public ActionResult Delete(long? id)
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
            return View(vendaProduto);
        }

        //GET
        public int QtdProdutos(int id)
        {
            var qtd = db.Produtos.FirstOrDefault(p => p.Id == id).Qtd;
            return (qtd);
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
