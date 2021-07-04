using GraphAppends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphing
{

    internal static class GraphExpressionProcesses
    {
        internal static Graph ExpressionProcessing()
        {
            var expression = GetExpression();

            int start = new int(), end = new int();

            GetStartEnd(out start, out end);

            return new Graph() { Expression = expression, Start = start, End = end };
        }

        /// <summary>
        /// Pega a expressão
        /// </summary>
        /// <returns>Expressão separada por membros, em forma de inteiros</returns>
        private static int[,] GetExpression()
        {
            while (true)
            {
                //Pega a expressão e converte para letras minúsculas
                Console.Write("Digite a expressao: ");
                string exp = Console.ReadLine().ToLower();

                //Remove espaços
                exp = exp.Replace(" ", "");

                if (exp.Length == 0)
                    continue;

                //Processa expressão
                var Expression = GetProcessedExpression(GetPolinomialMemberList(exp));

                if (Expression.Length != 0)
                    return Expression;
            }
        }

        #region Polinomial Member List Method Properties

        /// <value>Sinal do membro sendo processado</value>
        private static char Sign;

        /// <value>Coeficiente do membro sendo processado</value>
        private static string Coef;

        /// <value>Expoente do membro sendo processado</value>
        private static char Exp;

        #endregion

        #region

        /// <summary>
        /// Processa a expressão dada
        /// </summary>
        /// <param name="expression">String original</param>
        /// <returns>Lista de membros</returns>
        private static List<PolinomialMem> GetPolinomialMemberList(string expression)
        {
            var VS = new List<PolinomialMem>();

            Reset();

            // Chars usados como atual e próximo
            char ch, next;
            ch = next = new char();

            //Define primeiro carácter na expressão como + ou o mesmo (caso seja -)
            expression = "+-".Any(c => c == expression[0]) ? expression : '+' + expression;

            //Processa expressão
            for (int i = 0; i < expression.Length; ++i)
            {
                //Define ch como sendo o carácter atual
                ch = expression[i];

                //Caso o carácter seja um sinal
                if ("+-".Any(c => c == ch))
                {
                    Sign = ch;
                    continue;
                }

                //Caso o carácter seja "x"
                else if (ch == 'x')
                {
                    Coef = Coef == string.Empty ? "1" : Coef;

                    //Checa se existe um próximo carácter
                    if (i < expression.Length - 1)
                    {
                        //Define next como próximo carácter
                        next = expression[i + 1];

                        if ("+-".Any(c => c == next))
                            Exp = '1';
                        else
                            Exp = next;

                        VS.Add(new PolinomialMem(Sign, Coef, Exp));
                        Reset();

                    }
                    else
                        VS.Add(new PolinomialMem(Sign, Coef, '1'));

                    continue;
                }

                //Caso o carácter seja um número

                //Checa se existe um próximo carácter
                if (i < expression.Length - 1)
                {
                    next = expression[i + 1];

                    if ("+-".Any(c => c == next))
                        Exp = ch;
                    else
                        Coef += ch;

                    continue;
                }

                //Se Sign estiver vazio, o número só pode ser um expoente de uma rodada anterior
                if (Sign != '\0')
                {
                    Coef += ch;
                    Exp = '0';
                    VS.Add(new PolinomialMem(Sign, Coef, Exp));
                }
            }

            return VS;
        }

        /// <summary>
        /// Reseta as propriedades estáticas relativas ao membro atual
        /// </summary>
        private static void Reset()
        {
            Sign = Exp = new char();
            Coef = string.Empty;
        }

        #endregion

        private static int[,] GetProcessedExpression(this List<PolinomialMem> VS)
        {
            //Organiza a lista por expoente
            VS = VS.OrderByDescending(x => x.Exponent).ToList();

            //Pega o grau do polinomio baseado no maior expoente (na posição 0)
            int degree = Convert.ToInt32(VS[0].Exponent.ToString());

            //Define o comprimento do array baseado no grau do polinomio (+1 por conta do índice baseado em 0)
            var vals = new int[degree + 1, 2];

            //Conta o membro atual
            var count = 0;

            //Preenche lista VS
            for (int currentDeg = 0; currentDeg <= degree; currentDeg++)
            {
                //Checa se count não ultrapassou o comprimento de VS
                if (count < VS.Count)
                {
                    //Define c para a posição atual de VS
                    var currentElement = VS[count];

                    //Pega o índice atual de VS
                    var index = Convert.ToInt32(currentElement.Exponent.ToString());

                    //Checa se o índice atual de VS corresponde ao expoente atual da lista vals
                    if (index == degree - currentDeg)
                    {
                        ++count;

                        vals[currentDeg, 0] = int.Parse(currentElement.Sign + currentElement.Coefficient);
                        vals[currentDeg, 1] = index;

                        continue;
                    }
                }

                vals[currentDeg, 0] = 0;
                vals[currentDeg, 1] = degree - currentDeg;
            }

            return vals;
        }

        /// <summary>
        /// Pega o começo e o fim do dominio
        /// </summary>
        /// <param name="Expression"></param>
        private static void GetStartEnd(out int start, out int end)
        {
            do
            {
                //Pega comprimento do dominio desejado
                Console.Write("Digite o alcance da funcao: ");
                string[] rangevar = Console.ReadLine().Split(' ');

                start = Convert.ToInt32(rangevar[0]);
                end = Convert.ToInt32(rangevar[1]);
            }
            while (start > end);
        }
    }

}
