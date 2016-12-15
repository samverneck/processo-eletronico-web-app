using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class FuncaoModel
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public int idPlanoClassificacao { get; set; }
        public int idFuncaoPai { get; set; }
    }
}