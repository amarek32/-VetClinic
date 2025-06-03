using Projekt.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Projekt.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Telefon { get; set; }

        public virtual ICollection<Zwierze> Zwierzeta { get; set; }
    }
}