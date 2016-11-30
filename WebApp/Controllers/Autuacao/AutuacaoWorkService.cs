using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;
using WebApp.Models.Autuacao;
using WebApp.Models.Organograma;

namespace WebApp.Controllers.Autuacao
{
    public class AutuacaoWorkService : WorkServiceBase
    {
        public List<MunicipioModel> GetMunicipios(string uf)
        {
            List<MunicipioModel> listaMunicipios = new List<MunicipioModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "municipios?uf=" + uf;
                listaMunicipios = download_serialized_json_data<List<MunicipioModel>>(url);
                return listaMunicipios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaMunicipios;
            }            
        }

        public List<OrganizacaoModel> GetOrganizacoes(string poder, string esfera, string uf)
        {
            List<OrganizacaoModel> listaOrganizacoes = new List<OrganizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes?poder=" + poder + "&esfera=" + esfera + "&uf=" + uf;
                listaOrganizacoes = download_serialized_json_data<List<OrganizacaoModel>>(url);
                return listaOrganizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaOrganizacoes;
            }            
        }

        public List<OrganizacaoModel> GetOrganizacoesOutrosOrgaos()
        {
            List<OrganizacaoModel> listaOrganizacoes = new List<OrganizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes";
                listaOrganizacoes = download_serialized_json_data<List<OrganizacaoModel>>(url);

                //listaOrganizacoes = listaOrganizacoes.Where(a => a.poder.descricao.ToUpper() != "Executivo" && a.esfera.descricao.ToUpper() != "ESTADUAL").OrderBy(a => a.sigla).ToList();
                listaOrganizacoes = listaOrganizacoes.Where(a => a.poder.descricao.ToUpper() != "Executivo" && a.esfera.descricao.ToUpper() != "ESTADUAL" && a.endereco.municipio.uf.ToUpper() != "ES").ToList();
                return listaOrganizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaOrganizacoes;
            }            
        }

        public OrganizacaoModel GetOrganizacao(int id)
        {
            OrganizacaoModel organizacao = new OrganizacaoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + id;
                organizacao = download_serialized_json_data<OrganizacaoModel>(url);
                return organizacao;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return organizacao;
            }            
        }

        public List<UnidadeModel> GetUnidadesPorOrganizacao(int id)
        {
            List<UnidadeModel> unidades = new List<UnidadeModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "unidades?idorganizacao=" + id;
                unidades = download_serialized_json_data<List<UnidadeModel>>(url);
                return unidades;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return unidades;
            }            
        }


        public List<TipoContato> GetTiposContatos()
        {
            List<TipoContato> tiposContatos = new List<TipoContato>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "tipos-contato";
                tiposContatos = download_serialized_json_data<List<TipoContato>>(url);
                return tiposContatos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return tiposContatos;
            }            
        }

        public List<PlanoClassificacaoModel> GetPlanosClassificacao(int id, int idOrganizacao)
        {
            List<PlanoClassificacaoModel> planosClassificacao = new List<PlanoClassificacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + id + "/planos-classificacao/" + idOrganizacao;
                planosClassificacao = download_serialized_json_data<List<PlanoClassificacaoModel>>(url);
                return planosClassificacao;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return planosClassificacao;
            }
        }

        public List<FuncaoModel> GetFuncoes(int id, int idPlanoClassificacao)
        {
            List<FuncaoModel> funcoes = new List<FuncaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + id + "/funcoes/" + idPlanoClassificacao;
                funcoes = download_serialized_json_data<List<FuncaoModel>>(url);
                return funcoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return funcoes;
            }            
        }

        public List<AtividadeModel> GetAtividades(int id, int idFuncao)
        {
            List<AtividadeModel> atividades = new List<AtividadeModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + id + "/atividades/" + idFuncao;
                atividades = download_serialized_json_data<List<AtividadeModel>>(url);
                return atividades;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return atividades;
            }            
        }

        public List<SinalizacaoModel> GetSinalizacoes(int id)
        {
            List<SinalizacaoModel> sinalizacoes = new List<SinalizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + id + "/sinalizacoes";
                sinalizacoes = download_serialized_json_data<List<SinalizacaoModel>>(url);
                return sinalizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return sinalizacoes;
            }            
        }

        public string PostAtuacao(AutuacaoModel autuacao, int id)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + id + "/processos");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(autuacao));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
        }
    }
}