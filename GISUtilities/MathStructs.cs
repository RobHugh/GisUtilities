using System;

namespace GISUtilities
{
    public class Vector2
    {
        #region Fields
        public static Vector2 Zero = NewZero();
        public static Vector2 One = NewOne();

        public double x;
        public double y;
        #endregion // Fields

        #region Constructors
        public Vector2()
        {
            x = 0.0f;
            y = 0.0f;
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(double xy)
        {
            this.x = xy;
            this.y = xy;
        }

        public Vector2(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public static Vector2 NewZero()
        {
            return new Vector2(0.0f);
        }

        public static Vector2 NewOne()
        {
            return new Vector2(1.0f);
        }
        #endregion // Constructors

        #region Methods


        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 operator *(Vector2 v, double scalar)
        {
            return new Vector2(v.x * scalar, v.y * scalar);
        }

        public static Vector2 operator /(Vector2 v, double scalar)
        {
            return new Vector2(v.x / scalar, v.y / scalar);
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.x == v2.x && v1.y == v2.y;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return v1.x != v2.x || v1.y != v2.y;
        }

        public static double CrossProduct(Vector2 a, Vector2 b)
        {
            return (a.x * b.y - a.y * b.x);
        }

        public static Vector2 CrossProduct(Vector2 vec)
        {
            return new Vector2(vec.y, -vec.x);
        }

        public static double Magnitude(Vector2 point1, Vector2 point2)
        {
            var x = point2.x - point1.x;
            var y = point2.y - point1.y;
            return Math.Sqrt(x * x + y * y);
        }

        public double DotProduct(Vector2 other)
        {
            return x * other.x + y * other.y;
        }

        public Vector2 Add(Vector2 v)
        {
            x += v.x;
            y += v.y;
            return this;
        }

        public double DistanceTo(Vector2 v)
        {
            double dx = this.x - v.x;
            double dy = this.y - v.y;
            return (double)Math.Sqrt(dx * dx + dy * dy);
        }

        public double Size()
        {
            return DistanceTo(Vector2.Zero);
        }

        public Vector2 Normalize()
        {
            double size = Size();
            this.x /= size;
            this.y /= size;
            return this;
        }

        public Vector2 Clone()
        {
            return new Vector2(this);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public override bool Equals(object obj)
        {
            return this == (Vector2)obj;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }


        #endregion // Methods
    }
    /// <summary>
    /// An implementation of a 3 element vector for the use in 
    /// 3D Cartesian mathematics
    /// </summary>
    public class Vector3
    {
        #region Fields
        public static Vector3 Zero = NewZero();
        public static Vector3 One = NewOne();

        public double x;
        public double y;
        public double z; 
        #endregion // Fields

        #region Constructors
        public Vector3()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(double xyz)
        {
            this.x = xyz;
            this.y = xyz;
            this.z = xyz;
        }

        public Vector3(Vector3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public static Vector3 NewZero()
        {
            return new Vector3(0.0f);
        }

        public static Vector3 NewOne()
        {
            return new Vector3(1.0f);
        }
        #endregion // Constructors

        #region Methods
        public double DotProduct(Vector3 other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.x, -v.y, -v.z);
        }

        public static Vector3 operator *(Vector3 v, double scalar)
        {
            return new Vector3(v.x * scalar, v.y * scalar, v.z * scalar);
        }

        public static Vector3 operator /(Vector3 v, double scalar)
        {
            return new Vector3(v.x / scalar, v.y / scalar, v.z / scalar);
        }

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z;
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z;
        }

        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public Vector3 Add(Vector3 v)
        {
            x += v.x;
            y += v.y;
            z += v.z;
            return this;
        }

        public double DistanceTo(Vector3 v)
        {
            double dx = this.x - v.x;
            double dy = this.y - v.y;
            double dz = this.z - v.z;
            return (double)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public double Size()
        {
            return DistanceTo(Vector3.Zero);
        }

        public Vector3 Normalize()
        {
            double size = Size();
            this.x /= size;
            this.y /= size;
            this.z /= size;
            return this;
        }

        public Vector3 Clone()
        {
            return new Vector3(this);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        public override bool Equals(object obj)
        {
            return this == (Vector3)obj;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        #endregion // Methods
    }

    /// <summary>
    /// Base class for Matrix implementation
    /// </summary>
    public class Matrix
    {
        #region Fields
        public double[,] matrix;
        public int rows;
        public int cols;
        #endregion

        #region Constructors
        public Matrix(int rows, int cols)
        {
            this.matrix = new double[rows, cols];
            this.rows = rows;
            this.cols = cols;
        }

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
            this.rows = matrix.GetLength(0);
            this.cols = matrix.GetLength(1);
        }
        #endregion

        #region Methods
        protected static double[,] Multiply(Matrix matrix, double scalar)
        {
            int rows = matrix.rows;
            int cols = matrix.cols;
            double[,] m1 = matrix.matrix;
            double[,] m2 = new double[rows, cols];
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    m2[i, j] = m1[i, j] * scalar;
                }
            }
            return m2;
        }

        protected static double[,] Multiply(Matrix matrix1, Matrix matrix2)
        {
            int m1rows = matrix1.rows;
            int m1cols = matrix1.cols;
            int m2rows = matrix2.rows;
            int m2cols = matrix2.cols;
            if (m1cols != m2rows)
            {
                throw new ArgumentException();
            }
            double[,] m1 = matrix1.matrix;
            double[,] m2 = matrix2.matrix;
            double[,] m3 = new double[m1rows, m2cols];
            for (int i = 0; i < m1rows; ++i)
            {
                for (int j = 0; j < m2cols; ++j)
                {
                    double sum = 0;
                    for (int it = 0; it < m1cols; ++it)
                    {
                        sum += m1[i, it] * m2[it, j];
                    }
                    m3[i, j] = sum;
                }
            }
            return m3;
        }

        public static Matrix operator *(Matrix m, double scalar)
        {
            return new Matrix(Multiply(m, scalar));
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return new Matrix(Multiply(m1, m2));
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < rows; ++i)
            {
                if (i > 0)
                {
                    res += "|";
                }
                for (int j = 0; j < cols; ++j)
                {
                    if (j > 0)
                    {
                        res += ",";
                    }
                    res += matrix[i, j];
                }
            }
            return "(" + res + ")";
        }
        #endregion
    }

