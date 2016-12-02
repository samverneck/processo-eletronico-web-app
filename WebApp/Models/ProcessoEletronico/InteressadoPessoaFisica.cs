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
        public string nomeMunicipio { get; set; }
        public string ufMunicipio { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
    }
}