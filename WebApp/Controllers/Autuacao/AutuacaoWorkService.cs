using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;
using WebApp.Models.Organograma;

namespace WebApp.Controllers.Autuacao
{
    public class AutuacaoWorkService: WorkServiceBase
    {
        public List<MunicipioModel> GetMunicipios(string uf)
        {
            List<MunicipioModel> listaMunicipios = new List<MunicipioModel>();

            try
            {                
                var urlMunicipios = ConfigurationManager.AppSettings["OrganogramaAPIBase"];
                listaMunicipios = download_serialized_json_data<List<MunicipioModel>>(urlMunicipios + "municipios?uf=" + uf);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listaMunicipios;
        }

        public List<OrganizacaoModel> GetOrganizacoes(string poder, string esfera, string uf)
        {
            List<OrganizacaoModel> listaOrganizacoes = new List<OrganizacaoModel>();

            try
            {
                var urlOrganizacoes = ConfigurationManager.AppSettings["OrganogramaAPIBase"];
                listaOrganizacoes = download_serialized_json_data<List<OrganizacaoModel>>(urlOrganizacoes + "organizacoes?poder=" + poder+ "&esfera=" + esfera + "&uf=" + uf);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return listaOrganizacoes;
        }

        public OrganizacaoModel GetOrganizacao(int id)
        {
            OrganizacaoModel organizacao = new OrganizacaoModel();

            try
            {
                var urlOrganizacoes = ConfigurationManager.AppSettings["OrganogramaAPIBase"];

                string url = urlOrganizacoes + "organizacoes/" + id;

                organizacao = download_serialized_json_data<OrganizacaoModel>(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return organizacao;
        }

        //public async Task<List<OrganogramaModel.Municipio>> GetMunicipios()
        //{
        //    List<OrganogramaModel.Municipio> listaMunicipios = new List<OrganogramaModel.Municipio>();

        //    try
        //    {
        //        string municipio = null;
        //        var urlMunicipios = ConfigurationManager.AppSettings["OrganogramaAPIBase"];
        //        HttpResponseMessage response = await client.GetAsync(urlMunicipios + "municipios");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            municipio = await response.Content.ReadAsStringAsync();
        //            listaMunicipios = JsonConvert.DeserializeObject<List<OrganogramaModel.Municipio>>(municipio);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    return listaMunicipios;
        //}
    }
}