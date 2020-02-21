using System;
using System.Text;

namespace MathMatrix
{
    public class Matrix
    {

        private double[,] matrix;
        private bool transposed;
        private Matrix tr;
        private bool trIsUpdated;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            matrix = new double[rows, columns];
            transposed = false;
            trIsUpdated = false;
        }
        public Matrix(double[,] mass)
        {
            Rows = mass.GetLength(0);
            Columns = mass.GetLength(1);
            matrix = new double[Rows, Columns];
            CopyMassToMatrix(mass);
            transposed = false;
            trIsUpdated = false;
        }
        public double this[int row, int column]
        {
            get
            {        
                if(row >= Rows || row < 0)
                    throw new IndexOutOfRangeException($"row = {row}");
                if(column >= Columns || column < 0)
                    throw new IndexOutOfRangeException($"column = {column}");
                return matrix[row, column];
            }
            set
            {  
                if(row >= Rows || row < 0)
                    throw new IndexOutOfRangeException($"row = {row}");
                if(column >= Columns || column < 0)
                    throw new IndexOutOfRangeException($"column = {column}");
                matrix[row, column] = value;
                trIsUpdated = false;
            }
        }
        private void CopyMassToMatrix(double[,] mass)
        {
            for(int i = 0; i < Rows; i++)
                for(int j = 0; j < Columns; j++)
                    matrix[i,j] = mass[i,j];
        }
        public Matrix Copy()
        {
            Matrix newMatrix = new Matrix(Rows, Columns);
            for(int i = 0; i < Rows; i++)
                for(int j = 0; j < Columns; j++)
                    newMatrix[i,j] = matrix[i,j];
            return newMatrix;
        }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        internal bool Transposed { get => transposed; }
        public Matrix Tr 
        { 
            get
            {
                if(tr == null || !trIsUpdated)
                    TransposedMatrix();
                return tr;
            }
        }

        public void InitMatrixRandom(int from, int to, bool integer = false)
        {
            for (int i = 0; i < Rows; i++)
                for(int j = 0; j < Columns; j++)
                    matrix[i,j] = GenerateNumber(from, to, integer);
        }

        private double GenerateNumber(int from, int to, bool integer)
        {
            Random rnm = new Random();
            if(integer)
                return GenerateIntegerNumber(random: rnm, from: from, to: to);
            else
                return GenerateDoubleNumber(random: rnm, from: from, to: to);
        }

        private void TransposedMatrix()
        {
            tr = new Matrix(Columns, Rows);
            tr.transposed = true;
            for(int i = 0; i < tr.Rows; i++)
                for(int j = 0; j < tr.Columns; j++)
                    tr[i,j] = matrix[j,i];
            trIsUpdated = true;
        }

        public double[] GetRow(int index)
        {
            double[] dbl = new double[Columns];
            for(int i = 0; i < Columns; i++)
                dbl[i] = matrix[index, i];
            return dbl;
        }
        
        public double[] GetColumn(int index)
        {
            double[] dbl = new double[Rows];
            for(int i = 0; i < Rows; i++)
                dbl[i] = matrix[i, index];
            return dbl;
        }
        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            StringBuilder partText = new StringBuilder();
            if(matrix == null || Rows == 0 || Columns == 0)
                throw new Exception("Size matrix is zero or not initialize");
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    partText.Append($"{matrix[i, j].ToString("0.###"), 6} ");
                }
                text.AppendLine($"| {partText.ToString().TrimEnd()} |");
                partText.Clear();
            }
            return text.ToString();
        }
        private double GenerateDoubleNumber(Random random, int from, int to)
        {
            double num = random.Next(from, to) + random.NextDouble();
            return num;
        }
        private double GenerateIntegerNumber(Random random, int from, int to)
        {
            int num = random.Next(from, to + 1);
            return num;
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Columns != matrix2.Columns || matrix1.Rows != matrix2.Rows)
                throw new Exception("Count columns/rows matrix1 don't equals count columns/rows matrix2");
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < newMatrix.Rows; i++)
                for (int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
            return newMatrix;
        }
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Columns != matrix2.Columns || matrix1.Rows != matrix2.Rows)
                throw new Exception("Count columns/rows matrix1 don't equals count columns/rows matrix2");
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix1.Columns);
            for (int i = 0; i < newMatrix.Rows; i++)
                for (int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i, j] = matrix1[i, j] - matrix2[i, j];
            return newMatrix;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Columns != matrix2.Rows)
                throw new Exception("Count columns matrix1 don't equals count rows matrix2");
            Matrix newMatrix = new Matrix(matrix1.Rows, matrix2.Columns);
            for (int i = 0; i < newMatrix.Rows; i++)
                for (int j = 0; j < newMatrix.Columns; j++)
                    newMatrix[i, j] = VectorOperation.ScalarMultiplyVectors(new Vector(matrix1.GetRow(i)), new Vector(matrix2.GetColumn(j)));
            return newMatrix;
        }
        public static Matrix operator *(Matrix currentMatrix, Vector vector)
        {
            if (!vector.Transposed)
            {
                if (currentMatrix.Columns != vector.Count)
                    throw new Exception("Count columns matrix1 don't equals count element vector");
                Matrix newMatrix = new Matrix(currentMatrix.Rows, 1);
                for (int i = 0; i < newMatrix.Rows; i++)
                    for (int j = 0; j < newMatrix.Columns; j++)
                        newMatrix[i, j] = VectorOperation.ScalarMultiplyVectors(new Vector(currentMatrix.GetRow(i)), vector);
                return newMatrix;
            }
            else
            {
                if (currentMatrix.Columns > 1)
                    throw new Exception("Matrix have more than 1 columns");
                if (currentMatrix.Rows != vector.Count)
                    throw new Exception("Count columns matrix1 don't equals count element vector");
                double[,] mass = new double[1, vector.Count];
                for (int i = 0; i < vector.Count; i++)
                    mass[0, i] = vector[i];
                Matrix newMatrix = currentMatrix * new Matrix(mass);
                return newMatrix;
            }
        }
    }
}