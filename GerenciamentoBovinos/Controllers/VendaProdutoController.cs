using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId")] VendaProduto vendaProduto)
        {
            items = (List<ItemsVendaProduto>)Session["Items"];

            if (ModelState.IsValid && items.Count > 0 && vendaProduto.ClienteId > 0)
            {
                bool retorno = true;
                var listaProd = db.Produtos.ToList();
                decimal valorTotal = 0;

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

                    valorTotal += item.ValorTotal;
                }

                if (retorno)
                {
                    foreach (var item in items)
                    {
                        item.Produto = null;
                    }

                    vendaProduto.Items = items;
                    vendaProduto.TotalPedido = valorTotal;

                    //Persistindo os items de venda e dando baixa no estoque de produtos
                    foreach (var item in listaProd)
                    {
                        db.Entry(item).State = EntityState.Modified;
                    }
                    db.VendaProdutos.Add(vendaProduto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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

        // GET: VendaProduto/Delete/5
        public ActionResult Delete(long? id)
        {
            VendaProduto venda = db.VendaProdutos.Find(id);
            if (venda != null)
            {
                var listaProd = db.Produtos.ToList();

                //Retornando os produtos ao estoque
                foreach (var item in venda.Items)
                {
                    foreach (var prod in listaProd)
                    {
                        if (item.ProdutoId == prod.Id)
                        {
                            prod.Qtd += item.Qtd;
                        }
                        db.Entry(prod).State = EntityState.Modified;
                    }
                }

                db.VendaProdutos.Remove(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
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
        public JsonResult AddItem(int qtd, int selected, int margem, int remove)
        {
            items = (List<ItemsVendaProduto>)Session["Items"];

            if (remove != -1 && items.Count > 0)
            {
                items.RemoveAt(remove);
                return Json(items, JsonRequestBehavior.AllowGet);
            }

            Produto prod = db.Produtos.Find(selected);

            ItemsVendaProduto obj = new ItemsVendaProduto();
            obj.ProdutoId = selected;
            obj.Produto = prod;
            obj.Qtd = qtd;
            obj.ValorUnitario = prod.Valor * margem / 100 + prod.Valor;
            obj.ValorTotal = obj.ValorUnitario * qtd;

            //Adiciona objeto na lista
            items.Add(obj);

            //retorna um objeto JSON
            return Json(items, JsonRequestBehavior.AllowGet);
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