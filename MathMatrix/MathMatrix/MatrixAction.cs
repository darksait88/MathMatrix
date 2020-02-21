using System;

namespace MathMatrix
{
    public static class MatrixAction
    {
        public static Matrix MultiplyOnMatrix(Matrix currentMatrix, Matrix matrix)
        {
            if(currentMatrix.Columns != matrix.Rows)
                throw new Exception("Count columns matrix1 don't equals count rows matrix2");
            Matrix newMatrix = new Matrix(currentMatrix.Rows, matrix.Columns);
            for(int i = 0; i < newMatrix.Rows; i++)
                for(int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i,j] = VectorAction.ScalarMultiplyVectors(new Vector(currentMatrix.GetRow(i)), new Vector(matrix.GetColumn(j)));
            return newMatrix;
        }
        public static Matrix MultiplyAdamaraOnMatrix(Matrix currentMatrix, Matrix matrix)
        {
            if(currentMatrix.Columns != matrix.Columns || currentMatrix.Rows != matrix.Rows)
                throw new Exception("Count columns/rows matrix1 don't equals count columns/rows matrix2");
            Matrix newMatrix = new Matrix(currentMatrix.Rows, currentMatrix.Columns);
            for(int i = 0; i < newMatrix.Rows; i++)
                for(int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i,j] = currentMatrix[i,j] * matrix[i,j];
            return newMatrix;
        }
        public static Matrix MultiplyOnVector(Matrix currentMatrix, Vector vector)
        {
            if(!vector.Transposed)
            {
                if(currentMatrix.Columns != vector.Count)
                    throw new Exception("Count columns matrix1 don't equals count element vector");
                Matrix newMatrix = new Matrix(currentMatrix.Rows, 1);
                for(int i = 0; i < newMatrix.Rows; i++)
                    for(int j = 0; j < newMatrix.Columns ; j++)
                        newMatrix[i,j] = VectorAction.ScalarMultiplyVectors(new Vector(currentMatrix.GetRow(i)), vector);
                return newMatrix;
            }
            else
            {
                if(currentMatrix.Columns > 1)
                    throw new Exception("Matrix have more than 1 columns");
                if(currentMatrix.Rows != vector.Count)
                    throw new Exception("Count columns matrix1 don't equals count element vector");
                double[,] mass = new double[1, vector.Count];
                for(int i = 0; i < vector.Count; i++)
                    mass[0, i] = vector[i];
                Matrix newMatrix = MultiplyOnMatrix(currentMatrix, new Matrix(mass));
                return newMatrix;                
            }
        }
        public static Matrix MultiplyOnScalar(Matrix currentMatrix, double scalar)
        {
            Matrix newMatrix = currentMatrix.Copy();
            for(int i = 0; i < newMatrix.Rows; i++)
                for(int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i,j] = newMatrix[i, j] * scalar;
            return newMatrix;
        }
        public 
    }
}
