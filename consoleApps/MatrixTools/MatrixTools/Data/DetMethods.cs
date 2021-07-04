using System;
using System.Collections.Generic;
using System.Linq;

namespace MatrixTools
{
    /// <summary>
    /// Methods for finding the Determinant
    /// </summary>
    internal static class DetMethods
    {
        #region accessible methods

        /// <summary>
        /// Swaps two rows or two columns
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        internal static MatrixGen ChangeRowsOrColumns(this MatrixGen matrix, int line1, int line2, Line line)
        {
            // Copies Matrix
            var newMatrix = new MatrixGen(matrix);

            // Changes lines
            if (line == Line.Row)
            {
                newMatrix[line1] = matrix[line2];
                newMatrix[line2] = matrix[line1];
            }

            for (int row = 0; row < matrix.RowAmmount; row++)
            {
                newMatrix[row][line1] = matrix[row][line2];
                newMatrix[row][line2] = matrix[row][line1];
            }

            // Changes Matrix signal
            for (int i = 0; i < newMatrix.RowAmmount; i++)
            {
                for (int j = 0; j < newMatrix.ColumnAmmount; j++)
                {
                    newMatrix[i, j] = -newMatrix[i, j];
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// Jacobi Theorem
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="refCol"></param>
        /// <param name="targetCol"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static MatrixGen JacobiTheorem(this MatrixGen matrix, int refCol, int targetCol, double val)
        {
            // Checkes whether the reference column is different from the target column
            if (refCol == targetCol)
                return matrix;

            // Copies the Matrix
            var newMat = new MatrixGen(matrix);

            // Applies the Theorem
            for (int row = 0; row < matrix.ColumnAmmount; row++)
            {
                newMat[row, targetCol] = newMat[row, refCol] * val + newMat[row, targetCol];
            }

            return newMat;
        }

        #endregion

        #region Getting the Determinant

        /// <summary>
        /// Gets the determinant
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Determinant</returns>
        internal static Det GetDeterminant(this MatrixGen matrix)
        {
            // If Matrix is not square then it's not possible to find its Determinant
            if (!matrix.Square)
                return new Det(false);

            // If the square Matrix size is 1, then the determinant is its only element
            if (matrix.Size == 1)
                return new Det(true, matrix[0, 0]);

            var mostZeros = GetMostZeros(matrix);

            // If the ammount of zeroed places in the matrix is the same as its size, then its Determinant must be zero
            if (mostZeros.Ammount == matrix.Size)
                return new Det(true, 0);

            // Initializes the Matrix Determinant
            var det = new double();

            // Finds its value by running the Laplacian Theorem
            foreach (var cofactor in LaplaceTheorem(matrix, mostZeros))
            {
                det += cofactor;
            }

            return new Det(true, det);
        }

        /// <summary>
        /// Iterable Laplace Theorem
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="mostZeros"></param>
        /// <returns>Iterable cofactors</returns>
        private static IEnumerable<double> LaplaceTheorem(this MatrixGen matrix, ZeroLocation mostZeros)
        {
            // Runs through one dimension of the Matrix
            for (int i = 0; i < matrix.Size; i++)
            {
                // Initializes the current row/column index
                int row = 0, column = 0;

                // If most zeros are located in a row
                if (mostZeros.Location == Line.Row)
                {
                    // Then the current row is going to be that in which those zeros are in
                    row = mostZeros.Index;

                    // And the column is going to be changed as we go through the for loop
                    column = i;
                }
                else
                {
                    // Same thing, but opposite...
                    row = i;
                    column = mostZeros.Index;
                }

                // If we are in a zero, then no operations must be done and the result is zero
                if (matrix[row, column] == 0)
                    yield return 0;

                // Else, return the Cofactor times the current number in the Matrix
                yield return matrix[row, column] * Cofactor(matrix, row, column);
            }

            // When the loop ends, break it
            yield break;
        }

        /// <summary>
        /// Gets the Matrix Cofactor
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="rowToRemove"></param>
        /// <param name="columnToRemove"></param>
        /// <returns></returns>
        private static double Cofactor(this MatrixGen matrix, int rowToRemove, int columnToRemove)
        {
            // Initializes a new, smaller Matrix
            var cofMat = new MatrixGen(matrix.Size - 1, matrix.Size - 1);

            // Holds whether the loops have gone through the row we want to skip
            var passedRow = 0;

            // Gets the Matrix Minor
            for (int i = 0; i < matrix.Size; i++)
            {
                if (i == rowToRemove)
                {
                    passedRow = -1;
                    continue;
                }

                // Holds whether the loops have gone through the column we want to skip
                var passedColumn = 0;

                for (int j = 0; j < matrix.Size; j++)
                {
                    if (j == columnToRemove)
                    {
                        passedColumn = -1;
                        continue;
                    }

                    cofMat[i + passedRow, j + passedColumn] = matrix[i, j];
                }
            }

            // Gets the Cofactor
            return Math.Pow(-1, (rowToRemove + columnToRemove)) * cofMat.Determinant.Value;
        }

        /// <summary>
        /// Gets the row/ column where there are most zeros located in the Matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>A ZeroLocation struct</returns>
        private static ZeroLocation GetMostZeros(this MatrixGen matrix)
        {
            // Initializes the variables that will store the ammount of zeros in rows and columns...
            int[] zeroCountInColumns, zeroCountInRows;
            zeroCountInColumns = zeroCountInRows = new int[matrix.Size];

            for (int i = 0; i < matrix.RowAmmount; i++)
            {
                zeroCountInColumns[i] = zeroCountInRows[i] = new int();
            }

            // Now we must get the ammount of zeros in each row/ column
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = 0; j < matrix.Size; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        zeroCountInRows[i]++;
                        zeroCountInColumns[j]++;
                    }
                }
            }

            // Gets the maximum ammount in rows/ columns
            var maxInRows = zeroCountInRows.Max();
            var maxInColumns = zeroCountInColumns.Max();

            // Gets the column/row with the largest ammount of zeros
            var biggestInColumns = Array.FindIndex(zeroCountInColumns, x => maxInColumns == x);
            var biggestInRows = Array.FindIndex(zeroCountInRows, x => maxInColumns == x);

            // Returns the value that has the biggest coverage of zeros
            return matrix.ColumnAmmount - maxInColumns < matrix.RowAmmount - maxInRows
                ? new ZeroLocation(Line.Column, biggestInColumns, maxInColumns)
                : new ZeroLocation(Line.Row, biggestInRows, maxInRows);
        }

        #endregion
    }
}
