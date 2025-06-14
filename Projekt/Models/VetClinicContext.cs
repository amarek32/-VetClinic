using System.Data.Entity;

namespace Projekt.Models
{
    public class VetClinicContext : DbContext
    {
        public VetClinicContext() : base("VetClinicConnection")
        {
        }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Zwierze> Zwierzeta { get; set; }
        public DbSet<Weterynarz> Weterynarze { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }
        public DbSet<Leczenie> Leczenia { get; set; }

        object placeHolderVariable;
    }
}