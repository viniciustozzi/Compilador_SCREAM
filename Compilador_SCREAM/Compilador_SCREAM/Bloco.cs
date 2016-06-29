using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Bloco : Instrucao
    {
        /// <summary>
        /// Lista de instruções que estão dentro desse bloco
        /// </summary>
        public List<Instrucao> Instrucoes { get; set; }
        
        /// <summary>
        //Se o bloco tiver retorno como na main ou função, preencher este valor
        /// </summary>
        public string Retorno { get; set; }

        public Bloco(List<Instrucao> listInstrucoes)
        {
            Instrucoes = listInstrucoes;
        }

        public override string ToCode()
        {
            string blocoResult = "{ ";

            foreach (Instrucao inst in Instrucoes)
            {
                blocoResult += inst.ToCode();
            }

            //if (Retorno != string.Empty)
            //    blocoResult += "return " + Retorno + ";";

            blocoResult += " }";

            return blocoResult;
        }
    }
}
