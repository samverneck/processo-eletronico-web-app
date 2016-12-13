using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class AtividadeModel
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public FuncaoModel funcao { get; set; }
    }    
}