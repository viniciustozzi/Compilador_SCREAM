using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class If : Instrucao
    {
        public Expressao expressao { get; set; }
        public Bloco bloco { get; set; }
        public If otherIf { get; set; }

        public If(Expressao expressaoBooleana, Bloco blocoInstrucoes, If otherIf)
        {
            expressao = expressaoBooleana;
            bloco = blocoInstrucoes;
            this.otherIf = otherIf;
        }

        public override string ToCode()
        {
            string v = "";
            
            v += "if (" + expressao.ToCode() + ")\n" + bloco.ToCode();

            if (otherIf != null)
            {
                v += otherIf.ToCode();
            }

            return v;
        }
    }
}