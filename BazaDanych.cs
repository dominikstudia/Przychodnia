using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Przychodnia
{
    internal class BazaDanych
    {
        public static readonly string POLACZENIE_STRING = @"Server=(localdb)\Local;Database=Przychodnia;Trusted_Connection=True;TrustServerCertificate=True;";

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

        public static void ZaladujBazeDanych()
        {
            Uzytkownicy.Clear();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string kwerenda = @"
                            SELECT UserID, Login, FirstName, LastName, Email, IsArchived, Phone,
                                   PESEL, BirthDate, Gender, City, Street, PostalCode, HouseNumber, ApartmentNumber
                            FROM Users";

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
                            // Zapisujemy wszystko od razu do tabeli Users
                            string sqlUser = @"INSERT INTO Users (Login, PasswordHash, Email, FirstName, LastName, PESEL, BirthDate, Gender, Phone, City, PostalCode, Street, HouseNumber, ApartmentNumber, IsArchived, CreatedAt, FailedLoginAttempts, WymagaZmianyHasla) 
                                               OUTPUT INSERTED.UserID
                                               VALUES (@Login, @Pass, @Email, @FirstName, @LastName, @Pesel, @BD, @Gen, @Phone, @City, @Postal, @Street, @HN, @AN, @IsArchived, GETDATE(), 0, 0)";

                            int nowyId;
                            string hashedPass = HashujHaslo(uzytkownik.Haslo ?? "");
                            using (var cmdUser = new Microsoft.Data.SqlClient.SqlCommand(sqlUser, polaczenie, transakcja))
                            {
                                cmdUser.Parameters.AddWithValue("@Login", uzytkownik.Login ?? "");
                                cmdUser.Parameters.AddWithValue("@Pass", hashedPass);
                                cmdUser.Parameters.AddWithValue("@Email", uzytkownik.Email ?? "");
                                cmdUser.Parameters.AddWithValue("@FirstName", uzytkownik.Imiona ?? "");
                                cmdUser.Parameters.AddWithValue("@LastName", uzytkownik.Nazwisko ?? "");
                                cmdUser.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel ?? "");
                                cmdUser.Parameters.AddWithValue("@BD", uzytkownik.DataUrodzenia);
                                cmdUser.Parameters.AddWithValue("@Gen", uzytkownik.CzyMezczyzna ? 1 : 0);
                                cmdUser.Parameters.AddWithValue("@Phone", uzytkownik.Telefon ?? "");
                                cmdUser.Parameters.AddWithValue("@City", uzytkownik.Miejscowosc ?? "");
                                cmdUser.Parameters.AddWithValue("@Postal", uzytkownik.KodPocztowy ?? "");
                                cmdUser.Parameters.AddWithValue("@Street", string.IsNullOrEmpty(uzytkownik.Ulica) ? DBNull.Value : (object)uzytkownik.Ulica);
                                cmdUser.Parameters.AddWithValue("@HN", uzytkownik.NumerPosesji ?? "");
                                cmdUser.Parameters.AddWithValue("@AN", string.IsNullOrEmpty(uzytkownik.NumerLokalu) ? DBNull.Value : (object)uzytkownik.NumerLokalu);
                                cmdUser.Parameters.AddWithValue("@IsArchived", uzytkownik.CzyZarchiwizowany);

                                nowyId = (int)cmdUser.ExecuteScalar();
                            }

                            uzytkownik.Id = nowyId;

                            // Zapisujemy hasło w historii
                            string histSql = "INSERT INTO PasswordHistory (UserID, PasswordHash, ModifiedAt) VALUES (@UId, @PHash, GETDATE())";
                            using (var histCmd = new Microsoft.Data.SqlClient.SqlCommand(histSql, polaczenie, transakcja))
                            {
                                histCmd.Parameters.AddWithValue("@UId", uzytkownik.Id);
                                histCmd.Parameters.AddWithValue("@PHash", hashedPass);
                                histCmd.ExecuteNonQuery();
                            }

                            Uzytkownicy.Add(uzytkownik);
                        }
                        else // EDYCJA UŻYTKOWNIKA
                        {
                            // Aktualizacja wszystkich danych leci prosto do Users
                            string updUser = @"UPDATE Users SET FirstName=@FN, LastName=@LN, Email=@Em, Phone=@Ph, IsArchived=@Arc, PESEL=@Pesel, BirthDate=@BD, Gender=@Gen, City=@City, Street=@Street, PostalCode=@Postal, HouseNumber=@HN, ApartmentNumber=@AN ";
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
                                cmd.Parameters.AddWithValue("@Pesel", uzytkownik.Pesel ?? "");
                                cmd.Parameters.AddWithValue("@BD", uzytkownik.DataUrodzenia);
                                cmd.Parameters.AddWithValue("@Gen", uzytkownik.CzyMezczyzna ? 1 : 0);
                                cmd.Parameters.AddWithValue("@City", uzytkownik.Miejscowosc ?? "");
                                cmd.Parameters.AddWithValue("@Postal", uzytkownik.KodPocztowy ?? "");
                                cmd.Parameters.AddWithValue("@Street", string.IsNullOrEmpty(uzytkownik.Ulica) ? DBNull.Value : (object)uzytkownik.Ulica);
                                cmd.Parameters.AddWithValue("@HN", uzytkownik.NumerPosesji ?? "");
                                cmd.Parameters.AddWithValue("@AN", string.IsNullOrEmpty(uzytkownik.NumerLokalu) ? DBNull.Value : (object)uzytkownik.NumerLokalu);

                                if (!string.IsNullOrEmpty(uzytkownik.Haslo))
                                {
                                    string hashedPass = HashujHaslo(uzytkownik.Haslo);
                                    cmd.Parameters.AddWithValue("@Pass", hashedPass);
                                    cmd.ExecuteNonQuery();

                                    string histSql = "INSERT INTO PasswordHistory (UserID, PasswordHash, ModifiedAt) VALUES (@UId, @PHash, GETDATE())";
                                    using (var histCmd = new Microsoft.Data.SqlClient.SqlCommand(histSql, polaczenie, transakcja))
                                    {
                                        histCmd.Parameters.AddWithValue("@UId", uzytkownik.Id);
                                        histCmd.Parameters.AddWithValue("@PHash", hashedPass);
                                        histCmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    cmd.ExecuteNonQuery();
                                }
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

                                // Akceptujemy prawidłowy Hash ORAZ awaryjnie jawny tekst
                                if (dbHash == HashujHaslo(haslo) || dbHash == haslo)
                                {
                                    czytnik.Close();

                                    // Auto-naprawa: Jeśli hasło było w bazie jawnym tekstem, od razu je szyfrujemy i podmieniamy na poprawne
                                    string naprawQuery = "UPDATE Users SET PasswordHash = @Hash, FailedLoginAttempts = 0, BlockedUntil = NULL WHERE UserID = @Id";
                                    using (var cmdNapraw = new Microsoft.Data.SqlClient.SqlCommand(naprawQuery, polaczenie))
                                    {
                                        cmdNapraw.Parameters.AddWithValue("@Hash", HashujHaslo(haslo));
                                        cmdNapraw.Parameters.AddWithValue("@Id", id);
                                        cmdNapraw.ExecuteNonQuery();
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

        public static (bool Sukces, string Komunikat) ZresetujHaslo(string login, string email)
        {
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    // 1. Sprawdzamy czy para Login + Email istnieje
                    string szukajSql = "SELECT UserID FROM Users WHERE Login = @Login AND Email = @Email AND IsArchived = 0";
                    int userId = 0;
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(szukajSql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Email", email);
                        var wynik = cmd.ExecuteScalar();
                        if (wynik == null) return (false, "Nie znaleziono aktywnego konta z podanym loginem i adresem e-mail.");
                        userId = Convert.ToInt32(wynik);
                    }

                    // 2. Generujemy hasło i hashujemy je
                    string noweHaslo = GenerujSilneHaslo();
                    string hashedPass = HashujHaslo(noweHaslo);

                    // 3. Update hasła w bazie i zapis do historii
                    string updateSql = @"UPDATE Users SET PasswordHash = @Hash, WymagaZmianyHasla = 1, FailedLoginAttempts = 0, BlockedUntil = NULL WHERE UserID = @Id;
                                         INSERT INTO PasswordHistory (UserID, PasswordHash, ModifiedAt) VALUES (@Id, @Hash, GETDATE());";
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(updateSql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@Hash", hashedPass);
                        cmd.Parameters.AddWithValue("@Id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    // 4. Fizyczna wysyłka
                    WyslijEmailResetujacy(email, noweHaslo);

                    return (true, "Hasło zostało zresetowane pomyślnie. Instrukcje zostały wysłane na Twój adres e-mail.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Błąd systemu podczas resetowania: " + ex.Message);
            }
        }

        private static void WyslijEmailResetujacy(string doAdres, string tymczasoweHaslo)
        {
            // Konfiguracja SMTP
            var smtp = new SmtpClient("sandbox.smtp.mailtrap.io")
            {
                Port = 2525,
                Credentials = new NetworkCredential("1df70a2b8177fd", "61e02e2e72128c"),
                EnableSsl = true,
            };

            var mail = new MailMessage();
            mail.From = new MailAddress("system@przychodnia.pl", "System Przychodnia");
            mail.To.Add(doAdres);
            mail.Subject = "Resetowanie hasła - Przychodnia";
            mail.Body = $"Witaj!\n\nTwoje hasło tymczasowe to: {tymczasoweHaslo}\n\nSystem wymusi jego zmianę przy pierwszym logowaniu.";

            smtp.Send(mail);
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

        public static bool CzyHasloByloUzyteOstatnio(int userId, string noweHaslo)
        {
            string hashNowegoHasla = HashujHaslo(noweHaslo);
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string sql = "SELECT COUNT(1) FROM PasswordHistory WHERE UserID = @UserId AND PasswordHash = @Hash";
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Hash", hashNowegoHasla);
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
            catch { return false; }
        }

        public static bool CzyPeselZajety(string pesel, int idOmijanegoUzytkownika)
        {
            return Uzytkownicy.Any(u => u.Pesel == pesel && u.Id != idOmijanegoUzytkownika);
        }

        public static bool CzyEmailZajety(string email, int idOmijanegoUzytkownika)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return Uzytkownicy.Any(u => u.Email != null && u.Email.ToLower() == email.ToLower() && u.Id != idOmijanegoUzytkownika);
        }

    }
}