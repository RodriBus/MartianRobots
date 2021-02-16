using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Abstractions.Robots;
using RodriBus.MartianRobots.Application.Extensions;
using RodriBus.MartianRobots.Domain;

namespace RodriBus.MartianRobots.Application.Robots.Actions
{
    /// <summary>
    /// Changes robot orientation to the right.
    /// </summary>
    public sealed class TurnRightAction : IRobotActionHandler
    {
        private static TurnRightAction CurrentInstance { get; set; }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        public static TurnRightAction Instance => CurrentInstance ??= new TurnRightAction();

        private TurnRightAction()
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
            robot.Orientation = map.TurnRight(robot.Orientation);
        }
    }
}