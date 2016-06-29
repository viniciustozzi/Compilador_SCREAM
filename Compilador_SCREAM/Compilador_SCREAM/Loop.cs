using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Loop : Instrucao
    {
        /// <summary>
        /// Number of iterations that this loop will execute
        /// </summary>
        public string Number { get; set; }

        public Bloco bloco { get; set; }

        public Loop(string numberOfIterations, Bloco blocoInstrucoes)
        {
            Number = numberOfIterations;
            bloco = blocoInstrucoes;
        }

        public override string ToCode()
        {
            return "int i = 0;\n for (i = 0; i < " + Number + "; i++)" + bloco.ToCode(); 
        }
    }
}
