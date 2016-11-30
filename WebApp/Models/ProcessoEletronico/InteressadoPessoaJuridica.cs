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
        public string nomeMunicipio { get; set; }
        public string ufMunicipio { get; set; }
        public string[] contatos { get; set; }
        public string[] emails { get; set; }
    }

}