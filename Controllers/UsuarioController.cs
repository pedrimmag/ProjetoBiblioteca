using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult CadastrarUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioeAdmin(this);
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario user)
        {
            user.Senha = Criptografia.TextoCriptografado(user.Senha);
            new UsuarioService().Inserir(user);
            return RedirectToAction("ListaUsuarios");
        }

        public IActionResult ListaUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioeAdmin(this);
            return View(new UsuarioService().Listar());
        }
        public IActionResult EditarUsuario(int id)
        {
            Usuario u = new UsuarioService.Listar(id);
            return View(u);
        }
        public IActionResult EditarUsuario(Usuario userEditado)
        {
            userEditado.Senha = Criptografia.TextoCriptografado(userEditado.Senha);
            new UsuarioService().Editar(userEditado);
            return RedirectToAction("ListaUsuarios");
        }
        public IActionResult ExcluirUsuario(int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioeAdmin(this);
            new UsuarioService().Excluir(id);
            return RedirectToAction("ListaUsuarios");
        }
        [HttpPost]
        public IActionResult Permissao()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Home");

        }
    }
}