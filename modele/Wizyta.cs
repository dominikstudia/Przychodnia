using System;

namespace Przychodnia.modele
{
    public class Wizyta
    {
        public int IdWizyty { get; set; }
        public string Status { get; set; } = "Zarejestrowana";

        // Pola odpowiadające kolumnom Symptoms i Recommendations z tabeli VisitResults
        public string Schorzenia { get; set; }
        public string Zalecenia { get; set; }
    }
}