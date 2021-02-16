using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Maps.Landmarks;
using RodriBus.MartianRobots.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RodriBus.MartianRobots.Application.Extensions
{
    /// <summary>
    /// Map extension methods.
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Returns lost landmarks from a mao.
        /// </summary>
        public static IDictionary<Coordinates, IEnumerable<LostLandmark>> GetLostLandmarks(this IPlanetMap map)
        {
            return map.GetLandmarks()
                .Where(p => p.Value.OfType<LostLandmark>().Any())
                .ToDictionary(p => p.Key, p => p.Value.OfType<LostLandmark>());
        }
    }
}