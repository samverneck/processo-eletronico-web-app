using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApp.Autorizacao;

namespace WebApp.Controllers
{
    public class WorkServiceBase
    {
        public static UsuarioLogado GetusuarioLogado()
        {
            UsuarioLogado usuario = new UsuarioLogado();
            return usuario;
        }

        public static T download_serialized_json_data<T>(string url) where T : new()
        {
            using (var client = new HttpClient())
            {
                //TODO: Está pulando a validação de certificado por causa do certificado inválido de DES e HMG
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;                

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("");

                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return new T();
                }
            }

            //using (var w = new WebClient())
            //{
            //    var json_data = string.Empty;
            //    // attempt to download JSON data as a string
            //    try
            //    {
            //        json_data = w.DownloadString(url);

            //        throw new Exception(json_data);

            //        byte[] bytes = Encoding.Default.GetBytes(json_data);
            //        json_data = Encoding.UTF8.GetString(bytes);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }

            //    // if string with JSON data is not empty, deserialize it to class and return its instance 
            //    return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            //}
        }

    }
}