using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ProcessoEletronico
{
    public class TipoDocumentalModel
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public int idAtividade { get; set; }
    }
}