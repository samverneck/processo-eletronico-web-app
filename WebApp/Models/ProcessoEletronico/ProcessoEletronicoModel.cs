using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Autuacao;
using WebApp.Models.Despacho;
using WebApp.Models.Organograma;

namespace WebApp.Models.ProcessoEletronico
{
    public class ProcessoEletronicoModel
    {
        public int id { get; set; }
        public string resumo { get; set; }
        public string guidOrganizacaoAutuadora { get; set; }
        public string nomeOrganizacaoAutuadora { get; set; }
        public string siglaOrganizacaoAutuadora { get; set; }
        public string guidUnidadeAutuadora { get; set; }
        public string nomeUnidadeAutuadora { get; set; }
        public string siglaUnidadeAutuadora { get; set; }
        public string idUsuarioAutuador { get; set; }
        public string nomeUsuarioAutuador { get; set; }
        public string dataAutuacao { get; set; }
        public string dataUltimoTramite { get; set; }

        public DateTime dataAutuacao_DateTime
        {
            get { return Convert.ToDateTime(this.dataAutuacao); }
        }
        public DateTime dataUltimoTramite_DateTime
        {
            get { return Convert.ToDateTime(this.dataUltimoTramite); }
        }

        public string numero { get; set; }
        public int idOrganizacaoProcesso { get; set; }
        public List<DespachoGetModel> despachos { get; set; }
        public List<InteressadoPessoaFisica> interessadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridica> interessadosPessoaJuridica { get; set; }
        public List<MunicipioModel> municipiosProcesso { get; set; }
        public List<SinalizacaoModel> sinalizacoes { get; set; }
        public AtividadeModel atividade { get; set; }
        public List<AnexoModel> anexos { get; set; }
    }
}