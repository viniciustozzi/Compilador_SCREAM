using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Expressao : Instrucao
    {
        public string Variavel_1 { get; set; }
        public string Variavel_2 { get; set; }
        public string Operador { get; set; }

        public override string ToCode()
        {
            return Variavel_1 + " " + Operador + " " + Variavel_1 + ";";
        }
    }
}
