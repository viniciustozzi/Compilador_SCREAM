using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Declaracao : Instrucao
    {
        public string Tipo { get; set; }
        public string NomeVariavel { get; set; }

        public override string ToCode()
        {
            return Tipo + " " + NomeVariavel + ";";
        }
    }
}
