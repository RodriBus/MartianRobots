using System;
using System.Diagnostics;

namespace RodriBus.MartianRobots.Domain
{
    /// <summary>
    /// Tridimensional space coordinates.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public struct Coordinates : IEquatable<Coordinates>
    {
        /// <summary>
        /// Max allowed coordinate in any axis.
        /// </summary>
        public const int Max = 50;

        /// <summary>
        /// Coordinates representing 0,0,0.
        /// </summary>
        public static Coordinates Zero => new Coordinates(0, 0, 0);

        /// <summary>
        /// X coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Creates an instance.
        /// </summary>
        public Coordinates(int x, int y, int z = 0)
        {
            ThrowIfOverLimit(x);
            ThrowIfOverLimit(y);
            ThrowIfOverLimit(z);
            X = x;
            Y = y;
            Z = z;
        }

        private static void ThrowIfOverLimit(int coord)
        {
            if (coord > Max) throw new ArgumentException($"Coordinate cannot be more than {Max}");
        }

        /// <summary>
        /// Sum operator.
        /// </summary>
        public static Coordinates operator +(Coordinates a, Coordinates b) =>
            new Coordinates(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        /// <summary>
        /// Equals operator.
        /// </summary>
        public static bool operator ==(Coordinates a, Coordinates b) => a.Equals(b);

        /// <summary>
        /// Not equals operator.
        /// </summary>
        public static bool operator !=(Coordinates a, Coordinates b) => !a.Equals(b);

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="instance">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="instance" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Coordinates instance)
        {
            return X.Equals(instance.X)
                && Y.Equals(instance.Y)
                && X.Equals(instance.X);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode()
                    ^ Y.GetHashCode()
                    ^ Z.GetHashCode();
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Coordinates instance)
                return Equals(instance);
            else
                return false;
        }

        private string DebuggerDisplay => $"X: {X}, Y: {Y}, Z: {Z}";
    }
}