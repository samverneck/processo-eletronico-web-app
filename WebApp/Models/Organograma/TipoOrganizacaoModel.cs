using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TipoOrganizacaoModel
    {
        public TipoOrganizacao tipoOrganizacao { get; set; }
    }

    public class TipoOrganizacao
    {
        public string inicioVigencia { get; set; }
        public int id { get; set; }
        public string descricao { get; set; }
    }
}