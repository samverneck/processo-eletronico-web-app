using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Autuacao;

namespace WebApp.Models.Despacho
{
    public class DespachoPostModel
    {
        public int idProcesso { get; set; }
        public string texto { get; set; }
        public List<AnexoAutuacaoModel> anexos { get; set; }
        public string guidOrganizacaoDestino { get; set; }
        public string guidUnidadeDestino { get; set; }
    }    
}


