using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity;
using System.IO;

namespace WebApplication1.Controllers
{
    public class EstudiosController : Controller
    {
        private Contexto db = new Contexto();

        public ActionResult Index()
        {
            return View(db.Estudio.Where(p => p.Disponivel == true).ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Estudios()
        {
            return View(db.Estudio.ToList());
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = db.Estudio.Where(p => p.Id == id).ToList().FirstOrDefault();
            Estudio resultado = db.Estudio.Find(id);
            if (estudio == null || resultado == null)
            {
                TempData["MSG"] = "error|Permissão negada para acesso a esta página";
                return RedirectToAction("Index");
            }
            return View(estudio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Bio,UsuarioId,Bairro,Logradouro,Complemento,Cidade,Estado,Facebook,Instagram,Linkedin,Twitter,Whatsapp,NomeEstudio,Cep,Numero,Cnpj,Disponivel,Foto")] Estudio estudio)
        {

            if (ModelState.IsValid)
            {
                db.Entry(estudio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudio);
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
        public ActionResult EditarEstudios([Bind(Include = "Id,Bio,UsuarioId,Bairro,Logradouro,Complemento,Cidade,Estado,Facebook,Instagram,Linkedin,Twitter,Whatsapp,NomeEstudio,Cep,Numero,Cnpj,Disponivel,Foto")] Estudio estudio)
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
            Estudio estudio;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault() == null)
            {
                return RedirectToAction("Cadastro", "Estudios", new { @id = User.Identity.Name.Split('|')[0] });
            }
            else
            {
               estudio = db.Estudio.Include(x => x.EstudioEstilos).Include(x => x.Portfolio).Where(p => p.UsuarioId == id).ToList().FirstOrDefault();
            }

            var aprovado = db.Estudio.Where(p => p.Disponivel == true).ToList().FirstOrDefault();
            if (aprovado != null)
            {
                ViewBag.Aprovado = "Estúdio aguardando aprovação";
                return View(estudio);
            }
            ViewBag.Aprovado = "";
            if (estudio == null)
            {
                if (db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault().Equals(null) || db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault().Equals(0))
                {
                    TempData["MSG"] = "error|Permissão negada para acesso a esta página";
                    return RedirectToAction("Cadastro");
                }
            }
            return View(estudio);
        }

        public ActionResult Perfil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudio estudio = db.Estudio.Include(x => x.EstudioEstilos).Include(x => x.Portfolio).Where(p => p.Id == id).ToList().FirstOrDefault();
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
                return RedirectToAction("MeuPerfil", "Estudios", new { @id = id });
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
                estudio.Twitter = cad.Twitter;
                estudio.Linkedin = cad.Linkedin;
                estudio.Bio = cad.Bio;
                estudio.Whatsapp = cad.Whatsapp.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
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
                            estudio.Foto = "Uploads\\" + estudio.Id + "\\" + nomearq;
                            db.Entry(estudio).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["MSG"] = "success|Estúdio cadastrado com sucesso";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\" + estudio.Id + "\\FotoEstudio" + estudio.Id
                            + ".png");
                            estudio.Foto = "Uploads\\" + estudio.Id + "\\" + nomearq;
                            db.Entry(estudio).State = EntityState.Modified;
                            db.SaveChanges();
                            TempData["MSG"] = "warning|Problema no Upload da foto: " + valor;
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        System.IO.File.Copy(Request.PhysicalApplicationPath + "Uploads\\user.png", Request.PhysicalApplicationPath + "Uploads\\" + estudio.Id + "\\FotoEstudio" + estudio.Id
                            + ".png");
                        estudio.Foto = "Uploads\\" + estudio.Id + "\\" + "FotoEstudio" + estudio.Id + ".png";
                        db.Entry(estudio).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Estúdio cadastrado com sucesso";
                        return RedirectToAction("Index");
                    }
                }
                db.SaveChanges();
                TempData["MSG"] = "success|Estúdio cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            return View(cad);
        }
        [Authorize(Roles = "Comum, Admin")]
        public ActionResult CadastroEstilos(int? id)
        {
            if (id != null)
            {
                Estudio estudio = db.Estudio.Where(p => p.UsuarioId == id).ToList().FirstOrDefault();
                if (estudio != null)
                {
                    ViewBag.Estilos = db.Estilos.Where(p => p.Status == true).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Portfolio(int id, int idusu,/*string descricao*/ HttpPostedFileBase[] arquivos)
        {
            string valor = "";
            if (ModelState.IsValid)
            {
                Estudio estudio = db.Estudio.Find(id);

                if (arquivos != null)
                {
                    Funcoes.CriarPortfolio(estudio.Id);
                    int cont = 0;
                    foreach (var upload in arquivos)
                    {
                        if (db.Portfolio.Where(x => x.EstudioId == id).ToList().Count < 5)
                        {
                            string extensao = Path.GetExtension(upload.FileName).ToLower();
                            string nomearq = estudio.Id.ToString() + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extensao;
                            valor = Funcoes.UploadPortfolio(upload, nomearq, estudio.Id);
                            if (valor == "sucesso")
                            {
                                Portfolio port = new Portfolio();
                                port.EstudioId = id;
                                port.Imagem = nomearq;
                                //port.Descricao = descricao;
                                db.Portfolio.Add(port);
                                db.SaveChanges();
                                cont++;
                            }
                        }
                    }


                    if (arquivos.Count() > 0)
                    {

                        if (arquivos.Count() == cont)
                        {
                            TempData["MSG"] = "success|Imagens cadastrada com sucesso";
                        }
                        else if (cont > 0)
                        {
                            TempData["MSG"] = "warning|Algumas imagens não cadastradas";
                        }
                        else
                        {
                            TempData["MSG"] = "warning|Nenhuma imagem cadastrada";
                        }
                    }
                    else
                    {
                        TempData["MSG"] = "warning|Nenhuma imagem não cadastrada";
                    }
                }
            }
            return RedirectToAction("MeuPerfil", "Estudios", new { @id = idusu });
        }

    }
}