    public class Matrix2 : Matrix
    {
        public Matrix2() : base(2, 2) { }

        public Matrix2(double[,] matrix) : base(matrix)
        {
            if(rows != 2 || cols != 2)
            {
                throw new ArgumentException();
            }
        }

        public static Matrix2 I()
        {
            return new Matrix2(new double[,] {
                                { 1.0f, 0.0f},
                                { 0.0f, 1.0f }});
        }

        public static Vector2 operator *(Matrix2 matrix2, Vector2 v)
        {
            double[,] m = matrix2.matrix;
            return new Vector2(
                m[0, 0] * v.x + m[0, 1] * v.y ,
                m[1, 0] * v.x + m[1, 1] * v.y );
        }

        public static Matrix2 operator *(Matrix2 mat1, Matrix2 mat2)
        {
            double[,] m1 = mat1.matrix;
            double[,] m2 = mat2.matrix;
            double[,] m3 = new double[2, 2];
            m3[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0];
            m3[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1];            
            m3[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0];
            m3[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1];
            return new Matrix2(m3);
        }

        /// <summary>
        /// Multiplies a Matrix3 by a scalar
        /// </summary>
        /// <param name="m">Matrix3</param>
        /// <param name="scalar">Scalar</param>
        /// <returns>Matrix3</returns>
        public static Matrix2 operator *(Matrix2 m, double scalar)
        {
            return new Matrix2(Multiply(m, scalar));
        }
    }

