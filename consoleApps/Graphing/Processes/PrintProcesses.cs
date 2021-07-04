using GraphAppends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphing
{
    internal static class PrintProcesses
    {
        public static void Printing(this Graph graph)
        {
            List<Val> vals = GetValsList(graph.Start, graph.End, graph.Expression);

            //Calcula valores de imagem do começo ao final do domínio dado

            //Organiza todos os valores em ordem decrescente
            vals = vals.OrderByDescending(x => x.Yv).ThenBy(x => x.Xv).ToList();

            //Define valor máximo, mínimo e comprimento do intervalo
            var maxVal = vals[0].Yv;
            var minVal = vals.Last().Yv;
            var imageInterval = maxVal - minVal + 1;

            //Define o comprimento do valor de imagem mais longo
            var imMaxValSize = minVal.ToString().Length > maxVal.ToString().Length ? minVal.ToString().Length : maxVal.ToString().Length;

            var linesList = vals.GetLinesList(minVal, maxVal, graph.Start);

            //Define o comprimento do valor mais longo no dominio
            var domMaxValSize = graph.Start.ToString().Length > graph.End.ToString().Length ? graph.Start.ToString().Length : graph.End.ToString().Length;

            linesList.PrintGraph(imageInterval, imMaxValSize, domMaxValSize);

            PrintXAxis(graph.Start, graph.End, imMaxValSize, domMaxValSize);
        }

        private static List<Val> GetValsList(int start, int end, int[,] expression)
        {
            var vals = new List<Val>();

            for (int x = start; x <= end; x++)
            {
                var y = new double();

                for (int j = 0; j < expression.GetLength(0); ++j)
                    y += (expression[j, 0]) * Math.Pow(x, expression[j, 1]);

                vals.Add(new Val(x, (int)y));
            }

            return vals;
        }

        /// <summary>
        /// Gera Lista de Linhas
        /// </summary>
        /// <param name="vals">Lista de valroes da imagem</param>
        /// <returns>Lista com valores entre pontos (em order crescente)</returns>
        private static List<Line> GetLinesList(this List<Val> vals, int minVal, int maxVal, int start)
        {
            var lines = new List<Line>();

            //Representam linha atual da lista de linhas e valor atual na lista de valores respectivamente
            int curLineCount = 0, curValCount = 0;

            //Pega uma lista com todos os valores de imagem separados pelo seu espaço em relação ao começo do domínio dado
            for (int i = maxVal; i >= minVal; --i)
            {
                //Pega valor atual (em relação ao loop)
                var v = vals[curValCount];

                //Adiciona nova linha à lista
                lines.Add(new Line());

                //Enquanto o valor atual foi igual ao valor atual da lista
                while (v.Yv == i)
                {
                    ++curValCount;

                    //Pega o comprimento da linha atual
                    var CurrentLineLen = lines[curLineCount].Size();

                    //Define intervalo atual para a linha atual da lista 
                    lines[curLineCount][CurrentLineLen] = v.Xv - start;

                    if (curValCount >= vals.Count)
                        break;

                    //Atualiza o valor atual
                    v = vals[curValCount];
                }
                ++curLineCount;
            }

            return lines;
        }

        private static void PrintGraph(this List<Line> linesList, int imageInterval, int imMaxValSize, int domMaxValSize)
        {
            //Imprime o gráfico
            for (int i = 0; i < imageInterval; ++i)
            {
                //Inicializa espaçamento atual
                var space = 0;

                //Imprime valor atual da imagem
                Console.Write($"{imageInterval - (i + 1)}|".PadLeft(imMaxValSize + 1));

                //Inicializa linha atual do gráfico
                var str = string.Empty;

                for (int j = 0; j < linesList[i].Size(); ++j)
                {
                    //Define espaçamento
                    space = linesList[i][j] * (domMaxValSize + 1) - (str.Length - 1);

                    //Preenche * até que alcance o tamanho de space
                    str += "*".PadLeft(space);
                }

                //Imprime a linha atual
                Console.WriteLine(str);
            }

        }

        private static void PrintXAxis(int start, int end, int imMaxValSize, int domMaxValSize)
        {
            //Imprimes espacinho do eixo X para alinhar
            Console.Write("|".PadLeft(imMaxValSize + 1));

            //Imprime valores do eixo X
            for (int i = start; i <= end; i++)
                Console.Write($"{i}|".PadLeft(domMaxValSize + 1));
        }
    }
}
