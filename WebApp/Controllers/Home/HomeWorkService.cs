﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Models.Autuacao;
using WebApp.Models.Despacho;
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Controllers.Home
{
    public class HomeWorkService : WorkServiceBase
    {
        public List<ProcessoEletronicoModel> GetProcessosPorOrgao(string guidOrganizacao, string token)
        {
            List<ProcessoEletronicoModel> listaProcessos = new List<ProcessoEletronicoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "processos/organizacao/" + guidOrganizacao;
                listaProcessos = download_serialized_json_data<List<ProcessoEletronicoModel>>(url, token);

                return listaProcessos.OrderByDescending(a => a.dataUltimoTramite_DateTime).ToList();
            }
            catch (Exception e)
            {
                return listaProcessos;
            }
        }

        public RetornoAjaxModel GetProcessoPorNumero(string numeroProcesso, string token)
        {
            var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "processos/numero/" + numeroProcesso;
            var retorno = Get(url, token);
            return retorno;
        }

        public RetornoAjaxModel GetDespachoPorId(int id, string token)
        {
            var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "despachos/" + id;
            return Get(url, token);
        }

        public AnexoModel GetAnexo(int idAnexo, string token)
        {
            AnexoModel anexo = new AnexoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "anexos/" + idAnexo;
                anexo = download_serialized_json_data<AnexoModel>(url, token);
                return anexo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return anexo;
            }
        }

        public RetornoAjaxModel PostDespacho(DespachoPostModel despacho, string token)
        {
            var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "despachos";
            //return PostJson(despacho, url, token);
            return Post(despacho, url, token);
        }
    }
}