using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace WebApp.Autorizacao
{
    public class UsuarioLogado : ClaimsPrincipal    {
        public UsuarioLogado()
        {
            if (HttpContext.Current.User != null)
            {
                var identity = HttpContext.Current.User.Identity as ClaimsIdentity;

                if (identity == null)
                    throw new Exception("Não foi encontrada uma Identity para o usuário autenticado.");

                this.AddIdentity(identity);
            }
        }

        public bool Autenticado
        {
            get { return this.Identity.IsAuthenticated; }
        }

        public string Id
        {
            get { return this.FindFirst("sid").Value; }
        }

        public string Nome
        {
            get { return this.FindFirst("nome").Value; }
        }

        public string CPF
        {
            get { return this.FindFirst("cpf").Value; }
        }        

        public string Email
        {
            get { return this.FindFirst("email").Value; }
        }


    }
}