    /// <summary>
    /// Implements a 3 x 3 Matrix
    /// </summary>
    public class Matrix3 : Matrix
    {
        #region Constructors
        public Matrix3()
            : base(3, 3)
        {
        }

        public Matrix3(double[,] matrix)
            : base(matrix)
        {
            if (rows != 3 || cols != 3)
            {
                throw new ArgumentException();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an Identity Matrix
        /// </summary>
        /// <returns>Identity matrix</returns>
        public static Matrix3 I()
        {
            return new Matrix3(new double[,] {
        { 1.0f, 0.0f, 0.0f },
        { 0.0f, 1.0f, 0.0f },
        { 0.0f, 0.0f, 1.0f } });
        }

        /// <summary>
        /// Multiplies a Matrix3 by a Vector3
        /// </summary>
        /// <param name="matrix3">matrix</param>
        /// <param name="v">vector</param>
        /// <returns>Vector3</returns>
        public static Vector3 operator *(Matrix3 matrix3, Vector3 v)
        {
            double[,] m = matrix3.matrix;
            return new Vector3(
                m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z,
                m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z,
                m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z);
        }

        /// <summary>
        /// Multiplies a matrix3 by a Matrix3
        /// </summary>
        /// <param name="mat1">Matrix3</param>
        /// <param name="mat2">Matrix3</param>
        /// <returns>Matrix3</returns>
        public static Matrix3 operator *(Matrix3 mat1, Matrix3 mat2)
        {
            double[,] m1 = mat1.matrix;
            double[,] m2 = mat2.matrix;
            double[,] m3 = new double[3, 3];
            m3[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0] + m1[0, 2] * m2[2, 0];
            m3[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1] + m1[0, 2] * m2[2, 1];
            m3[0, 2] = m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2] * m2[2, 2];
            m3[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0] + m1[1, 2] * m2[2, 0];
            m3[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1] + m1[1, 2] * m2[2, 1];
            m3[1, 2] = m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2] * m2[2, 2];
            m3[2, 0] = m1[2, 0] * m2[0, 0] + m1[2, 1] * m2[1, 0] + m1[2, 2] * m2[2, 0];
            m3[2, 1] = m1[2, 0] * m2[0, 1] + m1[2, 1] * m2[1, 1] + m1[2, 2] * m2[2, 1];
            m3[2, 2] = m1[2, 0] * m2[0, 2] + m1[2, 1] * m2[1, 2] + m1[2, 2] * m2[2, 2];
            return new Matrix3(m3);
        }
        
        /// <summary>
        /// Multiplies a Matrix3 by a scalar
        /// </summary>
        /// <param name="m">Matrix3</param>
        /// <param name="scalar">Scalar</param>
        /// <returns>Matrix3</returns>
        public static Matrix3 operator *(Matrix3 m, double scalar)
        {
            return new Matrix3(Multiply(m, scalar));
        }

        public static Matrix3 Transpose(Matrix3 matrix)
        {
            Matrix3 returnMatrix = new Matrix3();
            for (int i = 0; i < matrix.cols; ++i)
            {
                for (int j = 0; j < matrix.rows; ++j)
                {
                    returnMatrix.matrix[i, j] = matrix.matrix[j, i];
                }
            }

            return returnMatrix;
        }
        #endregion
    }

    /// <summary>
    /// Implementation of a 4 * 4 Matrix
    /// </summary>
    public class Matrix4 : Matrix
    {
        #region Fields
        public static Matrix4 I = NewI();
        #endregion

        #region Constructors
        public Matrix4()
            : base(4, 4)
        {
        }

        public Matrix4(double[,] matrix)
            : base(matrix)
        {
            if (rows != 4 || cols != 4)
            {
                throw new ArgumentException();
            }
        }
        #endregion

