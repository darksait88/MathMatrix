using System;

namespace MathMatrix
{
    public static class VectorOp
    {
        public static double Dot(Vector vector1, Vector vector2)
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
    }
}