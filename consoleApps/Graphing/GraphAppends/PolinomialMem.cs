namespace GraphAppends
{
    /// <summary>
    /// Representa o membro de um polinomio
    /// </summary>
    public struct PolinomialMem
    {
        /// <value>Representa o sinal do coeficiente</value>
        public char Sign { get; set; }

        /// <value>Representa o coeficiente</value>
        public string Coefficient { get; set; }

        /// <value>Representa o expoente</value>
        public char Exponent { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="coefficient"></param>
        /// <param name="exponent"></param>
        public PolinomialMem(char sign, string coefficient, char exponent) { Sign = sign; Coefficient = coefficient; Exponent = exponent; }

    }
}