        #region Methods
        public static Matrix4 NewI()
        {
            return new Matrix4(new double[,] {
        { 1.0f, 0.0f, 0.0f, 0.0f },
        { 0.0f, 1.0f, 0.0f, 0.0f },
        { 0.0f, 0.0f, 1.0f, 0.0f },
        { 0.0f, 0.0f, 0.0f, 1.0f } });
        }

        public static Vector3 operator *(Matrix4 matrix4, Vector3 v)
        {
            double[,] m = matrix4.matrix;
            double w = m[3, 0] * v.x + m[3, 1] * v.y + m[3, 2] * v.z + m[3, 3];
            return new Vector3(
                (m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z + m[0, 3]) / w,
                (m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z + m[1, 3]) / w,
                (m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z + m[2, 3]) / w
                );
        }

        public static Matrix4 operator *(Matrix4 mat1, Matrix4 mat2)
        {
            double[,] m1 = mat1.matrix;
            double[,] m2 = mat2.matrix;
            double[,] m3 = new double[4, 4];
            m3[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0] + m1[0, 2] * m2[2, 0] + m1[0, 3] * m2[3, 0];
            m3[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1] + m1[0, 2] * m2[2, 1] + m1[0, 3] * m2[3, 1];
            m3[0, 2] = m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2] * m2[2, 2] + m1[0, 3] * m2[3, 2];
            m3[0, 3] = m1[0, 0] * m2[0, 3] + m1[0, 1] * m2[1, 3] + m1[0, 2] * m2[2, 3] + m1[0, 3] * m2[3, 3];
            m3[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0] + m1[1, 2] * m2[2, 0] + m1[1, 3] * m2[3, 0];
            m3[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1] + m1[1, 2] * m2[2, 1] + m1[1, 3] * m2[3, 1];
            m3[1, 2] = m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2] * m2[2, 2] + m1[1, 3] * m2[3, 2];
            m3[1, 3] = m1[1, 0] * m2[0, 3] + m1[1, 1] * m2[1, 3] + m1[1, 2] * m2[2, 3] + m1[1, 3] * m2[3, 3];
            m3[2, 0] = m1[2, 0] * m2[0, 0] + m1[2, 1] * m2[1, 0] + m1[2, 2] * m2[2, 0] + m1[2, 3] * m2[3, 0];
            m3[2, 1] = m1[2, 0] * m2[0, 1] + m1[2, 1] * m2[1, 1] + m1[2, 2] * m2[2, 1] + m1[2, 3] * m2[3, 1];
            m3[2, 2] = m1[2, 0] * m2[0, 2] + m1[2, 1] * m2[1, 2] + m1[2, 2] * m2[2, 2] + m1[2, 3] * m2[3, 2];
            m3[2, 3] = m1[2, 0] * m2[0, 3] + m1[2, 1] * m2[1, 3] + m1[2, 2] * m2[2, 3] + m1[2, 3] * m2[3, 3];
            m3[3, 0] = m1[3, 0] * m2[0, 0] + m1[3, 1] * m2[1, 0] + m1[3, 2] * m2[2, 0] + m1[3, 3] * m2[3, 0];
            m3[3, 1] = m1[3, 0] * m2[0, 1] + m1[3, 1] * m2[1, 1] + m1[3, 2] * m2[2, 1] + m1[3, 3] * m2[3, 1];
            m3[3, 2] = m1[3, 0] * m2[0, 2] + m1[3, 1] * m2[1, 2] + m1[3, 2] * m2[2, 2] + m1[3, 3] * m2[3, 2];
            m3[3, 3] = m1[3, 0] * m2[0, 3] + m1[3, 1] * m2[1, 3] + m1[3, 2] * m2[2, 3] + m1[3, 3] * m2[3, 3];
            return new Matrix4(m3);
        }

        public static Matrix4 operator *(Matrix4 m, double scalar)
        {
            return new Matrix4(Multiply(m, scalar));
        }
        #endregion
    }
}
