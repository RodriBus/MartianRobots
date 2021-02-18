# .Net Code Challenge - Martian Robots

Code challenge implementation of the requirements defined in the [problem](#The-Problem) section below.

# Objectives
- Complete the challenge using C# language and a .Net program.

  For this challenge, an LTS version of .Net Core will be used as the target for the executable interface, specifically .Net Core 3.1.

  Libraries for the project provide maximum compatibility for the interface through .Net Standard 2.0 to be implemented in a wide variety of compilation targets.
- Leave margin for improvement and expansion with future implementations.
- Keep it simple. Don't over-engineer.
- User interface it's not the focus of the challenge.

# How to run

## Requirements
To compile this source code you will need to have [.Net Core 5.0 SDK](https://dotnet.microsoft.com/download) installed to take advantage of the latest C# 9.0 features.

## Execution
To run the program you can use the following command:

```
$ dotnet run --project .\src\Console\Console.csproj -c Release
```

# How to implement new instructions

In order to further implement new instructions beyond those specified in the challenge, there is an interface `IRobotActionHandler` you can implement.
The following is an example of an action implementation:

```csharp
using RodriBus.MartianRobots.Application.Abstractions;
using RodriBus.MartianRobots.Application.Abstractions.Maps;
using RodriBus.MartianRobots.Domain;

public class TurnArroundAction : IRobotActionHandler
{
	public void Execute(Robot robot, IPlanetMap map)
	{
		// Turn arround could be implemented as double 90º turns:
		var nextOrientation = map.TurnLeft(robot.Orientation);
		nextOrientation = map.TurnLeft(nextOrientation);

		robot.Orientarion = nextOrientation;
	}
}
```

# How to implement new maps

The project provides a basic rectangular map definition with some specific behaviors, like north being at Y+1.

If you were to implement other types of planets such as a spherical map where getting out of bounds would set you at the other side of the map, a tridimensional map, or a non-euclidean map with hyperbolic displacement, you could easily implement its behavior using `IPlanetMap`.

# Tests

This code contains some tests.
Test libraries used:
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq4)
- [FluentAssertions](https://fluentassertions.com/)

To run the test you can use the following command:

```
$ dotnet test RodriBus.MartianRobots.sln -c Release
```

# Remarks

This development still has a lot of room for improvements regarding code structure, build pipeline definition, and so on.

For example, manual input from the user it's not implemented yet.
The orientation system could also be improved using more fine-grained rotation instead of four whole chunks.

Still, code allows the implementation of other input interfaces such as web services besides the current console application.

# The Problem

The surface of Mars can be modeled by a rectangular grid around which robots are able to move according to instructions provided by Earth. You are to write a program that determines each sequence of robot positions and reports the final position of the robot.

A robot position consists of a grid coordinate (a pair of integers: x-coordinate followed by y-coordinate) and an orientation (N, S, E, W for north, south, east, and west). A robot instruction is a string of the letters "L", "R", and "F" which represent, respectively, the instructions:

*   Left: the robot turns left 90 degrees and remains on the current grid point.
*   Right: the robot turns right 90 degrees and remains on the current grid point.
*   Forward: the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation.

The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).

There is also a possibility that additional command types may be required in the future and provision should be made for this.

Since the grid is rectangular and bounded (...yes Mars is a strange planet), a robot that moves "off" an edge of the grid is lost forever. However, lost robots leave a robot "scent" that prohibits future robots from dropping off the world at the same grid point. The scent is left at the last grid position the robot occupied before disappearing over the edge. An instruction to move "off" the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.

## The Input

The first line of input is the upper-right coordinates of the rectangular world, the lower-left coordinates are assumed to be 0, 0.

The remaining input consists of a sequence of robot positions and instructions (two lines per robot). A position consists of two integers specifying the initial coordinates of the robot and an orientation (N, S, E, W), all separated by whitespace on one line. A robot instruction is a string of the letters "L", "R", and "F" on one line.

Each robot is processed sequentially, i.e., finishes executing the robot instructions before the next robot begins execution.

The maximum value for any coordinate is 50.

All instruction strings will be less than 100 characters in length.

## The Output

For each robot position/instruction in the input, the output should indicate the final grid position and orientation of the robot. If a robot falls off the edge of the grid the word "LOST" should be printed after the position and orientation.

### Sample Input

```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

### Sample Output

```
1 1 E
3 3 N LOST
2 3 S
```
