using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attestant
{
    internal class WordSet
    {
        public string OldIrish;
        public string MiddleWelsh;
        public string ProtoCeltic;
        public string Reconstructed;

        public WordSet(string OIr, string MW, string PC)
        {
            OldIrish = OIr;
            MiddleWelsh = MW;
            ProtoCeltic = PC;
        }

        public bool Equals {
            get {
                return ProtoCeltic == Reconstructed;
            }
        }
    }
}
