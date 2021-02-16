using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Abstractions.Robots;
using RodriBus.MartianRobots.Application.Extensions;
using RodriBus.MartianRobots.Domain;

namespace RodriBus.MartianRobots.Application.Robots.Actions
{
    /// <summary>
    /// Changes robot orientation to the left.
    /// </summary>
    public sealed class TurnLeftAction : IRobotActionHandler
    {
        private static TurnLeftAction CurrentInstance { get; set; }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        public static TurnLeftAction Instance => CurrentInstance ??= new TurnLeftAction();

        private TurnLeftAction()
        {
        }

        /// <summary>
        /// Executes the robor action
        /// </summary>
        public void Execute(Robot robot, IPlanetMap map)
        {
            // Troubled robots can't move
            if (robot.IsLost()) return;

            // Get current orientation
            // Change orientation
            robot.Orientation = map.TurnLeft(robot.Orientation);
        }
    }
}