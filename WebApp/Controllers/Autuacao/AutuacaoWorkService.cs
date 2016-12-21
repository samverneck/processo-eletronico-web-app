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
using WebApp.Models.ProcessoEletronico;

namespace WebApp.Controllers.Autuacao
{
    public class AutuacaoWorkService : WorkServiceBase
    {
        public List<MunicipioModel> GetMunicipios(string uf, string token)
        {
            List<MunicipioModel> listaMunicipios = new List<MunicipioModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "municipios?uf=" + uf;
                listaMunicipios = download_serialized_json_data<List<MunicipioModel>>(url, token);
                return listaMunicipios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaMunicipios;
            }            
        }

        public List<OrganizacaoModel> GetOrganizacoes(string poder, string esfera, string uf, string token)
        {
            List<OrganizacaoModel> listaOrganizacoes = new List<OrganizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes?poder=" + poder + "&esfera=" + esfera + "&uf=" + uf;
                listaOrganizacoes = download_serialized_json_data<List<OrganizacaoModel>>(url, token);
                return listaOrganizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaOrganizacoes;
            }            
        }

        public List<OrganizacaoModel> GetOrganizacoesOutrosOrgaos(string token)
        {
            List<OrganizacaoModel> listaOrganizacoes = new List<OrganizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes";
                listaOrganizacoes = download_serialized_json_data<List<OrganizacaoModel>>(url, token);

                //listaOrganizacoes = listaOrganizacoes.Where(a => a.poder.descricao.ToUpper() != "Executivo" && a.esfera.descricao.ToUpper() != "ESTADUAL").OrderBy(a => a.sigla).ToList();
                listaOrganizacoes = listaOrganizacoes.Where(a => a.poder.descricao.ToUpper() != "EXECUTIVO" && a.esfera.descricao.ToUpper() != "ESTADUAL" && a.endereco.municipio.uf.ToUpper() != "ES").ToList();
                return listaOrganizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return listaOrganizacoes;
            }            
        }

        public OrganizacaoModel GetPatriarca(string guidOrganizacao, string token)
        {
            OrganizacaoModel patriarca = new OrganizacaoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + guidOrganizacao + "/patriarca";
                patriarca = download_serialized_json_data<OrganizacaoModel>(url, token);
                return patriarca;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return patriarca;
            }            
        }

        public List<OrganizacaoModel> GetOrgaosPorPatriarca(string guidOrganizacao, string token)
        {
            List<OrganizacaoModel> orgaos = new List<OrganizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + guidOrganizacao + "/filhas";
                orgaos = download_serialized_json_data<List<OrganizacaoModel>>(url, token);
                return orgaos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return orgaos;
            }
        }

        public OrganizacaoModel GetOrganizacaoPorGuid(string guidOrganizacao, string token)
        {
            OrganizacaoModel organizacao = new OrganizacaoModel();

            try
            {
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "organizacoes/" + guidOrganizacao;
                organizacao = download_serialized_json_data<OrganizacaoModel>(url, token);
                return organizacao;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return organizacao;
            }
        }


        public List<UnidadeModel> GetUnidadesPorOrganizacao(string guidOrganizacao, string token)
        {
            List<UnidadeModel> unidades = new List<UnidadeModel>();

            try
            {                
                var url = ConfigurationManager.AppSettings["OrganogramaAPIBase"] + "unidades/organizacao/" + guidOrganizacao;
                unidades = download_serialized_json_data<List<UnidadeModel>>(url, token);
                return unidades;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return unidades;
            }            
        }


        public List<TipoContato> GetTiposContatos(string token)
        {
            List<TipoContato> tiposContatos = new List<TipoContato>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "tipos-contato";
                tiposContatos = download_serialized_json_data<List<TipoContato>>(url, token);
                return tiposContatos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return tiposContatos;
            }            
        }

        public List<PlanoClassificacaoModel> GetPlanosClassificacao(string guidOrganizacao, string token)
        {
            List<PlanoClassificacaoModel> planosClassificacao = new List<PlanoClassificacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "planos-classificacao/organizacao/" + guidOrganizacao;
                planosClassificacao = download_serialized_json_data<List<PlanoClassificacaoModel>>(url, token);
                return planosClassificacao;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return planosClassificacao;
            }
        }

        public List<FuncaoModel> GetFuncoes(int idPlanoClassificacao, string token)
        {
            List<FuncaoModel> funcoes = new List<FuncaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "funcoes/plano-classificacao/" + idPlanoClassificacao;
                funcoes = download_serialized_json_data<List<FuncaoModel>>(url, token);
                return funcoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return funcoes;
            }            
        }

        public List<AtividadeModel> GetAtividades(int idFuncao, string token)
        {
            List<AtividadeModel> atividades = new List<AtividadeModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "atividades/funcao/" + idFuncao;
                atividades = download_serialized_json_data<List<AtividadeModel>>(url, token);
                return atividades;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return atividades;
            }            
        }

        public List<SinalizacaoModel> GetSinalizacoes(string guidPatriarca, string token)
        {
            List<SinalizacaoModel> sinalizacoes = new List<SinalizacaoModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "sinalizacoes/organizacao-patriarca/" + guidPatriarca;
                sinalizacoes = download_serialized_json_data<List<SinalizacaoModel>>(url, token);
                return sinalizacoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return sinalizacoes;
            }            
        }

        public List<TipoDocumentalModel> GetTipoDocumental(int idAtividade, string token)
        {
            List<TipoDocumentalModel> tiposDocumentais = new List<TipoDocumentalModel>();

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "tipos-documento/atividade/" + idAtividade;
                tiposDocumentais = download_serialized_json_data<List<TipoDocumentalModel>>(url, token);
                return tiposDocumentais;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return tiposDocumentais;
            }
        }

        public string PostAtuacao(Object objeto, string url, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "organizacoes-processo/" + "/processos");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

            try
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(objeto));
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

        public string PostAutuacao(AutuacaoModel autuacao, string token)
        {

            try
            {
                var url = ConfigurationManager.AppSettings["ProcessoEletronicoAPIBase"] + "processos";
                return PostJson(autuacao, url, token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "0";
            }
        }
    }
}