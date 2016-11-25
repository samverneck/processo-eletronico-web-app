using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class TipoContato
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public int quantidadeDigitos { get; set; }
    }
}