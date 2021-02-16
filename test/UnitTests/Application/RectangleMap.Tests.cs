using FluentAssertions;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Maps;
using RodriBus.MartianRobots.Domain;
using System.ComponentModel;
using Xunit;

namespace RodriBus.MartianRobots.UnitTests.Application
{
    public class RectangleMapTests
    {
        public RectangleMap Map { get; set; }

        /// <summary>
        /// Before each test.
        /// </summary>
        public RectangleMapTests()
        {
            Map = new RectangleMap();
            var config = new PlanetMapConfiguration
            {
                Origin = Coordinates.Zero,
                Height = 5,
                Width = 5,
            };
            Map.Configure(config);
        }

        [Fact]
        public void ShouldSetTopRightCoordinates()
        {
            // If grid is 5x5, origin is 0,0 and top right is 4,4
            Map.TopRight.Should().Be(new Coordinates(4, 4));
        }

        [Fact]
        [Description("The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).")]
        public void MoveNorthShouldIncrementYAxis()
        {
            // Arrange
            var initialCoords = new Coordinates(1, 1);

            // Act
            var result = Map.GetNextCoordinates(initialCoords, Orientation.North);

            // Assert
            result.Should().Be(new Coordinates(1, 2));
        }

        [Theory]
        [InlineData(Orientation.North, 1, 2)]
        [InlineData(Orientation.South, 1, 0)]
        [InlineData(Orientation.East, 2, 1)]
        [InlineData(Orientation.West, 0, 1)]
        [Description("Forward: the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation.")]
        public void GetNextCoordinatesShouldWorkProperly(Orientation orientation, int xExpected, int yExpected)
        {
            // Arrange
            var initialCoords = new Coordinates(1, 1);

            // Act
            var result = Map.GetNextCoordinates(initialCoords, orientation);

            // Assert
            result.Should().Be(new Coordinates(xExpected, yExpected));
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.West, Orientation.South)]
        [Description("Left: the robot turns left 90 degrees and remains on the current grid point.")]
        public void TurnLeftShouldWorkProperly(Orientation orientation, Orientation expected)
        {
            // Act
            var result = Map.TurnLeft(orientation);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.West, Orientation.North)]
        [Description("Right: the robot turns right 90 degrees and remains on the current grid point.")]
        public void TurnRightShouldWorkProperly(Orientation orientation, Orientation expected)
        {
            // Act
            var result = Map.TurnRight(orientation);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        [Description("The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).")]
        public void ShouldFindOutOfBoundsCoordinates()
        {
            // Arrange
            var outOfBounds = Map.TopRight + new Coordinates(1, 1);

            // Act
            var result = Map.IsOutOfBounds(outOfBounds);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        [Description("The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).")]
        public void ShouldFindAllowedCoordinates()
        {
            // Arrange
            var outOfBounds = Map.Origin + new Coordinates(1, 1);

            // Act
            var result = Map.IsOutOfBounds(outOfBounds);

            // Assert
            result.Should().BeFalse();
        }
    }
}