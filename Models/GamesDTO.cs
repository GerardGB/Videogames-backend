using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Videogames_backend.Models
{
    public class GamesDTO
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Gènere { get; set; }
        public System.DateTime Release { get; set; }
        public DesarrolladoraDTO Desarrolladora { get; set; }
        public Nullable<int> PEGI { get; set; }
        public Nullable<bool> PS4 { get; set; }
        public Nullable<bool> NSwitch { get; set; }
        public Nullable<bool> XboxOne { get; set; }
        public Nullable<bool> PC { get; set; }
    }
}