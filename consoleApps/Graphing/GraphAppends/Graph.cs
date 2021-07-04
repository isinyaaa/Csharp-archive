namespace GraphAppends
{
    public class Graph
    {
        /// <value>Expressão do gráfico</value>
        public int[,] Expression { get; set; }

        public int LargestExponent => (int)Expression?.GetValue(0, 1);

        /// <value>Valor de inicio do dominio dado</value>
        public int Start { get; set; }
        /// <value>Valor de fim do dominio dado</value>
        public int End { get; set; }

        /// <value>Menor raiz no dominio dado</value>
        public int? Root1 { get; set; } = null;
        /// <value>Maior raiz no dominio dado</value>
        public int? Root2 { get; set; } = null;
    }

}
