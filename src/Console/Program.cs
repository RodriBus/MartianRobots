using RodriBus.MartianRobots.Application;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Application.Maps;
using RodriBus.MartianRobots.Console;
using RodriBus.MartianRobots.Console.Extensions;
using RodriBus.MartianRobots.Domain;
using System.Collections.Generic;
using console = System.Console;

// TODO: Implement reading user input

console.WriteLine("~ AUTOMATIC EXECUTION RUNNING ~");
console.WriteLine("# WARNING #");
console.WriteLine(" - Upcoming versions will allow you to input commands!");
console.WriteLine();
console.WriteLine(">_ Running example input...");

// SET UP PLANET GRID
console.WriteLine();
console.WriteLine(">_ Discovering Mars! Rectangle grid mapped to its surface.");

const int marsHeight = 6;
const int marsWidth = 4;
var config = new PlanetMapConfiguration { Height = marsHeight, Width = marsWidth, Origin = Coordinates.Zero };
console.WriteLine($">_ Mars size: {marsHeight}x{marsWidth}");

var marsMap = new RectangleMap();
marsMap.Configure(config);

console.WriteLine($">_ Origin: {marsMap.Origin.X} {marsMap.Origin.Y}");
console.WriteLine($">_ Top coordinates: {marsMap.TopRight.X} {marsMap.TopRight.Y}");

var control = new MissionControl();
control.MapPlanet(marsMap);

var instructionPairs = new Dictionary<string, string> {
    { "1 1 E", "RFRFRFRF" },
    { "3 2 N", "FRRFLLFFRRFLL" },
    { "0 3 W", "LLFFFLFLFL" },
};

console.WriteLine(">_ Processing instructions:");
foreach (var pair in instructionPairs)
{
    console.WriteLine(pair.Key);
    console.WriteLine(pair.Value);
    var (coord, or) = InputParser.ParseDeployment(pair.Key);
    var instructions = InputParser.ParseInstructions(pair.Value);
    // SET UP NEW ROBOT
    control.DeployRobot(new Robot(coord, or));
    foreach (var instruction in instructions)
    {
        // SEND INSTRUCTION(S)
        control.CommandRobot(instruction);
    }
}

console.WriteLine();
console.WriteLine("# STATUS REPORT #");
console.WriteLine(control.GetStatusReport());
console.WriteLine(">_ Press any key to quit.");
console.ReadKey(true);