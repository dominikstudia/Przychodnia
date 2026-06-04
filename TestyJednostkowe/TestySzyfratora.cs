using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Przychodnia.TestyJednostkowe
{
    public class TestySzyfratora
    {
        private readonly string JAWNE_SCHORZENIE = "Ostre zapalenie gardła, pacjent skarży się na ból XYZ";
        private readonly string ZASZYFROWANE_SCHORZENIE = "sUR7lzDGT8QoAJLXL8WsIb7TzU1/XOOVFjgfssZAvGouHhfYOAIz7Hv0Wx4p7kQskyLUGBnsvSGKyFM0hmAqag==";

        [Fact]
        public void SzyfrujDaneMedyczneZwracaTenSamZabepieczonySzyfr()
        {
            string wynik = Szyfrator.Szyfruj(JAWNE_SCHORZENIE);
            string wynik2 = Szyfrator.Szyfruj(JAWNE_SCHORZENIE);
            Assert.Equal(wynik2, wynik);
        }

        [Fact]
        public void SzyfrujDaneMedyczneZwracaStalyZabepieczonySzyfr()
        {
            string wynik = Szyfrator.Szyfruj(JAWNE_SCHORZENIE);
            Assert.Equal(ZASZYFROWANE_SCHORZENIE, wynik);
        }

        [Fact]
        public void SzyfrujDaneMedyczneZwracaBlednyZabepieczonySzyfr()
        {
            string wynik = Szyfrator.Szyfruj(JAWNE_SCHORZENIE + " test");
            Assert.NotEqual(ZASZYFROWANE_SCHORZENIE, wynik);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void SzyfrujBrakHaslaZwracaToCoWyslano(string? haslo)
        {
            string wynik = Szyfrator.Szyfruj(haslo);
            Assert.Equal(haslo, wynik);
        }

        [Fact]
        public void OdszyfrujDaneMedyczneZwracaCzytelnyTekst()
        {
            string wynik = Szyfrator.Odszyfruj(ZASZYFROWANE_SCHORZENIE);
            Assert.Equal(JAWNE_SCHORZENIE, wynik);
        }

        [Fact]
        public void OdszyfrujDaneMedyczneZwracaBlednieCzytelnyTekst()
        {
            string wynik = Szyfrator.Odszyfruj(ZASZYFROWANE_SCHORZENIE);
            Assert.NotEqual(JAWNE_SCHORZENIE + " test", wynik);
        }

        [Fact]
        public void OdszyfrujDaneMedyczneBledneZaszyfrowanie()
        {
            string tekst = "niepoprawnie_zaszyfrowany_tekst";
            string wynik = Szyfrator.Odszyfruj(tekst);
            Assert.Equal(tekst, wynik);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void OdszyfrujBrakHaslaZwracaToCoWyslano(string? haslo)
        {
            string wynik = Szyfrator.Odszyfruj(haslo);
            Assert.Equal(haslo, wynik);
        }
    }
}
