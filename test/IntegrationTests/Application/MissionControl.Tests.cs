using FluentAssertions;
using RodriBus.MartianRobots.Application;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Abstractions.Robots;
using RodriBus.MartianRobots.Application.Maps;
using RodriBus.MartianRobots.Application.Robots.Actions;
using RodriBus.MartianRobots.Domain;
using RodriBus.MartianRobots.Domain.RobotTroubles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace RodriBus.MartianRobots.IntegrationTests.Application
{
    public class MissionControlTests
    {
        [Fact]
        [Description(@"
            INPUT
            5 3
            1 1 E
            RFRFRFRF
            3 2 N
            FRRFLLFFRRFLL
            0 3 W
            LLFFFLFLFL

            OUTPUT
            1 1 E
            3 3 N LOST
            2 3 S")]
        public void ShouldComputeInstructions()
        {
            // Arrange
            var map = new RectangleMap();
            map.Configure(new PlanetMapConfiguration { Height = 5 + 1, Width = 3 + 1, Origin = Coordinates.Zero });

            var pairs = new Dictionary<Robot, IRobotActionHandler[]>
            {
                {
                    new Robot(new Coordinates(1, 1), Orientation.East),
                    new IRobotActionHandler[] {
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                    }
                },
                {
                    new Robot(new Coordinates(3, 2), Orientation.North),
                    new IRobotActionHandler[] {
                        MoveForwardAction.Instance,
                        TurnRightAction.Instance,
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                        TurnLeftAction.Instance,
                        TurnLeftAction.Instance,
                        MoveForwardAction.Instance,
                        MoveForwardAction.Instance,
                        TurnRightAction.Instance,
                        TurnRightAction.Instance,
                        MoveForwardAction.Instance,
                        TurnLeftAction.Instance,
                        TurnLeftAction.Instance,
                    }
                },
                {
                    new Robot(new Coordinates(0, 3),Orientation.West),
                    new IRobotActionHandler[] {
                        TurnLeftAction.Instance,
                        TurnLeftAction.Instance,
                        MoveForwardAction.Instance,
                        MoveForwardAction.Instance,
                        MoveForwardAction.Instance,
                        TurnLeftAction.Instance,
                        MoveForwardAction.Instance,
                        TurnLeftAction.Instance,
                        MoveForwardAction.Instance,
                        TurnLeftAction.Instance}
                },
            };

            // Act
            var control = new MissionControl();
            control.MapPlanet(map);

            foreach (var pair in pairs)
            {
                control.DeployRobot(pair.Key);
                foreach (var instruction in pair.Value)
                {
                    control.CommandRobot(instruction);
                }
            }

            // Assert
            control.Robots
                .Should()
                .HaveCount(3);

            control.Robots
                .Should()
                .ContainSingle(r => r.Coordinates == new Coordinates(1, 1, 0) && r.Orientation == Orientation.East);

            control.Robots
                .Should()
                .ContainSingle(r => r.Coordinates == new Coordinates(3, 3, 0) && r.Orientation == Orientation.North
                    && r.Troubles.OfType<LostRobotTrouble>().Any());

            control.Robots
                .Should()
                .ContainSingle(r => r.Coordinates == new Coordinates(2, 3, 0) && r.Orientation == Orientation.South);
        }
    }
}