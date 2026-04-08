using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Przychodnia
{
    internal class BazaDanych
    {
        public static readonly string POLACZENIE_STRING = @"Server=KAKUR\SQLEXPRESS01;Database=Przychodnia;Trusted_Connection=True;TrustServerCertificate=True;";

        public static BindingList<Uzytkownik> Uzytkownicy { get; set; } = new BindingList<Uzytkownik>();

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
                        LEFT JOIN Patients p ON u.UserID = p.UserID";

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
                                CzyMezczyzna = czytnik["Gender"]?.ToString() == "M",
                                DataUrodzenia = czytnik["BirthDate"] != DBNull.Value ? Convert.ToDateTime(czytnik["BirthDate"]) : DateTime.Now,
                                Telefon = czytnik["Phone"].ToString(),
                                Miejscowosc = czytnik["City"].ToString(),
                                Ulica = czytnik["Street"].ToString(),
                                KodPocztowy = czytnik["PostalCode"].ToString(),
                                NumerPosesji = czytnik["HouseNumber"].ToString(),
                                NumerLokalu = czytnik["ApartmentNumber"].ToString(),
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
                MessageBox.Show("Błąd bazy: " + ex.Message);
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
                            // Dodajemy PasswordHash do INSERT
                            string sqlUser = @"INSERT INTO Users (Login, FirstName, LastName, Email, Phone, IsArchived, CreatedAt, PasswordHash) 
                                     OUTPUT INSERTED.UserID
                                     VALUES (@Login, @FirstName, @LastName, @Email, @Phone, @IsArchived, GETDATE(), @Pass)";

                            int nowyId;
                            using (var cmdUser = new Microsoft.Data.SqlClient.SqlCommand(sqlUser, polaczenie, transakcja))
                            {
                                cmdUser.Parameters.AddWithValue("@Login", uzytkownik.Login ?? "");
                                cmdUser.Parameters.AddWithValue("@FirstName", uzytkownik.Imiona ?? "");
                                cmdUser.Parameters.AddWithValue("@LastName", uzytkownik.Nazwisko ?? "");
                                cmdUser.Parameters.AddWithValue("@Email", uzytkownik.Email ?? "");
                                cmdUser.Parameters.AddWithValue("@Phone", uzytkownik.Telefon ?? "");
                                cmdUser.Parameters.AddWithValue("@IsArchived", uzytkownik.CzyZarchiwizowany);
                                cmdUser.Parameters.AddWithValue("@Pass", uzytkownik.Haslo ?? ""); // Zapis hasła
                                nowyId = (int)cmdUser.ExecuteScalar();
                            }

                            // ... tutaj sekcja INSERT INTO Patients (bez zmian) ...
                            uzytkownik.Id = nowyId;
                            Uzytkownicy.Add(uzytkownik);
                        }
                        else // EDYCJA
                        {
                            // Dodajemy PasswordHash do UPDATE
                            string updUser = @"UPDATE Users SET FirstName=@FN, LastName=@LN, Email=@Em, 
                                      Phone=@Ph, IsArchived=@Arc, PasswordHash=@Pass WHERE UserID=@Id";
                            using (var cmd = new Microsoft.Data.SqlClient.SqlCommand(updUser, polaczenie, transakcja))
                            {
                                cmd.Parameters.AddWithValue("@Id", uzytkownik.Id);
                                cmd.Parameters.AddWithValue("@FN", uzytkownik.Imiona);
                                cmd.Parameters.AddWithValue("@LN", uzytkownik.Nazwisko);
                                cmd.Parameters.AddWithValue("@Em", uzytkownik.Email);
                                cmd.Parameters.AddWithValue("@Ph", uzytkownik.Telefon);
                                cmd.Parameters.AddWithValue("@Arc", uzytkownik.CzyZarchiwizowany);
                                cmd.Parameters.AddWithValue("@Pass", uzytkownik.Haslo ?? ""); // Aktualizacja hasła
                                cmd.ExecuteNonQuery();
                            }
                        }
                        // 1. Najpierw usuwamy wszystkie stare przypisania ról dla tego użytkownika
                        string usunRole = "DELETE FROM UserRoles WHERE UserID=@Id";
                        using (var cmdUsun = new Microsoft.Data.SqlClient.SqlCommand(usunRole, polaczenie, transakcja))
                        {
                            cmdUsun.Parameters.AddWithValue("@Id", uzytkownik.Id);
                            cmdUsun.ExecuteNonQuery();
                        }

                        // 2. Następnie przypisujemy nowe role (tylko jeśli konto jest aktywne)
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
    }
}