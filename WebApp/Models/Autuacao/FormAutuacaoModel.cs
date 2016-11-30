using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Organograma;

namespace WebApp.Models.Autuacao
{
    public class FormAutuacaoModel
    {
        public int idUsuarioAutuador { get; set; }
        public string nomeUsuarioAutuador { get; set; }
        public int idOrganizacaoPai { get; set; }

        public List<SinalizacaoModel> sinalizacoes {get; set;}
        public List<PlanoClassificacaoModel> planosClassificacao { get; set; }

        public int idOrgaoAutuador { get; set; }
        public string nomeOrgaoAutuador { get; set; }
        public string siglaOrgaoAutuador { get; set; }
        
        public int idUnidadeAutuadora { get; set; }
        public string nomeUnidadeAutuadora { get; set; }
        public string siglaUnidadeAutuadora { get; set; }        
        
        
    }
}