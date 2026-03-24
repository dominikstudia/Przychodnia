using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;

namespace Przychodnia
{
    public static class RegexPatterny
    {
        public const string MALE_LITERY_I_PODLOGA = @"[^a-z_]";
        public const string LITERY_LICZBY_PODLOGA_KROPKA_MYSLNIK_PODLOGA = @"[^a-zA-Z0-9.@\-_]";
        public const string LICZBY_LITERY_UKOSNIK = @"[^0-9a-zA-Z/]";

        public const string POLSKIE_LITERY = @"[^a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŹŻ]";
        public const string POLSKIE_LITERY_SPACJA_MYSLNIK = @"[^a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŻŹ \-]";
        public const string POLSKIE_LITERY_SPACJA = @"[^a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŻŹ ]";
        public const string POLSKIE_LITERY_MYSLNIK = @"[^a-zA-ZąćęłńóśżźĄĆĘŁŃÓŚŻŹ\-]";

        public const string WALIDATOR_EMAIL = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        private static bool SprawdzCzyUsunac(char klawisz, string pattern)
        {
            return Regex.IsMatch(klawisz.ToString(), pattern);
        }

        public static bool SprawdzCzyMoznaKliknacPrzycisk(string pattern, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return true;
            if (SprawdzCzyUsunac(e.KeyChar, pattern))
            {
                if (char.IsUpper(e.KeyChar) && !SprawdzCzyUsunac(char.ToLower(e.KeyChar), pattern))
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                    return true;
                }
                e.Handled = true;
                SystemSounds.Beep.Play();
                return false;
            }
            return true;
        }
    }
}
