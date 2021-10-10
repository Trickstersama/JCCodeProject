using Features.Gameplay.Domain.ValueObjects;
using Features.Gameplay.Infrastructure;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.WorldCoordinateMother;

namespace Features.Gameplay.Tests.Editor
{
    [TestFixture]
    public class CoordinatesServiceTests
    {
        [Test]
        public void BaseTileIsLocatedInZeroPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate();
            var expectedPosition = AWorldCoordinate(0, 0);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
        
        [Test]
        public void TileOn_1_1_IsOnExpectedPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate(1,1);
            var expectedPosition = AWorldCoordinate(.5f, .75f);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
    }

    public class CoordinatesService : ICoordinateService
    {
        public WorldCoordinate ToWorldPosition(Coordinate coordinate)
        {
            return new WorldCoordinate
            {
                X = coordinate.X * .5f,
                Y = coordinate.Y * .75f
            };
        }
    }
}