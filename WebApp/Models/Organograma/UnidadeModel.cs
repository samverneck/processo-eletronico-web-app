using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UnidadeModel
    {
        public Unidade unidade { get; set; }
    }

    public class Unidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public int idOrganizacao { get; set; }
        public int idTipoUnidade { get; set; }
        public int idUnidadePai { get; set; }
    }
}