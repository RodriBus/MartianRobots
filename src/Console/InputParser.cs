using RodriBus.MartianRobots.Application.Abstractions.Robots;
using RodriBus.MartianRobots.Application.Robots.Actions;
using RodriBus.MartianRobots.Console.Extensions;
using RodriBus.MartianRobots.Domain;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RodriBus.MartianRobots.Console
{
    /// <summary>
    /// Parser util to convert input strings into instructions.
    /// </summary>
    public static class InputParser
    {
        /// <summary>
        /// Max allowed instruction length.
        /// </summary>
        public const int MaxInstructionLength = 100;

        /// <summary>
        /// Parses deployment instructions into coordinates and orientarion.
        /// </summary>
        public static (Coordinates coords, Orientation orientation) ParseDeployment(string input)
        {
            // Check patter
            // N* N* X
            const string pattern = @"^(\d*)\s(\d*)\s(\w)$";
            // Should be 3 positions
            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (!match.Success || match.Groups.Count != 4)
                throw new ArgumentException($"The value '{input}' is not valid.");

            var coords = new Coordinates
            {
                X = int.Parse(match.Groups[1].Value),
                Y = int.Parse(match.Groups[2].Value),
            };
            var orientation = match.Groups[3].Value.GetOrientationFromKey();

            return (coords, orientation);
        }

        /// <summary>
        /// Parses robot instructions.
        /// </summary>
        public static IEnumerable<IRobotActionHandler> ParseInstructions(string input)
        {
            // MAX ALLOWED LENGTH 100
            if (input.Length > MaxInstructionLength)
            {
                throw new ArgumentException($"Robot instructions cannot exceed length of {MaxInstructionLength}.");
            }

            var instructions = new List<IRobotActionHandler>();

            foreach (var c in input.ToUpper())
            {
                switch (c)
                {
                    case 'L':
                        instructions.Add(TurnLeftAction.Instance);
                        break;

                    case 'R':
                        instructions.Add(TurnRightAction.Instance);
                        break;

                    case 'F':
                        instructions.Add(MoveForwardAction.Instance);
                        break;
                }
            }

            return instructions;
        }
    }
}