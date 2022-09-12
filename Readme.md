# Toy Robot
## Description
The application is a console application which accepts the following commands:
```
PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT
```
These commands move a toy robot on a table 5x5.
Place along with the X and Y positions and F being what way it's facing (north, east, south, west) must be the first command but can be run again.
Move moves the robot forward one in the direction it's facing.
Left and right rotate the robot.
Report prints the position and direction onto the console window.
## Getting Started
This project was built using Visual Studio 2022 Community and .net 6.
Open the .sln project file in Visual Studio.

There are 2 projects:

* ToyRobot.Console
* ToyRobot.UnitTests.

If you build the ToyRobot.Console project in release mode it should create the file ToyRobot.Console.exe in ./ToyRobot.Console/bin/Release/net6.0.
## Example
Run ToyRobot.Console.exe and enter the following into the console window to get the expected output.
```
PLACE 0,0,NORTH
MOVE
MOVE
LEFT
MOVE
MOVE
MOVE
RIGHT
MOVE
REPORT
```
This should output 3,3,NORTH.
## Design Decisions
I have kept the main program file to only have logic for reading commands and writing output to the console. This is because the Program class
is hard to unit test and I wanted it to only be concerned with managing the console window and have the core logic outside of this class.
I have created a Command class which takes the raw input, validates it and runs it. The run method takes an interface IToy. This makes it easy
to mock the toy in unit tests to verify methods like SetPosition or Move are called for the correct command. This also adds flexability so
if there was a requirement to use a different toy this would be easy to implement.