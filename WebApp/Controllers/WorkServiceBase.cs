using Newtonsoft.Json;
using StackExchange.Profiling;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebApp.Controllers
{
    public class WorkServiceBase
    {

        public static string download_data(string url, string token, int i = 0)
        {
            using (MiniProfiler.Current.Step($"url{i}: {url}"))
            {
                using (var client = new HttpClient())
                {
                    //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                    
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var result = client.GetAsync(url).Result;

                    if (result.IsSuccessStatusCode)
                    {                        
                        return result.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        if (i < 5)
                            return download_data(url, token, i++);

                        //throw new Exception("Erro inesperado: " + result.StatusCode);
                        return null;                        
                    }
                }
            }
        }

        public static T download_serialized_json_data<T>(string url, string token) where T : new()
        {
            var data = download_data(url, token);
            if (data != null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return new T();
            }
        }

        public static string JsonPostProcessoEletronico(string objeto, string url, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
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


        public static string PostJson(object jsonString, string urlPost, string token)
        {
            var Json = JsonConvert.SerializeObject(jsonString);
            var httpContent = new StringContent(Json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Do the actual request and await the response
                var httpResponse = client.PostAsync(urlPost, httpContent);


                return httpResponse.Result.ToString();

            }
        }

    }
}
