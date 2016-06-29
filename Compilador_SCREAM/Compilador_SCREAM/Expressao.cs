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

        public Expressao(string var1, string var2, string operador)
        {
            Variavel_1 = var1;
            Variavel_2 = var2;
            Operador = operador;
        }

        public override string ToCode()
        {
            return Variavel_1 + " " + Operador + " " + Variavel_1;
        }
    }
}
