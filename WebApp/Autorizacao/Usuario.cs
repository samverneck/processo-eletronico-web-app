using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WebApp.Controllers;
using WebApp.Models;
using WebApp.Models.Organograma;

namespace WebApp.Autorizacao
{
    public class UsuarioLogado : ClaimsPrincipal
    {
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

        public string Token
        {
            get { return this.FindFirst("access_token").Value; }
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

        public string SiglaOrganizacao
        {
            get
            {
                if (this.HasClaim(a => a.Type == "orgao"))
                {
                    return this.FindFirst("orgao").Value;
                }
                else
                {
                    throw new Exception($"Usuário {Nome} sem Organização.");
                }
            }
        }

        public OrganizacaoModel Orgao
        {
            get
            {
                if (this.HasClaim(a => a.Type == "organizacao"))
                {
                    return JsonConvert.DeserializeObject<OrganizacaoModel>(this.FindFirst("organizacao").Value);
                }
                else
                {
                    throw new Exception($"Usuário {Nome} sem Organização.");
                }
            }
        }

        public OrganizacaoModel Patriarca
        {
            get
            {
                if (this.HasClaim(a => a.Type == "organizacao_patriarca"))
                {
                    return JsonConvert.DeserializeObject<OrganizacaoModel>(this.FindFirst("organizacao_patriarca").Value);
                }
                else
                {
                    throw new Exception($"Usuário {Nome} sem Organização Patriarca.");
                }
            }
        }

    }
}