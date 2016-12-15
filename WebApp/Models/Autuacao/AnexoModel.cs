using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Models.Autuacao
{
    public class AnexoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string mimeType { get; set; }
        public string conteudo { get; set; }
        public ProcessoEletronicoModel processo { get; set; }
        public TipoDocumentalModel tipoDocumental { get; set; }
    }    
}