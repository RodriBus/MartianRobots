using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.Collections.Generic;
using System.Diagnostics;

namespace RodriBus.MartianRobots.Domain
{
    /// <summary>
    /// Planetary robot.
    /// </summary>
    [DebuggerDisplay("{Coordinates.DebuggerDisplay,nq} - {Orientation,nq}")]
    public class Robot
    {
        /// <summary>
        /// Current coordinates.
        /// </summary>
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// Current orientation.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Reported troubles.
        /// </summary>
        public IList<IRobotTrouble> Troubles { get; } = new List<IRobotTrouble>();

        /// <summary>
        /// Creates an instance.
        /// </summary>
        public Robot(Coordinates coordinates, Orientation orientation)
        {
            Coordinates = coordinates;
            Orientation = orientation;
        }
    }
}