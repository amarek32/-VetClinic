using Projekt.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Weterynarz
    {
        public int Id { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        public string Specjalizacja { get; set; }

        public virtual ICollection<Wizyta> Wizyty { get; set; }
    }
}