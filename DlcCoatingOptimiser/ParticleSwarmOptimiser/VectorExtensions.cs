using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DlcCoatingOptimiser.ParticleSwarmOptimiser
{
    internal static class Vector4Extensions
    {
        public static Vector4 Multiply(this Vector4 vector, float x)
        {
            return Vector4.Multiply(vector, x);
        }

        public static Vector4 Multiply(this Vector4 vector, Vector4 vector2)
        {
            return Vector4.Multiply(vector, vector2);
        }

        public static Vector4 Add(this Vector4 vector1, Vector4 vector2)
        {
            return Vector4.Add(vector1, vector2);
        }

        public static Vector4 Add(this Vector4 vector1, Vector4 vector2, Vector4 vector3)
        {
            var vectorA = Vector4.Add(vector1, vector2);
            return Vector4.Add(vectorA, vector3);
        }
    }
}
