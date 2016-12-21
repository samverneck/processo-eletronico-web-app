using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Organograma;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Models.Autuacao
{
    public class AutuacaoModel
    {
        public int idAtividade { get; set; }
        public string resumo { get; set; }
        public List<InteressadoPessoaFisica> interessadosPessoaFisica { get; set; }
        public List<InteressadoPessoaJuridica> interessadosPessoaJuridica { get; set; }
        public List<MunicipioAutuacao> municipios { get; set; }
        public List<AnexoAutuacaoModel> anexos { get; set; }
        public List<int> idSinalizacoes { get; set; }
        public string guidOrganizacaoAutuadora { get; set; }
        public string guidUnidadeAutuadora { get; set; }
    }    

    public class MunicipioAutuacao
    {
        public string guidMunicipio { get; set; }
    }

    public class AnexoAutuacaoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string mimeType { get; set; }
        public string conteudo { get; set; }     
        public Nullable<int> idTipoDocumental { get; set; }
    }
}