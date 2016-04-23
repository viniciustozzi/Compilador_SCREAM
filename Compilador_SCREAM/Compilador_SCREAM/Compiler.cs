using Compiler.Core;
using Compiler.Core.SyntacticAnalysers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_SCREAM
{
    public class Compiler
    {
        private Grammar grammar;
        private LexicalAnalyser lexicalAnalyser;
        private LR1Analyser lalrAnalyser;
        private SyntacticAnalyser syntacticAnalyser;

        public DerivationTree DerivationTree { get; private set; }
        public List<Token> Tokens { get; private set; }

        /// <summary>
        /// Inicializa o analisador léxico.
        /// </summary>
        public Compiler()
        {
            grammar = new Grammar();
            ConfigLexicalAnalyser();
            ConfigSyntacticAnalyser();
        }

        /// <summary>
        /// Cria tabela de análise léxica.
        /// </summary>
        private void ConfigLexicalAnalyser()
        {
            //Main
            grammar.AddTokenType("main", "[M][A][I][N]");
            grammar.AddTokenType("endmain", "[E][N][D][_][M][A][I][N]");

            //Tipos de variáveis
            grammar.AddTokenType("int", "[I][N][T]");
            grammar.AddTokenType("float", "[F][L][O][A][T]");
            grammar.AddTokenType("char", "[C][H][A][R]");
            grammar.AddTokenType("bool", "[B][O][O][L]");
            grammar.AddTokenType("string", "[S][T][R][I][N][G]");
            grammar.AddTokenType("array", "[A][R][R][A][Y]");

            //Repetição
            grammar.AddTokenType("while", "[W][H][I][L][E]");
            grammar.AddTokenType("endwhile", "[E][N][D][_][W][H][I][L][E]");
            grammar.AddTokenType("loop", "[L][O][O][P]");
            grammar.AddTokenType("endloop", "[E][N][D][_][L][O][O][P]");
            grammar.AddTokenType("foreach", "[F][O][R][E][A][C][H]");
            grammar.AddTokenType("endforeach", "[E][N][D][_][F][O][R][E][A][C][H]");

            //Condicional
            grammar.AddTokenType("if", "[I][F]");
            grammar.AddTokenType("elseif", "[E][L][S][E][I][F]");
            grammar.AddTokenType("else", "[E][L][S][E]");
            grammar.AddTokenType("endif", "[E][N][D][_][I][F]");

            //Função
            grammar.AddTokenType("funcao", "[_][a-zA-Z0-9]+[a-zA-Z0-9]*");
            grammar.AddTokenType("end", "[E][N][D]");

            //Array
            grammar.AddTokenType("insert", "[I][N][S][E][R][T]");
            grammar.AddTokenType("delete", "[D][E][L][E][T][E]");

            //Operadores Relacionais
            grammar.AddTokenType("igual", "[E][Q]");
            grammar.AddTokenType("diferente", "[D][I][F]");
            grammar.AddTokenType("maiorigual", "[G][E]");
            grammar.AddTokenType("menorigual", "[L][E]");
            grammar.AddTokenType("maior", "[G]");
            grammar.AddTokenType("menor", "[L]");

            //Operadores aritméticos
            grammar.AddTokenType("add", "([A][D][D])|[+]");
            grammar.AddTokenType("sub", "([S][U][B])|[-]");
            grammar.AddTokenType("mul", "([M][U][L])|[*]");
            grammar.AddTokenType("div", "([D][I][V])|[/]");

            //Operadores lógicos

            grammar.AddTokenType("or", "[O][R]");
            grammar.AddTokenType("and", "[A][N][D]");
            grammar.AddTokenType("not", "[N][O][T]");

            //Coisas
            grammar.AddTokenType("{", "[{]");
            grammar.AddTokenType("}", "[}]");
            grammar.AddTokenType("(", "[(]");
            grammar.AddTokenType(")", "[)]");
            grammar.AddTokenType("=", "[=]");
            grammar.AddTokenType(";", "[;]");
            grammar.AddTokenType(".", "[.]");
            grammar.AddTokenType(",", "[,]");


            //Padrão de valores
            grammar.AddTokenType("boolon", "[O][N]");
            grammar.AddTokenType("booloff", "[O][F][F]");
            grammar.AddTokenType("letra", "[\"][a-zA-Z][\"]");
            grammar.AddTokenType("numerofloat", "[0-9]+[.][0-9]+");
            grammar.AddTokenType("numero", "[0-9]+");
            grammar.AddTokenType("variavel", "[a-zA-Z]+[a-zA-Z0-9]*");
            grammar.AddTokenType("palavra", "[#].*[#]");


            //grammar.AddTokenType("Operador", "[+]|[-]|[*]|[/]|[>]|[<]");
            lexicalAnalyser = new LexicalAnalyser(grammar);
            lexicalAnalyser.ParsingData += lexicalAnalyser_ParsingData;
        }

        /// <summary>
        /// Constroi o analisador sintático.
        /// </summary>
        private void ConfigSyntacticAnalyser()
        {
            grammar.AddVariable("initial");
            grammar.AddVariable("S");
            grammar.AddVariable("MAIN");
            grammar.AddVariable("BLOCO");

            grammar.AddVariable("VARTYPE");

            #region Variáveis Declaração
            //Declaração
            grammar.AddVariable("DEC");
            grammar.AddVariable("DECINT");
            grammar.AddVariable("DECFLOAT");
            grammar.AddVariable("DECCHAR");
            grammar.AddVariable("DECBOOL");
            grammar.AddVariable("DECSTRING");
            #endregion

            #region Variáveis Atribuicao
            //Atribuição
            grammar.AddVariable("ATRIB");
            grammar.AddVariable("ATRIBINT");
            grammar.AddVariable("ATRIBFLOAT");
            grammar.AddVariable("ATRIBBOOL");
            grammar.AddVariable("ATRIBCHAR");
            grammar.AddVariable("ATRIBSTRING");
            grammar.AddVariable("ATRIBVAR");

            #endregion

            #region Variáveis de operação algébrica
            grammar.AddVariable("OPTYPE");
            grammar.AddVariable("OP");
            grammar.AddVariable("OPAUX");
            #endregion

            #region Variáveis operações booleanas

            grammar.AddVariable("OPBOOLTYPE");
            grammar.AddVariable("OPBOOLIN");
            grammar.AddVariable("OPBOOLCOND");
            grammar.AddVariable("OPBOOL");
            grammar.AddVariable("OPBOOLAUX");

            #endregion

            #region Variáveis condicionais

            grammar.AddVariable("IF");
            grammar.AddVariable("ELSEIF");
            grammar.AddVariable("ELSE");

            #endregion

            #region Variáveis Repetição

            grammar.AddVariable("WHILE");
            grammar.AddVariable("LOOP");

            #endregion

            #region Variáveis Função

            grammar.AddVariable("PARAM");
            grammar.AddVariable("RETURN");
            grammar.AddVariable("ATRIBFUNC");
            grammar.AddVariable("CHAMFUNC");
            grammar.AddVariable("SENDPARAM");
            grammar.AddVariable("FUNCAO");

            #endregion

            grammar.AddRule("initial", "S");

            grammar.AddRule("S", "MAIN");

            #region Regras Declaração

            // Tipo de Variáveis
            grammar.AddRule("VARTYPE", "int");
            grammar.AddRule("VARTYPE", "float");
            grammar.AddRule("VARTYPE", "char");
            grammar.AddRule("VARTYPE", "bool");
            grammar.AddRule("VARTYPE", "string");


            // Declaração
            grammar.AddRule("DEC", "VARTYPE variavel ;");
            #endregion

            #region Regras Atribuição
            //Atribuição
            grammar.AddRule("ATRIB", "variavel = ATRIBVAR ;");
            grammar.AddRule("ATRIBINT", "numero");
            grammar.AddRule("ATRIBFLOAT", "numerofloat");
            grammar.AddRule("ATRIBBOOL", "boolon");
            grammar.AddRule("ATRIBBOOL", "booloff");
            grammar.AddRule("ATRIBCHAR", "letra");
            grammar.AddRule("ATRIBSTRING", "palavra");

            grammar.AddRule("ATRIBVAR", "ATRIBINT");
            grammar.AddRule("ATRIBVAR", "ATRIBFLOAT");
            grammar.AddRule("ATRIBVAR", "ATRIBBOOL");
            grammar.AddRule("ATRIBVAR", "ATRIBCHAR");
            grammar.AddRule("ATRIBVAR", "ATRIBSTRING");
            #endregion

            #region Regras operação algébrica

            grammar.AddRule("OPTYPE", "add");
            grammar.AddRule("OPTYPE", "sub");
            grammar.AddRule("OPTYPE", "mul");
            grammar.AddRule("OPTYPE", "div");

            grammar.AddRule("OP", "variavel = OP ;");
            grammar.AddRule("OP", "numero OPAUX ");
            grammar.AddRule("OP", "numero ");
            grammar.AddRule("OP", "numerofloat OPAUX ");
            grammar.AddRule("OP", "numerofloat ");
            grammar.AddRule("OP", "variavel OPAUX ");
            grammar.AddRule("OP", "variavel ");
            grammar.AddRule("OP", "( OP ) OPAUX ");
            grammar.AddRule("OP", "( OP ) ");

            grammar.AddRule("OPAUX", "OPTYPE OP");

            #endregion

            #region Regras operações booleanas

            grammar.AddRule("OPBOOLTYPE", "igual");
            grammar.AddRule("OPBOOLTYPE", "diferente");
            grammar.AddRule("OPBOOLTYPE", "maiorigual");
            grammar.AddRule("OPBOOLTYPE", "menorigual");
            grammar.AddRule("OPBOOLTYPE", "maior");
            grammar.AddRule("OPBOOLTYPE", "menor");

            grammar.AddRule("OPBOOLIN", "numero");
            grammar.AddRule("OPBOOLIN", "numerofloat");
            grammar.AddRule("OPBOOLIN", "variavel");

            grammar.AddRule("OPBOOLCOND", "and");
            grammar.AddRule("OPBOOLCOND", "or");

            grammar.AddRule("OPBOOL", "OPBOOLIN");
            grammar.AddRule("OPBOOL", "OPBOOLIN OPBOOLTYPE OPBOOLIN");
            grammar.AddRule("OPBOOL", "OPBOOL OPBOOLAUX");
            grammar.AddRule("OPBOOL", "not OPBOOL");

            grammar.AddRule("OPBOOLAUX", "OPBOOLCOND OPBOOL");

            #endregion

            #region Regras Condicional

            grammar.AddRule("IF", " if ( OPBOOL ) BLOCO endif ELSEIF");
            grammar.AddRule("IF", " if ( OPBOOL ) BLOCO endif ELSE");
            grammar.AddRule("IF", " if ( OPBOOL ) BLOCO endif");

            grammar.AddRule("ELSEIF", " elseif ( OPBOOL ) BLOCO endif ELSEIF");
            grammar.AddRule("ELSEIF", " elseif ( OPBOOL ) BLOCO endif ELSE");
            grammar.AddRule("ELSEIF", " elseif ( OPBOOL ) BLOCO endif");

            grammar.AddRule("ELSE", " else BLOCO endif");

            #endregion

            #region Regras Repetição

            grammar.AddRule("WHILE", "while ( OPBOOL ) BLOCO endwhile");

            grammar.AddRule("LOOP", "loop ( numero ) BLOCO endloop");

            #endregion

            #region Regras Função

            grammar.AddRule("PARAM", "VARTYPE variavel PARAM");
            grammar.AddRule("PARAM", "VARTYPE variavel");

            grammar.AddRule("RETURN", " = variavel ;");
            grammar.AddRule("RETURN", " = numero ;");

            grammar.AddRule("ATRIBFUNC", " variavel = CHAMFUNC");
            grammar.AddRule("CHAMFUNC", "funcao ( SENDPARAM )");
            grammar.AddRule("SENDPARAM", "variavel , SENDPARAM ");
            grammar.AddRule("SENDPARAM", "variavel");

            grammar.AddRule("FUNCAO", "VARTYPE funcao { PARAM } BLOCO end FUNCAO");
            grammar.AddRule("FUNCAO", "VARTYPE funcao { PARAM } BLOCO end");
            #endregion

            #region Regras Bloco

            grammar.AddRule("BLOCO", "ATRIB");
            grammar.AddRule("BLOCO", "ATRIB BLOCO");
            grammar.AddRule("BLOCO", "DEC BLOCO");
            grammar.AddRule("BLOCO", "DEC");
            grammar.AddRule("BLOCO", "WHILE BLOCO");
            grammar.AddRule("BLOCO", "WHILE");
            grammar.AddRule("BLOCO", "IF BLOCO");
            grammar.AddRule("BLOCO", "IF");
            grammar.AddRule("BLOCO", "LOOP BLOCO");
            grammar.AddRule("BLOCO", "LOOP");
            grammar.AddRule("BLOCO", "CHAMFUNC BLOCO");
            grammar.AddRule("BLOCO", "CHAMFUNC");
            grammar.AddRule("BLOCO", "RETURN");

            #endregion

            grammar.AddRule("MAIN", "main { } BLOCO endmain FUNCAO");
            grammar.AddRule("MAIN", "main { } BLOCO endmain");

            // Programa

            // Bloco de código
            //grammar.AddRule("P", "{ Q }");
            //grammar.AddRule("Q", "C Q");
            //grammar.AddRule("Q", "B Q");
            //grammar.AddRule("Q", "");
            // Atribuição
            //grammar.AddRule("B", "var = A");
            // Expressão
            //grammar.AddRule("A", "Numeros");
            //grammar.AddRule("A", "A T");
            //grammar.AddRule("T", "Operador A");
            // If
            //grammar.AddRule("C", "se ( A ) P");
            syntacticAnalyser = new SyntacticAnalyser(grammar);
            // Calcula a tabela nullable..
            var nullable = syntacticAnalyser.Nullable();
            var first = syntacticAnalyser.First(nullable);
            // Cria o analisador LR1..
            lalrAnalyser = new LR1Analyser(grammar, first, null, null);
            lalrAnalyser.Compute();
        }

        /// <summary>
        /// Realiza o parse dos dados.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        void lexicalAnalyser_ParsingData(string input, List<string> output)
        {
            //string[] words = input.Split(new char[] { ' ', '+', '*', '-', '/', '=', ',', '.', ';', '(', ')', '{', '}', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string word = "";
            int hashtag = 0;
            bool lastIsNum = false;
            foreach (char c in input)
            {
                if (c == '#')
                    hashtag++;

                if (hashtag % 2 == 0)
                {
                    if (c == '+' || c == '-' || c == '/' || c == '=' || c == ',' || c == ';' || c == '(' || c == ')' || c == '{' || c == '}')
                    {
                        if (word != "")
                            output.Add(word);
                        word = "";
                        word += c;
                        output.Add(word);
                        word = "";
                    }

                    else if (c == '.')
                    {
                        if (!lastIsNum)
                        {
                            if (word != "")
                                output.Add(word);
                            word = "";
                            word += c;
                            output.Add(word);
                            word = "";
                        }

                        else
                        {
                            word += c;
                        }
                    }

                    //else if (c == '_')
                    //{
                    //    if (word != "")
                    //        output.Add(word);
                    //    word = "";
                    //    word += c;
                    //}

                    else if (c == ' ' || c == '\r' || c == '\n' || c == '\t' || c == '\0')
                    {
                        if (word != "")
                        {
                            output.Add(word);
                            word = "";
                        }
                    }

                    else
                    {
                        word += c;
                    }

                    lastIsNum = false;

                    if (c >= '0' && c <= '9')
                    {
                        lastIsNum = true;
                    }
                }

                else
                {
                    word += c;
                }

            }
            output.Add(word);
        }

        /// <summary>
        /// Compila o código gerado.
        /// </summary>
        /// <param name="code"></param>
        public bool Compile(string code)
        {
            Tokens = lexicalAnalyser.Parse(code);
            return CheckGrammar();
        }

        /// <summary>
        /// Analisa a gramática utilizando a análise LALR.
        /// </summary>
        private bool CheckGrammar()
        {
            bool accepted = false;
            List<StackMovement> stackMovements = new List<StackMovement>();
            // Inicia a validação da gramática..
            StackMovement sm = lalrAnalyser.StartAnalysis(Tokens);
            // Executa enquanto houver dados..
            while (sm != null)
            {
                stackMovements.Add(sm);
                // Verifica se a linguagem foi aceita..
                if (sm.Movement == MovementType.Accept)
                {
                    accepted = true;
                }
                // Executa o próximo passo.
                sm = lalrAnalyser.PerformStep(sm);
            }
            // Cria árvore de derivação..
            DerivationTree = new DerivationTree(AnalyserStrategy.BottomUp);
            DerivationTree.AddMovements(stackMovements);
            return accepted;
        }
    }
}
