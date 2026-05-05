using System;
using System.Text.RegularExpressions;

namespace Przychodnia
{
    public static class Walidator
    {
        public static (bool Poprawny, string Komunikat) SprawdzPesel(string pesel, DateTime dataUrodzenia, bool czyMezczyzna)
        {
            string blad = "Numer PESEL jest nieprawidłowy lub niezgodny z danymi";

            if (string.IsNullOrWhiteSpace(pesel) || pesel.Length != 11 || !Regex.IsMatch(pesel, @"^\d{11}$")) return (false, blad);

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;
            for (int i = 0; i < 10; i++) suma += int.Parse(pesel[i].ToString()) * wagi[i];

            if ((10 - (suma % 10)) % 10 != int.Parse(pesel[10].ToString())) return (false, blad);

            if ((int.Parse(pesel[9].ToString()) % 2 != 0) != czyMezczyzna) return (false, blad);

            int rok = int.Parse(pesel.Substring(0, 2)), miesiac = int.Parse(pesel.Substring(2, 2)), dzien = int.Parse(pesel.Substring(4, 2));
            int pelnyRok = 0;

            if (miesiac >= 1 && miesiac <= 12) { pelnyRok = 1900 + rok; }
            else if (miesiac >= 21 && miesiac <= 32) { pelnyRok = 2000 + rok; miesiac -= 20; }
            else if (miesiac >= 41 && miesiac <= 52) { pelnyRok = 2100 + rok; miesiac -= 40; }
            else return (false, blad);

            if (dataUrodzenia.Year != pelnyRok || dataUrodzenia.Month != miesiac || dataUrodzenia.Day != dzien) return (false, blad);

            return (true, "");
        }

        public static (bool Poprawny, string Komunikat) SprawdzEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return (true, "");
            if (email.Length > 25 || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return (false, "Nieprawidłowy format adresu e-mail");
            return (true, "");
        }

        public static (bool Poprawny, string Komunikat) SprawdzTelefon(string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon)) return (false, "Pole jest wymagane");
            if (telefon.Length != 9 || !Regex.IsMatch(telefon, @"^\d{9}$"))
                return (false, "Nieprawidłowy numer telefonu ");
            return (true, "");
        }
    }
}