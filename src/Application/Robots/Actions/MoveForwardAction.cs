using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Abstractions.Robots;
using RodriBus.MartianRobots.Application.Extensions;
using RodriBus.MartianRobots.Application.Maps.Landmarks;
using RodriBus.MartianRobots.Domain;
using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.Linq;

namespace RodriBus.MartianRobots.Application.Robots.Actions
{
    /// <summary>
    /// Moves a robot forward from current position.
    /// </summary>
    public sealed class MoveForwardAction : IRobotActionHandler
    {
        private static MoveForwardAction CurrentInstance { get; set; }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        public static MoveForwardAction Instance => CurrentInstance ??= new MoveForwardAction();

        private MoveForwardAction()
        {
        }

        /// <summary>
        /// Executes the robor action.
        /// </summary>
        /// <remarks>
        /// Lost robots will not move.
        /// Move to known lost places will be ignored.
        /// If a robors is lost a landmark will be added.
        /// </remarks>
        public void Execute(Robot robot, IPlanetMap map)
        {
            // Troubled robots can't move
            if (robot.IsLost()) return;

            var nextCoords = map.GetNextCoordinates(robot.Coordinates, robot.Orientation);

            // If any lost landmark in current position
            // Get next position to check if next coord it's marked as lost
            // Already recorded as lost landmarks will be avoided
            var lostLandmarks = map.GetLostLandmarks();
            var nextIsLost = lostLandmarks.TryGetValue(robot.Coordinates, out var marks)
                && marks.Any(m => m.LostRecordedCoords == nextCoords);

            if (nextIsLost) return;

            if (map.IsOutOfBounds(nextCoords))
            {
                // If next position is out of bounds, robot will be lost
                // and a lost landmark should be marked at last seen coordinates
                robot.Troubles.Add(new LostRobotTrouble());
                map.AddLandmark(robot.Coordinates, new LostLandmark(nextCoords));
            }
            else
            {
                robot.Coordinates = nextCoords;
            }
        }
    }
}