using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ProcessoEletronico
{
    public class InteressadoPessoaFisica
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string guidMunicipio { get; set; }        
        public List<ContatoModel> contatos { get; set; }
        public List<EmailModel> emails { get; set; }
    }
}