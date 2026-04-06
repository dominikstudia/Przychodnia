using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        
        public string Login {  get; set; }
        public string Email {  get; set; }
        public string Imiona { get; set; }
        public string Nazwisko { get; set; }

        public string Haslo { get; set; }
        public string Pesel { get; set; }
        public bool CzyMezczyzna { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string Telefon { get; set; }

        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string KodPocztowy { get; set; }
        public string NumerPosesji { get; set; }
        public string NumerLokalu { get; set; }

        public bool CzyZarchiwizowany { get; set; }


        ///

        public string PobierzWszystkieDane()
        {
            return $"{Login} {Email} {Imiona} {Nazwisko} {Pesel} {(CzyMezczyzna ? "Męźczyzna" : "Kobieta")} {Telefon} {Miejscowosc} {Ulica} {KodPocztowy} {NumerPosesji} {NumerLokalu} {DataUrodzenia.ToString("dd.MM.yyyy")}".ToLower();
        }

    }
}
