using System.Linq;

namespace MatrixTools
{
    /// <summary>
    /// Linear System of Equations
    /// </summary>
    internal class ExpressionSystem
    {
        #region private stuff

        /// <value>Holds the Matrix representation of the System</value>
        private MatrixGen MatrixRep;

        /// <value>Tells whether the representation has already been done</value>
        private bool IsMatrixRepMade = false;

        /// <value>Holds the values of the System Expressions</value>
        private SysLine[] System;

        #endregion

        #region properties

        /// <value>Returns the ammount of lines in the System</value>
        public int LineAmmount { get; private set; }

        /// <value>Returns the ammount of coefficients (total) in the System</value>
        public int CoefficientAmmount { get; private set; }

        /// <value>Returns whether the System is solvable or not</value>
        public bool Solvable { get => LineAmmount >= CoefficientAmmount; }

        /// <value>Returns the Matrix representation of the System</value>
        public MatrixGen MatrixRepresentation
        {
            get
            {
                if (!IsMatrixRepMade)
                    MatrixRep = MatrixRepresenter();

                return MatrixRep;
            }
        }

        #endregion

        #region indexer

        public SysLine this[int line]
        {
            get => System[line];

            set => System[line] = value;
        }

        public Coefficient this[int line, int coefficient]
        {
            get => System[line][coefficient];

            set => System[line][coefficient] = value;
        }

        #endregion

        #region ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="lineAmmount"></param>
        /// <param name="coefficientAmmount"></param>
        public ExpressionSystem(int lineAmmount, int coefficientAmmount)
        {
            // Initializes the ammounts
            LineAmmount = lineAmmount;
            CoefficientAmmount = coefficientAmmount;

            // Initializes the current Linear System
            System = new SysLine[lineAmmount];

            for (int i = 0; i < coefficientAmmount; i++)
            {
                System[i] = new SysLine(coefficientAmmount);
            }
        }

        /// <summary>
        /// Constructor from a Matrix
        /// </summary>
        /// <param name="sysLines"></param>
        public ExpressionSystem(MatrixGen sysLines) : this(sysLines.RowAmmount, sysLines.ColumnAmmount)
        {
            // Copies the Matrix
            for (int i = 0; i < LineAmmount; i++)
            {
                for (int j = 0; j < CoefficientAmmount; j++)
                {
                    this[i, j].Value = sysLines[i, j];
                }
            }
        }

        #endregion

        #region methods
        
        /// <summary>
        /// Gets the Matrix representation of the current System
        /// </summary>
        /// <returns></returns>
        private MatrixGen MatrixRepresenter()
        {
            // Sets the Matrix representation as made
            IsMatrixRepMade = true;

            // Initializes a new Matrix
            var newMat = new MatrixGen(LineAmmount, CoefficientAmmount);

            // Sets the values in this Matrix
            for (int curLine = 0; curLine < LineAmmount; curLine++)
            {
                for (int curCoef = 0; curCoef < CoefficientAmmount; curCoef++)
                {
                    newMat[curLine][curCoef] = System[curLine][curCoef].Value;
                }
            }

            return newMat;
        }

        #endregion
    }
}
