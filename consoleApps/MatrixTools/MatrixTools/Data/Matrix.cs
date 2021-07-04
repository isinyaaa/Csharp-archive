namespace MatrixTools
{
    internal class MatrixGen
    {
        #region Private Stuff

        /// <value>Stores the Matrix values</value>
        private double[][] Matrix;

        /// <value>Whether the determinant for the Matrix has already been calculated</value>
        private bool IsCalculated = false;

        /// <value>Stores the value of the Determinant</value>
        private Det DetVal { get; set; }

        #endregion

        #region Properties

        /// <value>Returns the ammount of rows in the matrix</value>
        public int RowAmmount { get; private set; }

        /// <value>Returns the ammount of columns in the matrix</value>
        public int ColumnAmmount { get; private set; }

        /// <value>Tells whether the matrix is square or not</value>
        public bool Square => RowAmmount == ColumnAmmount;

        /// <value>If is square, tells what is its size, if not, returns 0</value>
        public int Size => Square ? RowAmmount : 0;

        /// <value>Stores each column variable letter if matrix has any</value>
        public Letter[] VariableNames { get; set; }

        /// <value>Tells whether the matrix is based on a linear system</value>
        public bool BasedOnSystem { get; set; }

        /// <value>Returns the Determinant</value>
        public Det Determinant
        {
            get
            {
                if (!IsCalculated)
                {
                    DetVal = this.GetDeterminant();
                    IsCalculated = true;
                }

                return DetVal;
            }
        }

        #endregion

        #region Indexers

        public double[] this[int row]
        {
            get => Matrix[row];

            set => Matrix[row] = value;
        }

        public double this[int row, int column]
        {
            get => Matrix[row][column];

            set => Matrix[row][column] = value;
        }

        #endregion

        #region ctor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="rowAmmount"></param>
        /// <param name="columnAmmount"></param>
        public MatrixGen(int rowAmmount, int columnAmmount)
        {
            RowAmmount = rowAmmount;
            ColumnAmmount = columnAmmount;

            VariableNames = new Letter[RowAmmount];

            Matrix = new double[rowAmmount][];

            for (int i = 0; i < rowAmmount; i++)
            {
                Matrix[i] = new double[columnAmmount];

                for (int j = 0; j < columnAmmount; j++)
                {
                    Matrix[i][j] = new double();
                }
            }
        }

        /// <summary>
        /// Copies the passed Matrix
        /// </summary>
        /// <param name="oldMat"></param>
        public MatrixGen(MatrixGen oldMat) : this(oldMat.RowAmmount, oldMat.ColumnAmmount)
        {
            for (int row = 0; row < oldMat.RowAmmount; row++)
            {
                for (int column = 0; column < oldMat.ColumnAmmount; column++)
                {
                    Matrix[row][column] = oldMat[row][column];
                }
            }
        }

        /// <summary>
        /// Transposed Matrix constructor
        /// </summary>
        /// <returns>A Matrix where the lines and columns are exchanged</returns>
        public MatrixGen Transposed()
        {
            var newMat = new MatrixGen(ColumnAmmount, RowAmmount);

            // Runs through the Matrix exchanging the columns for the rows
            for (int i = 0; i < RowAmmount; i++)
            {
                for (int j = 0; j < ColumnAmmount; j++)
                {
                    newMat[j, i] = this[i, j];
                }
            }

            if (IsCalculated)
            {
                newMat.IsCalculated = IsCalculated;
                newMat.DetVal = DetVal;
            }
            
            return newMat;
        }

        #endregion
    }
}
