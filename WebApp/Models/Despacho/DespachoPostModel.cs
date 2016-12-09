using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Autuacao;

namespace WebApp.Models.Despacho
{
    public class DespachoPostModel    
    {
        public string texto { get; set; }
        public List<AnexoModel> anexos { get; set; }
        public int idOrgaoDestino { get; set; }
        public string nomeOrgaoDestino { get; set; }
        public string siglaOrgaoDestino { get; set; }
        public int idUnidadeDestino { get; set; }
        public string nomeUnidadeDestino { get; set; }
        public string siglaUnidadeDestino { get; set; }
        public string idUsuarioDespachante { get; set; }
        public string nomeUsuarioDespachante { get; set; }
    }   
}


