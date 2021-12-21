using AspNetMVCeEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetMVCeEF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        //public IActionResult Pessoa()
        //{
        //    Pessoa p = new Pessoa();
        //    p.Nome = "Fabrício Tonetto Londero";
        //    return View(p);
        //}

        public IActionResult Pessoa(Pessoa p)
        {
            return View(p);
        }

        public IActionResult PessoaId(int id)
        {
            Contexto contexto = new Contexto();

            Pessoa? p = contexto.Pessoas.Find(id);

            return View(p);
        }

        public IActionResult Pessoas()
        {
            Contexto contexto = new Contexto();

            List<Pessoa> pessoas = (from Pessoa p in contexto.Pessoas
                                    select p).Include(e => e.Emails).ToList<Pessoa>();
            return View(pessoas);
        }

        [HttpPost]
        public IActionResult Cadastrar(Pessoa p)
        {
            Contexto contexto = new Contexto();

            contexto.Pessoas.Add(p);
            contexto.SaveChanges();

            //--
            //int pessoaId = p.Id;
            //List<Email> emails = (from Email e in contexto.Emails
            //                      select e).Include(e => e.pessoa).Where(e => e.pessoa.Id == pessoaId).ToList<Email>();

            //ViewData["Emails"] = emails;

            //ViewBag.pessoaId = p.Id;

            return RedirectToAction("CadastroEmail", new { id = p.Id });
            //return RedirectToAction("CadastroEmail", "Home", p.Id);
        }

        [HttpGet]
        public IActionResult CadastroEmail(int id)
        {
            //int pessoaId = ViewBag.pessoaId;
            Contexto contexto = new Contexto();
            List<Email> emails = (from Email p in contexto.Emails
                                  select p).Include(e => e.pessoa).Where(p => p.pessoa.Id == id).ToList<Email>();
            
            ViewData["Emails"] = emails;
            ViewData["idPessoa"] = id;

            return View();
        }

        //public IActionResult CadastroEmail(Email email, int? id)
        [HttpPost]
        public IActionResult CadastroEmail(string emailTexto, int? id)
        {
            Email email = new Email();
            email.email = emailTexto;

            Contexto contexto = new Contexto();

            Pessoa? pessoa = new Pessoa();
            pessoa = (from Pessoa p in contexto.Pessoas
                 select p).Include(e => e.Emails).Where(p => p.Id == id).FirstOrDefault<Pessoa>();

            email.pessoa = pessoa;

            contexto.Emails.Add(email);
            contexto.SaveChanges();

            return RedirectToAction("PessoaId", new { id = id });
        }

        //[HttpPost]
        //public IActionResult Cadastrar(Pessoa p)
        //{
        //    Contexto contexto = new Contexto();

        //    contexto.Pessoas.Add(p);
        //    contexto.SaveChanges();

        //    return View("Pessoa", p);
        //}

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
