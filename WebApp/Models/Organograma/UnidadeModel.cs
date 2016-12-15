using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Organograma
{
    public class UnidadeModel
    {
        public string guid { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public TipounidadeModel tipoUnidade { get; set; }
        public Organizacao organizacao { get; set; }
        public Unidadepai unidadePai { get; set; }
        public Endereco endereco { get; set; }
        public List<ContatoModel> contatos { get; set; }
        public List<EmailModel> emails { get; set; }
        public List<SiteModel> sites { get; set; }
    }
}

public class TipounidadeModel
{
    public int id { get; set; }
    public string descricao { get; set; }
}

public class Organizacao
{
    public string guid { get; set; }
    public string razaoSocial { get; set; }
    public string nomeFantasia { get; set; }
    public string sigla { get; set; }
}

public class Unidadepai
{
    public string guid { get; set; }
    public string nome { get; set; }
    public string sigla { get; set; }
}
