using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Else : Instrucao
    {
        public Bloco bloco { get; set; }

        public override string ToCode()
        {
            return "else" + bloco.ToCode();
        }
    }
}
