using RodriBus.MartianRobots.Domain;
using System;

namespace RodriBus.MartianRobots.Console.Extensions
{
    /// <summary>
    /// Orientation extension methods.
    /// </summary>
    public static class OrientationExtensions
    {
        /// <summary>
        /// Transforms an orientation to a key code.
        /// </summary>
        public static string GetKeyCode(this Orientation orientation)
            => orientation switch
            {
                Orientation.North => "N",
                Orientation.South => "S",
                Orientation.East => "E",
                Orientation.West => "W",
                _ => throw new ArgumentException(message: "Invalid enum value", paramName: nameof(orientation)),
            };

        /// <summary>
        /// Transforms a key code to an orientation.
        /// </summary>
        public static Orientation GetOrientationFromKey(this string code)
            => (code.ToUpper()) switch
            {
                "N" => Orientation.North,
                "S" => Orientation.South,
                "E" => Orientation.East,
                "W" => Orientation.West,
                _ => throw new ArgumentException(message: "Invalid orientation code value", paramName: nameof(code)),
            };
    }
}