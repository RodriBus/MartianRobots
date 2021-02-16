using FluentAssertions;
using Moq;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Maps.Landmarks;
using RodriBus.MartianRobots.Application.Robots.Actions;
using RodriBus.MartianRobots.Domain;
using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace RodriBus.MartianRobots.UnitTests.Application.Robots.Actions
{
    public class MoveForwardActionTest
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
            MoveForwardAction.Instance.Execute(robot, map.Object);

            // Assert
            map.Verify(mock => mock.TurnLeft(It.IsAny<Orientation>()), Times.Never());
        }

        [Fact]
        public void ShouldMoveRobot()
        {
            // Arrange
            var map = new Mock<IPlanetMap>();
            var initialPosition = Coordinates.Zero;
            var robot = new Robot(initialPosition, Orientation.North);
            var destination = new Coordinates(0, 1);
            var lostLandmark = new LostLandmark(destination);

            map.Setup(m => m.GetNextCoordinates(It.IsAny<Coordinates>(), It.IsAny<Orientation>()))
                .Returns(() => destination);

            map.Setup(m => m.GetLandmarks())
                .Returns(() => new Dictionary<Coordinates, IEnumerable<Landmark>> { { robot.Coordinates, new Landmark[0] } });

            // Act
            MoveForwardAction.Instance.Execute(robot, map.Object);

            // Assert
            // Robot should move
            robot.Coordinates.Should().Be(destination);
        }

        [Fact]
        [Description("An instruction to move off the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.")]
        public void ShouldIgnoreIfRobotWillBeLost()
        {
            // Arrange
            var map = new Mock<IPlanetMap>();
            var initialPosition = Coordinates.Zero;
            var robot = new Robot(initialPosition, Orientation.North);
            var destination = new Coordinates(0, 1);
            var lostLandmark = new LostLandmark(destination);

            map.Setup(m => m.GetNextCoordinates(It.IsAny<Coordinates>(), It.IsAny<Orientation>()))
                .Returns(() => destination);

            map.Setup(m => m.GetLandmarks())
                .Returns(() => new Dictionary<Coordinates, IEnumerable<Landmark>> { { robot.Coordinates, new[] { lostLandmark } } });

            // Act
            MoveForwardAction.Instance.Execute(robot, map.Object);

            // Assert
            // Robot should not move
            robot.Coordinates.Should().Be(initialPosition);
        }

        [Fact]
        [Description("The scent is left at the last grid position the robot occupied before disappearing over the edge.")]
        public void ShouldMarkLostLandmark()
        {
            // Arrange
            var map = new Mock<IPlanetMap>();
            var initialPosition = Coordinates.Zero;
            var robot = new Robot(initialPosition, Orientation.North);
            var destination = new Coordinates(0, 1);
            var lostLandmark = new LostLandmark(destination);

            map.Setup(m => m.GetNextCoordinates(It.IsAny<Coordinates>(), It.IsAny<Orientation>()))
                .Returns(() => destination);

            map.Setup(m => m.GetLandmarks())
                .Returns(() => new Dictionary<Coordinates, IEnumerable<Landmark>> { { robot.Coordinates, new Landmark[0] } });

            map.Setup(m => m.IsOutOfBounds(It.IsAny<Coordinates>()))
                .Returns(() => true);

            // Act
            MoveForwardAction.Instance.Execute(robot, map.Object);

            // Assert
            // Robot should have trouble
            // Landmark should be added
            robot.Troubles.OfType<LostRobotTrouble>().Should().HaveCount(1);
            map.Verify(mock => mock.AddLandmark(It.IsAny<Coordinates>(), It.IsAny<Landmark>()), Times.Once());
        }
    }
}