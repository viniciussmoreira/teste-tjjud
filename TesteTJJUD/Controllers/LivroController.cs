using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            ValidaAutorAssunto(autoresSelecionados, assuntosSelecionados);

            if (ModelState.IsValid)
            {
                try
                {
                    livro.LivroAutores = autoresSelecionados.Select(autorId =>
                        new LivroAutor { Autor_CodAu = autorId, Livro_Codl = livro.Codl }).ToList();

                    livro.LivroAssuntos = assuntosSelecionados.Select(assuntoId =>
                        new LivroAssunto { Assunto_CodAs = assuntoId, Livro_Codl = livro.Codl }).ToList();

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

        private void ValidaAutorAssunto(int[] autoresSelecionados, int[] assuntosSelecionados)
        {
            if (autoresSelecionados == null || !autoresSelecionados.Any())
                ModelState.AddModelError("LivroAutores", "Selecione pelo menos um autor.");


            if (assuntosSelecionados == null || !assuntosSelecionados.Any())
                ModelState.AddModelError("LivroAssuntos", "Selecione pelo menos um assunto.");
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

        public ActionResult Edit(int id)
        {
            var livro = _context.Livros.Find(id);
            if (livro == null)
                return HttpNotFound();

            // Autores|Assuntos gravados pra vir selecionado
            var autoresSelecionados = _context.LivroAutores
                .Where(la => la.Livro_Codl == id)
                .Select(la => la.Autor_CodAu)
                .ToList();

            var assuntosSelecionados = _context.LivroAssuntos
                .Where(la => la.Livro_Codl == id)
                .Select(la => la.Assunto_CodAs)
                .ToList();
                        
            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.Assuntos = _context.Assuntos.ToList();
            ViewBag.AutoresSelecionados = autoresSelecionados;
            ViewBag.AssuntosSelecionados = assuntosSelecionados;

            //ViewBag.listaAutores = _context.LivroAutores.Where(x => x.Livro_Codl.Equals(id))
            //    .Select(x => x.Autor.Nome).ToList();

            //ViewBag.listaAssuntos = _context.LivroAssuntos.Where(x => x.Livro_Codl.Equals(id))
            //    .Select(x => x.Assunto.Descricao).ToList();

            return View(livro);
        }
        [HttpPost]
        public ActionResult Edit(int id, Livro livro, int[] autoresSelecionados, int[] assuntosSelecionados)
        {
            ValidaAutorAssunto(autoresSelecionados, assuntosSelecionados);

            if (ModelState.IsValid)
            {
                var livroDb = _context.Livros.Find(id);
                if (livroDb == null)
                    return HttpNotFound();

                livroDb.Titulo = livro.Titulo;
                livroDb.Editora = livro.Editora;
                livroDb.Edicao = livro.Edicao;
                livroDb.AnoPublicacao = livro.AnoPublicacao;
                livroDb.Valor = livro.Valor;

                // Atualizar Autores
                var autoresAtuais = _context.LivroAutores.Where(la => la.Livro_Codl == id).ToList();
                var novosAutores = autoresSelecionados ?? new int[0];

                // Remover autores não selecionados
                foreach (var autor in autoresAtuais)
                {
                    if (!novosAutores.Contains(autor.Autor_CodAu))
                        _context.LivroAutores.Remove(autor);
                }

                // Adicionar novos autores
                foreach (var autorId in novosAutores)
                {
                    if (!autoresAtuais.Any(a => a.Autor_CodAu == autorId))
                        _context.LivroAutores.Add(new LivroAutor { Livro_Codl = id, Autor_CodAu = autorId });
                }

                // Atualizar Assuntos
                var assuntosAtuais = _context.LivroAssuntos.Where(la => la.Livro_Codl == id).ToList();
                var novosAssuntos = assuntosSelecionados ?? new int[0];

                // Remover assuntos não selecionados
                foreach (var assunto in assuntosAtuais)
                {
                    if (!novosAssuntos.Contains(assunto.Assunto_CodAs))
                        _context.LivroAssuntos.Remove(assunto);
                }

                // Adicionar novos assuntos
                foreach (var assuntoId in novosAssuntos)
                {
                    if (!assuntosAtuais.Any(a => a.Assunto_CodAs == assuntoId))
                        _context.LivroAssuntos.Add(new LivroAssunto { Livro_Codl = id, Assunto_CodAs = assuntoId });
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
            ViewBag.listaAutores = _context.LivroAutores.Where(x => x.Livro_Codl.Equals(id))
                .Select(x => x.Autor.Nome).ToList();

            ViewBag.listaAssuntos = _context.LivroAssuntos.Where(x => x.Livro_Codl.Equals(id))
                .Select(x => x.Assunto.Descricao).ToList();

            if (livro == null)
                return HttpNotFound();

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
