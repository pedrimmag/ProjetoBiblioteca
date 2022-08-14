using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
        public static bool verificaloginsenha(string Login, string Senha, Controller controller)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                verificaSeUsuarioeAdminExiste();

                senha = criptografia.textocriptografado(senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.usuario.where(u => u.Login == Login && u.Senha == senha);
                List<usuario> ListaUsuarios = UsuarioEncontrado.ToList();
                if(ListaUsuarios.Count == 0)
                {
                    return false;
                }
                else
                {
                    controller.HttpContext.Session.setstring("Login", ListaUsuarios[0].login);
                    controller.HttpContext.Session.setint32("Tipo", ListaUsuarios[0].Tipo);
                    return true;
                }
            }
        }
        public static void verificaSeUsuarioeAdmin(Controller controller)
        {
            if(!(controller.HttpContext.Session.getInt32("Tipo") == usuario.Admin))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static void verificaSeUsuarioeAdminExiste()
        {
             using (BibliotecaContext bc = new BibliotecaContext())
             {
                IQueryable<Usuario> userAdmin = bc.usuarios.where(u => i.Login == "admin");
                if(userAdmin.ToList().Count == 0)
                {
                    Usuario novoAdm = new Usuario();
                    novoAdm.Nome = "Administrador";
                    novoAdm.Login = "admin";
                    novoAdm.Senha = criptografia.textocriptografado("123");
                    novoAdm.Tipo = Usuario.Admin;

                    bc.Usuarios.add(novoAdm);
                    bc.SaveChanges();                 
                                    
                }
             }
        }
    }
}