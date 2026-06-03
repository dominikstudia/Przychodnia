using System;
using System.Text.RegularExpressions;

namespace Przychodnia
{
    public static class Narzedzia
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


        // Metody nie zostały napisane przeze mnie [Dominik] - zostały przeniesione z klasy BazaDanych.cs ponieważ nie tam jest ich miejsce
        public static string GenerujSilneHaslo()
        {
            const string male = "abcdefghijklmnopqrstuvwxyz";
            const string duze = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string cyfry = "0123456789";
            const string znaki = "!@#$%^&*()_+-=[]{};:,.<>?";

            Random rnd = new Random();
            char[] haslo = new char[10];

            for (int i = 0; i < 3; i++) haslo[i] = male[rnd.Next(male.Length)];
            for (int i = 3; i < 6; i++) haslo[i] = duze[rnd.Next(duze.Length)];
            for (int i = 6; i < 8; i++) haslo[i] = cyfry[rnd.Next(cyfry.Length)];
            for (int i = 8; i < 10; i++) haslo[i] = znaki[rnd.Next(znaki.Length)];

            // Składamy hasło w całość
            string gotoweHaslo = new string(haslo.OrderBy(x => rnd.Next()).ToArray());

            // Automatyczne kopiowanie do schowka z zabezpieczeniem
            try
            {
                System.Windows.Forms.Clipboard.SetText(gotoweHaslo);
            }
            catch (Exception ex)
            {
                // Wyświetlamy ostrzeżenie, jeśli Windows zablokuje dostęp do schowka
                System.Windows.Forms.MessageBox.Show(
                    "Hasło zostało wygenerowane, ale system zablokował automatyczne skopiowanie go do schowka.\nMusisz skopiować je ręcznie.\n\nSzczegóły: " + ex.Message,
                    "Błąd schowka",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
            }

            return gotoweHaslo;
        }

        public static string HashujHaslo(string haslo)
        {
            if (string.IsNullOrEmpty(haslo)) return "";

            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(haslo));
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static (bool CzySaBledy, string Komunikat) SprawdzSileHasla(string haslo, string login = "")
        {
            if (string.IsNullOrEmpty(haslo)) return (true, "Hasło nie może być puste.");

            List<string> bledy = new List<string>();

            if (haslo.Length < 8) bledy.Add("- jest za krótkie (minimum 8 znaków)");
            if (haslo.Length > 15) bledy.Add("- jest za długie (maksymalnie 15 znaków)");
            if (!string.IsNullOrEmpty(login) && haslo.Equals(login, StringComparison.OrdinalIgnoreCase))
                bledy.Add("- nie może być takie samo jak login użytkownika");
            if (!haslo.Any(char.IsUpper)) bledy.Add("- nie zawiera wielkiej litery");
            if (!haslo.Any(char.IsLower)) bledy.Add("- nie zawiera małej litery");
            if (!haslo.Any(char.IsDigit)) bledy.Add("- nie zawiera cyfry");
            if (!haslo.Any(ch => !char.IsLetterOrDigit(ch))) bledy.Add("- nie zawiera znaku specjalnego");

            if (bledy.Count > 0)
            {
                string komunikat = "Hasło nie spełnia wymagań bezpieczeństwa, ponieważ:\n\n" + string.Join("\n", bledy);
                return (true, komunikat);
            }

            return (false, "");
        }
    }
}