using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class EsferaOrganizacaoModel
    {
        public EsferaOrganizacao esferaOrganizacao { get; set; }
    }

    public class EsferaOrganizacao
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }
}