using FluentAssertions;
using RodriBus.MartianRobots.Domain;
using System;
using System.ComponentModel;
using Xunit;

namespace RodriBus.MartianRobots.UnitTests.Domain
{
    public class CoordinatesTest
    {
        [Fact]
        public void ShouldImplementEquals()
        {
            // Arrange
            var arrange = new Coordinates(1, 2, 3);
            var expected = new Coordinates(1, 2, 3);

            // Assert
            arrange.Equals(expected).Should().BeTrue();
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 1, 1, 2, 3, 4)]
        [InlineData(1, 0, 0, 1, 0, 0, 2, 0, 0)]
        [InlineData(0, 1, 0, 0, 1, 0, 0, 2, 0)]
        [InlineData(0, 0, 1, 0, 0, 1, 0, 0, 2)]
        public void ShouldImplementSumOperator(int xA, int yA, int zA, int xB, int yB, int zB, int xR, int yR, int zR)
        {
            // Act
            var result = new Coordinates(xA, yA, zA) + new Coordinates(xB, yB, zB);
            // Assert
            result.Should().Be(new Coordinates(xR, yR, zR));
        }

        [Fact]
        public void ShouldImplementEqualsOperator()
        {
            // Arrange
            var arrange = new Coordinates(1, 2, 3);
            var expected = new Coordinates(1, 2, 3);

            // Act
            var result = arrange == expected;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldImplementNotEqualsOperator()
        {
            // Arrange
            var arrange = new Coordinates(1, 2, 3);
            var expected = new Coordinates(3, 2, 1);

            // Act
            var result = arrange != expected;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        [Description("The maximum value for any coordinate is 50.")]
        public void ShouldNotAllowOverMax()
        {
            // Act
            Action act = () => new Coordinates(Coordinates.Max + 1, 0, 0);

            // Assert
            act.Should().Throw<ArgumentException>();

            // Act
            act = () => new Coordinates(0, Coordinates.Max + 1, 0);

            // Assert
            act.Should().Throw<ArgumentException>();

            // Act
            act = () => new Coordinates(0, 0, Coordinates.Max + 1);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}