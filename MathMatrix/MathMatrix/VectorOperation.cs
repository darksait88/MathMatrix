using System;

namespace MathMatrix
{
    public static class VectorOperation
    {
        public static double ScalarMultiplyVectors(Vector vector1, Vector vector2)
        {
            if(vector1.Count != vector2.Count)
                throw new Exception("Count element vector1 don't equals count element vector2");
            double sum = 0;
            for (int i = 0; i < vector1.Count; i++)
            {
                sum += vector1[i] * vector2[i];
            }
            return sum;
        }

        public static Vector MultiplyOnScalar(Vector vector, double scalar)
        {
            Vector newVector = vector.Copy();
            for(int i = 0; i < newVector.Count; i++)
                newVector[i] = newVector[i] * scalar;
            return newVector;
        }
    }
}