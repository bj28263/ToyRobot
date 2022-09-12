using ToyRobot.Console.Positioning;
using ToyRobot.Console.Toys;

string line;
Position Boundary = new(5, 5);
Robot robot = new(Boundary);

Console.WriteLine(@"
    ###############################################################
    #                                                             #
    #                          Toy Robot                          #
    #                                                             #
    ###############################################################

    To move the robot start giving it a starting position.
    This is done by PLACE X,Y,F (F being north, east, south, west).
    Type MOVE to move the robot forward the direction it's facing.
    Type LEFT or RIGHT to rotate the robot.
    Finally you can type REPORT to get the current position and
    direction of the robot.
");

while ((line = Console.ReadLine()) != null)
{
    var command = new Command(line);
    var output = command.Run(robot);

    if (string.IsNullOrWhiteSpace(output))
    {
        continue;
    }

    Console.WriteLine(output);
}
