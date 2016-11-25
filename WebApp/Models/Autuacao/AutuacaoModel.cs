using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class AutuacaoModel
    {
        public int idAtividade { get; set; }
        public string resumo { get; set; }
        public List<InteressadosPessoaFisica> interessadosPessoaFisica { get; set; }
        public List<InteressadosPessoaJuridica> interessadosPessoaJuridica { get; set; }
        public List<Municipio> municipios { get; set; }
        public List<Anexo> anexos { get; set; }
        public List<int> idSinalizacoes { get; set; }
        public int idOrgaoAutuador { get; set; }
        public string nomeOrgaoAutuador { get; set; }
        public string siglaOrgaoAutuador { get; set; }
        public int idUnidadeAutuadora { get; set; }
        public string nomeUnidadeAutuadora { get; set; }
        public string siglaUnidadeAutuadora { get; set; }
        public string idUsuarioAutuador { get; set; }
        public string nomeUsuarioAutuador { get; set; }
    }

    public class Contato
    {
        public string telefone { get; set; }
        public int idTipoContato { get; set; }
    }

    public class Email
    {
        public string endereco { get; set; }
    }

    public class InteressadosPessoaFisica
    {
        public string nome { get; set; }
        public string cpf { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
    }    

    public class InteressadosPessoaJuridica
    {
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public string sigla { get; set; }
        public string nomeUnidade { get; set; }
        public string siglaUnidade { get; set; }
        public List<Contato> contatos { get; set; }
        public List<Email> emails { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
    }

    public class Municipio
    {
        public string uf { get; set; }
        public string nome { get; set; }
    }

    public class Anexo
    {
        public string nome { get; set; }
        public string conteudo { get; set; }
        public string tipo { get; set; }
    }    
}