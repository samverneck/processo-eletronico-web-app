using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class SinalizacaoModel
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string cor { get; set; }
        public int idOrganizacaoProcesso { get; set; }

    }
}