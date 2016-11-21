using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Organograma;

namespace WebApp.Models.Autuacao
{
    public class FormAutuacaoModel
    {
        public List<MunicipioModel> municipios {get; set;}
        public List<OrganizacaoModel> organizacoes { get; set; }
    }
}