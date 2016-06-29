using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class While : Instrucao
    {
        public Expressao expressao { get; set; }
        public Bloco bloco { get; set; }

        public While(Expressao expressaoBooleana, Bloco blocoInstrucoes)
        {
            expressao = expressaoBooleana;
            bloco = blocoInstrucoes;
        }

        public override string ToCode()
        {
            return "while (" + expressao.ToCode() + ") " + bloco.ToCode();
        }
    }
}
