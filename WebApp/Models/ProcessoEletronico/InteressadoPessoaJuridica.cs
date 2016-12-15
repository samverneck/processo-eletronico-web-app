using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ProcessoEletronico
{
    public class InteressadoPessoaJuridica
    {    
        public int id { get; set; }
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public string sigla { get; set; }
        public string nomeUnidade { get; set; }
        public string siglaUnidade { get; set; }
        public string guidMunicipio { get; set; }        
        public List<ContatoModel> contatos { get; set; }
        public List<EmailModel> emails { get; set; }
    }

}