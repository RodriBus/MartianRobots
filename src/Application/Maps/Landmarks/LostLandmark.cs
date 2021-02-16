using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Domain;

namespace RodriBus.MartianRobots.Application.Maps.Landmarks
{
    /// <summary>
    /// A landmark representing last traces of a lost robot.
    /// </summary>
    public record LostLandmark : Landmark
    {
        /// <summary>
        /// Coordinates to where the lost robot was headed.
        /// </summary>
        public Coordinates LostRecordedCoords { get; }

        /// <summary>
        /// Creates an instance.
        /// </summary>
        public LostLandmark(Coordinates lostRecordedCoords)
        {
            LostRecordedCoords = lostRecordedCoords;
        }
    }
}