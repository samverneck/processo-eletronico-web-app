using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class PlanoClassificacaoModel
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string guidOrganizacao { get; set; }
        public bool areaFim { get; set; }
        public string observacao { get; set; }
        public int idOrganizacaoProcesso { get; set; }
    }
}