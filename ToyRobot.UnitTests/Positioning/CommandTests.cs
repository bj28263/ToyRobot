using Moq;
using ToyRobot.Console.Positioning;
using ToyRobot.Console.Toys;

namespace ToyRobot.UnitTests.Positioning
{
    public class CommandTests
    {
        [InlineData("PLACE 0,0,NORTH")]
        [InlineData("MOVE")]
        [InlineData("LEFT")]
        [InlineData("RIGHT")]
        [InlineData("REPORT")]
        [Theory]
        public void ReportsCommandsAsValid(string rawCommandText)
        {
            //Arrange
            var SUT = new Command(rawCommandText);

            //Act
            var isValid = SUT.IsValid();

            //Assert
            Assert.True(isValid);
        }

        [InlineData("PLACE")]
        [InlineData("MOV")]
        [InlineData("LEFTS")]
        [InlineData("PLACE 0,NORTH")]
        [InlineData("PLACE 0,0,NORTHEAST")]
        [Theory]
        public void ReportsCommandsAsInvalid(string rawCommandText)
        {
            //Arrange
            var SUT = new Command(rawCommandText);

            //Act
            var isValid = SUT.IsValid();

            //Assert
            Assert.False(isValid);
        }

        [Fact]
        public void ParseCommandTypePositionAndDirection()
        {
            //Arrange
            var SUT = new Command("PLACE 2,1,EAST");

            //Act
            SUT.IsValid();

            //Assert
            Assert.Equal(CommandType.Place, SUT.CommandType);
            Assert.Equal(2, SUT.Position.X);
            Assert.Equal(1, SUT.Position.Y);
            Assert.Equal(Direction.East, SUT.Direction);
        }

        [InlineData("MOVE")]
        [InlineData("LEFT")]
        [InlineData("RIGHT")]
        [InlineData("REPORT")]
        [Theory]
        public void HasCorrectCommandType(string rawCommandText)
        {
            //Arrange
            var SUT = new Command(rawCommandText);

            //Act
            SUT.IsValid();

            //Assert
            Assert.Equal(rawCommandText, SUT.CommandType.ToString().ToUpper());
        }

        [Fact]
        public void RunPlaceCommand()
        {
            //Arrange
            var SUT = new Command("PLACE 2,1,EAST");

            var toy = new Mock<IToy>();

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.SetPosition(It.Is<Position>(position => position.X == 2 && position.Y == 1)), Times.Once);
            toy.Verify(x => x.SetDirection(Direction.East), Times.Once);
        }

        [Fact]
        public void RunMoveCommand()
        {
            //Arrange
            var SUT = new Command("MOVE");

            var toy = new Mock<IToy>();
            toy.Setup(x => x.HasBeenPlaced()).Returns(true);

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.Move(), Times.Once);
        }

        [Fact]
        public void RunLeftCommand()
        {
            //Arrange
            var SUT = new Command("LEFT");

            var toy = new Mock<IToy>();
            toy.Setup(x => x.HasBeenPlaced()).Returns(true);

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.Rotate(CommandType.Left), Times.Once);
        }

        [Fact]
        public void RunRightCommand()
        {
            //Arrange
            var SUT = new Command("RIGHT");

            var toy = new Mock<IToy>();
            toy.Setup(x => x.HasBeenPlaced()).Returns(true);

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.Rotate(CommandType.Right), Times.Once);
        }

        [Fact]
        public void RunReportCommand()
        {
            //Arrange
            var SUT = new Command("REPORT");

            var toy = new Mock<IToy>();
            toy.SetupGet(x => x.Position).Returns(new Position(3, 4));
            toy.SetupGet(x => x.Direction).Returns(Direction.South);
            toy.Setup(x => x.HasBeenPlaced()).Returns(true);

            //Act
            var output = SUT.Run(toy.Object);

            //Assert
            Assert.Equal("3,4,SOUTH", output.ToUpper());
        }

        [Fact]
        public void DontMoveIfNotPlaced()
        {
            //Arrange
            var SUT = new Command("MOVE");

            var toy = new Mock<IToy>();
            toy.Setup(x => x.HasBeenPlaced()).Returns(false);

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.Move(), Times.Never);
        }

        [Fact]
        public void RePlaceIfAlreadyPlaced()
        {
            //Arrange
            var SUT = new Command("PLACE 2,1,EAST");

            var toy = new Mock<IToy>();
            toy.Setup(x => x.HasBeenPlaced()).Returns(true);

            //Act
            SUT.Run(toy.Object);

            //Assert
            toy.Verify(x => x.SetPosition(It.Is<Position>(position => position.X == 2 && position.Y == 1)), Times.Once);
            toy.Verify(x => x.SetDirection(Direction.East), Times.Once);
        }
    }
}
