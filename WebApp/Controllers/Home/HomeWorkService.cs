using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Controllers.Home
{
    public class HomeWorkService : WorkServiceBase
    {
        public List<ProcessoEletronicoModel> GetProcessosPorOrganizacaoPorOrgao(string guidOrganizacao, string token)
        {
            List<ProcessoEletronicoModel> listaProcessos = new List<ProcessoEletronicoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "processos/organizacao/" + guidOrganizacao;
                listaProcessos = download_serialized_json_data<List<ProcessoEletronicoModel>>(url, token);

                return listaProcessos.OrderByDescending(a=>a.dataUltimoTramite_DateTime).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaProcessos;
            }
        }

        public ProcessoEletronicoModel GetProcessosPorOrganizacaoPorProcesso(string numeroProcesso, string token)
        {
            ProcessoEletronicoModel processos = new ProcessoEletronicoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "processos/numero/" + numeroProcesso;
                processos = download_serialized_json_data<ProcessoEletronicoModel>(url, token);
                return processos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return processos;
            }
        }
    }
}