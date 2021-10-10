using Features.Gameplay.Domain.Infrastructure;
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
        [Test]
        public void TileOn_1_0_IsOnExpectedPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate(1,0);
            var expectedPosition = AWorldCoordinate(1, 0);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
        
        [Test]
        public void TileOn_0_1_IsOnExpectedPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate(0,1);
            var expectedPosition = AWorldCoordinate(-.5f, .75f);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
        
        [Test]
        public void TileOnEvenRowIsOnExpectedPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate(10,8);
            var expectedPosition = AWorldCoordinate(10, 6);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
        
        [Test]
        public void TileOnUnevenRowIsOnExpectedPosition()
        {
            //given
            var coordinatesService = new CoordinatesService();
            var coordinate = ACoordinate(5,7);
            var expectedPosition = AWorldCoordinate(4.5f, 5.25f);
            
            //when
            var position = coordinatesService.ToWorldPosition(coordinate);
            
            //then
            Assert.AreEqual(position, expectedPosition);
        }
    }
}