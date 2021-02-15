using RodriBus.MartianRobots.Domain;

namespace RodriBus.MartianRobots.Application.Abstractions.Maps
{
    /// <summary>
    /// Planet map configuration parameters.
    /// </summary>
    public class PlanetMapConfiguration
    {
        /// <summary>
        /// Origin coordinates of the map.
        /// </summary>
        public Coordinates Origin { get; set; }

        /// <summary>
        /// Total height of the map.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Total width of the map.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Total depth of the map.
        /// </summary>
        public int Depth { get; set; }
    }
}