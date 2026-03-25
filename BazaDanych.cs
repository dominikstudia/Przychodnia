using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Przychodnia
{
    // TODO CALA KLASA DO PRZEBUDOWY POD FINALNA BAZE DANYCH
    internal class BazaDanych
    {
        public static readonly string POLACZENIE_STRING = "Data Source=przychodnia.db";
        public static BindingList<Uzytkownik> Uzytkownicy { get; set; } = new BindingList<Uzytkownik>();

        public static void ZaladujBazeDanych()
        {
            StworzTabele();

            using (var polaczenie = new SqliteConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();
                string kwerenda = "SELECT * FROM Uzytkownicy";

                using var komenda = new SqliteCommand(kwerenda, polaczenie);
                using var czytnik = komenda.ExecuteReader();
                while (czytnik.Read())
                {
                    Uzytkownicy.Add(new Uzytkownik { Id = Convert.ToInt32(czytnik["Id"]), Login = czytnik["Login"].ToString(), Imiona = czytnik["Imiona"].ToString(), Nazwisko = czytnik["Nazwisko"].ToString(), Email = czytnik["Email"].ToString(), Pesel = czytnik["Pesel"].ToString(), CzyMezczyzna = Convert.ToBoolean(czytnik["CzyMezczyzna"]), DataUrodzenia = DateTime.Parse(czytnik["DataUrodzenia"].ToString()), Telefon = czytnik["Telefon"].ToString(), Miejscowosc = czytnik["Miejscowosc"].ToString(), Ulica = czytnik["Ulica"].ToString(), KodPocztowy = czytnik["KodPocztowy"].ToString(), NumerPosesji = czytnik["NumerPosesji"].ToString(), NumerLokalu = czytnik["NumerLokalu"].ToString(), CzyZarchiwizowany = Convert.ToBoolean(czytnik["CzyZarchiwizowany"]) });
                }
            }

            // TODO DODAWANIE PRZYKŁADOWYCH DANYCH GDY NIC NIE MA W SYSTEMIE [GDY BEDZIE PRODUKCJA - USUNAC]
            if (Uzytkownicy.Count == 0) 
            {
                DodajLubZaaktualizujUzytkownika(new Uzytkownik { Id = -1, Login = "jankowalski", Imiona = "Jan", Nazwisko = "Kowalski", Email = "jan.kowalski@przychodnia.pl", Pesel = "80011512378", CzyMezczyzna = true, DataUrodzenia = new DateTime(1980, 1, 15), Telefon = "123456789", Miejscowosc = "Warszawa", Ulica = "Marszałkowska", KodPocztowy = "00-001", NumerPosesji = "15", NumerLokalu = "4A", CzyZarchiwizowany = false });
                DodajLubZaaktualizujUzytkownika(new Uzytkownik { Id = -1, Login = "anowak", Imiona = "Anna", Nazwisko = "Nowak", Email = "anna.nowak@przychodnia.pl", Pesel = "92102045621", CzyMezczyzna = false, DataUrodzenia = new DateTime(1992, 10, 20), Telefon = "987654321", Miejscowosc = "Kraków", Ulica = "Marszałkowska", KodPocztowy = "30-021", NumerPosesji = "1A", NumerLokalu = "", CzyZarchiwizowany = false });
                DodajLubZaaktualizujUzytkownika(new Uzytkownik { Id = -1, Login = "bewak", Imiona = "Bewa", Nazwisko = "Pieaso", Email = "mibosai-siu@weiaop.soa", Pesel = "00000000000", CzyMezczyzna = false, DataUrodzenia = new DateTime(1990, 3, 3), Telefon = "", Miejscowosc = "", Ulica = "", KodPocztowy = "", NumerPosesji = "", NumerLokalu = "", CzyZarchiwizowany = true });
            }
        }

        private static void StworzTabele()
        {
            using (var polaczenie = new SqliteConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();
                string kwerenda = @"CREATE TABLE IF NOT EXISTS Uzytkownicy(Id INTEGER PRIMARY KEY AUTOINCREMENT, Login TEXT, Imiona TEXT, Nazwisko TEXT, Email TEXT, Pesel TEXT, CzyMezczyzna INTEGER, DataUrodzenia TEXT, Telefon TEXT, Miejscowosc TEXT, Ulica TEXT, KodPocztowy TEXT, NumerPosesji TEXT, NumerLokalu TEXT, CzyZarchiwizowany INTEGER)";

                using (var command = new SqliteCommand(kwerenda, polaczenie))
                {
                    command.ExecuteNonQuery();
                }

            }
        }


        public static bool DodajLubZaaktualizujUzytkownika(Uzytkownik uzytkownik)
        {
            using (var polaczenie = new SqliteConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();


                string sprawdzenieKwerenda = "SELECT COUNT(*) FROM Uzytkownicy WHERE (Email = @Email OR Pesel = @Pesel) AND ID != @Id";
                using (var komendaSprawdzacz = new SqliteCommand(sprawdzenieKwerenda, polaczenie))
                {
                    komendaSprawdzacz.Parameters.AddWithValue("@Email", uzytkownik.Email);
                    komendaSprawdzacz.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel);
                    komendaSprawdzacz.Parameters.AddWithValue("@Id", uzytkownik.Id);

                    long iloscRekordow = (long) komendaSprawdzacz.ExecuteScalar();
                    if (iloscRekordow > 0)
                    {
                        MessageBox.Show("Wykryto duplikacje adresu e-mail lub numeru pesel. Użytkownik o takch danych istnieje już w bazie", "Błąd zapisu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    using (var komenda = new SqliteCommand("", polaczenie))
                    {
                        if (uzytkownik.Id == -1)
                        {
                            komenda.CommandText = "INSERT INTO Uzytkownicy (Login, Imiona, Nazwisko, Email, Pesel, CzyMezczyzna, DataUrodzenia, Telefon, Miejscowosc, Ulica, KodPocztowy, NumerPosesji, NumerLokalu, CzyZarchiwizowany) VALUES (@Login, @Imiona, @Nazwisko, @Email, @Pesel, @CzyMezczyzna, @DataUrodzenia, @Telefon, @Miejscowosc, @Ulica, @KodPocztowy, @NumerPosesji, @NumerLokalu, @CzyZarchiwizowany)";
                        } 
                        else
                        {
                            komenda.CommandText = "UPDATE Uzytkownicy SET Login = @Login, Imiona = @Imiona, Nazwisko = @Nazwisko, Email = @Email, Pesel = @Pesel, CzyMezczyzna = @CzyMezczyzna, DataUrodzenia = @DataUrodzenia, Telefon = @Telefon, Miejscowosc = @Miejscowosc, Ulica = @Ulica, KodPocztowy = @KodPocztowy, NumerPosesji = @NumerPosesji, NumerLokalu = @NumerLokalu, CzyZarchiwizowany = @CzyZarchiwizowany WHERE Id = @Id";
                            komenda.Parameters.AddWithValue("@Id", uzytkownik.Id);
                        }

                        komenda.Parameters.AddWithValue("@Login", uzytkownik.Login ?? "");
                        komenda.Parameters.AddWithValue("@Imiona", uzytkownik.Imiona ?? "");
                        komenda.Parameters.AddWithValue("@Nazwisko", uzytkownik.Nazwisko ?? "");
                        komenda.Parameters.AddWithValue("@Email", uzytkownik.Email ?? "");
                        komenda.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel ?? "");
                        komenda.Parameters.AddWithValue("@CzyMezczyzna", uzytkownik.CzyMezczyzna ? 1 : 0);
                        komenda.Parameters.AddWithValue("@DataUrodzenia", uzytkownik.DataUrodzenia.ToString("yyyy-MM-dd"));
                        komenda.Parameters.AddWithValue("@Telefon", uzytkownik.Telefon ?? "");
                        komenda.Parameters.AddWithValue("@Miejscowosc", uzytkownik.Miejscowosc ?? "");
                        komenda.Parameters.AddWithValue("@Ulica", uzytkownik.Ulica ?? "");
                        komenda.Parameters.AddWithValue("@KodPocztowy", uzytkownik.KodPocztowy ?? "");
                        komenda.Parameters.AddWithValue("@NumerPosesji", uzytkownik.NumerPosesji ?? "");
                        komenda.Parameters.AddWithValue("@NumerLokalu", uzytkownik.NumerLokalu ?? "");
                        komenda.Parameters.AddWithValue("@CzyZarchiwizowany", uzytkownik.CzyZarchiwizowany ? 1 : 0);

                        if (uzytkownik.Id == -1)
                        {
                            uzytkownik.Id = Convert.ToInt32(komenda.ExecuteScalar());
                            Uzytkownicy.Add(uzytkownik);
                        } 
                        else
                        {
                            komenda.ExecuteNonQuery();
                        }

                    }

                }
            }
            return true;
        }

        public static bool ZaarchiwizujUzytkownika(Uzytkownik wybrany)
        {
            wybrany.Imiona = "Zarchiwizowane";
            wybrany.Nazwisko = "Dane";
            wybrany.Email = $"brak_{wybrany.Id}@danych.pl";
            wybrany.Pesel = wybrany.Id.ToString().PadLeft(11, '0');
            wybrany.Telefon = "";
            wybrany.Miejscowosc = "";
            wybrany.Ulica = "";
            wybrany.KodPocztowy = "";
            wybrany.NumerPosesji = "";
            wybrany.NumerLokalu = "";
            wybrany.CzyZarchiwizowany = true;
          
            return DodajLubZaaktualizujUzytkownika(wybrany);
        }
    }
}
