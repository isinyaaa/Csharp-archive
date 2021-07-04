namespace GraphAppends
{
    /// <summary>
    /// Representa um valor no gráfico
    /// </summary>
    public struct Val
    {
        /// <value>Valor do domínio</value>
        public int Xv { get; set; }

        /// <value>Valor da imagem (y)</value>
        public int Yv { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="xv"></param>
        /// <param name="yv"></param>
        public Val(int xv, int yv) { Xv = xv; Yv = yv; }
    }
}
