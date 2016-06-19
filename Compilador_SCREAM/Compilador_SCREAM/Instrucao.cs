using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    /// <summary>
    /// Retorna código em C equivalente
    /// </summary>
    public abstract class Instrucao
    {
        public abstract string ToCode();
    }
}
