using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RodriBus.MartianRobots.Application.Maps
{
    /// <summary>
    /// A rectangle map with limited bounds. North is considered to be Y + 1.
    /// </summary>
    /// <remarks>
    /// <code>Planet coordinates are handled as:
    ///
    /// Y           .TopRight
    /// |
    /// |
    /// |
    /// |_ _ _ _ _ _ X
    /// 0
    ///
    ///   N
    /// W   E
    ///   S
    /// </code>
    /// </remarks>
    public class RectangleMap : IPlanetMap
    {
        /// <summary>
        /// Origin coordinates.
        /// </summary>
        public Coordinates Origin { get; private set; }

        /// <summary>
        /// Top right coordinates.
        /// </summary>
        public Coordinates TopRight { get; private set; }

        private readonly IDictionary<Coordinates, List<Landmark>> Landmarks = new Dictionary<Coordinates, List<Landmark>>();
        private bool Configured { get; set; }

        /// <summary>
        /// Configures the current map.
        /// </summary>
        public void Configure(PlanetMapConfiguration configuration)
        {
            TopRight = new Coordinates(configuration.Height - 1, configuration.Width - 1);
            Origin = configuration.Origin;
            Configured = true;
        }

        /// <summary>
        /// Adds a landmark to the map
        /// </summary>
        public void AddLandmark(Coordinates coordinates, Landmark landmark)
        {
            if (!Landmarks.ContainsKey(coordinates))
            {
                Landmarks.Add(coordinates, new List<Landmark>());
            }
            Landmarks[coordinates].Add(landmark);
        }

        /// <summary>
        /// Gets map landmarks.
        /// </summary>
        public IDictionary<Coordinates, IEnumerable<Landmark>> GetLandmarks() => Landmarks.ToDictionary(l => l.Key, l => l.Value.AsEnumerable());

        /// <summary>
        /// Calculates the next coordinates increment from current position and orientation.
        /// </summary>
        public Coordinates GetNextCoordinates(Coordinates coordinates, Orientation orientation)
        {
            ThrowIfNotConfigured();

            // North is Y+1, not X+1
            // X axis for W-E
            // Y axis for N-S
            const int chunkAdvances = 1;

            var xAmmount = orientation == Orientation.East || orientation == Orientation.West ? chunkAdvances : 0;
            var yAmmount = orientation == Orientation.North || orientation == Orientation.South ? chunkAdvances : 0;

            // South or West are decrements in coordinate
            if (orientation == Orientation.South)
                yAmmount *= -1;

            if (orientation == Orientation.West)
                xAmmount *= -1;

            return coordinates + new Coordinates(xAmmount, yAmmount);
        }

        /// <summary>
        /// Returns if given coordinates are out of bounds.
        /// </summary>
        public bool IsOutOfBounds(Coordinates coordinates)
        {
            ThrowIfNotConfigured();
            if (coordinates.X < Origin.X || coordinates.X > TopRight.X) return true;
            if (coordinates.Y < Origin.Y || coordinates.Y > TopRight.Y) return true;
            return false;
        }

        /// <summary>
        /// Calculates next orientation 90º to left.
        /// </summary>
        public Orientation TurnLeft(Orientation orientation)
            // Clockwise -1
            => Turn(orientation, -1);

        /// <summary>
        /// Calculates next orientation 90º to right.
        /// </summary>
        public Orientation TurnRight(Orientation orientation)
            // Clockwise +1
            => Turn(orientation, 1);

        private Orientation Turn(Orientation orientation, int times)
        {
            ThrowIfNotConfigured();

            // Turn n times, clockwise
            var min = (int)Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Min();
            var max = (int)Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Max();

            var next = (int)orientation + times;
            if (next > max) next = min;
            if (next < min) next = max;

            return (Orientation)next;
        }

        private void ThrowIfNotConfigured()
        {
            if (!Configured)
            {
                throw new InvalidOperationException("Map is not configured yet. You should call Configure first.");
            }
        }
    }
}