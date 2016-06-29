using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    class Atribuicao : Instrucao
    {
        public string NomeVariavel { get; set; }
        public string Valor { get; set; }

        public Atribuicao(string nomeVariavel, string valor)
        {
            this.NomeVariavel = nomeVariavel;
            this.Valor = valor;
        }

        public override string ToCode()
        {
            return NomeVariavel + " = " + Valor + ";\n";
        }
    }
}
