using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Przychodnia.TestyJednostkowe
{
    public class TestyNarzedzi
    {
        [Theory]
        [InlineData("04260431848", 2004, 6, 4, false)]
        [InlineData("85010157212", 1985, 1, 1, true)]
        public void SprawdzPeselPoprawnyZwracaTrue(string pesel, int rok, int miesiac, int dzien, bool czyMezczyzna)
        {
            var wynik = Narzedzia.SprawdzPesel(pesel, new DateTime(rok, miesiac, dzien), czyMezczyzna, false);
            Assert.True(wynik.Poprawny);
        }

        [Theory]
        [InlineData("Tekst11znak")]
        [InlineData("8501015721")]
        [InlineData("850101572123")]
        public void SprawdzPeselNiepoprawnyFormatZwracaFalse(String pesel)
        {
            var wynik = Narzedzia.SprawdzPesel(pesel, new DateTime(1985, 1, 1), true, false);
            Assert.False(wynik.Poprawny);
        }

        [Theory]
        [InlineData(1990, 1, 1)]
        [InlineData(1985, 2, 1)]
        [InlineData(1985, 1, 3)]
        public void SprawdzPeselNiepoprawnaDataUrodzeniaZwracaFalse(int rok, int miesiac, int dzien)
        {
            var wynik = Narzedzia.SprawdzPesel("85010157212", new DateTime(rok, miesiac, dzien), true, false);
            Assert.False(wynik.Poprawny);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void SprawdzPeselBrakPeseluZwracaFalse(string? pesel)
        {
            var wynik = Narzedzia.SprawdzPesel(pesel, new DateTime(1985, 1, 1), true, false);
            Assert.False(wynik.Poprawny);
        }

        [Fact]
        public void SprawdzPeselNiepoprawnaPlecZwracaFalse()
        {
            var wynik = Narzedzia.SprawdzPesel("85010157212", new DateTime(1985, 1, 1), false, false);
            Assert.False(wynik.Poprawny);
        }

        [Fact]
        public void SprawdzPeselNiepoprawnaSumaKontrolnaZwracaFalse()
        {
            var wynik = Narzedzia.SprawdzPesel("85010157213", new DateTime(1985, 1, 1), false, false);
            Assert.False(wynik.Poprawny);
        }

        //

        [Fact]
        public void GenerujHasloSpelniaWymaganiaBezpieczenstwaBezBledowWalidacji()
        {
            string haslo = Narzedzia.GenerujSilneHaslo(false);
            var wynik = Narzedzia.SprawdzSileHasla(haslo);
            Assert.False(wynik.CzySaBledy);
        }

        [Theory]
        [InlineData("e1^tezpso")]
        [InlineData("E1^TEZPSO")]
        [InlineData("Ek^TezPSO")]
        [InlineData("Ek0TezPSO")]
        public void SprawdzSileHaslaBrakWymaganychZnakowZwracaBlad(string haslo)
        {
            var wynik = Narzedzia.SprawdzSileHasla(haslo);
            Assert.True(wynik.CzySaBledy);
        }

        [Theory]
        [InlineData("e1^TeZ")]
        [InlineData("e1^TeZe1^TeZe1^TeZ")]
        public void SprawdzSileHaslaZlaDlugoscHaslaZwracaBlad(string haslo)
        {
            var wynik = Narzedzia.SprawdzSileHasla(haslo);
            Assert.True(wynik.CzySaBledy);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void SprawdzSileHaslaBrakHaslaZwracaBlad(string? haslo)
        {
            var wynik = Narzedzia.SprawdzSileHasla(haslo);

            Assert.True(wynik.CzySaBledy);
        }

        //

        [Fact]
        public void HashujHasloZwracaPoprawnyHash()
        {
            string hasloJawne = "admin";
            string zahaszowane = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";

            Assert.Equal(zahaszowane, Narzedzia.HashujHaslo(hasloJawne));
        }

        [Fact]
        public void HashujHasloZwracaNiepoprawnyHash()
        {
            string hasloJawne = "adminek";
            string zahaszowane = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";

            Assert.NotEqual(zahaszowane, Narzedzia.HashujHaslo(hasloJawne));
        }
    }
}
