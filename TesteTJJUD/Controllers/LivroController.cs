using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Livro livro, int[] autoresSelecionados, int[] assuntosSelecionados)
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
            }


            if (ModelState.IsValid)
            {
                try
                {


                    livro.LivroAutores = new List<LivroAutor>();
                    livro.LivroAssuntos = new List<LivroAssunto>();

                    if (autoresSelecionados != null)
                    {
                        livro.LivroAutores = new List<LivroAutor>();
                        foreach (var autorId in autoresSelecionados)
                        {
                            livro.LivroAutores.Add(new LivroAutor { Autor_CodAu = autorId });
                        }
                    }

                    if (assuntosSelecionados != null)
                    {
                        livro.LivroAssuntos = new List<LivroAssunto>();
                        foreach (var assuntoId in assuntosSelecionados)
                        {
                            livro.LivroAssuntos.Add(new LivroAssunto { Assunto_CodAs = assuntoId });
                        }
                    }

                    _context.Livros.Add(livro);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    var innerMessage = ex.InnerException?.InnerException?.Message ?? ex.Message;
                    ModelState.AddModelError("", "Erro ao salvar no banco: " + innerMessage);
                }
            }

            // Volta para a view com os dados corretos
            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
            return View(livro);
        }


        public ActionResult Edit(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
                return HttpNotFound();

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
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

        [HttpPost]
        public ActionResult Edit(int id, Livro livro, int[] autoresSelecionados, int[] assuntosSelecionados)
        {
            if (ModelState.IsValid)
            {
                var livroDb = _context.Livros.Find(id);
                if (livroDb == null)
                    return HttpNotFound();

                livroDb.Titulo = livro.Titulo;
                livroDb.Editora = livro.Editora;
                livroDb.Edicao = livro.Edicao;
                livroDb.AnoPublicacao = livro.AnoPublicacao;

                // Atualizar Autores
                livroDb.LivroAutores.Clear();
                if (autoresSelecionados != null)
                {
                    foreach (var autorId in autoresSelecionados)
                    {
                        livroDb.LivroAutores.Add(new LivroAutor { Autor_CodAu = autorId });
                    }
                }

                // Atualizar Assuntos
                livroDb.LivroAssuntos.Clear();
                if (assuntosSelecionados != null)
                {
                    foreach (var assuntoId in assuntosSelecionados)
                    {
                        livroDb.LivroAssuntos.Add(new LivroAssunto { Assunto_CodAs = assuntoId });
                    }
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
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
