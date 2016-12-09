using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Organograma
{
    public class OrgaoModel
    {    
        public string guid { get; set; }
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string sigla { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public Endereco endereco { get; set; }
        public Esfera esfera { get; set; }
        public Poder poder { get; set; }
        public Organizacaopai organizacaoPai { get; set; }
        public List<Site> sites { get; set; }
        public Tipoorganizacao tipoOrganizacao { get; set; }
    }

    public class Endereco
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public Municipio municipio { get; set; }
    }

    public class Municipio
    {
        public int id { get; set; }
        public int codigoIbge { get; set; }
        public string nome { get; set; }
        public string uf { get; set; }
    }

    public class Esfera
    {
        public string descricao { get; set; }
    }

    public class Poder
    {
        public string descricao { get; set; }
    }

    public class Organizacaopai
    {
        public string guid { get; set; }
        public string razaoSocial { get; set; }
        public string sigla { get; set; }
    }

    public class Tipoorganizacao
    {
        public string descricao { get; set; }
    }

    public class Contato
    {
        public string telefone { get; set; }
        public int idTipoContato { get; set; }
        public string nome { get; set; }
    }

    public class Email
    {
        public string endereco { get; set; }
    }

    public class Site
    {
        public string url { get; set; }
    }

}