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
        public static readonly string POLACZENIE_STRING = @"Server=localhost\SQLEXPRESS;Database=Przychodnia;Trusted_Connection=True;TrustServerCertificate=True;";

        public static BindingList<Uzytkownik> Uzytkownicy { get; set; } = new BindingList<Uzytkownik>();
        public static Uzytkownik? ZALOGOWANY_UZYTKOWNIK { get; set; } = null;

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
                            string hashedPass = Narzedzia.HashujHaslo(uzytkownik.Haslo ?? "");
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
                                    string hashedPass = Narzedzia.HashujHaslo(uzytkownik.Haslo);
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

        public static void ZaarchiwizujUzytkownika(Uzytkownik wybrany)
        {
            wybrany.Imiona = "Zarchiwizowane";
            wybrany.Nazwisko = "Dane";
            wybrany.Email = $"brak_{wybrany.Id}@danych.pl";
            wybrany.Pesel = wybrany.Id.ToString().PadLeft(11, '0');

            wybrany.Telefon = "000000000";
            wybrany.Miejscowosc = "Brak";
            wybrany.Ulica = "Brak";
            wybrany.KodPocztowy = "00-000";
            wybrany.NumerPosesji = "0";
            wybrany.NumerLokalu = "0";
            wybrany.CzyZarchiwizowany = true;
            wybrany.IdRol.Clear();

            DodajLubZaaktualizujUzytkownika(wybrany);
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
                                if (dbHash == Narzedzia.HashujHaslo(haslo) || dbHash == haslo)
                                {
                                    czytnik.Close();

                                    // Auto-naprawa: Jeśli hasło było w bazie jawnym tekstem, od razu je szyfrujemy i podmieniamy na poprawne
                                    string naprawQuery = "UPDATE Users SET PasswordHash = @Hash, FailedLoginAttempts = 0, BlockedUntil = NULL WHERE UserID = @Id";
                                    using (var cmdNapraw = new Microsoft.Data.SqlClient.SqlCommand(naprawQuery, polaczenie))
                                    {
                                        cmdNapraw.Parameters.AddWithValue("@Hash", Narzedzia.HashujHaslo(haslo));
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
                    string noweHaslo = Narzedzia.GenerujSilneHaslo();
                    string hashedPass = Narzedzia.HashujHaslo(noweHaslo);

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

        public static bool CzyHasloByloUzyteOstatnio(int userId, string noweHaslo)
        {
            string hashNowegoHasla = Narzedzia.HashujHaslo(noweHaslo);
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

        // =========================================================================
        // METODY DO OBSŁUGI REJESTRACJI WIZYT (ZGODNIE ZE SCHEMATEM BAZY I UC_WIZ_01)
        // =========================================================================

        /// <summary>
        /// Pobiera listę wszystkich dostępnych specjalizacji medycznych.
        /// </summary>
        public static List<string> PobierzSpecjalizacje()
        {
            List<string> specjalizacje = new List<string>();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string sql = "SELECT Name FROM Specializations ORDER BY Name ASC";
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    using (var czytnik = cmd.ExecuteReader())
                    {
                        while (czytnik.Read())
                        {
                            specjalizacje.Add(czytnik["Name"].ToString() ?? "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd pobierania specjalizacji: " + ex.Message, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return specjalizacje;
        }

        /// <summary>
        /// Pobiera lekarzy przefiltrowanych po wybranej specjalizacji (zgodnie z wymaganiem funkcjonalnym UC_WIZ_01).
        /// </summary>
        public static List<Uzytkownik> PobierzLekarzyPoSpecjalizacji(string nazwaSpecjalizacji)
        {
            List<Uzytkownik> lekarze = new List<Uzytkownik>();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    // Łączymy tabelę Users z Doctors oraz Specializations na podstawie kluczy obcych ze schematu
                    string sql = @"SELECT u.UserID, u.FirstName, u.LastName 
                                   FROM Users u
                                   JOIN Doctors d ON u.UserID = d.UserID
                                   JOIN Specializations s ON d.SpecializationID = s.SpecializationID
                                   WHERE s.Name = @SpecName AND u.IsArchived = 0";

                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@SpecName", nazwaSpecjalizacji);
                        using (var czytnik = cmd.ExecuteReader())
                        {
                            while (czytnik.Read())
                            {
                                // Tworzymy obiekt użytkownika z danymi potrzebnymi do identyfikacji lekarza
                                lekarze.Add(new Uzytkownik
                                {
                                    Id = Convert.ToInt32(czytnik["UserID"]),
                                    Imiona = czytnik["FirstName"].ToString(),
                                    Nazwisko = czytnik["LastName"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd pobierania lekarzy: " + ex.Message, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lekarze;
        }

        /// <summary>
        /// Pobiera listę wszystkich gabinetów lekarskich (pokojów).
        /// </summary>
        public static List<string> PobierzGabinety()
        {
            List<string> gabinety = new List<string>();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string sql = "SELECT RoomNumber FROM Rooms ORDER BY RoomNumber ASC";
                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    using (var czytnik = cmd.ExecuteReader())
                    {
                        while (czytnik.Read())
                        {
                            gabinety.Add(czytnik["RoomNumber"].ToString() ?? "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd pobierania gabinetów: " + ex.Message, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return gabinety;
        }

        /// <summary>
        /// Rejestruje nową wizytę w bazie danych, weryfikując uprzednio wszystkie warunki biznesowe i wyjątki z dokumentacji.
        /// </summary>
        public static (bool Sukces, string Komunikat) ZarejestrujWizyte(int idPacjentaUzytkownik, int idLekarzaUzytkownik, string numerGabinetu, DateTime dataGodzinaWizyty)
        {
            // E1: Walidacja daty z przeszłości
            if (dataGodzinaWizyty < DateTime.Now)
            {
                return (false, "Nie można zaplanować wizyty z datą wsteczną."); // Zgodnie z E1 w UC_WIZ_01
            }

            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();

                    // --- KROK 1: Pobranie ID z tabeli Doctors na podstawie UserID lekarza ---
                    int doctorId = 0;
                    string sqlDoc = "SELECT DoctorID FROM Doctors WHERE UserID = @UID";
                    using (var cmdDoc = new Microsoft.Data.SqlClient.SqlCommand(sqlDoc, polaczenie))
                    {
                        cmdDoc.Parameters.AddWithValue("@UID", idLekarzaUzytkownik);
                        var res = cmdDoc.ExecuteScalar();
                        if (res == null) return (false, "Nie odnaleziono powiązanego profilu lekarza.");
                        doctorId = Convert.ToInt32(res);
                    }

                    // --- KROK 2: Pobranie ID z tabeli Patients na podstawie UserID pacjenta ---
                    int patientId = 0;
                    string sqlPat = "SELECT PatientID FROM Patients WHERE UserID = @UID";
                    using (var cmdPat = new Microsoft.Data.SqlClient.SqlCommand(sqlPat, polaczenie))
                    {
                        cmdPat.Parameters.AddWithValue("@UID", idPacjentaUzytkownik);
                        var res = cmdPat.ExecuteScalar();
                        if (res == null) return (false, "Nie odnaleziono powiązanego profilu pacjenta.");
                        patientId = Convert.ToInt32(res);
                    }

                    // --- KROK 3: Pobranie RoomID na podstawie numeru gabinetu ---
                    int roomId = 0;
                    string sqlRoom = "SELECT RoomID FROM Rooms WHERE RoomNumber = @RNum";
                    using (var cmdRoom = new Microsoft.Data.SqlClient.SqlCommand(sqlRoom, polaczenie))
                    {
                        cmdRoom.Parameters.AddWithValue("@RNum", numerGabinetu);
                        var res = cmdRoom.ExecuteScalar();
                        if (res == null) return (false, "Nie odnaleziono wskazanego gabinetu w bazie.");
                        roomId = Convert.ToInt32(res);
                    }

                    // --- KROK 4: E2 - Walidacja konfliktu terminów lekarza (wizyta trwa 30 min) ---
                    string sqlCheckDoctor = @"
                        SELECT COUNT(1) FROM Visits 
                        WHERE DoctorID = @DocID AND Status != 'Anulowana'
                        AND VisitDateTime < DATEADD(minute, 30, @VTime) 
                        AND DATEADD(minute, 30, VisitDateTime) > @VTime";

                    using (var cmdCheckDoc = new Microsoft.Data.SqlClient.SqlCommand(sqlCheckDoctor, polaczenie))
                    {
                        cmdCheckDoc.Parameters.AddWithValue("@DocID", doctorId);
                        cmdCheckDoc.Parameters.AddWithValue("@VTime", dataGodzinaWizyty);
                        if (Convert.ToInt32(cmdCheckDoc.ExecuteScalar()) > 0)
                        {
                            return (false, "Lekarz jest niedostępny w wybranym terminie. Wybierz inną godzinę.");
                        }
                    }

                    // --- KROK 5: E3 - Walidacja konfliktu gabinetu (wizyta trwa 30 min) ---
                    string sqlCheckRoom = @"
                        SELECT COUNT(1) FROM Visits 
                        WHERE RoomID = @RoomID AND Status != 'Anulowana'
                        AND VisitDateTime < DATEADD(minute, 30, @VTime) 
                        AND DATEADD(minute, 30, VisitDateTime) > @VTime";

                    using (var cmdCheckRoom = new Microsoft.Data.SqlClient.SqlCommand(sqlCheckRoom, polaczenie))
                    {
                        cmdCheckRoom.Parameters.AddWithValue("@RoomID", roomId);
                        cmdCheckRoom.Parameters.AddWithValue("@VTime", dataGodzinaWizyty);
                        if (Convert.ToInt32(cmdCheckRoom.ExecuteScalar()) > 0)
                        {
                            return (false, "Wybrany gabinet jest przypisany do innej wizyty w tym czasie.");
                        }
                    }

                    // --- KROK 6: Zapisanie rekordu do tabeli Visits (Warunek końcowy: Status „Zarejestrowana”) ---
                    string sqlInsert = @"INSERT INTO Visits (PatientID, DoctorID, RoomID, VisitDateTime, Status) 
                                         VALUES (@PatientID, @DoctorID, @RoomID, @VisitDateTime, @Status)";

                    using (var cmdInsert = new Microsoft.Data.SqlClient.SqlCommand(sqlInsert, polaczenie))
                    {
                        cmdInsert.Parameters.AddWithValue("@PatientID", patientId);
                        cmdInsert.Parameters.AddWithValue("@DoctorID", doctorId);
                        cmdInsert.Parameters.AddWithValue("@RoomID", roomId);
                        cmdInsert.Parameters.AddWithValue("@VisitDateTime", dataGodzinaWizyty);
                        cmdInsert.Parameters.AddWithValue("@Status", "Zarejestrowana"); // Zgodnie z warunkiem końcowym

                        cmdInsert.ExecuteNonQuery();
                    }

                    return (true, "Wizyta została pomyślnie zarejestrowana w systemie.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Błąd krytyczny bazy danych: " + ex.Message);
            }
        }
        public static DataTable PobierzListeWizyt(Uzytkownik zalogowanyUzytkownik)
        {
            DataTable tabelaWynikowa = new DataTable();

            tabelaWynikowa.Columns.Add("ID Wizyty", typeof(int));
            tabelaWynikowa.Columns.Add("Data i Godzina", typeof(DateTime));
            tabelaWynikowa.Columns.Add("Imię i Nazwisko Pacjenta", typeof(string));
            tabelaWynikowa.Columns.Add("PESEL", typeof(string));
            tabelaWynikowa.Columns.Add("Imię i Nazwisko Lekarza", typeof(string));
            tabelaWynikowa.Columns.Add("Specjalizacja", typeof(string));
            tabelaWynikowa.Columns.Add("Numer Gabinetu", typeof(string));
            tabelaWynikowa.Columns.Add("Status Wizyty", typeof(string));

            // Pola techniczne, ukryte w widoku interfejsu użytkownika
            tabelaWynikowa.Columns.Add("Schorzenia", typeof(string));
            tabelaWynikowa.Columns.Add("Zalecenia", typeof(string));

            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string sql = @"
                SELECT 
                    v.VisitID,
                    v.VisitDateTime,
                    u_pat.FirstName + ' ' + u_pat.LastName AS Pacjent,
                    u_pat.PESEL,
                    u_doc.FirstName + ' ' + u_doc.LastName AS Lekarz,
                    s.Name AS Specjalizacja,
                    r.RoomNumber,
                    v.Status,
                    vr.Symptoms,
                    vr.Recommendations
                FROM Visits v
                JOIN Patients p ON v.PatientID = p.PatientID
                JOIN Users u_pat ON p.UserID = u_pat.UserID
                JOIN Doctors d ON v.DoctorID = d.DoctorID
                JOIN Users u_doc ON d.UserID = u_doc.UserID
                JOIN Specializations s ON d.SpecializationID = s.SpecializationID
                JOIN Rooms r ON v.RoomID = r.RoomID
                LEFT JOIN VisitResults vr ON v.VisitID = vr.VisitID";

                    if (Role.SprawdzCzyMaRole(zalogowanyUzytkownik, Role.LEKARZ))
                    {
                        sql += " WHERE u_doc.UserID = @LekarzUserID";
                    }

                    sql += " ORDER BY v.VisitDateTime ASC";

                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    {
                        if (Role.SprawdzCzyMaRole(zalogowanyUzytkownik, Role.LEKARZ))
                        {
                            cmd.Parameters.AddWithValue("@LekarzUserID", zalogowanyUzytkownik.Id);
                        }

                        using (var czytnik = cmd.ExecuteReader())
                        {
                            while (czytnik.Read())
                            {
                                tabelaWynikowa.Rows.Add(
                                    Convert.ToInt32(czytnik["VisitID"]),
                                    Convert.ToDateTime(czytnik["VisitDateTime"]),
                                    czytnik["Pacjent"].ToString(),
                                    czytnik["PESEL"].ToString(),
                                    czytnik["Lekarz"].ToString(),
                                    czytnik["Specjalizacja"].ToString(),
                                    czytnik["RoomNumber"].ToString(),
                                    czytnik["Status"].ToString(),
                                    czytnik["Symptoms"] != DBNull.Value ? czytnik["Symptoms"].ToString() : "",
                                    czytnik["Recommendations"] != DBNull.Value ? czytnik["Recommendations"].ToString() : ""
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas pobierania listy wizyt: " + ex.Message, "Błąd bazy danych", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabelaWynikowa;
        }

        public static bool AktualizujWynikiWizyty(Wizyta wizyta)
        {
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();

                    string sqlVisits = "UPDATE Visits SET Status = @Status WHERE VisitID = @VisitID";
                    using (var cmdVisits = new Microsoft.Data.SqlClient.SqlCommand(sqlVisits, polaczenie))
                    {
                        cmdVisits.Parameters.AddWithValue("@Status", wizyta.Status);
                        cmdVisits.Parameters.AddWithValue("@VisitID", wizyta.IdWizyty);
                        cmdVisits.ExecuteNonQuery();
                    }

                    string sqlResults = @"
                IF EXISTS (SELECT 1 FROM VisitResults WHERE VisitID = @VisitID)
                    UPDATE VisitResults 
                    SET Symptoms = @Symptoms, Recommendations = @Recommendations 
                    WHERE VisitID = @VisitID
                ELSE
                    INSERT INTO VisitResults (VisitID, Symptoms, Recommendations) 
                    VALUES (@VisitID, @Symptoms, @Recommendations)";

                    using (var cmdResults = new Microsoft.Data.SqlClient.SqlCommand(sqlResults, polaczenie))
                    {
                        cmdResults.Parameters.AddWithValue("@VisitID", wizyta.IdWizyty);
                        cmdResults.Parameters.AddWithValue("@Symptoms", (object)wizyta.Schorzenia ?? DBNull.Value);
                        cmdResults.Parameters.AddWithValue("@Recommendations", (object)wizyta.Zalecenia ?? DBNull.Value);
                        cmdResults.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu do bazy: " + ex.Message);
                return false;
            }
        }

        public static bool AktualizujDanePacjenta(Uzytkownik pacjent)
        {
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();

                    // Aktualizacja tylko danych demograficznych i adresowych pacjenta (bez ról i hasła)
                    string sql = @"UPDATE Users 
                           SET FirstName=@FN, LastName=@LN, Email=@Em, Phone=@Ph, 
                               PESEL=@Pesel, BirthDate=@BD, Gender=@Gen, City=@City, 
                               Street=@Street, PostalCode=@Postal, HouseNumber=@HN, ApartmentNumber=@AN 
                           WHERE UserID=@Id";

                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    {
                        cmd.Parameters.AddWithValue("@Id", pacjent.Id);
                        cmd.Parameters.AddWithValue("@FN", pacjent.Imiona ?? "");
                        cmd.Parameters.AddWithValue("@LN", pacjent.Nazwisko ?? "");
                        cmd.Parameters.AddWithValue("@Em", string.IsNullOrEmpty(pacjent.Email) ? DBNull.Value : (object)pacjent.Email);
                        cmd.Parameters.AddWithValue("@Ph", pacjent.Telefon ?? "");
                        cmd.Parameters.AddWithValue("@Pesel", pacjent.Pesel ?? "");
                        cmd.Parameters.AddWithValue("@BD", pacjent.DataUrodzenia);
                        cmd.Parameters.AddWithValue("@Gen", pacjent.CzyMezczyzna ? 1 : 0);
                        cmd.Parameters.AddWithValue("@City", pacjent.Miejscowosc ?? "");
                        cmd.Parameters.AddWithValue("@Postal", pacjent.KodPocztowy ?? "");
                        cmd.Parameters.AddWithValue("@Street", string.IsNullOrEmpty(pacjent.Ulica) ? DBNull.Value : (object)pacjent.Ulica);
                        cmd.Parameters.AddWithValue("@HN", pacjent.NumerPosesji ?? "");
                        cmd.Parameters.AddWithValue("@AN", string.IsNullOrEmpty(pacjent.NumerLokalu) ? DBNull.Value : (object)pacjent.NumerLokalu);

                        cmd.ExecuteNonQuery();
                    }

                    // Odświeżenie listy użytkowników w pamięci aplikacji, żeby tabela od razu pokazała nowe dane
                    ZaladujBazeDanych();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd zapisu do bazy podczas edycji pacjenta: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // Prosta klasa do trzymania ID i Nazwy specjalizacji dla ComboBoxa
        public class Specjalizacja
        {
            public int Id { get; set; }
            public string Nazwa { get; set; }

            // Nadpisanie ToString() sprawia, że ComboBox domyślnie wyświetla tę wartość
            public override string ToString()
            {
                return Nazwa;
            }
        }

        /// <summary>
        /// Pobiera listę specjalizacji wraz z ich identyfikatorami.
        /// </summary>
        public static List<Specjalizacja> PobierzWszystkieSpecjalizacjeZId()
        {
            List<Specjalizacja> lista = new List<Specjalizacja>();
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();
                    string sql = "SELECT SpecializationID, Name FROM Specializations ORDER BY Name ASC";

                    using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, polaczenie))
                    using (var czytnik = cmd.ExecuteReader())
                    {
                        while (czytnik.Read())
                        {
                            lista.Add(new Specjalizacja
                            {
                                Id = Convert.ToInt32(czytnik["SpecializationID"]),
                                Nazwa = czytnik["Name"].ToString() ?? ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd pobierania specjalizacji: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lista;
        }

        /// <summary>
        /// Przypisuje lub aktualizuje specjalizację dla danego lekarza w tabeli Doctors.
        /// </summary>
        public static bool PrzypiszSpecjalizacjeLekarzowi(int userId, int specializationId)
        {
            try
            {
                using (var polaczenie = new Microsoft.Data.SqlClient.SqlConnection(POLACZENIE_STRING))
                {
                    polaczenie.Open();

                    // Krok 1: Sprawdzamy, czy ten użytkownik figuruje już w tabeli Doctors
                    string checkSql = "SELECT DoctorID FROM Doctors WHERE UserID = @UID";
                    int? doctorId = null;

                    using (var checkCmd = new Microsoft.Data.SqlClient.SqlCommand(checkSql, polaczenie))
                    {
                        checkCmd.Parameters.AddWithValue("@UID", userId);
                        var result = checkCmd.ExecuteScalar();
                        if (result != null)
                        {
                            doctorId = Convert.ToInt32(result);
                        }
                    }

                    // Krok 2: Jeśli istnieje - robimy UPDATE, jeśli nie - robimy INSERT
                    if (doctorId.HasValue)
                    {
                        string updateSql = "UPDATE Doctors SET SpecializationID = @SpecID WHERE DoctorID = @DocID";
                        using (var updateCmd = new Microsoft.Data.SqlClient.SqlCommand(updateSql, polaczenie))
                        {
                            updateCmd.Parameters.AddWithValue("@SpecID", specializationId);
                            updateCmd.Parameters.AddWithValue("@DocID", doctorId.Value);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertSql = "INSERT INTO Doctors (UserID, SpecializationID) VALUES (@UID, @SpecID)";
                        using (var insertCmd = new Microsoft.Data.SqlClient.SqlCommand(insertSql, polaczenie))
                        {
                            insertCmd.Parameters.AddWithValue("@UID", userId);
                            insertCmd.Parameters.AddWithValue("@SpecID", specializationId);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas przypisywania specjalizacji: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}