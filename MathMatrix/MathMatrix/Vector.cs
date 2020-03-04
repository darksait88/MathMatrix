using System;
using System.Text;

namespace MathMatrix
{
    public class Vector
    {
        private double[,] vector;
        private Vector tr;
        private bool transposed;
        private bool trIsUpdated;
        public Vector(double[] mass)
        {
            Count = mass.Length;
            vector = new double[Count, 1];
            transposed = false;
            trIsUpdated = false;
            CopyMassToVector(mass);
        }
        public Vector(int length)
        {
            Count = length;
            vector = new double[Count, 1];
            transposed = false;
            trIsUpdated = false;
        }
        public double this[int index]
        {
            get
            {        
                if(index >= Count || index < 0)
                    throw new IndexOutOfRangeException($"index = {index}");
                return vector[index, 0];
            }
            set
            {  
                if(index >= Count || index < 0)
                    throw new IndexOutOfRangeException($"index = {index}");
                vector[index, 0] = value;
                trIsUpdated = false;
            }
        }

        public bool Transposed { get => transposed; }
        public int Count { get; private set; }
        public Vector Tr
        {
            get
            {
                if (tr == null || !trIsUpdated)
                    TransposedVector();
                return tr;
            }
        }

        private void CopyMassToVector(double[] mass)
        {
            for(int i = 0; i < Count; i++)
                vector[i,0] = mass[i];
        }

        public Vector Copy()
        {
            Vector newVector = new Vector(Count);
            for(int i = 0; i < Count; i++)
                newVector[i] = this[i];
            return newVector;
        }
        private void TransposedVector()
        {
            double[] tempMass = new double[Count];
            for(int i = 0; i < Count; i++)
                tempMass[i] = vector[i, 0]; 
            tr = new Vector(tempMass);
            tr.transposed = true;
            trIsUpdated = true;
        }
        public double EuNormVector()
        {
            double norm = 0;
            for (int i = 0; i < Count; i++)
                norm += this[i] * this[i];
            norm = Math.Sqrt(norm);
            return norm;
        }
        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            if(vector == null || Count == 0)
                throw new Exception("Length vector is zero or not initialize");
            if(Count == 1)
                return $"| {vector[0, 0]} |";
            if(transposed)
                {
                    for(int i = 0; i < Count; i++)
                    {
                        if(i == 0)
                            text.Append($"| {vector[i, 0].ToString("0.###"), 6}");
                        else if (i == Count - 1)
                            text.Append($" {vector[i, 0].ToString("0.###"), 6} |");
                        else
                            text.Append($" {vector[i, 0].ToString("0.###"), 6}");
                    }
                }
            else
                for(int i = 0; i < Count; i++)
                {
                    text.AppendLine($"| {vector[i, 0].ToString("0.###"), 6} |");
                }
            return text.ToString();
        }
        public static Matrix operator *(Vector vector, Matrix currentMatrix)
        {
            if (vector.Transposed)
            {
                if (currentMatrix.Rows != vector.Count)
                    throw new Exception("Count element vector don't equals count rows matrix1");
                Matrix newMatrix = new Matrix(1, currentMatrix.Columns);
                for (int i = 0; i < newMatrix.Columns; i++)
                    newMatrix[0, i] = VectorOp.Dot(new Vector(currentMatrix.GetColumn(i)), vector);
                return newMatrix;
            }
            else
            {
                if (currentMatrix.Rows > 1)
                    throw new Exception("Matrix have more than 1 rows");
                double[,] mass = new double[vector.Count, 1];
                for (int i = 0; i < vector.Count; i++)
                    mass[i, 0] = vector[i];
                Matrix newMatrix = new Matrix(mass) * currentMatrix;
                return newMatrix;
            }
        }
        public static Vector operator *(Vector vector1, Double scalar)
        {
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] * scalar;
            return newVector;
        }
        public static Vector operator *(Double scalar, Vector vector1) 
        {
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] * scalar;
            return newVector;
        }
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
                throw new Exception("Count element vector1 don't equals count element vector2");
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] + vector2[i];
            return newVector;
        }
        public static Vector operator +(Vector vector1, Double scalar)
        {
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] + scalar;
            return newVector;
        }
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            if (vector1.Count != vector2.Count)
                throw new Exception("Count element vector1 don't equals count element vector2");
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] - vector2[i];
            return newVector;
        }
        public static Vector operator -(Vector vector1, Double scalar)
        {
            Vector newVector = new Vector(vector1.Count);
            for (int i = 0; i < newVector.Count; i++)
                newVector[i] = vector1[i] - scalar;
            return newVector;
        }
    }
}
