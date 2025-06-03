using Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Zwierze
    {
        public int Id { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Gatunek { get; set; }

        [Required]
        public string Rasa { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataUrodzenia { get; set; }

        [ForeignKey("Klient")]
        public int KlientId { get; set; }
        public virtual Klient Klient { get; set; }

        public virtual ICollection<Wizyta> Wizyty { get; set; }
    }
}