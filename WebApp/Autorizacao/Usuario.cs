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

            //Set Órgao

            OrgaoModel _orgao = new OrgaoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + this.SiglaOrganizacao;
                _orgao = WorkServiceBase.download_serialized_json_data<OrgaoModel>(url, this.Token);
                Orgao = _orgao;
            }
            catch (Exception e)
            {
                Orgao = _orgao;
            }


            //Set Patriarca

            OrgaoModel _patriarca = new OrgaoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + this.Orgao.guid + "/patriarca";
                _patriarca = WorkServiceBase.download_serialized_json_data<OrgaoModel>(url, this.Token);
                Patriarca = _patriarca;
            }
            catch (Exception e)
            {
                Patriarca = _patriarca;
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

        public OrgaoModel Orgao;        

        public OrgaoModel Patriarca;        

    }
}