using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace Projeto_Tatuou.Controllers
{

    public class EstilosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Estilos
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Estilos.ToList());
        }

        [AllowAnonymous]
        public ActionResult Estilos()
        {
            return View(db.Estilos.Where(p => p.Status == true).OrderBy(p => p.Nome).ToList());
        }

        // GET: Estilos/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estilos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Status")] Estilos estilos, HttpPostedFileBase arquivo)
        {
            string valor = "";
            if (ModelState.IsValid)
            {
                db.Estilos.Add(estilos);
                db.SaveChanges();
                if (arquivo != null)
                {
                    string nomearq = estilos.Nome.ToLower() + ".png";
                    valor = Funcoes.UploadEstilos(arquivo, nomearq);
                    if (valor == "sucesso")
                    {
                        estilos.Foto = nomearq;
                        db.Entry(estilos).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "success|Estilo cadastrado com sucesso";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        System.IO.File.Copy(Request.PhysicalApplicationPath + "images\\user.png", Request.PhysicalApplicationPath + "images\\estilos\\" + estilos.Id + estilos.Nome + ".png");
                        estilos.Foto = estilos.Id + estilos.Nome + ".png";
                        db.Entry(estilos).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["MSG"] = "warning|Problema no Upload da foto: " + valor;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    System.IO.File.Copy(Request.PhysicalApplicationPath + "images\\user.png", Request.PhysicalApplicationPath + "images\\estilos\\" + estilos.Id + estilos.Nome + ".png");
                    estilos.Foto = estilos.Id + estilos.Nome + ".png";
                    db.Entry(estilos).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        // GET: Estilos/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estilos estilos = db.Estilos.Find(id);
            if (estilos == null)
            {
                return HttpNotFound();
            }
            return View(estilos);
        }

        // POST: Estilos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome, Descricao,Status")] Estilos estilos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estilos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estilos);
        }

        // GET: Estilos/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estilos estilos = db.Estilos.Find(id);
            if (estilos == null)
            {
                return HttpNotFound();
            }
            return View(estilos);
        }

        // POST: Estilos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estilos estilos = db.Estilos.Find(id);
            db.Estilos.Remove(estilos);
            db.SaveChanges();
            return RedirectToAction("Index");
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
