using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Przychodnia
{
    internal class BazaDanych
    {
        public static readonly string POLACZENIE_STRING = @"Server=KAKUR\SQLEXPRESS01;Database=Przychodnia(NOWA);Trusted_Connection=True;TrustServerCertificate=True;";

        public static BindingList<Uzytkownik> Uzytkownicy { get; set; } = new BindingList<Uzytkownik>();
        public static Uzytkownik? ZALOGOWANY_UZYTKOWNIK { get; set; } = null;

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

        public static (bool CzySaBledy, string Komunikat) SprawdzSileHasla(string haslo)
        {
            if (string.IsNullOrEmpty(haslo)) return (true, "Hasło nie może być puste.");

            List<string> bledy = new List<string>();

            if (haslo.Length < 8) bledy.Add("- jest za krótkie (minimum 8 znaków)");
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

        public static void ZaladujBazeDanych()
        {
            Uzytkownicy.Clear();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string kwerenda = @"
                            SELECT u.UserID, u.Login, u.FirstName, u.LastName, u.Email, u.IsArchived, u.Phone,
                                   p.PESEL, p.BirthDate, p.Gender, p.City, p.Street, p.PostalCode, p.HouseNumber, p.ApartmentNumber
                            FROM Users u
                            LEFT JOIN Persons p ON u.PersonID = p.PersonID";

                    using (var komenda = new Microsoft.Data.SqlClient.SqlCommand(kwerenda, polaczenie))
                    using (var czytnik = komenda.ExecuteReader())
                    {
                        while (czytnik.Read())
                        {
                            Uzytkownicy.Add(new Uzytkownik
                            {
                                Id = Convert.ToInt32(czytnik["UserID"]),
                                Login = czytnik["Login"].ToString(),
                                Imiona = czytnik["FirstName"].ToString(),
                                Nazwisko = czytnik["LastName"].ToString(),
                                Email = czytnik["Email"].ToString(),
                                Pesel = czytnik["PESEL"]?.ToString() ?? "",
                                CzyMezczyzna = czytnik["Gender"] != DBNull.Value && Convert.ToInt32(czytnik["Gender"]) == 1,
                                DataUrodzenia = czytnik["BirthDate"] != DBNull.Value ? Convert.ToDateTime(czytnik["BirthDate"]) : DateTime.Now,
                                Telefon = czytnik["Phone"].ToString(),
                                Miejscowosc = czytnik["City"]?.ToString() ?? "",
                                Ulica = czytnik["Street"]?.ToString() ?? "",
                                KodPocztowy = czytnik["PostalCode"]?.ToString() ?? "",
                                NumerPosesji = czytnik["HouseNumber"]?.ToString() ?? "",
                                NumerLokalu = czytnik["ApartmentNumber"]?.ToString() ?? "",
                                CzyZarchiwizowany = Convert.ToBoolean(czytnik["IsArchived"])
                            });
                        }
                    }

                    string kwerendaRole = "SELECT UserID, RoleID FROM UserRoles";
                    using (var komendaRole = new Microsoft.Data.SqlClient.SqlCommand(kwerendaRole, polaczenie))
                    using (var czytnikRole = komendaRole.ExecuteReader())
                    {
                        while (czytnikRole.Read())
                        {
                            int uid = Convert.ToInt32(czytnikRole["UserID"]);
                            int rid = Convert.ToInt32(czytnikRole["RoleID"]);
                            var u = Uzytkownicy.FirstOrDefault(x => x.Id == uid);
                            if (u != null) u.IdRol.Add(rid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd ładowania bazy: " + ex.Message);
            }
        }

        public static List<Rola> PobierzWszystkieRole()
        {
            List<Rola> role = new List<Rola>();
            using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();
                string sql = "SELECT RoleID, RoleName FROM Roles ORDER BY RoleName ASC";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                using (var czytnik = cmd.ExecuteReader())
                {
                    while (czytnik.Read())
                    {
                        role.Add(new Rola
                        {
                            Id = Convert.ToInt32(czytnik["RoleID"]),
                            Nazwa = czytnik["RoleName"].ToString()
                        });
                    }
                }
            }
            return role;
        }

        public static bool DodajLubZaaktualizujUzytkownika(Uzytkownik uzytkownik)
        {
            using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();
                using (var transakcja = polaczenie.BeginTransaction())
                {
                    try
                    {
                        if (uzytkownik.Id <= 0) // NOWY UŻYTKOWNIK
                        {
                            string sqlPerson = @"INSERT INTO Persons (FirstName, LastName, PESEL, BirthDate, Gender, Phone, City, Street, PostalCode, HouseNumber, ApartmentNumber) 
                                                 OUTPUT INSERTED.PersonID
                                                 VALUES (@FN, @LN, @Pesel, @BD, @Gen, @Ph, @City, @Street, @Postal, @HN, @AN)";
                            int nowePersonId;
                            using (var cmdPerson = new Microsoft.Data.SqlClient.SqlCommand(sqlPerson, polaczenie, transakcja))
                            {
                                cmdPerson.Parameters.AddWithValue("@FN", uzytkownik.Imiona ?? "");
                                cmdPerson.Parameters.AddWithValue("@LN", uzytkownik.Nazwisko ?? "");
                                cmdPerson.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel ?? "");
                                cmdPerson.Parameters.AddWithValue("@BD", uzytkownik.DataUrodzenia);
                                cmdPerson.Parameters.AddWithValue("@Gen", uzytkownik.CzyMezczyzna ? 1 : 0);
                                cmdPerson.Parameters.AddWithValue("@Ph", uzytkownik.Telefon ?? "");
                                cmdPerson.Parameters.AddWithValue("@City", uzytkownik.Miejscowosc ?? "");
                                cmdPerson.Parameters.AddWithValue("@Street", uzytkownik.Ulica ?? "");
                                cmdPerson.Parameters.AddWithValue("@Postal", uzytkownik.KodPocztowy ?? "");
                                cmdPerson.Parameters.AddWithValue("@HN", uzytkownik.NumerPosesji ?? "");
                                cmdPerson.Parameters.AddWithValue("@AN", uzytkownik.NumerLokalu ?? "");
                                nowePersonId = (int)cmdPerson.ExecuteScalar();
                            }

                            string sqlUser = @"INSERT INTO Users (Login, FirstName, LastName, Email, Phone, IsArchived, CreatedAt, PasswordHash, PersonID) 
                                               OUTPUT INSERTED.UserID
                                               VALUES (@Login, @FirstName, @LastName, @Email, @Phone, @IsArchived, GETDATE(), @Pass, @PersonID)";

                            int nowyId;
                            using (var cmdUser = new Microsoft.Data.SqlClient.SqlCommand(sqlUser, polaczenie, transakcja))
                            {
                                cmdUser.Parameters.AddWithValue("@Login", uzytkownik.Login ?? "");
                                cmdUser.Parameters.AddWithValue("@FirstName", uzytkownik.Imiona ?? "");
                                cmdUser.Parameters.AddWithValue("@LastName", uzytkownik.Nazwisko ?? "");
                                cmdUser.Parameters.AddWithValue("@Email", uzytkownik.Email ?? "");
                                cmdUser.Parameters.AddWithValue("@Phone", uzytkownik.Telefon ?? "");
                                cmdUser.Parameters.AddWithValue("@IsArchived", uzytkownik.CzyZarchiwizowany);
                                cmdUser.Parameters.AddWithValue("@Pass", HashujHaslo(uzytkownik.Haslo ?? ""));
                                cmdUser.Parameters.AddWithValue("@PersonID", nowePersonId);
                                nowyId = (int)cmdUser.ExecuteScalar();
                            }

                            uzytkownik.Id = nowyId;
                            Uzytkownicy.Add(uzytkownik);
                        }
                        else // EDYCJA UŻYTKOWNIKA
                        {
                            // Poprawka: Aktualizujemy hasło TYLKO wtedy, gdy wpisano nowe (żeby nie nadpisać go pustym stringiem przy edycji danych)
                            string updUser = @"UPDATE Users SET FirstName=@FN, LastName=@LN, Email=@Em, Phone=@Ph, IsArchived=@Arc ";
                            if (!string.IsNullOrEmpty(uzytkownik.Haslo))
                            {
                                updUser += ", PasswordHash=@Pass, WymagaZmianyHasla=0 ";
                            }
                            updUser += "WHERE UserID=@Id";

                            using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(updUser, polaczenie, transakcja))
                            {
                                cmd.Parameters.AddWithValue("@Id", uzytkownik.Id);
                                cmd.Parameters.AddWithValue("@FN", uzytkownik.Imiona);
                                cmd.Parameters.AddWithValue("@LN", uzytkownik.Nazwisko);
                                cmd.Parameters.AddWithValue("@Em", uzytkownik.Email);
                                cmd.Parameters.AddWithValue("@Ph", uzytkownik.Telefon);
                                cmd.Parameters.AddWithValue("@Arc", uzytkownik.CzyZarchiwizowany);

                                if (!string.IsNullOrEmpty(uzytkownik.Haslo))
                                {
                                    cmd.Parameters.AddWithValue("@Pass", HashujHaslo(uzytkownik.Haslo));
                                }

                                cmd.ExecuteNonQuery();
                            }

                            string updPerson = @"UPDATE Persons SET FirstName=@FN, LastName=@LN, PESEL=@Pesel, BirthDate=@BD, Gender=@Gen, Phone=@Ph, 
                                                 City=@City, Street=@Street, PostalCode=@Postal, HouseNumber=@HN, ApartmentNumber=@AN 
                                                 WHERE PersonID = (SELECT PersonID FROM Users WHERE UserID = @Id)";
                            using (var cmdP = new Microsoft.Data.SqlClient.SqlCommand(updPerson, polaczenie, transakcja))
                            {
                                cmdP.Parameters.AddWithValue("@Id", uzytkownik.Id);
                                cmdP.Parameters.AddWithValue("@FN", uzytkownik.Imiona ?? "");
                                cmdP.Parameters.AddWithValue("@LN", uzytkownik.Nazwisko ?? "");
                                cmdP.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel ?? "");
                                cmdP.Parameters.AddWithValue("@BD", uzytkownik.DataUrodzenia);
                                cmdP.Parameters.AddWithValue("@Gen", uzytkownik.CzyMezczyzna ? 1 : 0);
                                cmdP.Parameters.AddWithValue("@Ph", uzytkownik.Telefon ?? "");
                                cmdP.Parameters.AddWithValue("@City", uzytkownik.Miejscowosc ?? "");
                                cmdP.Parameters.AddWithValue("@Street", uzytkownik.Ulica ?? "");
                                cmdP.Parameters.AddWithValue("@Postal", uzytkownik.KodPocztowy ?? "");
                                cmdP.Parameters.AddWithValue("@HN", uzytkownik.NumerPosesji ?? "");
                                cmdP.Parameters.AddWithValue("@AN", uzytkownik.NumerLokalu ?? "");
                                cmdP.ExecuteNonQuery();
                            }
                        }

                        // Zapis ról
                        string usunRole = "DELETE FROM UserRoles WHERE UserID=@Id";
                        using (var cmdUsun = new Microsoft.Data.SqlClient.SqlCommand(usunRole, polaczenie, transakcja))
                        {
                            cmdUsun.Parameters.AddWithValue("@Id", uzytkownik.Id);
                            cmdUsun.ExecuteNonQuery();
                        }

                        if (!uzytkownik.CzyZarchiwizowany)
                        {
                            string dodajRole = "INSERT INTO UserRoles (UserID, RoleID) VALUES (@Uid, @Rid)";
                            foreach (int rid in uzytkownik.IdRol)
                            {
                                using (var cmdDodaj = new Microsoft.Data.SqlClient.SqlCommand(dodajRole, polaczenie, transakcja))
                                {
                                    cmdDodaj.Parameters.AddWithValue("@Uid", uzytkownik.Id);
                                    cmdDodaj.Parameters.AddWithValue("@Rid", rid);
                                    cmdDodaj.ExecuteNonQuery();
                                }
                            }
                        }

                        transakcja.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transakcja.Rollback();
                        MessageBox.Show("Błąd zapisu: " + ex.Message);
                        return false;
                    }
                }
            }
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

        public static void MasowoNadajRole(List<Uzytkownik> zaznaczeni, int id)
        {
            foreach (Uzytkownik uzytkownik in zaznaczeni)
            {
                if (!uzytkownik.IdRol.Contains(id))
                {
                    uzytkownik.IdRol.Add(id);
                    DodajLubZaaktualizujUzytkownika(uzytkownik);
                }
            }
        }

        public static void MasowoZabierzRole(List<Uzytkownik> zaznaczeni, int id)
        {
            foreach (Uzytkownik uzytkownik in zaznaczeni)
            {
                if (uzytkownik.IdRol.Contains(id))
                {
                    uzytkownik.IdRol.Remove(id);
                    DodajLubZaaktualizujUzytkownika(uzytkownik);
                }
            }
        }

        public static (bool Sukces, string Komunikat, DateTime? ZablokowanyDo, bool WymagaZmiany) SprobujZalogowac(string login, string haslo)
        {
            ZALOGOWANY_UZYTKOWNIK = null;
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();

                    string kwerendaSzukaj = @"SELECT UserID, PasswordHash, FailedLoginAttempts, BlockedUntil, WymagaZmianyHasla 
                                      FROM Users WHERE Login = @Login AND IsArchived = 0";

                    using (var komenda = new Microsoft.Data.SqlClient.SqlCommand(kwerendaSzukaj, polaczenie))
                    {
                        komenda.Parameters.AddWithValue("@Login", login);
                        using (var czytnik = komenda.ExecuteReader())
                        {
                            if (czytnik.Read())
                            {
                                int id = Convert.ToInt32(czytnik["UserID"]);
                                string dbHash = czytnik["PasswordHash"].ToString();
                                int bledy = Convert.ToInt32(czytnik["FailedLoginAttempts"]);
                                DateTime? blokada = czytnik["BlockedUntil"] as DateTime?;
                                bool wymagaZmiany = Convert.ToBoolean(czytnik["WymagaZmianyHasla"]);

                                if (blokada.HasValue && blokada.Value > DateTime.Now)
                                    return (false, "dany login został zablokowany", blokada.Value, false);

                                if (dbHash == HashujHaslo(haslo))
                                {
                                    czytnik.Close();
                                    string resetQuery = "UPDATE Users SET FailedLoginAttempts = 0, BlockedUntil = NULL WHERE UserID = @Id";
                                    using (var cmdReset = new Microsoft.Data.SqlClient.SqlCommand(resetQuery, polaczenie))
                                    {
                                        cmdReset.Parameters.AddWithValue("@Id", id);
                                        cmdReset.ExecuteNonQuery();
                                    }

                                    ZaladujBazeDanych();
                                    ZALOGOWANY_UZYTKOWNIK = Uzytkownicy.FirstOrDefault(u => u.Id == id);
                                    if (ZALOGOWANY_UZYTKOWNIK != null) ZALOGOWANY_UZYTKOWNIK.Haslo = haslo;

                                    return (true, "Zalogowano pomyślnie.", null, wymagaZmiany);
                                }
                                else
                                {
                                    bledy++;
                                    DateTime? nowaBlokada = null;
                                    string komunikat = "Dane logowania są niepoprawne.";
                                    if (bledy >= 3)
                                    {
                                        nowaBlokada = DateTime.Now.AddSeconds(30);
                                        komunikat = "Przekroczono limit prób. Konto zostało zablokowane na 30 sekund.";
                                    }
                                    czytnik.Close();
                                    string updateQuery = "UPDATE Users SET FailedLoginAttempts = @Bledy, BlockedUntil = @Blokada WHERE UserID = @Id";
                                    using (var cmdFail = new Microsoft.Data.SqlClient.SqlCommand(updateQuery, polaczenie))
                                    {
                                        cmdFail.Parameters.AddWithValue("@Id", id);
                                        cmdFail.Parameters.AddWithValue("@Bledy", bledy);
                                        if (nowaBlokada.HasValue) cmdFail.Parameters.AddWithValue("@Blokada", nowaBlokada.Value);
                                        else cmdFail.Parameters.AddWithValue("@Blokada", DBNull.Value);
                                        cmdFail.ExecuteNonQuery();
                                    }
                                    return (false, komunikat, nowaBlokada, false);
                                }
                            }
                            else return (false, "Dane logowania są niepoprawne.", null, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
                return (false, "Błąd systemu.", null, false);
            }
        }

        public static (bool Sukces, string Komunikat) ZresetujHaslo(string email)
        {
            // Walidacja formatu e-mail
            if (string.IsNullOrWhiteSpace(email) || !System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return (false, "Podano niepoprawny format adresu e-mail.");
            }

            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    // Sprawdzamy, czy użytkownik istnieje
                    string szukajSql = "SELECT UserID FROM Users WHERE Email = @Email AND IsArchived = 0";
                    int userId = 0;
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(szukajSql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        var wynik = cmd.ExecuteScalar();
                        if (wynik == null) return (false, "Brak aktywnego konta przypisanego do tego adresu e-mail.");
                        userId = Convert.ToInt32(wynik);
                    }

                    // Generujemy jednorazowe hasło (np. Temp4281!)
                    string noweHaslo = "Temp" + new Random().Next(1000, 9999) + "!";

                    // Zapisujemy nowy Hash i oznaczamy flagę "WymagaZmianyHasla = 1"
                    string updateSql = "UPDATE Users SET PasswordHash = @Hash, WymagaZmianyHasla = 1, FailedLoginAttempts = 0, BlockedUntil = NULL WHERE UserID = @Id";
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(updateSql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@Hash", HashujHaslo(noweHaslo));
                        cmd.Parameters.AddWithValue("@Id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    // Tutaj wysyłalibyśmy fizycznego e-maila protokołem SMTP. Do testów pokazujemy w oknie.
                    return (true, $"Hasło zresetowane pomyślnie.\n\n[SYMULACJA WYSYŁKI E-MAIL]\nWiadomość wysłana na: {email}\nTwoje hasło jednorazowe to: {noweHaslo}");
                }
            }
            catch (Exception ex)
            {
                return (false, "Błąd systemu: " + ex.Message);
            }
        }

        public static bool CzyNadalWymagaZmiany(int userId)
        {
            using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
            {
                polaczenie.Open();
                string sql = "SELECT WymagaZmianyHasla FROM Users WHERE UserID = @Id";
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                {
                    cmd.Parameters.AddWithValue("@Id", userId);
                    var wynik = cmd.ExecuteScalar();
                    return wynik != null && Convert.ToBoolean(wynik);
                }
            }
        }

    }
}