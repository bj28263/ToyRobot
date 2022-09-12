using ToyRobot.Console.Positioning;

namespace ToyRobot.Console.Toys
{
    public class Robot : IToy
    {
        private readonly Position Boundary;
        private Position? CurrentPosition;
        private Direction CurrentDirection;

        public Position? Position { get => CurrentPosition; }

        public Direction Direction { get => CurrentDirection; }

        public Robot(Position boundary)
        {
            Boundary = boundary;
        }

        public bool HasBeenPlaced()
        {
            return CurrentPosition != null;
        }

        public void SetPosition(Position newPosition)
        {
            var isXInvalid = newPosition.X < 0 || newPosition.X >= Boundary.X;
            var isYInvalid = newPosition.Y < 0 || newPosition.Y >= Boundary.Y;

            if (isXInvalid || isYInvalid)
            {
                return;
            }

            CurrentPosition = newPosition;
        }

        public void SetDirection(Direction newDirection)
        {
            CurrentDirection = newDirection;
        }

        public void Rotate(CommandType commandType)
        {
            var newDirection = (int)CurrentDirection;

            if (commandType == CommandType.Left)
            {
                newDirection--;

                // If the currect direction is north (0) move to west (3).
                if (newDirection < 0)
                {
                    newDirection = 3;
                }
            }
            else if (commandType == CommandType.Right)
            {
                newDirection++;

                // If the currect direction is west (3) move to north (0).
                if (newDirection > 3)
                {
                    newDirection = 0;
                }
            }

            CurrentDirection = (Direction)newDirection;
        }

        public void Move()
        {
            if (CurrentPosition == null)
            {
                return;
            }

            var newPosition = new Position(CurrentPosition.X, CurrentPosition.Y);

            switch (Direction)
            {
                case Direction.North:
                    newPosition.Y++;
                    break;
                case Direction.East:
                    newPosition.X--;
                    break;
                case Direction.South:
                    newPosition.Y--;
                    break;
                case Direction.West:
                    newPosition.X++;
                    break;
                default:
                    break;
            }

            // SetPosition checks the new position won't be off the table.
            SetPosition(newPosition);
        }
    }
}
