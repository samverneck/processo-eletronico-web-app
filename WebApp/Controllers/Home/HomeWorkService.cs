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
        public List<ProcessoEletronicoModel> GetProcessosPorOrganizacaoPorUnidade(int idOrganizacao, int idUnidade)
        {
            List<ProcessoEletronicoModel> listaProcessos = new List<ProcessoEletronicoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/"+ idOrganizacao + "/processos/unidade/" + idUnidade;
                listaProcessos = download_serialized_json_data<List<ProcessoEletronicoModel>>(url);

                return listaProcessos.OrderByDescending(a => a.dataAutuacao).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaProcessos;
            }
        }

        public ProcessoEletronicoModel GetProcessosPorOrganizacaoPorProcesso(int idOrganizacao, int idProcesso)
        {
            ProcessoEletronicoModel processos = new ProcessoEletronicoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + idOrganizacao + "/processos/" + idProcesso;
                processos = download_serialized_json_data<ProcessoEletronicoModel>(url);
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