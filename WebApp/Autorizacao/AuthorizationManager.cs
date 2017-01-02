using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace WebApp.Autorizacao
{
    public class AuthorizationManager : ResourceAuthorizationManager
    {
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            var recurso = context.Resource.First().Value;
            var acao = context.Action.First().Value;

            var claims = context.Principal.Claims;
            var claimsRecursos = claims.FirstOrDefault(x => x.Type == "Recurso" && x.Value == recurso);
            if (claimsRecursos != null)
            {
                var claimsAcao = claims.FirstOrDefault(x => x.Type == "Acao$" + recurso && x.Value == acao);
                if (claimsAcao != null)
                {
                    return Ok();
                }
            }
            return Nok();
        }
    }
}