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
    }
}
