using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia
{
    public class Rola
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }

        public override string ToString() => Nazwa;
    }
}