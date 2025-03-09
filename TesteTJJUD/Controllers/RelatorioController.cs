using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteTJJUD.Data;

namespace TesteTJJUD.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatorioController()
        {
            _context = new ApplicationDbContext();
        }

       
        public ActionResult Index()
        {
            var dados = _context.VwLivros
                .Select(l => new
                {
                    l.Titulo,
                    l.Editora,
                    l.Autor,
                    l.Assunto,
                    l.AnoPublicacao,
                    l.Valor
                }).ToList();

            var pdfDocument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Relatório de Livros")
                        .SemiBold()
                        .FontSize(18)
                        .AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(200); // Título
                            columns.RelativeColumn(); // Editora
                            columns.RelativeColumn(); // Autor
                            columns.RelativeColumn(); // Assunto
                            columns.ConstantColumn(80); // Ano
                            columns.ConstantColumn(80); // Valor
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Título").Bold();
                            header.Cell().Text("Editora").Bold();
                            header.Cell().Text("Autor").Bold();
                            header.Cell().Text("Assunto").Bold();
                            header.Cell().Text("Ano").Bold();
                            header.Cell().Text("Valor").Bold();
                        });

                        foreach (var livro in dados)
                        {
                            table.Cell().Text(livro.Titulo);
                            table.Cell().Text(livro.Editora);
                            table.Cell().Text(livro.Autor);
                            table.Cell().Text(livro.Assunto);
                            table.Cell().Text(livro.AnoPublicacao.ToString());
                            table.Cell().Text(livro.Valor.ToString("C"));
                        }
                    });
                });
            });
            QuestPDF.Settings.License = LicenseType.Community;
            byte[] pdfBytes = pdfDocument.GeneratePdf();
            return File(pdfBytes, "application/pdf", "RelatorioLivros.pdf");
        }
    }
}