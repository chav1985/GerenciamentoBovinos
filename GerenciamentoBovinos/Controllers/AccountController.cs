using GerenciamentoBovinos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GerenciamentoBovinos.Controllers
{

    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = "../Home/Index";
            return View(new Acesso());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(Acesso login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (GerenciamentoContext db = new GerenciamentoContext())
                {
                    var vLogin = db.Acessos.Where(p => p.Email.Equals(login.Email)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. 
                    Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/
                        if (Equals(vLogin.Ativo, "S"))
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a 
                            senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.Senha, login.Senha))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.Email, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*código abaixo cria uma session para armazenar o nome do usuário*/
                                Session["Nome"] = vLogin.Nome;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                                Session["Sobrenome"] = vLogin.Sobrenome;
                                /*retorna para a tela inicial do Home*/
                                return RedirectToAction("Index", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new Acesso());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                            /*Retorna a tela de login*/
                            return View(new Acesso());
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "E-mail informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new Acesso());
                    }
                }
            }
            /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login 
            com as mensagem dos campos*/
            return View(login);
        }
    }
}