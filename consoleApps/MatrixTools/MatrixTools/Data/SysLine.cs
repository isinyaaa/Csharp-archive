using System.Collections.Generic;
using System.Linq;

namespace MatrixTools
{
    public class SysLine
    {
        /// <value>Represents the System Line Expression</value>
        private Coefficient[] Coeficients;

        /// <value>Represents the y-intercept of the System Line Expression</value>
        public double Intercept { get; set; }

        /// <value>Represents the ammount of varibles in the System Line Expression</value>
        public int VariableAmmount => Coeficients.Count();

        #region indexer

        public Coefficient this[int variableIndex] { get => Coeficients[variableIndex]; set => Coeficients[variableIndex] = value; }

        #endregion

        #region ctor

        public SysLine(int size) => Coeficients = new List<Coefficient>(size).Select(x => x = new Coefficient()).ToArray();

        #endregion
    }
}
