using System;

namespace GISUtilities
{
    /// <summary>
    /// Creates a rotation matrix
    /// </summary>
    public class RotationMatrix
    {
        public Matrix3 matrix;

        public RotationMatrix()
        {
            matrix = new Matrix3();
        }

        public void MakeIdentity()
        {
            matrix = Matrix3.I();
        }

        /// <summary>
        /// Calculates a rotation matrix around the X axis
        /// Equivalent to Orientation to North
        /// </summary>
        /// <param name="thetaRadians">orientation in radians</param>
        public void MakeRotateX(double thetaRadians)
        {
            double cosTheta = Math.Cos(thetaRadians);
            double sinTheta = Math.Sin(thetaRadians);
            matrix = new Matrix3(new double[3, 3] {
                {1.0 , 0.0, 0.0},
                {0.0, cosTheta, sinTheta },
                {0.0, -sinTheta, cosTheta }
                });
        }

        /// <summary>
        /// Calculates a rotation matrix around the Y axis equivalent to a
        /// rotation in Latitude. 
        /// </summary>
        /// <param name="thetaRadians">latitude in radians</param>
        public void MakeRotateY(double thetaRadians)
        {
            double cosTheta = Math.Cos(thetaRadians);
            double sinTheta = Math.Sin(thetaRadians);
            matrix = new Matrix3(new double[3, 3] {
                {cosTheta, 0.0, sinTheta},
                {0.0, 1.0 , 0.0 },
                {-sinTheta, 0.0 , cosTheta }
                });
        }

        /// <summary>
        /// Calculates a rotation matrix around the Z axis equivalent to a 
        /// rotation in Longitude.
        /// </summary>
        /// <param name="thetaRadians">longitude in radians</param>
        public void MakeRotateZ(double thetaRadians)
        {
            double cosTheta = Math.Cos(thetaRadians);
            double sinTheta = Math.Sin(thetaRadians);
            matrix = new Matrix3(new double[3, 3] {
                {cosTheta ,sinTheta, 0.0},
                {-sinTheta,cosTheta , 0.0 },
                {0.0, 0.0, 1.0 }
                });
        }

        /// <summary>
        /// Transforms an NVector by the rotation matrix stored in this
        /// instance
        /// </summary>
        /// <param name="vec">Input Nvector</param>
        /// <returns>Output NVector</returns>
        public NVector Transform(NVector vec)
        {
            Vector3 vec3 = matrix * vec.nVec;
            NVector vec2 = new NVector(vec3.x, vec3.y, vec3.z);
            vec2.Normalize();
            return vec2;
        }
    }
}
