using System.Collections.Generic;

namespace GraphAppends
{
    /// <summary>
    /// Representa um valor de Imagem no gráfico
    /// </summary>
    public class Line
    {
        /// <value>Uma lista de int, cada qual representando 
        /// uma distância do valor de início e organizada em 
        /// ordem crescente</value>
        public List<int> Intervals { get; set; } = new List<int>();

        /// <summary>
        /// Tamanho da linha
        /// </summary>
        /// <returns>Tamanho da Linha</returns>
        public int Size() => Intervals.Count;

        /// <summary>
        /// Indexador para a lista
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int this[int index]
        {
            get => Intervals[index];

            set
            {
                //Se o índice não existir, criar
                if (index > Intervals.Count - 1)
                    Intervals.Add(value);

                //Se existir, mudar
                else
                    Intervals[index] = value;
            }
        }
    }
}
