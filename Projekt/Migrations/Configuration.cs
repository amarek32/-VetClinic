namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Projekt.Models; // Dodaj to using, aby mieć dostęp do modeli

    internal sealed class Configuration : DbMigrationsConfiguration<Projekt.Models.VetClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Projekt.Models.VetClinicContext context)
        {
            // Dodawanie przykładowych klientów
            context.Klienci.AddOrUpdate(
                k => k.Email, // Klucz do identyfikacji unikalnych klientów (może być Imie i Nazwisko, ale email jest zazwyczaj lepszy)
                new Klient { Imie = "Anna", Nazwisko = "Kowalska", Email = "anna.kowalska@example.com", Telefon = "123456789" },
                new Klient { Imie = "Jan", Nazwisko = "Nowak", Email = "jan.nowak@example.com", Telefon = "987654321" },
                new Klient { Imie = "Ewa", Nazwisko = "Lis", Email = "ewa.lis@example.com", Telefon = "555111222" }
            );
            context.SaveChanges(); // Zapisz zmiany, aby Id klientów były dostępne dla zwierząt

            // Dodawanie przykładowych weterynarzy
            context.Weterynarze.AddOrUpdate(
                w => w.Nazwisko, // Klucz do identyfikacji unikalnych weterynarzy
                new Weterynarz { Imie = "Piotr", Nazwisko = "Mazurek", Specjalizacja = "Chirurg" },
                new Weterynarz { Imie = "Maria", Nazwisko = "Wójcik", Specjalizacja = "Internista" }
            );
            context.SaveChanges(); // Zapisz zmiany, aby Id weterynarzy były dostępne dla wizyt

            // Pobierz dodanych klientów i weterynarzy, aby przypisać ich do zwierząt i wizyt
            var annaKowalska = context.Klienci.Single(k => k.Email == "anna.kowalska@example.com");
            var janNowak = context.Klienci.Single(k => k.Email == "jan.nowak@example.com");
            var piotrMazurek = context.Weterynarze.Single(w => w.Nazwisko == "Mazurek");
            var mariaWojcik = context.Weterynarze.Single(w => w.Nazwisko == "Wójcik");

            // Dodawanie przykładowych zwierząt
            context.Zwierzeta.AddOrUpdate(
                z => new { z.Imie, z.KlientId }, // Klucz do identyfikacji unikalnych zwierząt (imię + właściciel)
                new Zwierze { Imie = "Burek", Gatunek = "Pies", Rasa = "Owczarek Niemiecki", DataUrodzenia = new DateTime(2020, 5, 10), KlientId = annaKowalska.Id },
                new Zwierze { Imie = "Mruczek", Gatunek = "Kot", Rasa = "Dachowiec", DataUrodzenia = new DateTime(2022, 1, 15), KlientId = annaKowalska.Id },
                new Zwierze { Imie = "Azor", Gatunek = "Pies", Rasa = "Labrador", DataUrodzenia = new DateTime(2019, 8, 20), KlientId = janNowak.Id }
            );
            context.SaveChanges(); // Zapisz zmiany, aby Id zwierząt były dostępne dla wizyt

            // Pobierz dodane zwierzęta
            var burek = context.Zwierzeta.Single(z => z.Imie == "Burek" && z.KlientId == annaKowalska.Id);
            var mruczek = context.Zwierzeta.Single(z => z.Imie == "Mruczek" && z.KlientId == annaKowalska.Id);
            var azor = context.Zwierzeta.Single(z => z.Imie == "Azor" && z.KlientId == janNowak.Id);

            // Dodawanie przykładowych wizyt
            context.Wizyty.AddOrUpdate(
                w => new { w.Data, w.ZwierzeId, w.WeterynarzId }, // Klucz do identyfikacji unikalnych wizyt
                new Wizyta { Data = new DateTime(2024, 6, 1, 10, 0, 0), Opis = "Szczepienie przeciw wściekliźnie", ZwierzeId = burek.Id, WeterynarzId = piotrMazurek.Id },
                new Wizyta { Data = new DateTime(2024, 6, 5, 14, 30, 0), Opis = "Kontrola roczna", ZwierzeId = mruczek.Id, WeterynarzId = mariaWojcik.Id },
                new Wizyta { Data = new DateTime(2024, 5, 20, 11, 0, 0), Opis = "Badanie krwi", ZwierzeId = azor.Id, WeterynarzId = piotrMazurek.Id }
            );
            context.SaveChanges(); // Zapisz zmiany, aby Id wizyt były dostępne dla leczeń

            // Pobierz dodane wizyty
            var wizytaBurek1 = context.Wizyty.Single(w => w.Data == new DateTime(2024, 6, 1, 10, 0, 0) && w.ZwierzeId == burek.Id);
            var wizytaMruczek1 = context.Wizyty.Single(w => w.Data == new DateTime(2024, 6, 5, 14, 30, 0) && w.ZwierzeId == mruczek.Id);
            var wizytaAzor1 = context.Wizyty.Single(w => w.Data == new DateTime(2024, 5, 20, 11, 0, 0) && w.ZwierzeId == azor.Id);


            // Dodawanie przykładowych leczeń
            context.Leczenia.AddOrUpdate(
                l => new { l.WizytaId, l.Opis }, // Klucz do identyfikacji unikalnych leczeń
                new Leczenie { Opis = "Podanie szczepionki", Lek = "Rabies vaccine", Koszt = 50.00m, WizytaId = wizytaBurek1.Id },
                new Leczenie { Opis = "Ogólne badanie, kontrola wagi", Lek = "Brak", Koszt = 70.00m, WizytaId = wizytaMruczek1.Id },
                new Leczenie { Opis = "Pobranie próbki krwi", Lek = "Brak", Koszt = 80.00m, WizytaId = wizytaAzor1.Id },
                new Leczenie { Opis = "Analiza laboratoryjna", Lek = "Brak", Koszt = 120.00m, WizytaId = wizytaAzor1.Id }
            );
            context.SaveChanges();
        }
    }
}
