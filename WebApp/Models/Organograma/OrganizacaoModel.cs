using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Organograma;

namespace WebApp.Models
{

    public class OrganizacaoModel
    {
        public string guid { get; set; }
        public string cnpj { get; set; }
        public string razaoSocial { get; set; }
        public string nomeFantasia { get; set; }
        public string sigla { get; set; }
        public List<ContatoModel> contatos { get; set; }
        public List<EmailModel> emails { get; set; }
        public Endereco endereco { get; set; }
        public Esfera esfera { get; set; }
        public PoderModel poder { get; set; }
        public Organizacaopai organizacaoPai { get; set; }
        public List<SiteModel> sites { get; set; }
        public Tipoorganizacao tipoOrganizacao { get; set; }
    }

    public class Endereco
    {
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public MunicipioModel municipio { get; set; }
    }

    public class Esfera
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

    public class ContatoModel
    {
        public string telefone { get; set; }
        public int idTipoContato { get; set; }
        public string nome { get; set; }
    }

    public class EmailModel
    {
        public string endereco { get; set; }
    }

    public class SiteModel
    {
        public string url { get; set; }
    }

}
