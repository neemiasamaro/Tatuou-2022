using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class EstudiosController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            Estudio estudio = new Estudio();
            var estudioEstilos = db.Estudio.Include(p => p.EstudioEstilos.Where(x => x.EstudioId == estudio.Id).ToList());
            return View(estudioEstilos.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Estudios()
        {
            return View(db.Estudio.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditarEstudios(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = db.Estudio.Find(id);
            if (estudio == null)
            {
                return HttpNotFound();
            }
            return View(estudio);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarEstudios([Bind(Include = "Id,UsuarioId,Bairro,Logradouro,Complemento,Cidade,Estado,Whatsapp,Facebook,Instagram,Site,NomeEstudio,Cep,Numero,Cnpj,Disponivel")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudio);
        }

        public ActionResult MeuPerfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault();
            if (estudio == null)
            {
                TempData["MSG"] = "error|Permissão negada para acessoa a esta página";
                return RedirectToAction("Index");
            }
            return View(estudio);
        }
        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = db.Estudio.Where(p => p.Id == id).ToList().FirstOrDefault();
            if (estudio == null)
            {
                return HttpNotFound();
            }
            return View(estudio);
        }

        [Authorize(Roles = "Comum, Admin")]
        public ActionResult Cadastro()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[0]);
            Estudio estudio = db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault();
            if (estudio != null)
            {
                CadastroEstudio cadastroEstudio = new CadastroEstudio();
                cadastroEstudio.Id = estudio.Id;
                cadastroEstudio.NomeEstudio = estudio.nomeEstudio;
                return View(cadastroEstudio);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(CadastroEstudio cad, HttpPostedFileBase arquivo)
        {
            string valor = "";
            if (ModelState.IsValid)
            {
                Estudio estudio;

                if (cad.Id > 0)
                {
                    estudio = db.Estudio.Find(cad.Id);
                }
                else
                {
                    estudio = new Estudio();
                }
                estudio.nomeEstudio = cad.NomeEstudio;
                estudio.Cep = cad.Cep;
                estudio.Cidade = cad.Cidade;
                estudio.Cnpj = cad.Cnpj;
                estudio.Bairro = cad.Bairro;
                estudio.Complemento = cad.Complemento;
                estudio.Estado = cad.Estado;
                estudio.Facebook = cad.Facebook;
                estudio.Instagram = cad.Instagram;
                estudio.Logradouro = cad.Logradouro;
                estudio.Numero = cad.Numero;
                estudio.Site = cad.Site;
                string cod_usu = User.Identity.Name.Split('|')[0];
                estudio.Usuario = db.Usuario.Find(Convert.ToInt32(cod_usu));
                estudio.Disponivel = false;
                if (cad.Id > 0)
                {
                    db.Entry(estudio).State = EntityState.Modified;
                }
                else
                {
                    db.Estudio.Add(estudio);
                    db.SaveChanges();
                    if (arquivo != null)
                    {
                        Funcoes.CriarDiretorio(estudio.Id);
                        string nomearq = "FotoEstudio" + estudio.Id + ".png";
                        valor = Funcoes.UploadArquivo(arquivo, nomearq, estudio.Id);
                        if (valor == "sucesso")
                        {
                            estudio.Foto = nomearq;
                            db.Entry(estudio).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["MSG"] = "success|Usuário cadastrado com sucesso";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\FotoEstudio" + estudio.Id
                            + ".png");
                            estudio.Foto = "FotoPessoa" + estudio.Id + ".png";
                            db.Entry(estudio).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["MSG"] = "warning|Problema no Upload da foto: " + valor;
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath +
                        "Uploads\\FotoEstudio" + estudio.Id + ".png");
                        estudio.Foto = "FotoPessoa" + estudio.Id + ".png";
                        db.Entry(estudio).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Usuário cadastrado com sucesso";
                        return RedirectToAction("Index");
                    }
                }
                db.SaveChanges();
                TempData["MSG"] = "success|Usuário Cadastrado com Sucesso";
                return RedirectToAction("Index");
            }
            return View(cad);
        }
        [Authorize(Roles = "Comum, Admin")]
        public ActionResult CadastroEstilos(int? id)
        {
            if (id != null)
            {
                Estudio estudio = db.Estudio.Find(id);
                if (estudio != null)
                {
                    ViewBag.Estilos = db.Estilos.Where(p => p.Ativos == true).ToList();
                    return View(estudio);
                }
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult EditarEstilo(string id, string estudioId)
        {
            int idEstilo = Convert.ToInt32(id);
            int idEstudio = Convert.ToInt32(estudioId);

            EstudioEstilo estudioEstilo = db.EstudioEstilo.Where(p => p.EstudioId == idEstudio && p.EstilosId == idEstilo).ToList().FirstOrDefault();
            if (estudioEstilo != null)
            {
                db.EstudioEstilo.Remove(estudioEstilo);
                db.SaveChanges();
                return Json("f");
            }
            else
            {
                estudioEstilo = new EstudioEstilo();
                estudioEstilo.EstilosId = idEstilo;
                estudioEstilo.EstudioId = idEstudio;
                db.EstudioEstilo.Add(estudioEstilo);
                db.SaveChanges();
                return Json("t");
            }
        }

    }
}