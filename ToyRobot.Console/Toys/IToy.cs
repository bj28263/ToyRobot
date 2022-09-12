using ToyRobot.Console.Positioning;

namespace ToyRobot.Console.Toys
{
    public interface IToy
    {
        Position? Position { get; }

        Direction Direction { get; }

        bool HasBeenPlaced();
        
        void SetPosition(Position newPosition);

        void SetDirection(Direction newDirection);

        void Rotate(CommandType commandType);

        void Move();
    }
}
