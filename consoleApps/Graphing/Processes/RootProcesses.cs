using GraphAppends;
using System;
using System.Linq;

namespace Graphing
{
    internal static class RootProcesses
    {
        public static void RootProcessing(this Graph graph)
        {
            if (graph.LargestExponent == 2)
            {
                var roots = FindRootsForQuadratic(graph.Expression[0, 0], graph.Expression[1, 0], graph.Expression[2, 0]);

                graph.Root1 = roots[0];
                graph.Root2 = roots[1];
            }
            else if (graph.LargestExponent == 1)
                graph.Root1 = graph.Root2 = (int)(-graph.Expression[1, 0] / graph.Expression[0, 0]);

            //Confere se há raízes e/ou se o maior expoente é <= 2
            if (!graph.Root1.HasValue && graph.LargestExponent <= 2)
            {
                Console.WriteLine("Nao foi possivel achar nenhuma raiz");
                return;
            }

            //Converte raízes para ints regulares
            int r1 = graph.Root1 ?? default(int);
            int r2 = graph.Root2 ?? default(int);

            //Checa se as raízes estão no domínio dado
            if (Enumerable.Range(graph.Start, graph.End).Any(x => x == r1 || x == r2))
                return;

            int closest = new int(), distToClosest = new int(); ;

            var isStart = GetClosestRoot(r1, r2, graph.Start, graph.End, out closest, out distToClosest);

            Console.WriteLine($"Voce deseja imprimir ate a raiz mais proxima ({distToClosest} de distancia do alcance dado)?");

            if (!Console.ReadLine().ToLower().StartsWith('s'))
                return;

            if (isStart)
                graph.Start = closest;
            else
                graph.End = closest;

        }

        private static bool GetClosestRoot(int r1, int r2, int start, int end, out int closest, out int distToClosest)
        {
            closest = new int();

            distToClosest = new int();

            bool isStart = false;

            //Checa onde as raízes estão localizadas em relação ao domínio dado
            if (r1 > end)
            {
                distToClosest = Math.Abs(r1 - end);
                closest = r1;
            }
            else if (r2 < start)
            {
                distToClosest = Math.Abs(r2 - start);
                closest = r2;
                isStart = true;
            }
            else
            {
                if (start - r1 >= r2 - end)
                {
                    distToClosest = start - r1;
                    closest = r1;
                    isStart = true;
                }
                else
                {
                    distToClosest = r2 - end;
                    closest = r2;
                }
            }

            return isStart;
        }

        private static int?[] FindRootsForQuadratic(float a, float b, float c)
        {
            int? root1, root2;

            root1 = root2 = null;

            //Calcula o delta da função
            float delta = b * b - 4 * a * c;

            if (delta > 0)
            {
                //Pega ambas raízes
                delta = (float)Math.Sqrt(delta);
                root1 = (int)Math.Round((-b + delta) / (2 * a));
                root2 = (int)Math.Round((-b - delta) / (2 * a));

                //Organiza as raízes baseado no valor
                if (root1 > root2)
                {
                    int? m = root1;
                    root1 = root2;
                    root2 = m;
                }
            }
            else if (delta == 0)
                root1 = root2 = (int)(-b / (2 * a));

            return new int?[] { root1, root2 };
        }
    }
}
