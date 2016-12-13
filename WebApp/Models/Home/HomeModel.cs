using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Autorizacao;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Models.Home
{
    public class HomeModel
    {
        public List<ProcessoEletronicoModel> processosPorOrgao { get; set; }
        public List<ProcessoEletronicoModel> processosPorUnidade { get; set; }
        public UsuarioLogado usuario { get; set; }
    }
}