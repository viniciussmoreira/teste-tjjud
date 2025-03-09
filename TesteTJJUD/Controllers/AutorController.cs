﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteTJJUD.Data;
using TesteTJJUD.Models;

namespace TesteTJJUD.Controllers
{
    public class AutorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutorController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(_context.Autores.ToList());
        }
        public ActionResult Edit(int id)
        {
            var autor = _context.Autores.Find(id);
            if (autor == null)
                return HttpNotFound();

            ViewBag.qtdLivros = _context.LivroAutores.Count(x => x.Autor_CodAu.Equals(id));
            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
            return View(autor);
        }
        [HttpPost]
        public ActionResult Edit(int id, Autor autor)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Autores.Find(id);
                if (item == null)
                {
                    return HttpNotFound();
                }

                // Atualiza os campos necessários
                item.Nome = autor.Nome;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }

        public ActionResult Delete(int id)
        {
            var autor = _context.Autores.Find(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var autor = _context.Autores.Find(id);
            if (autor != null)
            {
                _context.Autores.Remove(autor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Autores.Add(autor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autor);
        }


        public ActionResult Details(int id)
        {
            var autor = _context.Autores.Find(id);            
            if (autor == null)
            {
                return HttpNotFound();
            }
            ViewBag.qtdLivros = _context.LivroAutores.Count(x => x.Autor_CodAu.Equals(id));
            return View(autor);
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