using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Autuacao
{
    public class AnexoModel
    {    
        public string nome { get; set; }
        public string descricao { get; set; }
        public string conteudo { get; set; }
        public string mimeType { get; set; }
        public int idTipoDocumental { get; set; }
    }
}