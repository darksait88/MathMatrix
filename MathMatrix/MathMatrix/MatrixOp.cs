using System;

namespace MathMatrix
{
    public static class MatrixOp
    {

        public static Matrix MultiplyAdamara(Matrix matrix1, Matrix matrix2)
        {
            if(matrix1.Columns != matrix2.Columns || matrix1.Rows != matrix2.Rows)
                throw new Exception("Count columns/rows matrix1 don't equals count columns/rows matrix2");
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix1.Columns);
            for(int i = 0; i < newMatrix.Rows; i++)
                for(int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i,j] = matrix1[i,j] * matrix2[i,j];
            return newMatrix;
        }
        public static Matrix MultiplyOnScalar(Matrix currentMatrix, double scalar)
        {
            Matrix newMatrix = currentMatrix.Copy();
            for(int i = 0; i < newMatrix.Rows; i++)
                for(int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i,j] = newMatrix[i, j] * scalar;
            return newMatrix;
        }
        public static Matrix SingleMatrix(int size)
        {
            Matrix matrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
                matrix[i, i] = 1;
            return matrix;
        }
    }
}
