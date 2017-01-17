using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebApp.Models.Mensagem;

namespace WebApp.Models
{
    public class ListaMensagens
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<Mensagem> Mensagens
        {
            get
            {
                if (HttpContext.Current.Session["ListaMensagens"] == null)
                {
                    HttpContext.Current.Session["ListaMensagens"] = new List<Mensagem>();
                }

                return HttpContext.Current.Session["ListaMensagens"] as List<Mensagem>;
            }
            private set
            {
                HttpContext.Current.Session["ListaMensagens"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="texto"></param>
        /// <param name="titulo"></param>
        public static void AdicionarMensagem(TipoMensagem tipo, string texto, string titulo = null)
        {
            Mensagens.Add(new Mensagem() { Tipo = tipo, Titulo = titulo, Texto = texto });
        }

        public static void Limpar()
        {
            Mensagens = new List<Mensagem>();
        }
    }
}