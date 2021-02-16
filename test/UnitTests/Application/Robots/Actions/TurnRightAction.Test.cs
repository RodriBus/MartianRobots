using Moq;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Robots.Actions;
using RodriBus.MartianRobots.Domain;
using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.ComponentModel;
using Xunit;

namespace RodriBus.MartianRobots.UnitTests.Application.Robots.Actions
{
    public class TurnRightActionTest
    {
        [Fact]
        [Description("A robot that moves off an edge of the grid is lost forever." +
            "However, lost robots leave a robot scent that prohibits future robots from dropping off the world at the same grid point." +
            "The scent is left at the last grid position the robot occupied before disappearing over the edge." +
            "An instruction to move off the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.")]
        public void ShouldIgnoreIfRobotIsLost()
        {
            // Arrange
            var map = new Mock<IPlanetMap>();
            var robot = new Robot(Coordinates.Zero, Orientation.North);

            robot.Troubles.Add(new LostRobotTrouble());

            // Act
            TurnRightAction.Instance.Execute(robot, map.Object);

            // Assert
            map.Verify(mock => mock.TurnRight(It.IsAny<Orientation>()), Times.Never());
        }

        [Fact]
        public void ShouldMoveRobot()
        {
            // Arrange
            var map = new Mock<IPlanetMap>();
            var robot = new Robot(Coordinates.Zero, Orientation.North);

            // Act
            TurnRightAction.Instance.Execute(robot, map.Object);

            // Assert
            map.Verify(mock => mock.TurnRight(It.IsAny<Orientation>()), Times.Once());
        }
    }
}