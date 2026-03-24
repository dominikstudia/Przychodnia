using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Przychodnia
{
    internal class BazaDanych
    {
        public static BindingList<Uzytkownik> Uzytkownicy { get; set; } = new BindingList<Uzytkownik>();

        public static void ZaladujBazeDanych()
        {
            // TODO ZAPYTANIE DO BAZY DANYCH

            Uzytkownicy.Add(new Uzytkownik { Id = 1, Login = "jankowalski", Imiona = "Jan", Nazwisko = "Kowalski", Email = "jan.kowalski@przychodnia.pl", Pesel = "80011512378", CzyMezczyzna = true, DataUrodzenia = new DateTime(1980, 1, 15), Telefon = "123456789", Miejscowosc = "Warszawa", Ulica = "Marszałkowska", KodPocztowy = "00-001", NumerPosesji = "15", NumerLokalu = "4A", CzyZarchiwizowany = false });
            Uzytkownicy.Add(new Uzytkownik { Id = 2, Login = "anowak", Imiona = "Anna", Nazwisko = "Nowak", Email = "anna.nowak@przychodnia.pl", Pesel = "92102045621", CzyMezczyzna = false, DataUrodzenia = new DateTime(1992, 10, 20), Telefon = "987654321", Miejscowosc = "Kraków", Ulica = "Marszałkowska", KodPocztowy = "30-021", NumerPosesji = "1A", NumerLokalu = "", CzyZarchiwizowany = false });
            Uzytkownicy.Add(new Uzytkownik { Id = 3, Login = "bewak", Imiona = "Bewa", Nazwisko = "Pieaso", Email = "mibosai-siu@weiaop.soa", Pesel = "00000000000", CzyMezczyzna = false, DataUrodzenia = new DateTime(1990, 3, 3), Telefon = "", Miejscowosc = "", Ulica = "", KodPocztowy = "", NumerPosesji = "", NumerLokalu = "", CzyZarchiwizowany = true });
        }

        public static bool DodajLubZaaktualizujUzytkownika(Uzytkownik uzytkownik)
        {
            // TODO ZAPYTANIE DO BAZY DANYCH
            // JEZELI UZYTKOWNIK ma niepodane id [id == -1] oznacza, ze nowy
            // JEZELI UZYTKOWNIK ma podane id [id >= 0] oznacza, że aktualizacja
            // SPRAWDZANIE CZY MOZNA NA PEWNO DODAC [DUPLIKACJA MAILA, PESELU ETC] - ZWROC FALSE, jezeli blad [i moze wysweitl msgboxa, ze np. "Wykryto duplikacje adresu e-mail, etc."

            if (uzytkownik.Id == -1)
            {
                uzytkownik.Id = Uzytkownicy.Any() ? Uzytkownicy.Max(u => u.Id) + 1 : 1; // tymczasowe, żeby działało, po insercie trzeba pobrać id które nadał w bazie danych. ew. dać to co jest tutaj w parametrach
                Uzytkownicy.Add(uzytkownik);
            }
            return true;
        }

        public static bool ZaarchiwizujUzytkownika(Uzytkownik wybrany)
        {
            wybrany.Imiona = "Zarchiwizowane";
            wybrany.Nazwisko = "Dane";
            wybrany.Email = "brak@danych.pl";
            wybrany.CzyZarchiwizowany = true;
            
            // TODO BAZA DANYCH + KTORE POLA ZAARCHIWIZOWAC I W JAKI SPOSOB?
            return true;
        }
    }
}
