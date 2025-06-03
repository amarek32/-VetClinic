using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Leczenie
    {
        public int Id { get; set; }

        public string Opis { get; set; }

        public string Lek { get; set; }

        [DataType(DataType.Currency)]
        public decimal Koszt { get; set; }

        [ForeignKey("Wizyta")]
        public int WizytaId { get; set; }
        public virtual Wizyta Wizyta { get; set; }
    }
}