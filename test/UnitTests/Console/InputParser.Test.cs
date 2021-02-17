using FluentAssertions;
using RodriBus.MartianRobots.Application.Robots.Actions;
using RodriBus.MartianRobots.Console;
using RodriBus.MartianRobots.Domain;
using System;
using System.ComponentModel;
using Xunit;

namespace RodriBus.MartianRobots.UnitTests.Console
{
    public class InputParserTest
    {
        [Theory]
        [InlineData("1 2 N", 1, 2, Orientation.North)]
        [InlineData("2 3 S", 2, 3, Orientation.South)]
        [InlineData("3 4 E", 3, 4, Orientation.East)]
        [InlineData("4 5 W", 4, 5, Orientation.West)]
        public void ShouldParseDeployInstructions(string input, int x, int y, Orientation orientation)
        {
            // Act
            var result = InputParser.ParseDeployment(input);

            // Assert
            result.coords.Should().Be(new Coordinates(x, y));
            result.orientation.Should().Be(orientation);
        }

        [Theory]
        [InlineData("")]
        [InlineData("46gt s")]
        [InlineData("1 1")]
        [InlineData("11 N")]
        [InlineData("1111v")]
        public void ShouldThrowOnInvalidInput(string input)
        {
            // Arrange
            Action act = () => InputParser.ParseDeployment(input);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("L", typeof(TurnLeftAction))]
        [InlineData("R", typeof(TurnRightAction))]
        [InlineData("F", typeof(MoveForwardAction))]
        public void ShouldParseRobotInstructions(string input, Type type)
        {
            // Arrange
            var result = InputParser.ParseInstructions(input);

            // Assert
            result.Should().ContainSingle(i => i.GetType() == type);
        }

        [Fact]
        [Description("All instruction strings will be less than 100 characters in length.")]
        public void ShouldThrowIfInstructionLengthOverMax()
        {
            // Arrange
            var input = "".PadLeft(InputParser.MaxInstructionLength + 1, 'X');
            Action act = () => InputParser.ParseInstructions(input);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}