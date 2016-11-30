using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Autuacao;

namespace WebApp.Models.ProcessoEletronico
{

    public class ProcessoEletronicoModel
    {
        public int id { get; set; }
        public string resumo { get; set; }
        public int idOrgaoAutuador { get; set; }
        public string nomeOrgaoAutuador { get; set; }
        public string siglaOrgaoAutuador { get; set; }
        public int idUnidadeAutuadora { get; set; }
        public string nomeUnidadeAutuadora { get; set; }
        public string siglaUnidadeAutuadora { get; set; }
        public string idUsuarioAutuador { get; set; }
        public string nomeUsuarioAutuador { get; set; }
        public string dataAutuacao { get; set; }
        public string numero { get; set; }
        public int idOrganizacaoProcesso { get; set; }
        public string[] despachos { get; set; }
        public List<InteressadoPessoaFisica> interessadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridica> interessadosPessoaJuridica { get; set; }
        public List<Municipio> municipiosProcesso { get; set; }
        public List<SinalizacaoModel> sinalizacoes { get; set; }
        public AtividadeModel atividade { get; set; }
    }
}