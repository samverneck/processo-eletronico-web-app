using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Models.Home
{
    public class HomeModel
    {
        public List<ProcessoEletronicoModel> processosPorUnidade { get; set; }
    }
}