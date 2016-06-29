using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class ProgTotal : Instrucao
    {
        public List<Instrucao> Instrucoes { get; set; }

        public override string ToCode()
        {
            string program = string.Empty;

            foreach (var item in Instrucoes)
            {
                program += item.ToCode();
            }

            return program;
        }
    }
}
