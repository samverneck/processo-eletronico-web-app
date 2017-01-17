using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Mensagem
    {
        public TipoMensagem Tipo { get; set; }

        private string titulo;
        public string Titulo
        {
            get
            {
                if (titulo == null)
                {
                    switch (Tipo)
                    {
                        case TipoMensagem.Sucesso:
                            titulo = "Sucesso";
                            break;
                        case TipoMensagem.Informacao:
                            titulo = "Informação";
                            break;
                        case TipoMensagem.Atencao:
                            titulo = "Atenção";
                            break;
                        case TipoMensagem.Erro:
                            titulo = "Erro";
                            break;
                    }
                }

                return titulo;
            }
            set
            {
                titulo = value;
            }
        }

        public string Texto { get; set; }

        public string TipoToastr
        {
            get
            {
                switch (Tipo)
                {
                    case TipoMensagem.Sucesso:
                        return "success";
                    case TipoMensagem.Informacao:
                        return "info";
                    case TipoMensagem.Atencao:
                        return "warning";
                    case TipoMensagem.Erro:
                        return "error";
                    default:
                        return "info";
                }
            }
        }        
    }
}