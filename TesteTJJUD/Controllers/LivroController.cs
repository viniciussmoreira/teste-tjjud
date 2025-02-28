using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteTJJUD.Data;
using TesteTJJUD.Models;

namespace TesteTJJUD.Controllers
{
    public class LivroController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivroController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var livros = _context.Livros.ToList();
            return View(livros);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Livros.Add(livro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livro);

        }

        public ActionResult Edit(int id)
        {

            var livro = _context.Livros.Find(id);
            if (livro == null)
                return HttpNotFound();

            return View(livro);
        }

        [HttpPost]
        public ActionResult Edit(int id, Livro livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(livro).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    // Trate o erro conforme necessário
                }
            }
            return View(livro);
        }

        public ActionResult Details(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        public ActionResult Delete(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
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
