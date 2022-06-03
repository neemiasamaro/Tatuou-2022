using WebApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private Contexto db = new Contexto();

        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Estilos.ToList());
        }

        public ActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Cadastro cad)
        {
            if (ModelState.IsValid)
            {
                if (db.Usuario.Where(x => x.Email == cad.Email).ToList().Count > 0)
                {
                    ModelState.AddModelError("", "E-mail já utilizado!");
                    return View(cad);
                }

                Usuario usu = new Usuario();
                usu.Nome = cad.Nome;
                usu.Email = cad.Email;
                usu.Senha = Funcoes.HashTexto(cad.Senha, "SHA512");
                usu.Cpf = cad.Cpf;
                usu.Status = true;
                usu.Perfil = db.Perfil.Find(2);
                if (usu.Perfil == null)
                {
                    ModelState.AddModelError("", "Não existe o perfil para cadastro");
                    return View(cad);
                }
                db.Usuario.Add(usu);
                db.SaveChanges();
                TempData["MSG"] = "success|Usuário Cadastrado com Sucesso";
                return RedirectToAction("Cadastro", "Estudios");
            }
            return View(cad);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Acesso acesso)
        {
            if (ModelState.IsValid)
            {
                string senhacrip = Funcoes.HashTexto(acesso.Senha, "SHA512");
                Usuario usu = db.Usuario.Where(x => x.Email == acesso.Email && x.Senha == senhacrip).ToList().FirstOrDefault();
                if (usu != null)
                {
                    FormsAuthentication.SetAuthCookie(usu.Id + "|" + usu.Nome, true);
                    string permissoes = usu.Perfil.Descricao;
                    FormsAuthenticationTicket ticket = new
                    FormsAuthenticationTicket(1, usu.Id + "|" + usu.Email + "|" + usu.PerfilId,
                    DateTime.Now, DateTime.Now.AddMinutes(30), true, permissoes);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new
                    HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    Response.Cookies.Add(cookie);
                    if (usu.Perfil.Descricao == "Admin")
                    {
                        return RedirectToAction("Estudios", "Estudios");
                    }
                    else
                    {
                        return RedirectToAction("Cadastro", "Estudios");
                    }
                }
                else
                {
                    TempData["MSG"] = "error|E-mail e/ou Senha inválidos ou não encontrados";
                    return View(acesso);
                }
            }
            return View(acesso);
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Email(Mensagem msg)
        {
            if (ModelState.IsValid)
            {
                TempData["MSG"] = Funcoes.EnviarEmail(msg.Email, msg.Assunto, msg.CorpoMsg);
            }
            else
            {
                TempData["MSG"] = "warning|Preencha todos os campos";
            }
            return View(msg);
        }

        public ActionResult EsqueceuSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EsqueceuSenha(EsqueceuSenha esq)
        {
            if (ModelState.IsValid)
            {
                var usu = db.Usuario.Where(x => x.Email == esq.Email).ToList().FirstOrDefault();
                if (usu != null)
                {
                    usu.Hash = Funcoes.Codifica(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                    string msg = "<h3>Sistema</h3>";
                    msg += "Para alterar sua senha <a href='http://localhost:26786/Home/Redefinir/" + usu.Hash + "' target='_blank'>clique aqui</a>";
                    TempData["MSG"] = Funcoes.EnviarEmail(usu.Email, "Redefinição de senha", msg);
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|E-mail não encontrado";
                return View();
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View();
        }

        public ActionResult Redefinir(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var usu = db.Usuario.Where(x => x.Hash == id).ToList().FirstOrDefault();
                if (usu != null)
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(Funcoes.Decodifica(usu.Hash));
                        if (dt > DateTime.Now)
                        {
                            RedefinirSenha red = new RedefinirSenha();
                            red.Hash = usu.Hash;
                            red.Email = usu.Email;
                            return View(red);
                        }
                        TempData["MSG"] = "warning|Esse link já expirou!";
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        TempData["MSG"] = "error|Hash inválida!";
                        return RedirectToAction("Index");
                    }
                }
                TempData["MSG"] = "error|Hash inválida!";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "error|Acesso inválido!";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Redefinir(RedefinirSenha red)
        {
            if (ModelState.IsValid)
            {
                var usu = db.Usuario.Where(x => x.Hash == red.Hash).ToList().FirstOrDefault();
                if (usu != null)
                {
                    usu.Hash = null;
                    usu.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha redefinida com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MSG"] = "error|E-mail não encontrado";
                return View(red);
            }
            TempData["MSG"] = "warning|Preencha todos os campos";
            return View(red);
        }



    }
}