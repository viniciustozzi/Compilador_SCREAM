using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Loop : Instrucao
    {
        public string NumeroIteracoes { get; set; }

        public override string ToCode()
        {
            return
            "for (int i = 0; i < " + 
        }
    }
}
