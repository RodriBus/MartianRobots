using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Domain;

namespace RodriBus.MartianRobots.Application.Abstractions.Robots
{
    /// <summary>
    /// Represents a robot action handler contract.
    /// </summary>
    public interface IRobotActionHandler
    {
        /// <summary>
        /// Executes the robor action
        /// </summary>
        public void Execute(Robot robot, IPlanetMap map);
    }
}