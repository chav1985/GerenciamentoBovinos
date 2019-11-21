using GerenciamentoBovinos.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;

namespace GerenciamentoBovinos.Controllers
{
    public class VendaBovinoController : Controller
    {
        private GerenciamentoContext db = new GerenciamentoContext();
        private List<ItemsVendaBovino> items = new List<ItemsVendaBovino>();

        // GET: VendaBovino
        /// <summary>
        /// Retorna a lista de venda de bovinos
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            var vendaBovinos = db.VendaBovinos.Include(v => v.Cliente);
            return View(vendaBovinos.ToList());
        }

        // GET: VendaBovino/Details/5
        /// <summary>
        /// Retorna os detalhes de um venda de bovino
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ACtionResult</returns>
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaBovino venda = db.VendaBovinos.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: VendaBovino/Create
        /// <summary>
        /// Gera a view para criar um venda de bovinos
        /// </summary>
        /// <returns>ActionResult</returns>
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
        /// <summary>
        /// Adiciona um venda de bovinos
        /// </summary>
        /// <param name="vendaBovino"></param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DtVenda,PrazoEntrega,ClienteId,TotalPedido")] VendaBovino vendaBovino)
        {
            items = (List<ItemsVendaBovino>)Session["Items"];

            if (ModelState.IsValid && items.Count > 0 && vendaBovino.ClienteId > 0)
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

                foreach (var item in items)
                {
                    item.Bovino = null;
                }

                vendaBovino.Items = items;
                vendaBovino.TotalPedido = totalPedido;
                db.VendaBovinos.Add(vendaBovino);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Thread.Sleep(2000);
            List<Confinamento> listaConfinamento = db.Confinamentos.Include(a => a.Bovino).Where(b => b.Vendido == false).ToList();
            List<Bovino> listaBovino = new List<Bovino>();

            foreach (var item in listaConfinamento)
            {
                listaBovino.Add(item.Bovino);
            }

            if (db.Confinamentos != null && db.Confinamentos.Where(b => b.Vendido == false).Count() != 0)
            {
                ViewBag.VlrCusto = db.Confinamentos.Where(x => x.Vendido == false).FirstOrDefault().CustoTotal.ToString("C");
            }

            ViewBag.BovinoId = new SelectList(listaBovino, "Id", "Brinco");
            ViewBag.ClienteId = new SelectList(db.Clientes, "Id", "Nome", vendaBovino.ClienteId);

            return View(vendaBovino);
        }

        // GET: VendaBovino/Delete/5
        /// <summary>
        /// Exclui uma venda de bovinos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(long? id)
        {
            VendaBovino venda = db.VendaBovinos.Find(id);
            if (venda != null)
            {
                var listaBovinos = db.Confinamentos.Where(x => x.Vendido == true).ToList();

                //Retornando os bovinos ao confinamento
                foreach (var item in venda.Items)
                {
                    foreach (var bovino in listaBovinos)
                    {
                        if (item.BovinoId == bovino.BovinoId)
                        {
                            bovino.Vendido = false;
                        }
                        db.Entry(bovino).State = EntityState.Modified;
                    }
                }

                List<ItemsVendaBovino> listaVendaBovinos = db.ItemsVendaBovinos.Where(x => x.VendaBovinoId == id).ToList();
                db.ItemsVendaBovinos.RemoveRange(listaVendaBovinos);
                db.VendaBovinos.Remove(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return new HttpNotFoundResult();
        }

        //GET
        /// <summary>
        /// Retorna o valor de custo de determinado bovino
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult VlrCustoBovino(int id)
        {
            decimal custo = db.Confinamentos.FirstOrDefault(p => p.BovinoId == id).CustoTotal;

            return Json(custo.ToString("C"), JsonRequestBehavior.AllowGet);
        }

        //GET
        /// <summary>
        /// Adiciona um bovino a lista de compra
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="margem"></param>
        /// <param name="remove"></param>
        /// <returns>ActionResult</returns>
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