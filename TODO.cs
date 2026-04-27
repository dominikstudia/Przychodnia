using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia
{
    internal class TODO
    {

        // TA KLASA SŁUZY DO ZAPISANIA TEGO, CO MAMY ZROBIĆ

        // * BLOKADA KONKRETNEGO KONTA NA OKREŚLONY CZAS PRZY 3 NIEUDANYCH PRÓBACH LOGOWANIA [np. konto jest zablokowane na 15 minut ze względu na pomylke hasla 3 krotnie]
        // * ^ DO TEGO POWINNA BYĆ PODPIĘTA BAZA DANYCH [zablokowani_uzytkownicy] gdzie przechowujemy login i czas do kiedy jest zablokowane konto

        // * Po resecie hasła przez administratora, bądź po wysyłce nowego hasła na e-mail po zalogowaniu powinnien być wyświetlone panel ZmianaHasla.cs [bez przycisku anuluj]

        // * Aktualizacja bazy danych - stwórzcie folder resources i dajcie tam bazadanych.sql, bo ja mam chyba przestarzałą baze danych i nie wszystko zapisuje

        // * Wysyłka maili - po założeniu konta login i hasło wysyła sie na email
        // * Dodać przycisk do resetu hasła [LogowaniePanel.cs] - po podaniu emaila wysyła na e-mail kod, który wpisujemy, żeby zrestartowac hasło.



        // TODO NIEZBYT PILNE
        // * Hashowanie haseł
    }
}
