using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models.Autuacao;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Models.Despacho
{
    public class DespachoGetModel
    {
        public int id { get; set; }
        public string texto { get; set; }
        public string guidOrganizacaoDestino { get; set; }
        public string nomeOrganizacaoDestino { get; set; }
        public string siglaOrganizacaoDestino { get; set; }
        public string guiUnidadeDestino { get; set; }
        public string nomeUnidadeDestino { get; set; }
        public string siglaUnidadeDestino { get; set; }
        public string idUsuarioDespachante { get; set; }
        public string nomeUsuarioDespachante { get; set; }
        public string dataHoraDespacho { get; set; }
        public List<AnexoModel> anexos { get; set; }
        public ProcessoEletronicoModel processo { get; set; }
    }

    public class DespachoTela
    {   
        public ProcessoEletronicoModel processo { get; set; }
        public List<OrganizacaoModel> orgaosDestino { get; set; }
    }

    public class DespachoForm
    {
        [Required]
        [Display(Name = "Texto do Despacho")]
        public string texto { get; set; }

        [Display(Name = "Anexos")]
        public List<HttpPostedFileBase> anexos { get; set; }

        [Required]
        [Display(Name = "Organização Destino")]
        public string guidOrganizacaoDestino { get; set; }

        [Required]
        [Display(Name = "Unidade Destino")]
        public string guidUnidadeDestino { get; set; }
    }
}

