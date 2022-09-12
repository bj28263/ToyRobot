using System.Text.RegularExpressions;
using ToyRobot.Console.Toys;

namespace ToyRobot.Console.Positioning
{
    public class Command
    {
        private static readonly Regex PlaceRegex = new("(?<CommandType>PLACE) (?<X>\\d),(?<Y>\\d),(?<Direction>[A-Z]*)", RegexOptions.IgnoreCase);
        private readonly string RawInput;

        public CommandType CommandType { get; set; }
        public Position? Position { get; set; }
        public Direction? Direction { get; set; }

        public Command(string rawInput)
        {
            RawInput = rawInput;
        }

        public bool IsValid()
        {
            var match = PlaceRegex.Match(RawInput);

            // Checks if the command is a place command.
            if (match.Success)
            {
                CommandType = CommandType.Place;
                Position = new Position(int.Parse(match.Groups["X"].Value), int.Parse(match.Groups["Y"].Value));

                var isDirectionValid = Enum.TryParse(match.Groups["Direction"].Value, true, out Direction direction);

                Direction = direction;

                return isDirectionValid;
            }

            // Checks if it is a valid command other than place.
            var isCommandTypeValid = Enum.TryParse(RawInput, true, out CommandType commandType);

            if (commandType == CommandType.Place)
            {
                return false;
            }

            CommandType = commandType;

            return isCommandTypeValid;
        }

        public string? Run(IToy toy)
        {
            if (IsValid() == false)
            {
                return null;
            }

            if (toy.HasBeenPlaced() == false && CommandType != CommandType.Place)
            {
                return null;
            }

            switch (CommandType)
            {
                case CommandType.Place:
                    if (Position != null)
                    {
                        toy.SetPosition(Position);
                    }

                    if (Direction != null)
                    {
                        toy.SetDirection(Direction.Value);
                    }
                    break;
                case CommandType.Move:
                    toy.Move();
                    break;
                case CommandType.Left:
                    toy.Rotate(CommandType.Left);
                    break;
                case CommandType.Right:
                    toy.Rotate(CommandType.Right);
                    break;
                case CommandType.Report:
                    if (toy.Position == null)
                    {
                        return null;
                    }

                    return $"{toy.Position.X},{toy.Position.Y},{toy.Direction}";
                default:
                    break;
            }

            return null;
        }
    }
}
