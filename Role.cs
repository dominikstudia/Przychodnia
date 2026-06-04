using Przychodnia.modele;
using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia
{
    internal class Role
    {

        public static readonly String ADMINISTRATOR = "administrator";
        public static readonly String RECEPCJONISTA = "recepcjonista";
        public static readonly String LEKARZ = "lekarz";
        public static readonly String PACJENT = "pacjent";

        private static readonly Dictionary<string, int> ROLE = new Dictionary<string, int>();

        public static void ZaladujRole(List<Rola> role)
        {
            ROLE.Clear();
            foreach (Rola rola in role)
            {
                ROLE[rola.Nazwa.ToLower()] = rola.Id;
            }

            //MessageBox.Show(string.Join(",", ROLE.Keys));
        }

        public static bool SprawdzCzyMaRole(Uzytkownik uzytkownik, string nazwaRoli)
        {
            if (uzytkownik == null || string.IsNullOrWhiteSpace(nazwaRoli))
            {
                return false;
            }

            nazwaRoli = nazwaRoli.ToLower();
            return ROLE.ContainsKey(nazwaRoli) && uzytkownik.IdRol.Contains(ROLE[nazwaRoli]);
        }

        public static int ZdobadzIdRoli(string nazwaRoli)
        {
            if (string.IsNullOrWhiteSpace(nazwaRoli) || !ROLE.ContainsKey(nazwaRoli.ToLower()))
            {
                return -1;
            }
            return ROLE[nazwaRoli];
            
        }
    }
}
