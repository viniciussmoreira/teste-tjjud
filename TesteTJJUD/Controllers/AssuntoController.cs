﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteTJJUD.Data;
using TesteTJJUD.Models;

namespace TesteTJJUD.Controllers
{
    public class AssuntoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssuntoController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(_context.Assuntos.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Assunto assunto)
        {
            if (ModelState.IsValid)
            {
                _context.Assuntos.Add(assunto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assunto);
        }
        public ActionResult Edit(int id)
        {
            var assunto = _context.Assuntos.Find(id);
            if (assunto == null)
            {
                return HttpNotFound();
            }
            return View(assunto);
        }

        [HttpPost]
        public ActionResult Edit(int id, Assunto assunto)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Assuntos.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                // Atualiza os campos necessários
                item.Descricao = assunto.Descricao;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assunto);
        }


        public ActionResult Delete(int id)
        {
            var assunto = _context.Assuntos.Find(id);
            if (assunto == null)
            {
                return HttpNotFound();
            }
            return View(assunto);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var assunto = _context.Assuntos.Find(id);
            var livrosRelacionados = _context.LivroAutores
                .Where(x => x.Autor_CodAu.Equals(id))
                .Select(x => x.Livro.Titulo)
                .ToList();

            if(livrosRelacionados.Any())
            {
                ViewBag.Error = "Não é possível excluir o assunto, pois ele está vinculado aos seguintes livros: " +
                       string.Join(", ", livrosRelacionados);
                return View("Delete", assunto);
            }

            if (assunto != null)
            {
                _context.Assuntos.Remove(assunto);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var assunto = _context.Assuntos.Find(id);
            if (assunto == null)
                return HttpNotFound();


            ViewBag.qtdLivros = _context.LivroAutores.Count(x => x.Autor_CodAu.Equals(id));
            return View(assunto);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}