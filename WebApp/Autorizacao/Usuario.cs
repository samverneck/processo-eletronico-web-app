using Microsoft.IdentityModel.Protocols;
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
                if (this.HasClaim(a => a.Type == "orgao")){
                    return this.FindFirst("orgao").Value;
                }else
                {
                    return "";
                }
            }
        }

        public OrganizacaoModel Orgao
        {
            get
            {
                if (HttpContext.Current.Session["OrgaoUsuario"] == null)
                {
                    try
                    {
                        var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/sigla/" + this.SiglaOrganizacao;
                        HttpContext.Current.Session["OrgaoUsuario"] = WorkServiceBase.download_serialized_json_data<OrganizacaoModel>(url, this.Token);
                    }
                    catch (Exception e)
                    {
                        HttpContext.Current.Session["OrgaoUsuario"] = null;
                    }
                }

                return (OrganizacaoModel)HttpContext.Current.Session["OrgaoUsuario"];
            }
        }

        private OrganizacaoModel _patriarca;
        public OrganizacaoModel Patriarca
        {
            get
            {
                if (HttpContext.Current.Session["OrgaoPatriarcaUsuario"] == null)
                {
                    try
                    {
                        var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + this.Orgao.guid + "/patriarca";
                        HttpContext.Current.Session["OrgaoPatriarcaUsuario"] = WorkServiceBase.download_serialized_json_data<OrganizacaoModel>(url, this.Token);
                    }
                    catch (Exception e)
                    {
                        HttpContext.Current.Session["OrgaoPatriarcaUsuario"] = null;
                    }
                }

                return (OrganizacaoModel)HttpContext.Current.Session["OrgaoPatriarcaUsuario"];
            }
        }

    }
}