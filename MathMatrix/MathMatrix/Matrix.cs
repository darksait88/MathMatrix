using System;
using System.Text;

namespace MathMatrix
{
    public class Matrix
    {

        private double[,] matrix;
        private bool transposed = false;
        private Matrix tr;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            matrix = new double[rows, columns];
        }
        public Matrix(double[,] mass)
        {
            Rows = mass.GetLength(0);
            Columns = mass.GetLength(1);
            matrix = new double[Rows, Columns];
            CopyMassToMatrix(mass);
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

        public double GenerateNumber(int from, int to, bool integer)
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

        public void MultiplyOnScalar(double scalar)
        {
            for(int i = 0; i < Rows; i++)
                for(int j = 0; j < Columns; j++)
                    matrix[i,j] = matrix[i, j] * scalar;
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
    }
}