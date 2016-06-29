using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Function : Instrucao
    {
        public string Tipo { get; set; }
        public Bloco bloco { get; set; }
        public string NomeFuncao { get; set; }
        public List<ParameterInfo> parameters { get; set; }

        public Function(string tipoRetorno, Bloco blocoInstrucoes, string nomeFuncao, List<ParameterInfo> parametros)
        {
            Tipo = tipoRetorno;
            bloco = blocoInstrucoes;
            NomeFuncao = nomeFuncao;
            parameters = parametros;
        }

        public override string ToCode()
        {
            string resultFunction = string.Empty;

            resultFunction = Tipo + " " + NomeFuncao + "(";

            for (int i = 0; i < parameters.Count; i++)
            {
                resultFunction += parameters[i].Type + " " + parameters[i].Value;

                //Se não for o ultimo parametro, deve ter uma vírgula em seguida
                if (i != parameters.Count - 1)
                    resultFunction += ",";
            }

            //Bloco deve ter retorno especificado
            resultFunction += ")" + bloco.ToCode();

            return resultFunction;
        }
    }

    public class ParameterInfo
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
