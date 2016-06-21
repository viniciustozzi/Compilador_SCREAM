using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Declaracao : Instrucao
    {
        /// <summary>
        /// Tipo da variável a ser declarada
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Nome da variável a ser declarada
        /// </summary>
        public string Nome { get; set; }
        
        public override string ToCode()
        {
            return Tipo + " " + Nome + ";";
        }
    }
}
