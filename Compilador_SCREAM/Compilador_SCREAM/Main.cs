using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Main : Instrucao
    {
        /// <summary>
        /// Para a main, o bloco deve ter retorno especificado (inteiro)
        /// </summary>
        public Bloco bloco { get; set; }
        
        /// <summary>
        /// Bloco de instrucoes pertencentes a main
        /// </summary>
        /// <param name="blocoMain"></param>
        public Main(Bloco blocoMain)
        {
            bloco = blocoMain;
        }

        public override string ToCode()
        {
            return "int main()" + bloco.ToCode();
        }
    }
}
