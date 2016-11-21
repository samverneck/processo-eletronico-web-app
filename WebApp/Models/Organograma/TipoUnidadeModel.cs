using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TipoUnidadeModel
    {
        public TipoUnidade tipoUnidade { get; set; }
    }

    public class TipoUnidade
    {
        public string inicioVigencia { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
    }
}