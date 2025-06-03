using Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Wizyta
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        public string Opis { get; set; }

        [ForeignKey("Zwierze")]
        public int ZwierzeId { get; set; }
        public virtual Zwierze Zwierze { get; set; }

        [ForeignKey("Weterynarz")]
        public int WeterynarzId { get; set; }
        public virtual Weterynarz Weterynarz { get; set; }

        public virtual ICollection<Leczenie> Leczenia { get; set; }
    }
}