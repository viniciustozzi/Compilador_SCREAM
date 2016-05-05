using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Atribuicao : Instrucao
    {
        public string NomeVariavel { get; set; }
        public string Valor { get; set; }

        public override string ToCode()
        {
            return NomeVariavel + " = " + Valor + ";";
        }
    }
}
