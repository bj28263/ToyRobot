using ToyRobot.Console.Positioning;
using ToyRobot.Console.Toys;

namespace ToyRobot.UnitTests
{
    public class RobotTests
    {
        public static IEnumerable<object[]> GetPositions()
        {
            var positions = Enumerable.Range(0, 5).SelectMany(x => Enumerable.Range(0, 5).Select(y => new Position(x, y)));

            foreach (var position in positions)
            {
                yield return new object[] { position };
            }
        }

        [MemberData(nameof(GetPositions))]
        [Theory]
        public void SetPositionWithinBoundary(Position position)
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);

            //Act
            SUT.SetPosition(position);

            //Assert
            Assert.True(SUT.HasBeenPlaced());
            Assert.Equal(position, SUT.Position);
        }

        [InlineData(0, -1)]
        [InlineData(-2, 1)]
        [InlineData(0, 5)]
        [InlineData(5, 5)]
        [InlineData(6, 1)]
        [Theory]
        public void DontSetPositionIfOutsideBoundary(int x, int y)
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);

            //Act
            SUT.SetPosition(new Position(x, y));

            //Assert
            Assert.False(SUT.HasBeenPlaced());
            Assert.Null(SUT.Position);
        }

        [Fact]
        public void RotateNorthLeftShouldBeWest()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetDirection(Direction.North);

            //Act
            SUT.Rotate(CommandType.Left);

            //Assert
            Assert.Equal(Direction.West, SUT.Direction);
        }

        [Fact]
        public void RotateWestLeftShouldBeSouth()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetDirection(Direction.West);

            //Act
            SUT.Rotate(CommandType.Left);

            //Assert
            Assert.Equal(Direction.South, SUT.Direction);
        }

        [Fact]
        public void RotateWestRightShouldBeNorth()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetDirection(Direction.West);

            //Act
            SUT.Rotate(CommandType.Right);

            //Assert
            Assert.Equal(Direction.North, SUT.Direction);
        }

        [Fact]
        public void RotateEastRightShouldBeSouth()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetDirection(Direction.East);

            //Act
            SUT.Rotate(CommandType.Right);

            //Assert
            Assert.Equal(Direction.South, SUT.Direction);
        }

        [Fact]
        public void CanMoveNorthFrom0()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(0, 0));
            SUT.SetDirection(Direction.North);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.North, SUT.Direction);
            Assert.Equal(0, SUT.Position.X);
            Assert.Equal(1, SUT.Position.Y);
        }

        [Fact]
        public void CanMoveWestFrom0()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(0, 0));
            SUT.SetDirection(Direction.West);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.West, SUT.Direction);
            Assert.Equal(1, SUT.Position.X);
            Assert.Equal(0, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveNorthFrom4()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(4, 4));
            SUT.SetDirection(Direction.North);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.North, SUT.Direction);
            Assert.Equal(4, SUT.Position.X);
            Assert.Equal(4, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveWestFrom4()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(4, 4));
            SUT.SetDirection(Direction.West);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.West, SUT.Direction);
            Assert.Equal(4, SUT.Position.X);
            Assert.Equal(4, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveSouthFrom0()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(0, 0));
            SUT.SetDirection(Direction.South);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.South, SUT.Direction);
            Assert.Equal(0, SUT.Position.X);
            Assert.Equal(0, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveEastFrom0()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(0, 0));
            SUT.SetDirection(Direction.East);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.East, SUT.Direction);
            Assert.Equal(0, SUT.Position.X);
            Assert.Equal(0, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveSouthFrom4()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(4, 4));
            SUT.SetDirection(Direction.South);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.South, SUT.Direction);
            Assert.Equal(4, SUT.Position.X);
            Assert.Equal(3, SUT.Position.Y);
        }

        [Fact]
        public void CantMoveEastFrom4()
        {
            //Arrange
            var boundary = new Position(5, 5);
            var SUT = new Robot(boundary);
            SUT.SetPosition(new Position(4, 4));
            SUT.SetDirection(Direction.East);

            //Act
            SUT.Move();

            //Assert
            Assert.Equal(Direction.East, SUT.Direction);
            Assert.Equal(3, SUT.Position.X);
            Assert.Equal(4, SUT.Position.Y);
        }
    }
}
