using RodriBus.MartianRobots.Domain;
using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.Linq;

namespace RodriBus.MartianRobots.Application.Extensions
{
    /// <summary>
    /// Robot extension methods.
    /// </summary>
    public static class RobotExtensions
    {
        /// <summary>
        /// Checks if a robot is lost based of its troubles.
        /// </summary>
        public static bool IsLost(this Robot robot) => robot.Troubles.OfType<LostRobotTrouble>().Any();
    }
}