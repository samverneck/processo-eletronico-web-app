using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class PoderModel
    {
        public Poder poder { get; set; }
    }

    public class Poder
    {
        public int id { get; set; }
        public string descricao { get; set; }
    }
}